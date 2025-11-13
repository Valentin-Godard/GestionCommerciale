using GesCom.DAL;
using GesCom.Models;
using GesComModels;
using System.Collections.Generic;

namespace GesCom.BLL
{
    public class ProduitService
    {
        private GesComDAO dao = new GesComDAO();

        public void AjouterProduit(Produit produit)
        {
            dao.InsertProduit(produit);
        }

        public List<Produit> GetAllProduits()
        {
            return dao.GetAllProduits();
        }
    }
}
