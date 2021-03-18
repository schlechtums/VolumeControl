using AudioSwitcher.AudioApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolumeControl.ViewModel.Types.Observers
{
	internal class AudioDeviceVolumeChangedObserver : IObserver<DeviceVolumeChangedArgs>
	{
		public void OnCompleted()
		{
		}

		public void OnError(Exception error)
		{
			throw error;
		}

		public void OnNext(DeviceVolumeChangedArgs value)
		{
			var advc = this.AudioDeviceVolumeChanged;
			if (advc != null)
				advc(value);
		}

		public delegate void AudioDeviceChangedHandler(DeviceVolumeChangedArgs args);
		public event AudioDeviceChangedHandler AudioDeviceVolumeChanged;
	}
}