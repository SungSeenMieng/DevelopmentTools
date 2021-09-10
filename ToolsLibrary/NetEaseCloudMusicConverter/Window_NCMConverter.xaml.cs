using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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

namespace DevelopmentTools.Tools.NetEaseCloudMusicConverter
{
    /// <summary>
    /// Window_NCMConverter.xaml 的交互逻辑
    /// </summary>
    public partial class Window_NCMConverter : Window
    {
        public Window_NCMConverter()
        {
            InitializeComponent();
        }
        System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            openFileDialog1.Filter = "网易云音乐ncm文件|*.ncm";
            openFileDialog1.Multiselect = true;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ViewModel_NCMConverter data = this.DataContext as ViewModel_NCMConverter;
                foreach (var item in openFileDialog1.FileNames)
                {
                    bool IsExists = false;
                    foreach (var exists in data.ncms)
                    {
                        if (exists.FullName == item)
                        {
                            IsExists = true;
                            break;
                        }
                    }
                    if (!IsExists&&File.Exists(item))
                    {
                        FileInfo info = new FileInfo(item);
                        data.ncms.Add(info);
                    }
                }
                data.AppendFile();
            }
        }
        System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            switch ((sender as Border).Uid)
            {
                case "path":
                    if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        (this.DataContext as ViewModel_NCMConverter).outputPath = folderBrowserDialog.SelectedPath;
                    }
                    break;
                case "convert":
                    (this.DataContext as ViewModel_NCMConverter).Convert();
                    break;
                default:
                    break;
            }
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            if (Directory.Exists(Environment.CurrentDirectory + "\\temp"))
            {
                Directory.Delete(Environment.CurrentDirectory + "\\temp", true);
            }
            base.OnClosing(e);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            (this.DataContext as ViewModel_NCMConverter).outputCount = 0;
        }
    }
}
