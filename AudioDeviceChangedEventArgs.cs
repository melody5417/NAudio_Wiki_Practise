using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAudio_Wiki_Practise
{
    public class AudioDeviceChangedEventArgs : EventArgs
    {

        public AudioDeviceChangedEventArgs(DataFlow flow, Role role, string deviceId)
        {
            Flow = flow;
            Role = role;
            DeviceID = deviceId;
        }

        public DataFlow Flow { get; set; }

        public Role Role { get; set; }

        public string DeviceID { get; set; }
    }
}
