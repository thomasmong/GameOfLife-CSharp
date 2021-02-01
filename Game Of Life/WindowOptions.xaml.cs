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
using System.Windows.Shapes;

namespace Game_Of_Life
{
    /// <summary>
    /// Logique d'interaction pour WindowOptions.xaml
    /// </summary>
    public partial class WindowOptions : Window
    {
        private MainWindow Main;

        public WindowOptions(MainWindow wind)
        {
            Main = wind;
            InitializeComponent();

            //init
            StepperRow.Value = Main.Hauteur;
            StepperColumn.Value = Main.Largeur;
            StepperFreq.Value = (int) Main.Freq;
            CheckTheme.IsChecked = Main.Theme;
        }

        private void Appliquer(object sender, RoutedEventArgs e)
        {
            Main.Hauteur = StepperRow.Value.Value;
            Main.Largeur = StepperColumn.Value.Value;
            Main.Freq = StepperFreq.Value.Value;
            Main.Theme = CheckTheme.IsChecked.Value;
            Main.CreateConfig();
            Main.InitialiserCellules();
            this.Close();
        }
    }
}
