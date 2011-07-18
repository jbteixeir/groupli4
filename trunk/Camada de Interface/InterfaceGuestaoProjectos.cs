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
        private List<string> tabPages;

        public InterfaceGuestaoProjectos()
        {
            InitializeComponent();

            GestaodeProjectos.init();
            indexes = new List<int>();
            tabPages = new List<string>();
            initTree();
            initStartPage();
        }

        private void initTree()
        {
            List<string> nomes = GestaodeProjectos.nomesProjectos();

            for (int i = 0; i < nomes.Count; i++)
            {
                TreeNode nodo = new TreeNode();
                nodo.Text = nomes[i];
                nodo.Nodes.Add("");
                this.treeView_Projectos.Nodes.Add(nodo);
            }
        }

        /* Projectos e Analises Adicionadas */

        // rdone
        public static void addProjectoReenc(object sender, EventArgs e)
        {
            igp.addProjecto(sender, e);
        }

        // rdone
        private void addProjecto(object sender, EventArgs e)
        {
            TreeNode nodo = new TreeNode();
            nodo.Text = (string) sender;
            this.treeView_Projectos.Nodes.Add(nodo);
        }

        // rdone
        public static void addAnaliseReenc(object sender, EventArgs e)
        {
            igp.addAnalise(sender, e);
        }

        // rdone
        private void addAnalise(object sender, EventArgs e)
        {
            List<string> s = (List<string>) sender;
            bool found = false;
            for (int i = 0 ; i < treeView_Projectos.Nodes.Count && !found ; i++)
                if (treeView_Projectos.Nodes[i].Text == s[0])
                {
                    TreeNode t = new TreeNode();
                    t.Text = s[1];
                    treeView_Projectos.Nodes[i].Nodes.Add(t);
                    found = true;
                }
            closeTab(tabControl1.SelectedIndex);
            initProgetPage(s[0],long.Parse(s[2]));
        }

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

        /* Gestao das Tabs */

        private void initStartPage()
        {
            if (!tabPages.Contains("StartPage"))
            {
                tabPages.Add("StartPage");
                System.Windows.Forms.TabPage p =
                    new System.Windows.Forms.TabPage();
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
                l1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                l1.Location = new Point(7, 7);
                l1.Size = new System.Drawing.Size(111, 29);

                p.Controls.Add(l1);
                System.Windows.Forms.PictureBox pictureBox1 = new System.Windows.Forms.PictureBox();
                setImage(pictureBox1, global::ETdA.Properties.Resources._1309271576_folder_add);
                pictureBox1.Location = new Point(0, 40);
                pictureBox1.Name = "pictureBox1";
                pictureBox1.Size = new System.Drawing.Size(25, 25);
                pictureBox1.TabIndex = 1;
                pictureBox1.TabStop = false;

                p.Controls.Add(pictureBox1);

                System.Windows.Forms.Label l2 =
                    new System.Windows.Forms.Label();
                l2.Width = 200;
                l2.Text = "Criar Novo Projecto";
                l2.Location = new Point(26, 40);
                l2.Cursor = System.Windows.Forms.Cursors.Hand;
                l2.Click += new System.EventHandler(CriarProjectoClick);
                l2.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                l2.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
                l2.Size = new System.Drawing.Size(250, 25);

                p.Controls.Add(l2);

                System.Windows.Forms.PictureBox pictureBox2 = new System.Windows.Forms.PictureBox();
                setImage(pictureBox2, global::ETdA.Properties.Resources._1309271471_file_add);
                pictureBox2.Location = new Point(0, 79);
                pictureBox2.Name = "pictureBox2";
                pictureBox2.Size = new System.Drawing.Size(25, 25);
                pictureBox2.TabIndex = 1;
                pictureBox2.TabStop = false;

                p.Controls.Add(pictureBox2);

                System.Windows.Forms.Label l3 =
                    new System.Windows.Forms.Label();
                l3.Text = "Abrir Projecto";
                l3.Location = new Point(26, 79);
                l3.Size = new System.Drawing.Size(250, 25);
                l3.Cursor = System.Windows.Forms.Cursors.Hand;
                //l3.Click += new System.EventHandler(this.OpenProjectClick);
                l3.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                l3.MouseLeave += new System.EventHandler(this.MouseLeaveAction);

                p.Controls.Add(l3);

                System.Windows.Forms.Label l4 =
                    new System.Windows.Forms.Label();
                l4.Text = "Projectos Recentes";
                l4.Location = new Point(7, 120);
                l4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                l4.Size = new System.Drawing.Size(250, 29);

                p.Controls.Add(l4);

                List<string> rs = GestaodeProjectos.projectosRecentes();

                int x = 20, y = 130;
                foreach (string s in rs)
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

                            p1.Text = s;
                            p1.Location = new Point(x, y);
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

        // rdone
        private void closeTab(int nome)
        {
            tabControl1.TabPages.RemoveAt(nome);
            tabPages.RemoveAt(nome);
        }

        // rdone
        private void initProgetPage(string nome_projecto, long codp)
        {
            if (!tabPages.Contains("" + codp))
            {
                tabPages.Add("" + codp);
                System.Windows.Forms.TabPage p =
                    new System.Windows.Forms.TabPage();
                p.Name = "" + codp;
                p.AutoScroll = true;
                p.Text = nome_projecto;
                p.Size = new System.Drawing.Size(218, 385);

                ContextMenu m = new ContextMenu();
                m.MenuItems.Add(new MenuItem("Fechar Tab", new EventHandler(fecharTab)));
                p.ContextMenu = m;

                tabControl1.Controls.Add(p);

                System.Windows.Forms.Label l1 =
                    new System.Windows.Forms.Label();
                l1.Text = "Nova Analise";
                l1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, FontStyle.Regular,
                    GraphicsUnit.Point, ((byte)(0)));
                l1.Location = new Point(26, 20);
                l1.Cursor = System.Windows.Forms.Cursors.Hand;
                l1.Click += new System.EventHandler(CriarAnaliseClick);
                l1.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                l1.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
                l1.Size = new System.Drawing.Size(250, 25);

                p.Controls.Add(l1);

                System.Windows.Forms.PictureBox pictureBox1 = new System.Windows.Forms.PictureBox();
                setImage(pictureBox1, global::ETdA.Properties.Resources._1309271471_file_add);
                pictureBox1.Location = new Point(0, 20);
                pictureBox1.Name = "pictureBox1";
                pictureBox1.Size = new System.Drawing.Size(25, 25);

                p.Controls.Add(pictureBox1);

                System.Windows.Forms.Label l2 =
                    new System.Windows.Forms.Label();
                l2.Text = "Analises";
                l2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, FontStyle.Regular,
                    GraphicsUnit.Point, ((byte)(0)));
                l2.Location = new Point(0, 100);
                l2.Size = new System.Drawing.Size(250, 25);

                p.Controls.Add(l2);

                List<string> ans = GestaodeAnalises.getNomeAnalises(codp);

                int x = 7, y = 130;
                foreach (string s in ans)
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

                            p1.Text = s;
                            p1.Location = new Point(x, y);
                            p1.Cursor = System.Windows.Forms.Cursors.Hand;
                            p1.Click += new System.EventHandler(OpenAnaliseClick);
                            p1.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                            p1.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
                            p.Controls.Add(p1);
                        }
                    }
                }
            }
            tabControl1.SelectedIndex = getTabNumber("" + codp);
        }

        // rdone
        private void initAnalisePage(long codp, string nome_analise, long coda)
        {
            if (!tabPages.Contains("" + codp + "." + coda))
            {
                tabPages.Add("" + codp + "." + coda);
                System.Windows.Forms.TabPage p =
                    new System.Windows.Forms.TabPage();
                p.Name = "" + codp + "." + "" + coda;
                p.AutoScroll = true;
                p.Text = nome_analise;
                p.Size = new System.Drawing.Size(218, 385);

                ContextMenu m = new ContextMenu();
                m.MenuItems.Add(new MenuItem("Fechar Tab", new EventHandler(fecharTab)));
                p.ContextMenu = m;

                this.tabControl1.Controls.Add(p);

                System.Windows.Forms.Label lconsulta =
                   new System.Windows.Forms.Label();
                lconsulta.Text = "Consultar";
                lconsulta.Location = new Point(7, 17);
                lconsulta.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, FontStyle.Regular,
                    GraphicsUnit.Point, ((byte)(0)));

                p.Controls.Add(lconsulta);

                System.Windows.Forms.Label l1 =
                    new System.Windows.Forms.Label();
                l1.Text = "Itens";
                l1.Location = new Point(15, 47);
                l1.Cursor = System.Windows.Forms.Cursors.Hand;
                l1.Click += new System.EventHandler(this.OpenItensClick);
                l1.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                l1.MouseLeave += new System.EventHandler(this.MouseLeaveAction);

                p.Controls.Add(l1);

                System.Windows.Forms.Label l2 =
                    new System.Windows.Forms.Label();
                l2.Text = "Zonas";
                l2.Location = new Point(15, 70);
                l2.Cursor = System.Windows.Forms.Cursors.Hand;
                l2.Click += new System.EventHandler(this.OpenZonasClick);
                l2.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                l2.MouseLeave += new System.EventHandler(this.MouseLeaveAction);

                p.Controls.Add(l2);

                System.Windows.Forms.Label lrel =
                   new System.Windows.Forms.Label();
                lrel.Text = "Relatório";
                lrel.Location = new Point(7, 100);
                lrel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, FontStyle.Regular,
                    GraphicsUnit.Point, ((byte)(0)));

                p.Controls.Add(lrel);


                System.Windows.Forms.Label l3 =
                    new System.Windows.Forms.Label();
                l3.Text = "Gerar Relatorio";
                l3.Location = new Point(15, 130);
                l3.Cursor = System.Windows.Forms.Cursors.Hand;
                l3.Click += new System.EventHandler(this.GerarRelatorio);
                l3.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                l3.MouseLeave += new System.EventHandler(this.MouseLeaveAction);

                p.Controls.Add(l3);

                /*
                System.Windows.Forms.Label l4 =
                    new System.Windows.Forms.Label();
                l4.Width = 150;
                l4.Text = "Importar Dados de Ficheiro";
                l4.Location = new System.Drawing.Point(7, 110);
                l4.Cursor = System.Windows.Forms.Cursors.Hand;
                l4.Click += new System.EventHandler(this.OpenProjectClick);
                l4.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                l4.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
                 
                p.Controls.Add(l4);
                * */

                System.Windows.Forms.Label ldados =
                   new System.Windows.Forms.Label();
                ldados.Text = "Dados";
                ldados.Location = new Point(7, 160);
                ldados.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, FontStyle.Regular,
                    GraphicsUnit.Point, ((byte)(0)));

                p.Controls.Add(ldados);

                System.Windows.Forms.Label l5 =
                    new System.Windows.Forms.Label();
                l5.Width = 200;
                l5.Text = "Inserir Dados Manualmente";
                //l5.Location = new System.Drawing.Point(217, 110);
                l5.Location = new Point(15, 190);
                l5.Cursor = System.Windows.Forms.Cursors.Hand;
                l5.Click += new System.EventHandler(this.InserirManualClick);
                l5.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                l5.MouseLeave += new System.EventHandler(this.MouseLeaveAction);

                p.Controls.Add(l5);


                System.Windows.Forms.Label lfo =
                   new System.Windows.Forms.Label();
                lfo.Text = "Formulários Online";
                lfo.Location = new Point(7, 220);
                lfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, FontStyle.Regular,
                    GraphicsUnit.Point, ((byte)(0)));

                p.Controls.Add(lfo);


                System.Windows.Forms.Label l6 =
                    new System.Windows.Forms.Label();
                l6.Width = 150;
                l6.Text = "Gerar Formulários Online";
                l6.Location = new Point(15, 250);
                l6.Cursor = System.Windows.Forms.Cursors.Hand;
                l6.Click += new System.EventHandler(this.PerguntasAction);
                l6.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                l6.MouseLeave += new System.EventHandler(this.MouseLeaveAction);

                p.Controls.Add(l6);

                System.Windows.Forms.Label l7 =
                    new System.Windows.Forms.Label();
                l7.Width = 150;
                l7.Text = "CheckList Online";
                l7.Location = new Point(15, 280);

                p.Controls.Add(l7);

                CheckBox checkBox1 = new System.Windows.Forms.CheckBox();
                checkBox1.Appearance = System.Windows.Forms.Appearance.Button;

                checkBox1.Text = "Activar";
                checkBox1.Location = new Point(175, 275);
                //l8.Click += new System.EventHandler(this.OpenProjectClick);

                p.Controls.Add(checkBox1);

                System.Windows.Forms.Label l8 =
                    new System.Windows.Forms.Label();
                l8.Width = 150;
                l8.Text = "Ficha de Avaliação Online";
                l8.Location = new Point(15, 310);

                p.Controls.Add(l8);

                CheckBox checkBox2 = new System.Windows.Forms.CheckBox();
                checkBox2.Appearance = System.Windows.Forms.Appearance.Button;

                checkBox2.Text = "Activar";
                checkBox2.Location = new Point(175, 305);
                //l8.Click += new System.EventHandler(this.OpenProjectClick);

                p.Controls.Add(checkBox2);

                System.Windows.Forms.Label l9 =
                    new System.Windows.Forms.Label();
                l9.Width = 150;
                l9.Text = "Questionário Online";
                l9.Location = new Point(15, 340);

                p.Controls.Add(l9);

                CheckBox checkBox3 = new System.Windows.Forms.CheckBox();
                checkBox3.Appearance = System.Windows.Forms.Appearance.Button;

                checkBox3.Text = "Activar";
                checkBox3.Location = new Point(175, 335);
                //l8.Click += new System.EventHandler(this.OpenProjectClick);

                p.Controls.Add(checkBox3);

            }
            tabControl1.SelectedIndex = getTabNumber("" + codp + "." + coda);
        }

        // rdone
        private void fecharTab(object sender, EventArgs e)
        {
            closeTab(tabControl1.SelectedIndex);
        }

        private int getTabNumber(string cod)
        {
            int index = 0;
            bool found = false;
            for (int i = 0; i < tabPages.Count && !found; i++)
                if (tabPages[i] == cod)
                    index = i;

            return index;
        }

        /* Abrir Projectos e Analises (Eventos) */

        // rdone
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

            long cod = GestaodeProjectos.abreProjecto(e.Node.Text);

            if (!indexes.Contains(index))
            {
                treeView_Projectos.Nodes[index].Nodes.RemoveAt(0);
                List<string> nomes = GestaodeAnalises.getNomeAnalises(cod);
                foreach (string s in nomes)
                    treeView_Projectos.Nodes[index].Nodes.Add(new TreeNode(s));
                indexes.Add(index);
            }
        }

        // rdone
        private void OpenAction(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Level == 1)
            {
                long codp = GestaodeProjectos.getCodProjecto(e.Node.Parent.Text);
                long coda = GestaodeAnalises.abreAnalise(codp, e.Node.Text);

                initAnalisePage(codp,e.Node.Text,coda);
            }
            else
            {
                long cod = GestaodeProjectos.abreProjecto(e.Node.Text);

                initProgetPage(e.Node.Text, cod);
            }
        }

        // rdone
        private void OpenProjectClick(object sender, EventArgs e)
        {
            Label l = (Label)sender;
            long cod = GestaodeProjectos.abreProjecto(l.Text);

            initProgetPage(l.Text, cod);
        }

        // rdone
        private void OpenAnaliseClick(object sender, EventArgs e)
        {
            Label l = (Label) sender;
            long codp = long.Parse(tabControl1.SelectedTab.Name);
            long coda = GestaodeAnalises.abreAnalise(codp, l.Text);

            initAnalisePage(codp ,l.Text, coda);
        }

        /* Criar Projectos e Analises (Eventos) */

        // rdone
        private void CriarProjectoClick(object sender, EventArgs e)
        {
            Interface_CriarProjecto.main();
        }

        // rdone
        private void CriarAnaliseClick(object sender, EventArgs e)
        {
            long cod = long.Parse(tabControl1.SelectedTab.Name);
            string nome = tabControl1.SelectedTab.Text;
            Interface_CriarAnalise.main(cod,nome);
        }

        /* Analise (Eventos) */

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

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        private void InterfaceGuestaoProjectos_Load(object sender, EventArgs e)
        {

        }

        private void InserirManualClick(object sender, EventArgs e)
        {
            string[] cods = tabControl1.SelectedTab.Name.Split('.');
            long codp = long.Parse(cods[0]);
            long coda = long.Parse(cods[1]);

            Interface_IntroduzirManualmente.main(codp,coda);

        }

        private void PerguntasAction(object sender, EventArgs e)
        {
            long codProjecto = long.Parse(tabControl1.SelectedTab.Name.Split('.')[0]);
            long codAnalise = long.Parse(tabControl1.SelectedTab.Name.Split('.')[1]);
            List<Item> itens = GestaodeAnalises.getItensAnalise(codProjecto, codAnalise);
            List<Zona> zonas = GestaodeAnalises.getZonasAnalise(codProjecto, codAnalise);

            Interface_GestaoFormulariosOnline.main(codAnalise, itens, zonas);
        }
    }
}
