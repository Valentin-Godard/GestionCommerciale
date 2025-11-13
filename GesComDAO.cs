using GesCom.Models;
using GesComModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesCom.DAL
{
    public class GesComDAO
    {
        // 🔑 Authentification utilisateur par email
        public Utilisateur GetUtilisateurByEmail(string email)
        {
            Utilisateur user = null;
            ConnexionBD.OuvrirConnexion();

            SqlCommand cmd = new SqlCommand(
                "SELECT * FROM Utilisateur WHERE email_ut = @email",
                ConnexionBD.GetConnexionBD());
            cmd.Parameters.AddWithValue("@email", email);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                user = new Utilisateur
                {
                    Id = (int)dr["id_ut"],
                    Email = dr["email_ut"].ToString(),
                    MotDePasse = dr["mp_ut"].ToString()
                };
            }

            dr.Close();
            ConnexionBD.FermerConnexion();

            return user;
        }

        // 📋 Liste des catégories
        public List<Categorie> GetAllCategories()
        {
            List<Categorie> categories = new List<Categorie>();
            ConnexionBD.OuvrirConnexion();

            SqlCommand cmd = new SqlCommand("SELECT * FROM Categorie", ConnexionBD.GetConnexionBD());
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                categories.Add(new Categorie
                {
                    Id = (int)dr["id_ct"],
                    Libelle = dr["libelle_categorie_ct"].ToString()
                });
            }

            dr.Close();
            ConnexionBD.FermerConnexion();
            return categories;
        }

        // 📋 Liste des produits avec leur catégorie
        public List<Produit> GetAllProduits()
        {
            List<Produit> produits = new List<Produit>();
            ConnexionBD.OuvrirConnexion();

            string sql = @"
                SELECT p.id_produit, p.libelle_produit, p.prix_ht, c.id_ct, c.libelle_categorie_ct
                FROM Produit p
                INNER JOIN Categorie c ON p.id_ct = c.id_ct";

            SqlCommand cmd = new SqlCommand(sql, ConnexionBD.GetConnexionBD());
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                produits.Add(new Produit
                {
                    Id = (int)dr["id_produit"],
                    Libelle = dr["libelle_produit"].ToString(),
                    PrixHT = (decimal)dr["prix_ht"],
                    Categorie = new Categorie
                    {
                        Id = (int)dr["id_ct"],
                        Libelle = dr["libelle_categorie_ct"].ToString()
                    }
                });
            }

            dr.Close();
            ConnexionBD.FermerConnexion();
            return produits;
        }

        //Ajout Catégorie
        public int InsertCategorie(Categorie cat)
        {
            ConnexionBD.OuvrirConnexion();

            SqlCommand cmd = new SqlCommand(
                "INSERT INTO Categorie (libelle_produit_ct) OUTPUT INSERTED.id_ct VALUES (@libelle)",
                ConnexionBD.GetConnexionBD());
            cmd.Parameters.AddWithValue("@libelle", cat.Libelle);

            // Récupérer l'ID généré automatiquement
            int id = (int)cmd.ExecuteScalar();

            ConnexionBD.FermerConnexion();

            return id;
        }

        // ➕ Ajout d’un produit
        public void InsertProduit(Produit produit)
        {
            if (produit.Categorie == null)
                throw new ArgumentException("Le produit doit avoir une catégorie assignée avant l'insertion.");

            ConnexionBD.OuvrirConnexion();

            SqlCommand cmd = new SqlCommand(
                "INSERT INTO Produit (libelle_produit, prix_ht, id_ct) VALUES (@lib, @prix, @cat)",
                ConnexionBD.GetConnexionBD());

            cmd.Parameters.AddWithValue("@lib", produit.Libelle);
            cmd.Parameters.AddWithValue("@prix", produit.PrixHT);
            cmd.Parameters.AddWithValue("@cat", produit.Categorie.Id); // ID de la catégorie

            cmd.ExecuteNonQuery();
            ConnexionBD.FermerConnexion();
        }
    }
}
