namespace ETdAnalyser.CamadaInterface
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InterfaceQuestionario));
            this.Questionario = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // Questionario
            // 
            resources.ApplyResources(this.Questionario, "Questionario");
            this.Questionario.MinimumSize = new System.Drawing.Size(20, 20);
            this.Questionario.Name = "Questionario";
            // 
            // InterfaceQuestionario
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Questionario);
            this.Name = "InterfaceQuestionario";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser Questionario;
    }
}