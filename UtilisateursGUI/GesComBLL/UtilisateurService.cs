using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GesCom.DAL;
using GesComModels;
using System.Security.Cryptography;

namespace GesCom.BLL
{
    public class UtilisateurService
    {
        private GesComDAO dao = new GesComDAO();

        public bool Authentifier(string email, string motDePasse)
        {
            GesComDAO dao = new GesComDAO();
            Utilisateur user = dao.GetUtilisateurByEmail(email.Trim());

            if (user != null && user.MotDePasse.Trim() == motDePasse.Trim())
                return true;

            return false;
        }

        private static string HashPassword(string input)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] data = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sb = new StringBuilder();
                foreach (byte b in data)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }
    }
}
