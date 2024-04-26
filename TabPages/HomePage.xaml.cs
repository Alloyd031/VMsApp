using Microsoft.UI.Input;
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
using WinUIEx;
using VMsApp.Wizards;

namespace VMsApp.TabPages
{
    public sealed partial class HomePage : Page
    {
        private Window m_window;
        public HomePage()
        {
            this.InitializeComponent();
        }
        public void ChangeCursor(InputCursor cursor)
        {
            this.ProtectedCursor = cursor;
        } 
        private void Button_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            ChangeCursor(InputSystemCursor.Create(InputSystemCursorShape.Hand));
        }
        private void Button_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            ChangeCursor(InputSystemCursor.Create(InputSystemCursorShape.Arrow));
        }
        private void CreateVMButton_Click(object sender, RoutedEventArgs e)
        {
            m_window = new NewVMWizard();
            m_window.Show();
        }
    }
}
