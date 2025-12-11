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
    public class ProduitDAO
    {
        //Authentification utilisateur par email
        public Utilisateur GetUtilisateurByEmail(string email)
        {
            Utilisateur user = null;
            ConnexionBD.OuvrirConnexion();

            // Préparation de la commande SQL avec paramètre
            SqlCommand cmd = new SqlCommand(
                "SELECT * FROM Utilisateur WHERE email_ut = @email",
                ConnexionBD.GetConnexionBD());
            cmd.Parameters.AddWithValue("@email", email);

            // Exécution de la commande
            SqlDataReader dr = cmd.ExecuteReader();

            // Lecture des données
            if (dr.Read())
            {
                //  Création de l'objet Utilisateur
                user = new Utilisateur
                {
                    Id = (int)dr["id_ut"],
                    Email = dr["email_ut"].ToString(),
                    MotDePasse = dr["mp_ut"].ToString()
                };
            }

            // Fermeture du DataReader et de la connexion
            dr.Close();
            ConnexionBD.FermerConnexion();

            return user;
        }

        //Liste des catégories
        public List<Categorie> GetAllCategories()
        {
            List<Categorie> categories = new List<Categorie>();
            ConnexionBD.OuvrirConnexion();

            // Préparation de la commande SQL
            SqlCommand cmd = new SqlCommand("SELECT * FROM Categorie", ConnexionBD.GetConnexionBD());
            SqlDataReader dr = cmd.ExecuteReader();

            // Lecture des données
            while (dr.Read())
            {
                // Création de l'objet Categorie
                categories.Add(new Categorie
                {
                    Id = (int)dr["id_ct"],
                    Libelle = dr["libelle_categorie_ct"].ToString()
                });
            }

            // Fermeture du DataReader et de la connexion
            dr.Close();
            ConnexionBD.FermerConnexion();
            return categories;
        }

        //Liste des produits avec leur catégorie
        public List<Produit> GetAllProduits()
        {
            // Liste pour stocker les produits
            List<Produit> produits = new List<Produit>();
            ConnexionBD.OuvrirConnexion();

            // Requête SQL pour récupérer les produits avec leurs catégories
            string sql = @"
                SELECT p.id_produit, p.libelle_produit, p.prix_ht, 
                       c.id_ct, c.libelle_categorie_ct
                FROM Produit p
                INNER JOIN Categorie c ON p.id_ct = c.id_ct";

            // Exécution de la commande SQL
            SqlCommand cmd = new SqlCommand(sql, ConnexionBD.GetConnexionBD());
            SqlDataReader dr = cmd.ExecuteReader();

            // Lecture des données
            while (dr.Read())
            {
                // Création de l'objet Produit avec sa Catégorie
                produits.Add(new Produit
                {
                    Id = (int)dr["id_produit"],
                    Libelle = dr["libelle_produit"].ToString(),
                    PrixHT = (decimal)dr["prix_ht"],

                    // --- On remplit aussi l'IdCategorie du produit ---
                    IdCategorie = (int)dr["id_ct"],

                    Categorie = new Categorie
                    // Initialisation de la catégorie associée
                    {
                        Id = (int)dr["id_ct"],
                        Libelle = dr["libelle_categorie_ct"].ToString()
                    }
                });
            }

            // Fermeture du DataReader et de la connexion
            dr.Close();
            ConnexionBD.FermerConnexion();
            return produits;
        }

        //Ajout d’une catégorie
        public int InsertCategorie(Categorie cat)
        {
            // Vérification doublon
            ConnexionBD.OuvrirConnexion();

            // Préparation de la commande SQL
            SqlCommand cmd = new SqlCommand(
                "INSERT INTO Categorie (libelle_categorie_ct) OUTPUT INSERTED.id_ct VALUES (@libelle)",
                ConnexionBD.GetConnexionBD());
            cmd.Parameters.AddWithValue("@libelle", cat.Libelle);

            // Exécution de la commande et récupération de l'ID inséré
            int id = (int)cmd.ExecuteScalar();

            // Fermeture de la connexion
            ConnexionBD.FermerConnexion();

            // Retour de l'ID de la catégorie insérée
            return id;
        }

        //Vérifie si un produit existe déjà (doublon)
        public bool CheckProduitExists(string libelle, int idCategorie, int? produitId = null)
        {
            // Ouverture de la connexion
            ConnexionBD.OuvrirConnexion();

            // Préparation de la commande SQL
            string sql = @"
                SELECT COUNT(*) FROM Produit 
                WHERE libelle_produit = @lib 
                AND id_ct = @cat";

            // Si un produitId est fourni, on l'exclut de la vérification
            if (produitId != null)
                sql += " AND id_produit <> @id";

            //  Exécution de la commande SQL
            SqlCommand cmd = new SqlCommand(sql, ConnexionBD.GetConnexionBD());
            cmd.Parameters.AddWithValue("@lib", libelle);
            cmd.Parameters.AddWithValue("@cat", idCategorie);

            //  Ajout du paramètre produitId si fourni
            if (produitId != null)
                cmd.Parameters.AddWithValue("@id", produitId);

            // Exécution de la commande et récupération du nombre de doublons
            int count = (int)cmd.ExecuteScalar();

            ConnexionBD.FermerConnexion();

            return (count > 0);
        }

        //Ajout d’un produit
        public void InsertProduit(Produit produit)
        {
            // Vérification catégorie assignée
            if (produit.Categorie == null)
                throw new ArgumentException("Le produit doit avoir une catégorie assignée avant l'insertion.");

            // Vérification doublon
            if (CheckProduitExists(produit.Libelle, produit.Categorie.Id))
                throw new Exception("Ce produit existe déjà dans cette catégorie.");

            // Insertion du produit
            ConnexionBD.OuvrirConnexion();

            // Préparation de la commande SQL
            SqlCommand cmd = new SqlCommand(
                "INSERT INTO Produit (libelle_produit, prix_ht, id_ct) VALUES (@lib, @prix, @cat)",
                ConnexionBD.GetConnexionBD());

            // Ajout des paramètres
            cmd.Parameters.AddWithValue("@lib", produit.Libelle);
            cmd.Parameters.AddWithValue("@prix", produit.PrixHT);
            cmd.Parameters.AddWithValue("@cat", produit.Categorie.Id);

            // Exécution de la commande
            cmd.ExecuteNonQuery();
            ConnexionBD.FermerConnexion();
        }

        //MODIFICATION 
        public void ModifierProduit(Produit produit)
        {
            // Vérification catégorie assignée
            if (produit.Categorie == null)
                throw new ArgumentException("Le produit doit avoir une catégorie assignée avant la mise à jour.");

            // Vérification doublon sauf pour lui-même
            if (CheckProduitExists(produit.Libelle, produit.Categorie.Id, produit.Id))
                throw new Exception("Un autre produit avec le même nom existe déjà dans cette catégorie.");

            // Mise à jour du produit
            ConnexionBD.OuvrirConnexion();

            //  Préparation de la commande SQL
            SqlCommand cmd = new SqlCommand(
                "UPDATE Produit SET libelle_produit = @lib, prix_ht = @prix, id_ct = @cat " +
                "WHERE id_produit = @id",
                ConnexionBD.GetConnexionBD());

            // Ajout des paramètres
            cmd.Parameters.AddWithValue("@lib", produit.Libelle);
            cmd.Parameters.AddWithValue("@prix", produit.PrixHT);
            cmd.Parameters.AddWithValue("@cat", produit.Categorie.Id);
            cmd.Parameters.AddWithValue("@id", produit.Id);

            // Exécution de la commande
            cmd.ExecuteNonQuery();
            ConnexionBD.FermerConnexion();
        }
        // Méthode publique pour mettre à jour un produit
        public void UpdateProduit(Produit produit)
        {
            ModifierProduit(produit);
        }

        // Vérifie si le produit est utilisé dans un devis 
        public bool CheckProduitInDevis(int produitId)
        {
            // Ouverture de la connexion
            ConnexionBD.OuvrirConnexion();

            // Préparation de la commande SQL
            SqlCommand cmd = new SqlCommand(
                "SELECT COUNT(*) FROM Contenir WHERE id_produit = @id",
                ConnexionBD.GetConnexionBD());
            cmd.Parameters.AddWithValue("@id", produitId);

            // Exécution de la commande et récupération du nombre d'occurrences
            int count = (int)cmd.ExecuteScalar();

            // Fermeture de la connexion
            ConnexionBD.FermerConnexion();
            return (count > 0);
        }

        //Suppression d’un produit
        public void SupprimerProduit(int produitId)
        {
            // Vérification si le produit est utilisé dans un devis
            ConnexionBD.OuvrirConnexion();

            //  Préparation de la commande SQL
            SqlCommand cmd = new SqlCommand(
                "DELETE FROM Produit WHERE id_produit = @id",
                ConnexionBD.GetConnexionBD());
            cmd.Parameters.AddWithValue("@id", produitId);

            // Exécution de la commande
            cmd.ExecuteNonQuery();
            ConnexionBD.FermerConnexion();
        }
    }
}
