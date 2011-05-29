namespace ETdA.Camada_de_Interface
{
    partial class Interface_Relatorio
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.painelHeaderlZonas = new System.Windows.Forms.Panel();
            this.painelZona = new System.Windows.Forms.Panel();
            this.painelGeralItens = new System.Windows.Forms.Panel();
            this.painelItem = new System.Windows.Forms.Panel();
            this.labelItem = new System.Windows.Forms.Label();
            this.labelZona = new System.Windows.Forms.Label();
            this.TituloZonas = new System.Windows.Forms.Label();
            this.painelGeralZonas = new System.Windows.Forms.Panel();
            this.painelHeaderlZonas.SuspendLayout();
            this.painelZona.SuspendLayout();
            this.painelGeralItens.SuspendLayout();
            this.painelItem.SuspendLayout();
            this.painelGeralZonas.SuspendLayout();
            this.SuspendLayout();
            // 
            // painelHeaderlZonas
            // 
            this.painelHeaderlZonas.Controls.Add(this.TituloZonas);
            this.painelHeaderlZonas.Dock = System.Windows.Forms.DockStyle.Top;
            this.painelHeaderlZonas.Location = new System.Drawing.Point(0, 0);
            this.painelHeaderlZonas.Name = "painelHeaderlZonas";
            this.painelHeaderlZonas.Size = new System.Drawing.Size(943, 45);
            this.painelHeaderlZonas.TabIndex = 0;
            // 
            // painelZona
            // 
            this.painelZona.Controls.Add(this.painelGeralItens);
            this.painelZona.Controls.Add(this.labelZona);
            this.painelZona.Dock = System.Windows.Forms.DockStyle.Top;
            this.painelZona.Location = new System.Drawing.Point(0, 0);
            this.painelZona.Name = "painelZona";
            this.painelZona.Size = new System.Drawing.Size(943, 215);
            this.painelZona.TabIndex = 1;
            // 
            // painelGeralItens
            // 
            this.painelGeralItens.Controls.Add(this.painelItem);
            this.painelGeralItens.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.painelGeralItens.Location = new System.Drawing.Point(0, 45);
            this.painelGeralItens.Name = "painelGeralItens";
            this.painelGeralItens.Size = new System.Drawing.Size(943, 170);
            this.painelGeralItens.TabIndex = 1;
            // 
            // painelItem
            // 
            this.painelItem.Controls.Add(this.labelItem);
            this.painelItem.Dock = System.Windows.Forms.DockStyle.Top;
            this.painelItem.Location = new System.Drawing.Point(0, 0);
            this.painelItem.Name = "painelItem";
            this.painelItem.Size = new System.Drawing.Size(943, 167);
            this.painelItem.TabIndex = 0;
            // 
            // labelItem
            // 
            this.labelItem.AutoSize = true;
            this.labelItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelItem.Location = new System.Drawing.Point(24, 9);
            this.labelItem.Name = "labelItem";
            this.labelItem.Size = new System.Drawing.Size(57, 20);
            this.labelItem.TabIndex = 0;
            this.labelItem.Text = "label1";
            // 
            // labelZona
            // 
            this.labelZona.AutoSize = true;
            this.labelZona.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelZona.Location = new System.Drawing.Point(24, 0);
            this.labelZona.Name = "labelZona";
            this.labelZona.Size = new System.Drawing.Size(66, 24);
            this.labelZona.TabIndex = 0;
            this.labelZona.Text = "label2";
            // 
            // TituloZonas
            // 
            this.TituloZonas.AutoSize = true;
            this.TituloZonas.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TituloZonas.Location = new System.Drawing.Point(13, 9);
            this.TituloZonas.Name = "TituloZonas";
            this.TituloZonas.Size = new System.Drawing.Size(77, 25);
            this.TituloZonas.TabIndex = 1;
            this.TituloZonas.Text = "Zonas";
            // 
            // painelGeralZonas
            // 
            this.painelGeralZonas.Controls.Add(this.painelZona);
            this.painelGeralZonas.Dock = System.Windows.Forms.DockStyle.Top;
            this.painelGeralZonas.Location = new System.Drawing.Point(0, 45);
            this.painelGeralZonas.Name = "painelGeralZonas";
            this.painelGeralZonas.Size = new System.Drawing.Size(943, 218);
            this.painelGeralZonas.TabIndex = 1;
            // 
            // Interface_Relatorio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(943, 356);
            this.Controls.Add(this.painelGeralZonas);
            this.Controls.Add(this.painelHeaderlZonas);
            this.Name = "Interface_Relatorio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Interface_Relatorio";
            this.painelHeaderlZonas.ResumeLayout(false);
            this.painelHeaderlZonas.PerformLayout();
            this.painelZona.ResumeLayout(false);
            this.painelZona.PerformLayout();
            this.painelGeralItens.ResumeLayout(false);
            this.painelItem.ResumeLayout(false);
            this.painelItem.PerformLayout();
            this.painelGeralZonas.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel painelHeaderlZonas;
        private System.Windows.Forms.Panel painelZona;
        private System.Windows.Forms.Panel painelGeralItens;
        private System.Windows.Forms.Panel painelItem;
        private System.Windows.Forms.Label labelItem;
        private System.Windows.Forms.Label labelZona;
        private System.Windows.Forms.Label TituloZonas;
        private System.Windows.Forms.Panel painelGeralZonas;







    }
}