using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAudio_Wiki_Practise
{
    public interface IOutputDevicePlugin
    {
        IWavePlayer CreateDevice(int deviceNumber, int latency);

        string Name { get;  }

        bool IsAvailabel { get; }

        int Priority { get; }

    }
}
