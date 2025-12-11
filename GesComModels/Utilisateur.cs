using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesComModels
{
    public class Utilisateur
    {
        // Attributs privés
        private int _id;
        private string _email;
        private string _motDePasse;

        // Propriétés publiques
        public int Id { get => _id; set => _id = value; }
        public string Email { get => _email; set => _email = value; }
        public string MotDePasse { get => _motDePasse; set => _motDePasse = value; }
    }
}
