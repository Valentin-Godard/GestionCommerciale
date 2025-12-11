using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesCom.Models
{
    public class Categorie
    {
        // Attributs privés
        private int _id;
        private string _libelle;

        // Propriétés en une ligne
        public int Id { get => _id; set => _id = value; }
        public string Libelle { get => _libelle; set => _libelle = value; }

        public override string ToString()
        {
            return Libelle;
        }
    }
}

