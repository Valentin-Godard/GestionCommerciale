using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesCom.Models
{
    public class SyntheseClient
    {
        // --- ATTRIBUTS PRIVÉS (Champs) ---
        private string _nomClient;
        private int _nbrDevis;
        private int _nbrDevisAcceptes;
        private int _nbrDevisAttente;
        private int _nbrDevisRefuses;
        private decimal _montantTotalHT;

        // --- PROPRIÉTÉS PUBLIQUES (Accesseurs) ---

        public string NomClient
        {
            get { return _nomClient; }
            set { _nomClient = value; }
        }

        public int NbrDevis
        {
            get { return _nbrDevis; }
            set { _nbrDevis = value; }
        }

        public int NbrDevisAcceptes
        {
            get { return _nbrDevisAcceptes; }
            set { _nbrDevisAcceptes = value; }
        }

        public int NbrDevisAttente
        {
            get { return _nbrDevisAttente; }
            set { _nbrDevisAttente = value; }
        }

        public int NbrDevisRefuses
        {
            get { return _nbrDevisRefuses; }
            set { _nbrDevisRefuses = value; }
        }

        public decimal MontantTotalHT
        {
            get { return _montantTotalHT; }
            set { _montantTotalHT = value; }
        }

        // --- PROPRIÉTÉS CALCULÉES (Lecture Seule) ---
        // Utilisation des champs privés pour le calcul

        public string PourcentageAcceptes
        {
            get
            {
                return _nbrDevis > 0 ? $"{(double)_nbrDevisAcceptes / _nbrDevis * 100:0.00}%" : "0%";
            }
        }

        public string PourcentageAttente
        {
            get
            {
                return _nbrDevis > 0 ? $"{(double)_nbrDevisAttente / _nbrDevis * 100:0.00}%" : "0%";
            }
        }

        public string PourcentageRefuses
        {
            get
            {
                return _nbrDevis > 0 ? $"{(double)_nbrDevisRefuses / _nbrDevis * 100:0.00}%" : "0%";
            }
        }
    }
}