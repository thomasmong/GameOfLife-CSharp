using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
using System.Windows.Threading;
using Game_Of_Life.model;

namespace Game_Of_Life
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int Largeur = 20;
        public int Hauteur = 12;
        private Cellule[,] TableauCellules;
        private DispatcherTimer Timer;
        public double Freq = 1;
        private bool IsSimul;
        private bool IsSettingPattern;
        public bool Theme = false;
        private List<Cellule> CellToProcess;
        private List<Cellule> CellToProcessCopy;

        public MainWindow()
        {
            InitializeComponent();
            SetMenuPatterns();
            CreateConfig();
            InitialiserCellules();

            //initt timer
            Timer = new DispatcherTimer();
            Timer.Tick += Update;
        }

        public void CreateConfig()
        {
            MainCanvas.Children.Clear();
            TableauCellules = new Cellule[Hauteur,Largeur];

            //calcul des dimensions
            int dim1 = (int)MainCanvas.MaxWidth / Largeur;
            int dim2 = (int)MainCanvas.MaxHeight / Hauteur;
            int dim = Math.Min(dim1, dim2);

            //maj des dimensions du canvas
            MainCanvas.Width = dim * Largeur;
            MainCanvas.Height = dim * Hauteur;

            for (int i = 0; i < Hauteur; i++)
            {
                for (int j = 0; j < Largeur; j++)
                {
                    Cellule cel = new Cellule(this, dim, i, j, MainCanvas);
                    TableauCellules[i, j] = cel;
                }
            }

            if (Theme)
            {
                foreach (var cel in TableauCellules)
                {
                    cel.Alive = Brushes.WhiteSmoke;
                    cel.Dead = Brushes.Black;
                }
            }
            else
            {
                foreach (var cel in TableauCellules)
                {
                    cel.Alive = Brushes.Black;
                    cel.Dead = Brushes.WhiteSmoke;
                }
            }
        }

        public void InitialiserCellules()
        {
            foreach (var cel in TableauCellules)
            {
                cel.InitialiserRect();
                cel.TrouverVoisins(TableauCellules);
            }
        }

        private void LancerSimul(object sender, RoutedEventArgs e)
        {
            //etat
            IsSimul = true;

            //Timer
            Timer.Interval = TimeSpan.FromSeconds(1.0/Freq);
            Timer.Start();

            //maj boutons menu
            StartMenuItem.IsEnabled = false;
            StopMenuItem.IsEnabled = true;
            PauseMenuItem.IsEnabled = true;

            //cell to process
            InitCellToProcess();
        }

        private void InitCellToProcess()
        {
            CellToProcess = new List<Cellule>();
            CellToProcessCopy = new List<Cellule>();
            foreach (var cel in TableauCellules)
            {
                if (cel.State)
                {
                    AddCellToProcess(cel.I,cel.J);
                    foreach (var celVoisin in cel.Voisins)
                    {
                        AddCellToProcess(celVoisin.I,celVoisin.J);
                    }
                }
            }
        }

        private void PauseSimul(object sender, RoutedEventArgs e)
        {
            Timer.IsEnabled = !Timer.IsEnabled;
            PauseMenuItem.Header = Timer.IsEnabled ? "Mettre en pause la simulation" : "Reprendre la simulation";
            InitCellToProcess();
        }

        private void StopSimul(object sender, RoutedEventArgs e)
        {
            Timer.Stop();
            foreach (var cel in TableauCellules)
            {
                cel.State = false;
                cel.SetColor();
            }

            StartMenuItem.IsEnabled = true;
            PauseMenuItem.IsEnabled = false;
            StopMenuItem.IsEnabled = false;

            //etat
            IsSimul = false;
        }

        private void ToucheEvent(object sender, KeyEventArgs e)
        {
            var touche = e.Key;

            switch (touche)
            {
                //lancer la simulation
                case Key.F5:
                    if (!IsSimul)
                    {
                        LancerSimul(new object(), new RoutedEventArgs());
                    }

                    break;

                //arrêt de la simulation
                case Key.Escape:
                    if (IsSimul)
                    {
                        StopSimul(new object(), new RoutedEventArgs());
                    }

                    break;

                //mise en pause de la simulation
                case Key.Space:
                    if (IsSimul)
                    {
                        PauseSimul(new object(), new RoutedEventArgs());
                    }

                    break;

                case Key.R:
                    if (IsSettingPattern)
                    {
                        if (Keyboard.Modifiers == ModifierKeys.Control)
                        {
                            RotatePatterns();
                        }
                    } 
                    break;
            }
        }

        private void RotatePatterns()
        {
            foreach (var cel in TableauCellules)
            {
                cel.SetColor();
            }
            TableauCellules[0,0].RotatePattern();
        }

        private void Update(object sender, EventArgs e)
        {
            //copie
            foreach (var cellule in CellToProcess)
            {
                CellToProcessCopy.Add(cellule);
            }

            //suppr
            CellToProcess.Clear();


            foreach (var cel in CellToProcessCopy)
            {
                cel.FindNextState();
            }

            foreach (var cel in CellToProcessCopy)
            { 
                cel.Update();
            }

            CellToProcessCopy.Clear();
        }

        private void DisplayOptions(object sender, RoutedEventArgs e)
        {
            var OptionsW = new WindowOptions(this){Owner = this};
            OptionsW.ShowDialog();
        }

        private void DisplayNewPattern(object sender, RoutedEventArgs e)
        {
            var MotifW = new WindowMotif(this){Owner = this};
            MotifW.ShowDialog();
        }

        public void SetMenuPatterns()
        {
            MenuPlaneur.Items.Clear();
            MenuGener.Items.Clear();
            MenuStatio.Items.Clear();
            MenuOsci.Items.Clear();
            MenuAutre.Items.Clear();

            var files = Directory.GetFiles("Resources/motifs");
            foreach (var file in files)
            {
                var f = File.Open(file,FileMode.Open);
                BinaryFormatter b = new BinaryFormatter();
                var pattern = (Motif) b.Deserialize(f);
                f.Close();

                //item
                MenuItem item = new MenuItem();
                item.Header = pattern.Nom;
                item.Click += Pattern_Click;


                switch (pattern.Categorie)
                {
                    case "Planeur":
                        MenuPlaneur.Items.Add(item);
                        break;
                    case "Générateur":
                        MenuGener.Items.Add(item);
                        break;
                    case "Stationnaire":
                        MenuStatio.Items.Add(item);
                        break;
                    case "Oscillateur":
                        MenuOsci.Items.Add(item);
                        break;
                    case "Autre":
                        MenuAutre.Items.Add(item);
                        break;
                }

            }
        }

        private void Pattern_Click(object sender, EventArgs e)
        {
            IsSettingPattern = true;

            //recuperation du motif
            var item = (MenuItem) sender;
            var name = item.Header;
            var f = File.Open("Resources/motifs/"+name+".dat", FileMode.Open);
            BinaryFormatter b = new BinaryFormatter();
            var pattern = (Motif)b.Deserialize(f);
            f.Close();

            //fonctions des cellules
            foreach (var cel in TableauCellules)
            {
                cel.DragPattern(pattern);
            }
        }

        public void NoPattern()
        {
            IsSettingPattern = false;
            foreach (var cel in TableauCellules)
            {
                cel.NoDragPattern();
            }
        }


        public void ChangeCellColor(int i, int j, Brush color)
        {
            TableauCellules[i, j].Rect.Fill = color;
        }

        public void SetColorCell(int i, int j)
        {
            TableauCellules[i,j].SetColor();
        }

        public void SetStateCell(int i, int j)
        {
            TableauCellules[i, j].State = true;
            TableauCellules[i,j].SetColor();
        }

        public void AddCellToProcess(int i, int j)
        {
            var cel = TableauCellules[i, j];
            if (!CellToProcess.Contains(cel))
            {
                CellToProcess.Add(cel);
            }
        }
    }
}