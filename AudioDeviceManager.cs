using AudioSwitcher.AudioApi;
using AudioSwitcher.AudioApi.CoreAudio;
using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace NAudio_Wiki_Practise
{
	public class AudioDeviceManager
	{
		public static event EventHandler<EventArgs> DefaultDeviceChanged;

		public AudioDeviceManager()
		{
			MMDeviceEnumerator enumerator = new NAudio.CoreAudioApi.MMDeviceEnumerator();
			// Hook to the actual event
			notificationClient = new AudioNotificationClient();
			notificationClient.DeviceChanged += OnDeviceChanged;
			enumerator.RegisterEndpointNotificationCallback(notificationClient);
		}

		public void RefreshAudioDevices(DataFlow flow = DataFlow.All)
		{
			MMDeviceEnumerator enumerator = new NAudio.CoreAudioApi.MMDeviceEnumerator();

			if (flow == DataFlow.All || flow == DataFlow.Capture)
			{
				MMDevice captureEndPoint = enumerator.GetDefaultAudioEndpoint(DataFlow.Capture, NAudio.CoreAudioApi.Role.Multimedia);
				DefaultCaptureDevice = new AudioDevice(captureEndPoint.ID,
					captureEndPoint.DeviceFriendlyName,
					captureEndPoint.State == NAudio.CoreAudioApi.DeviceState.Active,
					true);
			}

			if (flow == DataFlow.All || flow == DataFlow.Render)
			{
				MMDevice renderEndPoint = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, NAudio.CoreAudioApi.Role.Multimedia);
				DefaultRenderDevice = new AudioDevice(renderEndPoint.ID,
					renderEndPoint.DeviceFriendlyName,
					renderEndPoint.State == NAudio.CoreAudioApi.DeviceState.Active,
					true);
			}
		}

		private void OnDeviceChanged(object sender, AudioDeviceChangedEventArgs e)
		{
			RefreshAudioDevices(e.Flow);
		}

		private static AudioNotificationClient notificationClient;

		private static AudioDevice defaultCaptureDevice;

		private static AudioDevice defaultRenderDevice;

		public static AudioDevice DefaultCaptureDevice
		{
			get
			{
				return DefaultCaptureDevice;
			}
			set
			{
				if (defaultCaptureDevice != null && defaultCaptureDevice.DeviceId != value.DeviceId)
				{
					if (DefaultDeviceChanged != null)
					{
                        // no matter capture or render both alert
                        DefaultDeviceChanged(typeof(AudioDeviceManager), EventArgs.Empty);
                    }
					
				}
				defaultCaptureDevice = value;
			}
		}

		public static AudioDevice DefaultRenderDevice
		{
			get
			{
				return defaultRenderDevice;
			}
			set
			{
				if (defaultRenderDevice != null && defaultRenderDevice.DeviceId != value.DeviceId)
				{
					if (DefaultDeviceChanged != null)
					{
						// no matter capture or render both alert
						DefaultDeviceChanged(typeof(AudioDeviceManager), EventArgs.Empty);
					}
				}
				defaultRenderDevice = value;
			}
		}
	}
}
