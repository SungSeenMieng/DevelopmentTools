using LifeLines.BaseModel;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LifeLines.UIComponents
{
    /// <summary>
    /// Label_Current.xaml 的交互逻辑
    /// </summary>
    public partial class Label_Current : UserControl
    {
        public Label_Current()
        {
            InitializeComponent();
            double left = ((StaticData.DatetimeNow - StaticData.FocusDatetime) / StaticData.TimeScalePerPixie) + StaticData.DisplayPixies/2-40;
            left = left < 0 ? 0 : left;
            left = left > StaticData.DisplayPixies - 80 ? StaticData.DisplayPixies - 80 : left;
            Canvas.SetLeft(this,left);
        }
    }
}
