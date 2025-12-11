using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GesCom.Models;
using System.Data.SqlClient;

namespace GesCom.DAL
{
    public class StatistiquesDAO
    {
        public List<SyntheseClient> GetSyntheseClients(DateTime dateDebut, DateTime dateFin)
        {
            // Liste pour stocker les synthèses par client
            List<SyntheseClient> liste = new List<SyntheseClient>();
            ConnexionBD.OuvrirConnexion();

            // Requête groupée par client avec filtrage par date sur les devis
            string req = @"
                SELECT 
                    c.nom_cl,
                    COUNT(d.id_dv) AS TotalDevis,
                    SUM(CASE WHEN s.libelle_st = 'Accepté' THEN 1 ELSE 0 END) AS NbAcceptes,
                    SUM(CASE WHEN s.libelle_st = 'En attente' THEN 1 ELSE 0 END) AS NbAttente,
                    SUM(CASE WHEN s.libelle_st = 'Refusé' THEN 1 ELSE 0 END) AS NbRefuses,
                    SUM(CASE WHEN s.libelle_st = 'Accepté' THEN d.prix_ht_sans_remise_dv ELSE 0 END) AS MontantTotal
                FROM Client c
                LEFT JOIN Devis d ON c.id_cl = d.id_cl AND d.date_dv >= @dateDebut AND d.date_dv <= @dateFin
                LEFT JOIN Statut s ON d.id_st = s.id_st
                GROUP BY c.nom_cl
                ORDER BY c.nom_cl";

            SqlCommand cmd = new SqlCommand(req, ConnexionBD.GetConnexionBD());

            // Ajuster la date de fin pour inclure toute la journée
            DateTime finJournee = dateFin.Date.AddDays(1).AddTicks(-1);

            cmd.Parameters.AddWithValue("@dateDebut", dateDebut.Date);
            cmd.Parameters.AddWithValue("@dateFin", finJournee);
            // Exécution de la requête
            SqlDataReader dr = cmd.ExecuteReader();
            // Parcours des résultats
            while (dr.Read())
            {
                // Création de l'objet SyntheseClient et affectation des valeurs
                SyntheseClient s = new SyntheseClient
                {
                    NomClient = dr["nom_cl"].ToString(),
                    NbrDevis = dr["TotalDevis"] != DBNull.Value ? (int)dr["TotalDevis"] : 0,
                    NbrDevisAcceptes = dr["NbAcceptes"] != DBNull.Value ? (int)dr["NbAcceptes"] : 0,
                    NbrDevisAttente = dr["NbAttente"] != DBNull.Value ? (int)dr["NbAttente"] : 0,
                    NbrDevisRefuses = dr["NbRefuses"] != DBNull.Value ? (int)dr["NbRefuses"] : 0,
                    MontantTotalHT = dr["MontantTotal"] != DBNull.Value ? (decimal)dr["MontantTotal"] : 0m
                };
                liste.Add(s);
            }
            // Fermeture du DataReader et de la connexion
            dr.Close();
            ConnexionBD.FermerConnexion();
            return liste;
        }
    }
}
