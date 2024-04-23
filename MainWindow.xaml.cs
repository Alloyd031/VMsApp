using Microsoft.UI.Composition.SystemBackdrops;
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
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.WindowManagement;
using Microsoft.UI.Xaml.Hosting;
using Windows.UI;
using WinUIEx;
using Windows.Graphics;
using Microsoft.UI;
using System.Runtime.InteropServices;
using Microsoft.UI.Input;

namespace VMsApp
{
    public sealed partial class MainWindow : WindowEx
    {
        private Window m_window;
        public MainWindow()
        {
            this.InitializeComponent();
            ExtendsContentIntoTitleBar = true;
            SetTitleBar(AppTitleBar);
            AppWindow.Resize(new SizeInt32(1404, 916));
            this.CenterOnScreen();

            AppWindow.TitleBar.PreferredHeightOption = TitleBarHeightOption.Collapsed;

            this.FolderView.Visibility = Visibility.Collapsed;
            this.TabsGrid.Margin = new Thickness(201, 47, 0, 32);

            if (ShowHideLibrary.IsChecked == false)
            {
                ShowHideLibrary.IsChecked = true;
            }
        }
        public SolidColorBrush GetSolidColorBrush(string hex)
        {
            hex = hex.Replace("#", string.Empty);
            byte a = (byte)(Convert.ToUInt32(hex.Substring(0, 2), 16));
            byte r = (byte)(Convert.ToUInt32(hex.Substring(2, 2), 16));
            byte g = (byte)(Convert.ToUInt32(hex.Substring(4, 2), 16));
            byte b = (byte)(Convert.ToUInt32(hex.Substring(6, 2), 16));
            SolidColorBrush myBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(a, r, g, b));
            return myBrush;
        }
        private void HideLibrary_Click(object sender, RoutedEventArgs e)
        {
            this.LibraryPanel.Visibility = Visibility.Collapsed;
            this.TabsGrid.Margin = new Thickness(0, 47, 0, 32);
            this.FolderView.Margin = new Thickness(0, 0, 0, 32);

            if (ShowHideLibrary.IsChecked == true)
            {
                ShowHideLibrary.IsChecked = false;
            }
        }
        private void ShowHideLibrary_Click(object sender, RoutedEventArgs e)
        {
            Button btnE = sender as Button;
            if (this.LibraryPanel.Visibility == Visibility.Visible)
            {
                this.LibraryPanel.Visibility = Visibility.Collapsed;
                this.TabsGrid.Margin = new Thickness(0, 47, 0, 32);
                this.FolderView.Margin = new Thickness(0, 47, 0, 32);
                ShowHideLibrary.IsChecked = false;
            }
            else
            {
                this.LibraryPanel.Visibility = Visibility.Visible;
                this.TabsGrid.Margin = new Thickness(201, 47, 0, 32);
                this.FolderView.Margin = new Thickness(202, 47, 0, 32);
                ShowHideLibrary.IsChecked = true;
            }
        }
        private void HideFolderView_Click(object sender, RoutedEventArgs e)
        {
            this.FolderView.Visibility = Visibility.Collapsed;
            this.TabsView.Margin = new Thickness(0, 0, 0, 0);
            ShowHideFolderView.IsChecked = false;
        }
        private void ShowHideFolderView_Click(object sender, RoutedEventArgs e)
        {
            Button btnE = sender as Button;
            if (this.FolderView.Visibility == Visibility.Visible)
            {
                this.FolderView.Visibility = Visibility.Collapsed;
                this.TabsView.Margin = new Thickness(0, 0, 0, 0);
                ShowHideFolderView.IsChecked = false;
            }
            else
            {
                this.FolderView.Visibility = Visibility.Visible;
                this.TabsView.Margin = new Thickness(0, 0, 0, 152);
                ShowHideFolderView.IsChecked = true;
            }
        }
        private async void VMSettings_Click(object sender, RoutedEventArgs e)
        {
            VMSettings dialog = new VMSettings();
            dialog.XamlRoot = this.Content.XamlRoot;
            ContentDialogResult result = await dialog.ShowAsync();
        }
        private void About_Click(object sender, RoutedEventArgs e)
        {
            m_window = new AboutApp();
            m_window.Show();
        }
        private void Button_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }
        private void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                this.Maximize();
                MaximizeIcon.Glyph = "\uE923";
            }
            else
            {
                this.Restore();
                MaximizeIcon.Glyph = "\uE922";
            }
        }
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Minimize();
        }
    }
}
