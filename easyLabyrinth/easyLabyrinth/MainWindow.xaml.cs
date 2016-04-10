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
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
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
            this.Width = Global.maxX * multiplier + 16;
            this.Height = Global.maxY * multiplier + 39;

            Canvas labCanvas = new Canvas();
            labGrid.Height = this.Height - 39;
            labGrid.Width = this.Width - 16;

            labGrid.Children.Add(labCanvas);            
            labCanvas.Background = Global.backgroundColor;

            this.Background = Global.backgroundColor;
            this.Height += 0.6;
            this.Width += 0.6;

            Labyrinth lab = new Labyrinth();
            foreach (Cell i in lab.cells)
            {
                drawCell(labCanvas, lab, i.X, i.Y);
            }

            player = new Player(labGrid, lab);         

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
        }

        private void drawCell(Canvas canvas, Labyrinth currentLab, int X, int Y)
        {
            Cell currentCell = currentLab.cells[X, Y];

            double cellHeight = labGrid.Height / Global.maxY;
            double cellWidth = labGrid.Width / Global.maxX;

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

        }

        private void drawCellLine (Canvas canvas, Cell cell, double X1, double Y1, double X2, double Y2)
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
    }
}
