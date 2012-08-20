namespace ETdAnalyser.Camada_de_Interface
{
    partial class InterfaceRelatorio
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InterfaceRelatorio));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeViewZonaItem = new System.Windows.Forms.TreeView();
            this.panelZonaItem = new System.Windows.Forms.Panel();
            this.panelrel = new System.Windows.Forms.Panel();
            this.BotaoCancelar = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBoxInsDt = new System.Windows.Forms.CheckBox();
            this.obstb = new System.Windows.Forms.RichTextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.GerarDocumentoBar = new System.Windows.Forms.ToolStripProgressBar();
            this.ZonaActividadetextlabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ZonaActividadelabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.Itemtextlabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.Itemlabel = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panelZonaItem.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.splitContainer1.Panel1.Controls.Add(this.treeViewZonaItem);
            resources.ApplyResources(this.splitContainer1.Panel1, "splitContainer1.Panel1");
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.splitContainer1.Panel2.Controls.Add(this.panelZonaItem);
            this.splitContainer1.TabStop = false;
            // 
            // treeViewZonaItem
            // 
            this.treeViewZonaItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.treeViewZonaItem, "treeViewZonaItem");
            this.treeViewZonaItem.Name = "treeViewZonaItem";
            this.treeViewZonaItem.TabStop = false;
            this.treeViewZonaItem.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.OpenAction);
            // 
            // panelZonaItem
            // 
            resources.ApplyResources(this.panelZonaItem, "panelZonaItem");
            this.panelZonaItem.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panelZonaItem.Controls.Add(this.panelrel);
            this.panelZonaItem.Controls.Add(this.BotaoCancelar);
            this.panelZonaItem.Controls.Add(this.button1);
            this.panelZonaItem.Name = "panelZonaItem";
            // 
            // panelrel
            // 
            resources.ApplyResources(this.panelrel, "panelrel");
            this.panelrel.BackColor = System.Drawing.Color.White;
            this.panelrel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelrel.Name = "panelrel";
            // 
            // BotaoCancelar
            // 
            resources.ApplyResources(this.BotaoCancelar, "BotaoCancelar");
            this.BotaoCancelar.BackColor = System.Drawing.SystemColors.Control;
            this.BotaoCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BotaoCancelar.Name = "BotaoCancelar";
            this.BotaoCancelar.UseVisualStyleBackColor = false;
            this.BotaoCancelar.Click += new System.EventHandler(this.BotaoCancelar_Click);
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.BackColor = System.Drawing.SystemColors.Control;
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = false;
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
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GerarDocumentoBar,
            this.ZonaActividadetextlabel,
            this.ZonaActividadelabel,
            this.Itemtextlabel,
            this.Itemlabel});
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // GerarDocumentoBar
            // 
            this.GerarDocumentoBar.Name = "GerarDocumentoBar";
            resources.ApplyResources(this.GerarDocumentoBar, "GerarDocumentoBar");
            this.GerarDocumentoBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // ZonaActividadetextlabel
            // 
            this.ZonaActividadetextlabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ZonaActividadetextlabel.Margin = new System.Windows.Forms.Padding(10, 3, 0, 2);
            this.ZonaActividadetextlabel.Name = "ZonaActividadetextlabel";
            resources.ApplyResources(this.ZonaActividadetextlabel, "ZonaActividadetextlabel");
            // 
            // ZonaActividadelabel
            // 
            this.ZonaActividadelabel.Name = "ZonaActividadelabel";
            resources.ApplyResources(this.ZonaActividadelabel, "ZonaActividadelabel");
            // 
            // Itemtextlabel
            // 
            this.Itemtextlabel.Name = "Itemtextlabel";
            resources.ApplyResources(this.Itemtextlabel, "Itemtextlabel");
            // 
            // Itemlabel
            // 
            this.Itemlabel.Name = "Itemlabel";
            resources.ApplyResources(this.Itemlabel, "Itemlabel");
            // 
            // Interface_Relatorio
            // 
            this.AcceptButton = this.button1;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.CancelButton = this.BotaoCancelar;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "Interface_Relatorio";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panelZonaItem.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeViewZonaItem;
        private System.Windows.Forms.Button BotaoCancelar;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox obstb;
        private System.Windows.Forms.CheckBox checkBoxInsDt;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar GerarDocumentoBar;
        private System.Windows.Forms.ToolStripStatusLabel ZonaActividadetextlabel;
        private System.Windows.Forms.ToolStripStatusLabel ZonaActividadelabel;
        private System.Windows.Forms.ToolStripStatusLabel Itemtextlabel;
        private System.Windows.Forms.ToolStripStatusLabel Itemlabel;
        private System.Windows.Forms.Panel panelZonaItem;
        private System.Windows.Forms.Panel panelrel;
    }
}