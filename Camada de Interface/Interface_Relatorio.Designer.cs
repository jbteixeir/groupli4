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
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.painelHeaderZonas.SuspendLayout();
            this.SuspendLayout();
            // 
            // painelHeaderZonas
            // 
            this.painelHeaderZonas.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.painelHeaderZonas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.painelHeaderZonas.Controls.Add(this.label1);
            this.painelHeaderZonas.Controls.Add(this.TituloZonas);
            this.painelHeaderZonas.Dock = System.Windows.Forms.DockStyle.Top;
            this.painelHeaderZonas.Location = new System.Drawing.Point(0, 0);
            this.painelHeaderZonas.Name = "painelHeaderZonas";
            this.painelHeaderZonas.Size = new System.Drawing.Size(925, 88);
            this.painelHeaderZonas.TabIndex = 0;
            // 
            // TituloZonas
            // 
            this.TituloZonas.AutoSize = true;
            this.TituloZonas.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TituloZonas.Location = new System.Drawing.Point(42, 42);
            this.TituloZonas.Name = "TituloZonas";
            this.TituloZonas.Size = new System.Drawing.Size(208, 25);
            this.TituloZonas.TabIndex = 1;
            this.TituloZonas.Text = "Zonas / Atividades";
            // 
            // painelGeralZonas
            // 
            this.painelGeralZonas.AutoScroll = true;
            this.painelGeralZonas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.painelGeralZonas.Location = new System.Drawing.Point(0, 88);
            this.painelGeralZonas.Name = "painelGeralZonas";
            this.painelGeralZonas.Size = new System.Drawing.Size(925, 297);
            this.painelGeralZonas.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 29);
            this.label1.TabIndex = 2;
            this.label1.Text = "Relatório";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 88);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(35, 297);
            this.panel1.TabIndex = 2;
            // 
            // Interface_Relatorio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(925, 385);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.painelGeralZonas);
            this.Controls.Add(this.painelHeaderZonas);
            this.Name = "Interface_Relatorio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Interface_Relatorio";
            this.painelHeaderZonas.ResumeLayout(false);
            this.painelHeaderZonas.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel painelHeaderZonas;
        private System.Windows.Forms.Label TituloZonas;
        private System.Windows.Forms.Panel painelGeralZonas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;

    }
}