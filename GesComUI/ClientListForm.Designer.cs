namespace GesCom.UI
{
    partial class ClientListForm
    {
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Méthode requise pour la prise en charge du Designer.
        /// NE PAS MODIFIER le contenu directement.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.SuspendLayout();
            // 
            // DevisListForm
            // 
            this.ClientSize = new System.Drawing.Size(1100, 650);
            this.Name = "DevisListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestion des Devis";
            this.ResumeLayout(false);
        }

        #endregion
    }
}
