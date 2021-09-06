using AudioSwitcher.AudioApi;
using AudioSwitcher.AudioApi.CoreAudio;
using NAudio.CoreAudioApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAudio_Wiki_Practise
{
    public class AudioNotificationClient : IMMNotificationClient
    {

        public event EventHandler<AudioDeviceChangedEventArgs> DeviceChanged;

        void OnDeviceStateChanged(string deviceId, DeviceState newState) { }

        public void OnDeviceAdded(string pwstrDeviceId) { }

        public void OnDeviceRemoved(string deviceId) { }

        public void OnDefaultDeviceChanged(NAudio.CoreAudioApi.DataFlow flow, Role role, string defaultDeviceId) { }

        public void OnPropertyValueChanged(string pwstrDeviceId, PropertyKey key) { }

        public void OnDeviceStateChanged(string deviceId, NAudio.CoreAudioApi.DeviceState newState) { }

        public void OnPropertyValueChanged(string pwstrDeviceId, NAudio.CoreAudioApi.PropertyKey key) { }

        public void OnDefaultDeviceChanged(NAudio.CoreAudioApi.DataFlow flow, NAudio.CoreAudioApi.Role role, string defaultDeviceId)
        {
            if (DeviceChanged != null)
            {
                var arg = new AudioDeviceChangedEventArgs(flow, role, defaultDeviceId);
                DeviceChanged(this, arg);
            }
        }

        
    }
}
