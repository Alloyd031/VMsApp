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
        [DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool GetCursorPos(out Windows.Graphics.PointInt32 lpPoint);
        IntPtr hWnd = IntPtr.Zero;
        IntPtr hWndChild = IntPtr.Zero;
        private Microsoft.UI.Windowing.AppWindow _apw;
        private bool bMoving = false;
        private int nX = 0, nY = 0, nXWindow = 0, nYWindow = 0;
        public Window m_window;
        public MainWindow()
        {
            this.InitializeComponent();
            ExtendsContentIntoTitleBar = true;
            AppWindow.Resize(new SizeInt32(1404, 916));
            this.CenterOnScreen();

            hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            Microsoft.UI.WindowId myWndId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hWnd);
            _apw = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(myWndId);

            AppWindow.TitleBar.PreferredHeightOption = TitleBarHeightOption.Collapsed;

            this.FolderView.Visibility = Visibility.Collapsed;
            this.TabsGrid.Margin = new Thickness(201, 48, 0, 32);

            if (ShowHideLibrary.IsChecked == false)
            {
                ShowHideLibrary.IsChecked = true;
            }

            if (WindowState == WindowState.Normal)
            {
                MaximizeIcon.Glyph = "\uE922";
            }
            else
            {
                MaximizeIcon.Glyph = "\uE923";
            }
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
            this.TabsGrid.Margin = new Thickness(0, 48, 0, 32);
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
                this.TabsGrid.Margin = new Thickness(0, 48, 0, 32);
                this.FolderView.Margin = new Thickness(0, 48, 0, 32);
                ShowHideLibrary.IsChecked = false;
            }
            else
            {
                this.LibraryPanel.Visibility = Visibility.Visible;
                this.TabsGrid.Margin = new Thickness(201, 48, 0, 32);
                this.FolderView.Margin = new Thickness(202, 48, 0, 32);
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
        private void AppTitleBar_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            //nXWindow = _apw.Position.X;
            //nYWindow = _apw.Position.Y;
            ((UIElement)sender).ReleasePointerCaptures();
            bMoving = false;
        }
        private void AppTitleBar_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            var properties = e.GetCurrentPoint((UIElement)sender).Properties;
            if (properties.IsLeftButtonPressed)
            {
                ((UIElement)sender).CapturePointer(e.Pointer);
                nXWindow = _apw.Position.X;
                nYWindow = _apw.Position.Y;
                Windows.Graphics.PointInt32 pt;
                GetCursorPos(out pt);
                nX = pt.X;
                nY = pt.Y;
                //Console.Beep(1000, 10);
                bMoving = true;
            }
            else if (properties.IsRightButtonPressed)
            {

            }
        }
        private void AppTitleBar_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            //Microsoft.UI.Input.PointerPoint pp = e.GetCurrentPoint((UIElement)sender);
            //Point ptElement = new Point(pp.Position.X, pp.Position.Y);
            //IEnumerable<UIElement> elementStack = VisualTreeHelper.FindElementsInHostCoordinates(ptElement, (UIElement)sender);
            //foreach (UIElement item in elementStack)
            //{
            //    FrameworkElement feItem = item as FrameworkElement;
            //    //cast to FrameworkElement, need the Name property
            //    if (feItem != null)
            //    {
            //        if (feItem.Name.Equals("myButton"))
            //        {
            //            return;
            //        }
            //    }
            //}

            var properties = e.GetCurrentPoint((UIElement)sender).Properties;
            if (properties.IsLeftButtonPressed)
            {
                //Console.Beep(8000, 10);

                Windows.Graphics.PointInt32 pt;
                GetCursorPos(out pt);

                //if (((UIElement)sender).GetType() == typeof(StackPanel))
                if (bMoving)
                    _apw.Move(new Windows.Graphics.PointInt32(nXWindow + (pt.X - nX), nYWindow + (pt.Y - nY)));

                //Microsoft.UI.Input.PointerPoint pp = e.GetCurrentPoint((UIElement)sender);
                //pt.X -= (int)pp.Position.X;
                //pt.Y -= (int)pp.Position.Y;
                //pt.X -=8;
                //pt.Y -= 31;
                //Windows.Graphics.PointInt32 pt = new Windows.Graphics.PointInt32((int)pp.Position.X, (int)pp.Position.Y);               
                //IntPtr pPoint = Marshal.AllocHGlobal(Marshal.SizeOf(pt));
                //Marshal.StructureToPtr(pt, pPoint, false);
                //PostMessage(hWnd, WM_NCLBUTTONDOWN, HTCAPTION, pPoint);
                e.Handled = true;
            }
        }
    }
}
