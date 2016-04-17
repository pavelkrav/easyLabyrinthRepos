using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace easyLabyrinth
{
    public partial class MainWindow : Window
    {
        Player player;

        public MainWindow()
        {
            InitializeComponent(); 
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            double multiplier = Global.windowSizeModifier / (Global.maxX + Global.maxY);
            this.Width = Global.maxX * multiplier + 16.6;
            this.Height = Global.maxY * multiplier + 39.6;
            this.Background = Global.backgroundColor;

            labGrid.Height = this.Height - 39;
            labGrid.Width = this.Width - 16;
            drawCenteredText(labGrid, new Canvas(), "Press N to generate new maze");

        }

        private void OnPlayerWon(Object sender, WinEventArgs e)
        {
            Console.WriteLine($"You won in {e.steps} steps");
            drawCenteredText(labGrid, new Canvas(), $"You won in {e.steps} steps");
        }

        private void keyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right)
            {
                player.moveRight();
            }
            if (e.Key == Key.Down)
            {
                player.moveDown();
            }
            if (e.Key == Key.Up)
            {
                player.moveUp();
            }
            if (e.Key == Key.Left)
            {
                player.moveLeft();
            }
            if (e.Key == Key.N)
            {
                drawNewLab();
            }
        }

        private void drawNewLab()
        {
            labGrid.Children.Clear();
            double multiplier = Global.windowSizeModifier / (Global.maxX + Global.maxY);
            this.Width = Global.maxX * multiplier + 16;
            this.Height = Global.maxY * multiplier + 39;

            labGrid.Height = this.Height - 39;
            labGrid.Width = this.Width - 16;        

            drawCenteredText(labGrid, new Canvas(), "Generating labyrinth...");

            this.Height += 0.6;
            this.Width += 0.6;

            Labyrinth lab = new Labyrinth(Generators.random2);
            drawLabyrinth(labGrid, new Canvas(), lab); 

            player = new Player(labGrid, lab);
            player.PlayerWon += OnPlayerWon;
        }

        private void drawLabyrinth(Grid grid, Canvas canvas, Labyrinth lab)
        {
            try
            {
                grid.Children.Clear();
                grid.Children.Add(canvas);
            }
            catch (System.ArgumentException) {; }
            foreach (Cell i in lab.cells)
            {
                drawCell(grid, canvas, lab, i.X, i.Y);
            }
        }

        private void drawCell(Grid grid, Canvas canvas, Labyrinth currentLab, int X, int Y)
        {
            Cell currentCell = currentLab.cells[X, Y];

            double cellHeight = grid.Height / Global.maxY;
            double cellWidth = grid.Width / Global.maxX;

            if (currentCell.top && (Y != 0? !currentLab.cells[X, Y - 1].bottom : true))
            {
                drawCellLine(canvas, currentCell, 0, 0, cellWidth, 0);
            }
            if (currentCell.bottom/* && (Y != (Global.maxY - 1) ? !currentLab.cells[X, Y + 1].top : true)*/)
            {
                drawCellLine(canvas, currentCell, 0, cellHeight, cellWidth, cellHeight);
            }
            if (currentCell.left && (X != 0 ? !currentLab.cells[X - 1, Y].right : true))
            {
                drawCellLine(canvas, currentCell, 0, 0, 0, cellHeight);
            }
            if (currentCell.right/* && (X != (Global.maxX - 1) ? !currentLab.cells[X + 1, Y].left : true)*/)
            {
                drawCellLine(canvas, currentCell, cellWidth, 0, cellWidth, cellHeight);
            }

            if (currentCell == currentLab.finishCell)
            {
                Rectangle rect = new Rectangle() { Height = cellHeight * 0.9, Width = cellWidth * 0.9, Fill = Brushes.Green, Stroke = Brushes.Green };
                Canvas.SetTop(rect, (Y + 0.05) * cellHeight);
                Canvas.SetLeft(rect, (X + 0.05) * cellWidth);
                canvas.Children.Add(rect);
            }

        }

        private void drawCellLine (Canvas canvas, Cell cell, double X1, double Y1, double X2, double Y2) // could be upgraded with enum
        {
            Line currentLine = new Line();
            currentLine.X1 = X1;
            currentLine.X2 = X2;
            currentLine.Y1 = Y1;
            currentLine.Y2 = Y2;
            currentLine.Stroke = Global.lineColor;
            currentLine.StrokeThickness = 2;
            Canvas.SetLeft(currentLine, labGrid.Width / Global.maxX * cell.X);
            Canvas.SetTop(currentLine, labGrid.Height / Global.maxY * cell.Y);
            canvas.Children.Add(currentLine);
        }

        private void drawCenteredText (Grid grid, Canvas canvas, string text)
        {
            try
            {
                grid.Children.Clear();
                grid.Children.Add(canvas);
            }
            catch (System.ArgumentException) { ; }
            canvas.Children.Clear();
            TextBox ini = new TextBox() { Foreground = Brushes.Black, Background = canvas.Background, FontSize = 15, Height = grid.Height, Width = grid.Width,
                                        VerticalContentAlignment = VerticalAlignment.Center, HorizontalContentAlignment = HorizontalAlignment.Center, IsReadOnly = true, Cursor = Cursors.None};
            ini.Text = text;
            ini.FontWeight = FontWeights.Bold;
            canvas.Children.Add(ini);
        }
    }
}
