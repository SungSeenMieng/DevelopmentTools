using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace LifeLines.BaseRoutine
{
    public class StaticFunction
    {
        private static DateTime BaseDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        public static double GetTimeStamp(DateTime dateTime)
        {
            TimeSpan ts = dateTime-BaseDateTime;
           return Math.Floor(ts.TotalSeconds);
        }
        public const long TiksUtc1970 = 621355968000000000;
        /// <summary>
        /// 时间戳转换成时间,返回NULL则说明转换失败(如时间戳无效)
        /// </summary>
        /// <param name="timestamp">时间戳</param>
        /// <param name="millisecond">是否毫秒级,true毫秒级(默认值)</param>
        /// <param name="localTime">是否输出本地时间,true本地时间(默认值)</param>
        /// <returns></returns>
        public static DateTime ToDateTime(double timestamp, bool localTime = true)
        {
            try
            {
                var dt = new DateTime(TiksUtc1970 + (long)timestamp * 10000000, DateTimeKind.Utc);
                if (localTime)
                    dt.ToLocalTime();
                return dt;
            }
            catch
            {
                return DateTime.Now;
            }
        }
    }
}
