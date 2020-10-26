namespace STD_magasin
{
    partial class Magasin
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.scene1 = new STD_magasin.Scene();
            this.SuspendLayout();
            // 
            // scene1
            // 
            this.scene1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.scene1.Location = new System.Drawing.Point(0, -1);
            this.scene1.Name = "scene1";
            this.scene1.Size = new System.Drawing.Size(800, 500);
            this.scene1.TabIndex = 2;
            this.scene1.Text = "scene1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 498);
            this.Controls.Add(this.scene1);
            this.MaximumSize = new System.Drawing.Size(815, 537);
            this.MinimumSize = new System.Drawing.Size(815, 537);
            this.Name = "Form1";
            this.Text = "STD_magasin";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private Scene scene;
        private Scene scene1;
    }
}

