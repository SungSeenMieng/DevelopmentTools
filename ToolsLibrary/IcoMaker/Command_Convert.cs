using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DevelopmentTools.Tools.IcoMaker
{
    public class Command_Convert : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            ViewModel_IcoMaker obj = (ViewModel_IcoMaker)parameter;
            if (obj != null)
            {
                if (File.Exists(obj.source))
                {
                    FileInfo info = new FileInfo(obj.source);
                    if (info.Extension == "png")
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void Execute(object parameter)
        {
            ViewModel_IcoMaker obj = (ViewModel_IcoMaker)parameter;
            if (obj != null)
            {
                try
                {
                    obj.Convert();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message,"转换失败");
                }
              
            }
            }
    }
}
