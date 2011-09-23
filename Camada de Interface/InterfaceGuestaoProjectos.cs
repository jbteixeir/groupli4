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
            initStartPage();
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
        private void closeTab(int nome)
        {
            if (tabControl1.TabPages[nome]!=null)
                tabControl1.TabPages.RemoveAt(nome);
            if(tabControl1.Pages[nome]!=null)
                tabControl1.Pages.RemoveAt(nome);
        }

        // s_final
        private void initStartPage()
        {
            if (!tabControl1.Pages.Contains("StartPage"))
            {
                tabControl1.AddPage("StartPage");
                System.Windows.Forms.TabPage p = new System.Windows.Forms.TabPage();
                p.Name = "StartPage";
                p.AutoScroll = true;

                ContextMenu m = new ContextMenu();
                m.MenuItems.Add(new MenuItem("Fechar Tab", new EventHandler(fecharTab)));
                p.ContextMenu = m;

                this.tabControl1.Controls.Add(p);

                p.Text = "StartPage";
                p.Size = new System.Drawing.Size(218, 385);

                System.Windows.Forms.Label l1 =
                    new System.Windows.Forms.Label();
                l1.Text = "Bem Vindo";
                l1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                l1.Location = new System.Drawing.Point(7, 7);
                l1.Size = new System.Drawing.Size(111, 29);

                p.Controls.Add(l1);
                System.Windows.Forms.PictureBox pictureBox1 = new System.Windows.Forms.PictureBox();
                setImage(pictureBox1, global::ETdA.Properties.Resources._1309271576_folder_add);
                pictureBox1.Location = new System.Drawing.Point(0, 40);
                pictureBox1.Name = "pictureBox1";
                pictureBox1.Size = new System.Drawing.Size(25, 25);
                pictureBox1.TabIndex = 1;
                pictureBox1.TabStop = false;

                p.Controls.Add(pictureBox1);

                System.Windows.Forms.Label l2 =
                    new System.Windows.Forms.Label();
                l2.Width = 200;
                l2.Text = "Criar Novo Projecto";
                l2.Location = new System.Drawing.Point(26, 40);
                l2.Cursor = System.Windows.Forms.Cursors.Hand;
                l2.Click += new System.EventHandler(CriarProjectoClick);
                l2.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                l2.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
                l2.Size = new System.Drawing.Size(250, 25);

                p.Controls.Add(l2);

                System.Windows.Forms.PictureBox pictureBox2 = new System.Windows.Forms.PictureBox();
                setImage(pictureBox2, global::ETdA.Properties.Resources._1309271471_file_add);
                pictureBox2.Location = new System.Drawing.Point(0, 79);
                pictureBox2.Name = "pictureBox2";
                pictureBox2.Size = new System.Drawing.Size(25, 25);
                pictureBox2.TabIndex = 1;
                pictureBox2.TabStop = false;

                p.Controls.Add(pictureBox2);

                System.Windows.Forms.Label l3 =
                    new System.Windows.Forms.Label();
                l3.Text = "Abrir Projecto";
                l3.Location = new System.Drawing.Point(26, 79);
                l3.Size = new System.Drawing.Size(250, 25);
                l3.Cursor = System.Windows.Forms.Cursors.Hand;
                //l3.Click += new System.EventHandler(this.OpenProjectClick);
                l3.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                l3.MouseLeave += new System.EventHandler(this.MouseLeaveAction);

                p.Controls.Add(l3);

                System.Windows.Forms.Label l4 =
                    new System.Windows.Forms.Label();
                l4.Text = "Projectos Recentes";
                l4.Location = new System.Drawing.Point(7, 120);
                l4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                l4.Size = new System.Drawing.Size(250, 29);

                p.Controls.Add(l4);

                Dictionary<long, string> rs = GestaodeProjectos.projectosRecentes();

                int x = 20, y = 130;
                foreach (KeyValuePair<long,string> s in rs)
                {
                    if (x + 10 < Size.Width)
                    {
                        if (y + 10 >= this.Size.Height)
                            x = 50;
                        else
                        {
                            y += 30;
                            System.Windows.Forms.Label p1 =
                                new System.Windows.Forms.Label();

                            p1.Text = s.Value;
                            p1.Name = s.Key.ToString();
                            p1.Location = new System.Drawing.Point(x, y);
                            p1.Cursor = System.Windows.Forms.Cursors.Hand;
                            p1.Click += new System.EventHandler(this.OpenProjectClick);
                            p1.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                            p1.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
                            p.Controls.Add(p1);
                        }
                    }
                }
            }
            tabControl1.SelectedIndex = getTabNumber("StartPage");
        }

        // s_final
        private void initProgetPage(string nome_projecto, string codp)
        {
            if (!tabControl1.Pages.Contains(codp))
            {
                tabControl1.AddPage(codp);
                System.Windows.Forms.TabPage p = new System.Windows.Forms.TabPage();
                p.Name = codp;
                p.AutoScroll = true;
                p.Text = nome_projecto;
                p.Size = new System.Drawing.Size(218, 385);

                ContextMenu m = new ContextMenu();
                m.MenuItems.Add(new MenuItem("Fechar Tab", new EventHandler(fecharTab)));
                p.ContextMenu = m;

                tabControl1.Controls.Add(p);

                System.Windows.Forms.Label l1 = new System.Windows.Forms.Label();
                l1.Text = "Nova Analise";
                l1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                l1.Location = new System.Drawing.Point(26, 20);
                l1.Cursor = System.Windows.Forms.Cursors.Hand;
                l1.Click += new System.EventHandler(CriarAnaliseClick);
                l1.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                l1.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
                l1.Size = new System.Drawing.Size(250, 25);

                p.Controls.Add(l1);

                System.Windows.Forms.PictureBox pictureBox1 = new System.Windows.Forms.PictureBox();
                setImage(pictureBox1, global::ETdA.Properties.Resources._1309271471_file_add);
                pictureBox1.Location = new System.Drawing.Point(0, 20);
                pictureBox1.Size = new System.Drawing.Size(25, 25);

                p.Controls.Add(pictureBox1);

                System.Windows.Forms.Label l2 = new System.Windows.Forms.Label();
                l2.Text = "Analises";
                l2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                l2.Location = new System.Drawing.Point(0, 100);
                l2.Size = new System.Drawing.Size(250, 25);

                p.Controls.Add(l2);

                Dictionary<long,string> ans = GestaodeAnalises.getCodeNomeAnalises(long.Parse(codp));

                int x = 7, y = 130;
                foreach (KeyValuePair<long,string> s in ans)
                {
                    if (x + 10 < Size.Width)
                    {
                        if (y + 10 >= this.Size.Height)
                            x = 50;
                        else
                        {
                            y += 35;
                            System.Windows.Forms.Label p1 =
                                new System.Windows.Forms.Label();

                            p1.Height = 30;

                            p1.Name = s.Key.ToString();
                            p1.Text = s.Value;
                            p1.Location = new System.Drawing.Point(x, y);
                            p1.Cursor = System.Windows.Forms.Cursors.Hand;
                            p1.Click += new System.EventHandler(OpenAnaliseClick);
                            p1.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                            p1.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
                            p.Controls.Add(p1);
                        }
                    }
                }
            }
            tabControl1.SelectedIndex = getTabNumber(codp);
        }

        // s_final
        private void initAnalisePage(long codp, long coda, string nome_analise)
        {
            if (!tabControl1.Pages.Contains(codp.ToString() + "." + coda.ToString()))
            {
                tabControl1.AddPage(codp.ToString() + "." + coda.ToString());
                System.Windows.Forms.TabPage p = new System.Windows.Forms.TabPage();
                p.Name = codp.ToString() + "." + coda.ToString();
                p.AutoScroll = true;
                p.Text = nome_analise;
                p.Size = new System.Drawing.Size(218, 385);

                ContextMenu m = new ContextMenu();
                m.MenuItems.Add(new MenuItem("Fechar Tab", new EventHandler(fecharTab)));
                p.ContextMenu = m;

                this.tabControl1.Controls.Add(p);

                System.Windows.Forms.Label lconsulta = new System.Windows.Forms.Label();
                lconsulta.Text = "Consultar";
                lconsulta.Location = new Point(7, 17);
                lconsulta.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, FontStyle.Regular,GraphicsUnit.Point, ((byte)(0)));

                p.Controls.Add(lconsulta);

                System.Windows.Forms.Label l1 = new System.Windows.Forms.Label();
                l1.Text = "Itens";
                l1.Location = new Point(15, 47);
                l1.Cursor = System.Windows.Forms.Cursors.Hand;
                l1.Click += new System.EventHandler(this.OpenItensClick);
                l1.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                l1.MouseLeave += new System.EventHandler(this.MouseLeaveAction);

                p.Controls.Add(l1);

                System.Windows.Forms.Label l2 = new System.Windows.Forms.Label();
                l2.Text = "Zonas";
                l2.Location = new Point(15, 70);
                l2.Cursor = System.Windows.Forms.Cursors.Hand;
                l2.Click += new System.EventHandler(this.OpenZonasClick);
                l2.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                l2.MouseLeave += new System.EventHandler(this.MouseLeaveAction);

                p.Controls.Add(l2);

                System.Windows.Forms.Label lrel = new System.Windows.Forms.Label();
                lrel.Text = "Relatório";
                lrel.Location = new Point(7, 100);
                lrel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, FontStyle.Regular,GraphicsUnit.Point, ((byte)(0)));

                p.Controls.Add(lrel);


                System.Windows.Forms.Label l3 = new System.Windows.Forms.Label();
                l3.Text = "Gerar Relatorio";
                l3.Location = new Point(15, 130);
                l3.Cursor = System.Windows.Forms.Cursors.Hand;
                l3.Click += new System.EventHandler(this.GerarRelatorio);
                l3.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                l3.MouseLeave += new System.EventHandler(this.MouseLeaveAction);

                p.Controls.Add(l3);

                System.Windows.Forms.Label ldados = new System.Windows.Forms.Label();
                ldados.Text = "Dados";
                ldados.Location = new Point(7, 160);
                ldados.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, FontStyle.Regular,GraphicsUnit.Point, ((byte)(0)));

                p.Controls.Add(ldados);

                System.Windows.Forms.Label l5 = new System.Windows.Forms.Label();
                l5.Width = 200;
                l5.Text = "Inserir Dados Manualmente";
                l5.Location = new Point(15, 190);
                l5.Cursor = System.Windows.Forms.Cursors.Hand;
                l5.Click += new System.EventHandler(this.InserirManualClick);
                l5.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                l5.MouseLeave += new System.EventHandler(this.MouseLeaveAction);

                p.Controls.Add(l5);

                System.Windows.Forms.Label l4 = new System.Windows.Forms.Label();
                l4.Width = 150;
                l4.Text = "Importar Dados de Ficheiro";
                l4.Location = new System.Drawing.Point(15, 220);
                l4.Cursor = System.Windows.Forms.Cursors.Hand;
                l4.Click += new System.EventHandler(importer);
                l4.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                l4.MouseLeave += new System.EventHandler(this.MouseLeaveAction);

                p.Controls.Add(l4);


                System.Windows.Forms.Label lfo = new System.Windows.Forms.Label();
                lfo.Text = "Formulários Online";
                lfo.Location = new Point(7, 250);
                lfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, FontStyle.Regular,GraphicsUnit.Point, ((byte)(0)));

                p.Controls.Add(lfo);

                #region Formularios Online

                System.Windows.Forms.Label l6 = new System.Windows.Forms.Label();
                l6.Width = 150;
                l6.Text = "Gerar Formulários Online";
                l6.Location = new Point(15, 280);
                l6.Cursor = System.Windows.Forms.Cursors.Hand;
                l6.Click += new System.EventHandler(this.PerguntasAction);
                l6.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                l6.MouseLeave += new System.EventHandler(this.MouseLeaveAction);

                p.Controls.Add(l6);

                System.Windows.Forms.Label l7 = new System.Windows.Forms.Label();
                l7.Width = 150;
                l7.Text = "CheckList Online";
                l7.Location = new Point(15, 310);

                p.Controls.Add(l7);

                checkBox1 = new System.Windows.Forms.CheckBox();
                checkBox1.Appearance = System.Windows.Forms.Appearance.Button;

                
                checkBox1.Location = new Point(175, 305);
                
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
                p.Controls.Add(checkBox1);

                System.Windows.Forms.Label l8 = new System.Windows.Forms.Label();
                l8.Width = 150;
                l8.Text = "Ficha de Avaliação Online";
                l8.Location = new Point(15, 340);

                p.Controls.Add(l8);

                checkBox2 = new System.Windows.Forms.CheckBox();
                checkBox2.Appearance = System.Windows.Forms.Appearance.Button;

                
                checkBox2.Location = new Point(175, 335);
                
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
                p.Controls.Add(checkBox2);

                System.Windows.Forms.Label l9 = new System.Windows.Forms.Label();
                l9.Width = 150;
                l9.Text = "Questionário Online";
                l9.Location = new Point(15, 370);

                p.Controls.Add(l9);

                checkBox3 = new System.Windows.Forms.CheckBox();
                checkBox3.Appearance = System.Windows.Forms.Appearance.Button;

                
                checkBox3.Location = new Point(175, 365);
                
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
                p.Controls.Add(checkBox3);

                #endregion

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
            initProgetPage(s[1], s[0]);
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
            List<string> s = (List<string>) sender;
            bool found = false;
            for (int i = 0 ; i < treeView_Projectos.Nodes.Count && !found ; i++)
                if (treeView_Projectos.Nodes[i].Name == s[0])
                {
                    nomeP = treeView_Projectos.Nodes[i].Text;
                    TreeNode t = new TreeNode();
                    t.Name = s[1];
                    t.Text = s[2];
                    treeView_Projectos.Nodes[i].Nodes.Add(t);
                    found = true;
                }
            closeTab(tabControl1.SelectedIndex);
            initProgetPage(nomeP,s[0]);
            initAnalisePage(long.Parse(s[0]), long.Parse(s[1]), s[2]);
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
                Dictionary<long,string> cod_names = GestaodeAnalises.getCodeNomeAnalises(codProj);
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

                initAnalisePage(codp, coda, e.Node.Text);
            }
            else
            {
                initProgetPage(e.Node.Text, e.Node.Name);
            }
        }

        // s_final
        private void OpenProjectClick(object sender, EventArgs e)
        {
            Label l = (Label)sender;
            GestaodeProjectos.abreProjecto(long.Parse(l.Name));

            initProgetPage(l.Text, l.Name);
        }

        // s_final
        private void OpenAnaliseClick(object sender, EventArgs e)
        {
            Label l = (Label) sender;
            long codp = long.Parse(tabControl1.SelectedTab.Name);
            long coda = long.Parse(l.Name);
            GestaodeAnalises.abreAnalise(codp, coda);

            initAnalisePage(codp, coda, l.Text);
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
            Interface_CriarAnalise.main(cod,nome);
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
            else if (checkBox1.Checked == false)
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
            else if (checkBox1.Checked == false)
            {
                checkBox3.Text = "Activar";
                Camada_de_Dados.DataBaseCommunicator.FuncsToDataBase.setEstadoQuestionarioOnline(codp, coda, false);
            }

        }
        #endregion

        private void importer(object sender, EventArgs e)
		{
			Interface_Importer.main(long.Parse(tabControl1.SelectedTab.Name.Split('.')[1]));
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
            List<string> zonas = GestaodeAnalises.getNomeZonasAnalise(codProjecto,codAnalise);
            string tipo = GestaodeAnalises.getTipoAnalise(codProjecto,codAnalise);
            Interface_CriarAnaliseZonas.main(zonas,tipo,false);
        }

        // rdone
        private void OpenItensClick(object sender, EventArgs e)
        {
            long codProjecto = long.Parse(tabControl1.SelectedTab.Name.Split('.')[0]);
            long codAnalise = long.Parse(tabControl1.SelectedTab.Name.Split('.')[1]);
            List<Item> itens = GestaodeAnalises.getItensAnalise(codProjecto,codAnalise);
            Interface_CriarAnaliseItens.main(itens,false);
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

            Interface_IntroduzirManualmente.main(codProjecto,codAnalise);

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
                ListaAnaliseToolStrip.Click +=new EventHandler(ToolStripApagarAnalise);
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
                ToolStripItem[] newtsi = GetToolStripItemListaAnalises(long.Parse(ListaProjecto[i].Name));
                ListaProjecto[i].DropDownItems.AddRange(newtsi);
            }

            return (ListaProjecto);
        }

        private ToolStripItem[] GetFullToolStripMenuApagarAnalise()
        {
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

            for(int i=2; i < ListaProjectos.Count(); i++)
                ListaProjectos[i].Click += new EventHandler(ToolStripApagarProjecto);

            return ListaProjectos;
        }
        #endregion

        #region Event Handlers Menu

        #region Barra de botoes

        #region tabControl
        private void proximaTab(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex < tabControl1.TabCount -1)
                this.tabControl1.SelectTab(tabControl1.SelectedIndex + 1);
        }
        private void anteriorTab(object sender, EventArgs e)
        {
            //this.tabControl1.Select(true, true);
            if(tabControl1.SelectedIndex > 0)
                this.tabControl1.SelectTab(tabControl1.SelectedIndex -1);
        }

        private void fecharTab(object sender, EventArgs e)
        { 
            if(tabControl1.TabCount > 0)
                tabControl1.TabPages.Remove(tabControl1.SelectedTab);
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
            novaAnaliseToolStripMenuItem.DropDownItems.Clear();
            novaAnaliseToolStripMenuItem.DropDownItems.AddRange(GetToolStripMenuCriarAnalise());

            apagarProjectoToolStripMenuItem.DropDownItems.Clear();
            apagarProjectoToolStripMenuItem.DropDownItems.AddRange(getToolStripApagarProjecto());

            apagarAnáliseToolStripMenuItem.DropDownItems.Clear();
            apagarAnáliseToolStripMenuItem.DropDownItems.AddRange(GetFullToolStripMenuApagarAnalise());
        }

        #endregion
    }
}
