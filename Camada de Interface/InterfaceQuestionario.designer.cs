namespace ETdA.Camada_de_Interface
{
    partial class InterfaceQuestionario
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
            this.Questionario = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // Questionario
            // 
            this.Questionario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Questionario.Location = new System.Drawing.Point(0, 0);
            this.Questionario.MinimumSize = new System.Drawing.Size(20, 20);
            this.Questionario.Name = "Questionario";
            this.Questionario.Size = new System.Drawing.Size(601, 366);
            this.Questionario.TabIndex = 0;
            // 
            // Interface_Questionario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 366);
            this.Controls.Add(this.Questionario);
            this.Name = "Interface_Questionario";
            this.Text = "Ver Questionário";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser Questionario;
    }
}