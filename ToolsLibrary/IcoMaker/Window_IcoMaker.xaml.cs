using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DevelopmentTools.Tools.IcoMaker
{
    /// <summary>
    /// Window_IcoMaker.xaml 的交互逻辑
    /// </summary>
    public partial class Window_IcoMaker : Window
    {
        public Window_IcoMaker()
        {
            InitializeComponent();
        }
        System.Windows.Forms.OpenFileDialog openFileDialog1=new System.Windows.Forms.OpenFileDialog();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            openFileDialog1.Filter = "PNG图标|*.png";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                (this.DataContext as ViewModel_IcoMaker).source = openFileDialog1.FileName;
            }
        }
        System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
        private void Output_Click(object sender, RoutedEventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                (this.DataContext as ViewModel_IcoMaker).output = folderBrowserDialog.SelectedPath;
            }
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            if ((this.DataContext as ViewModel_IcoMaker).sourceImg != null)
            {
                PngImage.BeginStoryboard((Storyboard)PngImage.FindResource("MoveLeft"));
                ConvertIcon.BeginStoryboard((Storyboard)ConvertIcon.FindResource("showConvertIcon"));
                ConvertIcon.IsEnabled = true;
            }
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            if ((this.DataContext as ViewModel_IcoMaker).sourceImg != null)
            {
                PngImage.BeginStoryboard((Storyboard)PngImage.FindResource("MoveRight"));
                ConvertIcon.BeginStoryboard((Storyboard)ConvertIcon.FindResource("hideConvertIcon"));
                ConvertIcon.IsEnabled = false;
            }
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            (this.DataContext as ViewModel_IcoMaker).command.Execute(this.DataContext);
        }
    }
}
