using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ETdA.Camada_de_Negócio;
using System.Drawing.Drawing2D;
using ETdA.Camada_de_Dados.Classes;

namespace ETdA.Camada_de_Interface
{
    public partial class InterfaceGuestaoProjectos : Form
    {
        private static InterfaceGuestaoProjectos igp;
        private List<int> indexes;

        private CheckBox checkBox1, checkBox2, checkBox3;

        // s_final
        public InterfaceGuestaoProjectos()
        {
            GestaodeProjectos.init();
            InitializeComponent();

            indexes = new List<int>();

            initTree();
            initPaginaInicial();
            //inicializar tabcontrol
            // 
            // tabControl1
            // 
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.Normal;
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(16, 0);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(585, 428);
            this.tabControl1.TabIndex = 0;
            panel1.Controls.Add(tabControl1);
            //inicializar menu's dinâmicos
            apagarAnáliseToolStripMenuItem.DropDownItems.AddRange(GetFullToolStripMenuApagarAnalise());
            apagarProjectoToolStripMenuItem.DropDownItems.AddRange(getToolStripApagarProjecto());
            novaAnaliseToolStripMenuItem.DropDownItems.AddRange(GetToolStripMenuCriarAnalise());
        }

        // s_final
        private void initTree()
        {
            Dictionary<long, string> cod_names = GestaodeProjectos.Cod_names_Projects();

            for (int i = 0; i < cod_names.Count; i++)
            {
                TreeNode nodo = new TreeNode();
                nodo.Text = cod_names.ElementAt(i).Value;
                nodo.Name = cod_names.ElementAt(i).Key.ToString();
                nodo.Nodes.Add("");
                this.treeView_Projectos.Nodes.Add(nodo);
            }
        }

        # region Gestao das Abas

        // s_final
        private void fecharPagina(int index)
        {
            if (tabControl1.TabPages[index] != null)
                tabControl1.TabPages.RemoveAt(index);
            if (tabControl1.Pages[index] != null)
                tabControl1.Pages.RemoveAt(index);
        }

        private void fecharPagina(string nome)
        {
            TabPage tp = new TabPage();
            foreach (TabPage ttp in tabControl1.TabPages)
            {
                if (ttp.Name == nome) tp = ttp;
                break;
            }
            tabControl1.CloseTab(tp);
        }

        // s_final
        private void initPaginaInicial()
        {
            if (!tabControl1.Pages.Contains("StartPage"))
            {
                tabControl1.AddPage("StartPage");
                System.Windows.Forms.TabPage p = new System.Windows.Forms.TabPage();
                p.Name = "StartPage";
                p.AutoScroll = true;
                p.BackColor = Color.White;
                p.Text = "Página Inicial";
                p.Size = new System.Drawing.Size(218, 385);

                ContextMenu m = new ContextMenu();
                m.MenuItems.Add(new MenuItem("Fechar Tab", new EventHandler(fecharTab)));
                p.ContextMenu = m;


                this.tabControl1.Controls.Add(p);

                #region desenho página inicial
                System.Windows.Forms.Panel panelPaginaInicial;
                System.Windows.Forms.SplitContainer splitContainer2;
                System.Windows.Forms.SplitContainer splitContainer3;
                System.Windows.Forms.Label label8;
                System.Windows.Forms.PictureBox pictureBox1;
                System.Windows.Forms.Label label7;
                System.Windows.Forms.Label label6;
                System.Windows.Forms.Label label5;
                System.Windows.Forms.Label label4;
                System.Windows.Forms.Label label3;
                System.Windows.Forms.Label label2;
                System.Windows.Forms.Label label11;
                System.Windows.Forms.Label label10;
                System.Windows.Forms.Label label9;
                System.Windows.Forms.Panel panel2;
                System.Windows.Forms.Label label1;
                System.Windows.Forms.Panel panel4;
                System.Windows.Forms.Panel panel3;
                panelPaginaInicial = new System.Windows.Forms.Panel();
                panel2 = new System.Windows.Forms.Panel();
                label1 = new System.Windows.Forms.Label();
                splitContainer2 = new System.Windows.Forms.SplitContainer();
                splitContainer3 = new System.Windows.Forms.SplitContainer();
                label2 = new System.Windows.Forms.Label();
                label3 = new System.Windows.Forms.Label();
                label4 = new System.Windows.Forms.Label();
                label5 = new System.Windows.Forms.Label();
                label6 = new System.Windows.Forms.Label();
                label7 = new System.Windows.Forms.Label();
                pictureBox1 = new System.Windows.Forms.PictureBox();
                label8 = new System.Windows.Forms.Label();
                label9 = new System.Windows.Forms.Label();
                label10 = new System.Windows.Forms.Label();
                label11 = new System.Windows.Forms.Label();
                panel3 = new System.Windows.Forms.Panel();
                panel4 = new System.Windows.Forms.Panel();
                // 
                // panelPaginaInicial
                // 
                panelPaginaInicial.BackColor = System.Drawing.Color.White;
                panelPaginaInicial.Controls.Add(splitContainer2);
                panelPaginaInicial.Controls.Add(panel2);
                panelPaginaInicial.Dock = System.Windows.Forms.DockStyle.Fill;
                panelPaginaInicial.Location = new System.Drawing.Point(0, 0);
                panelPaginaInicial.Name = "panelPaginaProjecto";
                panelPaginaInicial.Padding = new System.Windows.Forms.Padding(20);
                panelPaginaInicial.Size = new System.Drawing.Size(596, 389);
                panelPaginaInicial.TabIndex = 0;
                // 
                // panel2
                // 
                panel2.Controls.Add(label1);
                panel2.Dock = System.Windows.Forms.DockStyle.Top;
                panel2.Location = new System.Drawing.Point(20, 20);
                panel2.Name = "panel2";
                panel2.Size = new System.Drawing.Size(556, 45);
                panel2.TabIndex = 0;
                // 
                // label1
                // 
                label1.AutoSize = true;
                label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                label1.Location = new System.Drawing.Point(0, 6);
                label1.Name = "label1";
                label1.Size = new System.Drawing.Size(126, 25);
                label1.TabIndex = 0;
                label1.Text = "ETdAnalyser";
                // 
                // splitContainer2
                // 
                splitContainer2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
                splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
                splitContainer2.Location = new System.Drawing.Point(20, 65);
                splitContainer2.Margin = new System.Windows.Forms.Padding(0);
                splitContainer2.Name = "splitContainer2";
                // 
                // splitContainer2.Panel1
                // 
                splitContainer2.Panel1.Controls.Add(splitContainer3);
                // 
                // splitContainer2.Panel2
                // 
                splitContainer2.Panel2.BackColor = System.Drawing.Color.White;
                splitContainer2.Panel2.Controls.Add(panel4);
                splitContainer2.Panel2.Controls.Add(panel3);
                splitContainer2.Size = new System.Drawing.Size(556, 304);
                splitContainer2.SplitterDistance = 212;
                splitContainer2.SplitterWidth = 1;
                splitContainer2.TabIndex = 1;
                // 
                // splitContainer3
                // 
                splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
                splitContainer3.Location = new System.Drawing.Point(0, 0);
                splitContainer3.Margin = new System.Windows.Forms.Padding(0);
                splitContainer3.Name = "splitContainer3";
                splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
                // 
                // splitContainer3.Panel1
                // 
                splitContainer3.Panel1.BackColor = System.Drawing.Color.White;
                splitContainer3.Panel1.Controls.Add(label8);
                splitContainer3.Panel1.Controls.Add(pictureBox1);
                // 
                // splitContainer3.Panel2
                // 
                splitContainer3.Panel2.BackColor = System.Drawing.Color.White;
                splitContainer3.Panel2.Controls.Add(label2);
                splitContainer3.Size = new System.Drawing.Size(212, 304);
                splitContainer3.SplitterDistance = 62;
                splitContainer3.SplitterWidth = 1;
                splitContainer3.TabIndex = 0;
                // 
                // label2
                // 
                label2.AutoSize = true;
                label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                label2.ForeColor = System.Drawing.SystemColors.ControlDark;
                label2.Location = new System.Drawing.Point(4, 10);
                label2.Name = "label2";
                label2.Size = new System.Drawing.Size(139, 18);
                label2.TabIndex = 0;
                label2.Text = "Projectos Recentes";
                // 
                // label3
                // 
                label3.AutoSize = true;
                label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                label3.ForeColor = System.Drawing.Color.Blue;
                label3.Location = new System.Drawing.Point(20, 44);
                label3.Name = "label3";
                label3.Size = new System.Drawing.Size(55, 13);
                label3.TabIndex = 1;
                label3.Text = "Projecto 1";
                label3.Visible = false;
                label3.Cursor = System.Windows.Forms.Cursors.Hand;
                label3.Click += new System.EventHandler(this.OpenProjectClick);
                label3.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                label3.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
                // 
                // label4
                // 
                label4.AutoSize = true;
                label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                label4.ForeColor = System.Drawing.Color.Blue;
                label4.Location = new System.Drawing.Point(20, 71);
                label4.Name = "label4";
                label4.Size = new System.Drawing.Size(55, 13);
                label4.TabIndex = 2;
                label4.Text = "Projecto 2";
                label4.Visible = false;
                label4.Cursor = System.Windows.Forms.Cursors.Hand;
                label4.Click += new System.EventHandler(this.OpenProjectClick);
                label4.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                label4.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
                // 
                // label5
                // 
                label5.AutoSize = true;
                label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                label5.ForeColor = System.Drawing.Color.Blue;
                label5.Location = new System.Drawing.Point(20, 97);
                label5.Name = "label5";
                label5.Size = new System.Drawing.Size(55, 13);
                label5.TabIndex = 3;
                label5.Text = "Projecto 3";
                label5.Visible = false;
                label5.Cursor = System.Windows.Forms.Cursors.Hand;
                label5.Click += new System.EventHandler(this.OpenProjectClick);
                label5.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                label5.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
                // 
                // label6
                // 
                label6.AutoSize = true;
                label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                label6.ForeColor = System.Drawing.Color.Blue;
                label6.Location = new System.Drawing.Point(20, 126);
                label6.Name = "label6";
                label6.Size = new System.Drawing.Size(55, 13);
                label6.TabIndex = 4;
                label6.Text = "Projecto 4";
                label6.Visible = false;
                label6.Cursor = System.Windows.Forms.Cursors.Hand;
                label6.Click += new System.EventHandler(this.OpenProjectClick);
                label6.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                label6.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
                // 
                // label7
                // 
                label7.AutoSize = true;
                label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                label7.ForeColor = System.Drawing.Color.Blue;
                label7.Location = new System.Drawing.Point(20, 157);
                label7.Name = "label7";
                label7.Size = new System.Drawing.Size(55, 13);
                label7.TabIndex = 5;
                label7.Text = "Projecto 5";
                label7.Visible = false;
                label7.Cursor = System.Windows.Forms.Cursors.Hand;
                label7.Click += new System.EventHandler(this.OpenProjectClick);
                label7.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                label7.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
                // 
                // pictureBox1
                // 
                pictureBox1.BackgroundImage = global::ETdA.Properties.Resources._1309271471_file_add;
                pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                pictureBox1.Location = new System.Drawing.Point(13, 19);
                pictureBox1.Name = "pictureBox1";
                pictureBox1.Size = new System.Drawing.Size(17, 17);
                pictureBox1.TabIndex = 0;
                pictureBox1.TabStop = false;
                // 
                // label8
                // 
                label8.AutoSize = true;
                label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                label8.ForeColor = System.Drawing.Color.Blue;
                label8.Location = new System.Drawing.Point(38, 22);
                label8.Name = "label8";
                label8.Size = new System.Drawing.Size(94, 16);
                label8.TabIndex = 6;
                label8.Text = "Novo Projecto";
                label8.Cursor = System.Windows.Forms.Cursors.Hand;
                label8.Click += new System.EventHandler(this.CriarProjectoClick);
                label8.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                label8.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
                // 
                // label9
                // 
                label9.AutoSize = true;
                label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                label9.Location = new System.Drawing.Point(18, 14);
                label9.Name = "label9";
                label9.Size = new System.Drawing.Size(131, 22);
                label9.TabIndex = 0;
                label9.Text = "Onde Começar";
                // 
                // label10
                // 
                label10.AutoSize = true;
                label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                label10.ForeColor = System.Drawing.Color.Blue;
                label10.Location = new System.Drawing.Point(19, 48);
                label10.Name = "label10";
                label10.Size = new System.Drawing.Size(80, 18);
                label10.TabIndex = 1;
                label10.Text = "Bem Vindo";
                // 
                // label11
                // 
                label11.AutoSize = true;
                label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                label11.ForeColor = System.Drawing.Color.Blue;
                label11.Location = new System.Drawing.Point(193, 48);
                label11.Name = "label11";
                label11.Size = new System.Drawing.Size(44, 18);
                label11.TabIndex = 2;
                label11.Text = "Ajuda";
                label11.Click += new System.EventHandler(this.CriarProjectoClick);
                label11.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                label11.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
                // 
                // panel3
                // 
                panel3.Controls.Add(label9);
                //panel3.Controls.Add(label11);
                panel3.Controls.Add(label10);
                panel3.Dock = System.Windows.Forms.DockStyle.Top;
                panel3.Location = new System.Drawing.Point(0, 0);
                panel3.Name = "panel3";
                panel3.Size = new System.Drawing.Size(343, 73);
                panel3.TabIndex = 3;

                // 
                // panel4
                // 
                panel4.Dock = System.Windows.Forms.DockStyle.Fill;
                panel4.Location = new System.Drawing.Point(0, 73);
                panel4.Name = "panel4";
                panel4.Size = new System.Drawing.Size(343, 231);
                panel4.TabIndex = 4;
                panel4.Padding = new Padding(10, 0, 0, 0);
                
                //
                // RIchtextbox
                //
                RichTextBox rtb = new RichTextBox();
                rtb.Dock = System.Windows.Forms.DockStyle.Fill;
                rtb.BorderStyle = BorderStyle.None;
                rtb.BackColor = Color.White;
                rtb.Text = "Sabia que...\n\n Apesar de ainda não termos nada de util para dizer aqui, este texto ajuda a tornar este separador mais agradável.";
                rtb.ReadOnly = true;
                panel4.Controls.Add(rtb);
                #endregion

                p.Controls.Add(panelPaginaInicial);

                Dictionary<long, string> rs = GestaodeProjectos.projectosRecentes();

                if (rs.Count() == 0)
                {
                    label3.Text = "Ainda não existem projectos...";
                    label3.Enabled = false;
                    label3.Visible = true;
                    splitContainer3.Panel2.Controls.Add(label3);
                }

                int x = 20, y = 44;
                foreach (KeyValuePair<long, string> s in rs)
                {
                    System.Windows.Forms.Label p1 =
                        new System.Windows.Forms.Label();

                    p1.Text = s.Value;
                    p1.Name = s.Key.ToString();
                    p1.Cursor = System.Windows.Forms.Cursors.Hand;
                    p1.Location = new System.Drawing.Point(x,y);
                    p1.Click += new System.EventHandler(this.OpenProjectClick);
                    p1.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                    p1.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
                    p1.AutoSize = true;
                    p1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    p1.ForeColor = System.Drawing.Color.Blue;
                    p1.TabIndex = 3;
                    splitContainer3.Panel2.Controls.Add(p1);

                    y += 25;
                }

            }
            tabControl1.SelectedIndex = getTabNumber("StartPage");
        }

        // s_final
        private void initPaginaProjecto(string nome_projecto, string codp)
        {
            if (!tabControl1.Pages.Contains(codp))
            {
                tabControl1.AddPage(codp);
                System.Windows.Forms.TabPage p = new System.Windows.Forms.TabPage();
                p.Name = codp;
                p.AutoScroll = true;
                p.Text = nome_projecto;
                p.Size = new System.Drawing.Size(218, 385);
                p.BackColor = Color.White;

                ContextMenu m = new ContextMenu();
                m.MenuItems.Add(new MenuItem("Fechar Tab", new EventHandler(fecharTab)));
                p.ContextMenu = m;

                tabControl1.Controls.Add(p);

                #region desenho página inicial
                System.Windows.Forms.Panel panelPaginaProjecto;
                System.Windows.Forms.SplitContainer splitContainer2;
                System.Windows.Forms.SplitContainer splitContainer3;
                System.Windows.Forms.Label label8;
                System.Windows.Forms.PictureBox pictureBox1;
                System.Windows.Forms.Label label7;
                System.Windows.Forms.Label label6;
                System.Windows.Forms.Label label5;
                System.Windows.Forms.Label label4;
                System.Windows.Forms.Label label3;
                System.Windows.Forms.Label label2;
                System.Windows.Forms.Label label11;
                System.Windows.Forms.Label label10;
                System.Windows.Forms.Label label9;
                System.Windows.Forms.Panel panel2;
                System.Windows.Forms.Label label1;
                System.Windows.Forms.Panel panel4;
                System.Windows.Forms.Panel panel3;
                panelPaginaProjecto = new System.Windows.Forms.Panel();
                panel2 = new System.Windows.Forms.Panel();
                label1 = new System.Windows.Forms.Label();
                splitContainer2 = new System.Windows.Forms.SplitContainer();
                splitContainer3 = new System.Windows.Forms.SplitContainer();
                label2 = new System.Windows.Forms.Label();
                label3 = new System.Windows.Forms.Label();
                label4 = new System.Windows.Forms.Label();
                label5 = new System.Windows.Forms.Label();
                label6 = new System.Windows.Forms.Label();
                label7 = new System.Windows.Forms.Label();
                pictureBox1 = new System.Windows.Forms.PictureBox();
                label8 = new System.Windows.Forms.Label();
                label9 = new System.Windows.Forms.Label();
                label10 = new System.Windows.Forms.Label();
                label11 = new System.Windows.Forms.Label();
                panel3 = new System.Windows.Forms.Panel();
                panel4 = new System.Windows.Forms.Panel();
                // 
                // panelPaginaInicial
                // 
                panelPaginaProjecto.BackColor = System.Drawing.Color.White;
                panelPaginaProjecto.Controls.Add(splitContainer2);
                panelPaginaProjecto.Controls.Add(panel2);
                panelPaginaProjecto.Dock = System.Windows.Forms.DockStyle.Fill;
                panelPaginaProjecto.Location = new System.Drawing.Point(0, 0);
                panelPaginaProjecto.Name = "panelPaginaProjecto";
                panelPaginaProjecto.Padding = new System.Windows.Forms.Padding(20);
                panelPaginaProjecto.Size = new System.Drawing.Size(596, 389);
                panelPaginaProjecto.TabIndex = 0;
                // 
                // panel2
                // 
                panel2.Controls.Add(label1);
                panel2.Dock = System.Windows.Forms.DockStyle.Top;
                panel2.Location = new System.Drawing.Point(20, 20);
                panel2.Name = "panel2";
                panel2.Size = new System.Drawing.Size(556, 45);
                panel2.TabIndex = 0;
                // 
                // label1
                // 
                label1.AutoSize = true;
                label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                label1.Location = new System.Drawing.Point(0, 6);
                label1.Name = "label1";
                label1.Size = new System.Drawing.Size(126, 25);
                label1.TabIndex = 0;
                label1.Text = nome_projecto;
                // 
                // splitContainer2
                // 
                splitContainer2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
                splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
                splitContainer2.Location = new System.Drawing.Point(20, 65);
                splitContainer2.Margin = new System.Windows.Forms.Padding(0);
                splitContainer2.Name = "splitContainer2";
                // 
                // splitContainer2.Panel1
                // 
                splitContainer2.Panel1.Controls.Add(splitContainer3);
                // 
                // splitContainer2.Panel2
                // 
                splitContainer2.Panel2.BackColor = System.Drawing.Color.White;
                splitContainer2.Panel2.Controls.Add(panel4);
                splitContainer2.Panel2.Controls.Add(panel3);
                splitContainer2.Size = new System.Drawing.Size(556, 304);
                splitContainer2.SplitterDistance = 212;
                splitContainer2.SplitterWidth = 1;
                splitContainer2.TabIndex = 1;
                // 
                // splitContainer3
                // 
                splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
                splitContainer3.Location = new System.Drawing.Point(0, 0);
                splitContainer3.Margin = new System.Windows.Forms.Padding(0);
                splitContainer3.Name = "splitContainer3";
                splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
                // 
                // splitContainer3.Panel1
                // 
                splitContainer3.Panel1.BackColor = System.Drawing.Color.White;
                splitContainer3.Panel1.Controls.Add(label8);
                splitContainer3.Panel1.Controls.Add(pictureBox1);
                // 
                // splitContainer3.Panel2
                // 
                splitContainer3.Panel2.BackColor = System.Drawing.Color.White;
                splitContainer3.Panel2.Controls.Add(label2);
                splitContainer3.Size = new System.Drawing.Size(212, 304);
                splitContainer3.SplitterDistance = 62;
                splitContainer3.SplitterWidth = 1;
                splitContainer3.TabIndex = 0;
                // 
                // label2
                // 
                label2.AutoSize = true;
                label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                label2.ForeColor = System.Drawing.SystemColors.ControlDark;
                label2.Location = new System.Drawing.Point(4, 10);
                label2.Name = "label2";
                label2.Size = new System.Drawing.Size(139, 18);
                label2.TabIndex = 0;
                label2.Text = "Analises desde projecto";
                // 
                // label3
                // 
                label3.AutoSize = true;
                label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                label3.ForeColor = System.Drawing.Color.Blue;
                label3.Location = new System.Drawing.Point(20, 44);
                label3.Name = "label3";
                label3.Size = new System.Drawing.Size(55, 13);
                label3.TabIndex = 1;
                label3.Text = "Analise 1";
                label3.Visible = false;
                label3.Cursor = System.Windows.Forms.Cursors.Hand;
                label3.Click += new System.EventHandler(this.OpenProjectClick);
                label3.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                label3.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
                //
                // pictureBox1
                // 
                pictureBox1.BackgroundImage = global::ETdA.Properties.Resources._1309271576_folder_add;
                pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                pictureBox1.Location = new System.Drawing.Point(13, 19);
                pictureBox1.Name = "pictureBox1";
                pictureBox1.Size = new System.Drawing.Size(17, 17);
                pictureBox1.TabIndex = 0;
                pictureBox1.TabStop = false;
                // 
                // label8
                // 
                label8.AutoSize = true;
                label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                label8.ForeColor = System.Drawing.Color.Blue;
                label8.Location = new System.Drawing.Point(38, 22);
                label8.Name = "label8";
                label8.Size = new System.Drawing.Size(94, 16);
                label8.TabIndex = 6;
                label8.Text = "Nova Analise";
                label8.Cursor = System.Windows.Forms.Cursors.Hand;
                label8.Click += new System.EventHandler(this.CriarAnaliseClick);
                label8.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                label8.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
                // 
                // label9
                // 
                label9.AutoSize = true;
                label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                label9.Location = new System.Drawing.Point(18, 14);
                label9.Name = "label9";
                label9.Size = new System.Drawing.Size(131, 22);
                label9.TabIndex = 0;
                label9.Text = "Mais Informação";
                // 
                // label10
                // 
                label10.AutoSize = true;
                label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                label10.ForeColor = System.Drawing.Color.Blue;
                label10.Location = new System.Drawing.Point(19, 48);
                label10.Name = "label10";
                label10.Size = new System.Drawing.Size(80, 18);
                label10.TabIndex = 1;
                label10.Text = "Projectos";
                // 
                // label11
                // 
                label11.AutoSize = true;
                label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                label11.ForeColor = System.Drawing.Color.Blue;
                label11.Location = new System.Drawing.Point(193, 48);
                label11.Name = "label11";
                label11.Size = new System.Drawing.Size(44, 18);
                label11.TabIndex = 2;
                label11.Text = "Ajuda";
                label11.Click += new System.EventHandler(this.CriarProjectoClick);
                label11.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                label11.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
                // 
                // panel3
                // 
                panel3.Controls.Add(label9);
                //panel3.Controls.Add(label11);
                panel3.Controls.Add(label10);
                panel3.Dock = System.Windows.Forms.DockStyle.Top;
                panel3.Location = new System.Drawing.Point(0, 0);
                panel3.Name = "panel3";
                panel3.Size = new System.Drawing.Size(343, 73);
                panel3.TabIndex = 3;

                // 
                // panel4
                // 
                panel4.Dock = System.Windows.Forms.DockStyle.Fill;
                panel4.Location = new System.Drawing.Point(0, 73);
                panel4.Name = "panel4";
                panel4.Size = new System.Drawing.Size(343, 231);
                panel4.TabIndex = 4;
                panel4.Padding = new Padding(10, 0, 0, 0);

                //
                // RIchtextbox
                //
                RichTextBox rtb = new RichTextBox();
                rtb.Dock = System.Windows.Forms.DockStyle.Fill;
                rtb.BorderStyle = BorderStyle.None;
                rtb.BackColor = Color.White;
                rtb.Text = "Sabia que...\n\n Apesar de ainda não termos nada de util para dizer aqui, este texto ajuda a tornar este separador mais agradável.";
                rtb.ReadOnly = true;
                panel4.Controls.Add(rtb);
                #endregion

                p.Controls.Add(panelPaginaProjecto);

                Dictionary<long, string> ans = GestaodeAnalises.getCodeNomeAnalises(long.Parse(codp));

                if (ans.Count() == 0)
                {
                    label3.Text = "Ainda não existem analises...";
                    label3.Enabled = false;
                    label3.Visible = true;
                    splitContainer3.Panel2.Controls.Add(label3);
                }

                int x = 20, y = 44;
                foreach (KeyValuePair<long, string> s in ans)
                {
                    System.Windows.Forms.Label p1 =
                        new System.Windows.Forms.Label();

                    p1.Text = s.Value;
                    p1.Name = s.Key.ToString();
                    p1.Cursor = System.Windows.Forms.Cursors.Hand;
                    p1.Location = new System.Drawing.Point(x, y);
                    p1.Click += new System.EventHandler(this.OpenAnaliseClick);
                    p1.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                    p1.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
                    p1.AutoSize = true;
                    p1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    p1.ForeColor = System.Drawing.Color.Blue;
                    p1.TabIndex = 3;
                    splitContainer3.Panel2.Controls.Add(p1);

                    y += 25;
                }
            }
            tabControl1.SelectedIndex = getTabNumber(codp);
        }

        // s_final
        private void initPaginaAnalise(long codp, long coda, string nome_analise)
        {
            if (!tabControl1.Pages.Contains(codp.ToString() + "." + coda.ToString()))
            {
                tabControl1.AddPage(codp.ToString() + "." + coda.ToString());
                System.Windows.Forms.TabPage p = new System.Windows.Forms.TabPage();
                p.Name = codp.ToString() + "." + coda.ToString();
                p.AutoScroll = true;
                p.Text = nome_analise;
                p.Size = new System.Drawing.Size(218, 385);
                p.BackColor = Color.White;

                ContextMenu m = new ContextMenu();
                m.MenuItems.Add(new MenuItem("Fechar Tab", new EventHandler(fecharTab)));
                p.ContextMenu = m;

                this.tabControl1.Controls.Add(p);

                #region desenho página Analise
                #region COnsultar
                System.Windows.Forms.Label lconsulta = new System.Windows.Forms.Label();
                lconsulta.Text = "Consultar";
                lconsulta.ForeColor = SystemColors.ControlDark;
                lconsulta.Location = new Point(7, 6);
                lconsulta.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));

                System.Windows.Forms.Label l1 = new System.Windows.Forms.Label();
                l1.Text = "Itens";
                l1.Location = new Point(15, 36);
                l1.ForeColor = Color.Blue;
                l1.Cursor = System.Windows.Forms.Cursors.Hand;
                l1.Click += new System.EventHandler(this.OpenItensClick);
                l1.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                l1.MouseLeave += new System.EventHandler(this.MouseLeaveAction);

                System.Windows.Forms.Label l2 = new System.Windows.Forms.Label();
                l2.Text = "Zonas";
                l2.ForeColor = Color.Blue;
                l2.Location = new Point(15, 66);
                l2.Cursor = System.Windows.Forms.Cursors.Hand;
                l2.Click += new System.EventHandler(this.OpenZonasClick);
                l2.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                l2.MouseLeave += new System.EventHandler(this.MouseLeaveAction);

                #endregion

                #region Formularios Online
                System.Windows.Forms.Label lfo = new System.Windows.Forms.Label();
                lfo.Text = "Formulários Online";
                lfo.Location = new Point(7, 10);
                lfo.ForeColor = SystemColors.ControlDark;
                lfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));


                System.Windows.Forms.Label l6 = new System.Windows.Forms.Label();
                l6.Width = 150;
                l6.Text = "Gerar Formulários Online";
                l6.Location = new Point(15, 40);
                l6.ForeColor = Color.Blue;
                l6.Cursor = System.Windows.Forms.Cursors.Hand;
                l6.Click += new System.EventHandler(this.PerguntasAction);
                l6.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                l6.MouseLeave += new System.EventHandler(this.MouseLeaveAction);

                
                System.Windows.Forms.Label l7 = new System.Windows.Forms.Label();
                l7.Width = 150;
                l7.Text = "CheckList Online";
                l7.Location = new Point(15, 70);

                checkBox1 = new System.Windows.Forms.CheckBox();
                checkBox1.Appearance = System.Windows.Forms.Appearance.Button;

                checkBox1.Location = new Point(175, 65);
                checkBox1.BackColor = SystemColors.Control;

                if (Camada_de_Dados.DataBaseCommunicator.FuncsToDataBase.getEstadoCheckListOnline(codp, coda))
                {
                    checkBox1.Checked = true;
                    checkBox1.Text = "Desactivar";
                }
                else
                {
                    checkBox1.Checked = false;
                    checkBox1.Text = "Activar";
                }
                checkBox1.CheckedChanged += checkBox1checkChanged;

                TextBox tb1 = new TextBox();
                tb1.Location = new Point(15, 100);
                tb1.BackColor = Color.White;
                tb1.BorderStyle = BorderStyle.None;
                tb1.ReadOnly = true;
                tb1.ForeColor = SystemColors.ControlDark;
                tb1.Text = "http://"+GestaodeAnalistas.nomeservidor()+":54749"+"/ETdA/Default.aspx?form=CL&usr=" +
                             Camada_de_Dados.ETdA.ETdA.Username + "&anl=" + coda + "&prj" + "=" + codp;
                
                
                System.Windows.Forms.Label l8 = new System.Windows.Forms.Label();
                l8.Width = 150;
                l8.Text = "Ficha de Avaliação Online";
                l8.Location = new Point(15, 130);

                checkBox2 = new System.Windows.Forms.CheckBox();
                checkBox2.Appearance = System.Windows.Forms.Appearance.Button;


                checkBox2.Location = new Point(175, 125);
                checkBox2.BackColor = SystemColors.Control;

                if (Camada_de_Dados.DataBaseCommunicator.FuncsToDataBase.getEstadoFichaAvaliacaoOnline(codp, coda))
                {
                    checkBox2.Checked = true;
                    checkBox2.Text = "Desactivar";
                }
                else
                {
                    checkBox2.Checked = false;
                    checkBox2.Text = "Activar";
                }
                checkBox2.CheckedChanged += checkBox2checkChanged;

                TextBox tb2 = new TextBox();
                tb2.Location = new Point(15, 160);
                tb2.BackColor = Color.White;
                tb2.BorderStyle = BorderStyle.None;
                tb2.ReadOnly = true;
                tb2.ForeColor = SystemColors.ControlDark;
                tb2.Text = "http://" + GestaodeAnalistas.nomeservidor() + ":54749" + "/ETdA/Default.aspx?form=FA&usr=" +
                             Camada_de_Dados.ETdA.ETdA.Username + "&anl=" + coda + "&prj" + "=" + codp;

                System.Windows.Forms.Label l9 = new System.Windows.Forms.Label();
                l9.Width = 150;
                l9.Text = "Questionário Online";
                l9.Location = new Point(15, 190);

                checkBox3 = new System.Windows.Forms.CheckBox();
                checkBox3.Appearance = System.Windows.Forms.Appearance.Button;


                checkBox3.Location = new Point(175, 185);
                checkBox3.BackColor = SystemColors.Control;

                if (Camada_de_Dados.DataBaseCommunicator.FuncsToDataBase.getEstadoQuestionariosOnline(codp, coda))
                {
                    checkBox3.Checked = true;
                    checkBox3.Text = "Desactivar";
                }
                else
                {
                    checkBox3.Checked = false;
                    checkBox3.Text = "Activar";
                }
                checkBox3.CheckedChanged += checkBox3checkChanged;

                TextBox tb3 = new TextBox();
                tb3.Location = new Point(15, 220);
                tb3.BackColor = Color.White;
                tb3.ForeColor = SystemColors.ControlDark;
                tb3.BorderStyle = BorderStyle.None;
                tb3.ReadOnly = true;
                tb3.Text = "http://" + GestaodeAnalistas.nomeservidor() + ":54749" + "/ETdA/Default.aspx?form=QT&usr=" +
                             Camada_de_Dados.ETdA.ETdA.Username + "&anl=" + coda + "&prj" + "=" + codp;
                #endregion

                #region Relatorio
                System.Windows.Forms.Label lrel = new System.Windows.Forms.Label();
                lrel.Text = "Relatório";
                lrel.ForeColor = SystemColors.ControlDark;
                lrel.Location = new Point(7, 18);
                lrel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));

                System.Windows.Forms.Label l3 = new System.Windows.Forms.Label();
                l3.Text = "Gerar Relatorio";
                l3.Location = new Point(15, 48);
                l3.ForeColor = Color.Blue;
                l3.Cursor = System.Windows.Forms.Cursors.Hand;
                l3.Click += new System.EventHandler(this.GerarRelatorio);
                l3.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                l3.MouseLeave += new System.EventHandler(this.MouseLeaveAction);

                #endregion

                #region Dados
                System.Windows.Forms.Label ldados = new System.Windows.Forms.Label();
                ldados.Text = "Dados";
                ldados.ForeColor = SystemColors.ControlDark;
                ldados.Location = new Point(7, 6);
                ldados.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));

                System.Windows.Forms.Label l5 = new System.Windows.Forms.Label();
                l5.Width = 200;
                l5.Text = "Inserir Dados Manualmente";
                l5.Location = new Point(15, 36);
                l5.ForeColor = Color.Blue;
                l5.Cursor = System.Windows.Forms.Cursors.Hand;
                l5.Click += new System.EventHandler(this.InserirManualClick);
                l5.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                l5.MouseLeave += new System.EventHandler(this.MouseLeaveAction);

                System.Windows.Forms.Label l4 = new System.Windows.Forms.Label();
                l4.Width = 150;
                l4.Text = "Importar Dados de Ficheiro";
                l4.Location = new System.Drawing.Point(15, 66);
                l4.ForeColor = Color.Blue;
                l4.Cursor = System.Windows.Forms.Cursors.Hand;
                l4.Click += new System.EventHandler(importer);
                l4.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                l4.MouseLeave += new System.EventHandler(this.MouseLeaveAction);

                #endregion

                #region declaraçoes
                System.Windows.Forms.Panel panelPaginaAnalise;
                System.Windows.Forms.SplitContainer splitContainer2;
                System.Windows.Forms.SplitContainer splitContainer3;
                System.Windows.Forms.Label label8;
                System.Windows.Forms.PictureBox pictureBox1;
                System.Windows.Forms.Label label7;
                System.Windows.Forms.Label label6;
                System.Windows.Forms.Label label5;
                System.Windows.Forms.Label label4;
                System.Windows.Forms.Label label3;
                System.Windows.Forms.Label label2;
                System.Windows.Forms.Label label11;
                System.Windows.Forms.Label label10;
                System.Windows.Forms.Label label9;
                System.Windows.Forms.Panel panel2;
                System.Windows.Forms.Label label1;
                System.Windows.Forms.Panel panel4;
                System.Windows.Forms.Panel panel3;
                panelPaginaAnalise = new System.Windows.Forms.Panel();
                panel2 = new System.Windows.Forms.Panel();
                label1 = new System.Windows.Forms.Label();
                splitContainer2 = new System.Windows.Forms.SplitContainer();
                splitContainer3 = new System.Windows.Forms.SplitContainer();
                label2 = new System.Windows.Forms.Label();
                label3 = new System.Windows.Forms.Label();
                label4 = new System.Windows.Forms.Label();
                label5 = new System.Windows.Forms.Label();
                label6 = new System.Windows.Forms.Label();
                label7 = new System.Windows.Forms.Label();
                pictureBox1 = new System.Windows.Forms.PictureBox();
                label8 = new System.Windows.Forms.Label();
                label9 = new System.Windows.Forms.Label();
                label10 = new System.Windows.Forms.Label();
                label11 = new System.Windows.Forms.Label();
                panel3 = new System.Windows.Forms.Panel();
                panel4 = new System.Windows.Forms.Panel();
                #endregion
                // 
                // panelPaginaAnalise
                // 
                panelPaginaAnalise.BackColor = System.Drawing.Color.White;
                panelPaginaAnalise.Controls.Add(splitContainer2);
                panelPaginaAnalise.Controls.Add(panel2);
                panelPaginaAnalise.Dock = System.Windows.Forms.DockStyle.Fill;
                panelPaginaAnalise.Location = new System.Drawing.Point(0, 0);
                panelPaginaAnalise.Name = "panelPaginaProjecto";
                panelPaginaAnalise.Padding = new System.Windows.Forms.Padding(20);
                panelPaginaAnalise.Size = new System.Drawing.Size(596, 389);
                panelPaginaAnalise.TabIndex = 0;
                // 
                // panel2
                // 
                panel2.Controls.Add(label1);
                panel2.Dock = System.Windows.Forms.DockStyle.Top;
                panel2.Location = new System.Drawing.Point(20, 20);
                panel2.Name = "panel2";
                panel2.Size = new System.Drawing.Size(556, 45);
                panel2.TabIndex = 0;
                // 
                // label1
                // 
                label1.AutoSize = true;
                label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                label1.Location = new System.Drawing.Point(0, 6);
                label1.Name = "label1";
                label1.Size = new System.Drawing.Size(126, 25);
                label1.TabIndex = 0;
                label1.Text = nome_analise;
                // 
                // splitContainer2
                // 
                splitContainer2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
                splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
                splitContainer2.Location = new System.Drawing.Point(20, 65);
                splitContainer2.Margin = new System.Windows.Forms.Padding(0);
                splitContainer2.Name = "splitContainer2";
                // 
                // splitContainer2.Panel1
                // 
                splitContainer2.Panel1.Controls.Add(splitContainer3);
                // 
                // splitContainer2.Panel2
                // 
                splitContainer2.Panel2.BackColor = System.Drawing.Color.White;
                splitContainer2.Panel2.Controls.Add(panel4);
                splitContainer2.Panel2.Controls.Add(panel3);
                splitContainer2.Size = new System.Drawing.Size(556, 304);
                splitContainer2.SplitterDistance = 290;
                splitContainer2.SplitterWidth = 1;
                splitContainer2.TabIndex = 1;
                // 
                // splitContainer3
                // 
                splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
                splitContainer3.Location = new System.Drawing.Point(0, 0);
                splitContainer3.Margin = new System.Windows.Forms.Padding(0);
                splitContainer3.Name = "splitContainer3";
                splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
                // 
                // splitContainer3.Panel1
                // 
                splitContainer3.Panel1.BackColor = System.Drawing.Color.White;
                splitContainer3.Panel1.Controls.Add(lconsulta);
                splitContainer3.Panel1.Controls.Add(l1);
                splitContainer3.Panel1.Controls.Add(l2);
                // 
                // splitContainer3.Panel2
                // 
                splitContainer3.Panel2.BackColor = System.Drawing.Color.White;
                splitContainer3.Size = new System.Drawing.Size(212, 304);
                splitContainer3.SplitterDistance = 90;
                splitContainer3.SplitterWidth = 1;
                splitContainer3.TabIndex = 0;
                splitContainer3.Panel2.Controls.Add(lfo);
                splitContainer3.Panel2.Controls.Add(l6);
                splitContainer3.Panel2.Controls.Add(l7);
                splitContainer3.Panel2.Controls.Add(checkBox1);
                splitContainer3.Panel2.Controls.Add(tb1);
                tb1.Size = new Size((splitContainer3.Size.Width - 20),10);
                splitContainer3.Panel2.Controls.Add(l8);
                splitContainer3.Panel2.Controls.Add(checkBox2);
                splitContainer3.Panel2.Controls.Add(tb2);
                tb2.Size = new Size((splitContainer3.Size.Width - 20), 10);
                splitContainer3.Panel2.Controls.Add(l9);
                splitContainer3.Panel2.Controls.Add(checkBox3);
                splitContainer3.Panel2.Controls.Add(tb3);
                tb3.Size = new Size((splitContainer3.Size.Width - 20), 10);
                
                
                
                
                // 
                // label2
                // 
                label2.AutoSize = true;
                label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                label2.ForeColor = System.Drawing.SystemColors.ControlDark;
                label2.Location = new System.Drawing.Point(4, 10);
                label2.Name = "label2";
                label2.Size = new System.Drawing.Size(139, 18);
                label2.TabIndex = 0;
                label2.Text = "Projectos Recentes";
                // 
                // label3
                // 
                label3.AutoSize = true;
                label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                label3.ForeColor = System.Drawing.Color.Blue;
                label3.Location = new System.Drawing.Point(20, 44);
                label3.Name = "label3";
                label3.Size = new System.Drawing.Size(55, 13);
                label3.TabIndex = 1;
                label3.Text = "Projecto 1";
                label3.Visible = false;
                label3.Cursor = System.Windows.Forms.Cursors.Hand;
                label3.Click += new System.EventHandler(this.OpenProjectClick);
                label3.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                label3.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
                // 
                // label4
                // 
                label4.AutoSize = true;
                label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                label4.ForeColor = System.Drawing.Color.Blue;
                label4.Location = new System.Drawing.Point(20, 71);
                label4.Name = "label4";
                label4.Size = new System.Drawing.Size(55, 13);
                label4.TabIndex = 2;
                label4.Text = "Projecto 2";
                label4.Visible = false;
                label4.Cursor = System.Windows.Forms.Cursors.Hand;
                label4.Click += new System.EventHandler(this.OpenProjectClick);
                label4.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                label4.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
                // 
                // label5
                // 
                label5.AutoSize = true;
                label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                label5.ForeColor = System.Drawing.Color.Blue;
                label5.Location = new System.Drawing.Point(20, 97);
                label5.Name = "label5";
                label5.Size = new System.Drawing.Size(55, 13);
                label5.TabIndex = 3;
                label5.Text = "Projecto 3";
                label5.Visible = false;
                label5.Cursor = System.Windows.Forms.Cursors.Hand;
                label5.Click += new System.EventHandler(this.OpenProjectClick);
                label5.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                label5.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
                // 
                // label6
                // 
                label6.AutoSize = true;
                label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                label6.ForeColor = System.Drawing.Color.Blue;
                label6.Location = new System.Drawing.Point(20, 126);
                label6.Name = "label6";
                label6.Size = new System.Drawing.Size(55, 13);
                label6.TabIndex = 4;
                label6.Text = "Projecto 4";
                label6.Visible = false;
                label6.Cursor = System.Windows.Forms.Cursors.Hand;
                label6.Click += new System.EventHandler(this.OpenProjectClick);
                label6.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                label6.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
                // 
                // label7
                // 
                label7.AutoSize = true;
                label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                label7.ForeColor = System.Drawing.Color.Blue;
                label7.Location = new System.Drawing.Point(20, 157);
                label7.Name = "label7";
                label7.Size = new System.Drawing.Size(55, 13);
                label7.TabIndex = 5;
                label7.Text = "Projecto 5";
                label7.Visible = false;
                label7.Cursor = System.Windows.Forms.Cursors.Hand;
                label7.Click += new System.EventHandler(this.OpenProjectClick);
                label7.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                label7.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
                
                // 
                // label8
                // 
                label8.AutoSize = true;
                label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                label8.ForeColor = System.Drawing.Color.Blue;
                label8.Location = new System.Drawing.Point(38, 22);
                label8.Name = "label8";
                label8.Size = new System.Drawing.Size(94, 16);
                label8.TabIndex = 6;
                label8.Text = "Novo Projecto";
                label8.Cursor = System.Windows.Forms.Cursors.Hand;
                label8.Click += new System.EventHandler(this.CriarProjectoClick);
                label8.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                label8.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
                // 
                // label9
                // 
                label9.AutoSize = true;
                label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                label9.Location = new System.Drawing.Point(18, 14);
                label9.Name = "label9";
                label9.Size = new System.Drawing.Size(131, 22);
                label9.TabIndex = 0;
                label9.Text = "Onde Começar";
                // 
                // panel3
                // 
                panel3.Dock = System.Windows.Forms.DockStyle.Top;
                panel3.Location = new System.Drawing.Point(0, 0);
                panel3.Name = "panel3";
                panel3.Size = new System.Drawing.Size(343, 90);
                panel3.TabIndex = 3;
                panel3.Controls.Add(ldados);
                panel3.Controls.Add(l5);
                panel3.Controls.Add(l4);

                // 
                // panel4
                // 
                panel4.Dock = System.Windows.Forms.DockStyle.Fill;
                panel4.Location = new System.Drawing.Point(0, 73);
                panel4.Name = "panel4";
                panel4.Size = new System.Drawing.Size(343, 231);
                panel4.TabIndex = 4;
                panel4.Padding = new Padding(10, 0, 0, 0);
                panel4.Controls.Add(lrel);
                panel4.Controls.Add(l3);
                
                #endregion

                p.Controls.Add(panelPaginaAnalise);

            }
            tabControl1.SelectedIndex = getTabNumber(codp.ToString() + "." + coda.ToString());
        }

        // s_final
        private int getTabNumber(string cod)
        {
            int i;
            bool found = false;
            for (i = 0; i < tabControl1.Pages.Count && !found; i++)
                if (tabControl1.Pages[i] == cod)
                    found = true;
            return i - 1;
        }

        #endregion

        #region Projectos e Analises Adicionadas

        // s_final
        public static void addProjectoReenc(object sender, EventArgs e)
        {
            igp.addProjecto(sender, e);
        }

        // s_final
        private void addProjecto(object sender, EventArgs e)
        {
            List<string> s = (List<string>)sender;
            TreeNode nodo = new TreeNode();
            nodo.Name = s[0];
            nodo.Text = s[1];
            this.treeView_Projectos.Nodes.Add(nodo);
            initPaginaProjecto(s[1], s[0]);
        }

        // s_final
        public static void addAnaliseReenc(object sender, EventArgs e)
        {
            igp.addAnalise(sender, e);
        }

        // s_final
        private void addAnalise(object sender, EventArgs e)
        {
            string nomeP = null;
            List<string> s = (List<string>)sender;
            bool found = false;
            for (int i = 0; i < treeView_Projectos.Nodes.Count && !found; i++)
                if (treeView_Projectos.Nodes[i].Name == s[0])
                {
                    nomeP = treeView_Projectos.Nodes[i].Text;
                    TreeNode t = new TreeNode();
                    t.Name = s[1];
                    t.Text = s[2];
                    treeView_Projectos.Nodes[i].Nodes.Add(t);
                    found = true;
                }
            initPaginaProjecto(nomeP, s[0]);
            initPaginaAnalise(long.Parse(s[0]), long.Parse(s[1]), s[2]);
        }
        #endregion

        /*
        public static void remProjectoReenc(object sender, EventArgs e)
        {
            igp.remProjecto(sender, e);
        }
        private void remProjecto(object sender, EventArgs e)
        {
        }

        public static void addAnaliseReenc(object sender, EventArgs e)
        {
            igp.addAnalise(sender, e);
        }
        private void addAnalise(object sender, EventArgs e)
        {
        }

        public static void remAnaliseReenc(object sender, EventArgs e)
        {
            igp.remAnalise(sender, e);
        }
        private void remAnalise(object sender, EventArgs e)
        {
        }
        */

        #region Abrir Projectos e Analises (Eventos)

        // s_final
        private void ExpandProjectAction(object sender, TreeViewEventArgs e)
        {
            int index = 0;
            for (int i = 0; i < treeView_Projectos.Nodes.Count; i++)
            {
                if (treeView_Projectos.Nodes[i] != e.Node)
                {
                    if (treeView_Projectos.Nodes[i].IsExpanded)
                        treeView_Projectos.Nodes[i].Collapse();
                }
                else
                    index = i;
            }

            long codProj = long.Parse(e.Node.Name);
            GestaodeProjectos.abreProjecto(codProj);

            if (!indexes.Contains(index))
            {
                treeView_Projectos.Nodes[index].Nodes.RemoveAt(0);
                Dictionary<long, string> cod_names = GestaodeAnalises.getCodeNomeAnalises(codProj);
                foreach (KeyValuePair<long, string> s in cod_names)
                {
                    TreeNode tn = new TreeNode();
                    tn.Name = s.Key.ToString();
                    tn.Text = s.Value;
                    treeView_Projectos.Nodes[index].Nodes.Add(tn);
                }
                indexes.Add(index);
            }
        }

        // s_final
        private void OpenAction(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Level == 1)
            {
                long codp = long.Parse(e.Node.Parent.Name);
                long coda = long.Parse(e.Node.Name);
                GestaodeAnalises.abreAnalise(codp, coda);

                initPaginaAnalise(codp, coda, e.Node.Text);
            }
            else
            {
                initPaginaProjecto(e.Node.Text, e.Node.Name);
            }
        }

        // s_final
        private void OpenProjectClick(object sender, EventArgs e)
        {
            Label l = (Label)sender;
            GestaodeProjectos.abreProjecto(long.Parse(l.Name));

            initPaginaProjecto(l.Text, l.Name);
        }

        // s_final
        private void OpenAnaliseClick(object sender, EventArgs e)
        {
            Label l = (Label)sender;
            long codp = long.Parse(tabControl1.SelectedTab.Name);
            long coda = long.Parse(l.Name);
            GestaodeAnalises.abreAnalise(codp, coda);

            initPaginaAnalise(codp, coda, l.Text);
        }

        #endregion

        #region Criar Projectos e Analises (Eventos)

        // s_final
        private void CriarProjectoClick(object sender, EventArgs e)
        {
            Interface_CriarProjecto.main();
            RefreshInterface();
        }

        // s_final
        private void CriarAnaliseClick(object sender, EventArgs e)
        {
            long cod = long.Parse(tabControl1.SelectedTab.Name);
            string nome = tabControl1.SelectedTab.Text;
            Interface_CriarAnalise.main(cod, nome);
            RefreshInterface();
        }

        #endregion

        #region Analise (Eventos)

        #region Website (Eventos)
        private void checkBox1checkChanged(object sender, EventArgs e)
        {
            string[] cods = tabControl1.SelectedTab.Name.Split('.');
            long codp = long.Parse(cods[0]);
            long coda = long.Parse(cods[1]);

            if (checkBox1.Checked == true)
            {
                checkBox1.Text = "Desactivar";
                Camada_de_Dados.DataBaseCommunicator.FuncsToDataBase.setEstadoCheckListOnline(codp, coda, true);
            }
            else if (checkBox1.Checked == false)
            {
                checkBox1.Text = "Activar";
                Camada_de_Dados.DataBaseCommunicator.FuncsToDataBase.setEstadoCheckListOnline(codp, coda, false);
            }

        }

        private void checkBox2checkChanged(object sender, EventArgs e)
        {
            string[] cods = tabControl1.SelectedTab.Name.Split('.');
            long codp = long.Parse(cods[0]);
            long coda = long.Parse(cods[1]);

            if (checkBox2.Checked == true)
            {
                checkBox2.Text = "Desactivar";
                Camada_de_Dados.DataBaseCommunicator.FuncsToDataBase.setEstadoFichaAvaliacaoOnline(codp, coda, true);
            }
            else if (checkBox2.Checked == false)
            {
                checkBox2.Text = "Activar";
                Camada_de_Dados.DataBaseCommunicator.FuncsToDataBase.setEstadoFichaAvaliacaoOnline(codp, coda, false);
            }

        }

        private void checkBox3checkChanged(object sender, EventArgs e)
        {
            string[] cods = tabControl1.SelectedTab.Name.Split('.');
            long codp = long.Parse(cods[0]);
            long coda = long.Parse(cods[1]);

            if (checkBox3.Checked == true)
            {
                checkBox3.Text = "Desactivar";
                Camada_de_Dados.DataBaseCommunicator.FuncsToDataBase.setEstadoQuestionarioOnline(codp, coda, true);
            }
            else if (checkBox3.Checked == false)
            {
                checkBox3.Text = "Activar";
                Camada_de_Dados.DataBaseCommunicator.FuncsToDataBase.setEstadoQuestionarioOnline(codp, coda, false);
            }

        }
        #endregion

        private void importer(object sender, EventArgs e)
		{
            long codProjecto = long.Parse(tabControl1.SelectedTab.Name.Split('.')[0]);
            long codAnalise = long.Parse(tabControl1.SelectedTab.Name.Split('.')[1]);
            List<Zona> zonas = GestaodeAnalises.getZonasAnalise(codProjecto, codAnalise);
            List<Item> itens = GestaodeAnalises.getItensAnalise(codProjecto, codAnalise);

			Interface_Importer.main(codAnalise,zonas,itens);
		}

        // rdone
        private void GerarRelatorio(object sender, EventArgs e)
        {
            // Tab tem no nome "codP.codA"
            string[] cods = tabControl1.SelectedTab.Name.Split('.');
            long codp = long.Parse(cods[0]);
            long coda = long.Parse(cods[1]);
            string nome = tabControl1.SelectedTab.Text;
            Interface_Relatorio.main(codp, coda, nome, new ETdA.Camada_de_Dados.Classes.Relatorio());
        }

        // rdone
        private void OpenZonasClick(object sender, EventArgs e)
        {
            long codProjecto = long.Parse(tabControl1.SelectedTab.Name.Split('.')[0]);
            long codAnalise = long.Parse(tabControl1.SelectedTab.Name.Split('.')[1]);
            List<string> zonas = GestaodeAnalises.getNomeZonasAnalise(codProjecto, codAnalise);
            string tipo = GestaodeAnalises.getTipoAnalise(codProjecto, codAnalise);
            Interface_CriarAnaliseZonas.main(zonas, tipo, false);
        }

        // rdone
        private void OpenItensClick(object sender, EventArgs e)
        {
            long codProjecto = long.Parse(tabControl1.SelectedTab.Name.Split('.')[0]);
            long codAnalise = long.Parse(tabControl1.SelectedTab.Name.Split('.')[1]);
            List<Item> itens = GestaodeAnalises.getItensAnalise(codProjecto, codAnalise);
            Interface_CriarAnaliseItens.main(itens, false);
        }

        #endregion

        /* Outros */

        // rdone
        private void MouseEnterAction(object sender, EventArgs e)
        {
            Label t = (Label)sender;
            t.Font = new Font(t.Font, FontStyle.Underline);
        }

        // rdone
        private void MouseLeaveAction(object sender, EventArgs e)
        {
            Label t = (Label)sender;
            t.Font = new Font(t.Font, FontStyle.Regular);
        }

        /* Funcoes privadas */

        // rdone
        private void setImage(PictureBox pb, Image i)
        {
            //create a new Bitmap with the proper dimensions
            Bitmap finalImg = new Bitmap(i, 25, 25);

            //create a new Graphics object from the image
            Graphics gfx = Graphics.FromImage(i);

            //clean up the image (take care of any image loss from resizing)
            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //empty the PictureBox
            pb.Image = null;

            //center the new image
            pb.SizeMode = PictureBoxSizeMode.CenterImage;

            //set the new image
            pb.Image = finalImg;
        }

        /* Gestao da Janela */

        // rdone
        public static void main(Boolean start)
        {
            igp = new InterfaceGuestaoProjectos();
            if (start)
                Application.Run(igp);
            else
                igp.Visible = true;
        }

        // rdone
        private void endFrame()
        {
            this.Dispose();
        }

        private void InserirManualClick(object sender, EventArgs e)
        {
            long codProjecto = long.Parse(tabControl1.SelectedTab.Name.Split('.')[0]);
            long codAnalise = long.Parse(tabControl1.SelectedTab.Name.Split('.')[1]);

            Interface_IntroduzirManualmente.main(codProjecto, codAnalise);

        }

        private void PerguntasAction(object sender, EventArgs e)
        {
            long codProjecto = long.Parse(tabControl1.SelectedTab.Name.Split('.')[0]);
            long codAnalise = long.Parse(tabControl1.SelectedTab.Name.Split('.')[1]);
            List<Item> itens = GestaodeAnalises.getItensAnalise(codProjecto, codAnalise);
            List<Zona> zonas = GestaodeAnalises.getZonasAnalise(codProjecto, codAnalise);

            Interface_GestaoFormulariosOnline.main(codAnalise, itens, zonas);
        }

        #region Menu de cima

        #region Analises

        private ToolStripItem[] GetToolStripItemListaAnalises(long codProjecto)
        {
            Dictionary<long, string> ans = GestaodeAnalises.getCodeNomeAnalises(codProjecto);

            ToolStripItem[] ListaAnalises = new ToolStripItem[ans.Count + 2];
            //cabeçalho - Lista Analises
            ToolStripMenuItem listaAnalisesToolStripMenuItem = new ToolStripMenuItem();
            listaAnalisesToolStripMenuItem.Enabled = false;
            listaAnalisesToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            listaAnalisesToolStripMenuItem.Name = "listaAnalisesToolStripMenuItem";
            listaAnalisesToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            listaAnalisesToolStripMenuItem.Text = "Lista de Análises";

            ListaAnalises[0] = listaAnalisesToolStripMenuItem;

            //Separador
            ToolStripSeparator separador = new ToolStripSeparator();
            separador.Name = "Separador";
            separador.Size = new System.Drawing.Size(149, 6);
            ListaAnalises[1] = separador;
            int i = 0;
            foreach (KeyValuePair<long, string> s in ans)
            {
                ToolStripMenuItem ListaAnaliseToolStrip = new System.Windows.Forms.ToolStripMenuItem();
                ListaAnaliseToolStrip.Enabled = true;
                ListaAnaliseToolStrip.Font = new System.Drawing.Font("Segoe UI", 9F);
                ListaAnaliseToolStrip.Name = codProjecto + "." + s.Key.ToString();
                ListaAnaliseToolStrip.Size = new System.Drawing.Size(154, 22);
                ListaAnaliseToolStrip.Text = s.Value;
                ListaAnaliseToolStrip.Click += new EventHandler(ToolStripApagarAnalise);
                ListaAnalises[i + 2] = ListaAnaliseToolStrip;
                i++;
            }
            return (ListaAnalises);
        }

        private ToolStripItem[] GetToolStripMenuCriarAnalise()
        {
            ToolStripItem[] ListaProjecto = GetToolStripListaProjectos();
            for (int i = 0; i < ListaProjecto.Count(); i++)
            {
                ListaProjecto[i].Click += new EventHandler(toolstripMenuCriarAnalise);
            }

            return ListaProjecto;
        }

        private ToolStripMenuItem[] GetToolStripMenuApagarAnaliseTemp()
        {
            ToolStripMenuItem[] ListaProjecto = GetToolStripMenuItemListaProjectos();
            for (int i = 0; i < ListaProjecto.Count(); i++)
            {
                if (GestaodeAnalises.getCodeNomeAnalises(long.Parse(ListaProjecto[i].Name)).Count == 0)
                    ListaProjecto[i].Enabled = false;
                else
                {
                    ToolStripItem[] newtsi = GetToolStripItemListaAnalises(long.Parse(ListaProjecto[i].Name));
                    ListaProjecto[i].DropDownItems.AddRange(newtsi);
                }
            }

            return (ListaProjecto);
        }

        private ToolStripItem[] GetFullToolStripMenuApagarAnalise()
        {
            if (GetToolStripMenuApagarAnaliseTemp().Count() == 0)
            {
                apagarAnáliseToolStripMenuItem.Enabled = false;
                return new ToolStripItem[] { };
            }
            else
            {
                apagarAnáliseToolStripMenuItem.Enabled = true;
            }

            ToolStripMenuItem listaProjectosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            listaProjectosToolStripMenuItem.Enabled = false;
            listaProjectosToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            listaProjectosToolStripMenuItem.Name = "listaProjectosToolStripMenuItem";
            listaProjectosToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            listaProjectosToolStripMenuItem.Text = "Lista de Projectos";

            //Separador

            System.Windows.Forms.ToolStripSeparator separador = new System.Windows.Forms.ToolStripSeparator();
            separador.Name = "Separador";
            separador.Size = new System.Drawing.Size(149, 6);


            System.Windows.Forms.ToolStripItem[] tsi1 = new System.Windows.Forms.ToolStripItem[] { listaProjectosToolStripMenuItem, separador };
            System.Windows.Forms.ToolStripItem[] tsi2 = GetToolStripMenuApagarAnaliseTemp();
            System.Windows.Forms.ToolStripItem[] tsiMerged = new System.Windows.Forms.ToolStripItem[tsi1.Length + tsi2.Length];
            System.Array.Copy(tsi1, tsiMerged, tsi1.Length);
            System.Array.Copy(tsi2, 0, tsiMerged, tsi1.Length, tsi2.Length);

            return (tsiMerged);
        }

        #endregion

        #region Projectos

        private ToolStripItem[] GetToolStripListaProjectos()
        {
            Dictionary<long, string> cod_names = GestaodeProjectos.Cod_names_Projects();

            //manipulação de dropdown menus (enable vs disable) mediante a existencia ou nao de projectos
            if (cod_names.Count == 0)
            {
                apagarAnáliseToolStripMenuItem.Enabled = false;
                apagarProjectoToolStripMenuItem.Enabled = false;
                novaAnaliseToolStripMenuItem.Enabled = false;
                return new ToolStripItem[] { };
            }
            else
            {
                apagarAnáliseToolStripMenuItem.Enabled = true;
                apagarProjectoToolStripMenuItem.Enabled = true;
                novaAnaliseToolStripMenuItem.Enabled = true;
            }
            ToolStripItem[] ListaProjectos = new ToolStripItem[cod_names.Count + 2];
            //cabeçalho - Lista Projectos
            ToolStripMenuItem listaProjectosToolStripMenuItem = new ToolStripMenuItem();
            listaProjectosToolStripMenuItem.Enabled = false;
            listaProjectosToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            listaProjectosToolStripMenuItem.Name = "listaProjectosToolStripMenuItem";
            listaProjectosToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            listaProjectosToolStripMenuItem.Text = "Lista de Projectos";

            ListaProjectos[0] = listaProjectosToolStripMenuItem;

            //Separador
            ToolStripSeparator separador = new ToolStripSeparator();
            separador.Name = "Separador";
            separador.Size = new System.Drawing.Size(149, 6);
            ListaProjectos[1] = separador;

            for (int i = 0; i < cod_names.Count; i++)
            {
                ToolStripMenuItem ListaProjectoToolStrip = new System.Windows.Forms.ToolStripMenuItem();
                ListaProjectoToolStrip.Enabled = true;
                ListaProjectoToolStrip.Font = new System.Drawing.Font("Segoe UI", 9F);
                ListaProjectoToolStrip.Name = cod_names.ElementAt(i).Key.ToString();
                ListaProjectoToolStrip.Size = new System.Drawing.Size(154, 22);
                ListaProjectoToolStrip.Text = cod_names.ElementAt(i).Value;
                ListaProjectos[i + 2] = ListaProjectoToolStrip;
            }
            return (ListaProjectos);
        }

        private ToolStripMenuItem[] GetToolStripMenuItemListaProjectos()
        {
            Dictionary<long, string> cod_names = GestaodeProjectos.Cod_names_Projects();

            ToolStripMenuItem[] ListaProjectos = new ToolStripMenuItem[cod_names.Count];


            for (int i = 0; i < cod_names.Count; i++)
            {
                ToolStripMenuItem ListaProjectoToolStrip = new System.Windows.Forms.ToolStripMenuItem();
                ListaProjectoToolStrip.Enabled = true;
                ListaProjectoToolStrip.Font = new System.Drawing.Font("Segoe UI", 9F);
                ListaProjectoToolStrip.Name = cod_names.ElementAt(i).Key.ToString();
                ListaProjectoToolStrip.Size = new System.Drawing.Size(154, 22);
                ListaProjectoToolStrip.Text = cod_names.ElementAt(i).Value;
                ListaProjectos[i] = ListaProjectoToolStrip;
            }
            return (ListaProjectos);
        }

        private ToolStripItem[] getToolStripApagarProjecto()
        {
            ToolStripItem[] ListaProjectos = GetToolStripListaProjectos();

            for (int i = 2; i < ListaProjectos.Count(); i++)
                ListaProjectos[i].Click += new EventHandler(ToolStripApagarProjecto);

            return ListaProjectos;
        }
        #endregion

        #region Event Handlers Menu

        #region Barra de botoes

        #region tabControl
        private void proximaTab(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex < tabControl1.TabCount - 1)
                this.tabControl1.SelectTab(tabControl1.SelectedIndex + 1);
        }
        private void anteriorTab(object sender, EventArgs e)
        {
            //this.tabControl1.Select(true, true);
            if (tabControl1.SelectedIndex > 0)
                this.tabControl1.SelectTab(tabControl1.SelectedIndex - 1);
        }

        private void fecharTab(object sender, EventArgs e)
        {
            if (tabControl1.TabCount > 0)
                tabControl1.CloseTab(tabControl1.SelectedTab);
        }
        #endregion

        #endregion

        private void toolstripMenuCriarAnalise(object sender, EventArgs e)
        {
            ToolStripItem tsi = (ToolStripItem)sender;

            Interface_CriarAnalise.main(long.Parse(tsi.Name), tsi.Text);

            RefreshInterface();
        }

        private void ToolStripApagarAnalise(object sender, EventArgs e)
        {
            ToolStripItem tsi = (ToolStripItem)sender;
            if (MessageBoxPortuguese.Show("", "Tem a certeza que pretende apagar a análise " + tsi.Text + " e todos os dados relativos a esta?",
                     MessageBoxPortuguese.Button_YesNo, MessageBoxPortuguese.Icon_Question) == System.Windows.Forms.DialogResult.Yes)
            {
                string[] cods = tsi.Name.Split('.');
                long codp = long.Parse(cods[0]);
                long coda = long.Parse(cods[1]);

                //Apagar da base de dados
                Camada_de_Dados.DataBaseCommunicator.FuncsToDataBase.desactivarAnalise(coda);
                //Apagar do ETdA
                GestaodeAnalises.removerAnalise(codp, coda);
                //Actualizar o interface
                RefreshInterface();
                //Apagar Analise do TreeView
                bool found = false;
                for (int i = 0; i < treeView_Projectos.Nodes.Count && !found; i++)
                {
                    String a = treeView_Projectos.Nodes[i].Text;
                    String b = codp.ToString();
                    if (treeView_Projectos.Nodes[i].Name == codp.ToString())
                    {
                        for (int j = 0; j < treeView_Projectos.Nodes[i].Nodes.Count && !found; j++)
                        {
                            if (treeView_Projectos.Nodes[i].Nodes[j].Name == coda.ToString())
                            {
                                treeView_Projectos.Nodes[i].Nodes[j].Remove();
                                found = true;
                            }
                        }
                    }
                }
            }
        }

        private void ToolStripApagarProjecto(object sender, EventArgs e)
        {
            ToolStripItem tsi = (ToolStripItem)sender;
            if (MessageBoxPortuguese.Show("", "Tem a certeza que pretende apagar o projecto " + tsi.Text + " e todos os dados relativos a este?",
                     MessageBoxPortuguese.Button_YesNo, MessageBoxPortuguese.Icon_Question) == System.Windows.Forms.DialogResult.Yes)
            {
                //Apagar da base de dados
                Camada_de_Dados.DataBaseCommunicator.FuncsToDataBase.desactivarProjecto(long.Parse(tsi.Name));
                //Apagar do ETdA
                Camada_de_Dados.ETdA.ETdA.removeProjecto(long.Parse(tsi.Name));
                //Actualizar o interface
                RefreshInterface();
                //Apagar Projecto do Treeview
                bool found = false;
                for (int i = 0; i < treeView_Projectos.Nodes.Count && !found; i++)
                    if (treeView_Projectos.Nodes[i].Name == tsi.Name)
                    {
                        treeView_Projectos.Nodes[i].Remove();
                        found = true;
                    }
            }
        }
        #endregion

        #endregion

        #region Refresh

        public void RefreshInterface()
        {
            //actualização das listas dos menus
            novaAnaliseToolStripMenuItem.DropDownItems.Clear();
            novaAnaliseToolStripMenuItem.DropDownItems.AddRange(GetToolStripMenuCriarAnalise());

            apagarProjectoToolStripMenuItem.DropDownItems.Clear();
            apagarProjectoToolStripMenuItem.DropDownItems.AddRange(getToolStripApagarProjecto());

            apagarAnáliseToolStripMenuItem.DropDownItems.Clear();
            apagarAnáliseToolStripMenuItem.DropDownItems.AddRange(GetFullToolStripMenuApagarAnalise());

            //pagina inicial
            if (tabControl1.Pages.Contains("StartPage"))
            {
                fecharPagina("StartPage");
                //tabControl1.TabPages.RemoveByKey("StartPage");
                //tabControl1.Pages.Remove("StartPage");
                initPaginaInicial();
            }

            //abas 
            foreach (System.Windows.Forms.TabPage tb in tabControl1.TabPages)
            {
                if (tb.Name != "StartPage")
                {
                    string[] cods = tb.Name.Split('.');
                    if (cods.Count() == 1)
                    {
                        long codp = long.Parse(cods[0]);
                        if (tabControl1.Pages.Contains(tb.Name))
                        {
                            fecharPagina(tb.Name);
                            if (GestaodeProjectos.Cod_names_Projects().ContainsKey(codp))
                                initPaginaProjecto(tb.Text, tb.Name);
                        }
                    }
                    else if (cods.Count() == 2)
                    {
                        long codp = long.Parse(cods[0]);
                        long coda = long.Parse(cods[1]);
                        if (tabControl1.Pages.Contains(tb.Name))
                        {
                            fecharPagina(tb.Name);
                            if (GestaodeAnalises.getCodeNomeAnalises(codp).ContainsKey(coda))
                                initPaginaAnalise(codp, coda, tb.Text);
                        }
                    }

                }
            }
        }

        #endregion

        private void verPaginaInicial(object sender, EventArgs e)
        {
            initPaginaInicial();
        }

        private void ajudaClick(object sender, EventArgs e)
        {
            Interface_Ajuda.main();
        }

        private void sobreETdAnalyserClick(object sender, EventArgs e)
        {
            Interface_Creditos.main();
        }

        private void terminarSessao(object sender, EventArgs e)
        {
            GestaodeAnalistas.remove_dados();
            Application.Restart();
        }

        private void fecharAplicacao(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void iniciarInterfaceConfLigBD(object sender, EventArgs e)
        {
            Interface_ConfigurarLigacaoBD.main();
        }

    }
}
