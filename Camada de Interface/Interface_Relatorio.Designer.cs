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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Interface_Relatorio));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeViewZonaItem = new System.Windows.Forms.TreeView();
            this.panelZonaItem = new System.Windows.Forms.Panel();
            this.panelrel = new System.Windows.Forms.Panel();
            this.BotaoCancelar = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBoxInsDt = new System.Windows.Forms.CheckBox();
            this.obstb = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panelZonaItem.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            resources.ApplyResources(this.splitContainer1.Panel1, "splitContainer1.Panel1");
            this.splitContainer1.Panel1.Controls.Add(this.treeViewZonaItem);
            // 
            // splitContainer1.Panel2
            // 
            resources.ApplyResources(this.splitContainer1.Panel2, "splitContainer1.Panel2");
            this.splitContainer1.Panel2.Controls.Add(this.panelZonaItem);
            // 
            // treeViewZonaItem
            // 
            resources.ApplyResources(this.treeViewZonaItem, "treeViewZonaItem");
            this.treeViewZonaItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeViewZonaItem.Name = "treeViewZonaItem";
            this.treeViewZonaItem.TabStop = false;
            this.treeViewZonaItem.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.OpenAction);
            // 
            // panelZonaItem
            // 
            resources.ApplyResources(this.panelZonaItem, "panelZonaItem");
            this.panelZonaItem.Controls.Add(this.panelrel);
            this.panelZonaItem.Controls.Add(this.BotaoCancelar);
            this.panelZonaItem.Controls.Add(this.button1);
            this.panelZonaItem.Name = "panelZonaItem";
            // 
            // panelrel
            // 
            resources.ApplyResources(this.panelrel, "panelrel");
            this.panelrel.Name = "panelrel";
            // 
            // BotaoCancelar
            // 
            resources.ApplyResources(this.BotaoCancelar, "BotaoCancelar");
            this.BotaoCancelar.Name = "BotaoCancelar";
            this.BotaoCancelar.UseVisualStyleBackColor = true;
            this.BotaoCancelar.Click += new System.EventHandler(this.BotaoCancelar_Click);
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBoxInsDt
            // 
            resources.ApplyResources(this.checkBoxInsDt, "checkBoxInsDt");
            this.checkBoxInsDt.Name = "checkBoxInsDt";
            this.checkBoxInsDt.CheckedChanged += new System.EventHandler(this.checkBoxInsDt_CheckedChanged);
            // 
            // obstb
            // 
            resources.ApplyResources(this.obstb, "obstb");
            this.obstb.Name = "obstb";
            this.obstb.TextChanged += new System.EventHandler(this.obstb_TextChanged);
            // 
            // Interface_Relatorio
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "Interface_Relatorio";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panelZonaItem.ResumeLayout(false);
            this.panelZonaItem.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeViewZonaItem;
        private System.Windows.Forms.Panel panelZonaItem;
        private System.Windows.Forms.Button BotaoCancelar;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panelrel;
        private System.Windows.Forms.RichTextBox obstb;
        private System.Windows.Forms.CheckBox checkBoxInsDt;
    }
}