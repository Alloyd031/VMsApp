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
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics;
using WinUIEx;
using WinUIEx.Messaging;

namespace VMsApp.Dialogs
{
    public sealed partial class FeatureNotAvailable : WindowEx
    {
        private WindowMessageMonitor _msgMonitor;
        public FeatureNotAvailable()
        {
            this.InitializeComponent(); 
            ExtendsContentIntoTitleBar = true;
            AppWindow.Resize(new SizeInt32(400, 250));
            this.CenterOnScreen();
            SetTitleBar(FeatureNotAvailableTitleBar);

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
        }
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
