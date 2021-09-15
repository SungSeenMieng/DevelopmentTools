using LifeLines.BaseRoutine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LifeLines.BaseModel
{
    public static class StaticData
    {
       
        public static event PropertyChangedEventHandler PropertyChanged;

        public static void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(StaticData.FocusDatetime, new PropertyChangedEventArgs(propertyName));
        }
        private static DateTime _Now;
        public static DateTime Now
        {
            get
            {
                return _Now;
            }
            set
            {
                _Now = value;
                DatetimeNow = StaticFunction.GetTimeStamp(value);
            }
        }
        public static double DatetimeNow;
        public static double FocusDatetime;
        public static double StartDatetime
        {
            get
            {
                return FocusDatetime - (DisplayPixies/2 * TimeScalePerPixie);
            }
        }
        public static double EndDatetime
        {
            get
            {
                return FocusDatetime+(DisplayPixies/2 * TimeScalePerPixie);
            }
        }
        public static double TimeScalePerPixie = 45;
        public static double DisplayPixies = 1920;
    }
}
