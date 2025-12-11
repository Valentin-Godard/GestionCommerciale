using GesCom.Models;
using GesComModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace GesCom.DAL
{
    public class DevisDAO
    {
        // --- Méthode utilitaire pour retrouver l'ID du statut à partir du texte ---
        private int GetIdStatut(string libelleStatut, SqlConnection cnx, SqlTransaction trans)
        {
            try
            {
                // Si le statut est vide ou null, on met par défaut à 1 (ou une autre valeur par défaut)
                if (string.IsNullOrEmpty(libelleStatut)) return 1;

                string sql = "SELECT id_st FROM STATUT WHERE libelle_st = @lib";
                SqlCommand cmd = new SqlCommand(sql, cnx, trans);
                cmd.Parameters.AddWithValue("@lib", libelleStatut);
                object result = cmd.ExecuteScalar();

                // Si on trouve le statut, on retourne son ID, sinon par défaut 1
                return result != null ? (int)result : 1;
            }
            catch { return 1; }
        }

        // LISTE DES STATUTS
        public List<Statut> GetAllStatuts()
        {
            List<Statut> liste = new List<Statut>();
            // Ouverture de la connexion
            try
            {
                ConnexionBD.OuvrirConnexion();
                string query = "SELECT id_st, libelle_st FROM STATUT";
                SqlCommand command = new SqlCommand(query, ConnexionBD.GetConnexionBD());

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    liste.Add(new Statut
                    {
                        Id = (int)reader["id_st"],
                        Libelle = (string)reader["libelle_st"]
                    });
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur statuts : " + ex.Message);
            }
            finally
            {
                ConnexionBD.FermerConnexion();
            }
            return liste;
        }

        // AJOUT D'UN DEVIS (INSERT)
        public void CreateDevis(Devis devis)
        {
            ConnexionBD.OuvrirConnexion();
            SqlTransaction transaction = ConnexionBD.GetConnexionBD().BeginTransaction();

            try
            {
                // On récupère l'ID du statut via son Libellé (c'est ta logique demandée)
                int idStatut = GetIdStatut(devis.Statut, ConnexionBD.GetConnexionBD(), transaction);
                // Calcul du total HT sans remise
                decimal totalHT_SansRemise = devis.Lignes.Sum(l => l.PrixUnitaireHT * l.Quantite);

                // INSERTION ENTÊTE
                string reqDevis = @"INSERT INTO Devis (
                                        date_dv, taux_tva_dv, remise_ligne_dv, 
                                        prix_ht_sans_remise_dv, id_cl, id_st
                                    ) 
                                    OUTPUT INSERTED.id_dv 
                                    VALUES (
                                        @date, @tva, @remise, 
                                        @htSansRemise, @idClient, @idStatut
                                    )";
                // Création de la commande avec la transaction
                SqlCommand cmdDevis = new SqlCommand(reqDevis, ConnexionBD.GetConnexionBD(), transaction);
                cmdDevis.Parameters.AddWithValue("@date", devis.Date);
                cmdDevis.Parameters.AddWithValue("@tva", devis.TauxTVA);
                cmdDevis.Parameters.AddWithValue("@remise", devis.TauxRemiseGlobal);
                cmdDevis.Parameters.AddWithValue("@htSansRemise", totalHT_SansRemise);
                cmdDevis.Parameters.AddWithValue("@idClient", devis.Client.Id);
                cmdDevis.Parameters.AddWithValue("@idStatut", idStatut);

                // Exécution et récupération de l'ID généré
                int idDevis = (int)cmdDevis.ExecuteScalar();
                devis.Id = idDevis;

                // INSERTION LIGNES
                foreach (var ligne in devis.Lignes)
                {
                    string reqLigne = @"INSERT INTO CONTENIR (id_dv, id_produit, quantite, pourcentage_remise_total_dv) 
                                        VALUES (@idD, @idP, @qte, @remise)";

                    SqlCommand cmdLigne = new SqlCommand(reqLigne, ConnexionBD.GetConnexionBD(), transaction);
                    cmdLigne.Parameters.AddWithValue("@idD", idDevis);
                    cmdLigne.Parameters.AddWithValue("@idP", ligne.Produit.Id);
                    cmdLigne.Parameters.AddWithValue("@qte", ligne.Quantite);
                    cmdLigne.Parameters.AddWithValue("@remise", ligne.TauxRemise);

                    cmdLigne.ExecuteNonQuery();
                }

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                ConnexionBD.FermerConnexion();
            }
        }

        // LISTE DES DEVIS (SELECT)
        public List<Devis> GetAllDevis()
        {
            List<Devis> liste = new List<Devis>();
            // Ouverture de la connexion
            try
            {
                ConnexionBD.OuvrirConnexion();

                string req = @"SELECT d.id_dv, d.date_dv, d.taux_tva_dv, d.remise_ligne_dv, 
                                      d.prix_ht_sans_remise_dv, d.id_st,
                                      c.id_cl, c.nom_cl, 
                                      c.rue_livraison_cl, c.code_postale_livraison_cl, c.ville_livraison_cl,
                                      s.libelle_st
                               FROM Devis d 
                               INNER JOIN CLIENT c ON d.id_cl = c.id_cl 
                               LEFT JOIN STATUT s ON d.id_st = s.id_st 
                               ORDER BY d.date_dv DESC";

                SqlCommand cmd = new SqlCommand(req, ConnexionBD.GetConnexionBD());
                SqlDataReader dr = cmd.ExecuteReader();
                // Lecture des données
                while (dr.Read())
                {
                    Client client = new Client
                    {
                        Id = (int)dr["id_cl"],
                        Nom = dr["nom_cl"].ToString(),
                        RueLivraison = dr["rue_livraison_cl"] != DBNull.Value ? dr["rue_livraison_cl"].ToString() : "",
                        CpLivraison = dr["code_postale_livraison_cl"] != DBNull.Value ? dr["code_postale_livraison_cl"].ToString() : "",
                        VilleLivraison = dr["ville_livraison_cl"] != DBNull.Value ? dr["ville_livraison_cl"].ToString() : ""
                    };

                    liste.Add(new Devis
                    {
                        Id = (int)dr["id_dv"],
                        Code = "DEV-" + dr["id_dv"].ToString(),
                        Date = (DateTime)dr["date_dv"],
                        TauxTVA = dr["taux_tva_dv"] != DBNull.Value ? (decimal)dr["taux_tva_dv"] : 0m,
                        TauxRemiseGlobal = dr["remise_ligne_dv"] != DBNull.Value ? (decimal)dr["remise_ligne_dv"] : 0m,
                        // On stocke le texte et l'ID
                        Statut = dr["libelle_st"] != DBNull.Value ? dr["libelle_st"].ToString() : "Inconnu",
                        IdStatut = dr["id_st"] != DBNull.Value ? (int)dr["id_st"] : 1,
                        Client = client
                    });
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lecture Devis : " + ex.Message);
            }
            finally
            {
                ConnexionBD.FermerConnexion();
            }

            return liste;
        }

        // RÉCUPÉRER LES LIGNES
        public List<LigneDevis> GetLignesDevis(int idDevis)
        {
            List<LigneDevis> lignes = new List<LigneDevis>();
            ConnexionBD.OuvrirConnexion();
            // Requête pour récupérer les lignes avec les détails des produits et catégories
            string req = @"SELECT c.id_dv, c.quantite, c.pourcentage_remise_total_dv,
                                  p.id_produit, p.libelle_produit, p.prix_ht,
                                  cat.id_ct, cat.libelle_categorie_ct
                           FROM CONTENIR c
                           INNER JOIN PRODUIT p ON c.id_produit = p.id_produit
                           LEFT JOIN CATEGORIE cat ON p.id_ct = cat.id_ct
                           WHERE c.id_dv = @idDevis";

            SqlCommand cmd = new SqlCommand(req, ConnexionBD.GetConnexionBD());
            cmd.Parameters.AddWithValue("@idDevis", idDevis);

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                // Création de l'objet Produit  
                Produit prod = new Produit
                {
                    Id = (int)dr["id_produit"],
                    Libelle = dr["libelle_produit"].ToString(),
                    PrixHT = (decimal)dr["prix_ht"],
                    Categorie = new Categorie
                    {
                        Id = dr["id_ct"] != DBNull.Value ? (int)dr["id_ct"] : 0,
                        Libelle = dr["libelle_categorie_ct"] != DBNull.Value ? dr["libelle_categorie_ct"].ToString() : ""
                    }
                };
                // Création de la ligne de devis
                LigneDevis ligne = new LigneDevis
                {
                    IdProduit = prod.Id,
                    Produit = prod,
                    Quantite = (int)dr["quantite"],
                    PrixUnitaireHT = prod.PrixHT,
                    TauxRemise = dr["pourcentage_remise_total_dv"] != DBNull.Value ? Convert.ToDecimal(dr["pourcentage_remise_total_dv"]) : 0m
                };
                lignes.Add(ligne);
            }
            dr.Close();
            ConnexionBD.FermerConnexion();
            return lignes;
        }

        // MODIFICATION (UPDATE)
        public void UpdateDevis(Devis devis)
        {
            ConnexionBD.OuvrirConnexion();
            SqlTransaction transaction = ConnexionBD.GetConnexionBD().BeginTransaction();

            try
            {
                // On récupère l'ID du statut via le libellé (Ta logique)
                int idStatut = GetIdStatut(devis.Statut, ConnexionBD.GetConnexionBD(), transaction);

                decimal totalHT_SansRemise = devis.Lignes.Sum(l => l.PrixUnitaireHT * l.Quantite);

                // UPDATE DEVIS
                string reqUpdate = @"UPDATE Devis 
                                     SET date_dv=@date, 
                                         id_cl=@idClient, 
                                         id_st=@idStatut, 
                                         remise_ligne_dv=@remise, 
                                         prix_ht_sans_remise_dv=@htSans
                                     WHERE id_dv=@id";

                SqlCommand cmdUpdate = new SqlCommand(reqUpdate, ConnexionBD.GetConnexionBD(), transaction);
                cmdUpdate.Parameters.AddWithValue("@date", devis.Date);
                cmdUpdate.Parameters.AddWithValue("@idClient", devis.Client.Id);
                cmdUpdate.Parameters.AddWithValue("@idStatut", idStatut);
                cmdUpdate.Parameters.AddWithValue("@remise", devis.TauxRemiseGlobal);
                cmdUpdate.Parameters.AddWithValue("@htSans", totalHT_SansRemise);
                cmdUpdate.Parameters.AddWithValue("@id", devis.Id);
                cmdUpdate.ExecuteNonQuery();

                // RECREATION LIGNES
                SqlCommand cmdDel = new SqlCommand("DELETE FROM CONTENIR WHERE id_dv=@id", ConnexionBD.GetConnexionBD(), transaction);
                cmdDel.Parameters.AddWithValue("@id", devis.Id);
                cmdDel.ExecuteNonQuery();

                foreach (var ligne in devis.Lignes)
                {
                    string reqLigne = @"INSERT INTO CONTENIR (id_dv, id_produit, quantite, pourcentage_remise_total_dv) 
                                        VALUES (@idD, @idP, @qte, @remise)";

                    SqlCommand cmdLigne = new SqlCommand(reqLigne, ConnexionBD.GetConnexionBD(), transaction);
                    cmdLigne.Parameters.AddWithValue("@idD", devis.Id);
                    cmdLigne.Parameters.AddWithValue("@idP", ligne.Produit.Id);
                    cmdLigne.Parameters.AddWithValue("@qte", ligne.Quantite);
                    cmdLigne.Parameters.AddWithValue("@remise", ligne.TauxRemise);

                    cmdLigne.ExecuteNonQuery();
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                ConnexionBD.FermerConnexion();
            }
        }

        // SUPPRESSION (DELETE)
        public void DeleteDevis(int id)
        {
            ConnexionBD.OuvrirConnexion();
            SqlTransaction transaction = ConnexionBD.GetConnexionBD().BeginTransaction();
            // Suppression des lignes puis du devis
            try
            {
                SqlCommand cmdLignes = new SqlCommand("DELETE FROM CONTENIR WHERE id_dv = @id", ConnexionBD.GetConnexionBD(), transaction);
                cmdLignes.Parameters.AddWithValue("@id", id);
                cmdLignes.ExecuteNonQuery();

                SqlCommand cmdDevis = new SqlCommand("DELETE FROM Devis WHERE id_dv = @id", ConnexionBD.GetConnexionBD(), transaction);
                cmdDevis.Parameters.AddWithValue("@id", id);
                cmdDevis.ExecuteNonQuery();

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                ConnexionBD.FermerConnexion();
            }
        }
    }
}