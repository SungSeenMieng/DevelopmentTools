using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace DevelopmentTools.Tools.LocalPortsReviewer
{
    /// <summary>
    /// Window_LocalPortsReviewer.xaml 的交互逻辑
    /// </summary>
    public partial class Window_LocalPortsReviewer : Window
    {
        public Window_LocalPortsReviewer()
        {
            InitializeComponent();
        }
        ViewModel_LocalPortsReviewer data
        {
            get
            {
                return this.DataContext as ViewModel_LocalPortsReviewer;
            }
        }
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            switch ((sender as Border).Uid)
            {
                case "get":
                    data.GetInfo();
                    break;
                case "process":
                    data.TurnToProcess();
                    break;
                case "path":
                    data.TurnToPath();
                    break;
                case "kill":
                    data.KillProcess();
                    break;
                default:
                    break;
            }

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is null||(sender as DataGrid)?.SelectedItem==null)
            {
                return;
            }
            try
            {
                int Pid =int.Parse( ((sender as DataGrid).SelectedItem as PortInfo).Pid);
                data.GetProcessInfo(Pid);
            }
            catch 
            { 
                
            }
        }
    }
}
