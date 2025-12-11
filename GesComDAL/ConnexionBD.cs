using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace GesCom.DAL
{
    public class ConnexionBD
    {
        // Objet SqlConnection unique
        private static SqlConnection connexion;

        // Chaîne de connexion pour SQL Server Express local
        private static readonly string connectionString =
            @"Server=localhost;Database=GesCom;Trusted_Connection=True;";

        // Retourne l'objet SqlConnection (créé si nécessaire)
        public static SqlConnection GetConnexionBD()
        {
            if (connexion == null)
                connexion = new SqlConnection(connectionString);
            return connexion;
        }

        // Ouvre la connexion si elle n'est pas déjà ouverte
        public static void OuvrirConnexion()
        {
            try // Gestion des erreurs de connexion
            {
                if (connexion == null)
                    connexion = new SqlConnection(connectionString);

                if (connexion.State != System.Data.ConnectionState.Open)
                    connexion.Open();
            }
            catch (SqlException ex) // Capture les erreurs SQL
            {
                throw new Exception("Impossible d'ouvrir la connexion SQL. Vérifie le serveur, la base ou le réseau.", ex);
            }
        }

        // Ferme la connexion si elle est ouverte
        public static void FermerConnexion()
        {
            try
            {
                if (connexion != null && connexion.State == System.Data.ConnectionState.Open)
                    connexion.Close();
            }
            catch (SqlException ex)
            {
                throw new Exception("Impossible de fermer la connexion SQL.", ex);
            }
        }
    }
}
