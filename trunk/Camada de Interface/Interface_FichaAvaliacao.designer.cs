namespace ETdA.Camada_de_Interface
{
    partial class Interface_FichaAvaliacao
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
            this.FichaAvaliacao = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // FichaAvaliacao
            // 
            this.FichaAvaliacao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FichaAvaliacao.Location = new System.Drawing.Point(0, 0);
            this.FichaAvaliacao.MinimumSize = new System.Drawing.Size(20, 20);
            this.FichaAvaliacao.Name = "FichaAvaliacao";
            this.FichaAvaliacao.Size = new System.Drawing.Size(620, 366);
            this.FichaAvaliacao.TabIndex = 0;
            this.FichaAvaliacao.Url = new System.Uri("http://jbteixeir.dyndns.org:54749/ETdA/Default.aspx?form=FA&usr=test_user&anl=1&p" +
                    "rj=1", System.UriKind.Absolute);
            // 
            // Interface_FichaAvaliacao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 366);
            this.Controls.Add(this.FichaAvaliacao);
            this.Name = "Interface_FichaAvaliacao";
            this.Text = "Ver Ficha de Avaliação";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser FichaAvaliacao;
    }
}