using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Game_Of_Life.model
{
    [Serializable]
    class Motif
    {
        public bool[,] TableauCellules;
        public string Nom;
        public string Categorie;
        public int[] Shape;

        public Motif(bool[,] tabCel, string name, string cat, int[] shape)
        {
            TableauCellules = tabCel;
            Nom = name;
            Categorie = cat;
            Shape = shape;
        }

        public override string ToString()
        {
            return Nom;
        }
    }
}
