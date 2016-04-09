using System;
using System.Windows.Media;

namespace easyLabyrinth
{
    static class Global
    {
        static public int maxX { get; set; } = 15;
        static public int maxY { get; set; } = 10;

        static public double windowSizeModifier { get; set; } = 1900;

        static public Brush backgroundColor { get; set; } = Brushes.LightCyan;
        static public Brush lineColor { get; set; } = Brushes.Black;
    }
}
