using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyLabyrinth
{
    class Labyrinth
    {
        public Cell[,] cells { get; set; } = new Cell[Global.maxX, Global.maxY];

        public Labyrinth()
        {
            for (int i = 0; i < Global.maxX; i++)
            {
                for (int j = 0; j < Global.maxY; j++)
                {
                    cells[i, j] = new Cell(i, j);
                }
            }
        }
    }
}
