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
            this.label1 = new System.Windows.Forms.Label();
            this.TituloZonas = new System.Windows.Forms.Label();
            this.painelGeralZonas = new System.Windows.Forms.Panel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
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
            this.painelHeaderZonas.Size = new System.Drawing.Size(742, 88);
            this.painelHeaderZonas.TabIndex = 0;
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
            this.painelGeralZonas.Location = new System.Drawing.Point(0, 0);
            this.painelGeralZonas.Name = "painelGeralZonas";
            this.painelGeralZonas.Size = new System.Drawing.Size(742, 373);
            this.painelGeralZonas.TabIndex = 1;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(460, 344);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(193, 17);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "Incluir estatísticas sobre os clientes";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(659, 340);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Concluir";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Interface_Relatorio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(742, 373);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.painelHeaderZonas);
            this.Controls.Add(this.painelGeralZonas);
            this.Name = "Interface_Relatorio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Interface_Relatorio";
            this.painelHeaderZonas.ResumeLayout(false);
            this.painelHeaderZonas.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel painelHeaderZonas;
        private System.Windows.Forms.Label TituloZonas;
        private System.Windows.Forms.Panel painelGeralZonas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button1;

    }
}