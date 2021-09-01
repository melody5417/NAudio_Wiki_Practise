using Microsoft.Win32;
using NAudio.Wave;
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
    public partial class MainWindow : Window
    {
        private IWavePlayer wavePlayer; 
        private AudioFileReader audioFileReader; 
        private string fileName;

        private DispatcherTimer dispatcherTimer;

        public MainWindow()
        {
            InitializeComponent();
            InitialiseDeviceCombo();
            EnableButtons(false);
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0,0, 1);
            dispatcherTimer.Start();
        }

        public void Dispose()
        {
            CleanUp();
        }

        private void InitialiseDeviceCombo()
        {
            if (WaveOut.DeviceCount <= 0) return;
            for (var deviceId = -1; deviceId < WaveOut.DeviceCount; deviceId++)
            {
                var capabilities = WaveOut.GetCapabilities(deviceId);
                comboBoxWaveOutDevice.Items.Add($"Device {deviceId} ({capabilities.ProductName})");
            }
            comboBoxWaveOutDevice.SelectionChanged += OnComboBoxWaveOutDeviceSelectionChanged;
            comboBoxWaveOutDevice.SelectedIndex = 0;
        }

        #region Actions

        private void btnOpenFileClick(object sender, RoutedEventArgs e)
        {
            fileName = SelectInputFile();
        }

        private void btnPlaybackClick(object sender, RoutedEventArgs e)
        {
            //if (!SelectedOutputDevicePlugin.IsAvailabel)
            //{
            //    MessageBox.Show("The selected output driver is not available on this system");
            //    return;
            //}

            if (fileName == null) fileName = SelectInputFile();
            if (fileName != null)
            {
                BeginPlayback(fileName);
            }
        }

        private void btnPauseClick(object sender, RoutedEventArgs e)
        {
            wavePlayer.Pause();
        }

        private void btnStopClick(object sender, RoutedEventArgs e)
        {
            wavePlayer.Stop();
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
            buttonOpen.IsEnabled = !playing;
            buttonPlayback.IsEnabled = !playing;
            buttonPause.IsEnabled = playing;
            buttonStop.IsEnabled = playing;
        }

        private static string SelectInputFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Audio Files|*.mp3;*.wav;*.aiff;*.wma";
            if (openFileDialog.ShowDialog() == true)
                return openFileDialog.FileName;
            return null;
        }

        private void BeginPlayback(string filename)
        {
            Debug.Assert(wavePlayer == null);
            wavePlayer = new WaveOut();
            audioFileReader = new AudioFileReader(filename);
            wavePlayer.Init(audioFileReader);
            wavePlayer.PlaybackStopped += OnPlaybackStopped;
            wavePlayer.Play();
            EnableButtons(true);
            dispatcherTimer.IsEnabled = true; // timer for updating current time label
        }

        private void OnPlaybackStopped(object sender, StoppedEventArgs e)
        {
            // we want to be always on the GUI thread and be able to change GUI components
            // Debug.Assert(!InvokeRequired, "PlaybackStopped on wrong thread");
            // we want it to be safe to clean up input stream and playback device in the handler for PlaybackStopped
            CleanUp();
            EnableButtons(false);
            dispatcherTimer.IsEnabled = false;
            labelCurrentTime.Content = "Current Time: 00:00";
            
            if (e.Exception != null)
            {
                MessageBox.Show(String.Format("Playback Stopped due to an error {0}", e.Exception.Message));
            }
        }

        private void CleanUp()
        {
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

        private static string FormatTimeSpan(TimeSpan ts)
        {
            return string.Format("{0:D2}:{1:D2}", (int)ts.TotalMinutes, ts.Seconds);
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (audioFileReader != null)
            {
                labelCurrentTime.Content = "Current Time: " + FormatTimeSpan(audioFileReader.CurrentTime);
                labelTotalTime.Content = "Total Time: " + FormatTimeSpan(audioFileReader.TotalTime);
            }
        }

        private IOutputDevicePlugin SelectedOutputDevicePlugin
        {
            get { return (IOutputDevicePlugin)comboBoxWaveOutDevice.SelectedItem; }
        }


    }
}
