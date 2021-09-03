using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAudio_Wiki_Practise
{
    public class AudioStoppedEventArgs : EventArgs
    {
        private Exception exception;

        /// <summary>
        /// An exception. Will be null if the playback or record operation stopped due to 
        /// the user requesting stop or reached the end of the input audio
        /// </summary>
        public Exception Exception 
        { 
            get 
            {
                return exception;
            }
            set
            {
                exception = value;
            }
        }
    }
}
