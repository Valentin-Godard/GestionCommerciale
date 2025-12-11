using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesCom.Models
{
    public class Statut
    {
        // --- ATTRIBUTS PRIVÉS (Champs) ---
        private int _id;
        private string _libelle;

        // --- PROPRIÉTÉS PUBLIQUES (Accesseurs) ---

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Libelle
        {
            get { return _libelle; }
            set { _libelle = value; }
        }

        // --- MÉTHODES ---

        public override string ToString()
        {
            return Libelle;
        }
    }
}