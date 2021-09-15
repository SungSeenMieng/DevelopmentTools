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
    /// WPFControl_ImageButton.xaml 的交互逻辑
    /// </summary>
    public partial class UIComponent_TextButton : UserControl,INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        public UIComponent_TextButton()
        {
            InitializeComponent();
        }
        #region 文字
        private string _TextContent;
        public string TextContent
        {
            get
            {
                return _TextContent;
            }
            set
            {
                _TextContent = value;
                OnPropertyChanged("TextContent");
            }
        }
        private Brush _TextBrush;
        public Brush TextBrush
        {
            get
            {
                return _TextBrush;
            }
            set
            {
                _TextBrush = value;
                OnPropertyChanged("TextBrush");
            }
        }
        
        #endregion
        #region 尺寸
        private double _ImageWidth=32;
        private double _ImageHeight=32;
        private double _ButtonHeight=60;
        private double _ButtonWidth=60;
        public double ImageWidth
        {
            get
            {
                return _ImageWidth;
            }
            set
            {
                _ImageWidth = value;
                OnPropertyChanged("ImageWidth");
            }
        }
        public double ImageHeight
        {
            get
            {
                return _ImageHeight;
            }
            set
            {
                _ImageHeight = value;
                OnPropertyChanged("ImageHeight");
            }
        }
        public double ButtonWidth
        {
            get
            {
                return _ButtonWidth;
            }
            set
            {
                _ButtonWidth = value;
                OnPropertyChanged("ButtonWidth");
            }
        }
        public double ButtonHeight
        {
            get
            {
                return _ButtonHeight;
            }
            set
            {
                _ButtonHeight= value;
                OnPropertyChanged("ButtonHeight");
            }
        }
#endregion
        #region 背景
        bool _RegularBackgroundSet = false;
        bool _MouseOverBackgroundSet = false;
        bool _MouseDownBackgroundSet = false;
        private Brush _Background;
        public Brush CurrentBackground
        {
            get
            {
                return _Background;
            }
            private set
            {
                _Background = value;
                OnPropertyChanged("CurrentBackground");
            }
        }

        private Brush _RegularBackground;
        private Brush _MouseOverBackground;
        private Brush _MouseDownBackground;
        public Brush RegularBackground
        {
            get
            {
                return _RegularBackground;
            }
            set
            {
                _RegularBackground = value;
                CurrentBackground = value;
                _RegularBackgroundSet = true;
            }
        }
        public Brush MouseOverBackground
        {
            get
            {
                return _MouseOverBackground;
            }
            set
            {
                _MouseOverBackground = value;
                _MouseOverBackgroundSet = true;
            }
        }
        public Brush MouseDownBackground {
            get
            {
                return _MouseDownBackground;
            }
            set
            {
                _MouseDownBackground = value;
                _MouseDownBackgroundSet = true;
            }
        }
        #endregion
        #region 边框
        private double _BorderRadius = 0;
        public double BorderRadius
        {
            get
            {
                return _BorderRadius;
            }
            set
            {
                _BorderRadius = value;
                OnPropertyChanged("BorderRadius");
            }
        }
        private Brush _BorderBrush = new SolidColorBrush(Color.FromArgb(40, 0, 0, 0));
        public Brush BorderBrush
        {
            get
            {
                    return _BorderBrush;
            }
            set
            {
                _BorderBrush = value;
                OnPropertyChanged("BorderBrush");
            }
        }
        private Thickness _BorderThickness = new Thickness(1);
        public Thickness ButtonBorderThickness
        {
            get
            {
                    return _BorderThickness;
            }
            set
            {
                _BorderThickness = value;
                OnPropertyChanged("BorderThickness");
            }
        }
        #endregion
        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            if (_MouseOverBackgroundSet)
            {
                CurrentBackground = MouseOverBackground;
            }
        }
        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {

            if (_MouseOverBackgroundSet)
            {
                if (_RegularBackgroundSet)
                {
                    if (e.LeftButton == MouseButtonState.Pressed)
                    {
                        CurrentBackground = MouseDownBackground;
                    }
                    else
                    {
                        CurrentBackground = RegularBackground;
                    }
                }
                else
                {
                    CurrentBackground = new SolidColorBrush();
                }
            }
        }
       
        private void Border_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.CaptureMouse();
            if (_MouseDownBackgroundSet)
            {
                CurrentBackground = MouseDownBackground;
            }
 
        }

        private void Border_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released)
            {
                this.ReleaseMouseCapture();
                if (_MouseDownBackgroundSet)
                {
                    if (_RegularBackgroundSet)
                    {
                        CurrentBackground = MouseOverBackground;
                    }
                    else
                    {
                        CurrentBackground = new SolidColorBrush();
                    }
                }
                TextButtonClick?.Invoke(this,e);

            }
        }

        public event MouseButtonEventHandler TextButtonClick;
    }
}
