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

namespace VMsApp.NewVMWizardPages
{
    public sealed partial class OperatingSystem : Page
    {
        public OperatingSystem()
        {
            this.InitializeComponent();
        }
        private void HandleCheck(object sender, RoutedEventArgs e)
        {
            if (this.OSSelectionBox != null && this.WindowsOS.IsChecked == true)
            {
                this.OSSelectionBox.IsEnabled = false;
            }
            if (this.OSSelectionBox != null && this.LinuxOS.IsChecked == true)
            {
                this.OSSelectionBox.IsEnabled = false;
            }
            if (this.OSSelectionBox != null && this.OtherOS.IsChecked == true)
            {
                this.OSSelectionBox.IsEnabled = true;
            }
        }
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.OtherOS.IsChecked == true)
            {
                this.Frame.Navigate(typeof(NameVirtualMachine));
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }
    }
}
