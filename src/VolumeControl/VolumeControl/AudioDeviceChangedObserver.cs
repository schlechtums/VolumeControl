using AudioSwitcher.AudioApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolumeControl
{
	internal class AudioDeviceChangedObserver : IObserver<DeviceChangedArgs>
	{
		public void OnCompleted()
		{
		}

		public void OnError(Exception error)
		{
			throw error;
		}

		public void OnNext(DeviceChangedArgs value)
		{
			var adc = this.AudioDeviceChanged;
			if (adc != null)
				adc(value);
		}

		public delegate void AudioDeviceChangedHandler(DeviceChangedArgs args);
		public event AudioDeviceChangedHandler AudioDeviceChanged;
	}
}