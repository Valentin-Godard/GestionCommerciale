namespace GesCom.UI
{
    partial class DevisListForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnProduits = new System.Windows.Forms.Button();
            this.btnClients = new System.Windows.Forms.Button();
            this.btnSynthese = new System.Windows.Forms.Button();
            this.dgvDevis = new System.Windows.Forms.DataGridView();
            this.grpDetails = new System.Windows.Forms.GroupBox();

            // --- Nouveaux Champs ---
            this.lblStatut = new System.Windows.Forms.Label();
            this.cbStatut = new System.Windows.Forms.ComboBox();
            this.lblRemiseGlobal = new System.Windows.Forms.Label();
            this.txtRemiseGlobal = new System.Windows.Forms.TextBox();
            // -----------------------

            this.lblLabProduit = new System.Windows.Forms.Label();
            this.lblLabQuantite = new System.Windows.Forms.Label();
            this.lblLabRemise = new System.Windows.Forms.Label();
            this.lblMontantTVA = new System.Windows.Forms.Label();
            this.lblTauxRemise = new System.Windows.Forms.Label();
            this.lblTauxTVA = new System.Windows.Forms.Label();
            this.lblTotalNet = new System.Windows.Forms.Label();
            this.lblTotalBrut = new System.Windows.Forms.Label();
            this.lblAdresseClient = new System.Windows.Forms.Label();
            this.btnNouveau = new System.Windows.Forms.Button();
            this.lblCl = new System.Windows.Forms.Label();
            this.cbClients = new System.Windows.Forms.ComboBox();
            this.lblDt = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.grpLigne = new System.Windows.Forms.GroupBox();
            this.cbProduits = new System.Windows.Forms.ComboBox();
            this.txtQuantite = new System.Windows.Forms.TextBox();
            this.txtRemiseLigne = new System.Windows.Forms.TextBox();
            this.btnAjouterLigne = new System.Windows.Forms.Button();
            this.btnSupprimerLigne = new System.Windows.Forms.Button();
            this.dgvLignes = new System.Windows.Forms.DataGridView();
            this.lblTTC = new System.Windows.Forms.Label();
            this.btnAjouter = new System.Windows.Forms.Button();
            this.btnModifier = new System.Windows.Forms.Button();
            this.btnSupprimer = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);

            ((System.ComponentModel.ISupportInitialize)(this.dgvDevis)).BeginInit();
            this.grpDetails.SuspendLayout();
            this.grpLigne.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLignes)).BeginInit();
            this.SuspendLayout();

            // btnProduits
            this.btnProduits.Location = new System.Drawing.Point(20, 10);
            this.btnProduits.Name = "btnProduits";
            this.btnProduits.Size = new System.Drawing.Size(100, 30);
            this.btnProduits.Text = "Produits";
            this.btnProduits.UseVisualStyleBackColor = true;
            this.btnProduits.Click += new System.EventHandler(this.btnProduits_Click);

            // btnClients
            this.btnClients.Location = new System.Drawing.Point(130, 10);
            this.btnClients.Name = "btnClients";
            this.btnClients.Size = new System.Drawing.Size(100, 30);
            this.btnClients.Text = "Clients";
            this.btnClients.UseVisualStyleBackColor = true;
            this.btnClients.Click += new System.EventHandler(this.btnClients_Click);

            // dgvDevis
            this.dgvDevis.Location = new System.Drawing.Point(20, 50);
            this.dgvDevis.Size = new System.Drawing.Size(500, 580);
            this.dgvDevis.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDevis.MultiSelect = false;
            this.dgvDevis.ReadOnly = true;
            this.dgvDevis.RowHeadersVisible = false;
            this.dgvDevis.AllowUserToAddRows = false;
            this.dgvDevis.SelectionChanged += new System.EventHandler(this.dgvDevis_SelectionChanged);

            // btnSynthese
            this.btnSynthese.Location = new System.Drawing.Point(240, 10);
            this.btnSynthese.Name = "btnSynthese";
            this.btnSynthese.Size = new System.Drawing.Size(100, 30);
            this.btnSynthese.Text = "Synthèse";
            this.btnSynthese.UseVisualStyleBackColor = true;
            this.btnSynthese.Click += new System.EventHandler(this.btnSynthese_Click);

            // grpDetails
            this.grpDetails.Location = new System.Drawing.Point(540, 50);
            this.grpDetails.Size = new System.Drawing.Size(520, 580);
            this.grpDetails.Text = "Détails du Devis";

            // Ajout des contrôles au groupe
            this.grpDetails.Controls.Add(this.lblStatut);
            this.grpDetails.Controls.Add(this.cbStatut);
            this.grpDetails.Controls.Add(this.lblRemiseGlobal);
            this.grpDetails.Controls.Add(this.txtRemiseGlobal);

            this.grpDetails.Controls.Add(this.lblMontantTVA);
            this.grpDetails.Controls.Add(this.lblTauxRemise);
            this.grpDetails.Controls.Add(this.lblTauxTVA);
            this.grpDetails.Controls.Add(this.lblTotalNet);
            this.grpDetails.Controls.Add(this.lblTotalBrut);
            this.grpDetails.Controls.Add(this.lblAdresseClient);
            this.grpDetails.Controls.Add(this.btnNouveau);
            this.grpDetails.Controls.Add(this.lblCl);
            this.grpDetails.Controls.Add(this.cbClients);
            this.grpDetails.Controls.Add(this.lblDt);
            this.grpDetails.Controls.Add(this.dtpDate);
            this.grpDetails.Controls.Add(this.grpLigne);
            this.grpDetails.Controls.Add(this.dgvLignes);
            this.grpDetails.Controls.Add(this.lblTTC);
            this.grpDetails.Controls.Add(this.btnAjouter);
            this.grpDetails.Controls.Add(this.btnModifier);
            this.grpDetails.Controls.Add(this.btnSupprimer);

            // btnNouveau
            this.btnNouveau.Location = new System.Drawing.Point(20, 25);
            this.btnNouveau.Size = new System.Drawing.Size(100, 30);
            this.btnNouveau.Text = "Nouveau";
            this.btnNouveau.Click += new System.EventHandler(this.btnNouveau_Click);

            // LIGNE 1 : Client + Date
            this.lblCl.Location = new System.Drawing.Point(20, 70);
            this.lblCl.Text = "Client :";
            this.lblCl.AutoSize = true;

            this.cbClients.Location = new System.Drawing.Point(80, 67);
            this.cbClients.Size = new System.Drawing.Size(200, 21);
            this.cbClients.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbClients.SelectedIndexChanged += new System.EventHandler(this.CbClients_SelectedIndexChanged);

            this.lblDt.Location = new System.Drawing.Point(300, 70);
            this.lblDt.Text = "Date :";
            this.lblDt.AutoSize = true;

            this.dtpDate.Location = new System.Drawing.Point(350, 67);
            this.dtpDate.Size = new System.Drawing.Size(150, 20);
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;

            // Adresse
            this.lblAdresseClient.Location = new System.Drawing.Point(80, 95);
            this.lblAdresseClient.Size = new System.Drawing.Size(400, 20);
            this.lblAdresseClient.ForeColor = System.Drawing.Color.Gray;

            // LIGNE 2 : Statut + Remise Globale
            int yStatut = 125;
            this.lblStatut.Location = new System.Drawing.Point(20, yStatut);
            this.lblStatut.Text = "Statut :";
            this.lblStatut.AutoSize = true;

            this.cbStatut.Location = new System.Drawing.Point(80, yStatut - 3);
            this.cbStatut.Size = new System.Drawing.Size(150, 21);
            this.cbStatut.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            // On ajoute les statuts possibles
            this.cbStatut.Items.AddRange(new object[] { "En attente", "Accepté", "Refusé", "Facturé" });

            this.lblRemiseGlobal.Location = new System.Drawing.Point(300, yStatut);
            this.lblRemiseGlobal.Text = "Remise Globale % :";
            this.lblRemiseGlobal.AutoSize = true;

            this.txtRemiseGlobal.Location = new System.Drawing.Point(410, yStatut - 3);
            this.txtRemiseGlobal.Size = new System.Drawing.Size(50, 20);
            this.txtRemiseGlobal.Text = "0";
            // Événement pour recalculer quand on change la remise globale
            this.txtRemiseGlobal.TextChanged += new System.EventHandler(this.txtRemiseGlobal_TextChanged);

            // --- ZONE AJOUT PRODUIT ---
            this.grpLigne.Location = new System.Drawing.Point(20, 160); // Décalé
            this.grpLigne.Size = new System.Drawing.Size(480, 90);
            this.grpLigne.Text = "Ajouter un produit";

            this.grpLigne.Controls.Add(this.lblLabProduit);
            this.grpLigne.Controls.Add(this.lblLabQuantite);
            this.grpLigne.Controls.Add(this.lblLabRemise);
            this.grpLigne.Controls.Add(this.cbProduits);
            this.grpLigne.Controls.Add(this.txtQuantite);
            this.grpLigne.Controls.Add(this.txtRemiseLigne);
            this.grpLigne.Controls.Add(this.btnAjouterLigne);
            this.grpLigne.Controls.Add(this.btnSupprimerLigne);

            this.lblLabProduit.AutoSize = true;
            this.lblLabProduit.Location = new System.Drawing.Point(10, 25);
            this.lblLabProduit.Text = "Produit :";

            this.cbProduits.Location = new System.Drawing.Point(10, 45);
            this.cbProduits.Size = new System.Drawing.Size(190, 21);
            this.cbProduits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.lblLabQuantite.AutoSize = true;
            this.lblLabQuantite.Location = new System.Drawing.Point(210, 25);
            this.lblLabQuantite.Text = "Quantité :";

            this.txtQuantite.Location = new System.Drawing.Point(210, 45);
            this.txtQuantite.Size = new System.Drawing.Size(60, 20);
            this.txtQuantite.Text = "1";

            this.lblLabRemise.AutoSize = true;
            this.lblLabRemise.Location = new System.Drawing.Point(280, 25);
            this.lblLabRemise.Text = "Remise (%) :";

            this.txtRemiseLigne.Location = new System.Drawing.Point(280, 45);
            this.txtRemiseLigne.Size = new System.Drawing.Size(60, 20);
            this.txtRemiseLigne.Text = "0";

            this.btnAjouterLigne.Location = new System.Drawing.Point(360, 42);
            this.btnAjouterLigne.Size = new System.Drawing.Size(40, 25);
            this.btnAjouterLigne.Text = "+";
            this.btnAjouterLigne.Click += new System.EventHandler(this.btnAjouterLigne_Click);

            this.btnSupprimerLigne.Location = new System.Drawing.Point(410, 42);
            this.btnSupprimerLigne.Size = new System.Drawing.Size(40, 25);
            this.btnSupprimerLigne.Text = "-";
            this.btnSupprimerLigne.Click += new System.EventHandler(this.btnSupprimerLigne_Click);

            // --- GRILLE LIGNES ---
            this.dgvLignes.Location = new System.Drawing.Point(20, 270); // Décalé
            this.dgvLignes.Size = new System.Drawing.Size(480, 150);
            this.dgvLignes.AllowUserToAddRows = false;
            this.dgvLignes.ReadOnly = true;
            this.dgvLignes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLignes.RowHeadersVisible = false;

            // --- TOTAUX ---
            int yTotaux = 440;

            this.lblTotalBrut.Location = new System.Drawing.Point(20, yTotaux);
            this.lblTotalBrut.AutoSize = true;
            this.lblTotalBrut.Text = "HT Hors Remise : 0.00 €";

            this.lblTauxRemise.Location = new System.Drawing.Point(20, yTotaux + 25);
            this.lblTauxRemise.AutoSize = true;
            this.lblTauxRemise.Text = "Remise Globale : 0 %";

            this.lblTotalNet.Location = new System.Drawing.Point(20, yTotaux + 50);
            this.lblTotalNet.AutoSize = true;
            this.lblTotalNet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblTotalNet.Text = "HT Avec Remise : 0.00 €";

            this.lblTauxTVA.Location = new System.Drawing.Point(260, yTotaux);
            this.lblTauxTVA.AutoSize = true;
            this.lblTauxTVA.Text = "Taux TVA : 20 %";

            this.lblMontantTVA.Location = new System.Drawing.Point(260, yTotaux + 25);
            this.lblMontantTVA.AutoSize = true;
            this.lblMontantTVA.Text = "Montant TVA : 0.00 €";

            this.lblTTC.Location = new System.Drawing.Point(260, yTotaux + 55);
            this.lblTTC.AutoSize = true;
            this.lblTTC.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.lblTTC.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTTC.Text = "TOTAL TTC : 0.00 €";

            // --- BOUTONS ACTIONS ---
            int yBtn = 540;
            this.btnAjouter.Location = new System.Drawing.Point(20, yBtn);
            this.btnAjouter.Size = new System.Drawing.Size(140, 40);
            this.btnAjouter.Text = "Enregistrer";
            this.btnAjouter.BackColor = System.Drawing.Color.LightGreen;
            this.btnAjouter.Click += new System.EventHandler(this.btnAjouter_Click);

            this.btnModifier.Location = new System.Drawing.Point(170, yBtn);
            this.btnModifier.Size = new System.Drawing.Size(140, 40);
            this.btnModifier.Text = "Mettre à jour";
            this.btnModifier.Click += new System.EventHandler(this.btnModifier_Click);

            this.btnSupprimer.Location = new System.Drawing.Point(320, yBtn);
            this.btnSupprimer.Size = new System.Drawing.Size(140, 40);
            this.btnSupprimer.Text = "Supprimer";
            this.btnSupprimer.BackColor = System.Drawing.Color.Salmon;
            this.btnSupprimer.Click += new System.EventHandler(this.btnSupprimer_Click);

            // Form
            this.ClientSize = new System.Drawing.Size(1100, 700);
            this.Controls.Add(this.btnProduits);
            this.Controls.Add(this.btnClients);
            this.Controls.Add(this.btnSynthese);
            this.Controls.Add(this.dgvDevis);
            this.Controls.Add(this.grpDetails);
            this.Text = "Gestion des Devis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            ((System.ComponentModel.ISupportInitialize)(this.dgvDevis)).EndInit();
            this.grpDetails.ResumeLayout(false);
            this.grpDetails.PerformLayout();
            this.grpLigne.ResumeLayout(false);
            this.grpLigne.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLignes)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        // Boutons Nav
        private System.Windows.Forms.Button btnProduits;
        private System.Windows.Forms.Button btnClients;
        private System.Windows.Forms.Button btnSynthese;

        // Composants principaux
        private System.Windows.Forms.DataGridView dgvDevis;
        private System.Windows.Forms.GroupBox grpDetails;

        // Header Devis
        private System.Windows.Forms.Button btnNouveau;
        private System.Windows.Forms.Label lblCl;
        private System.Windows.Forms.ComboBox cbClients;
        private System.Windows.Forms.Label lblDt;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label lblAdresseClient;

        // Nouveaux champs demandés
        private System.Windows.Forms.Label lblStatut;
        private System.Windows.Forms.ComboBox cbStatut;
        private System.Windows.Forms.Label lblRemiseGlobal;
        private System.Windows.Forms.TextBox txtRemiseGlobal;

        // Zone Ligne (Produit)
        private System.Windows.Forms.GroupBox grpLigne;
        private System.Windows.Forms.Label lblLabProduit;
        private System.Windows.Forms.Label lblLabQuantite;
        private System.Windows.Forms.Label lblLabRemise;
        private System.Windows.Forms.ComboBox cbProduits;
        private System.Windows.Forms.TextBox txtQuantite;
        private System.Windows.Forms.TextBox txtRemiseLigne;
        private System.Windows.Forms.Button btnAjouterLigne;
        private System.Windows.Forms.Button btnSupprimerLigne;

        // Grille Lignes
        private System.Windows.Forms.DataGridView dgvLignes;

        // Totaux
        private System.Windows.Forms.Label lblTotalBrut;
        private System.Windows.Forms.Label lblTotalNet;
        private System.Windows.Forms.Label lblTauxRemise;
        private System.Windows.Forms.Label lblTauxTVA;
        private System.Windows.Forms.Label lblMontantTVA;
        private System.Windows.Forms.Label lblTTC;

        // Actions
        private System.Windows.Forms.Button btnAjouter;
        private System.Windows.Forms.Button btnModifier;
        private System.Windows.Forms.Button btnSupprimer;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}