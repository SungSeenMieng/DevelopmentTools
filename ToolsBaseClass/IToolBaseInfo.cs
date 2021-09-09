using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace DevelopmentTools.Base
{
    public interface IToolBaseInfo
    {
        string ToolName { get; }

        string ToolDesc { get; }
        Window ToolWindow { get; }
        ImageSource ToolIcon { get; }
        string ToolAuthor { get; }
        Color ToolThemeColor { get; }
    }
}
