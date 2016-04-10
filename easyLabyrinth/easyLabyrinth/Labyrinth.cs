using System;

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
            for (int i = 0; i < Global.maxX; i++)
            {
                for (int j = 0; j < Global.maxY; j++)
                {
                    if (i != 0)
                    {
                        if (cells[i - 1, j].right)
                            cells[i, j].left = true;
                    }
                    else cells[i, j].left = true;

                    if (i != (Global.maxX - 1))
                    {
                        if (cells[i + 1, j].left)
                            cells[i, j].right = true;
                    }
                    else cells[i, j].right = true;

                    if (j != 0)
                    {
                        if (cells[i, j - 1].bottom)
                            cells[i, j].top = true;

                    }
                    else cells[i, j].top = true;

                    if (j != (Global.maxY - 1))
                    {
                        if (cells[i, j + 1].top)
                            cells[i, j].bottom = true;
                    }
                    else cells[i, j].bottom = true;

                    Console.WriteLine("({0},{1}) - top: {2}, bottom: {3}, left: {4}, right: {5}", cells[i, j].X, cells[i, j].Y, cells[i, j].top, cells[i, j].bottom, cells[i, j].left, cells[i, j].right);
                }
            }
        }




    }
}
