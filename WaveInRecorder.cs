using NAudio.Mixer;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace NAudio_Wiki_Practise
{

    public enum RecordState
    {
        Stopped = 0,
        Playing = 1,
        Paused = 2
    }


    public class WaveInRecorder : IAudioRecorder
    {
        private string fileName;
        private WaveIn waveRecorder;
        private WaveFileWriter waveFileWriter;
        private UnsignedMixerControl volumeControl;

        private RecordState recordState;


        public TimeSpan RecordedTime
        {
            get
            {
                if (waveFileWriter == null)
                {
                    return TimeSpan.Zero;
                }
                return TimeSpan.FromSeconds((double)waveFileWriter.Length / waveFileWriter.WaveFormat.AverageBytesPerSecond);
            }
        }


        public event EventHandler<AudioStoppedEventArgs> RecordStopped;

        public event EventHandler<AudioVolumeMeterEventArgs> RecordVolumeMeter;

        public void setFileName(string fileName)
        {
            if (String.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException("FileName is invalid");
            }
            this.fileName = fileName;

            // todo yiqi check exist and create dir

            var waveFormat = new WaveFormat(16000, 16, 1);

            waveRecorder = new WaveIn();
            waveRecorder.WaveFormat = waveFormat;
            waveRecorder.DataAvailable += OnDataAvailable;
            waveRecorder.RecordingStopped += OnRecordingStopped;

            waveFileWriter = new WaveFileWriter(fileName, waveFormat);

            TryGetVolumeControl();
        }


        private void TryGetVolumeControl()
        {
            int waveInDeviceNumber = waveRecorder.DeviceNumber;
            if (Environment.OSVersion.Version.Major >= 6) // Vista and over
            {
                var mixerLine = waveRecorder.GetMixerLine();
                foreach (var control in mixerLine.Controls)
                {
                    if (control.ControlType == MixerControlType.Volume)
                    {
                        this.volumeControl = control as UnsignedMixerControl;
                        Volume = volume;
                        break;
                    }
                }
            }
            else
            {
                var mixer = new Mixer(waveInDeviceNumber);
                foreach (var destination in mixer.Destinations
                    .Where(d => d.ComponentType == MixerLineComponentType.DestinationWaveIn))
                {
                    foreach (var source in destination.Sources
                        .Where(source => source.ComponentType == MixerLineComponentType.SourceMicrophone))
                    {
                        foreach (var control in source.Controls
                            .Where(control => control.ControlType == MixerControlType.Volume))
                        {
                            volumeControl = control as UnsignedMixerControl;
                            Volume = volume;
                            break;
                        }
                    }
                }
            }

        }

        private float volume = 1.0f;
        public float Volume
        {
            get
            {
                return volume;
            }
            set
            {
                volume = value;
                if (volumeControl != null)
                {
                    volumeControl.Percent = value * 100;
                }
            }
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void StartRecording()
        {
            if (waveRecorder == null)
            {
                Debug.WriteLine("StartRecording fail: recorder is null");
                return;
            }

            if (recordState == RecordState.Paused)
            {
                waveRecorder.DataAvailable += OnDataAvailable;
                recordState = RecordState.Playing;
            }
            else
            {
                waveRecorder.StartRecording();
                recordState = RecordState.Playing;
            }
        }

        public void PauseRecording()
        {
            if (waveRecorder == null)
            {
                Debug.WriteLine("PauseRecording fail: recorder is null");
                return;
            }

            if (recordState == RecordState.Playing)
            {
                waveRecorder.DataAvailable -= OnDataAvailable;
                recordState = RecordState.Paused;
            }
        }

        public void StopRecording()
        {
            if (waveRecorder == null)
            {
                Debug.WriteLine("StopRecording fail: recorder is null");
                return;
            }

            waveRecorder.StopRecording();
            recordState = RecordState.Stopped;
        }

        void OnDataAvailable(object sender, WaveInEventArgs e)
        {
            if (waveFileWriter != null)
            {
                waveFileWriter.Write(e.Buffer, 0, e.BytesRecorded);
                waveFileWriter.Flush();
            }

            // Calculating max sample value: from 0 to 1
            float max = 0;
            // interpret as 16 bit audio
            for (int index = 0; index < e.BytesRecorded; index += 2)
            {
                short sample = (short)((e.Buffer[index + 1] << 8) |
                                        e.Buffer[index + 0]);
                // to floating point
                var sample32 = sample / 32768f;
                // absolute value 
                if (sample32 < 0) sample32 = -sample32;
                // is this the max value?
                if (sample32 > max) max = sample32;
            }

            if (RecordVolumeMeter != null)
            {
                var arg = new AudioVolumeMeterEventArgs();
                arg.MaxSampleValue = max;
                RecordVolumeMeter(this, arg);
            }
        }

        void OnRecordingStopped(object sender, StoppedEventArgs e)
        {
            recordState = RecordState.Stopped;

            if (waveRecorder != null)
            {
                waveRecorder.Dispose();
                waveRecorder = null;
            }

            if (waveFileWriter != null)
            {
                waveFileWriter.Dispose();
                waveFileWriter = null;
            }

            if (RecordStopped != null)
            {
                var arg = new AudioStoppedEventArgs();
                arg.Exception = e.Exception;
                RecordStopped(this, arg);
            }
        }
    }
}
