namespace ETdAnalyser.CamadaInterface
{
    partial class InterfaceCheckList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InterfaceCheckList));
            this.CheckList = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // CheckList
            // 
            resources.ApplyResources(this.CheckList, "CheckList");
            this.CheckList.MinimumSize = new System.Drawing.Size(20, 20);
            this.CheckList.Name = "CheckList";
            this.CheckList.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.CheckList_DocumentCompleted);
            // 
            // InterfaceCheckList
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CheckList);
            this.Name = "InterfaceCheckList";
            this.Load += new System.EventHandler(this.Interface_CheckList_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser CheckList;
    }
}