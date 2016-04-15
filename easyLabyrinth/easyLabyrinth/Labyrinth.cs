using System;
using System.Collections.Generic;

namespace easyLabyrinth
{
    enum Generators { random, random2 };

    class Labyrinth
    {
        public Cell[,] cells { get; set; } = new Cell[Global.maxX, Global.maxY];
        public Cell startCell { get; set; }
        public Cell finishCell { get; set; }

        public Labyrinth(Generators param)
        {
            switch (param)
            {
                case Generators.random:
                    fullRandom();
                    break;
                case Generators.random2:
                    smartRandom();
                    break;
            }
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

        /// <summary>
        /// Checks if there is a link between cells and returns distance.
        /// </summary>
        /// <returns>-1 if no link there. Else distance between cells.</returns>
        private double cellsLinked(int x1, int y1, int x2, int y2)
        {
            double result = -1;
            zeroVisits();
            try
            {
                cells[x1, y1].visited = true;
            }
            catch (System.IndexOutOfRangeException) {; }
            for (int times = 0; times < Global.maxX * Global.maxY; times++)
            {
                for (int i = 0; i < Global.maxX; i++)
                {
                    for (int j = 0; j < Global.maxY; j++)
                    {
                        if (cells[i, j].visited)
                        {
                            if (!cells[i, j].top)
                                cells[i, j - 1].visited = true;
                            if (!cells[i, j].bottom)
                                cells[i, j + 1].visited = true;
                            if (!cells[i, j].right)
                                cells[i + 1, j].visited = true;
                            if (!cells[i, j].left)
                                cells[i - 1, j].visited = true;
                        }
                    }
                }
            }
            try
            {
                if (cells[x2, y2].visited)
                    result = Math.Sqrt(Math.Pow((cells[x1, y1].X - cells[x2, y2].X), 2) + Math.Pow((cells[x1, y1].Y - cells[x2, y2].Y), 2));
            }
            catch (System.IndexOutOfRangeException) {; }
            zeroVisits();
            return result;
        }

        private void smartRandom()
        {
            //
            // Generating random cells.
            //
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
            //
            // Placing finish in a random place.
            //
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            finishCell = cells[rand.Next(Global.maxX), rand.Next(Global.maxY)];
            startCell = finishCell;
            //
            // Marking linked cells as visited.
            //
            zeroVisits();
            finishCell.visited = true;
            for (int times = 0; times < Global.maxX * Global.maxY; times++)
            {
                for (int i = 0; i < Global.maxX; i++)
                {
                    for (int j = 0; j < Global.maxY; j++)
                    {
                        if (cells[i, j].visited)
                        {
                            if (!cells[i, j].top)
                                cells[i, j - 1].visited = true;
                            if (!cells[i, j].bottom)
                                cells[i, j + 1].visited = true;
                            if (!cells[i, j].right)
                                cells[i + 1, j].visited = true;
                            if (!cells[i, j].left)
                                cells[i - 1, j].visited = true;
                        }
                    }
                }
            }
            //
            // Searching for the farthest cell to place start.
            //
            double distance = 0;
            for (int i = 0; i < Global.maxX; i++)
            {
                for (int j = 0; j < Global.maxY; j++)
                {
                    if (cells[i, j].visited)
                    {
                        double buff = Math.Sqrt(Math.Pow((finishCell.X - cells[i, j].X), 2) + Math.Pow((finishCell.Y - cells[i, j].Y), 2));
                        if (buff > distance)
                        {
                            distance = buff;
                            startCell = cells[i, j];
                        }
                    }
                }
            }
            //
            // Cheking if the result is interesting enough (distance between start and finish).
            //
            if (Math.Sqrt(Math.Pow((startCell.X - finishCell.X), 2) + Math.Pow((startCell.Y - finishCell.Y), 2)) < (Global.maxX + Global.maxY) / (0.023 * (Global.maxX + Global.maxY) + 1.8))
                smartRandom();

            zeroVisits();
        }
    }
}
