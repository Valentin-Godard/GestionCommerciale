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
        public string IdCategorie { get; set; }
        public Categorie Categorie { get; set; }
    }
}


