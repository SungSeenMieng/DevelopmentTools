using LifeLines.BaseModel;
using LifeLines.BaseRoutine;
using LifeLines.UIComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DevelopmentTools.Tools.TimeLines
{
    /// <summary>
    /// Window_TimeLines.xaml 的交互逻辑
    /// </summary>
    public partial class Window_TimeLines : Window
    {
        bool isFocusToday = true;
        DispatcherTimer timer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };
        ViewModel_TimeLines data
        {
            get
            {
                return this.DataContext as ViewModel_TimeLines;
            }
        }
        public Window_TimeLines()
        {
            InitializeComponent();
            StaticData.Now = DateTime.Now;
            StaticData.FocusDatetime = StaticData.DatetimeNow;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            StaticData.Now = DateTime.Now;
            if (isFocusToday)
            {
                StaticData.FocusDatetime = StaticData.DatetimeNow;
            }
            DrawRule();
        }
        SolidColorBrush pen = new SolidColorBrush(Color.FromRgb(255,255,255));
        private void DrawRule()
        {
          
            if (TimeBar.Children != null)
            {
                TimeBar.Children.Clear();
            }
            if (InfoBar.Children != null)
            {
                InfoBar.Children.Clear();
            }
            Line _line;
            TextBlock _textBlock;
            TimeBarDisplayMode mode = GetMode();
            string str = "HH:mm:ss";
            int scale = 80;
            double step = 1;
            switch (mode)
            {
                case TimeBarDisplayMode.Second:
                case TimeBarDisplayMode.FiveSeconds:
                case TimeBarDisplayMode.TenSeconds:
                case TimeBarDisplayMode.ThirtySeconds:
                    scale = 80;
                    str = "HH:mm:ss";
                    break;
                case TimeBarDisplayMode.Minute:
                case TimeBarDisplayMode.FiveMinutes:
                case TimeBarDisplayMode.TenMinutes:
                case TimeBarDisplayMode.ThirtyMinutes:
                    scale = 100;
                    str = "HH:mm";
                    break;
                case TimeBarDisplayMode.Hour:
                case TimeBarDisplayMode.SixHours:
                case TimeBarDisplayMode.TwelveHours:
                    scale = 120;
                    str = "HH:mm";
                    break;
                case TimeBarDisplayMode.Day:
                    scale = 140;
                    str = "yyyy-MM-dd";
                    break;
                case TimeBarDisplayMode.Week:
                    scale = 160;
                    str = "yyyy-MM-dd";
                    break;
                case TimeBarDisplayMode.Month:
                case TimeBarDisplayMode.Season:

                case TimeBarDisplayMode.TwoSeasons:
                    scale = 180;
                    str = "yyyy-MM";
                    break;
                case TimeBarDisplayMode.Year:
                    scale = 200;
                    str = "yyyy";
                    break;
            }
            switch (mode)
            {
                case TimeBarDisplayMode.Second:
                    step = 1;
                    break;
                case TimeBarDisplayMode.FiveSeconds:
                    step = 5;
                    break;
                case TimeBarDisplayMode.TenSeconds:
                    step = 10;
                    break;
                case TimeBarDisplayMode.ThirtySeconds:
                    step = 30;
                    break;
                case TimeBarDisplayMode.Minute:
                    step = 60;
                    break;
                case TimeBarDisplayMode.FiveMinutes:
                    step = 300;
                    break;
                case TimeBarDisplayMode.TenMinutes:
                    step = 600;
                    break;
                case TimeBarDisplayMode.ThirtyMinutes:
                    step = 1800;
                    break;
                case TimeBarDisplayMode.Hour:
                    step = 3600;
                    break;
                case TimeBarDisplayMode.SixHours:
                    step = 21600;
                    break;
                case TimeBarDisplayMode.TwelveHours:
                    step = 43200;
                    break;
                case TimeBarDisplayMode.Day:
                    step = 86400;
                    break;
                case TimeBarDisplayMode.Week:
                    step = 86400;
                    break;
                case TimeBarDisplayMode.Month:
                    step = 86400;
                    break;
                case TimeBarDisplayMode.Season:
                    step = 86400;
                    break;

                case TimeBarDisplayMode.TwoSeasons:
                    step = 86400;
                    break;
                case TimeBarDisplayMode.Year:
                    step = 86400;
                    break;
            }
            double unit = scale * StaticData.TimeScalePerPixie;
            double startPos = 0;

            for (double i = Math.Floor(StaticData.StartDatetime); i <Math.Ceiling(StaticData.EndDatetime) ; i++)
            {
                if (i % step == 0&&i!=0)
                {
                    startPos = i;
                    break;
                }
            }
            if (startPos == 0)
            {
                startPos = StaticData.DisplayPixies/2*StaticData.TimeScalePerPixie%step+StaticData.StartDatetime;
            }
            switch (mode)
            {
                case TimeBarDisplayMode.Week:
                    DateTime date = StaticFunction.ToDateTime(startPos);
                    startPos =StaticFunction.GetTimeStamp(date.AddDays(1-(int)date.DayOfWeek));
                    break;
                case TimeBarDisplayMode.Month:
                    date = StaticFunction.ToDateTime(startPos);
                    startPos = StaticFunction.GetTimeStamp(date.AddDays(1 - date.Day));
                    break;
                case TimeBarDisplayMode.Season:
                    date = StaticFunction.ToDateTime(startPos);
                    startPos = StaticFunction.GetTimeStamp(date.AddDays(1 - date.DayOfYear));
                    break;
                case TimeBarDisplayMode.TwoSeasons:
                    date = StaticFunction.ToDateTime(startPos);
                    startPos = StaticFunction.GetTimeStamp(date.AddDays(1 - date.DayOfYear));
                    break;
                case TimeBarDisplayMode.Year:
                    date = StaticFunction.ToDateTime(startPos);
                    startPos = StaticFunction.GetTimeStamp(date.AddDays(1 - date.DayOfYear));
                    break;
            }
            bool showDay = true;
            bool showWeek = true;
            bool showMonth = true;
            bool showSeason = true;
            bool showYear = true;
            switch (mode)
            {
                case TimeBarDisplayMode.Second:
                case TimeBarDisplayMode.FiveSeconds:
                case TimeBarDisplayMode.TenSeconds:
                case TimeBarDisplayMode.ThirtySeconds:
                case TimeBarDisplayMode.Minute:
                case TimeBarDisplayMode.FiveMinutes:
                case TimeBarDisplayMode.TenMinutes:
                case TimeBarDisplayMode.ThirtyMinutes:
                case TimeBarDisplayMode.Hour:
                case TimeBarDisplayMode.SixHours:
                case TimeBarDisplayMode.TwelveHours:
                    break;
                case TimeBarDisplayMode.Day:
                    showDay = false;
                    break;
                case TimeBarDisplayMode.Week:
                    showDay = false;
                    break;
                case TimeBarDisplayMode.Month:
                case TimeBarDisplayMode.Season:
                    showDay = false;
                    showMonth = false;
                    showWeek = false;
                    showYear = false;
                    break;
                case TimeBarDisplayMode.TwoSeasons:
                    showSeason = false;
                    showDay = false;
                    showMonth = false;
                    showWeek = false;
                    break;
                default:
                    showSeason = false;
                    showDay = false;
                    showWeek = false;
                    showMonth = false;
                    showYear = false;
                    break;
            }
            for (double i = startPos; i < Math.Ceiling(StaticData.EndDatetime); i += step)
            {
                double count = 1;
                if (step >= 86400)
                {
                    DateTime dateTime =StaticFunction.ToDateTime(i);
                    switch (mode)
                    {
                        case TimeBarDisplayMode.Week:
                            count = 7;
                            break;
                        case TimeBarDisplayMode.Month:
                            count = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
                            break;
                        case TimeBarDisplayMode.Season:
                            dateTime = dateTime.AddMonths((dateTime.Month - 1) % 3);
                            int days = 0;
                            for (int j = 0; j < 3; j++)
                            {
                                days += DateTime.DaysInMonth(dateTime.AddMonths(j).Year, dateTime.AddMonths(j).Month);
                            }
                            count = days;
                            break;
                        case TimeBarDisplayMode.TwoSeasons:
                            dateTime = dateTime.AddMonths((dateTime.Month - 1) % 6);
                            days = 0;
                            for (int j = 0; j <6; j++)
                            {
                                days += DateTime.DaysInMonth(dateTime.AddMonths(j).Year, dateTime.AddMonths(j).Month);
                            }
                            count = days;
                            break;
                        case TimeBarDisplayMode.Year:
                            if (DateTime.IsLeapYear(dateTime.Year))
                            {
                                count = 366;
                            }
                            else
                            {
                                count = 365;
                            }
                            break;
                    }
                    step =86400* count;
                }
                
                double x = (i - StaticData.StartDatetime) / StaticData.TimeScalePerPixie;
               
                _line = new Line()
                {
                    X1 = x,
                    X2 = x,
                    Y1 = 0,
                    Y2 = 5,
                    Stroke = pen,
                    StrokeThickness = 1,
                };
                DateTime LineDatetime = StaticFunction.ToDateTime(i);
                _textBlock = new TextBlock()
                {
                    Text = LineDatetime.ToString(str),
                    Foreground = pen,
                    FontSize = 13,
                   Width = 100,
                   Height=30,
                   TextAlignment = TextAlignment.Center,
                   ToolTip = LineDatetime.ToString("yyyy-MM-dd\r\nHH:mm:ss")
                };
                _textBlock.Width = 100;
                _textBlock.Height = 30;
                Canvas.SetLeft(_textBlock, x-50);
                Canvas.SetTop(_textBlock, 10);
                TimeBar.Children.Add(_line);
                TimeBar.Children.Add(_textBlock);

                if (showYear && LineDatetime.Day == 1 && LineDatetime.Month == 1 && LineDatetime.Hour == 0 && LineDatetime.Minute == 0 && LineDatetime.Second == 0)
                {
                    _line = new Line()
                    {
                        X1 = x,
                        X2 = x,
                        Y1 = 30,
                        Y2 = 35,
                        Stroke = pen,
                        StrokeThickness = 1,
                    };
                    _textBlock = new TextBlock()
                    {
                        Text = LineDatetime.ToString("yyyy年"),
                        Foreground = pen,
                        FontSize = 11,
                        Width = 100,
                        Height = 30,
                        TextAlignment = TextAlignment.Center,
                    };
                    _textBlock.Width = 100;
                    _textBlock.Height = 30;
                    Canvas.SetLeft(_textBlock, x - 50);
                    Canvas.SetTop(_textBlock, 15);
                    InfoBar.Children.Add(_line);
                    InfoBar.Children.Add(_textBlock);
                }
                else if (showSeason &&(LineDatetime.Month==1||LineDatetime.Month==4|| LineDatetime.Month == 7|| LineDatetime.Month == 10) &&LineDatetime.Day == 1 && LineDatetime.Hour == 0 && LineDatetime.Minute == 0 && LineDatetime.Second == 0)
                {
                    _line = new Line()
                    {
                        X1 = x,
                        X2 = x,
                        Y1 = 30,
                        Y2 = 35,
                        Stroke = pen,
                        StrokeThickness = 1,
                    };
                    _textBlock = new TextBlock()
                    {
                        Text =string.Format("{0}\r\n第{1}季度",LineDatetime.ToString("yyyy年MM月"),(LineDatetime.Month-1)/3+1),
                        Foreground = pen,
                        FontSize = 11,
                        Width = 100,
                        Height = 30,
                        TextAlignment = TextAlignment.Center
                    };
                    _textBlock.Width = 100;
                    _textBlock.Height = 30;
                    Canvas.SetLeft(_textBlock, x - 50);
                    Canvas.SetTop(_textBlock, 5);
                    InfoBar.Children.Add(_line);
                    InfoBar.Children.Add(_textBlock);
                }
                else if (showMonth && LineDatetime.Day == 1 && LineDatetime.Hour == 0 && LineDatetime.Minute == 0 && LineDatetime.Second == 0)
                {
                    _line = new Line()
                    {
                        X1 = x,
                        X2 = x,
                        Y1 = 30,
                        Y2 = 35,
                        Stroke = pen,
                        StrokeThickness = 1,
                    };
                    _textBlock = new TextBlock()
                    {
                        Text = LineDatetime.ToString("yyyy年MM月"),
                        Foreground = pen,
                        FontSize = 11,
                        Width = 100,
                        Height = 30,
                        TextAlignment = TextAlignment.Center
                    };
                    _textBlock.Width = 100;
                    _textBlock.Height = 30;
                    Canvas.SetLeft(_textBlock, x - 50);
                    Canvas.SetTop(_textBlock, 15);
                    InfoBar.Children.Add(_line);
                    InfoBar.Children.Add(_textBlock);
                }
                else if (showWeek && LineDatetime.DayOfWeek == DayOfWeek.Monday&&LineDatetime.Hour==0&&LineDatetime.Minute==0&&LineDatetime.Second==0)
                {
                    _line = new Line()
                    {
                        X1 = x,
                        X2 = x,
                        Y1 = 30,
                        Y2 = 35,
                        Stroke = pen,
                        StrokeThickness = 1,
                    };
                    _textBlock = new TextBlock()
                    {
                        Text = string.Format("{0}年{1}周\r\n{2}", LineDatetime.Year, WeekOfYear(LineDatetime, new CultureInfo("zh-CN")),LineDatetime.ToString("MM月dd日")),
                        Foreground = pen,
                        FontSize = 11,
                        Width = 100,
                        Height = 30,
                        TextAlignment = TextAlignment.Center,
                    };
                    _textBlock.Width = 100;
                    _textBlock.Height = 30;
                    Canvas.SetLeft(_textBlock, x - 50);
                    Canvas.SetTop(_textBlock, 5);
                    InfoBar.Children.Add(_line);
                    InfoBar.Children.Add(_textBlock);
                }
                else if (showDay && LineDatetime.Hour == 0&&LineDatetime.Minute==0&&LineDatetime.Second==0)
                {
                    _line = new Line()
                    {
                        X1 = x,
                        X2 = x,
                        Y1 = 30,
                        Y2 = 35,
                        Stroke = pen,
                        StrokeThickness = 1,
                    };
                    _textBlock = new TextBlock()
                    {
                        Text = LineDatetime.ToString("yyyy年MM月dd日"),
                        Foreground = pen,
                        FontSize = 11,
                        Width = 100,
                        Height = 30,
                        TextAlignment = TextAlignment.Center
                    };
                    _textBlock.Width = 100;
                    _textBlock.Height = 30;
                    Canvas.SetLeft(_textBlock, x - 50);
                    Canvas.SetTop(_textBlock, 15);
                    InfoBar.Children.Add(_line);
                    InfoBar.Children.Add(_textBlock);
                }
            }
                double left = ((StaticData.DatetimeNow - StaticData.FocusDatetime) / StaticData.TimeScalePerPixie) + StaticData.DisplayPixies / 2;
            BackRect.Width = left > StaticData.DisplayPixies ? StaticData.DisplayPixies : left < 0 ? 0 : left;

            left = left < 50 ? 50 : left > StaticData.DisplayPixies - 50 ? StaticData.DisplayPixies - 50 : left;
            Canvas.SetLeft(current, left - 50);
            LeftArrow.Visibility = Visibility.Collapsed;
            RightArrow.Visibility = Visibility.Collapsed;
            if (BackRect.Width==0)
            {
                LeftArrow.Visibility = Visibility.Visible;
            }
            if (BackRect.Width==StaticData.DisplayPixies)
            {
                RightArrow.Visibility = Visibility.Visible;
            }
            data.OnPropertyChanged("Now");
            
        }
        public static int WeekOfYear(DateTime dt, CultureInfo ci)
        {
            return ci.Calendar.GetWeekOfYear(dt, ci.DateTimeFormat.CalendarWeekRule, ci.DateTimeFormat.FirstDayOfWeek);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Tick += Timer_Tick;
            timer.Start();
        }


        private void Canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            StaticData.DisplayPixies = TimeBar.ActualWidth;
            DrawRule();
        }
        private TimeBarDisplayMode GetMode()
        {
            if (StaticData.TimeScalePerPixie > 201600) return TimeBarDisplayMode.Year;
            if (StaticData.TimeScalePerPixie < 0.02) return TimeBarDisplayMode.Second;
            if (StaticData.TimeScalePerPixie < 0.04) return TimeBarDisplayMode.FiveSeconds;
            if (StaticData.TimeScalePerPixie < 0.1) return TimeBarDisplayMode.TenSeconds;
            if (StaticData.TimeScalePerPixie < 0.3) return TimeBarDisplayMode.ThirtySeconds;
            if (StaticData.TimeScalePerPixie < 1) return TimeBarDisplayMode.Minute;
            if (StaticData.TimeScalePerPixie < 5) return TimeBarDisplayMode.FiveMinutes;
            if (StaticData.TimeScalePerPixie < 10) return TimeBarDisplayMode.TenMinutes;
            if (StaticData.TimeScalePerPixie < 30) return TimeBarDisplayMode.ThirtyMinutes;
            if (StaticData.TimeScalePerPixie < 60) return TimeBarDisplayMode.Hour;
            if (StaticData.TimeScalePerPixie < 240) return TimeBarDisplayMode.SixHours;
            if (StaticData.TimeScalePerPixie < 480) return TimeBarDisplayMode.TwelveHours;
            if (StaticData.TimeScalePerPixie < 960) return TimeBarDisplayMode.Day;
            if (StaticData.TimeScalePerPixie < 6720) return TimeBarDisplayMode.Week;
            if (StaticData.TimeScalePerPixie < 33600) return TimeBarDisplayMode.Month;
            if (StaticData.TimeScalePerPixie <100800) return TimeBarDisplayMode.Season;
            else return TimeBarDisplayMode.TwoSeasons;
        }
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            switch ((sender as Border).Uid)
            {
                case "today":
                    isFocusToday = true;
                    StaticData.FocusDatetime = StaticData.DatetimeNow;
                    break;
            }
            DrawRule();
        }

        private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                StaticData.FocusDatetime -= (StaticData.TimeScalePerPixie * 50);
            }
            else
            {
                StaticData.FocusDatetime += (StaticData.TimeScalePerPixie * 50);
            }
            isFocusToday = false;
            DrawRule();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.IsLoaded)
            {
                double val  = Math.Pow(0.6,e.NewValue)*500000;
                StaticData.TimeScalePerPixie = val < 0.01 ? 0.01 : val > 500000 ? 500000 : val;
                DrawRule();
            }
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
    }
    public enum TimeBarDisplayMode
    {
       Second = 0,//0.000002-0.00002
       FiveSeconds=1,//0.00002-0.0002
       TenSeconds=2,//0.0002-0.002
       ThirtySeconds=3,//0.002-0.02
       Minute = 4,//0.02-0.2
       FiveMinutes  =5,//0.2-5
       TenMinutes = 6,//5-10
       ThirtyMinutes = 7,//10-30
       Hour = 8,//30-60
       SixHours = 9,//60-240
       TwelveHours = 10,//240-480
       Day = 11,//480-960
       Week = 12,//960-6720
       Month = 13,//6720-33600
       Season = 14,//33600-100800
       TwoSeasons = 15,//100800-201600
       Year = 16//201600-
    }
}
