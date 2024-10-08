﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SleepTimer
{
    /// <summary>
    /// Логика взаимодействия для StopPage.xaml
    /// </summary>
    public partial class StopPage : Window
    {
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }
        public StopPage()
        {
            InitializeComponent();
        }
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool LockWorkStation();

        private void LockComputer()
        {
            LockWorkStation();
        }
        private void ApplyClose_Click(object sender, RoutedEventArgs e)
        {
            LockComputer();
            Hide();
            this.Close();
        }
    }
}
