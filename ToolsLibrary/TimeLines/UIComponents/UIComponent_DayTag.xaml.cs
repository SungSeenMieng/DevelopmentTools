using LifeLines.BaseModel;
using LifeLines.BaseRoutine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LifeLines.UIComponents
{
    /// <summary>
    /// UIComponent_Day.xaml 的交互逻辑
    /// </summary>
    public partial class UIComponent_DayTag : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        public UIComponent_DayTag(double TagTime)
        {
            this.TagTime = StaticFunction.ToDateTime((long)TagTime);
            TimeStamp = TagTime;
            InitializeComponent();
            Canvas.SetLeft(this, left);
        }
        private DateTime _TagTime;
        public DateTime TagTime
        {
            get
            {
                return _TagTime;
            }
            set
            {
                _TagTime = value;
            }
        }
        private double TimeStamp;
        public string TimeString
        {
            get
            {
                return TagTime.Day.ToString()+"日";
            }
        }
        public double left
        {
            get
            {
                return ((TimeStamp - StaticData.FocusDatetime) * StaticData.TimeScalePerPixie ) + StaticData.DisplayPixies - 30;
            }
        }
    }
}
