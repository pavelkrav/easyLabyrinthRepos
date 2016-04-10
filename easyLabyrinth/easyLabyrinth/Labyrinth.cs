using System;
using System.Collections.Generic;

namespace easyLabyrinth
{
    class Labyrinth
    {
        public Cell[,] cells { get; set; } = new Cell[Global.maxX, Global.maxY];
        public Cell startCell { get; set; }
        public Cell finishCell { get; set; }

        public Labyrinth(string param)
        {
            if (param == "random")
                fullRandom();
            if (param == "random2")
                cleverRandom();
        }

        private void fullRandom()
        {
            for (int i = 0; i < Global.maxX; i++)
            {
                for (int j = 0; j < Global.maxY; j++)
                {
                    cells[i, j] = new Cell(i, j);

                    if (i != 0)
                        cells[i, j].left = cells[i - 1, j].right;
                    else cells[i, j].left = true;

                    if (i == (Global.maxX - 1))
                        cells[i, j].right = true;

                    if (j != 0)
                        cells[i, j].top = cells[i, j - 1].bottom;
                    else cells[i, j].top = true;

                    if (j == (Global.maxY - 1))
                        cells[i, j].bottom = true;

                    Console.WriteLine("({0},{1}) - top: {2}, bottom: {3}, left: {4}, right: {5}", cells[i, j].X, cells[i, j].Y, cells[i, j].top, cells[i, j].bottom, cells[i, j].left, cells[i, j].right);
                }
            }

            Random rand = new Random(Guid.NewGuid().GetHashCode());
            startCell = cells[rand.Next(Global.maxX), rand.Next(Global.maxY)];
            finishCell = startCell;
            while (finishCell == startCell)
                finishCell = cells[rand.Next(Global.maxX), rand.Next(Global.maxY)];
        }

        private bool stepAvailable(int x, int y, string direction)
        {
            bool result = false;
            try
            {
                if (direction == "up")
                    result = !cells[x, y].top;
                if (direction == "down")
                    result = !cells[x, y].bottom;
                if (direction == "left")
                    result = !cells[x, y].left;
                if (direction == "right")
                    result = !cells[x, y].right;
            }
            catch (System.IndexOutOfRangeException)
            {
                ;
            }
            return result;
        }

        private void cleverRandom()
        {
            for (int i = 0; i < Global.maxX; i++)
            {
                for (int j = 0; j < Global.maxY; j++)
                {
                    cells[i, j] = new Cell(i, j);

                    if (i != 0)
                        cells[i, j].left = cells[i - 1, j].right;
                    else cells[i, j].left = true;

                    if (i == (Global.maxX - 1))
                        cells[i, j].right = true;

                    if (j != 0)
                        cells[i, j].top = cells[i, j - 1].bottom;
                    else cells[i, j].top = true;

                    if (j == (Global.maxY - 1))
                        cells[i, j].bottom = true;

                    Console.WriteLine("({0},{1}) - top: {2}, bottom: {3}, left: {4}, right: {5}", cells[i, j].X, cells[i, j].Y, cells[i, j].top, cells[i, j].bottom, cells[i, j].left, cells[i, j].right);
                }
            }

            Random rand = new Random(Guid.NewGuid().GetHashCode());
            finishCell = cells[rand.Next(Global.maxX), rand.Next(Global.maxY)];
            startCell = finishCell;

            List<string> directions = new List<string> { "up", "down", "left", "right"};
            for (int i = 0; i < Global.maxX * Global.maxY; i++)
            {
                int num = rand.Next(4);
                if (stepAvailable(startCell.X, startCell.Y, directions[num]))
                {
                    if (num == 0)
                        startCell.Y--;
                    if (num == 1)
                        startCell.Y++;
                    if (num == 2)
                        startCell.X--;
                    if (num == 3)
                        startCell.X++;
                }
            }
        }
    }
}
