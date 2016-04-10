using System;
using System.Windows.Media;

namespace easyLabyrinth
{
    static class Global
    {
        static public int maxX { get; set; } = 80;
        static public int maxY { get; set; } = 60;

        static public double windowSizeModifier { get; set; } = 1900;

        static public Brush backgroundColor { get; set; } = Brushes.LightCyan;
        static public Brush lineColor { get; set; } = Brushes.Black;
        static public Brush playerColor { get; set; } = Brushes.Red;
    }
}
