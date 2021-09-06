using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace NAudio_Wiki_Practise
{
    public class WaveOutPlayer : IAudioPlayer
    {
        private string fileName;
        private AudioFileReader audioFileReader;
        private IWavePlayer wavePlayer;

        public TimeSpan CurrentTime 
        { 
            get 
            {
                if (audioFileReader == null)
                {
                    Debug.WriteLine("[WaveOutPlayer] Get CurrentTime fail: reader is null");
                    return TimeSpan.Zero;
                }
                return audioFileReader.CurrentTime;
            }
            set
            {
                if (audioFileReader == null)
                {
                    Debug.WriteLine("[WaveOutPlayer] Set CurrentTime fail: reader is null");
                    return;
                }

                audioFileReader.CurrentTime = value;
            }
        }

        public TimeSpan TotalTime
        {
            get 
            {
                if (audioFileReader == null)
                {
                    Debug.WriteLine("[WaveOutPlayer] Get TotalTime fail: reader is null");
                    return TimeSpan.Zero;
                }

                return audioFileReader.TotalTime;
            }
        }

        public float Volume {
            get 
            {
                if (audioFileReader == null)
                {
                    return 0;
                }

                return audioFileReader.Volume;
            }
            set
            {
                if (audioFileReader == null)
                {
                    return;
                }

                audioFileReader.Volume = value;
            } 
        }

        public event EventHandler<AudioStoppedEventArgs> PlaybackStopped;

        public event EventHandler<AudioVolumeMeterEventArgs> PlaybackVolumeMeter;

        public void OpenFile(string fileName)
        {
            if (String.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException("[WaveOutPlayer] FileName is invalid");
            }

            // Open file will close last session.
            CleanUp();

            this.fileName = fileName;

            audioFileReader = new AudioFileReader(fileName);
            
            var sampleChannel = new SampleChannel(audioFileReader, true);
            var sampleProvider = new MeteringSampleProvider(sampleChannel);
            sampleProvider.StreamVolume += OnStreamVolumeMeter;

            wavePlayer = new WaveOut();
            wavePlayer.Init(sampleProvider);
            wavePlayer.PlaybackStopped += OnPlaybackStopped;
        }

        public void Play()
        {
            if (wavePlayer == null)
            {
                Debug.WriteLine("[WaveOutPlayer] Play fail: player is null");
                return;
            }
            wavePlayer.Play();
        }

        public void Pause()
        {
            if (wavePlayer == null)
            {
                Debug.WriteLine("[WaveOutPlayer] Pause fail: player is null");
                return;
            }
            wavePlayer.Pause();
        }
        
        public void Stop()
        {
            if (wavePlayer == null)
            {
                Debug.WriteLine("[WaveOutPlayer] Stop fail: player is null");
                return;
            }
            wavePlayer.Stop();
        }

        public void CleanUp()
        {
            if (fileName != null) {
                fileName = null;
            }

            if (wavePlayer != null)
            {
                wavePlayer.Stop();
            }

            if (audioFileReader != null)
            {
                audioFileReader.Dispose();
                audioFileReader = null;
            }

            if (wavePlayer != null)
            {
                wavePlayer.Dispose();
                wavePlayer = null;
            }

        }

        void OnPlaybackStopped(object sender, StoppedEventArgs e)
        {
            if (audioFileReader != null)
            {
                audioFileReader.Position = 0;
            }

            if (PlaybackStopped != null)
            {
                var arg = new AudioStoppedEventArgs();
                arg.Exception = e.Exception;
                PlaybackStopped(this, arg);
            }
        }

        void OnStreamVolumeMeter(object sender, StreamVolumeEventArgs e)
        {
            if (PlaybackVolumeMeter != null)
            {
                var arg = new AudioVolumeMeterEventArgs();
                arg.MaxSampleValue = e.MaxSampleValues[0];
                PlaybackVolumeMeter(this, arg);    
            }
        }

    }
}
