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
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics;
using WinUIEx;

namespace VMsApp.Dialogs
{
    public sealed partial class FeatureNotAvailable : WindowEx
    {
        public FeatureNotAvailable()
        {
            this.InitializeComponent(); 
            ExtendsContentIntoTitleBar = true;
            AppWindow.Resize(new SizeInt32(400, 250));
            this.CenterOnScreen();
            SetTitleBar(FeatureNotAvailableTitleBar);
            FeatureNotAvailableTitleBar.Loaded += FeatureNotAvailableTitleBar_Loaded;
        }
        private void FeatureNotAvailableTitleBar_Loaded(object sender, RoutedEventArgs e)
        {
            // Parts get delay loaded. If you have the parts, make them visible.
            VisualStateManager.GoToState(FeatureNotAvailableTitleBar, "SubtitleTextVisible", false);
            VisualStateManager.GoToState(FeatureNotAvailableTitleBar, "HeaderVisible", false);
            VisualStateManager.GoToState(FeatureNotAvailableTitleBar, "ContentVisible", false);
            VisualStateManager.GoToState(FeatureNotAvailableTitleBar, "FooterVisible", false);

            // Run layout so we re-calculate the drag regions.
            FeatureNotAvailableTitleBar.InvalidateMeasure();
        }
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
