using DevelopmentTools.Base;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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
            (sender as Border).BorderBrush = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Border).BorderBrush = null;
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
            if (this.ActualWidth < 360)
            {
               
                ToolsList.ItemTemplate = (DataTemplate)this.FindResource("ToolViewSmall");
                ToolsList.ItemContainerStyle = (Style)this.FindResource("SmallStyle");

            }
            else
            {

                ToolsList.ItemTemplate = (DataTemplate)this.FindResource("ToolViewTile");
                ToolsList.ItemContainerStyle = (Style)this.FindResource("TileStyle");
            }
        }
    }
}
