using GesCom.DAL;
using GesCom.Models;
using GesComModels;
using System.Collections.Generic;
using System;

namespace GesCom.BLL
{
    public class ProduitService
    {
        // Instanciation du DAO
        private ProduitDAO dao = new ProduitDAO();

        // Méthodes du service
        public void AjouterProduit(Produit produit)
        {
            dao.InsertProduit(produit);
        }

        public List<Produit> GetAllProduits()
        {
            return dao.GetAllProduits();
        }
        public void ModifierProduit(Produit produit)
        {
            dao.ModifierProduit(produit);
        }


        // Ajouté pour la suppression
        public void SupprimerProduit(int produitId)
        {
            // Vérifier la contrainte
            if (dao.CheckProduitInDevis(produitId))
            {
                // Si le produit est utilisé, lever une exception
                throw new Exception("Impossible de supprimer ce produit, car il est utilisé dans un ou plusieurs devis.");
            }

            // Sinon, procéder à la suppression
            dao.SupprimerProduit(produitId);
        }
    }
}
