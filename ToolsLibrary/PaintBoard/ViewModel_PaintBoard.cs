using DevelopmentTools.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DevelopmentTools.Tools.PaintBoard
{
    class ViewModel_PaintBoard : IToolBaseInfo
    {
        public string ToolName =>"画板";

        public string ToolDesc => "随便画画";

        public Window ToolWindow
        {
            get
            {
                if (window == null || !window.IsLoaded)
                {
                    window = new Window_PaintBoard();
                    window.DataContext = this;
                }
                return window;
            }
        }

        public ImageSource ToolIcon => new BitmapImage(new Uri("/ToolsLibrary;component/Resources/drawing-board.png", UriKind.Relative));

        public string ToolAuthor => "ssm";

        public Color ToolThemeColor => Color.FromRgb(231,80,4);

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }


        Window_PaintBoard window;
        public BindingList<Polyline> lines { get; set; } = new BindingList<Polyline>();
        Polyline line = new Polyline() { Stroke =new SolidColorBrush(Color.FromRgb(255,255,255)),StrokeThickness=1};
        public Point StartPaint
        {
            set
            {
                lines.Add(line);
                undos.Clear();
                redos.Push(line);
                line.Points.Add(value);
            }
        }
        public Point Draw
        {
            set
            {
                line.Points.Add(value);
            }
        }
        public Point EndPaint
        {
            set
            {
                line = new Polyline() { Stroke = new SolidColorBrush(Color.FromRgb(255, 255, 255)), StrokeThickness = 1 };
            }
        }
        Stack<Polyline> redos = new Stack<Polyline>(5);
        Stack<Polyline> undos = new Stack<Polyline>(5);
        public void Redo()
        {
            if (redos.Count == 0) return;
            Polyline line = redos.Pop();
            lines.Remove(line);
            undos.Push(line);
        }
        public void Undo()
        {
            if (undos.Count == 0) return;
            Polyline line = undos.Pop();
            lines.Add(line);
            redos.Push(line);
        }
    }
}
