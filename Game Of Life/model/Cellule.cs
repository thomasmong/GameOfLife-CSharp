using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Game_Of_Life.model
{
    class Cellule
    {
        public bool State = false;
        private bool NextState;
        private double Dim;
        public int I;
        public int J;
        public Rectangle Rect;
        private Canvas Canevas;
        public List<Cellule> Voisins;
        public Brush Alive = Brushes.Black;
        public Brush Dead = Brushes.WhiteSmoke;
        private Motif Pattern;
        private MainWindow Main;


        public Cellule(MainWindow main, double dimension, int i, int j, Canvas canevas)
        {
            Main = main;
            Dim = dimension;
            I = i;
            J = j;
            Canevas = canevas;
            Rect = new Rectangle();
        }

        public Cellule(double dimension, int i, int j, Canvas canevas)
        {
            Dim = dimension;
            I = i;
            J = j;
            Canevas = canevas;
            Rect = new Rectangle();
        }

        public Cellule(bool state)
        {
            State = state;
        }

        public void InitialiserRect()
        {
            Rect.Width = Dim;
            Rect.Height = Dim;
            Rect.Fill = Dead;
            Rect.Stroke = Brushes.Black;
            Rect.StrokeThickness = Dim / 100;

            //fonction maj état
            Rect.MouseEnter += Rect_MouseEnter;
            Rect.MouseLeave += Rect_MouseLeave;
            Rect.MouseDown += Mouse_SetState;

            Canvas.SetLeft(Rect, J * Dim);
            Canvas.SetTop(Rect, I * Dim);

            Canevas.Children.Add(Rect);
        }

        public void TrouverVoisins(Cellule[,] tab)
        {
            Voisins = new List<Cellule>(8);
            
            //ouest
            try
            {
                var v = tab[I, J - 1];
                Voisins.Add(v);
            }
            catch (IndexOutOfRangeException)
            {
                Voisins.Add(new Cellule(false));
            }
            //nord-ouest
            try
            {
                var v = tab[I - 1, J - 1];
                Voisins.Add(v);
            }
            catch (IndexOutOfRangeException)
            {
                Voisins.Add(new Cellule(false));
            }
            //nord
            try
            {
                var v = tab[I - 1, J];
                Voisins.Add(v);
            }
            catch (IndexOutOfRangeException)
            {
                Voisins.Add(new Cellule(false));
            }
            //nord-est
            try
            {
                var v = tab[I - 1, J + 1];
                Voisins.Add(v);
            }
            catch (IndexOutOfRangeException)
            {
                Voisins.Add(new Cellule(false));
            }
            //est
            try
            {
                var v = tab[I, J + 1];
                Voisins.Add(v);
            }
            catch (IndexOutOfRangeException)
            {
                Voisins.Add(new Cellule(false));
            }
            //sud-est
            try
            {
                var v = tab[I + 1, J + 1];
                Voisins.Add(v);
            }
            catch (IndexOutOfRangeException)
            {
                Voisins.Add(new Cellule(false));
            }
            //sud
            try
            {
                var v = tab[I + 1, J];
                Voisins.Add(v);
            }
            catch (IndexOutOfRangeException)
            {
                Voisins.Add(new Cellule(false));
            }
            //sud-ouest
            try
            {
                var v = tab[I + 1, J - 1];
                Voisins.Add(v);
            }
            catch (IndexOutOfRangeException)
            {
                Voisins.Add(new Cellule(false));
            }
        }

        private void Rect_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            SetColor();
        }

        private void Rect_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Rect.Fill = State ? Brushes.IndianRed : Brushes.LightGray;
        }

        private void Mouse_SetState(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            State = !State;
            SetColor();
        }

        public void SetColor()
        {
            Rect.Fill = State ? Alive : Dead;
        }

        public void FindNextState()
        {
            int n = 0;
            foreach (var cellule in Voisins)
            {
                if (cellule.State)
                {
                    n++;
                }
            }

            if (State)
            {
                if (n != 2 & n != 3)
                {
                    NextState = false;
                }
                else
                {
                    NextState = State;
                }
            }
            else
            {
                if (n == 3)
                {
                    NextState = true;
                }
                else
                {
                    NextState = State;
                }
            }

            if (NextState)
            {
                foreach (var cellule in Voisins)
                {
                    Main.AddCellToProcess(cellule.I,cellule.J);
                }
                Main.AddCellToProcess(I,J);
            }
        }

        public void Update()
        {
            State = NextState;
            SetColor();
        }

        public void DragPattern(Motif pat)
        {
            Pattern = pat;
            Rect.MouseEnter -= Rect_MouseEnter;
            Rect.MouseEnter += Rect_MouseEnterDragPattern;
            Rect.MouseLeave -= Rect_MouseLeave;
            Rect.MouseLeave += Rect_MouseLeaveDragPattern;

            Rect.MouseDown -= Mouse_SetState;
            Rect.MouseDown += Mouse_AfficherMotif;
        }

        public void NoDragPattern()
        {
            Rect.MouseEnter -= Rect_MouseEnterDragPattern;
            Rect.MouseEnter += Rect_MouseEnter;
            Rect.MouseLeave -= Rect_MouseLeaveDragPattern;
            Rect.MouseLeave += Rect_MouseLeave;

            Rect.MouseDown -= Mouse_AfficherMotif;
            Rect.MouseDown += Mouse_SetState;
        }

        private void Mouse_AfficherMotif(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var hauteur = Pattern.Shape[0];
            var largeur = Pattern.Shape[1];

            for (int i = 0; i < hauteur; i++)
            {
                for (int j = 0; j < largeur; j++)
                {
                    if (Pattern.TableauCellules[i, j])
                    {
                        try
                        {
                            Main.SetStateCell(I+i,J+j);
                        }
                        catch (IndexOutOfRangeException)
                        {

                        }
                    }
                }
            }
            Main.NoPattern();
        }

        private void Rect_MouseLeaveDragPattern(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var hauteur = Pattern.Shape[0];
            var largeur = Pattern.Shape[1];

            for (int i = 0; i < hauteur; i++)
            {
                for (int j = 0; j < largeur; j++)
                {
                    if (Pattern.TableauCellules[i, j])
                    {
                        try
                        {
                            Main.SetColorCell(I+i,J+j);
                        }
                        catch (IndexOutOfRangeException)
                        {

                        }
                    }
                }
            }
        }

        private void Rect_MouseEnterDragPattern(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var hauteur = Pattern.Shape[0];
            var largeur = Pattern.Shape[1];

            //Console.WriteLine(hauteur+", "+largeur);

            for (int i = 0; i < hauteur; i++)
            {
                for (int j = 0; j < largeur; j++)
                {
                    if (Pattern.TableauCellules[i, j])
                    {
                        try
                        {
                            Main.ChangeCellColor(I + i, J + j, Brushes.IndianRed);
                        }
                        catch (IndexOutOfRangeException)
                        {

                        }
                    }
                }
            }
        }

        public void RotatePattern()
        {
            Console.WriteLine(Pattern.Shape[0]+", "+Pattern.Shape[1]);
            //nouveau tableau - inversion des dimensions
            bool[,] newTab = new bool[Pattern.Shape[1],Pattern.Shape[0]];

            for (int i = 0; i < Pattern.Shape[0]; i++)
            {
                for (int j = 0; j < Pattern.Shape[1]; j++)
                {
                    newTab[j, Pattern.Shape[0] - i - 1] = Pattern.TableauCellules[i, j];
                }
            }

            Pattern.TableauCellules = newTab;

            var shape = new[] {Pattern.Shape[1], Pattern.Shape[0]};
            Pattern.Shape = shape;

            Console.WriteLine(Pattern.Shape[0] + ", " + Pattern.Shape[1]);
        }
    }
}