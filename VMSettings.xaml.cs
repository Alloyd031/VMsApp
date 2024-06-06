using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using VMsApp.Dialogs;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics;
using WinUIEx;
using WinUIEx.Messaging;

namespace VMsApp
{
    public sealed partial class VMSettings : WindowEx
    {
        private WindowMessageMonitor _msgMonitor;
        public VMSettings()
        {
            this.InitializeComponent();
            ExtendsContentIntoTitleBar = true;
            AppWindow.Resize(new SizeInt32(800, 650));
            this.CenterOnScreen();
            SetTitleBar(VMSettingsWindowTitleBar);

            _msgMonitor = new WindowMessageMonitor(this);
            _msgMonitor.WindowMessageReceived += (_, e) =>
            {
                const int WM_NCLBUTTONDBLCLK = 0x00A3;
                if (e.Message.MessageId == WM_NCLBUTTONDBLCLK)
                {
                    // Disable double click on title bar to maximize window
                    e.Result = 0;
                    e.Handled = true;
                }
            };
            VMSettingsWindowTitleBar.Loaded += VMSettingsWindowTitleBar_Loaded;
        }
        private void VMSettingsWindowTitleBar_Loaded(object sender, RoutedEventArgs e)
        {
            // Parts get delay loaded. If you have the parts, make them visible.
            VisualStateManager.GoToState(VMSettingsWindowTitleBar, "SubtitleTextVisible", false);
            VisualStateManager.GoToState(VMSettingsWindowTitleBar, "HeaderVisible", false);
            VisualStateManager.GoToState(VMSettingsWindowTitleBar, "ContentVisible", false);
            VisualStateManager.GoToState(VMSettingsWindowTitleBar, "FooterVisible", false);

            // Run layout so we re-calculate the drag regions.
            VMSettingsWindowTitleBar.InvalidateMeasure();
        }
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
