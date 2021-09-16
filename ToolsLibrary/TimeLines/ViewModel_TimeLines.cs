using DevelopmentTools.Base;
using LifeLines.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace DevelopmentTools.Tools.TimeLines
{
    public class ViewModel_TimeLines : IToolBaseInfo
    {
        private DateTime _Current;
        public DateTime Now
        {
            get
            {
                return DateTime.Now;
            }
        }
        
        public string ToolName => "TimeLines";

        public string ToolDesc => "时间与任务管理";
        Window_TimeLines window;
        public Window ToolWindow
        {
            get
            {
                if (window == null || !window.IsLoaded)
                {
                    window = new Window_TimeLines();
                    window.DataContext = this;
                }
                return window;
            }
        }
        public ImageSource ToolIcon => new BitmapImage(new Uri("/ToolsLibrary;component/Resources/timeline.png", UriKind.Relative));

        public string ToolAuthor => "ssm";

        public Color ToolThemeColor => Color.FromRgb(63, 81, 181);

        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        public BindingList<TimeLineTask> TimeLineTasks { get; set; } = new BindingList<TimeLineTask>();
    }
}
