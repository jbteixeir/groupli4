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
            this.painelHeaderZonas = new System.Windows.Forms.Panel();
            this.TituloZonas = new System.Windows.Forms.Label();
            this.painelGeralZonas = new System.Windows.Forms.Panel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.painelHeaderZonas.SuspendLayout();
            this.painelGeralZonas.SuspendLayout();
            this.SuspendLayout();
            // 
            // painelHeaderZonas
            // 
            this.painelHeaderZonas.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.painelHeaderZonas.Controls.Add(this.TituloZonas);
            this.painelHeaderZonas.Dock = System.Windows.Forms.DockStyle.Top;
            this.painelHeaderZonas.Location = new System.Drawing.Point(0, 0);
            this.painelHeaderZonas.Name = "painelHeaderZonas";
            this.painelHeaderZonas.Size = new System.Drawing.Size(925, 45);
            this.painelHeaderZonas.TabIndex = 0;
            // 
            // TituloZonas
            // 
            this.TituloZonas.AutoSize = true;
            this.TituloZonas.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TituloZonas.Location = new System.Drawing.Point(13, 9);
            this.TituloZonas.Name = "TituloZonas";
            this.TituloZonas.Size = new System.Drawing.Size(208, 25);
            this.TituloZonas.TabIndex = 1;
            this.TituloZonas.Text = "Zonas / Atividades";
            // 
            // painelGeralZonas
            // 
            this.painelGeralZonas.AutoScroll = true;
            this.painelGeralZonas.Controls.Add(this.checkBox1);
            this.painelGeralZonas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.painelGeralZonas.Location = new System.Drawing.Point(0, 45);
            this.painelGeralZonas.Name = "painelGeralZonas";
            this.painelGeralZonas.Size = new System.Drawing.Size(925, 340);
            this.painelGeralZonas.TabIndex = 1;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(310, 109);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(80, 17);
            this.checkBox1.TabIndex = 0;
            
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Checked = true;
                this.checkBox1.Text = "checkado!";
            // 
            // Interface_Relatorio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(925, 385);
            this.Controls.Add(this.painelGeralZonas);
            this.Controls.Add(this.painelHeaderZonas);
            this.Name = "Interface_Relatorio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Interface_Relatorio";
            this.painelHeaderZonas.ResumeLayout(false);
            this.painelHeaderZonas.PerformLayout();
            this.painelGeralZonas.ResumeLayout(false);
            this.painelGeralZonas.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel painelHeaderZonas;
        /*
        private System.Windows.Forms.Panel painelZona;
        private System.Windows.Forms.Panel painelGeralItens;
        private System.Windows.Forms.Panel painelItem;
        private System.Windows.Forms.Label labelItem;
        private System.Windows.Forms.Label labelZona;
         */
        private System.Windows.Forms.Label TituloZonas;
        private System.Windows.Forms.Panel painelGeralZonas;
        private System.Windows.Forms.CheckBox checkBox1;

    }
}