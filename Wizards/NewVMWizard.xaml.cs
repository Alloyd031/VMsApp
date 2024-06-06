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
using VMsApp.NewVMWizardPages;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics;
using WinUIEx;
using WinUIEx.Messaging;

namespace VMsApp.Wizards
{
    public sealed partial class NewVMWizard : WindowEx
    {
        private WindowMessageMonitor _msgMonitor;
        public NewVMWizard()
        {
            this.InitializeComponent(); 
            ExtendsContentIntoTitleBar = true;
            AppWindow.Resize(new SizeInt32(550, 550));
            this.CenterOnScreen();
            SetTitleBar(NewVMWizardTitleBar);

            this.ContentFrame.Navigate(typeof(Main));

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
            NewVMWizardTitleBar.Loaded += NewVMWizardTitleBar_Loaded;
        }
        private void NewVMWizardTitleBar_Loaded(object sender, RoutedEventArgs e)
        {
            // Parts get delay loaded. If you have the parts, make them visible.
            VisualStateManager.GoToState(NewVMWizardTitleBar, "SubtitleTextVisible", false);
            VisualStateManager.GoToState(NewVMWizardTitleBar, "HeaderVisible", false);
            VisualStateManager.GoToState(NewVMWizardTitleBar, "ContentVisible", false);
            VisualStateManager.GoToState(NewVMWizardTitleBar, "FooterVisible", false);

            // Run layout so we re-calculate the drag regions.
            NewVMWizardTitleBar.InvalidateMeasure();
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
