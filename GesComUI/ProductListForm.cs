using GesCom.BLL;
using GesCom.Models;
using GesCom.DAL;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing; // Nécessaire pour le formatting parfois, mais surtout System.Windows.Forms

namespace GesCom.UI
{
    public partial class ProductListForm : Form
    {
        private ProduitService produitService = new ProduitService();
        private List<Categorie> categories;

        public ProductListForm()
        {
            InitializeComponent();

            // Masquer les en-têtes de ligne pour un look plus propre
            dgvProduits.RowHeadersVisible = false;

            // --- AJOUT IMPORTANT : Gestion de l'affichage personnalisé ---
            dgvProduits.CellFormatting += dgvProduits_CellFormatting;

            ChargerCategories();
            ChargerProduits();

            // Initialisation de l'état des boutons
            btnNouveau_Click(null, null);
        }

        // --- NAVIGATION ---

        private void btnClients_Click(object sender, EventArgs e)
        {
            this.Hide();
            ClientListForm fenetreClient = new ClientListForm();
            fenetreClient.ShowDialog();
            this.Show();
        }

        private void btnDevis_Click(object sender, EventArgs e)
        {
            this.Hide();
            DevisListForm fenetreDevis = new DevisListForm();
            fenetreDevis.ShowDialog();
            this.Show();
        }

        private void btnSynthese_Click(object sender, EventArgs e)
        {
            this.Hide();
            SyntheseListForm fenetreSynthese = new SyntheseListForm();
            fenetreSynthese.ShowDialog();
            this.Show();
        }

        // --- CHARGEMENT DONNÉES ---

        private void ChargerCategories()
        {
            ProduitDAO dao = new ProduitDAO();
            categories = dao.GetAllCategories();

            cmbCategorie.DataSource = categories;
            cmbCategorie.DisplayMember = "Libelle";
            cmbCategorie.ValueMember = "Id";
        }

        private void ChargerProduits()
        {
            dgvProduits.DataSource = null;
            dgvProduits.DataSource = produitService.GetAllProduits();

            // --- GESTION DES COLONNES ---

            // 1. Masquer l'ID technique du produit
            if (dgvProduits.Columns["Id"] != null)
                dgvProduits.Columns["Id"].Visible = false;

            // 2. Masquer la colonne d'ID numérique (IdCategorie) car on veut voir le texte
            if (dgvProduits.Columns["IdCategorie"] != null)
            {
                dgvProduits.Columns["IdCategorie"].Visible = false;
            }

            // 3. Afficher la colonne Objet (Categorie) et la renommer
            if (dgvProduits.Columns["Categorie"] != null)
            {
                dgvProduits.Columns["Categorie"].Visible = true;
                dgvProduits.Columns["Categorie"].HeaderText = "Catégorie"; // Titre de la colonne
                dgvProduits.Columns["Categorie"].DisplayIndex = 2; // Positionner après Code et Libelle (optionnel)
            }
        }

        // --- ÉVÉNEMENT POUR AFFICHER LE TEXTE DE LA CATÉGORIE ---
        private void dgvProduits_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Vérifier si on est sur la colonne "Categorie" et s'il y a une valeur
            if (dgvProduits.Columns[e.ColumnIndex].Name == "Categorie" && e.Value != null)
            {
                // Récupérer l'objet Categorie
                Categorie cat = (Categorie)e.Value;

                // Remplacer l'affichage par le Libelle de la catégorie
                e.Value = cat.Libelle;
                e.FormattingApplied = true;
            }
        }

        // --- LOGIQUE SELECTION ---

        private void dgvProduits_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProduits.SelectedRows.Count > 0)
            {
                var row = dgvProduits.SelectedRows[0];
                Produit p = (Produit)row.DataBoundItem;

                txtCode.Text = p.Id.ToString();
                txtLibelle.Text = p.Libelle;
                numPrix.Value = p.PrixHT;

                // Sélection de la catégorie dans la combo
                cmbCategorie.SelectedIndex = -1;
                if (p.Categorie != null)
                {
                    cmbCategorie.SelectedValue = p.Categorie.Id;
                }

                // Gestion boutons
                btnAjouter.Enabled = false;
                btnModifier.Enabled = true;
                btnSupprimer.Enabled = true;
            }
        }

        // --- BOUTONS ACTIONS ---

        private void btnNouveau_Click(object sender, EventArgs e)
        {
            txtCode.Text = "";
            txtLibelle.Text = "";
            cmbCategorie.SelectedIndex = -1;
            numPrix.Value = 0;

            dgvProduits.ClearSelection();

            btnAjouter.Enabled = true;
            btnModifier.Enabled = false;
            btnSupprimer.Enabled = false;

            txtLibelle.Focus();
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

                var produit = new Produit
                {
                    Libelle = txtLibelle.Text,
                    PrixHT = numPrix.Value,
                    Categorie = (Categorie)cmbCategorie.SelectedItem,
                    IdCategorie = (int)cmbCategorie.SelectedValue
                };

                produitService.AjouterProduit(produit);
                MessageBox.Show("Produit ajouté avec succès !");
                ChargerProduits();
                btnNouveau_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnModifier_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvProduits.SelectedRows.Count == 0) return;
                if (cmbCategorie.SelectedItem == null) return;

                var produit = new Produit
                {
                    Id = int.Parse(txtCode.Text),
                    Libelle = txtLibelle.Text,
                    PrixHT = numPrix.Value,
                    Categorie = (Categorie)cmbCategorie.SelectedItem,
                    IdCategorie = (int)cmbCategorie.SelectedValue
                };

                produitService.ModifierProduit(produit);
                MessageBox.Show("Produit modifié avec succès !");
                ChargerProduits();
                btnNouveau_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            if (dgvProduits.SelectedRows.Count == 0) return;

            if (MessageBox.Show("Voulez-vous vraiment supprimer ce produit ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    int produitId = int.Parse(txtCode.Text);
                    produitService.SupprimerProduit(produitId);

                    MessageBox.Show("Produit supprimé avec succès !");
                    ChargerProduits();
                    btnNouveau_Click(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}