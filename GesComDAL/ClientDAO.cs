using GesCom.Models;
using GesComModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GesCom.DAL
{
    public class ClientDAO
    {
        // Liste des clients
        public List<Client> GetAllClients()
        {
            List<Client> clients = new List<Client>();
            ConnexionBD.OuvrirConnexion();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Client", ConnexionBD.GetConnexionBD());
            SqlDataReader dr = cmd.ExecuteReader(); // Exécution de la requête

            // Parcours des enregistrements
            while (dr.Read())
            {
                // Création d'un objet Client
                Client c = new Client
                {
                    // Affectation des propriétés
                    Id = (int)dr["id_cl"],
                    Nom = dr["nom_cl"].ToString(),
                    Telephone = dr["numero_telephone_cl"].ToString(),
                    Email = dr["adresse_mail_cl"].ToString()
                };
                // Gestion des NULLs
                c.Fax = dr["numero_fax_cl"] != DBNull.Value ? dr["numero_fax_cl"].ToString() : "";

                // Adresses
                c.NumeroLivraison = dr["numero_livraison_cl"]?.ToString();
                c.RueLivraison = dr["rue_livraison_cl"]?.ToString();
                c.VilleLivraison = dr["ville_livraison_cl"]?.ToString();
                c.CpLivraison = dr["code_postale_livraison_cl"]?.ToString();

                // Adresse de facturation
                c.NumeroFacturation = dr["numero_facturation_cl"]?.ToString();
                c.RueFacturation = dr["rue_facturation_cl"]?.ToString();
                c.VilleFacturation = dr["ville_facturation_cl"]?.ToString();
                c.CpFacturation = dr["code_postale_facturation_cl"]?.ToString();

                // Ajout à la liste
                clients.Add(c);
            }
            // Fermeture du DataReader et de la connexion
            dr.Close();
            ConnexionBD.FermerConnexion();
            return clients;
        }

        // Ajout Client
        public void InsertClient(Client client)
        {
            // Ouverture de la connexion
            ConnexionBD.OuvrirConnexion();
            // Requête d'insertion avec paramètres
            string req = @"INSERT INTO Client (nom_cl, numero_telephone_cl, numero_fax_cl, adresse_mail_cl,
                            numero_livraison_cl, rue_livraison_cl, ville_livraison_cl, code_postale_livraison_cl,
                            numero_facturation_cl, rue_facturation_cl, ville_facturation_cl, code_postale_facturation_cl) 
                           VALUES (@nom, @tel, @fax, @email, @numLiv, @rueLiv, @villeLiv, @cpLiv, @numFac, @rueFac, @villeFac, @cpFac)";
            // Création de la commande avec les paramètres
            SqlCommand cmd = new SqlCommand(req, ConnexionBD.GetConnexionBD());
            cmd.Parameters.AddWithValue("@nom", client.Nom);
            cmd.Parameters.AddWithValue("@tel", client.Telephone);
            cmd.Parameters.AddWithValue("@email", client.Email);
            cmd.Parameters.AddWithValue("@fax", (object)client.Fax ?? DBNull.Value);

            // Adresse de livraison
            cmd.Parameters.AddWithValue("@numLiv", (object)client.NumeroLivraison ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@rueLiv", (object)client.RueLivraison ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@villeLiv", (object)client.VilleLivraison ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@cpLiv", (object)client.CpLivraison ?? DBNull.Value);

            // Adresse de facturation
            cmd.Parameters.AddWithValue("@numFac", (object)client.NumeroFacturation ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@rueFac", (object)client.RueFacturation ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@villeFac", (object)client.VilleFacturation ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@cpFac", (object)client.CpFacturation ?? DBNull.Value);

            //  Exécution de la commande
            cmd.ExecuteNonQuery();
            ConnexionBD.FermerConnexion();
        }

        // Vérification Doublon (modifiée)
        public bool Existe(string nom, string email, int? excludeId = null)
        {
            // Ouverture de la connexion
            ConnexionBD.OuvrirConnexion();
            string req;
            SqlCommand cmd;

            // Requête différente si on exclut un ID (pour les mises à jour)
            if (excludeId.HasValue)
            {
                // Exclure le client avec l'ID spécifié
                req = "SELECT COUNT(*) FROM Client WHERE (nom_cl = @nom OR adresse_mail_cl = @mail) AND id_cl <> @id";
                cmd = new SqlCommand(req, ConnexionBD.GetConnexionBD());
                cmd.Parameters.AddWithValue("@id", excludeId.Value);
            }
            else // Pas d'exclusion d'ID
            {
                req = "SELECT COUNT(*) FROM Client WHERE nom_cl = @nom OR adresse_mail_cl = @mail";
                cmd = new SqlCommand(req, ConnexionBD.GetConnexionBD());
            }

            // Ajout des paramètres
            cmd.Parameters.AddWithValue("@nom", nom);
            cmd.Parameters.AddWithValue("@mail", email);
            int count = (int)cmd.ExecuteScalar();
            ConnexionBD.FermerConnexion();
            return count > 0;
        }

        // Vérifie si un client a des devis associés
        public bool ClientADevis(int idClient)
        {
            ConnexionBD.OuvrirConnexion();
            string req = "SELECT COUNT(*) FROM Devis WHERE id_cl = @id";
            SqlCommand cmd = new SqlCommand(req, ConnexionBD.GetConnexionBD());
            cmd.Parameters.AddWithValue("@id", idClient);
            int count = (int)cmd.ExecuteScalar();
            ConnexionBD.FermerConnexion();
            return count > 0;
        }

        // Suppression Client
        public void DeleteClient(int id)
        {
            ConnexionBD.OuvrirConnexion();
            string req = "DELETE FROM Client WHERE id_cl = @id";
            SqlCommand cmd = new SqlCommand(req, ConnexionBD.GetConnexionBD());
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            ConnexionBD.FermerConnexion();
        }

        // Mise à jour Client
        public void UpdateClient(Client client)
        {
            // Ouverture de la connexion
            ConnexionBD.OuvrirConnexion();
            string req = @"UPDATE Client SET 
                    nom_cl = @nom,
                    numero_telephone_cl = @tel,
                    numero_fax_cl = @fax,
                    adresse_mail_cl = @mail,
                    numero_livraison_cl = @numLiv,
                    rue_livraison_cl = @rueLiv,
                    ville_livraison_cl = @villeLiv,
                    code_postale_livraison_cl = @cpLiv,
                    numero_facturation_cl = @numFac,
                    rue_facturation_cl = @rueFac,
                    ville_facturation_cl = @villeFac,
                    code_postale_facturation_cl = @cpFac
                   WHERE id_cl = @id";
            // Création de la commande avec les paramètres
            SqlCommand cmd = new SqlCommand(req, ConnexionBD.GetConnexionBD());

            // Ajout des paramètres
            cmd.Parameters.AddWithValue("@id", client.Id);
            cmd.Parameters.AddWithValue("@nom", client.Nom);
            cmd.Parameters.AddWithValue("@tel", client.Telephone);
            cmd.Parameters.AddWithValue("@fax", (object)client.Fax ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@mail", client.Email);

            // Adresse de livraison
            cmd.Parameters.AddWithValue("@numLiv", (object)client.NumeroLivraison ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@rueLiv", (object)client.RueLivraison ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@villeLiv", (object)client.VilleLivraison ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@cpLiv", (object)client.CpLivraison ?? DBNull.Value);

            // Adresse de facturation
            cmd.Parameters.AddWithValue("@numFac", (object)client.NumeroFacturation ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@rueFac", (object)client.RueFacturation ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@villeFac", (object)client.VilleFacturation ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@cpFac", (object)client.CpFacturation ?? DBNull.Value);

            // Exécution de la commande
            cmd.ExecuteNonQuery();
            ConnexionBD.FermerConnexion();
        }

    }
}
