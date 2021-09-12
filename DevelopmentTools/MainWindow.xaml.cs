using DevelopmentTools.Base;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

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
    }
}
