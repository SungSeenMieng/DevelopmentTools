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
        public Label_Current(double dateTime)
        {
            InitializeComponent();
            double left = ((dateTime - StaticData.FocusDatetime) / StaticData.TimeScalePerPixie) + StaticData.DisplayPixies/2-40;
            Canvas.SetLeft(this,left);
        }
    }
}
