using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GesCom.DAL;
using GesCom.Models;

namespace GesCom.BLL
{
    public class StatistiquesService
    {
        // --- ATTRIBUTS PRIVÉS ---
        private StatistiquesDAO dao = new StatistiquesDAO();

        // --- MÉTHODES DE RÉCUPÉRATION ---
        public List<SyntheseClient> ObtenirSynthese(DateTime debut, DateTime fin)
        {
            // Règle de gestion : Cohérence des dates
            if (debut > fin)
                throw new Exception("La date de début ne peut pas être postérieure à la date de fin.");

            return dao.GetSyntheseClients(debut, fin);
        }
    }
}
