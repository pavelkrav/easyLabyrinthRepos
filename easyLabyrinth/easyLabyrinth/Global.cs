using System;
using System.Windows.Media;

namespace easyLabyrinth
{
    static class Global
    {
        static public int maxX { get; set; } = 40;
        static public int maxY { get; set; } = 30;

        static public double windowSizeModifier { get; set; } = 1900;

        static public Brush backgroundColor { get; set; } = Brushes.LightCyan;
        static public Brush lineColor { get; set; } = Brushes.Black;
        static public Brush playerColor { get; set; } = Brushes.Red;
    }
}
