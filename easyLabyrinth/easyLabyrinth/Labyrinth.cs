using System;
using System.Collections.Generic;

namespace easyLabyrinth
{
    enum Generators { random, smartRandom };

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
                case Generators.smartRandom:
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
        private double cellsLinking(int x1, int y1, int x2, int y2)
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

        private bool cellSetTop(int x, int y, bool state)
        {
            bool result = false;
            try
            {
                cells[x, y - 1].bottom = state;
                cells[x, y].top = state;
                result = true;
            }
            catch (System.IndexOutOfRangeException) {; }
            return result;
        }

        private bool cellSetBottom(int x, int y, bool state)
        {
            bool result = false;
            try
            {
                cells[x, y + 1].top = state;
                cells[x, y].bottom = state;
                result = true;
            }
            catch (System.IndexOutOfRangeException) {; }
            return result;
        }

        private bool cellSetLeft(int x, int y, bool state)
        {
            bool result = false;
            try
            {
                cells[x - 1, y].right = state;
                cells[x, y].left = state;
                result = true;
            }
            catch (System.IndexOutOfRangeException) {; }
            return result;
        }

        private bool cellSetRight(int x, int y, bool state)
        {
            bool result = false;
            try
            {
                cells[x + 1, y].left = state;
                cells[x, y].right = state;
                result = true;
            }
            catch (System.IndexOutOfRangeException) {; }
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
                }
            }
            Console.WriteLine("Random cells generated");
            //
            // Placing finish in a random place in a random corner.
            //
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            do
            {
                finishCell = cells[rand.Next(Global.maxX), rand.Next(Global.maxY)];
            }
            while (!(finishCell.X < Global.maxX * 0.1 || finishCell.X > Global.maxX * 0.9) || !(finishCell.Y < Global.maxY * 0.1 || finishCell.Y > Global.maxY * 0.9));
            startCell = finishCell;
            //
            // Opening single cells (not creating doubles).
            //
            for (int i = 0; i < Global.maxX; i++)
            {
                for (int j = 0; j < Global.maxY; j++)
                {
                    if (cells[i, j].top && cells[i, j].bottom && cells[i, j].left && cells[i, j].right)
                    {
                        bool temp = false;
                        int times = 0;
                        while (!temp && times < 4)
                        {
                            int rnd = rand.Next(4);
                            if (j != 0)
                                if (rnd == 0 && (!cells[i, j - 1].top || !cells[i, j - 1].bottom || !cells[i, j - 1].left || !cells[i, j - 1].right))
                                    temp = cellSetTop(i, j, false);
                            if (j != Global.maxY - 1)
                                if (rnd == 1 && (!cells[i, j + 1].top || !cells[i, j + 1].bottom || !cells[i, j + 1].left || !cells[i, j + 1].right))
                                    temp = cellSetBottom(i, j, false);
                            if (i != 0)
                                if (rnd == 2 && (!cells[i - 1, j].top || !cells[i - 1, j].bottom || !cells[i - 1, j].left || !cells[i - 1, j].right))
                                    temp = cellSetLeft(i, j, false);
                            if (i != Global.maxX - 1)
                                if (rnd == 3 && (!cells[i + 1, j].top || !cells[i + 1, j].bottom || !cells[i + 1, j].left || !cells[i + 1, j].right))
                                    temp = cellSetRight(i, j, false);
                            times++;
                        }
                    }
                }
            }
            //
            // Marking linked cells as visited.
            //
            zeroVisits();
            finishCell.visited = true;
            int visitable = 1;
            for (int a = 0; a < Global.maxX * Global.maxY; a++)
            {
                for (int b = 0; b < Global.maxX * Global.maxY - a; b++)
                {
                    for (int i = 0; i < Global.maxX; i++)
                    {
                        for (int j = 0; j < Global.maxY; j++)
                        {
                            if (cells[i, j].visited)
                            {
                                if (!cells[i, j].top)
                                {
                                    if (!cells[i, j - 1].visited)
                                    {
                                        cells[i, j - 1].visited = true;
                                        visitable++;
                                    }
                                }
                                if (!cells[i, j].bottom)
                                {
                                    if (!cells[i, j + 1].visited)
                                    {
                                        cells[i, j + 1].visited = true;
                                        visitable++;
                                    }
                                }
                                if (!cells[i, j].right)
                                {
                                    if (!cells[i + 1, j].visited)
                                    {
                                        cells[i + 1, j].visited = true;
                                        visitable++;
                                    }
                                }
                                if (!cells[i, j].left)
                                {
                                    if (!cells[i - 1, j].visited)
                                    {
                                        cells[i - 1, j].visited = true;
                                        visitable++;
                                    }
                                }
                            }
                        }
                    }
                }
                // checking if all cells already visitable
                Console.WriteLine($"Visitable cells {visitable} / {Global.maxX * Global.maxY}");
                if (visitable == Global.maxX * Global.maxY)
                    break;
                // opening a wall then check visitable cells again
                for (int i = 0; i < Global.maxX; i++)
                {
                    bool done = false;
                    for (int j = 0; j < Global.maxY; j++)
                    {                        
                        if (!cells[i, j].visited)
                        {
                            //int rnd = rand.Next(4);           ////////////
                            try
                            {
                                if (cells[i, j - 1].visited)
                                {
                                    cellSetTop(i, j, false);
                                    done = true;
                                    break;
                                }
                            }
                            catch (System.IndexOutOfRangeException) {; }

                            try
                            {
                                if (cells[i - 1, j].visited)
                                {
                                    cellSetLeft(i, j, false);
                                    done = true;
                                    break;
                                }
                            }
                            catch (System.IndexOutOfRangeException) {; }

                            try
                            {
                                if (cells[i, j + 1].visited)
                                {
                                    cellSetBottom(i, j, false);
                                    done = true;
                                    break;
                                }
                            }
                            catch (System.IndexOutOfRangeException) {; }

                            try
                            {
                                if (cells[i + 1, j].visited)
                                {
                                    cellSetRight(i, j, false);
                                    done = true;
                                    break;
                                }
                            }
                            catch (System.IndexOutOfRangeException) {; }
                        }                    
                    }
                    // opening only one wall per time
                    if (done)
                        break;
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
