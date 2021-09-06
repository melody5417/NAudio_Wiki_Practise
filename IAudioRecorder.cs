using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAudio_Wiki_Practise
{
    public interface IAudioRecorder
    {
        /// <summary>
        /// Record time in seconds.
        /// </summary>
        TimeSpan RecordedTime { get; }

        float Volume { get; set; }

        /// <summary>
        /// Indicates that record has gone into a stopped state or an error has been encountered during recording
        /// </summary>
        event EventHandler<AudioStoppedEventArgs> RecordStopped;

        /// <summary>
        ///  Raised periodically to inform the user of the max volume
        /// </summary>
        event EventHandler<AudioVolumeMeterEventArgs> RecordVolumeMeter;

        /// <summary>
        /// Set fileName will close last session and start new.
        /// </summary>
        /// <param name="fileName">audio file absolute path</param>
        void setFileName(string fileName);

        void StartRecording();

        void PauseRecording();

        void StopRecording();

        void Dispose();
    }
}
