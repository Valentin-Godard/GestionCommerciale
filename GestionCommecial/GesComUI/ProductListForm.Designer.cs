using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesCom.UI
{
    partial class ProductListForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvProduits;
        private System.Windows.Forms.GroupBox grpDetails;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.TextBox txtLibelle;
        private System.Windows.Forms.ComboBox cmbCategorie;
        private System.Windows.Forms.NumericUpDown numPrix;
        private System.Windows.Forms.Button btnAjouter;
        private System.Windows.Forms.Button btnModifier;
        private System.Windows.Forms.Button btnSupprimer;
        private System.Windows.Forms.Button btnNouveau;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.Label lblLibelle;
        private System.Windows.Forms.Label lblCategorie;
        private System.Windows.Forms.Label lblPrix;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvProduits = new System.Windows.Forms.DataGridView();
            this.grpDetails = new System.Windows.Forms.GroupBox();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.txtLibelle = new System.Windows.Forms.TextBox();
            this.cmbCategorie = new System.Windows.Forms.ComboBox();
            this.numPrix = new System.Windows.Forms.NumericUpDown();
            this.btnAjouter = new System.Windows.Forms.Button();
            this.btnModifier = new System.Windows.Forms.Button();
            this.btnSupprimer = new System.Windows.Forms.Button();
            this.btnNouveau = new System.Windows.Forms.Button();
            this.lblCode = new System.Windows.Forms.Label();
            this.lblLibelle = new System.Windows.Forms.Label();
            this.lblCategorie = new System.Windows.Forms.Label();
            this.lblPrix = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvProduits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrix)).BeginInit();
            this.grpDetails.SuspendLayout();
            this.SuspendLayout();

            // --- DataGridView ---
            this.dgvProduits.AllowUserToAddRows = false;
            this.dgvProduits.AllowUserToDeleteRows = false;
            this.dgvProduits.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProduits.Location = new System.Drawing.Point(20, 50);
            this.dgvProduits.MultiSelect = false;
            this.dgvProduits.Name = "dgvProduits";
            this.dgvProduits.ReadOnly = true;
            this.dgvProduits.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProduits.Size = new System.Drawing.Size(500, 300);
            this.dgvProduits.SelectionChanged += new System.EventHandler(this.dgvProduits_SelectionChanged);

            // --- GroupBox Détails ---
            this.grpDetails.Controls.Add(this.lblCode);
            this.grpDetails.Controls.Add(this.txtCode);
            this.grpDetails.Controls.Add(this.lblLibelle);
            this.grpDetails.Controls.Add(this.txtLibelle);
            this.grpDetails.Controls.Add(this.lblCategorie);
            this.grpDetails.Controls.Add(this.cmbCategorie);
            this.grpDetails.Controls.Add(this.lblPrix);
            this.grpDetails.Controls.Add(this.numPrix);
            this.grpDetails.Controls.Add(this.btnAjouter);
            this.grpDetails.Controls.Add(this.btnModifier);
            this.grpDetails.Controls.Add(this.btnSupprimer);
            this.grpDetails.Location = new System.Drawing.Point(540, 70);
            this.grpDetails.Size = new System.Drawing.Size(300, 280);
            this.grpDetails.Text = "Détails";

            // --- Labels & Champs ---
            this.lblCode.Text = "Code";
            this.lblCode.Location = new System.Drawing.Point(15, 40);
            this.txtCode.Location = new System.Drawing.Point(120, 40);
            this.txtCode.ReadOnly = true;

            this.lblLibelle.Text = "Libellé";
            this.lblLibelle.Location = new System.Drawing.Point(15, 80);
            this.txtLibelle.Location = new System.Drawing.Point(120, 80);

            this.lblCategorie.Text = "Catégorie";
            this.lblCategorie.Location = new System.Drawing.Point(15, 120);
            this.cmbCategorie.Location = new System.Drawing.Point(120, 120);

            this.lblPrix.Text = "Prix HT";
            this.lblPrix.Location = new System.Drawing.Point(15, 160);
            this.numPrix.Location = new System.Drawing.Point(120, 160);
            this.numPrix.Maximum = 10000;
            this.numPrix.DecimalPlaces = 2;

            // --- Boutons ---
            this.btnAjouter.Text = "Ajouter";
            this.btnAjouter.Location = new System.Drawing.Point(20, 210);
            this.btnAjouter.Click += new System.EventHandler(this.btnAjouter_Click);

            this.btnModifier.Text = "Modifier";
            this.btnModifier.Location = new System.Drawing.Point(110, 210);
            this.btnModifier.Click += new System.EventHandler(this.btnModifier_Click);

            this.btnSupprimer.Text = "Supprimer";
            this.btnSupprimer.Location = new System.Drawing.Point(200, 210);
            this.btnSupprimer.Click += new System.EventHandler(this.btnSupprimer_Click);

            this.btnNouveau.Text = "Nouveau";
            this.btnNouveau.Location = new System.Drawing.Point(550, 20);
            this.btnNouveau.Click += new System.EventHandler(this.btnNouveau_Click);

            // --- Formulaire principal ---
            this.ClientSize = new System.Drawing.Size(880, 400);
            this.Controls.Add(this.dgvProduits);
            this.Controls.Add(this.grpDetails);
            this.Controls.Add(this.btnNouveau);
            this.Text = "Gestion commerciale - Produits";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            ((System.ComponentModel.ISupportInitialize)(this.dgvProduits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrix)).EndInit();
            this.grpDetails.ResumeLayout(false);
            this.grpDetails.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}

