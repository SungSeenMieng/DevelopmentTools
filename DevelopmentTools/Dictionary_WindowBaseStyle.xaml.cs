using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace DevelopmentTools
{
    public partial class Dictionary_WindowBaseStyle
    {

        #region 调节窗口尺寸
        public enum ResizeDirection
        {
            Left = 1,
            Right = 2,
            Top = 3,
            TopLeft = 4,
            TopRight = 5,
            Bottom = 6,
            BottomLeft = 7,
            BottomRight = 8,
        }
        public const int WM_SYSCOMMAND = 0x112;
        public HwndSource _HwndSource;

        public Dictionary<ResizeDirection, Cursor> cursors = new Dictionary<ResizeDirection, Cursor>
        {
            {ResizeDirection.Top, Cursors.SizeNS},
            {ResizeDirection.Bottom, Cursors.SizeNS},
            {ResizeDirection.Left, Cursors.SizeWE},
            {ResizeDirection.Right, Cursors.SizeWE},
            {ResizeDirection.TopLeft, Cursors.SizeNWSE},
            {ResizeDirection.BottomRight, Cursors.SizeNWSE},
            {ResizeDirection.TopRight, Cursors.SizeNESW},
            {ResizeDirection.BottomLeft, Cursors.SizeNESW}
        };


        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        public void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton != MouseButtonState.Pressed)
            {
                FrameworkElement element = e.OriginalSource as FrameworkElement;
                if (element != null && !element.Name.Contains("Resize"))
                    (sender as Window).Cursor = Cursors.Arrow;
            }
        }

        public void ResizePressed(object sender, MouseEventArgs e)
        {
            this._HwndSource = PresentationSource.FromVisual((Visual)sender) as HwndSource;
            if (Window.GetWindow(sender as Rectangle).ResizeMode == ResizeMode.NoResize) return;
            if (Window.GetWindow(sender as Rectangle).WindowState == WindowState.Maximized) return;
            FrameworkElement element = sender as FrameworkElement;
            ResizeDirection direction = (ResizeDirection)Enum.Parse(typeof(ResizeDirection), element.Name.Replace("Resize", ""));
            Window.GetWindow(sender as Rectangle).Cursor = cursors[direction];
            if (e.LeftButton == MouseButtonState.Pressed)
                ResizeWindow(direction);
        }

        public void ResizeWindow(ResizeDirection direction)
        {
            SendMessage(_HwndSource.Handle, WM_SYSCOMMAND, (IntPtr)(61440 + direction), IntPtr.Zero);
        }
        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            (sender as Window).MouseMove += new MouseEventHandler(Window_MouseMove);
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed&&(sender as Window).IsEnabled)
            {
                GetCursorPos(out POINT pt);
                
                var bounds = System.Windows.Forms.Screen.FromPoint(new System.Drawing.Point(pt.X, pt.Y)).WorkingArea;
                (sender as Window).MaxHeight = bounds.Height+15;
                (sender as Window).MaxWidth = bounds.Width+20;
               (sender as Window).DragMove();
            }
        }
        public struct POINT
        {
            public int X;
            public int Y;
            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }
        }
        /// <summary>   
        /// 获取鼠标的坐标   
        /// </summary>   
        /// <param name="lpPoint">传址参数，坐标point类型</param>   
        /// <returns>获取成功返回真</returns>   
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetCursorPos(out POINT pt);
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {

            if ((sender as PasswordBox).Password.Length == 0)
            {
                (sender as PasswordBox).Background =(sender as PasswordBox).Resources["HintText"] as VisualBrush;
            }
            else
            {
                (sender as PasswordBox).Background = null;
            }

        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            ScaleTransform rtf = new ScaleTransform();
             (sender as Window).RenderTransform = rtf;
            DoubleAnimation animation = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromMilliseconds(200)));
            animation.AutoReverse = false;
            rtf.BeginAnimation(ScaleTransform.ScaleXProperty,animation);
            rtf.BeginAnimation(ScaleTransform.ScaleYProperty, animation);

        }


        private void Window_GotFocus(object sender, RoutedEventArgs e)
        {
         
        }
    }
   
  
}
