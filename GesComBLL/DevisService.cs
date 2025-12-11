using GesCom.DAL;
using GesCom.Models;
using GesComModels;
using System.Collections.Generic;

namespace GesCom.BLL
{
    public class DevisService
    {
        // Instance du DAO
        private DevisDAO dao = new DevisDAO();

        // Méthode pour récupérer tous les devis
        public List<Devis> GetTousLesDevis()
        {
            return dao.GetAllDevis();
        }

        // Méthode pour récupérer les lignes
        public void ChargerLignesDevis(Devis devis)
        {
            devis.Lignes = dao.GetLignesDevis(devis.Id);
        }

        // Méthode pour enregistrer 
        public void EnregistrerDevis(Devis devis)
        {
            dao.CreateDevis(devis);
        }

        // Méthode pour modifier
        public void ModifierDevis(Devis devis)
        {
            dao.UpdateDevis(devis);
        }

        // Méthode pour supprimer
        public void SupprimerDevis(int id)
        {
            dao.DeleteDevis(id);
        }
        // Méthode pour récupérer tous les statuts
        public List<Statut> GetTousLesStatuts()
        {
            return dao.GetAllStatuts();
        }
    }
}