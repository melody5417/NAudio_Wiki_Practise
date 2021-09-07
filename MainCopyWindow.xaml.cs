using Microsoft.Win32;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

// https://github.com/naudio/NAudio/wiki/Playing-an-Audio-File#choosing-an-output-device

namespace NAudio_Wiki_Practise
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainCopyWindow : Window
    {
        private IAudioPlayer player;

        private IAudioRecorder recorder;

        private DispatcherTimer dispatcherTimer;

        private AudioDeviceManager deviceManager;

        public MainCopyWindow()
        {
            InitializeComponent();
            EnableButtons(false);
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

            deviceManager = new AudioDeviceManager();
            deviceManager.RefreshAudioDevices();

            Closed += MainWindow_Closed;
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void Dispose()
        {
            CleanUp();
        }

        

        #region Actions

        private void btnOpenFileClick(object sender, RoutedEventArgs e)
        {
            string fileName = SelectInputFile();
            if (string.IsNullOrWhiteSpace(fileName)) return;
            player = new WaveOutPlayer();
            player.PlaybackStopped += OnPlaybackStopped;
            player.PlaybackVolumeMeter += PlaybackVolumeMeter;
            player.OpenFile(fileName);
        }

        private void btnPlaybackClick(object sender, RoutedEventArgs e)
        {
            player.Play();
            EnableButtons(true);
            dispatcherTimer.IsEnabled = true; // timer for updating current time label
        }

        private void btnPauseClick(object sender, RoutedEventArgs e)
        {
            player.Pause();
        }

        private void btnStopClick(object sender, RoutedEventArgs e)
        {
            player.Stop();
            // don't set button states now, we'll wait for our PlaybackStopped to come
        }

        #endregion

        #region Handlers

        private void OnComboBoxWaveOutDeviceSelectionChanged(object sender, RoutedEventArgs e) 
        {
            
        }

        #endregion

        private void EnableButtons(bool playing)
        {
            //buttonOpen.IsEnabled = !playing;
            //buttonPlayback.IsEnabled = !playing;
            //buttonPause.IsEnabled = playing;
            //buttonStop.IsEnabled = playing;
        }

        private static string SelectInputFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Audio Files|*.mp3;*.wav;*.aiff;*.wma";
            if (openFileDialog.ShowDialog() == true)
                return openFileDialog.FileName;
            return null;
        }

        private void OnPlaybackStopped(object sender, AudioStoppedEventArgs e)
        {
            MessageBox.Show("playback stopped");
            // we want to be always on the GUI thread and be able to change GUI components
            // Debug.Assert(!InvokeRequired, "PlaybackStopped on wrong thread");
            // we want it to be safe to clean up input stream and playback device in the handler for PlaybackStopped
            //CleanUp();
            //EnableButtons(false);
            //dispatcherTimer.IsEnabled = false;
            //labelCurrentTime.Content = "Current Time: 00:00";

            if (e.Exception != null)
            {
                MessageBox.Show(String.Format("Playback Stopped due to an error {0}", e.Exception.Message));
            }
        }

        private void OnRecordStopped(object sender, AudioStoppedEventArgs e)
        {
            MessageBox.Show("record stopped");

            if (WaveForm != null)
            {
                WaveForm.Reset();
            }

            // we want to be always on the GUI thread and be able to change GUI components
            // Debug.Assert(!InvokeRequired, "PlaybackStopped on wrong thread");
            // we want it to be safe to clean up input stream and playback device in the handler for PlaybackStopped
            //CleanUp();
            //EnableButtons(false);
            //dispatcherTimer.IsEnabled = false;
            //labelCurrentTime.Content = "Current Time: 00:00";

            if (e.Exception != null)
            {
                MessageBox.Show(String.Format("Record Stopped due to an error {0}", e.Exception.Message));
            }
        }

        void PlaybackVolumeMeter(object sender, AudioVolumeMeterEventArgs e)
        {
            Debug.WriteLine("[PlaybackVolumeMeter] max sample value is {0} ", e.MaxSampleValue);
            Debug.WriteLine("get current mic volume is {0}", player.Volume);
        }

        void RecordVolumeMeter(object sender, AudioVolumeMeterEventArgs e)
        {
            Debug.WriteLine("[RecordVolumeMeter] max sample value is {0} ", e.MaxSampleValue);
            Debug.WriteLine("get current player volume is {0}", recorder.Volume);

            WaveForm.AddValue(e.MaxSampleValue, -e.MaxSampleValue);
        }

        private void CleanUp()
        {
            if (player != null)
            {
                player.Dispose();
                player = null;
            }
            if (WaveForm != null)
            {
                WaveForm.Reset();
            }
            
        }

        private static string FormatTimeSpan(TimeSpan ts)
        {
            return string.Format("{0:D2}:{1:D2}", (int)ts.TotalMinutes, ts.Seconds);
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (player != null)
            {
                labelCurrentTime.Content = "Current Time: " + FormatTimeSpan(player.CurrentTime);
                labelTotalTime.Content = "Total Time: " + FormatTimeSpan(player.TotalTime);
            }
        }

        private void btnStartRecordClick(object sender, RoutedEventArgs e)
        {
            recorder = new WaveInRecorder();
            recorder.setFileName("D:\\Projects\\tongchuan\\Tongchuanclient_doc\\测试音频\\test_record.wav");
            recorder.RecordStopped += OnRecordStopped;
            recorder.RecordVolumeMeter += RecordVolumeMeter;
            recorder.StartRecording();
        }

        private void btnStopRecordClick(object sender, RoutedEventArgs e)
        {
            recorder.StopRecording();
        }

        private void btnPauseRecordClick(object sender, RoutedEventArgs e)
        {
            recorder.PauseRecording();
        }

        private void btnResumeRecordClick(object sender, RoutedEventArgs e)
        {
            recorder.StartRecording();
        }

        private float microLevel;
        public float MicrophoneLevel
        {
            get { return microLevel; }
            set 
            { 
                microLevel = value;
                player.Volume = value;
            }
        }

        private void OnSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (player != null)
            {
                player.Volume = (float)e.NewValue;
            }
            if (recorder != null)
            {
                recorder.Volume = (float)e.NewValue;
            }
            
        }

    }
}
