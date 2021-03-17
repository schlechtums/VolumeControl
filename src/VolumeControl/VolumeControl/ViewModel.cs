using AudioSwitcher.AudioApi;
using AudioSwitcher.AudioApi.CoreAudio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolumeControl
{
    public class ViewModel : INotifyPropertyChanged
    {
        public ViewModel()
        {
            this._AudioController = new CoreAudioController();
            this.CurrentDevice = this._AudioController.GetDefaultDevice(DeviceType.Playback, Role.Multimedia);

            var audioDeviceChangedHandler = new AudioDeviceChangedObserver();
            audioDeviceChangedHandler.AudioDeviceChanged += AudioDeviceChangedHandler_AudioDeviceChanged;

            this._AudioController.AudioDeviceChanged.Subscribe(audioDeviceChangedHandler);
        }

        private CoreAudioController _AudioController;
        #region <<< Properties >>>
        private int _DesiredVolume = new Random().Next(1, 100);
        public int DesiredVolume
        {
            get { return this._DesiredVolume; }
            set
            {
                if (this._DesiredVolume != value)
                {
                    if (value >= 0 && value <= 100)
                    {
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

        private String _Messages = "";
        public String Messages
        {
            get { return this._Messages; }
            set
            {
                if (this._Messages != value)
                {
                    this._Messages = value;
                    this.RaisePropertyChanged(nameof(Messages));
                }
            }
        }
        #endregion

        #region <<< Methods >>>
        private void AudioDeviceChangedHandler_AudioDeviceChanged(DeviceChangedArgs args)
        {
            this.CurrentDevice = args.Device;
            this.AddMessage($"Switched to: {this.CurrentDevice.FullName} with volume: {this.CurrentDevice.Volume}");
            
            if (this.CurrentDevice.Volume != this.DesiredVolume)
            {
                this.ChangeSelectedDeviceVolumeToDesiredVolume();
            }
        }

        private void ChangeSelectedDeviceVolumeToDesiredVolume()
        {
            this.AddMessage($"Changing volume to: {this.DesiredVolume}");
            this.CurrentDevice.Volume = this.DesiredVolume;
        }

        public void AddMessage(String msg)
        {
            this.Messages = $"{msg}{Environment.NewLine}{this.Messages}";
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