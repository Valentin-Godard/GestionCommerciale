using GesComModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GesCom.Models
{
    public class Devis
    {
        // --- ATTRIBUTS PRIVÉS ---
        private int _id;
        private string _code;
        private DateTime _date = DateTime.Now; 
        private string _statut = "En attente";
        private int _idStatut;

        private Client _client;
        private List<LigneDevis> _lignes = new List<LigneDevis>(); // Initialisation

        private decimal _tauxTVA = 20.0m;
        private decimal _tauxRemiseGlobal = 0.0m;


        // --- PROPRIÉTÉS PUBLIQUES  ---

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public string Statut
        {
            get { return _statut; }
            set { _statut = value; }
        }

        public int IdStatut
        {
            get { return _idStatut; }
            set { _idStatut = value; }
        }

        public Client Client
        {
            get { return _client; }
            set { _client = value; }
        }

        public List<LigneDevis> Lignes
        {
            get { return _lignes; }
            set { _lignes = value; }
        }

        public decimal TauxTVA
        {
            get { return _tauxTVA; }
            set { _tauxTVA = value; }
        }

        public decimal TauxRemiseGlobal
        {
            get { return _tauxRemiseGlobal; }
            set { _tauxRemiseGlobal = value; }
        }

        // --- PROPRIÉTÉS CALCULÉES (Lecture Seule) ---

        public decimal TotalHT_Brut
        {
            get { return _lignes.Sum(l => l.SousTotalHT); }
        }

        public decimal TotalHT_LignesRemisees
        {
            get { return _lignes.Sum(l => l.TotalLigneHT); }
        }

        public decimal MontantRemiseGlobal
        {
            get { return TotalHT_LignesRemisees * (_tauxRemiseGlobal / 100); }
        }

        public decimal TotalHT_Net
        {
            get { return TotalHT_LignesRemisees - MontantRemiseGlobal; }
        }

        public decimal MontantTVA
        {
            get { return TotalHT_Net * (_tauxTVA / 100); }
        }

        public decimal TotalTTC
        {
            get { return TotalHT_Net + MontantTVA; }
        }
    }
}