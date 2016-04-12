using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyLabyrinth
{
    class Player
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int steps { get; set; }

        public Labyrinth labyrinth { get; }
        private Canvas playerCanvas;
        private Grid playerGrid;
        private Ellipse playerSpot;

        public Player(Grid grid, Labyrinth lab, int x, int y)
        {
            X = x;
            Y = y;
            steps = 0;
            labyrinth = lab;
            labyrinth.cells[X, Y].visited = true;
            X = labyrinth.startCell.X;
            Y = labyrinth.startCell.Y;
            playerCanvas = new Canvas();
            playerSpot = new Ellipse();
            playerGrid = grid;
            playerGrid.Children.Add(playerCanvas);
            drawSpot();
        }

        public Player(Grid grid, Labyrinth lab)
        {
            steps = 0;
            labyrinth = lab;
            labyrinth.cells[X, Y].visited = true;
            X = labyrinth.startCell.X;
            Y = labyrinth.startCell.Y;
            playerCanvas = new Canvas();
            playerSpot = new Ellipse();
            playerGrid = grid;
            playerGrid.Children.Add(playerCanvas);
            drawSpot();
        }

        public void drawSpot()
        {
            double cellHeight = playerGrid.Height / Global.maxY;
            double cellWidth = playerGrid.Width / Global.maxX;

            playerCanvas.Children.Remove(playerSpot);
            playerSpot = new Ellipse() { Width = cellWidth / 2, Height = cellHeight / 2, Fill = Global.playerColor, Stroke = Brushes.Black };
            Canvas.SetLeft(playerSpot, cellWidth / 2 * (2 * X + 0.5));
            Canvas.SetTop(playerSpot, cellHeight / 2 * (2 * Y + 0.5));
            playerCanvas.Children.Add(playerSpot);
            Console.WriteLine("Player coordinates: ({0}, {1})", X, Y);
        }

        public void moveLeft()
        {
            if (!labyrinth.cells[X, Y].left)
            {
                try
                {
                    X -= 1;
                    labyrinth.cells[X, Y].visited = true;
                    drawSpot();
                    steps++;
                    if (PlayerWon != null && labyrinth.cells[X, Y] == labyrinth.finishCell)
                    {
                        WinEventArgs e = new WinEventArgs(steps);
                        PlayerWon(this, e);
                    }
                }
                catch (System.IndexOutOfRangeException) { ; }
            }
        }

        public void moveRight()
        {
            if (!labyrinth.cells[X, Y].right)
            {
                try
                {
                    X += 1;
                    labyrinth.cells[X, Y].visited = true;
                    drawSpot();
                    steps++;
                    if (PlayerWon != null && labyrinth.cells[X, Y] == labyrinth.finishCell)
                    {
                        WinEventArgs e = new WinEventArgs(steps);
                        PlayerWon(this, e);
                    }
                }
                catch (System.IndexOutOfRangeException) { ; }
            }
        }

        public void moveUp()
        {
            if (!labyrinth.cells[X, Y].top)
            {
                try
                {
                    Y -= 1;
                    labyrinth.cells[X, Y].visited = true;
                    drawSpot();
                    steps++;
                    if (PlayerWon != null && labyrinth.cells[X, Y] == labyrinth.finishCell)
                    {
                        WinEventArgs e = new WinEventArgs(steps);
                        PlayerWon(this, e);
                    }
                }
                catch (System.IndexOutOfRangeException) { ; }
            }
        }

        public void moveDown()
        {
            if (!labyrinth.cells[X, Y].bottom)
            {
                try
                {
                    Y += 1;
                    labyrinth.cells[X, Y].visited = true;
                    drawSpot();
                    steps++;
                    if (PlayerWon != null && labyrinth.cells[X, Y] == labyrinth.finishCell)
                    {
                        WinEventArgs e = new WinEventArgs(steps);
                        PlayerWon(this, e);
                    }
                }
                catch (System.IndexOutOfRangeException) { ; }
            }
        }

        public event EventHandler<WinEventArgs> PlayerWon;
    }

    public class WinEventArgs : EventArgs
    {
        public int steps;
        public WinEventArgs(int steps)
        {
            this.steps = steps;
        }
    }
}