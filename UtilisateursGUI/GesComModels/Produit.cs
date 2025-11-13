using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesCom.Models
{
    public class Produit
    {
        public int Id { get; set; }
        public string Libelle { get; set; }
        public decimal PrixHT { get; set; }

        // relation avec catégorie
        public string IdCategorie { get; set; }   // FK
        public Categorie Categorie { get; set; }  // Navigation optionnelle
    }
}


