using GesCom.BLL;
using GesCom.Models;
using GesComModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GesCom.UI
{
    public partial class DevisListForm : Form
    {
        private DevisService devisService = new DevisService();
        private ClientService clientService = new ClientService();
        private ProduitService productService = new ProduitService();

        private Devis currentDevis;
        private BindingList<LigneDevis> lignesBinding;

        public DevisListForm()
        {
            InitializeComponent();

            // Empêche la saisie libre dans le statut
            cbStatut.DropDownStyle = ComboBoxStyle.DropDownList;

            ChargerListesDeroulantes();
            ChargerDevisGrid();

            currentDevis = new Devis();
            btnAjouter.Enabled = true;
            btnModifier.Enabled = false;
            btnSupprimer.Enabled = false;

            btnNouveau_Click(null, null);
        }

        private void ChargerListesDeroulantes()
        {
            // Clients
            cbClients.DataSource = clientService.GetClients();
            cbClients.DisplayMember = "Nom";
            cbClients.ValueMember = "Id";

            // Produits
            cbProduits.DataSource = productService.GetAllProduits();
            cbProduits.DisplayMember = "Libelle";
            cbProduits.ValueMember = "Id";

            // STATUTS (Depuis la BDD via BLL)
            // La BLL doit avoir la méthode GetTousLesStatuts() qui appelle le DAO GetAllStatuts()
            cbStatut.DataSource = devisService.GetTousLesStatuts();
            cbStatut.DisplayMember = "Libelle";
            cbStatut.ValueMember = "Id";
        }

        private void ChargerDevisGrid()
        {
            dgvDevis.DataSource = null;
            dgvDevis.DataSource = devisService.GetTousLesDevis();

            if (dgvDevis.Columns["Lignes"] != null) dgvDevis.Columns["Lignes"].Visible = false;
            if (dgvDevis.Columns["Client"] != null) dgvDevis.Columns["Client"].Visible = false;
            if (dgvDevis.Columns["StatutObj"] != null) dgvDevis.Columns["StatutObj"].Visible = false;

            if (dgvDevis.Columns["TotalTTC"] != null) dgvDevis.Columns["TotalTTC"].DefaultCellStyle.Format = "C2";
            if (dgvDevis.Columns["Date"] != null) dgvDevis.Columns["Date"].DefaultCellStyle.Format = "dd/MM/yyyy";
        }

        private void dgvDevis_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDevis.SelectedRows.Count > 0)
            {
                Devis selection = (Devis)dgvDevis.SelectedRows[0].DataBoundItem;
                currentDevis = selection;

                try { devisService.ChargerLignesDevis(currentDevis); } catch { }

                cbClients.SelectedValue = currentDevis.Client.Id;
                dtpDate.Value = currentDevis.Date;

                // On sélectionne le statut via son ID
                cbStatut.SelectedValue = currentDevis.IdStatut;

                txtRemiseGlobal.Text = currentDevis.TauxRemiseGlobal.ToString();

                lignesBinding = new BindingList<LigneDevis>(currentDevis.Lignes);
                dgvLignes.DataSource = lignesBinding;
                ConfigLignesGridColumns();
                RecalculerTotauxUI();

                btnAjouter.Enabled = false;
                btnModifier.Enabled = true;
                btnSupprimer.Enabled = true;
                grpDetails.Text = $"Modification Devis : {currentDevis.Code}";
            }
        }

        private void ConfigLignesGridColumns()
        {
            dgvLignes.AutoGenerateColumns = false;
            dgvLignes.Columns.Clear();
            dgvLignes.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Produit", DataPropertyName = "LibelleProduit", Width = 140, ReadOnly = true });
            dgvLignes.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Qté", DataPropertyName = "Quantite", Width = 40, ReadOnly = true });
            dgvLignes.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "P.U. HT", DataPropertyName = "PrixUnitaireHT", Width = 70, DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }, ReadOnly = true });
            dgvLignes.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Remise %", DataPropertyName = "TauxRemise", Width = 60, ReadOnly = true });
        }

        private void CbClients_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbClients.SelectedItem != null)
            {
                Client c = (Client)cbClients.SelectedItem;
                lblAdresseClient.Text = $"Livraison : {c.RueLivraison} {c.CpLivraison} {c.VilleLivraison}";
            }
            else { lblAdresseClient.Text = ""; }
        }

        private void txtRemiseGlobal_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtRemiseGlobal.Text, out decimal remise))
            {
                currentDevis.TauxRemiseGlobal = remise;
                RecalculerTotauxUI();
            }
        }

        private void btnNouveau_Click(object sender, EventArgs e)
        {
            currentDevis = new Devis();
            currentDevis.Date = DateTime.Now;
            currentDevis.TauxRemiseGlobal = 0;
            currentDevis.Lignes = new List<LigneDevis>();

            if (cbClients.Items.Count > 0) cbClients.SelectedIndex = 0; else cbClients.SelectedIndex = -1;
            dtpDate.Value = DateTime.Now;

            // Sélectionne le premier statut par défaut
            if (cbStatut.Items.Count > 0) cbStatut.SelectedIndex = 0;

            txtRemiseGlobal.Text = "0";
            cbProduits.SelectedIndex = -1;
            txtQuantite.Text = "1";
            txtRemiseLigne.Text = "0";

            lignesBinding = new BindingList<LigneDevis>(currentDevis.Lignes);
            dgvLignes.DataSource = lignesBinding;
            ConfigLignesGridColumns();
            RecalculerTotauxUI();

            dgvDevis.ClearSelection();
            btnAjouter.Enabled = true;
            btnModifier.Enabled = false;
            btnSupprimer.Enabled = false;
            grpDetails.Text = "Nouveau Devis";
        }

        private void btnAjouterLigne_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbProduits.SelectedItem == null) return;
                Produit prod = (Produit)cbProduits.SelectedItem;
                if (!int.TryParse(txtQuantite.Text, out int qte) || qte <= 0) throw new Exception("Qté invalide.");
                if (!decimal.TryParse(txtRemiseLigne.Text, out decimal remise) || remise < 0 || remise > 100) throw new Exception("Remise invalide.");

                LigneDevis ligne = new LigneDevis
                {
                    Produit = prod,
                    IdProduit = prod.Id,
                    PrixUnitaireHT = prod.PrixHT,
                    Quantite = qte,
                    TauxRemise = remise
                };
                lignesBinding.Add(ligne);
                RecalculerTotauxUI();
            }
            catch (Exception ex) { MessageBox.Show("Erreur : " + ex.Message); }
        }

        private void btnSupprimerLigne_Click(object sender, EventArgs e)
        {
            if (dgvLignes.CurrentRow != null)
            {
                lignesBinding.Remove((LigneDevis)dgvLignes.CurrentRow.DataBoundItem);
                RecalculerTotauxUI();
            }
        }

        private void RecalculerTotauxUI()
        {
            lblTotalBrut.Text = $"HT Hors Remise : {currentDevis.TotalHT_Brut:C2}";
            lblTauxRemise.Text = $"Remise Globale : {currentDevis.TauxRemiseGlobal} %";
            lblTotalNet.Text = $"HT Remise Comprise : {currentDevis.TotalHT_Net:C2}";
            lblTauxTVA.Text = $"Taux TVA : {currentDevis.TauxTVA} %";
            lblMontantTVA.Text = $"Montant TVA : {currentDevis.MontantTVA:C2}";
            lblTTC.Text = $"TOTAL TTC : {currentDevis.TotalTTC:C2}";
        }

        // --- VALIDATION UI ---
        private void ValiderSaisieDevis()
        {
            if (cbClients.SelectedItem == null) throw new Exception("Veuillez sélectionner un client.");
            if (currentDevis.Lignes.Count == 0) throw new Exception("Le devis doit contenir au moins un produit.");
            if (cbStatut.SelectedItem == null) throw new Exception("Veuillez sélectionner un statut.");
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            try
            {
                ValiderSaisieDevis();

                currentDevis.Client = (Client)cbClients.SelectedItem;
                currentDevis.Date = dtpDate.Value;

                // --- IMPORTANT : On remplit le libellé pour que GetIdStatut fonctionne dans le DAO ---
                currentDevis.Statut = cbStatut.Text;
                // Et on stocke l'ID au cas où
                if (cbStatut.SelectedValue != null) currentDevis.IdStatut = (int)cbStatut.SelectedValue;

                if (decimal.TryParse(txtRemiseGlobal.Text, out decimal remG)) currentDevis.TauxRemiseGlobal = remG;

                devisService.EnregistrerDevis(currentDevis);
                MessageBox.Show("Enregistré !");
                ChargerDevisGrid();
                btnNouveau_Click(sender, e);
            }
            catch (Exception ex) { MessageBox.Show("Erreur : " + ex.Message); }
        }

        private void btnModifier_Click(object sender, EventArgs e)
        {
            try
            {
                ValiderSaisieDevis();
                if (MessageBox.Show("Modifier ?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    currentDevis.Client = (Client)cbClients.SelectedItem;
                    currentDevis.Date = dtpDate.Value;

                    // --- IMPORTANT ---
                    currentDevis.Statut = cbStatut.Text;
                    if (cbStatut.SelectedValue != null) currentDevis.IdStatut = (int)cbStatut.SelectedValue;

                    if (decimal.TryParse(txtRemiseGlobal.Text, out decimal remG)) currentDevis.TauxRemiseGlobal = remG;

                    devisService.ModifierDevis(currentDevis);
                    MessageBox.Show("Modifié !");
                    ChargerDevisGrid();
                    btnNouveau_Click(sender, e);
                }
            }
            catch (Exception ex) { MessageBox.Show("Erreur : " + ex.Message); }
        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            if (currentDevis != null && currentDevis.Id > 0)
            {
                if (MessageBox.Show("Supprimer ?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    devisService.SupprimerDevis(currentDevis.Id);
                    ChargerDevisGrid();
                    btnNouveau_Click(sender, e);
                }
            }
        }

        // --- NAVIGATION ---
        private void btnProduits_Click(object sender, EventArgs e) { this.Hide(); new ProductListForm().ShowDialog(); this.Show(); ChargerListesDeroulantes(); }
        private void btnClients_Click(object sender, EventArgs e) { this.Hide(); new ClientListForm().ShowDialog(); this.Show(); ChargerListesDeroulantes(); }
        private void btnSynthese_Click(object sender, EventArgs e) { this.Hide(); new SyntheseListForm().ShowDialog(); this.Show(); ChargerListesDeroulantes(); }
    }
}