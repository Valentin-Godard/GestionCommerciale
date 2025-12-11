using GesCom.DAL;
using GesCom.Models;
using GesComModels;
using System.Collections.Generic;

namespace GesCom.BLL
{
    public class ClientService
    {
        // --- ATTRIBUTS PRIVÉS ---
        private ClientDAO dao = new ClientDAO();

        // --- MÉTHODES DE RÉCUPÉRATION ---
        public List<Client> GetClients()
        {
            return dao.GetAllClients();
        }

        // --- MÉTHODES DE VÉRIFICATION ---
        public bool EstDoublon(string nom, string email)
        {
            return dao.Existe(nom, email);
        }

        public bool EstDoublonModification(string nom, string email, int idExclu)
        {
            return dao.Existe(nom, email, idExclu);
        }

        public bool ADesDevis(int idClient)
        {
            return dao.ClientADevis(idClient);
        }

        // --- ACTIONS (SANS VÉRIFICATION) ---
        // La BLL exécute aveuglément. C'est la responsabilité de l'UI de vérifier avant d'appeler.

        public void AjouterClient(Client client)
        {
            dao.InsertClient(client);
        }

        public void ModifierClient(Client client)
        {
            dao.UpdateClient(client);
        }

        public void SupprimerClient(int id)
        {
            dao.DeleteClient(id);
        }
    }
}