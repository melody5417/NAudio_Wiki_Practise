using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAudio_Wiki_Practise
{
    public class WaveOutPlugin : IOutputDevicePlugin
    {
        public string Name
        {
            get { return "WaveOut";  }
        }

        public bool IsAvailabel
        {
            get { return WaveOut.DeviceCount > 0;  }
        }

        public int Priority 
        {
            get { return 1; }
        }

        public IWavePlayer CreateDevice(int deviceNumber, int latency)
        {
            IWavePlayer device;
            var strategy = WaveCallbackStrategy.FunctionCallback;
            if (strategy == WaveCallbackStrategy.Event)
            {
                var waveOut = new WaveOutEvent();
                waveOut.DeviceNumber = deviceNumber;
                waveOut.DesiredLatency = latency;
                device = waveOut;
            }
            else
            {
                WaveCallbackInfo callbackInfo = strategy == WaveCallbackStrategy.NewWindow ? WaveCallbackInfo.NewWindow() : WaveCallbackInfo.FunctionCallback();
                WaveOut outputDevice = new WaveOut(callbackInfo);
                outputDevice.DeviceNumber = deviceNumber;
                outputDevice.DesiredLatency = latency;
                device = outputDevice;
            }
            // TODO: configurable number of buffers

            return device;
        }
    }
}
