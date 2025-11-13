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
        private UtilisateurService userService = new UtilisateurService();

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnConnexion_Click(object sender, EventArgs e)
        {
            string login = txtEmail.Text;
            string mdp = txtMotDePasse.Text;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(mdp))
            {
                MessageBox.Show("Veuillez saisir vos identifiants.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (userService.Authentifier(login, mdp))
            {
                ProductListForm f = new ProductListForm();
                f.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Identifiants incorrects.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
