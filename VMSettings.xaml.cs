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
using VMsApp.Dialogs;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics;
using WinUIEx;

namespace VMsApp
{
    public sealed partial class VMSettings : WindowEx
    {
        [DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool GetCursorPos(out Windows.Graphics.PointInt32 lpPoint);
        IntPtr hWnd = IntPtr.Zero;
        private Microsoft.UI.Windowing.AppWindow _apw;
        private bool bMoving = false;
        private int nX = 0, nY = 0, nXWindow = 0, nYWindow = 0;
        public VMSettings()
        {
            this.InitializeComponent();
            ExtendsContentIntoTitleBar = true;
            AppWindow.Resize(new SizeInt32(800, 800));
            this.CenterOnScreen();

            AppWindow.TitleBar.PreferredHeightOption = TitleBarHeightOption.Collapsed;

            hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            Microsoft.UI.WindowId myWndId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hWnd);
            _apw = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(myWndId);
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
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void VMSettingsTitleBar_PointerPressed(object sender, PointerRoutedEventArgs e)
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
        private void VMSettingsTitleBar_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            //nXWindow = _apw.Position.X;
            //nYWindow = _apw.Position.Y;
            ((UIElement)sender).ReleasePointerCaptures();
            bMoving = false;
        }
        private void VMSettingsTitleBar_PointerMoved(object sender, PointerRoutedEventArgs e)
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
