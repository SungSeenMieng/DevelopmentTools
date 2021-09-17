using DevelopmentTools.Base;
using Microsoft.Win32.SafeHandles;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace DevelopmentTools
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            (this.DataContext as MainViewModel).exit.Execute(this.DataContext);
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
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
            (sender as Border).RenderTransform = rtf;


            DoubleAnimation animationx = new DoubleAnimation(1, 1.02, new Duration(TimeSpan.FromMilliseconds(100)));
            DoubleAnimation animationy = new DoubleAnimation(1, 1.02, new Duration(TimeSpan.FromMilliseconds(100)));
            animationx.AutoReverse = false;
            animationy.AutoReverse = false;
            Storyboard.SetTarget(animationx, (sender as Border));
            Storyboard.SetTarget(animationy, (sender as Border));
            Storyboard.SetTargetProperty(animationx, new PropertyPath("(0).(1)", propertyChainx));
            Storyboard.SetTargetProperty(animationy, new PropertyPath("(0).(1)", propertyChainy));
            //Storyboard.SetTargetProperty(animation, new PropertyPath(ScaleTransform.ScaleYProperty));
            sb.Children.Add(animationx);
            sb.Children.Add(animationy);
            sb.Completed += (a, b) => { (sender as Border).BorderBrush = new SolidColorBrush(Color.FromRgb(255, 255, 255)); };
            sb.Begin();
          
          

        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            ScaleTransform rtf = new ScaleTransform();
            rtf.CenterX = 0.5;
            rtf.CenterY = 0.5;
            rtf.ScaleX = 1.02;
            rtf.ScaleY = 1.02;
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
            (sender as Border).RenderTransform = rtf;


            DoubleAnimation animationx = new DoubleAnimation(1.02, 1, new Duration(TimeSpan.FromMilliseconds(100)));
            DoubleAnimation animationy = new DoubleAnimation(1.02, 1, new Duration(TimeSpan.FromMilliseconds(100)));
            animationx.AutoReverse = false;
            animationy.AutoReverse = false;
            Storyboard.SetTarget(animationx, (sender as Border));
            Storyboard.SetTarget(animationy, (sender as Border));
            Storyboard.SetTargetProperty(animationx, new PropertyPath("(0).(1)", propertyChainx));
            Storyboard.SetTargetProperty(animationy, new PropertyPath("(0).(1)", propertyChainy));
            //Storyboard.SetTargetProperty(animation, new PropertyPath(ScaleTransform.ScaleYProperty));
            sb.Children.Add(animationx);
            sb.Children.Add(animationy);
            sb.Completed += (a, b) => { (sender as Border).BorderBrush = null; };
            sb.Begin();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isMouseDown = true;
            var viewElement = sender as Border;
            if (viewElement == null) return;
            viewElement.MouseMove += ToolsList_MouseMove;
            if (e.ClickCount == 2)
            {
                Window window = ((sender as Border).DataContext as ToolModel).instance.ToolWindow;

                if (!window.IsLoaded)
                {
                    window.Topmost = false;
                    window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    window.Style = (Style)this.FindResource("WindowBaseStyleWithScaleAnimation");
                    window.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                }
                window.Topmost = this.Topmost;
                if (window.IsVisible)
                {
                    window.Close();
                }
                else
                {
                    window.Show();

                }
                e.Handled = true;
            }
        }
        public void ToolWindow_Closing(object sender, CancelEventArgs e)
        {
            //e.Cancel = true;
            //(sender as Window).Hide();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var key = ToolsList.ItemTemplate.DataTemplateKey;
            if (this.ActualWidth < 383)
            {
                this.Title = "";
            }
            else
            {
                this.Title = "Development Tools";
            }
            if (this.ActualWidth < 383)
            {
                ToolsList.ItemTemplate = (DataTemplate)this.FindResource("ToolViewTiny");
                ToolsList.ItemContainerStyle = (Style)this.FindResource("TinyStyle");
            }
            else if (this.ActualWidth < 985)
            {
                if (this.ActualHeight < 210)
                {
                    ToolsList.ItemTemplate = (DataTemplate)this.FindResource("ToolViewTiny");
                    ToolsList.ItemContainerStyle = (Style)this.FindResource("TinyStyle");
                }
                else
                {
                    ToolsList.ItemTemplate = (DataTemplate)this.FindResource("ToolViewSmall");
                    ToolsList.ItemContainerStyle = (Style)this.FindResource("SmallStyle");
                }
            }
            else
            {
               
                if (this.ActualHeight > 580)
                {
                    ToolsList.ItemTemplate = (DataTemplate)this.FindResource("ToolViewLarge");
                    ToolsList.ItemContainerStyle = (Style)this.FindResource("LargeStyle");
                }
                else
                {
                    ToolsList.ItemTemplate = (DataTemplate)this.FindResource("ToolViewTile");
                    ToolsList.ItemContainerStyle = (Style)this.FindResource("TileStyle");
                }

            }
        }
      
        bool isMouseDown = false;
        Cursor HoloCursor;
        private void ToolsList_MouseMove(object sender, MouseEventArgs e)
        {
            var viewElement = sender as Border;
            if (viewElement == null) return;
            ToolModel model = ToolsList.SelectedItem as ToolModel;
            if (model == null) return;
            HoloCursor= CreateCursor(viewElement);
            (this.DataContext as MainViewModel).tools.Remove(model);
            viewElement.GiveFeedback += DragSource_GiveFeedback;
            DragDrop.DoDragDrop(viewElement, model, DragDropEffects.Copy);
            viewElement.MouseMove -= ToolsList_MouseMove;
            viewElement.GiveFeedback -= DragSource_GiveFeedback;
            Mouse.SetCursor(Cursors.Arrow);
        
        }
        void DragSource_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            var viewElement = sender as Border;
            if (viewElement == null) return;
            Mouse.SetCursor(HoloCursor);
            e.UseDefaultCursors = false;
            e.Handled = true;
        }
        private void ToolsList_Drop(object sender, DragEventArgs e)
        {
            ToolModel model = (ToolModel)e.Data.GetData(typeof(ToolModel));
            if (model == null) return;
         

        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isMouseDown = false;
        }
        public Cursor CreateCursor(UIElement element)
        {
            element.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            element.Arrange(new Rect(new Point(), element.DesiredSize));

            var rtb =
              new RenderTargetBitmap(
                (int)element.DesiredSize.Width,
                (int)element.DesiredSize.Height,
                96, 96, PixelFormats.Pbgra32);

            rtb.Render(element);

            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(rtb));

            using (var ms = new MemoryStream())
            {
                encoder.Save(ms);
                using (var bmp = new System.Drawing.Bitmap(ms))
                {
                    return InternalCreateCursor(bmp);
                }
            }
        }
        private Cursor InternalCreateCursor(System.Drawing.Bitmap bmp)
        {
            var iconInfo = new NativeMethods.IconInfo();
            NativeMethods.GetIconInfo(bmp.GetHicon(), ref iconInfo);

            iconInfo.xHotspot = 125;
            iconInfo.yHotspot = 65;
            iconInfo.fIcon = false;

            SafeIconHandle cursorHandle = NativeMethods.CreateIconIndirect(ref iconInfo);
            return CursorInteropHelper.Create(cursorHandle);
        }
      
    }
    public class NativeMethods
    {
        public struct IconInfo
        {
            public bool fIcon;
            public int xHotspot;
            public int yHotspot;
            public IntPtr hbmMask;
            public IntPtr hbmColor;
        }

        [DllImport("user32.dll")]
        public static extern SafeIconHandle CreateIconIndirect(ref IconInfo icon);

        [DllImport("user32.dll")]
        public static extern bool DestroyIcon(IntPtr hIcon);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetIconInfo(IntPtr hIcon, ref IconInfo pIconInfo);
    }
    [SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
    public class SafeIconHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        public SafeIconHandle()
            : base(true)
        {
        }

        override protected bool ReleaseHandle()
        {
            return NativeMethods.DestroyIcon(handle);
        }
    }
}
