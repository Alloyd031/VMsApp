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
using Windows.Foundation;
using Windows.Foundation.Collections;
using VMsApp.Dialogs;
using VMsApp.VMSettingsPages.VMSettingsHardware;

namespace VMsApp.VMSettingsPages
{
    public sealed partial class Hardware : Page
    {
        private Window m_window;
        public Hardware()
        {
            this.InitializeComponent();
        }
        private void HardwareNavView_ItemInvoked(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs args)
        {
            var item = args.InvokedItemContainer;
            switch (item.Name)
            {
                case "Memory":
                    HardwareFrame.Navigate(typeof(Memory));
                    break;
                case "Processors":
                    HardwareFrame.Navigate(typeof(Processors));
                    break;
                case "HardDisk":
                    HardwareFrame.Navigate(typeof(NotAvailable));
                    break;
                case "CDDVD":
                    HardwareFrame.Navigate(typeof(NotAvailable));
                    break;
                case "NetworkAdapter":
                    HardwareFrame.Navigate(typeof(NotAvailable));
                    break;
                case "USBController":
                    HardwareFrame.Navigate(typeof(NotAvailable));
                    break;
                case "SoundCard":
                    HardwareFrame.Navigate(typeof(NotAvailable));
                    break;
                case "Display":
                    HardwareFrame.Navigate(typeof(NotAvailable));
                    break;
            }
        }
        private void HardwareNavView_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (Microsoft.UI.Xaml.Controls.NavigationViewItemBase item in HardwareNavView.MenuItems)
            {
                if (item is Microsoft.UI.Xaml.Controls.NavigationViewItem && item.Tag?.ToString() == "Memory")
                {
                    HardwareNavView.SelectedItem = item;
                    break;
                }
            }
            HardwareFrame.Navigate(typeof(Memory));
        }
        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            m_window = new NotAvailableWindow();
            m_window.Activate();
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            m_window = new NotAvailableWindow();
            m_window.Activate();
        }
    }
}