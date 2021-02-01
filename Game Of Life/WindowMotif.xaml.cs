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
using System.Windows.Shapes;
using Game_Of_Life.model;

namespace Game_Of_Life
{
    /// <summary>
    /// Logique d'interaction pour WindowMotif.xaml
    /// </summary>
    public partial class WindowMotif : Window
    {
        private int Largeur = 5;
        private int Hauteur = 5;
        private Cellule[,] TabCellules;
        private MainWindow Main;

        public WindowMotif(MainWindow wind)
        {
            Main = wind;
            InitializeComponent();
            CreateConfig();
            InitialiserCellules();
        }

        public void CreateConfig()
        {
            MainCanvas.Children.Clear();
            TabCellules = new Cellule[Hauteur, Largeur];

            //calcul des dimensions
            int dim1 = (int) MainCanvas.MaxWidth / Largeur;
            int dim2 = (int) MainCanvas.MaxHeight / Hauteur;
            int dim = Math.Min(dim1, dim2);

            //maj des dimensions du canvas
            MainCanvas.Width = dim * Largeur;
            MainCanvas.Height = dim * Hauteur;

            for (int i = 0; i < Hauteur; i++)
            {
                for (int j = 0; j < Largeur; j++)
                {
                    Cellule cel = new Cellule(dim, i, j, MainCanvas);
                    TabCellules[i, j] = cel;
                }
            }
        }

        public void InitialiserCellules()
        {
            foreach (var cel in TabCellules)
            {
                cel.InitialiserRect();
                cel.TrouverVoisins(TabCellules);
            }
        }

        private void UpdateConfig(object sender, RoutedEventArgs e)
        {
            Largeur = StepperColumn.Value.Value;
            Hauteur = StepperRow.Value.Value;
            CreateConfig();
            InitialiserCellules();
        }

        private void SavePattern(object sender, RoutedEventArgs e)
        {
            //vers tableau d'entiers
            bool[,] tab = new bool[Hauteur,Largeur];

            for (int i = 0; i < Hauteur; i++)
            {
                for (int j = 0; j < Largeur; j++)
                {
                    tab[i, j] = TabCellules[i, j].State;
                }
            }

            //nouveau motif
            string nom = NameBox.Text;
            string categ = CategBox.Text;
            var shape = new []{Hauteur,Largeur};
            Motif pattern = new Motif(tab,nom,categ,shape);

            //ajout aux ressources
            var f = File.Create("Resources/motifs/" + pattern.Nom + ".dat");
            BinaryFormatter b = new BinaryFormatter();
            b.Serialize(f, pattern);
            f.Close();
            this.Close();

            Main.SetMenuPatterns();
        }

        private void TextEntered(object sender, TextChangedEventArgs e)
        {
            if (NameBox.Text.Length > 0 & CategBox.SelectedIndex != -1)
            {
                ValidButton.IsEnabled = true;
            }
        }

        private void CategorieEntered(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text.Length > 0 & CategBox.SelectedIndex != -1)
            {
                ValidButton.IsEnabled = true;
            }
        }
    }
}