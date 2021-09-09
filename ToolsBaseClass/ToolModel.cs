using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DevelopmentTools.Base
{
    public class ToolModel
    {
        public Type ToolType { get; set; }
        public IToolBaseInfo instance { get; set; }
        public string ToolName { get; set; }
        public string ToolDesc { get; set; }
        public ImageSource ToolIcon { get; set; }
        public string ToolAuthor { get; set; }
        public SolidColorBrush BackgroundBrush { get; set; }
        public SolidColorBrush ForegroundBrush { get; set; }
    }
}
