using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace easyLabyrinth
{
    class Cell
    {
        public bool top { get; }
        public bool bottom { get; }
        public bool left { get; }
        public bool right { get; }

        public int X { get; set; }
        public int Y { get; set; }

        public Cell(int X, int Y, bool top, bool bottom, bool left, bool right)
        {
            this.X = X;
            this.Y = Y;
            this.top = top;
            this.bottom = bottom;
            this.left = left;
            this.right = right;
        }

        public Cell(int X, int Y)
        {
            this.X = X;
            this.Y = Y;

            Random rand = new Random(Guid.NewGuid().GetHashCode());
            this.top = rand.Next(2) == 0;
            this.bottom = rand.Next(2) == 0;
            this.left = rand.Next(2) == 0;
            this.right = rand.Next(2) == 0;
        }
    }
}
