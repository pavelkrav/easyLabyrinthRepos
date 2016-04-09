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

        public MainWindow()
        {
            InitializeComponent(); 
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            Canvas labCanvas = new Canvas();
            labGrid.Height = this.Height - 39;
            labGrid.Width = this.Width - 16;

            labGrid.Children.Add(labCanvas);            
            labCanvas.Background = Global.background;

            this.Background = Global.background;
            this.Height += 0.6;
            this.Width += 0.6;

            Labyrinth testLab = new Labyrinth();
            foreach (Cell i in testLab.cells)
            {
                drawCell(labCanvas, i);
            }         

        }

        private void drawCell(Canvas canvas, Cell currentCell)
        {
            double cellHeight = labGrid.Height / Global.maxY;
            double cellWidth = labGrid.Width / Global.maxX;

            if (currentCell.top)
            {
                drawCellLine(canvas, currentCell, 0, 0, cellWidth, 0);
            }
            if (currentCell.bottom)
            {
                drawCellLine(canvas, currentCell, 0, cellHeight, cellWidth, cellHeight);
            }
            if (currentCell.left)
            {
                drawCellLine(canvas, currentCell, 0, 0, 0, cellHeight);
            }
            if (currentCell.right)
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
            currentLine.Stroke = Brushes.Black;
            currentLine.StrokeThickness = 2;
            Canvas.SetLeft(currentLine, labGrid.Width / Global.maxX * cell.X);
            Canvas.SetTop(currentLine, labGrid.Height / Global.maxY * cell.Y);
            canvas.Children.Add(currentLine);
        }
    }
}
