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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeViewZonaItem = new System.Windows.Forms.TreeView();
            this.BotaoGuardar = new System.Windows.Forms.Button();
            this.BotaoCancelar = new System.Windows.Forms.Button();
            this.panelZonaItem = new System.Windows.Forms.Panel();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panelZonaItem.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeViewZonaItem);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(5);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panelZonaItem);
            this.splitContainer1.Size = new System.Drawing.Size(818, 432);
            this.splitContainer1.SplitterDistance = 219;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeViewZonaItem
            // 
            this.treeViewZonaItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeViewZonaItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewZonaItem.Location = new System.Drawing.Point(5, 5);
            this.treeViewZonaItem.Name = "treeViewZonaItem";
            this.treeViewZonaItem.Size = new System.Drawing.Size(209, 422);
            this.treeViewZonaItem.TabIndex = 0;
            this.treeViewZonaItem.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.OpenAction);
            // 
            // BotaoGuardar
            // 
            this.BotaoGuardar.Location = new System.Drawing.Point(388, 397);
            this.BotaoGuardar.Name = "BotaoGuardar";
            this.BotaoGuardar.Size = new System.Drawing.Size(75, 23);
            this.BotaoGuardar.TabIndex = 2;
            this.BotaoGuardar.Text = "Guardar";
            this.BotaoGuardar.UseVisualStyleBackColor = true;
            this.BotaoGuardar.Click += new System.EventHandler(this.BotaoGuardar_Click);
            // 
            // BotaoCancelar
            // 
            this.BotaoCancelar.Location = new System.Drawing.Point(497, 397);
            this.BotaoCancelar.Name = "BotaoCancelar";
            this.BotaoCancelar.Size = new System.Drawing.Size(75, 23);
            this.BotaoCancelar.TabIndex = 1;
            this.BotaoCancelar.Text = "Cancelar";
            this.BotaoCancelar.UseVisualStyleBackColor = true;
            this.BotaoCancelar.Click += new System.EventHandler(this.BotaoCancelar_Click);
            // 
            // panelZonaItem
            // 
            this.panelZonaItem.AutoScroll = true;
            this.panelZonaItem.Controls.Add(this.BotaoGuardar);
            this.panelZonaItem.Controls.Add(this.BotaoCancelar);
            this.panelZonaItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelZonaItem.Location = new System.Drawing.Point(0, 0);
            this.panelZonaItem.Name = "panelZonaItem";
            this.panelZonaItem.Padding = new System.Windows.Forms.Padding(0, 5, 5, 40);
            this.panelZonaItem.Size = new System.Drawing.Size(595, 432);
            this.panelZonaItem.TabIndex = 0;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileFialog1_FileOk);
            // 
            // Interface_Relatorio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(818, 432);
            this.Controls.Add(this.splitContainer1);
            this.MaximizeBox = false;
            this.Name = "Interface_Relatorio";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relatório";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panelZonaItem.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeViewZonaItem;
        private System.Windows.Forms.Panel panelZonaItem;
        private System.Windows.Forms.Button BotaoGuardar;
        private System.Windows.Forms.Button BotaoCancelar;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;




    }
}