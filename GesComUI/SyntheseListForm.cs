using GesCom.BLL;
using GesCom.DAL;
using GesCom.Models;
using GesCom.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GesCom.UI
{
    public partial class SyntheseListForm : Form
    {
        private StatistiquesService statsService = new StatistiquesService();

        // Composants graphiques
        private DataGridView dgvSynthese;
        private DateTimePicker dtpDebut;
        private DateTimePicker dtpFin;
        private Button btnFiltrer;
        // private Button btnRetour; // Supprimé
        private Label lblTotalGeneral;

        public SyntheseListForm()
        {
            InitializeComponentManuel();

            // Initialisation des dates (ex: Depuis le début de l'année jusqu'à aujourd'hui)
            dtpDebut.Value = new DateTime(DateTime.Now.Year, 1, 1);
            dtpFin.Value = DateTime.Now;

            ChargerSynthese();
        }

        private void InitializeComponentManuel()
        {
            this.Size = new Size(1100, 650);
            this.Text = "Synthèse et Statistiques Clients";
            this.StartPosition = FormStartPosition.CenterScreen;

            // --- 1. BOUTONS DE NAVIGATION ---
            // J'ai réaligné les boutons vers la gauche (X=20) puisque le bouton Retour n'existe plus.

            Button btnProduits = new Button
            {
                Text = "Produits",
                Location = new Point(20, 10), // Début à 20
                Size = new Size(100, 30),
                BackColor = Color.AliceBlue
            };
            btnProduits.Click += (s, e) => { this.Hide(); new ProductListForm().ShowDialog(); this.Show(); };
            this.Controls.Add(btnProduits);

            Button btnDevis = new Button
            {
                Text = "Devis",
                Location = new Point(130, 10), // 20 + 100 + 10
                Size = new Size(100, 30),
                BackColor = Color.AliceBlue
            };
            btnDevis.Click += (s, e) => { this.Hide(); new DevisListForm().ShowDialog(); this.Show(); };
            this.Controls.Add(btnDevis);

            Button btnClients = new Button
            {
                Text = "Clients",
                Location = new Point(240, 10), // 130 + 100 + 10
                Size = new Size(100, 30),
                BackColor = Color.AliceBlue
            };
            btnClients.Click += (s, e) => { this.Hide(); new ClientListForm().ShowDialog(); this.Show(); };
            this.Controls.Add(btnClients);

            // --- 2. ZONE DE FILTRES ---
            GroupBox grpFiltres = new GroupBox { Text = "Période d'analyse", Location = new Point(20, 50), Size = new Size(1040, 80) };
            this.Controls.Add(grpFiltres);

            Label lblDu = new Label { Text = "Du :", Location = new Point(30, 35), AutoSize = true };
            grpFiltres.Controls.Add(lblDu);

            dtpDebut = new DateTimePicker { Location = new Point(60, 30), Width = 200, Format = DateTimePickerFormat.Short };
            grpFiltres.Controls.Add(dtpDebut);

            Label lblAu = new Label { Text = "Au :", Location = new Point(280, 35), AutoSize = true };
            grpFiltres.Controls.Add(lblAu);

            dtpFin = new DateTimePicker { Location = new Point(310, 30), Width = 200, Format = DateTimePickerFormat.Short };
            grpFiltres.Controls.Add(dtpFin);

            btnFiltrer = new Button { Text = "Actualiser", Location = new Point(550, 28), Size = new Size(120, 30), BackColor = Color.LightGreen };
            btnFiltrer.Click += new EventHandler(btnFiltrer_Click);
            grpFiltres.Controls.Add(btnFiltrer);

            // --- 3. TABLEAU DE SYNTHÈSE ---
            dgvSynthese = new DataGridView();
            dgvSynthese.Location = new Point(20, 150);
            dgvSynthese.Size = new Size(1040, 400);
            dgvSynthese.ReadOnly = true;
            dgvSynthese.AllowUserToAddRows = false;
            dgvSynthese.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSynthese.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.Controls.Add(dgvSynthese);

            ConfigGrille(); // Définir les colonnes

            // --- 4. TOTAL GÉNÉRAL EN BAS ---
            lblTotalGeneral = new Label { Text = "", Location = new Point(20, 560), AutoSize = true, Font = new Font(this.Font, FontStyle.Bold) };
            this.Controls.Add(lblTotalGeneral);
        }

        private void ConfigGrille()
        {
            dgvSynthese.AutoGenerateColumns = false;
            dgvSynthese.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Client", DataPropertyName = "NomClient" });
            dgvSynthese.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Nb Devis", DataPropertyName = "NbrDevis" });
            dgvSynthese.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Acceptés", DataPropertyName = "NbrDevisAcceptes" });
            dgvSynthese.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "% Acceptés", DataPropertyName = "PourcentageAcceptes" });
            dgvSynthese.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "En Attente", DataPropertyName = "NbrDevisAttente" });
            dgvSynthese.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "% Attente", DataPropertyName = "PourcentageAttente" });
            dgvSynthese.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Refusés", DataPropertyName = "NbrDevisRefuses" });
            dgvSynthese.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "% Refusés", DataPropertyName = "PourcentageRefuses" });
            dgvSynthese.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "CA (HT) Accepté", DataPropertyName = "MontantTotalHT", DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" } });
        }

        private void btnFiltrer_Click(object sender, EventArgs e)
        {
            ChargerSynthese();
        }

        private void ChargerSynthese()
        {
            try
            {
                var liste = statsService.ObtenirSynthese(dtpDebut.Value, dtpFin.Value);
                dgvSynthese.DataSource = liste;

                decimal totalCA = 0;
                foreach (var item in liste) totalCA += item.MontantTotalHT;
                lblTotalGeneral.Text = $"Chiffre d'Affaires Total (Devis Acceptés) sur la période : {totalCA:C2}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}