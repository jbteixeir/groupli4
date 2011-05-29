namespace ETdA.Camada_de_Interface
{
    partial class Interface_CheckList
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
            this.CheckList = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // CheckList
            // 
            this.CheckList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CheckList.Location = new System.Drawing.Point(0, 0);
            this.CheckList.MinimumSize = new System.Drawing.Size(20, 20);
            this.CheckList.Name = "CheckList";
            this.CheckList.Size = new System.Drawing.Size(594, 411);
            this.CheckList.TabIndex = 0;
            this.CheckList.Url = new System.Uri("http://jbteixeir.dyndns.org:54749/ETdA/Default.aspx?form=CL&usr=test_user&anl=1&p" +
                    "rj=1", System.UriKind.Absolute);
            this.CheckList.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.CheckList_DocumentCompleted);
            // 
            // Interface_CheckList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 411);
            this.Controls.Add(this.CheckList);
            this.Name = "Interface_CheckList";
            this.Text = "Interface_CheckList";
            this.Load += new System.EventHandler(this.Interface_CheckList_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser CheckList;
    }
}