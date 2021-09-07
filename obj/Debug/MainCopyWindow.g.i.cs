﻿#pragma checksum "..\..\MainCopyWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "6A189737ED197A431CC56524EBDC9FD31F6D350FCA31ACFCD7BAC474598D4302"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using NAudio_Wiki_Practise;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using VoiceRecorder.Core;


namespace NAudio_Wiki_Practise {
    
    
    /// <summary>
    /// MainCopyWindow
    /// </summary>
    public partial class MainCopyWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 29 "..\..\MainCopyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox comboBoxWaveOutDevice;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\MainCopyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonOpen;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\MainCopyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonPlayback;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\MainCopyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonPause;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\MainCopyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonStop;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\MainCopyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelCurrentTime;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\MainCopyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelTotalTime;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\MainCopyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider sliderOutputVolume;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\MainCopyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider sliderInputVolume;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\MainCopyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal VoiceRecorder.Core.PolygonWaveFormControl WaveForm;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/NAudio_Wiki_Practise;component/maincopywindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MainCopyWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.comboBoxWaveOutDevice = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 2:
            this.buttonOpen = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\MainCopyWindow.xaml"
            this.buttonOpen.Click += new System.Windows.RoutedEventHandler(this.btnOpenFileClick);
            
            #line default
            #line hidden
            return;
            case 3:
            this.buttonPlayback = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\MainCopyWindow.xaml"
            this.buttonPlayback.Click += new System.Windows.RoutedEventHandler(this.btnPlaybackClick);
            
            #line default
            #line hidden
            return;
            case 4:
            this.buttonPause = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\MainCopyWindow.xaml"
            this.buttonPause.Click += new System.Windows.RoutedEventHandler(this.btnPauseClick);
            
            #line default
            #line hidden
            return;
            case 5:
            this.buttonStop = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\MainCopyWindow.xaml"
            this.buttonStop.Click += new System.Windows.RoutedEventHandler(this.btnStopClick);
            
            #line default
            #line hidden
            return;
            case 6:
            this.labelCurrentTime = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.labelTotalTime = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.sliderOutputVolume = ((System.Windows.Controls.Slider)(target));
            return;
            case 9:
            this.sliderInputVolume = ((System.Windows.Controls.Slider)(target));
            
            #line 60 "..\..\MainCopyWindow.xaml"
            this.sliderInputVolume.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.OnSliderValueChanged);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 68 "..\..\MainCopyWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnStartRecordClick);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 69 "..\..\MainCopyWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnResumeRecordClick);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 70 "..\..\MainCopyWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnPauseRecordClick);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 71 "..\..\MainCopyWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnStopRecordClick);
            
            #line default
            #line hidden
            return;
            case 14:
            this.WaveForm = ((VoiceRecorder.Core.PolygonWaveFormControl)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

