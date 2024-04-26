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

namespace VMsApp.Wizards
{
    public sealed partial class NewVMWizard : WindowEx
    {
        [DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool GetCursorPos(out Windows.Graphics.PointInt32 lpPoint);
        IntPtr hWnd = IntPtr.Zero;
        private Microsoft.UI.Windowing.AppWindow _apw;
        private bool bMoving = false;
        private int nX = 0, nY = 0, nXWindow = 0, nYWindow = 0;
        public NewVMWizard()
        {
            this.InitializeComponent(); 
            ExtendsContentIntoTitleBar = true;
            AppWindow.Resize(new SizeInt32(550, 550));
            this.CenterOnScreen();

            this.ContentFrame.Navigate(typeof(Main));

            AppWindow.TitleBar.PreferredHeightOption = TitleBarHeightOption.Collapsed;

            hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            Microsoft.UI.WindowId myWndId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hWnd);
            _apw = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(myWndId);
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void NewVMWizardTitleBar_PointerPressed(object sender, PointerRoutedEventArgs e)
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
        private void NewVMWizardTitleBar_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            //nXWindow = _apw.Position.X;
            //nYWindow = _apw.Position.Y;
            ((UIElement)sender).ReleasePointerCaptures();
            bMoving = false;
        }
        private void NewVMWizardTitleBar_PointerMoved(object sender, PointerRoutedEventArgs e)
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
