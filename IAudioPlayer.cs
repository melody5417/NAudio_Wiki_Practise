using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAudio_Wiki_Practise
{
    public interface IAudioPlayer
    {
        #region Property

        /// <summary>
        /// The current position in the stream in Time format
        /// </summary>
        TimeSpan CurrentTime { get; set; }

        /// <summary>
        /// Total length in real-time of the stream (may be an estimate for compressed files)
        /// </summary>
        TimeSpan TotalTime { get; }

        /// <summary>
        /// Indicates that playback has gone into a stopped state due to 
        /// reaching the end of the input stream or an error has been encountered during playback
        /// </summary>
        event EventHandler<AudioStoppedEventArgs> PlaybackStopped;

        /// <summary>
        ///  Raised periodically to inform the user of the max volume
        /// </summary>
        event EventHandler<AudioVolumeMeterEventArgs> VolumeMeter;

        #endregion

        /// <summary>
        /// Open file will close last session and start new.
        /// </summary>
        /// <param name="fileName">audio file absolute path</param>
        void OpenFile(string fileName);

        /// <summary>
        /// If state is pause, will resume.
        /// If state is stop, will play from start.
        /// </summary>
        void Play();

        void Pause();

        /// <summary>
        /// If success will reset position to 0.
        /// If fail will throw exception through PlaybackStopped.
        /// </summary>
        void Stop();

        void CleanUp();

    }
}
