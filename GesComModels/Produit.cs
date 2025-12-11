using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesCom.Models
{
    public class Produit
    {
        // Attributs privés
        private int _id;
        private string _libelle;
        private decimal _prixHT;
        private int _idCategorie;
        private Categorie _categorie;

        // Propriétés publiques
        public int Id { get => _id; set => _id = value; }
        public string Libelle { get => _libelle; set => _libelle = value; }
        public decimal PrixHT { get => _prixHT; set => _prixHT = value; }

        // relation avec catégorie
        public int IdCategorie { get => _idCategorie; set => _idCategorie = value; }
        public Categorie Categorie { get => _categorie; set => _categorie = value; }
    }
}


