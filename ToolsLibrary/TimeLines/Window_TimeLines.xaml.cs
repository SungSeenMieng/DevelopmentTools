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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DevelopmentTools.Tools.TimeLines
{
    /// <summary>
    /// Window_TimeLines.xaml 的交互逻辑
    /// </summary>
    public partial class Window_TimeLines : Window
    {
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
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            DrawRule();
        }

        private void DrawRule()
        {
            if (TimeBar.Children != null)
            {
                TimeBar.Children.Clear();
            }

            System.Windows.Shapes.Line _line;
            TextBlock _textBlock;

            const double _minPixel = 30;        //长黑标线最小像素间距
            string _unit = "mm";                //记录尺度单位
            double _interval;                   //长黑标线实际距离
            double _intervalPixel;              //长黑标线实际像素距离

            double _scientificF;                //用来判断实际有效数字及尺度
            int _scientificE;
            string[] _strTemp = (_minPixel / sdScale.Value).ToString("E").Split('E');
            double.TryParse(_strTemp[0], out _scientificF);
            int.TryParse(_strTemp[1], out _scientificE);
            if (_scientificE >= 2 || (_scientificE >= 1 && _scientificF >= 5))
            {
                _unit = "m";
                _scientificE -= 3;
            }

            //文字“0”
            _textBlock = new TextBlock();
            _textBlock.Text = _unit;
            _textBlock.FontSize = 8;
            _textBlock.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            _textBlock.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            _textBlock.Margin = new Thickness(10, 25, 0, 0);
            TimeBar.Children.Add(_textBlock);

            //将间隔标准化
            if (_scientificF >= 5)
            {
                _interval = 10 * Math.Pow(10, _scientificE);
            }
            else if (_scientificF >= 2.5)
            {
                _interval = 5 * Math.Pow(10, _scientificE);
            }
            else
            {
                _interval = 2.5 * Math.Pow(10, _scientificE);
            }

            if (_unit == "mm")
            {
                _intervalPixel = _interval * sdScale.Value;
            }
            else
            {
                _intervalPixel = _interval * 1000 * sdScale.Value;
            }
            int _lineIndex = 0;
            double _width = TimeBar.ActualWidth - 10;
            double _pixelDistence = _intervalPixel / 5;     //单个小间隔像素距离
            for (double i = 10; i < _width; i += _pixelDistence)
            {
                _line = new System.Windows.Shapes.Line();
                if (_lineIndex % 5 == 0)
                {
                    _line.Stroke = Brushes.Black;
                    _line.StrokeThickness = 1;
                    _line.X1 = i;
                    _line.Y1 = 0;
                    _line.X2 = i;
                    _line.Y2 = 20;

                    _textBlock = new TextBlock();
                    _textBlock.Text = (_interval * (_lineIndex / 5)).ToString();
                    _textBlock.FontSize = 8;
                    _textBlock.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    _textBlock.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    _textBlock.Margin = new Thickness(i - 5, 25, 0, 0);
                    TimeBar.Children.Add(_textBlock);
                }
                else
                {
                    _line.Stroke = Brushes.DimGray;
                    _line.StrokeThickness = 1;
                    _line.X1 = i;
                    _line.Y1 = 0;
                    _line.X2 = i;
                    _line.Y2 = 15;
                }
                TimeBar.Children.Add(_line);

                _lineIndex++;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Tick += Timer_Tick;
            timer.Start();
        }
    }
}
