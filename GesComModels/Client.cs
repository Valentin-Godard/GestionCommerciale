using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesComModels
{
    public class Client
    {
        // --------- Attributs privés ---------
        private int _id;
        private string _nom;
        private string _telephone;
        private string _fax;
        private string _email;

        // Adresse Livraison
        private string _numeroLivraison;
        private string _rueLivraison;
        private string _villeLivraison;
        private string _cpLivraison;

        // Adresse Facturation
        private string _numeroFacturation;
        private string _rueFacturation;
        private string _villeFacturation;
        private string _cpFacturation;

        // --------- Propriétés en une ligne ---------
        public int Id { get => _id; set => _id = value; }
        public string Nom { get => _nom; set => _nom = value; }
        public string Telephone { get => _telephone; set => _telephone = value; }
        public string Fax { get => _fax; set => _fax = value; }
        public string Email { get => _email; set => _email = value; }

        // Adresse Livraison
        public string NumeroLivraison { get => _numeroLivraison; set => _numeroLivraison = value; }
        public string RueLivraison { get => _rueLivraison; set => _rueLivraison = value; }
        public string VilleLivraison { get => _villeLivraison; set => _villeLivraison = value; }
        public string CpLivraison { get => _cpLivraison; set => _cpLivraison = value; }

        // Adresse Facturation
        public string NumeroFacturation { get => _numeroFacturation; set => _numeroFacturation = value; }
        public string RueFacturation { get => _rueFacturation; set => _rueFacturation = value; }
        public string VilleFacturation { get => _villeFacturation; set => _villeFacturation = value; }
        public string CpFacturation { get => _cpFacturation; set => _cpFacturation = value; }

        public override string ToString()
        {
            return Nom;
        }
    }
}
