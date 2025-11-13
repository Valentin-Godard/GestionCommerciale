using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesComModels
{
    public class Utilisateur
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string MotDePasse { get; set; } // Haché


    }
}

