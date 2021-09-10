using System.Windows;

namespace DevelopmentTools
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            App.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
           
            MainViewModel main = new MainViewModel();
        }

        private void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
           
        }
    }
}
