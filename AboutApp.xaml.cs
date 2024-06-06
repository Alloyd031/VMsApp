using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Graphics;
using Microsoft.UI.Windowing;
using Microsoft.UI;
using WinUIEx;
using WinUIEx.Messaging;
using System.Runtime.InteropServices;
using System.Reflection;
using Microsoft.UI.Composition.SystemBackdrops;

namespace VMsApp
{
    public sealed partial class AboutApp : WindowEx
    {
        private WindowMessageMonitor _msgMonitor;
        public AboutApp()
        {
            this.InitializeComponent();
            ExtendsContentIntoTitleBar = true;
            AppWindow.Resize(new SizeInt32(647, 458));
            SystemBackdrop = new MicaBackdrop()
            {
                Kind = MicaKind.Base
            };
            this.CenterOnScreen();
            SetTitleBar(AboutWindowTitleBar);
            CompileDate.Text = "Compilation date " + GetBuildDate(Assembly.GetExecutingAssembly());

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

            AboutWindowTitleBar.Loaded += AboutWindowTitleBar_Loaded;
        }
        private void AboutWindowTitleBar_Loaded(object sender, RoutedEventArgs e)
        {
            // Parts get delay loaded. If you have the parts, make them visible.
            VisualStateManager.GoToState(AboutWindowTitleBar, "SubtitleTextVisible", false);
            VisualStateManager.GoToState(AboutWindowTitleBar, "HeaderVisible", false);
            VisualStateManager.GoToState(AboutWindowTitleBar, "ContentVisible", false);
            VisualStateManager.GoToState(AboutWindowTitleBar, "FooterVisible", false);

            // Run layout so we re-calculate the drag regions.
            AboutWindowTitleBar.InvalidateMeasure();
        }
        private static DateTime GetBuildDate(Assembly assembly)
        {
            var attribute = assembly.GetCustomAttribute<BuildDateAttribute>();
            return attribute != null ? attribute.DateTime : default(DateTime);
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
