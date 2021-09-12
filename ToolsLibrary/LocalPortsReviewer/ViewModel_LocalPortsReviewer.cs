using DevelopmentTools.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Management;
using Icon = System.Drawing.Icon;
using System.IO;

namespace DevelopmentTools.Tools.LocalPortsReviewer
{
    public class ViewModel_LocalPortsReviewer : IToolBaseInfo
    {
        public string ToolName => "本地网络端口管理器";

        public string ToolDesc => "查看本地网络端口使用情况，管理信息";

        public Window ToolWindow
        {
            get
            {
                if (window == null || !window.IsLoaded)
                {
                    window = new Window_LocalPortsReviewer();
                    window.DataContext = this;
                }
                return window;
            }
        }

        public ImageSource ToolIcon => new BitmapImage(new Uri("/ToolsLibrary;component/Resources/LocalPortsReviewer.png", UriKind.Relative));

        public string ToolAuthor => "ssm";

        public Color ToolThemeColor => Color.FromRgb(85, 0, 0);

        Window_LocalPortsReviewer window;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        private int _RestPort = 0;
        public string RestPort
        {
            get
            {
                return _RestPort.ToString();
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _RestPort = 0;
                }
                try
                {

                    int i = int.Parse(value);
                    if (i > 65535)
                    {
                        i = 65535;
                    }
                    _RestPort = i;

                }
                catch
                {

                }
                OnPropertyChanged("RestPort");
            }
        }
        Process process;
        private string _ProcessName;
        public string ProcessName
        {
            get
            {
                return _ProcessName;
            }
            set
            {
                _ProcessName = value;
                OnPropertyChanged("ProcessName");
            }
        }
        private string _ProcessStartTime;
        public string ProcessStartTime
        {
            get
            {
                return _ProcessStartTime;
            }
            set
            {
                this._ProcessStartTime = value;
                OnPropertyChanged("ProcessStartTime");
            }
        }
        private string _ProcessPath;
        public string ProcessPath
        {
            get
            {
                return _ProcessPath;
            }
            set
            {
                _ProcessPath = value;
                OnPropertyChanged("ProcessPath");
            }
        }
        private string _ProcessSize;
        public string ProcessSize
        {
            get
            {
                return _ProcessSize;
            }
            set
            {
                _ProcessSize = value;
                OnPropertyChanged("ProcessSize");
            }
        }
        private ImageSource _ProcessIcon;
        public ImageSource ProcessIcon
        {
            get
            {
                return _ProcessIcon;
            }
            set
            {
                _ProcessIcon = value;
                OnPropertyChanged("ProcessIcon");
            }
        }
        string[,] chart = new string[1600, 5];
        public BindingList<PortInfo> Infos { get; set; } = new BindingList<PortInfo>();
        public void GetInfo()
        {
            Infos.Clear();
            chart = new string[1600, 5];
            string str;
            if (_RestPort == 0) { str = "netstat -ano"; }
            else
            {
                str = "netstat -ano|findstr /c:\":" + _RestPort + " \"";
            }
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            p.StartInfo.CreateNoWindow = true;//不显示程序窗口
            p.Start();//启动程序
            p.StandardInput.WriteLine(str + "&exit");
            p.StandardInput.AutoFlush = true;
            string output = p.StandardOutput.ReadToEnd();
            // textBox1.Text=output;
            string[] strArray1;
            if (_RestPort == 0) { strArray1 = output.Split(new string[] { "PID" }, StringSplitOptions.RemoveEmptyEntries); }
            else
            {
                strArray1 = output.Split(new string[] { "&exit" }, StringSplitOptions.RemoveEmptyEntries);
            }
            string output1 = strArray1[1];
            if (strArray1[1] == null || strArray1[1] == "\r\n") { MessageBox.Show("该端口无进程"); }
            else
            {
                output1 = output1.Replace("*:*", "*:* 无 ");
                // textBox1.Text = output1;
                string[] strArray = new string[8000];
                Array.Clear(strArray, 0, strArray.Length);
                Array.Clear(chart, 0, chart.Length);
                string strArray2 = getnewstr(output1);
                strArray = strArray2.Split(' ');
                for (int i = 0; i < strArray.Length; i++)
                {
                    chart[i / 5, i % 5] = strArray[i];
                }
                for (int j = 0; j < 1600; j++)
                {
                    if (string.IsNullOrEmpty(chart[j, 0])) { break; }
                    else
                    {
                        PortInfo info = new PortInfo()
                        {
                            ProtoType = chart[j, 0],
                            LocalAddress = chart[j, 1],
                            RemoteAddress = chart[j, 2],
                            Status = chart[j, 3],
                            Pid = chart[j, 4]
                        };
                        Infos.Add(info);
                    }
                }
            }
        }
        public string getnewstr(string str)
        {
            return Regex.Replace(str.Trim(), "\\s+", " "); ;
        }
        public void GetProcessInfo(int Pid)
        {
            process = Process.GetProcessById(Pid);
            ProcessName =  process.ProcessName;
            ProcessSize = "进程内存:\r\n"+process.PrivateMemorySize64.ToString()+"字节";
            ProcessStartTime ="开始时间:\r\n"+ process.StartTime.ToString("yyyy-MM-dd HH:mm:ss");
            ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("select ProcessId, ExecutablePath from Win32_Process where ProcessId = \"" + Pid + "\"");
            using (var results = managementObjectSearcher.Get())
            {
                var processes = results.Cast<ManagementObject>().Select(p => new
                {
                    ProcessId = (UInt32)p["ProcessId"],
                    ExecutablePath = (string)p["ExecutablePath"]
                });
                Console.WriteLine();
                foreach (var p in processes)
                {
                    ProcessPath = "进程路径:" + p.ExecutablePath;
                    Icon icon = System.Drawing.Icon.ExtractAssociatedIcon(p.ExecutablePath);
                    ProcessIcon = BitmapToBitmapImage(icon.ToBitmap());
                }
            }
        }
        public BitmapImage BitmapToBitmapImage(System.Drawing.Bitmap bitmap)
        {
            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            BitmapImage bit3 = new BitmapImage();
            bit3.BeginInit();
            bit3.StreamSource = ms;
            bit3.EndInit();
            return bit3;
        }
        public void TurnToProcess()
        {
            if (File.Exists(ProcessPath.Replace("进程路径:", "").Trim()))
            {
                Process.Start(ProcessPath.Replace("进程路径:", "").Trim());
            }
        }
        public void TurnToPath()
        {
            if (File.Exists(ProcessPath.Replace("进程路径:","").Trim()))
            {
                FileInfo info = new FileInfo(ProcessPath.Replace("进程路径:", "").Trim());

                Process.Start(info.DirectoryName);
            }
        }
        public void KillProcess()
        {
            if (MessageBox.Show("是否确定结束进程！", "警告", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                process.Kill();
            }
            GetInfo();
        }
    }

    public class PortInfo
    {
        public string ProtoType { get; set; }
        public string LocalAddress { get; set; }
        public string RemoteAddress { get; set; }
        public string Status { get; set; }
        public string Pid { get; set; }
    }
}
