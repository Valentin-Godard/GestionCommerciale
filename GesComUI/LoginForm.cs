using GesCom.BLL;
using GesComUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GesCom.UI
{
    public partial class LoginForm : Form
    {
        // Service d'authentification
        private UtilisateurService userService = new UtilisateurService();

        // Constructeur
        public LoginForm()
        {
            InitializeComponent();
        }

        // Gestion du clic sur le bouton de connexion
        private void btnConnexion_Click(object sender, EventArgs e)
        {
            string login = txtEmail.Text;
            string mdp = txtMotDePasse.Text;

            // Validation des champs
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(mdp))
            {
                MessageBox.Show("Veuillez saisir vos identifiants.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tentative d'authentification
            if (userService.Authentifier(login, mdp))
            {
                ProductListForm f = new ProductListForm();
                f.Show();
                this.Hide();
            }
            else // Échec de l'authentification
            {
                MessageBox.Show("Identifiants incorrects.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
