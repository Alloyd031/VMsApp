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
using System.Threading.Tasks;
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
using Microsoft.UI.Xaml.Media.Animation;
using static System.Net.Mime.MediaTypeNames;

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

            this.TabsFrame.Navigate(typeof(HomePage));
            this.FolderView.Visibility = Visibility.Collapsed;
            this.TabsFrame.Margin = new Thickness(0, 48, 0, 0);

            this.TabsFrame.Navigate(typeof(HomePage));
            this.FolderView.Visibility = Visibility.Collapsed;
            this.TabsFrame.Margin = new Thickness(0, 48, 0, 0);

            if (ShowHideLibrary.IsChecked == false)
            {
                ShowHideLibrary.IsChecked = true;
            }

            MyComputerTab.IsChecked = false;
            MyComputer.IsChecked = false;
            VMTab.IsChecked = false;
            HomeTab.IsChecked = true;
            SharedVMsTab.IsChecked = false;
            SharedVMs.IsChecked = false;
            TestPageTab.IsChecked = false;
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
            this.TabsGrid.Margin = new Thickness(0, 72, 0, 0);
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
                this.TabsGrid.Margin = new Thickness(0, 72, 0, 32);
                this.FolderView.Margin = new Thickness(0, 0, 0, 32);
                ShowHideLibrary.IsChecked = false;
            }
            else
            {
                this.LibraryPanel.Visibility = Visibility.Visible;
                this.TabsGrid.Margin = new Thickness(200, 72, 0, 32);
                this.FolderView.Margin = new Thickness(202, 0, 0, 32);
                ShowHideLibrary.IsChecked = true;
            }
        }
        private void HideFolderView_Click(object sender, RoutedEventArgs e)
        {
            this.FolderView.Visibility = Visibility.Collapsed;
            this.TabsFrame.Margin = new Thickness(0, 48, 0, 0);
            ShowHideFolderView.IsChecked = false;
        }
        private void ShowHideFolderView_Click(object sender, RoutedEventArgs e)
        {
            Button btnE = sender as Button;
            if (this.FolderView.Visibility == Visibility.Visible)
            {
                this.FolderView.Visibility = Visibility.Collapsed;
                this.TabsFrame.Margin = new Thickness(0, 48, 0, 0);
                ShowHideFolderView.IsChecked = false;
            }
            else
            {
                this.FolderView.Visibility = Visibility.Visible;
                this.TabsFrame.Margin = new Thickness(0, 48, 0, 152);
                ShowHideFolderView.IsChecked = true;
            }
        }
        private void VMSettings_Click(object sender, RoutedEventArgs e)
        {
            m_window = new VMSettings();
            m_window.Activate();
        }
        private void About_Click(object sender, RoutedEventArgs e)
        {
            m_window = new AboutApp();
            m_window.Activate();
        }
        private void MyComputerTab_Tapped(object sender, TappedRoutedEventArgs e)
        {
            e.Handled = true;

            if (TabsFrame.CurrentSourcePageType != typeof(MyComputerPage) && typeof(MyComputerPage) != null)
            {
                TabsFrame.Navigate(typeof(MyComputerPage), null, new SuppressNavigationTransitionInfo());
                MyComputerTab.IsChecked = true;
                MyComputer.IsChecked = true;
                VMsAppOS.IsChecked = false;
                VMTab.IsChecked = false;
                HomeTab.IsChecked = false;
                SharedVMsTab.IsChecked = false;
                SharedVMs.IsChecked = false;
                TestPageTab.IsChecked = false;
            }
        }
        private void MyComputerTab_Click(object sender, RoutedEventArgs e)
        {
            MyComputer.IsChecked = true;
            MyComputerTab.IsChecked = true;
            VMsAppOS.IsChecked = false;
            VMTab.IsChecked = false;
            HomeTab.IsChecked = false;
            SharedVMsTab.IsChecked = false;
            SharedVMs.IsChecked = false;
            TestPageTab.IsChecked = false;
        }
        private void MyComputer_Tapped(object sender, TappedRoutedEventArgs e)
        {
            e.Handled = true;

            if (TabsFrame.CurrentSourcePageType != typeof(MyComputerPage) && typeof(MyComputerPage) != null)
            {
                TabsFrame.Navigate(typeof(MyComputerPage), null, new SuppressNavigationTransitionInfo());
                MyComputerTab.IsChecked = true;
                MyComputer.IsChecked = true;
                VMTab.IsChecked = false;
                HomeTab.IsChecked = false;
                SharedVMsTab.IsChecked = false;
                SharedVMs.IsChecked = false;
                TestPageTab.IsChecked = false;
            }
        }
        private void MyComputer_Click(object sender, RoutedEventArgs e)
        {
            MyComputer.IsChecked = true;
            MyComputerTab.IsChecked = true;
            VMsAppOS.IsChecked = false;
            VMTab.IsChecked = false;
            HomeTab.IsChecked = false;
            SharedVMsTab.IsChecked = false;
            SharedVMs.IsChecked = false;
            TestPageTab.IsChecked = false;
        }
        private void VMsAppOS_Click(object sender, RoutedEventArgs e)
        {
            MyComputer.IsChecked = false;
            MyComputerTab.IsChecked = false;
            VMsAppOS.IsChecked = true;
            VMTab.IsChecked = true;
            HomeTab.IsChecked = false;
            SharedVMsTab.IsChecked = false;
            SharedVMs.IsChecked = false;
            TestPageTab.IsChecked = false;
        }
        private void MyComputer_Checked(object sender, RoutedEventArgs e)
        {
            if (TabsFrame.CurrentSourcePageType != typeof(MyComputerPage))
            {
                TabsFrame.Navigate(typeof(MyComputerPage), null, new SuppressNavigationTransitionInfo());
            }
        }
        private void VMsAppOS_Tapped(object sender, TappedRoutedEventArgs e)
        {
            e.Handled = true;

            if (TabsFrame.CurrentSourcePageType != typeof(VMPage) && typeof(VMPage) != null)
            {
                TabsFrame.Navigate(typeof(VMPage), null, new SuppressNavigationTransitionInfo());
                MyComputerTab.IsChecked = false;
                MyComputer.IsChecked = false;
                VMsAppOS.IsChecked = true;
                VMTab.IsChecked = true;
                HomeTab.IsChecked = false;
                SharedVMsTab.IsChecked = false;
                SharedVMs.IsChecked = false;
                TestPageTab.IsChecked = false;
            }
        }
        private void VMTab_Tapped(object sender, TappedRoutedEventArgs e)
        {
            e.Handled = true;

            if (TabsFrame.CurrentSourcePageType != typeof(VMPage) && typeof(VMPage) != null)
            {
                TabsFrame.Navigate(typeof(VMPage), null, new SuppressNavigationTransitionInfo());
                MyComputerTab.IsChecked = false;
                MyComputer.IsChecked = false;
                VMsAppOS.IsChecked = true;
                VMTab.IsChecked = true;
                HomeTab.IsChecked = false;
                SharedVMsTab.IsChecked = false;
                SharedVMs.IsChecked = false;
                TestPageTab.IsChecked = false;
            }
        }
        private void VMTab_Click(object sender, RoutedEventArgs e)
        {
            MyComputerTab.IsChecked = false;
            MyComputer.IsChecked = false;
            VMsAppOS.IsChecked = true;
            VMTab.IsChecked = true;
            HomeTab.IsChecked = false;
            SharedVMsTab.IsChecked = false;
            SharedVMs.IsChecked = false;
            TestPageTab.IsChecked = false;
        }
        private void HomeTab_Tapped(object sender, TappedRoutedEventArgs e)
        {
            e.Handled = true;

            if (TabsFrame.CurrentSourcePageType != typeof(HomePage) && typeof(HomePage) != null)
            {
                TabsFrame.Navigate(typeof(HomePage), null, new SuppressNavigationTransitionInfo());
                MyComputerTab.IsChecked = false;
                MyComputer.IsChecked = false;
                VMsAppOS.IsChecked = false;
                VMTab.IsChecked = false;
                HomeTab.IsChecked = true;
                SharedVMsTab.IsChecked = false;
                SharedVMs.IsChecked = false;
                TestPageTab.IsChecked = false;
            }
        }
        private void HomeTab_Click(object sender, RoutedEventArgs e)
        {
            MyComputerTab.IsChecked = false;
            MyComputer.IsChecked = false;
            VMsAppOS.IsChecked = false;
            VMTab.IsChecked = false;
            HomeTab.IsChecked = true;
            SharedVMsTab.IsChecked = false;
            SharedVMs.IsChecked = false;
            TestPageTab.IsChecked = false;
        }
        private void SharedVMsTab_Click(object sender, RoutedEventArgs e)
        {
            MyComputerTab.IsChecked = false;
            MyComputer.IsChecked = false;
            VMsAppOS.IsChecked = false;
            VMTab.IsChecked = false;
            HomeTab.IsChecked = false;
            SharedVMsTab.IsChecked = true;
            SharedVMs.IsChecked = true;
            TestPageTab.IsChecked = false;
        }
        private void SharedVMsTab_Tapped(object sender, TappedRoutedEventArgs e)
        {
            e.Handled = true;

            if (TabsFrame.CurrentSourcePageType != typeof(SharedVMsPage) && typeof(SharedVMsPage) != null)
            {
                TabsFrame.Navigate(typeof(SharedVMsPage), null, new SuppressNavigationTransitionInfo());
                MyComputerTab.IsChecked = false;
                MyComputer.IsChecked = false;
                VMsAppOS.IsChecked = false;
                VMTab.IsChecked = false;
                HomeTab.IsChecked = false;
                SharedVMsTab.IsChecked = true;
                SharedVMs.IsChecked = true;
                TestPageTab.IsChecked = false;
            }
        }
        private void SharedVMs_Click(object sender, RoutedEventArgs e)
        {
            MyComputerTab.IsChecked = false;
            MyComputer.IsChecked = false;
            VMsAppOS.IsChecked = false;
            VMTab.IsChecked = false;
            HomeTab.IsChecked = false;
            SharedVMsTab.IsChecked = true;
            SharedVMs.IsChecked = true;
            TestPageTab.IsChecked = false;
        }
        private void SharedVMs_Tapped(object sender, TappedRoutedEventArgs e)
        {
            e.Handled = true;

            if (TabsFrame.CurrentSourcePageType != typeof(SharedVMsPage) && typeof(SharedVMsPage) != null)
            {
                TabsFrame.Navigate(typeof(SharedVMsPage), null, new SuppressNavigationTransitionInfo());
                MyComputerTab.IsChecked = false;
                MyComputer.IsChecked = false;
                VMsAppOS.IsChecked = false;
                VMTab.IsChecked = false;
                HomeTab.IsChecked = false;
                SharedVMsTab.IsChecked = true;
                SharedVMs.IsChecked = true;
                TestPageTab.IsChecked = false;
            }
        }
        private void TestTab_Click(object sender, RoutedEventArgs e)
        {
            MyComputerTab.IsChecked = false;
            MyComputer.IsChecked = false;
            VMsAppOS.IsChecked = false;
            VMTab.IsChecked = false;
            HomeTab.IsChecked = false;
            SharedVMsTab.IsChecked = false;
            SharedVMs.IsChecked = false;
            TestPageTab.IsChecked = true;
        }
        private void TestPageTab_Tapped(object sender, TappedRoutedEventArgs e)
        {
            e.Handled = true;

            if (TabsFrame.CurrentSourcePageType != typeof(TestPage) && typeof(TestPage) != null)
            {
                TabsFrame.Navigate(typeof(TestPage), null, new SuppressNavigationTransitionInfo());
                MyComputerTab.IsChecked = false;
                MyComputer.IsChecked = false;
                VMsAppOS.IsChecked = false;
                VMTab.IsChecked = false;
                HomeTab.IsChecked = false;
                SharedVMsTab.IsChecked = false;
                SharedVMs.IsChecked = false;
                TestPageTab.IsChecked = true;
            }
        }
        private void MyComputerTab_Checked(object sender, RoutedEventArgs e)
        {
            if (TabsFrame.CurrentSourcePageType != typeof(MyComputerPage))
            {
                TabsFrame.Navigate(typeof(MyComputerPage), null, new SuppressNavigationTransitionInfo());
            }
        }
        private void HomeTab_Checked(object sender, RoutedEventArgs e)
        {
            if (TabsFrame.CurrentSourcePageType != typeof(HomePage))
            {
                TabsFrame.Navigate(typeof(HomePage), null, new SuppressNavigationTransitionInfo());
            }
        }
        private void SharedVMsTab_Checked(object sender, RoutedEventArgs e)
        {
            if (TabsFrame.CurrentSourcePageType != typeof(SharedVMsPage))
            {
                TabsFrame.Navigate(typeof(SharedVMsPage), null, new SuppressNavigationTransitionInfo());
            }
        }
        private void VMTab_Checked(object sender, RoutedEventArgs e)
        {
            if (TabsFrame.CurrentSourcePageType != typeof(VMPage))
            {
                TabsFrame.Navigate(typeof(VMPage), null, new SuppressNavigationTransitionInfo());
            }
        }
        private void VMsAppOS_Checked(object sender, RoutedEventArgs e)
        {
            if (TabsFrame.CurrentSourcePageType != typeof(VMPage))
            {
                TabsFrame.Navigate(typeof(VMPage), null, new SuppressNavigationTransitionInfo());
            }
        }
        private void SharedVMs_Checked(object sender, RoutedEventArgs e)
        {
            if (TabsFrame.CurrentSourcePageType != typeof(SharedVMsPage))
            {
                TabsFrame.Navigate(typeof(SharedVMsPage), null, new SuppressNavigationTransitionInfo());
            }
        }
        private void TestPageTab_Checked(object sender, RoutedEventArgs e)
        {
            if (TabsFrame.CurrentSourcePageType != typeof(TestPage))
            {
                TabsFrame.Navigate(typeof(TestPage), null, new SuppressNavigationTransitionInfo());
            }
        }
        private void VMTab_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }
    }
}
