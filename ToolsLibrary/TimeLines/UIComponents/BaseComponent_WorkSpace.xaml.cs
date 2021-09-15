using LifeLines.BaseModel;
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
    /// BaseComponent_WorkSpace.xaml 的交互逻辑
    /// </summary>
    public partial class BaseComponent_WorkSpace : UserControl,INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        public BaseComponent_WorkSpace()
        {
            InitializeComponent();
            StaticData.PropertyChanged += StaticData_PropertyChanged;
        }

        private void StaticData_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

        }

        public double CurrentLineX
        {
            get
            {
                return ((StaticData.DatetimeNow - StaticData.FocusDatetime) / StaticData.TimeScalePerPixie) + StaticData.DisplayPixies;
            }
        }

        private void WorkSpace_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            CurrentLine.Y2 = this.ActualHeight;
            StaticData.DisplayPixies = this.ActualWidth/ 2;

        }

        private void userControl_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                StaticData.FocusDatetime = StaticData.FocusDatetime -(StaticData.TimeScalePerPixie * 100);
            }
            else
            {
                StaticData.FocusDatetime = StaticData.FocusDatetime+(StaticData.TimeScalePerPixie * 100);
            }
            HourBar.InvalidateVisual();
            OnPropertyChanged("CurrentLineX");
            //if (e.Delta > 0)
            //{
            //    if (StaticData.TimeScalePerPixie <= 1)
            //    {
            //        StaticData.TimeScalePerPixie /= 2;
            //    }
            //    else
            //    {
            //        StaticData.TimeScalePerPixie--;
            //    }
            //}
            //else
            //{
            //    if (StaticData.TimeScalePerPixie < 1)
            //    {
            //        StaticData.TimeScalePerPixie *= 2;
            //    }
            //    else { StaticData.TimeScalePerPixie++; }
            //}
        }
    }
}
