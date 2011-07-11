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
            this.panelZonaItem = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.BotaoGuardar = new System.Windows.Forms.Button();
            this.BotaoCancelar = new System.Windows.Forms.Button();
            this.panelrel = new System.Windows.Forms.Panel();
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
            this.splitContainer1.Size = new System.Drawing.Size(799, 427);
            this.splitContainer1.SplitterDistance = 213;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeViewZonaItem
            // 
            this.treeViewZonaItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeViewZonaItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewZonaItem.Location = new System.Drawing.Point(5, 5);
            this.treeViewZonaItem.Name = "treeViewZonaItem";
            this.treeViewZonaItem.Size = new System.Drawing.Size(203, 417);
            this.treeViewZonaItem.TabIndex = 0;
            this.treeViewZonaItem.TabStop = false;
            this.treeViewZonaItem.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.OpenAction);
            // 
            // panelZonaItem
            // 
            this.panelZonaItem.AutoScroll = true;
            this.panelZonaItem.Controls.Add(this.panelrel);
            this.panelZonaItem.Controls.Add(this.BotaoCancelar);
            this.panelZonaItem.Controls.Add(this.BotaoGuardar);
            this.panelZonaItem.Controls.Add(this.button1);
            this.panelZonaItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelZonaItem.Location = new System.Drawing.Point(0, 0);
            this.panelZonaItem.Name = "panelZonaItem";
            this.panelZonaItem.Padding = new System.Windows.Forms.Padding(0, 5, 5, 40);
            this.panelZonaItem.Size = new System.Drawing.Size(582, 427);
            this.panelZonaItem.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button1.Location = new System.Drawing.Point(150, 392);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(184, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Gerar Documento Word";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // BotaoGuardar
            // 
            this.BotaoGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BotaoGuardar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.BotaoGuardar.Location = new System.Drawing.Point(375, 392);
            this.BotaoGuardar.Name = "BotaoGuardar";
            this.BotaoGuardar.Size = new System.Drawing.Size(75, 23);
            this.BotaoGuardar.TabIndex = 2;
            this.BotaoGuardar.Text = "Guardar";
            this.BotaoGuardar.UseVisualStyleBackColor = true;
            this.BotaoGuardar.Click += new System.EventHandler(this.BotaoGuardar_Click);
            // 
            // BotaoCancelar
            // 
            this.BotaoCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BotaoCancelar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.BotaoCancelar.Location = new System.Drawing.Point(486, 392);
            this.BotaoCancelar.Name = "BotaoCancelar";
            this.BotaoCancelar.Size = new System.Drawing.Size(75, 23);
            this.BotaoCancelar.TabIndex = 1;
            this.BotaoCancelar.Text = "Cancelar";
            this.BotaoCancelar.UseVisualStyleBackColor = true;
            this.BotaoCancelar.Click += new System.EventHandler(this.BotaoCancelar_Click);
            // 
            // panelrel
            // 
            this.panelrel.AutoScroll = true;
            this.panelrel.AutoSize = true;
            this.panelrel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelrel.Location = new System.Drawing.Point(0, 5);
            this.panelrel.Name = "panelrel";
            this.panelrel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 60);
            this.panelrel.Size = new System.Drawing.Size(577, 382);
            this.panelrel.TabIndex = 0;
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
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(799, 427);
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
            this.panelZonaItem.PerformLayout();
            this.ResumeLayout(false);

            //
            // CheckBox Resultado Detalhado
            //
            checkBoxInsDt = new System.Windows.Forms.CheckBox();
            checkBoxInsDt.Text = "Incluir resultado detalhado no relatório";
            checkBoxInsDt.Location = new System.Drawing.Point(10, 245 + 105);
            checkBoxInsDt.Size = new System.Drawing.Size(300, 30);
            checkBoxInsDt.TabIndex = 3;
            checkBoxInsDt.TabStop = true;
            checkBoxInsDt.CheckedChanged += new System.EventHandler(checkBoxInsDt_CheckedChanged);

            //
            // TextBox observações
            //
            obstb = new System.Windows.Forms.RichTextBox();
            obstb.Location = new System.Drawing.Point(10, 335 + 105);
            obstb.Name = "obstb";
            obstb.Size = new System.Drawing.Size(500, 100);
            obstb.Margin = new System.Windows.Forms.Padding(0, 0, 0, 60);
            obstb.TabIndex = 4;
            obstb.TabStop = true;
            obstb.TextChanged += new System.EventHandler(obstb_TextChanged);
        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeViewZonaItem;
        private System.Windows.Forms.Panel panelZonaItem;
        private System.Windows.Forms.Button BotaoGuardar;
        private System.Windows.Forms.Button BotaoCancelar;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panelrel;
        private System.Windows.Forms.RichTextBox obstb;
        private System.Windows.Forms.CheckBox checkBoxInsDt;
    }
}