using System;
using System.Windows;
using System.Windows.Input;

namespace DevelopmentTools
{
    public class ExitCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            foreach (var tool in (parameter as MainViewModel).tools)
            {
                tool.instance.ToolWindow.Close();
            }
        }
    }
}
