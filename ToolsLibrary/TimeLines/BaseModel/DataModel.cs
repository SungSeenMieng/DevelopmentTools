using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeLines.BaseModel
{
    public class TimeLineTask : TimeLineData
    {
        public string Description { get; set; }
        public double StartTime { get; set; }
        public double EndTime { get; set; }
        public double Deadline { get; set; }
        public List<TimeLineEvent> Events { get; set; } = new List<TimeLineEvent>();
        public List<TimeLineTask> SubTasks { get; set; } = new List<TimeLineTask>();
        public List<TimeLineTick> Ticks { get; set; } = new List<TimeLineTick>();
        public List<TimeLineNote> Notes { get; set; } = new List<TimeLineNote>();
    }
    public class TimeLineEvent : TimeLineData
    {
        public double TimeStamp { get; set; }
        public string Description { get; set; }
        public List<TimeLineNote> Notes { get; set; } = new List<TimeLineNote>();
    }
    public class TimeLineTick : TimeLineData
    {
        public bool IsFinished { get; set; } = false;
        public string Description { get; set; }
        public double Deadline { get; set; }
        public double FinishTime { get; set; }
        public List<TimeLineNote> Notes { get; set; } = new List<TimeLineNote>();
    }
    public class TimeLineNote:TimeLineData
    {
        public string Content { get; set; }
    }
    public class TimeLineData
    {
        public string Title { get; set; }
        public string Owner { get; set; }
        public double CreateTime { get; set; }
        public string ThemeColor { get; set; }
        public short Level { get; set; }
        public List<string> Members { get; set; } = new List<string>();
    }
}
