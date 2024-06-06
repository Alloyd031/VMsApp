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
using VMsApp.PreferencesPages;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics;
using WinUIEx;
using WinUIEx.Messaging;

namespace VMsApp
{
    public sealed partial class Preferences : WindowEx
    {
        private WindowMessageMonitor _msgMonitor;
        public Preferences()
        {
            this.InitializeComponent(); 
            ExtendsContentIntoTitleBar = true;
            AppWindow.Resize(new SizeInt32(800, 700));
            this.CenterOnScreen();
            SetTitleBar(PreferencesWindowTitleBar);

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
            PreferencesWindowTitleBar.Loaded += PreferencesWindowTitleBar_Loaded;
        }
        private void PreferencesWindowTitleBar_Loaded(object sender, RoutedEventArgs e)
        {
            // Parts get delay loaded. If you have the parts, make them visible.
            VisualStateManager.GoToState(PreferencesWindowTitleBar, "SubtitleTextVisible", false);
            VisualStateManager.GoToState(PreferencesWindowTitleBar, "HeaderVisible", false);
            VisualStateManager.GoToState(PreferencesWindowTitleBar, "ContentVisible", false);
            VisualStateManager.GoToState(PreferencesWindowTitleBar, "FooterVisible", false);

            // Run layout so we re-calculate the drag regions.
            PreferencesWindowTitleBar.InvalidateMeasure();
        }
        private void NavigationView_ItemInvoked(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs args)
        {
            var item = args.InvokedItemContainer;
            switch (item.Name)
            {
                case "Workspace":
                    PreferencesFrame.Navigate(typeof(Workspace));
                    break;
                case "Input":
                    PreferencesFrame.Navigate(typeof(Input));
                    break;
                case "HotKeys":
                    PreferencesFrame.Navigate(typeof(HotKeys));
                    break;
                case "Display":
                    PreferencesFrame.Navigate(typeof(Display));
                    break;
                case "Unity":
                    PreferencesFrame.Navigate(typeof(Unity));
                    break;
                case "USB":
                    PreferencesFrame.Navigate(typeof(USB));
                    break;
                case "Updates":
                    PreferencesFrame.Navigate(typeof(Updates));
                    break;
                case "Feedback":
                    PreferencesFrame.Navigate(typeof(Feedback));
                    break;
                case "Memory":
                    PreferencesFrame.Navigate(typeof(Memory));
                    break;
                case "Priority":
                    PreferencesFrame.Navigate(typeof(Priority));
                    break;
                case "Devices":
                    PreferencesFrame.Navigate(typeof(Devices));
                    break;
            }
        }
        private void NavigationView_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (Microsoft.UI.Xaml.Controls.NavigationViewItemBase item in PreferencesNavView.MenuItems)
            {
                if (item is Microsoft.UI.Xaml.Controls.NavigationViewItem && item.Tag?.ToString() == "Workspace")
                {
                    PreferencesNavView.SelectedItem = item;
                    break;
                }
            }
            PreferencesFrame.Navigate(typeof(Workspace));
        }
    }
}
