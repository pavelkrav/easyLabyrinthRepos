using System;

namespace easyLabyrinth
{
    class Cell
    {
        public bool top { get; set; }
        public bool bottom { get; set; }
        public bool left { get; set; }
        public bool right { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        public bool visited { get; set; }

        public Cell(int X, int Y, bool top, bool bottom, bool left, bool right)
        {
            this.X = X;
            this.Y = Y;
            this.top = top;
            this.bottom = bottom;
            this.left = left;
            this.right = right;
            visited = false;
        }

        public Cell(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
            visited = false;

            Random rand = new Random(Guid.NewGuid().GetHashCode());
            this.top = rand.Next(2) == 0;
            this.bottom = rand.Next(2) == 0;
            this.left = rand.Next(2) == 0;
            this.right = rand.Next(2) == 0;
        }
    }
}
