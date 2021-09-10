using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DevelopmentTools.Tools.PaintBoard
{
    /// <summary>
    /// Window_PaintBoard.xaml 的交互逻辑
    /// </summary>
    public partial class Window_PaintBoard : Window
    {
        public Window_PaintBoard()
        {
            InitializeComponent(); 
        }

        ViewModel_PaintBoard data
        {
            get
            {
                return this.DataContext as ViewModel_PaintBoard;
            }
        }
        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            data.StartPaint = e.GetPosition(sender as Canvas);
            e.Handled = true;
        }

        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            data.EndPaint = e.GetPosition(sender as Canvas);
            e.Handled = true;
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                data.Draw = e.GetPosition(sender as Canvas);
                e.Handled = true;
            }
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            content.ItemsSource = null;
            base.OnClosing(e);
        }

        private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                data.Redo();
            }
            else
            {
                data.Undo();
            }
        }
    }

}
