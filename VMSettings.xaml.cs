using Microsoft.UI.Windowing;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using VMsApp.VMSettingsPages;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics;
using Microsoft.UI.Xaml.Interop;
using WinUIEx;

namespace VMsApp
{
    public sealed partial class VMSettings : WindowEx
    {
        public VMSettings()
        {
            this.InitializeComponent();
            ExtendsContentIntoTitleBar = true;
            SetTitleBar(SettingsWindowTitleBar);
            AppWindow.Resize(new SizeInt32(780, 760));
            this.CenterOnScreen();

            ContentFrame.Navigate(typeof(Hardware), null, new SuppressNavigationTransitionInfo());

            HardwareTab.IsChecked = true;
            OptionsTab.IsChecked = false;
        }
        private void HardwareTab_Tapped(object sender, TappedRoutedEventArgs e)
        {
            e.Handled = true;

            if (ContentFrame.CurrentSourcePageType != typeof(Hardware) && typeof(Hardware) != null)
            {
                ContentFrame.Navigate(typeof(Hardware), null, new SuppressNavigationTransitionInfo());
                HardwareTab.IsChecked = true;
                OptionsTab.IsChecked = false;
            }
        }
        private void HardwareTab_Click(object sender, RoutedEventArgs e)
        {
            HardwareTab.IsChecked = true;
            OptionsTab.IsChecked = false;
        }
        private void HardwareTab_Checked(object sender, RoutedEventArgs e)
        {
            if (ContentFrame.CurrentSourcePageType != typeof(Hardware))
            {
                ContentFrame.Navigate(typeof(Hardware), null, new SuppressNavigationTransitionInfo());
            }
        }
        private void OptionsTab_Tapped(object sender, TappedRoutedEventArgs e)
        {
            e.Handled = true;

            if (ContentFrame.CurrentSourcePageType != typeof(Options) && typeof(Options) != null)
            {
                ContentFrame.Navigate(typeof(Options), null, new SuppressNavigationTransitionInfo());
                HardwareTab.IsChecked = false;
                OptionsTab.IsChecked = true;
            }
        }
        private void OptionsTab_Click(object sender, RoutedEventArgs e)
        {
            HardwareTab.IsChecked = false;
            OptionsTab.IsChecked = true;
        }
        private void OptionsTab_Checked(object sender, RoutedEventArgs e)
        {
            if (ContentFrame.CurrentSourcePageType != typeof(Options))
            {
                ContentFrame.Navigate(typeof(Options), null, new SuppressNavigationTransitionInfo());
            }
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(Hardware), null, new SuppressNavigationTransitionInfo());

            HardwareTab.IsChecked = true;
            OptionsTab.IsChecked = false;
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
