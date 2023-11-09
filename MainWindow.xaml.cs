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
using Windows.Foundation;
using Windows.Foundation.Collections;
using VMsApp.TabPages;

namespace VMsApp
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            ExtendsContentIntoTitleBar = true;
            SetTitleBar(AppTitleBar);
            AppWindow.TitleBar.PreferredHeightOption = TitleBarHeightOption.Tall;
            this.SystemBackdrop = new MicaBackdrop { Kind = MicaKind.Base };

            this.TabsFrame.Navigate(typeof(HomePage));
            this.FolderView.Visibility = Visibility.Collapsed;
            this.TabsFrame.Margin = new Thickness(0, 31, 0, 0);

            if (ShowHideLibrary.IsChecked == false)
            {
                ShowHideLibrary.IsChecked = true;
            }
        }
        private void HideLibrary_Click(object sender, RoutedEventArgs e)
        {
            this.LibraryPanel.Visibility = Visibility.Collapsed;
            this.TabsGrid.Margin = new Thickness(0, 88, 0, 0);
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
                this.TabsGrid.Margin = new Thickness(0, 88, 0, 32);
                this.FolderView.Margin = new Thickness(0, 0, 0, 32);
                ShowHideLibrary.IsChecked = false;
            }
            else
            {
                this.LibraryPanel.Visibility = Visibility.Visible;
                this.TabsGrid.Margin = new Thickness(200, 88, 0, 32);
                this.FolderView.Margin = new Thickness(202, 0, 0, 32);
                ShowHideLibrary.IsChecked = true;
            }
        }
        private void HideFolderView_Click(object sender, RoutedEventArgs e)
        {
            this.FolderView.Visibility = Visibility.Collapsed;
            this.TabsFrame.Margin = new Thickness(0, 31, 0, 0);
            ShowHideFolderView.IsChecked = false;
        }
        private void ShowHideFolderView_Click(object sender, RoutedEventArgs e)
        {
            Button btnE = sender as Button;
            if (this.FolderView.Visibility == Visibility.Visible)
            {
                this.FolderView.Visibility = Visibility.Collapsed;
                this.TabsFrame.Margin = new Thickness(0, 31, 0, 0);
                ShowHideFolderView.IsChecked = false;
            }
            else
            {
                this.FolderView.Visibility = Visibility.Visible;
                this.TabsFrame.Margin = new Thickness(0, 31, 0, 152);
                ShowHideFolderView.IsChecked = true;
            }
        }
    }
}
