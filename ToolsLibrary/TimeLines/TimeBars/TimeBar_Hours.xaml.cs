using LifeLines.BaseModel;
using LifeLines.UIComponents;
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

namespace LifeLines.TimeBars
{
    /// <summary>
    /// TimeBar_Hours.xaml 的交互逻辑
    /// </summary>
    public partial class TimeBar_Hours : UserControl
    {
        public TimeBar_Hours()
        {
            InitializeComponent();
        }
        protected override void OnRender(DrawingContext drawingContext)
        {
            int seconds = (int)(StaticData.TimeScalePerPixie * (StaticData.DisplayPixies * 2));
            Application.Current.Dispatcher.BeginInvoke(new Action(() => {
                MainContent.Children.Clear();
            }));
            for (int i = 1; i<StaticData.DisplayPixies; i++)
            {
                double dateTime = StaticData.FocusDatetime-(i* StaticData.TimeScalePerPixie);
                if (seconds < 30)
                {
                    if (dateTime%3600 == 0)
                    {
                        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            UIComponent_HourTag tag = new UIComponent_HourTag(dateTime);
                            MainContent.Children.Add(tag);
                        }));
                    }
                    else
                    {
                        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            UIComponent_SecondTag tag = new UIComponent_SecondTag(dateTime);
                            MainContent.Children.Add(tag);
                        }));
                    }
                }
                else if (seconds < 300)
                {

                    if (dateTime%60 == 0)
                    {
                        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            UIComponent_HourTag tag = new UIComponent_HourTag(dateTime);
                            MainContent.Children.Add(tag);
                        }));
                    }
                    else if (dateTime%10 == 0)
                    {
                        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            UIComponent_SecondTag tag = new UIComponent_SecondTag(dateTime);
                            MainContent.Children.Add(tag);
                        }));
                    }
                }
                else if (seconds < 1800)
                {
                    if (dateTime % 300 == 0 )
                    {
                        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            UIComponent_HourTag tag = new UIComponent_HourTag(dateTime);
                            MainContent.Children.Add(tag);
                        }));
                    }
                }
                else if (seconds < 3600)
                {
                    if (dateTime % 600 == 0 )
                    {
                        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            UIComponent_HourTag tag = new UIComponent_HourTag(dateTime);
                            MainContent.Children.Add(tag);
                        }));
                    }
                }
                else if (seconds < 7200)
                {
                    if (dateTime % 1800 == 0)
                    {
                        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            UIComponent_HourTag tag = new UIComponent_HourTag(dateTime);
                            MainContent.Children.Add(tag);
                        }));
                    }
                }
                else if (seconds > 172800)
                {
                    if (dateTime%3600== 0 )
                    {
                        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            UIComponent_HourTag tag = new UIComponent_HourTag(dateTime);
                            MainContent.Children.Add(tag);
                        }));
                    }
                }
                else if (seconds > 86400)
                {
                    if (dateTime% 21600 == 0)
                    {
                        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            UIComponent_HourTag tag = new UIComponent_HourTag(dateTime);
                            MainContent.Children.Add(tag);
                        }));
                    }
                }
                else if (seconds > 43200)
                {
                    if (dateTime % 10800 == 0)
                    {
                        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            UIComponent_HourTag tag = new UIComponent_HourTag(dateTime);
                            MainContent.Children.Add(tag);
                        }));
                    }
                }
                else
                {
                    if (dateTime%60==0)
                    {
                        Application.Current.Dispatcher.BeginInvoke(new Action(() => {
                            UIComponent_HourTag tag = new UIComponent_HourTag(dateTime);
                            MainContent.Children.Add(tag);
                        }));
                    }
                }

            }
            for (int i = 1; i < StaticData.DisplayPixies; i++)
            {
                double dateTime = StaticData.FocusDatetime + (i * StaticData.TimeScalePerPixie);
                if (seconds < 30)
                {
                    if (dateTime % 3600 == 0)
                    {
                        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            UIComponent_HourTag tag = new UIComponent_HourTag(dateTime);
                            MainContent.Children.Add(tag);
                        }));
                    }
                    else
                    {
                        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            UIComponent_SecondTag tag = new UIComponent_SecondTag(dateTime);
                            MainContent.Children.Add(tag);
                        }));
                    }
                }
                else if (seconds < 300)
                {

                    if (dateTime % 60 == 0)
                    {
                        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            UIComponent_HourTag tag = new UIComponent_HourTag(dateTime);
                            MainContent.Children.Add(tag);
                        }));
                    }
                    else if (dateTime % 10 == 0)
                    {
                        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            UIComponent_SecondTag tag = new UIComponent_SecondTag(dateTime);
                            MainContent.Children.Add(tag);
                        }));
                    }
                }
                else if (seconds < 1800)
                {
                    if (dateTime % 300 == 0)
                    {
                        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            UIComponent_HourTag tag = new UIComponent_HourTag(dateTime);
                            MainContent.Children.Add(tag);
                        }));
                    }
                }
                else if (seconds < 3600)
                {
                    if (dateTime % 600 == 0)
                    {
                        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            UIComponent_HourTag tag = new UIComponent_HourTag(dateTime);
                            MainContent.Children.Add(tag);
                        }));
                    }
                }
                else if (seconds < 7200)
                {
                    if (dateTime % 1800 == 0)
                    {
                        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            UIComponent_HourTag tag = new UIComponent_HourTag(dateTime);
                            MainContent.Children.Add(tag);
                        }));
                    }
                }
                else if (seconds > 172800)
                {
                    if (dateTime % 3600 == 0)
                    {
                        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            UIComponent_HourTag tag = new UIComponent_HourTag(dateTime);
                            MainContent.Children.Add(tag);
                        }));
                    }
                }
                else if (seconds > 86400)
                {
                    if (dateTime % 21600 == 0)
                    {
                        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            UIComponent_HourTag tag = new UIComponent_HourTag(dateTime);
                            MainContent.Children.Add(tag);
                        }));
                    }
                }
                else if (seconds > 43200)
                {
                    if (dateTime % 10800 == 0)
                    {
                        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            UIComponent_HourTag tag = new UIComponent_HourTag(dateTime);
                            MainContent.Children.Add(tag);
                        }));
                    }
                }
                else
                {
                    if (dateTime % 60 == 0)
                    {
                        Application.Current.Dispatcher.BeginInvoke(new Action(() => {
                            UIComponent_HourTag tag = new UIComponent_HourTag(dateTime);
                            MainContent.Children.Add(tag);
                        }));
                    }
                }

            }
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                Label_Current current = new Label_Current();
                current.Uid = StaticData.Now.ToString("HH:mm");
                MainContent.Children.Add(current);
            }));
            base.OnRender(drawingContext);
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            StaticData.DisplayPixies = this.ActualWidth/2;
        }
    }
}
