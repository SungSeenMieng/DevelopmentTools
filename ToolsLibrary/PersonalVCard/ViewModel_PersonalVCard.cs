using DevelopmentTools.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DevelopmentTools.Tools.PersonalVCard
{
    public class ViewModel_PersonalVCard : IToolBaseInfo
    {
        public string ToolName => "电子名片工具";

        public string ToolDesc => "管理电子名片文件、二维码";

        public Window ToolWindow
        {
            get
            {
                if (window == null || !window.IsLoaded)
                {
                    window = new Window_PersonalVCard();
                    window.DataContext = this;
                }
                return window;
            }
        }

        public ImageSource ToolIcon => new BitmapImage(new Uri("/ToolsLibrary;component/Resources/PersonalVCard.png", UriKind.Relative));

        public string ToolAuthor => "ssm";

        public Color ToolThemeColor => Color.FromRgb(0, 128, 128);

        Window_PersonalVCard window;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        public BindingList<PersonalInfo> personals { get; set; } = new BindingList<PersonalInfo>();
    }
    public class PersonalInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        private string _Name;
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                OnPropertyChanged("Name");
            }
        }
        public BindingList<InfoItem> Info { get; set; } = new BindingList<InfoItem>();
    }
    public class InfoItem:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        private string _ItemType;
        private string _ItemKey;
        private string _ItemValue;
        public string ItemType
        {
            get
            {
                return _ItemType;
            }
            set
            {
                _ItemType = value;
                OnPropertyChanged("ItemType");
            }
        }
        public string ItemKey
        {
            get
            {
                return _ItemKey;
            }
            set
            {
                _ItemKey = value;
                OnPropertyChanged("ItemKey");
            }
        }
        public string ItemValue
        {
            get
            {
                return _ItemValue;
            }
            set
            {
                _ItemValue = value;
                OnPropertyChanged("ItemValue");
            }
        }
    }
}
