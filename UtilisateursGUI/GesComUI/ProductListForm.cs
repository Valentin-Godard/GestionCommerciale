using GesCom.BLL;
using GesCom.Models;
using GesComUI;
using GesCom.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace GesCom.UI
{
    public partial class ProductListForm : Form
    {
        private ProduitService produitService = new ProduitService();

        private List<Categorie> categories;

        public ProductListForm()
        {
            InitializeComponent();
            ChargerCategories();
            ChargerProduits();
        }

        private void ChargerCategories()
        {
            GesComDAO dao = new GesComDAO();
            categories = dao.GetAllCategories();

            cmbCategorie.DataSource = categories;
            cmbCategorie.DisplayMember = "Libelle"; // ce qu’on voit
            cmbCategorie.ValueMember = "Id";        // ce qu’on enregistre
        }


        private void ChargerProduits()
        {
            dgvProduits.DataSource = produitService.GetAllProduits();
        }

        private void btnNouveau_Click(object sender, EventArgs e)
        {
            txtCode.Text = "";
            txtLibelle.Text = "";
            cmbCategorie.SelectedIndex = -1;
            numPrix.Value = 0;
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbCategorie.SelectedItem == null)
                {
                    MessageBox.Show("Veuillez choisir une catégorie !");
                    return;
                }

                var categorieSelectionnee = (Categorie)cmbCategorie.SelectedItem;

                var produit = new Produit
                {
                    Libelle = txtLibelle.Text,
                    PrixHT = numPrix.Value,
                    Categorie = categorieSelectionnee 
                };

                produitService.AjouterProduit(produit);
                MessageBox.Show("Produit ajouté avec succès !");
                ChargerProduits();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvProduits_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProduits.SelectedRows.Count > 0)
            {
                var row = dgvProduits.SelectedRows[0];
                txtCode.Text = row.Cells["Id"].Value.ToString();
                txtLibelle.Text = row.Cells["Libelle"].Value.ToString();
                cmbCategorie.Text = row.Cells["Categorie"].Value.ToString();
                numPrix.Value = Convert.ToDecimal(row.Cells["PrixHT"].Value);
            }
        }

        private void btnModifier_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Fonction Modifier à implémenter");
        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Fonction Supprimer à implémenter");
        }
    }
}

