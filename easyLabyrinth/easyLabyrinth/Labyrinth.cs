using System;
using System.Collections.Generic;

namespace easyLabyrinth
{
    class Labyrinth
    {
        public Cell[,] cells { get; set; } = new Cell[Global.maxX, Global.maxY];
        public Cell startCell { get; set; }
        public Cell finishCell { get; set; }

        List<string> directions = new List<string> { "up", "down", "left", "right" }; // should be enum

        public Labyrinth(string param)
        {
            if (param == "random")
                fullRandom();
            if (param == "random2")
                smartRandom();
        }

        private void zeroVisits()
        {
            for (int i = 0; i < Global.maxX; i++)
            {
                for (int j = 0; j < Global.maxY; j++)
                {
                    cells[i, j].visited = false;
                }
            }
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

        private double cellsLinked(int x1, int y1, int x2, int y2)
        {
            double result = -1;
            try
            {
                zeroVisits();
                cells[x1, y1].visited = true;
                for (int times = 0; times < Global.maxX * Global.maxY; times++)
                {
                    for (int i = 0; i < Global.maxX; i++)
                    {
                        for (int j = 0; j < Global.maxY; j++)
                        {
                            if (cells[i, j].visited)
                            {
                                for (int num = 0; num < 4; num++)
                                {
                                    if (stepAvailable(cells[i, j].X, cells[i, j].Y, directions[num]))
                                    {
                                        int X = cells[i, j].X;
                                        int Y = cells[i, j].Y;
                                        if (num == 0)
                                            cells[i, j - 1].visited = true;
                                        if (num == 1)
                                            cells[i, j + 1].visited = true;
                                        if (num == 2)
                                            cells[i + 1, j].visited = true;
                                        if (num == 3)
                                            cells[i - 1, j].visited = true;
                                    }
                                }
                            }
                        }
                    }
                }
                if (cells[x2, y2].visited)
                    result = Math.Sqrt(Math.Pow((cells[x1, y1].X - cells[x2, y2].X), 2) + Math.Pow((cells[x1, y1].Y - cells[x2, y2].Y), 2));
                zeroVisits();
        }
            catch (System.IndexOutOfRangeException) {; }
            return result;
        }

        private void smartRandom()
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

            double distance = 0;
            for (int i = 0; i < Global.maxX; i++)
            {
                for (int j = 0; j < Global.maxY; j++)
                {
                    double buff = cellsLinked(cells[i, j].X, cells[i, j].Y, finishCell.X, finishCell.Y);
                    if (buff > distance)
                    {
                        distance = buff;
                        startCell = cells[i, j];
                    }
                }
            }

            if (Math.Sqrt(Math.Pow((startCell.X - finishCell.X), 2) + Math.Pow((startCell.Y - finishCell.Y), 2)) < (Global.maxX + Global.maxY) / (0.023 * (Global.maxX + Global.maxY) + 1.8))
                smartRandom();
        }
    }
}
