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
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace VMsApp.TabPages
{
    public sealed partial class VMPage : Page
    {
        private Window m_window;
        public VMPage()
        {
            this.InitializeComponent();
        }
        private void EditVMSettings_Click(object sender, RoutedEventArgs e)
        {
            m_window = new VMSettings();
            m_window.Activate();
        }
    }
}
