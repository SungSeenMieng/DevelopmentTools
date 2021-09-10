using DevelopmentTools.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DevelopmentTools.Tools.NetEaseCloudMusicConverter
{
    public class ViewModel_NCMConverter : IToolBaseInfo, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        public string ToolName => "网易云音乐ncm格式转换工具";

        public string ToolDesc => "网易云音乐ncm格式=>mp3格式";

        public Window ToolWindow
        {
            get
            {
                if (window == null || !window.IsLoaded)
                {
                    window = new Window_NCMConverter();
                    window.DataContext = this;
                }
                return window;
            }
        }

        public ImageSource ToolIcon => new BitmapImage(new Uri("/ToolsLibrary;component/Resources/netease-cloud-music.png", UriKind.Relative));

        public string ToolAuthor => "ssm";

        public Color ToolThemeColor => Color.FromRgb(198,47,47);
        Window_NCMConverter window;

        public BindingList<FileInfo> ncms { get; set; } = new BindingList<FileInfo>();
        bool HasOutput = false;
        private string _outputPath;
        string[] pathlist = new string[300];
        string[] filelist = new string[300];
        public void AppendFile()
        {
            int filecount = 0;
            foreach (var info in ncms)
            {
                pathlist[filecount] = info.FullName;
                string[] buf = new string[10];
                buf = info.FullName.Split('\\');
                filelist[filecount] = buf.Last();
                //MessageBox.Show(pathlist[filecount]);
                //MessageBox.Show(filelist[filecount]);
                filecount++;
            }
            OnPropertyChanged("ConvertBtnString");
        }
        public string outputPath
        {
            get
            {
                return _outputPath;
            }
            set
            {
                if (Directory.Exists(value))
                {
                    _outputPath = value;
                    OnPropertyChanged("outputPath");
                }
            }
        }
        private int _outputCount=0;
        public int outputCount
        {
            get
            {
                return _outputCount;
            }
            set
            {
                _outputCount = value;
                OnPropertyChanged("ProgressValue");
            }
        }
        public double ProgressValue
        {
            get
            {
                return outputCount * 668d / ncms.Count;
            }
            
        }
        public void Convert()
        {
            if (ncms.Count == 0) { MessageBox.Show("队列空"); }
            else
            {
                if (string.IsNullOrEmpty(outputPath)) { MessageBox.Show("请设置导出路径"); }
                else
                {
                    try
                    {
                        Thread thread = new Thread(() =>
                        {
                            if (Directory.Exists(Environment.CurrentDirectory + "\\temp"))
                            {
                                Directory.Delete(Environment.CurrentDirectory + "\\temp", true);
                            }
                            System.IO.Directory.CreateDirectory(Environment.CurrentDirectory + "\\temp");
                            for (int i = 0; i < ncms.Count; i++)
                            {
                                File.Copy(pathlist[i], Environment.CurrentDirectory + "\\temp\\temp" + i + ".ncm",true);
                                string str = "\"" + Environment.CurrentDirectory + "\\NetEaseCloudMusicConverter\\extern\\ncmtrans.exe\" " + "\"" + Environment.CurrentDirectory + "\\temp\\temp" + i + ".ncm\"";
                                System.Diagnostics.Process p = new System.Diagnostics.Process();
                                p.StartInfo.FileName = "cmd.exe";
                                p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
                                p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
                                p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
                                p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
                                p.StartInfo.StandardOutputEncoding = Encoding.UTF8;
                                p.StartInfo.StandardErrorEncoding = Encoding.UTF8;
                                p.StartInfo.CreateNoWindow = true;//不显示程序窗口
                                p.Start();//启动程序
                                p.StandardInput.WriteLine(str + "&exit");
                                p.StandardInput.AutoFlush = true;
                                string output = p.StandardOutput.ReadToEnd();
                                output = Regex.Split(output, "&exit", RegexOptions.IgnoreCase)[1];
                                output = System.Text.RegularExpressions.Regex.Replace(output, "[\r\n\t]", "");
                                p.WaitForExit();//等待程序执行完退出进程
                                p.Close();

                                string[] buf0 = new string[2];
                                buf0 = output.Split('.');
                                //MessageBox.Show(output);
                                //MessageBox.Show(textBox1.Text + "\\" + filelist[i].Split('.')[0] + "." + buf0[1]);
                                str = "copy \"" + output + "\" \"" + outputPath + "\\" + filelist[i].Split('.')[0] + "." + buf0[1] + "\"";
                                p = new System.Diagnostics.Process();
                                p.StartInfo.FileName = "cmd.exe";
                                p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
                                p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
                                p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
                                p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
                                p.StartInfo.StandardOutputEncoding = Encoding.UTF8;
                                p.StartInfo.StandardErrorEncoding = Encoding.UTF8;
                                p.StartInfo.CreateNoWindow = true;//不显示程序窗口
                                p.Start();//启动程序
                                          //向cmd窗口发送输入信息
                                p.StandardInput.WriteLine(str + "&exit");
                                p.StandardInput.AutoFlush = true;
                                p.Close();
                                outputCount = i + 1;
                            }
                            MessageBox.Show("导出结束");
                            filelist = new string[300];
                            pathlist = new string[300];
                            window.Dispatcher.Invoke(new Action(() => { ncms.Clear();

                            }));
                            outputCount = 0;
                        });
                        thread.IsBackground = true;
                        thread.Start();
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine(ex.ToString());
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }
       
        public string ConvertBtnString
        {
            get
            {
                return string.Format("开始转换 {0} 个文件", ncms.Count);
            }
        }
        public ViewModel_NCMConverter()
        {

        }

    }
}
