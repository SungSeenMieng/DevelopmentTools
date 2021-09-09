using DevelopmentTools.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DevelopmentTools.Tools.IcoMaker
{
    public class ViewModel_IcoMaker : IToolBaseInfo, INotifyPropertyChanged
    {
        public string ToolName => "图标转换工具";
        public string ToolDesc => "将PNG图标转换为Microsoft Icon(*.ico)图标文件";
        public Window ToolWindow
        {
            get
            {
                return window;
            }
        }
        public ImageSource ToolIcon
        {
            get
            {
                return new BitmapImage(new Uri("/ToolsLibrary;component/Resources/IcoMaker.png",UriKind.Relative));
            }
        }
        public System.Windows.Media.Color ToolThemeColor => System.Windows.Media.Color.FromRgb(18,150,219);
        public string ToolAuthor => "ssm";

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        Window_IcoMaker window;
        private string _source;
        private string _output;
        public string source
        {
            get
            {
                return _source;
            }
            set
            {
                _source = value;
                OnPropertyChanged("source");
                OnPropertyChanged("sourceImg");
            }
        }
        public string output
        {
            get
            {
                return _output;
            }
            set
            {
                _output = value;
                OnPropertyChanged("output");
            }
        }
        public ImageSource sourceImg
        {
            get
            {
                if (_source == null)
                {
                    return null;
                }
                return new BitmapImage(new Uri(_source, UriKind.Absolute));
            }
        }
        private Image image
        {
            get
            {
                return Bitmap.FromFile(_source);
            }
        }
        public Icon ConvertToIcon(Image image, bool nullTonull = false)
        {
            if (image == null)
            {
                if (nullTonull) { return null; }
                throw new ArgumentNullException("image");
            }
            using (MemoryStream msImg = new MemoryStream(), msIco = new MemoryStream())
            {
                image.Save(msImg, ImageFormat.Png);
                using (var bin = new BinaryWriter(msIco))
                {
                    //写图标头部
                    bin.Write((short)0);           //0-1保留
                    bin.Write((short)1);           //2-3文件类型。1=图标, 2=光标
                    bin.Write((short)1);           //4-5图像数量（图标可以包含多个图像）
                    bin.Write((byte)image.Width);  //6图标宽度
                    bin.Write((byte)image.Height); //7图标高度
                    bin.Write((byte)0);            //8颜色数（若像素位深>=8，填0。这是显然的，达到8bpp的颜色数最少是256，byte不够表示）
                    bin.Write((byte)0);            //9保留。必须为0
                    bin.Write((short)0);           //10-11调色板
                    bin.Write((short)32);          //12-13位深
                    bin.Write((int)msImg.Length);  //14-17位图数据大小
                    bin.Write(22);                 //18-21位图数据起始字节

                    //写图像数据
                    bin.Write(msImg.ToArray());

                    bin.Flush();
                    bin.Seek(0, SeekOrigin.Begin);
                    return new Icon(msIco);
                }
            }
        }
        public Command_Convert command { get; set; } = new Command_Convert();



        public void Convert()
        {
            Icon icon = ConvertToIcon(image);
            FileInfo info = new FileInfo(source);
            if (string.IsNullOrEmpty(output))
            {
                output = info.Directory.FullName;
            }
            Stream stream = File.Create(Path.Combine(output,info.Name.Replace("png", "ico")));
            icon.Save(stream);
            stream.Flush();
            stream.Close();
            MessageBox.Show("转换完成");
        }

  
        public ViewModel_IcoMaker()
        {
            window = new Window_IcoMaker();
            window.DataContext = this;
        }
    }
}
