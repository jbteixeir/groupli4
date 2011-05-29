using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ETdA.Camada_de_Negócio;

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

        public static void addProjectoReenc(object sender, EventArgs e)
        {
            igp.addProjecto(sender, e);
        }
        private void addProjecto(object sender, EventArgs e)
        {
            TreeNode nodo = new TreeNode();
            nodo.Text = (string) sender;
            this.treeView_Projectos.Nodes.Add(nodo);
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
        private int getTabNumber(string name)
        {
            int index = 0;
            bool found = false;
            for (int i = 0; i < tabPages.Count && !found; i++)
                if (tabPages[i] == name)
                    index = i;

            return index;
        }

        private void initStartPage()
        {
            if (!tabPages.Contains("StartPage"))
            {
                tabPages.Add("StartPage");
                System.Windows.Forms.TabPage p =
                    new System.Windows.Forms.TabPage();

                this.tabControl1.Controls.Add(p);

                p.Text = "StartPage";
                p.Size = new System.Drawing.Size(218, 385);

                System.Windows.Forms.Label l1 =
                    new System.Windows.Forms.Label();
                l1.Text = "Bem Vindo";
                l1.Location = new System.Drawing.Point(7, 7);

                p.Controls.Add(l1);

                System.Windows.Forms.Label l2 =
                    new System.Windows.Forms.Label();
                l2.Width = 200;
                l2.Text = "Criar Novo Projecto";
                l2.Location = new System.Drawing.Point(7, 30);
                l2.Cursor = System.Windows.Forms.Cursors.Hand;
                l2.Click += new System.EventHandler(CriarProjectoClick);
                l2.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                l2.MouseLeave += new System.EventHandler(this.MouseLeaveAction);

                p.Controls.Add(l2);

                System.Windows.Forms.Label l3 =
                    new System.Windows.Forms.Label();
                l3.Text = "Abrir Projecto";
                l3.Location = new System.Drawing.Point(7, 55);
                l3.Cursor = System.Windows.Forms.Cursors.Hand;
                //l3.Click += new System.EventHandler(this.OpenProjectClick);
                l3.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                l3.MouseLeave += new System.EventHandler(this.MouseLeaveAction);

                p.Controls.Add(l3);

                System.Windows.Forms.Label l4 =
                    new System.Windows.Forms.Label();
                l4.Text = "Projectos";
                l4.Location = new System.Drawing.Point(7, 120);

                p.Controls.Add(l4);

                List<string> rs = GestaodeProjectos.projectosRecentes();

                int x = 7, y = 110;
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

        private void initProgetPage(string nome_projecto)
        {
            if (!tabPages.Contains(nome_projecto))
            {
                tabPages.Add(nome_projecto);
                System.Windows.Forms.TabPage p =
                    new System.Windows.Forms.TabPage();

                this.tabControl1.Controls.Add(p);

                p.Text = nome_projecto;
                p.Size = new System.Drawing.Size(218, 385);

                System.Windows.Forms.Label l1 =
                    new System.Windows.Forms.Label();
                l1.Text = "Nova Analise";
                l1.Location = new System.Drawing.Point(20, 20);
                l1.Cursor = System.Windows.Forms.Cursors.Hand;
                l1.Click += new System.EventHandler(CriarAnaliseClick);
                l1.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                l1.MouseLeave += new System.EventHandler(this.MouseLeaveAction);

                p.Controls.Add(l1);

                List<string> ans = GestaodeAnalises.getNomeAnalises();

                int x = 7, y = 60;
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
                            //MessageBox.Show(p1.Width + " " + p1.Height);

                            p1.Text = s;
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
            tabControl1.SelectedIndex = getTabNumber(nome_projecto);
        }

        private void initAnalisePage(string nome_projecto, string nome_analise)
        {
            if (!tabPages.Contains(nome_projecto + "." + nome_analise))
            {
                tabPages.Add(nome_projecto + "." + nome_analise);
                System.Windows.Forms.TabPage p =
                    new System.Windows.Forms.TabPage();

                this.tabControl1.Controls.Add(p);

                p.Text = nome_analise;
                p.Size = new System.Drawing.Size(218, 385);

                System.Windows.Forms.Label l1 =
                    new System.Windows.Forms.Label();
                l1.Text = "Ver Itens";
                l1.Location = new System.Drawing.Point(7, 7);
                l1.Cursor = System.Windows.Forms.Cursors.Hand;
                //l1.Click += new System.EventHandler(this.OpenProjectClick);
                l1.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                l1.MouseLeave += new System.EventHandler(this.MouseLeaveAction);

                p.Controls.Add(l1);

                System.Windows.Forms.Label l2 =
                    new System.Windows.Forms.Label();
                l2.Text = "Ver Zonas";
                l2.Location = new System.Drawing.Point(7, 30);
                l2.Cursor = System.Windows.Forms.Cursors.Hand;
                //l2.Click += new System.EventHandler(this.OpenProjectClick);
                l2.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                l2.MouseLeave += new System.EventHandler(this.MouseLeaveAction);

                p.Controls.Add(l2);

                System.Windows.Forms.Label l3 =
                    new System.Windows.Forms.Label();
                l3.Text = "Gerar Relatorio";
                l3.Location = new System.Drawing.Point(7, 80);
                l3.Cursor = System.Windows.Forms.Cursors.Hand;
                //l3.Click += new System.EventHandler(this.OpenProjectClick);
                l3.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                l3.MouseLeave += new System.EventHandler(this.MouseLeaveAction);

                p.Controls.Add(l3);

                System.Windows.Forms.Label l4 =
                    new System.Windows.Forms.Label();
                l4.Width = 150;
                l4.Text = "Importar Dados de Ficheiro";
                l4.Location = new System.Drawing.Point(7, 110);
                l4.Cursor = System.Windows.Forms.Cursors.Hand;
                //l4.Click += new System.EventHandler(this.OpenProjectClick);
                l4.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                l4.MouseLeave += new System.EventHandler(this.MouseLeaveAction);

                p.Controls.Add(l4);

                System.Windows.Forms.Label l5 =
                    new System.Windows.Forms.Label();
                l5.Width = 200;
                l5.Text = "Importar Dados Manualmente";
                l5.Location = new System.Drawing.Point(217, 110);
                l5.Cursor = System.Windows.Forms.Cursors.Hand;
                //l5.Click += new System.EventHandler(this.OpenProjectClick);
                l5.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                l5.MouseLeave += new System.EventHandler(this.MouseLeaveAction);

                p.Controls.Add(l5);

                System.Windows.Forms.Label l6 =
                    new System.Windows.Forms.Label();
                l6.Width = 150;
                l6.Text = "Gerar Formulários Online";
                l6.Location = new System.Drawing.Point(7, 150);
                l6.Cursor = System.Windows.Forms.Cursors.Hand;
                //l6.Click += new System.EventHandler(this.OpenProjectClick);
                l6.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                l6.MouseLeave += new System.EventHandler(this.MouseLeaveAction);

                p.Controls.Add(l6);

                System.Windows.Forms.Label l7 =
                    new System.Windows.Forms.Label();
                l7.Width = 150;
                l7.Text = "Website CheckList";
                l7.Location = new System.Drawing.Point(190, 150);

                p.Controls.Add(l7);

                CheckBox checkBox1 = new System.Windows.Forms.CheckBox();
                checkBox1.Appearance = System.Windows.Forms.Appearance.Button;

                checkBox1.Text = "Activar";
                checkBox1.Location = new System.Drawing.Point(350, 150);
                //l8.Click += new System.EventHandler(this.OpenProjectClick);

                p.Controls.Add(checkBox1);

                System.Windows.Forms.Label l8 =
                    new System.Windows.Forms.Label();
                l8.Width = 150;
                l8.Text = "Website Ficha de Avaliação";
                l8.Location = new System.Drawing.Point(190, 180);

                p.Controls.Add(l8);

                CheckBox checkBox2 = new System.Windows.Forms.CheckBox();
                checkBox2.Appearance = System.Windows.Forms.Appearance.Button;

                checkBox2.Text = "Activar";
                checkBox2.Location = new System.Drawing.Point(350, 180);
                //l8.Click += new System.EventHandler(this.OpenProjectClick);

                p.Controls.Add(checkBox2);

                System.Windows.Forms.Label l9 =
                    new System.Windows.Forms.Label();
                l9.Width = 150;
                l9.Text = "Website Ficha de Avaliação";
                l9.Location = new System.Drawing.Point(190, 210);

                p.Controls.Add(l9);

                CheckBox checkBox3 = new System.Windows.Forms.CheckBox();
                checkBox3.Appearance = System.Windows.Forms.Appearance.Button;

                checkBox3.Text = "Activar";
                checkBox3.Location = new System.Drawing.Point(350, 210);
                //l8.Click += new System.EventHandler(this.OpenProjectClick);

                p.Controls.Add(checkBox3);

            }
            tabControl1.SelectedIndex = getTabNumber(nome_projecto + "." + nome_analise);
        }

        private void MouseEnterAction(object sender, EventArgs e)
        {
            Label t = (Label)sender;
            t.Font = new Font(t.Font,FontStyle.Underline);
        }

        private void MouseLeaveAction(object sender, EventArgs e)
        {
            Label t = (Label)sender;
            t.Font = new Font(t.Font, FontStyle.Regular);
        }

        public static void main(Boolean start)
        {
            igp = new InterfaceGuestaoProjectos();
            if (start)
                Application.Run(igp);
            else
                igp.Visible = true;
        }

        private void endFrame()
        {
            this.Dispose();
        }

        private void ProjectoSelectedAction(object sender, TreeViewEventArgs e)
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

            GestaodeProjectos.abreProjecto(e.Node.Text);

            if (!indexes.Contains(index))
            {
                treeView_Projectos.Nodes[index].Nodes.RemoveAt(0);
                List<string> nomes = GestaodeAnalises.getNomeAnalises();
                foreach (string s in nomes)
                    treeView_Projectos.Nodes[index].Nodes.Add(new TreeNode(s));
                indexes.Add(index);
            }
        }

        private void OpenAnaliseAction(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Level == 1)
            {
                initAnalisePage(e.Node.Parent.Text ,e.Node.Text);
            }
            else
            {
                GestaodeProjectos.abreProjecto(e.Node.Text);

                initProgetPage(e.Node.Text);
            }
        }

        private void OpenProjectClick(object sender, EventArgs e)
        {
            Label l = (Label)sender;
            GestaodeProjectos.abreProjecto(l.Text);

            initProgetPage(l.Text);
        }

        private void OpenAnaliseClick(object sender, EventArgs e)
        {
            Label l = (Label) sender;

            initAnalisePage(tabControl1.SelectedTab.Text ,l.Text);
        }

        private void CriarProjectoClick(object sender, EventArgs e)
        {
            Interface_CriarProjecto.main();
        }

        private void CriarAnaliseClick(object sender, EventArgs e)
        {
            Interface_CriarAnalise.main();
        }
    }
}
