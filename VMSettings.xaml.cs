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

namespace VMsApp
{
    public sealed partial class VMSettings : WindowEx
    {
        public VMSettings()
        {
            this.InitializeComponent();
            ExtendsContentIntoTitleBar = true;
            AppWindow.Resize(new SizeInt32(800, 650));
            this.CenterOnScreen();
            SetTitleBar(VMSettingsWindowTitleBar);
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
