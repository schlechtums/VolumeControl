using AudioSwitcher.AudioApi;
using AudioSwitcher.AudioApi.CoreAudio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolumeControl.ViewModel.Types.Loggers;
using VolumeControl.ViewModel.Types.Observers;

namespace VolumeControl.ViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {
        public ViewModel(ILogger logger)
        {
            this._Logger = logger;

            this._ChangingDevice = true;
            try
            {
                this._AudioController = new CoreAudioController();
                this.CurrentDevice = this._AudioController.GetDefaultDevice(DeviceType.Playback, Role.Multimedia);
                this._SelectedPlaybackDevice = this.CurrentDevice;
                this.RaisePropertyChanged(nameof(SelectedPlaybackDevice));
                this.DesiredVolume = (int)this.CurrentDevice.Volume;

                this.RegisterVolumeChangedObserver();

                var audioDeviceChangedHandler = new AudioDeviceChangedObserver();
                audioDeviceChangedHandler.AudioDeviceChanged += this.AudioDeviceChangedHandler_AudioDeviceChanged;

                this._AudioController.AudioDeviceChanged.Subscribe(audioDeviceChangedHandler);

                this.PlaybackDevices = this._AudioController
                                           .GetDevices(DeviceType.Playback)
                                           .Where(d => d.State == DeviceState.Active)
                                           .ToList();
            }
            finally
            {
                this._ChangingDevice = false;
                this._InConstructor = false;
            }
        }

        private CoreAudioController _AudioController;
        private ILogger _Logger;

        private Boolean _ChangingDevice = false;
        private Boolean _ChangingDeviceVolume = false;
        private Boolean _InConstructor = true;
        private IDisposable _VolumeChangedListener;
        private AudioDeviceVolumeChangedObserver _VolumeChangedObserver;

        #region <<< Properties >>>
        private int _DesiredVolume;
        public int DesiredVolume
        {
            get { return this._DesiredVolume; }
            set
            {
                if (this._DesiredVolume != value)
                {
                    if (value >= 0 && value <= 100)
                    {
                        if (!this._ChangingDevice)
                            this.LogMessage($"Changing desired volume to: {value}");

                        this._DesiredVolume = value;
                        this.RaisePropertyChanged(nameof(DesiredVolume));

                        if (this.CurrentDevice != null)
                            this.ChangeSelectedDeviceVolumeToDesiredVolume();
                    }
                }
            }
        }

        private IDevice _CurrentDevice;
        public IDevice CurrentDevice
        {
            get { return this._CurrentDevice; }
            set
            {
                if (this._CurrentDevice != value)
                {
                    this._CurrentDevice = value;
                    this.RaisePropertyChanged(nameof(CurrentDevice));
                }
            }
        }

        public List<CoreAudioDevice> PlaybackDevices { get; set; }

        private IDevice _SelectedPlaybackDevice;
        public IDevice SelectedPlaybackDevice
        {
            get { return this._SelectedPlaybackDevice; }
            set
            {
                if (this._SelectedPlaybackDevice != value)
                {
                    this._SelectedPlaybackDevice = value;
                    this.RaisePropertyChanged(nameof(SelectedPlaybackDevice));

                    this.ChangeDevice();
                }
            }
        }
        #endregion

        #region <<< Methods >>>
        private void AudioDeviceChangedHandler_AudioDeviceChanged(DeviceChangedArgs args)
        {
            //two events were being raised, one for the newly selected device and the previously selected device
            if (args.Device.IsDefaultDevice)
            {
                this._ChangingDevice = true;
                try
                {
                    this.UnregisterVolumeChangedObserver();

                    this.CurrentDevice = args.Device;
                    this.LogMessage($"Switched to: {this.CurrentDevice.FullName} with existing volume: {this.CurrentDevice.Volume}");

                    if (this.CurrentDevice.Volume != this.DesiredVolume)
                    {
                        this.ChangeSelectedDeviceVolumeToDesiredVolume();
                    }

                    this.RegisterVolumeChangedObserver();
                }
                finally
                {
                    this._ChangingDevice = false;
                }
            }
        }

        private void ChangeSelectedDeviceVolumeToDesiredVolume()
        {
            if (!this._ChangingDeviceVolume && !this._InConstructor)
            {
                this._ChangingDeviceVolume = true;
                try
                {
                    this.LogMessage($"Changing volume to: {this.DesiredVolume}");
                    this.CurrentDevice.Volume = this.DesiredVolume;
                }
                finally
                {
                    this._ChangingDeviceVolume = false;
                }
            }
        }

        private void RegisterVolumeChangedObserver()
        {
            var advco = new AudioDeviceVolumeChangedObserver();
            this._VolumeChangedObserver = advco;
            advco.AudioDeviceVolumeChanged += this.Advco_AudioDeviceVolumeChanged;
            this._VolumeChangedListener = this.CurrentDevice.VolumeChanged.Subscribe(advco);
        }

        private void Advco_AudioDeviceVolumeChanged(DeviceVolumeChangedArgs args)
        {
            if (!this._ChangingDevice && !this._ChangingDeviceVolume)
            {
                this._ChangingDeviceVolume = true;
                try
                {
                    this.DesiredVolume = (int)args.Volume;
                }
                finally
                {
                    this._ChangingDeviceVolume = false;
                }
            }
        }

        private void UnregisterVolumeChangedObserver()
        {
            this._VolumeChangedObserver.AudioDeviceVolumeChanged -= this.Advco_AudioDeviceVolumeChanged;
            this._VolumeChangedListener.Dispose();
        }

        public void LogMessage(String msg)
        {
            this._Logger.Log(msg);
        }

        public void ChangeDevice()
        {
            this._AudioController.SetDefaultDevice(this.SelectedPlaybackDevice);
        }
        #endregion

        #region <<< INotifyPropertyChanged >>>
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual Boolean RaisePropertyChanged(String propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));

                return true;
            }

            return false;
        }
        #endregion
    }
}