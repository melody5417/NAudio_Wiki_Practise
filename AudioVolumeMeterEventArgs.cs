using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAudio_Wiki_Practise
{
    public class AudioVolumeMeterEventArgs : EventArgs
    {
        private float maxSampleValue;

        /// <summary>
        /// Max sample value, now only support mono
        /// </summary>
        public float MaxSampleValue
        {
            get
            {
                return maxSampleValue;
            }
            set
            {
                maxSampleValue = value;
            }
        }
    }
}
