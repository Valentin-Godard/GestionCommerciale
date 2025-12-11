using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesCom.Models
{
    public class LigneDevis
    {
        // --- ATTRIBUTS PRIVÉS ---
        private int _id;
        private int _idProduit;
        private Produit _produit;
        private int _quantite;
        private decimal _tauxRemise;
        private decimal _prixUnitaireHT;

        // --- PROPRIÉTÉS PUBLIQUES (Accesseurs) ---

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int IdProduit
        {
            get { return _idProduit; }
            set { _idProduit = value; }
        }

        public Produit Produit
        {
            get { return _produit; }
            set { _produit = value; }
        }

        // Propriétés de lecture seule pour l'affichage (raccourcis)
        public string LibelleProduit => _produit?.Libelle;
        public string CategorieProduit => _produit?.Categorie?.Libelle;

        public int Quantite
        {
            get { return _quantite; }
            set { _quantite = value; }
        }

        public decimal TauxRemise
        {
            get { return _tauxRemise; }
            set { _tauxRemise = value; }
        }

        public decimal PrixUnitaireHT
        {
            get { return _prixUnitaireHT; }
            set { _prixUnitaireHT = value; }
        }

        // --- CHAMPS CALCULÉS (Lecture Seule) ---

        public decimal SousTotalHT
        {
            get { return PrixUnitaireHT * Quantite; }
        }

        public decimal MontantRemise
        {
            get { return SousTotalHT * (TauxRemise / 100); }
        }

        public decimal TotalLigneHT
        {
            get { return SousTotalHT - MontantRemise; }
        }
    }
}