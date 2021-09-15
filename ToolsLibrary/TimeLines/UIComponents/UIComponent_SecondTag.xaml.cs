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
    /// UIComponent_SecondTag.xaml 的交互逻辑
    /// </summary>
    public partial class UIComponent_SecondTag:UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
    public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChangedEventHandler handler = PropertyChanged;
        if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
    }
    public UIComponent_SecondTag(double TagTime)
    {
            this.TagTime = StaticFunction.ToDateTime((long)TagTime);
            TimeStamp = TagTime;
            InitializeComponent();
            Canvas.SetLeft(this,left);
    }
        private double TimeStamp;
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
    public string TimeString
    {
        get
        {
            return TagTime.Second.ToString();
        }
    }
    public double left
    {
        get
        {
                return ((TimeStamp - StaticData.FocusDatetime) * StaticData.TimeScalePerPixie) + StaticData.DisplayPixies - 30;
            }
    }
}
}
