namespace STD_magasin
{
    partial class Form1
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
            this.lbl_caisse = new System.Windows.Forms.Label();
            this.lbl_client = new System.Windows.Forms.Label();
            this.scene1 = new STD_magasin.Scene();
            this.SuspendLayout();
            // 
            // lbl_caisse
            // 
            this.lbl_caisse.AutoSize = true;
            this.lbl_caisse.Location = new System.Drawing.Point(12, 9);
            this.lbl_caisse.Name = "lbl_caisse";
            this.lbl_caisse.Size = new System.Drawing.Size(53, 13);
            this.lbl_caisse.TabIndex = 0;
            this.lbl_caisse.Text = "lbl_caisse";
            // 
            // lbl_client
            // 
            this.lbl_client.AutoSize = true;
            this.lbl_client.Location = new System.Drawing.Point(12, 31);
            this.lbl_client.Name = "lbl_client";
            this.lbl_client.Size = new System.Drawing.Size(48, 13);
            this.lbl_client.TabIndex = 1;
            this.lbl_client.Text = "lbl_client";
            // 
            // scene1
            // 
            this.scene1.Location = new System.Drawing.Point(12, 47);
            this.scene1.Name = "scene1";
            this.scene1.Size = new System.Drawing.Size(852, 469);
            this.scene1.TabIndex = 2;
            this.scene1.Text = "scene1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(865, 520);
            this.Controls.Add(this.scene1);
            this.Controls.Add(this.lbl_client);
            this.Controls.Add(this.lbl_caisse);
            this.Name = "Form1";
            this.Text = "STD_magasin";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_caisse;
        private System.Windows.Forms.Label lbl_client;
        private Scene scene;
        private Scene scene1;
    }
}

