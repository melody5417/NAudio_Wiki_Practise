using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAudio_Wiki_Practise
{
    public class AudioDevice
    {

		public AudioDevice(string deviceId, string deviceName, bool isActived, bool isDefault)
		{
			DeviceId = deviceId;
			DeviceName = deviceName;
			IsActived = isActived;
			IsDefault = isDefault;
		}

		public string DeviceId { get; set; }

		public string DeviceName { get; set; }

		public bool IsActived { get; set; }

		public bool IsDefault { get; set; }
	}
}
