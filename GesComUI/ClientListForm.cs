using GesCom.BLL;
using GesCom.Models;
using GesComModels;
using System;
using System.Drawing;
using System.Text.RegularExpressions; // Indispensable pour la validation
using System.Windows.Forms;

namespace GesCom.UI
{
    public partial class ClientListForm : Form
    {
        private ClientService clientService = new ClientService();

        // Composants
        private DataGridView dgvClients;
        private GroupBox grpDetails;

        // Infos Générales
        private TextBox txtNom, txtEmail, txtTel, txtFax;

        // Adresse Livraison
        private TextBox txtNumRue, txtRue, txtVille, txtCP;

        // Adresse Facturation
        private TextBox txtNumRueFac, txtRueFac, txtVilleFac, txtCPFac;

        // Boutons
        private Button btnSynthese, btnDevis, btnProduits, btnAjouter, btnModifier, btnSupprimer, btnNouveau;

        public ClientListForm()
        {
            InitializeComponentManuel();
            ChargerClients();
            // Etat initial
            btnNouveau_Click(null, null);
        }

        // --- VALIDATION LOCALE (REGEX & CHAMPS OBLIGATOIRES) ---
        private void ValiderClient(Client client)
        {
            if (client == null) throw new Exception("Client invalide.");
            if (string.IsNullOrWhiteSpace(client.Nom)) throw new Exception("Le nom est obligatoire.");
            if (string.IsNullOrWhiteSpace(client.Email)) throw new Exception("L'email est obligatoire.");
            if (string.IsNullOrWhiteSpace(client.Telephone)) throw new Exception("Le téléphone est obligatoire.");

            // Email
            if (!Regex.IsMatch(client.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new Exception("Email non valide.");

            // Téléphone
            if (!Regex.IsMatch(client.Telephone, @"^[0-9\+\-\s\(\)]{6,20}$"))
                throw new Exception("Téléphone non valide.");

            // Fax (si rempli)
            if (!string.IsNullOrWhiteSpace(client.Fax) && !Regex.IsMatch(client.Fax, @"^[0-9\+\-\s\(\)]{6,20}$"))
                throw new Exception("Fax non valide.");
        }

        private void InitializeComponentManuel()
        {
            this.Size = new Size(1000, 750);
            this.Text = "Gestion des Clients";
            this.StartPosition = FormStartPosition.CenterScreen;

            // --- NAVIGATION ---
            btnProduits = new Button { Text = "Produits", Location = new Point(20, 10), Size = new Size(100, 30), BackColor = Color.AliceBlue };
            btnProduits.Click += (s, e) => { this.Hide(); new ProductListForm().ShowDialog(); this.Show(); };
            this.Controls.Add(btnProduits);

            btnDevis = new Button { Text = "Devis", Location = new Point(130, 10), Size = new Size(100, 30), BackColor = Color.AliceBlue };
            btnDevis.Click += (s, e) => { this.Hide(); new DevisListForm().ShowDialog(); this.Show(); };
            this.Controls.Add(btnDevis);

            btnSynthese = new Button { Text = "Synthèse", Location = new Point(240, 10), Size = new Size(100, 30), BackColor = Color.AliceBlue };
            btnSynthese.Click += (s, e) => { this.Hide(); new SyntheseListForm().ShowDialog(); this.Show(); };
            this.Controls.Add(btnSynthese);

            // --- GRILLE ---
            dgvClients = new DataGridView();
            dgvClients.Location = new Point(20, 50);
            dgvClients.Size = new Size(550, 630);
            dgvClients.ReadOnly = true;
            dgvClients.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClients.MultiSelect = false;
            dgvClients.RowHeadersVisible = false;
            dgvClients.SelectionChanged += dgvClients_SelectionChanged;
            this.Controls.Add(dgvClients);

            // --- DETAILS ---
            grpDetails = new GroupBox { Text = "Détails Client", Location = new Point(590, 50), Size = new Size(350, 630) };
            this.Controls.Add(grpDetails);

            btnNouveau = new Button { Text = "Nouveau", Location = new Point(20, 25), Size = new Size(80, 25) };
            btnNouveau.Click += btnNouveau_Click;
            grpDetails.Controls.Add(btnNouveau);

            int y = 60; int spacing = 30;
            AddLabelAndBox("Nom :", y, out txtNom); y += spacing;
            AddLabelAndBox("Email :", y, out txtEmail); y += spacing;
            AddLabelAndBox("Tél :", y, out txtTel); y += spacing;
            AddLabelAndBox("Fax :", y, out txtFax); y += spacing + 10;

            Label lblLiv = new Label { Text = "--- Livraison ---", Location = new Point(20, y), AutoSize = true, Font = new Font(this.Font, FontStyle.Bold), ForeColor = Color.DarkBlue };
            grpDetails.Controls.Add(lblLiv); y += 25;
            AddLabelAndBox("N° :", y, out txtNumRue); y += spacing;
            AddLabelAndBox("Rue :", y, out txtRue); y += spacing;
            AddLabelAndBox("CP :", y, out txtCP); y += spacing;
            AddLabelAndBox("Ville :", y, out txtVille); y += spacing + 10;

            Label lblFac = new Label { Text = "--- Facturation ---", Location = new Point(20, y), AutoSize = true, Font = new Font(this.Font, FontStyle.Bold), ForeColor = Color.DarkRed };
            grpDetails.Controls.Add(lblFac); y += 25;
            AddLabelAndBox("N° :", y, out txtNumRueFac); y += spacing;
            AddLabelAndBox("Rue :", y, out txtRueFac); y += spacing;
            AddLabelAndBox("CP :", y, out txtCPFac); y += spacing;
            AddLabelAndBox("Ville :", y, out txtVilleFac); y += spacing + 20;

            int btnY = y + 10;
            btnAjouter = new Button { Text = "Ajouter", Location = new Point(20, btnY), Size = new Size(90, 35), BackColor = Color.LightGreen };
            btnAjouter.Click += btnAjouter_Click;
            grpDetails.Controls.Add(btnAjouter);

            btnModifier = new Button { Text = "Modifier", Location = new Point(120, btnY), Size = new Size(90, 35) };
            btnModifier.Click += btnModifier_Click;
            grpDetails.Controls.Add(btnModifier);

            btnSupprimer = new Button { Text = "Supprimer", Location = new Point(220, btnY), Size = new Size(90, 35), BackColor = Color.Salmon };
            btnSupprimer.Click += btnSupprimer_Click;
            grpDetails.Controls.Add(btnSupprimer);
        }

        private void AddLabelAndBox(string labelText, int y, out TextBox textBox)
        {
            Label lbl = new Label { Text = labelText, Location = new Point(20, y + 3), AutoSize = true };
            grpDetails.Controls.Add(lbl);
            textBox = new TextBox { Location = new Point(100, y), Width = 220 };
            grpDetails.Controls.Add(textBox);
        }

        private void ChargerClients()
        {
            dgvClients.DataSource = null;
            dgvClients.DataSource = clientService.GetClients();
            if (dgvClients.Columns["Id"] != null) dgvClients.Columns["Id"].Visible = false;
        }

        private void dgvClients_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvClients.SelectedRows.Count > 0)
            {
                Client client = (Client)dgvClients.SelectedRows[0].DataBoundItem;
                txtNom.Text = client.Nom;
                txtEmail.Text = client.Email;
                txtTel.Text = client.Telephone;
                txtFax.Text = client.Fax;
                txtNumRue.Text = client.NumeroLivraison;
                txtRue.Text = client.RueLivraison;
                txtCP.Text = client.CpLivraison;
                txtVille.Text = client.VilleLivraison;
                txtNumRueFac.Text = client.NumeroFacturation;
                txtRueFac.Text = client.RueFacturation;
                txtCPFac.Text = client.CpFacturation;
                txtVilleFac.Text = client.VilleFacturation;

                btnAjouter.Enabled = false;
                btnModifier.Enabled = true;
                btnSupprimer.Enabled = true;
            }
        }

        private void btnNouveau_Click(object sender, EventArgs e)
        {
            txtNom.Clear(); txtEmail.Clear(); txtTel.Clear(); txtFax.Clear();
            txtNumRue.Clear(); txtRue.Clear(); txtCP.Clear(); txtVille.Clear();
            txtNumRueFac.Clear(); txtRueFac.Clear(); txtCPFac.Clear(); txtVilleFac.Clear();
            dgvClients.ClearSelection();
            btnAjouter.Enabled = true;
            btnModifier.Enabled = false;
            btnSupprimer.Enabled = false;
            txtNom.Focus();
        }

        // --- AJOUTER ---
        private void btnAjouter_Click(object sender, EventArgs e)
        {
            try
            {
                Client nouveauClient = new Client
                {
                    Nom = txtNom.Text,
                    Email = txtEmail.Text,
                    Telephone = txtTel.Text,
                    Fax = txtFax.Text,
                    NumeroLivraison = txtNumRue.Text,
                    RueLivraison = txtRue.Text,
                    CpLivraison = txtCP.Text,
                    VilleLivraison = txtVille.Text,
                    NumeroFacturation = txtNumRueFac.Text,
                    RueFacturation = txtRueFac.Text,
                    CpFacturation = txtCPFac.Text,
                    VilleFacturation = txtVilleFac.Text
                };

                // 1. Validation de format
                ValiderClient(nouveauClient);

                // 2. Validation Métier (Appel BLL pour info)
                if (clientService.EstDoublon(nouveauClient.Nom, nouveauClient.Email))
                {
                    MessageBox.Show("Doublon détecté (Nom ou Email).", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 3. Action
                clientService.AjouterClient(nouveauClient);
                MessageBox.Show("Client ajouté avec succès !");
                ChargerClients();
                btnNouveau_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // --- MODIFIER ---
        private void btnModifier_Click(object sender, EventArgs e)
        {
            if (dgvClients.SelectedRows.Count == 0) return;

            try
            {
                Client client = (Client)dgvClients.SelectedRows[0].DataBoundItem;
                client.Nom = txtNom.Text;
                client.Email = txtEmail.Text;
                client.Telephone = txtTel.Text;
                client.Fax = txtFax.Text;
                client.NumeroLivraison = txtNumRue.Text;
                client.RueLivraison = txtRue.Text;
                client.CpLivraison = txtCP.Text;
                client.VilleLivraison = txtVille.Text;
                client.NumeroFacturation = txtNumRueFac.Text;
                client.RueFacturation = txtRueFac.Text;
                client.CpFacturation = txtCPFac.Text;
                client.VilleFacturation = txtVilleFac.Text;

                // 1. Validation de format
                ValiderClient(client);

                // 2. Validation Métier
                if (clientService.EstDoublonModification(client.Nom, client.Email, client.Id))
                {
                    MessageBox.Show("Ce nom ou email est déjà pris.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 3. Action
                clientService.ModifierClient(client);
                MessageBox.Show("Client modifié avec succès !");
                ChargerClients();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // --- SUPPRIMER ---
        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            if (dgvClients.SelectedRows.Count == 0) return;
            Client client = (Client)dgvClients.SelectedRows[0].DataBoundItem;

            if (MessageBox.Show($"Supprimer '{client.Nom}' ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    // Validation Métier
                    if (clientService.ADesDevis(client.Id))
                    {
                        MessageBox.Show("Impossible : ce client a des devis.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Action
                    clientService.SupprimerClient(client.Id);
                    MessageBox.Show("Client supprimé.");
                    ChargerClients();
                    btnNouveau_Click(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}