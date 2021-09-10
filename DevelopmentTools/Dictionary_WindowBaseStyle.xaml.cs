using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
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
            (sender as Window).Closing += Dictionary_WindowBaseStyle_Closing;
            (sender as Window).MouseMove += new MouseEventHandler(Window_MouseMove);
        }

        private void Dictionary_WindowBaseStyle_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if ((sender as Window).Uid != "Exit")
            {
                e.Cancel = true;
                StaticFunction.CloseAnimation(sender as Window);
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && (sender as Window).IsEnabled)
            {
                GetCursorPos(out POINT pt);

                var bounds = System.Windows.Forms.Screen.FromPoint(new System.Drawing.Point(pt.X, pt.Y)).WorkingArea;
                (sender as Window).MaxHeight = bounds.Height + 15;
                (sender as Window).MaxWidth = bounds.Width + 20;
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
                (sender as PasswordBox).Background = (sender as PasswordBox).Resources["HintText"] as VisualBrush;
            }
            else
            {
                (sender as PasswordBox).Background = null;
            }

        }

        private void Window_Button(object sender, MouseButtonEventArgs e)
        {
            switch ((sender as Border).Uid)
            {
                case "pin":
                    Window.GetWindow(sender as Border).Topmost = !Window.GetWindow(sender as Border).Topmost;
                    if (Window.GetWindow(sender as Border).Topmost)
                    {
                        ((sender as Border).Child as Image).Source = new BitmapImage(new Uri("/Resources/pinned.png", UriKind.Relative));
                    }
                    else
                    {
                        ((sender as Border).Child as Image).Source = new BitmapImage(new Uri("/Resources/pin-off.png", UriKind.Relative));
                    }
                    break;
                case "mini":
                    SystemCommands.MinimizeWindow(Window.GetWindow(sender as Border));
                    break;
                case "max":
                    if (Window.GetWindow(sender as Border).WindowState == WindowState.Maximized)
                    {
                        SystemCommands.RestoreWindow(Window.GetWindow(sender as Border));
                    }
                    else
                    {
                        SystemCommands.MaximizeWindow(Window.GetWindow(sender as Border));
                    }
                    break;

                case "close":
                    //StaticFunction.CloseAnimation(Window.GetWindow(sender as Border));
                    SystemCommands.CloseWindow(Window.GetWindow(sender as Border));
                    break;
                default:
                    break;
            }
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (Window.GetWindow(sender as Grid).WindowState == WindowState.Maximized)
                {
                    SystemCommands.RestoreWindow(Window.GetWindow(sender as Grid));
                }
                else
                {
                    SystemCommands.MaximizeWindow(Window.GetWindow(sender as Grid));
                }
            }
        }
        private void Window_GotFocus(object sender, RoutedEventArgs e)
        {

        }
    }
    public static class StaticFunction
    {
        public static void CloseAnimation(Window window)
        {
            Grid grid = ((window.Template as ControlTemplate).FindName("WindowGrid", window) as Grid);
            ScaleTransform rtf = new ScaleTransform();
            rtf.CenterX = 0.5;
            rtf.CenterY = 0.5;
            rtf.ScaleX = 1;
            rtf.ScaleY = 1;
            Storyboard sb = new Storyboard();
            DependencyProperty[] propertyChainx = new DependencyProperty[]
          {
                Grid.RenderTransformProperty,
                ScaleTransform.ScaleXProperty
          };
            DependencyProperty[] propertyChainy = new DependencyProperty[]
       {
                Grid.RenderTransformProperty,
                ScaleTransform.ScaleYProperty
       };
            grid.RenderTransform = rtf;


            DoubleAnimation animationx = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromMilliseconds(200)));
            DoubleAnimation animationy = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromMilliseconds(200)));
            animationx.AutoReverse = false;
            animationy.AutoReverse = false;
            Storyboard.SetTarget(animationx, grid);
            Storyboard.SetTarget(animationy, grid);
            Storyboard.SetTargetProperty(animationx, new PropertyPath("(0).(1)", propertyChainx));
            Storyboard.SetTargetProperty(animationy, new PropertyPath("(0).(1)", propertyChainy));
            //Storyboard.SetTargetProperty(animation, new PropertyPath(ScaleTransform.ScaleYProperty));
            sb.Children.Add(animationx);
            sb.Children.Add(animationy);
            sb.Completed += new EventHandler((a, b) =>
            {
                Timer timer;
                timer = new Timer();
                timer.Interval = 200;
                timer.Elapsed += new ElapsedEventHandler((c, d) =>
                {
                    window.Dispatcher.Invoke(new Action(() =>
                    {
                        window.Uid = "Exit";
                        SystemCommands.CloseWindow(window);
                    }));
                });
                timer.Start();
            });
            sb.Begin();
        }
    }

}
