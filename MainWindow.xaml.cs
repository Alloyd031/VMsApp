using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using System;
using Windows.UI.WindowManagement;
using WinUIEx;
using Windows.Graphics;
using VMsApp.Dialogs;
using VMsApp.Wizards;
using System.Runtime.InteropServices;
using Microsoft.UI.Input;
using Windows.UI.Core;
using VMsApp.VMSettingsPages;

namespace VMsApp
{
    public sealed partial class MainWindow : WindowEx
    {
        public Window m_window;
        public MainWindow()
        {
            this.InitializeComponent();
            ExtendsContentIntoTitleBar = true;
            AppWindow.Resize(new SizeInt32(1200, 700));
            this.CenterOnScreen();
            SetTitleBar(AppTitleBar);

            this.FolderView.Visibility = Visibility.Collapsed;
            this.TabsGrid.Margin = new Thickness(201, 88, 0, 32);

            if (ShowHideLibrary.IsChecked == false)
            {
                ShowHideLibrary.IsChecked = true;
            }
            AppTitleBar.Loaded += AppTitleBar_Loaded;
        }
        private void AppTitleBar_Loaded(object sender, RoutedEventArgs e)
        {
            // Parts get delay loaded. If you have the parts, make them visible.
            VisualStateManager.GoToState(AppTitleBar, "SubtitleTextVisible", false);
            VisualStateManager.GoToState(AppTitleBar, "HeaderVisible", false);
            VisualStateManager.GoToState(AppTitleBar, "ContentVisible", false);
            VisualStateManager.GoToState(AppTitleBar, "FooterVisible", false);

            // Run layout so we re-calculate the drag regions.
            AppTitleBar.InvalidateMeasure();
        }
        public static void CreateModalWindow(WindowEx parentWindow, WindowEx childWindow, bool summonWindowAutomatically = true, bool blockInput = false)
        {
            IntPtr hWndChildWindow = WinRT.Interop.WindowNative.GetWindowHandle(childWindow);
            IntPtr hWndParentWindow = WinRT.Interop.WindowNative.GetWindowHandle(parentWindow);
            SetWindowLong(hWndChildWindow, GWL_HWNDPARENT, hWndParentWindow);
            (childWindow.AppWindow.Presenter as OverlappedPresenter).IsModal = true;
            if (blockInput == true)
            {
                EnableWindow(hWndParentWindow, false);
                childWindow.Closed += ChildWindow_Closed;
                void ChildWindow_Closed(object sender, Microsoft.UI.Xaml.WindowEventArgs args)
                {
                    EnableWindow(hWndParentWindow, true);
                }
            }
            if (summonWindowAutomatically == true) childWindow.Show();
        }
        private static IntPtr SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
        {
            if (IntPtr.Size == 4)
            {
                return SetWindowLongPtr32(hWnd, nIndex, dwNewLong);
            }
            return SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
        }
        [DllImport("User32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool EnableWindow(IntPtr hWnd, bool bEnable);

        private const int GWL_HWNDPARENT = (-8);

        [DllImport("User32.dll", CharSet = CharSet.Auto, EntryPoint = "SetWindowLong")]
        private static extern IntPtr SetWindowLongPtr32(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("User32.dll", CharSet = CharSet.Auto, EntryPoint = "SetWindowLongPtr")]
        private static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, IntPtr dwNewLong);
        private void HideLibrary_Click(object sender, RoutedEventArgs e)
        {
            this.LibraryPanel.Visibility = Visibility.Collapsed;
            this.TabsGrid.Margin = new Thickness(0, 88, 0, 32);
            this.FolderView.Margin = new Thickness(0, 0, 0, 32);

            if (ShowHideLibrary.IsChecked == true)
            {
                ShowHideLibrary.IsChecked = false;
            }
        }
        private void CreateNewVM_Click(object sender, RoutedEventArgs e)
        {
            var logWin = new NewVMWizard();
            CreateModalWindow(App.m_window, logWin, true, true);
            App.currentWizardWin = logWin;
        }
        private void NewWindow_Click(object sender, RoutedEventArgs e)
        {
            m_window = new MainWindow();
            m_window.Activate();
        }
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            var logWin = new FeatureNotAvailable();
            CreateModalWindow(App.m_window, logWin, true, true);
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
                this.TabsGrid.Margin = new Thickness(201, 88, 0, 32);
                this.FolderView.Margin = new Thickness(202, 0, 0, 32);
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
        private void VMSettings_Click(object sender, RoutedEventArgs e)
        {
            var logWin = new VMSettings();
            CreateModalWindow(App.m_window, logWin, true, true);
        }
        private void About_Click(object sender, RoutedEventArgs e)
        {
            var logWin = new AboutApp();
            CreateModalWindow(App.m_window, logWin, true, true);
        }
        private void Button_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }
        private void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void MessageLog_Click(object sender, RoutedEventArgs e)
        {
            var logWin = new MessageLog();
            CreateModalWindow(App.m_window, logWin, true, true);
        }
        private void VMMessageLog_Click(object sender, RoutedEventArgs e)
        {
            var logWin = new MessageLog();
            CreateModalWindow(App.m_window, logWin, true, true);
        }
        private void Preferences_Click(object sender, RoutedEventArgs e)
        {
            var logWin = new Preferences();
            CreateModalWindow(App.m_window, logWin, true, true);
        }
    }
}
