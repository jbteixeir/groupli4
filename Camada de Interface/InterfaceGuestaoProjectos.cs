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
        List<int> indexes;
        List<string> tabPages;

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
                l2.Text = "Projectos";
                l2.Location = new System.Drawing.Point(7, 70);

                p.Controls.Add(l2);

                List<string> rs = GestaodeProjectos.projectosRecentes();

                int x = 7, y = 60;
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
                //l1.Click += new System.EventHandler(this.OpenProjectClick);
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
            InterfaceGuestaoProjectos igp = new InterfaceGuestaoProjectos();
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
                MessageBox.Show("Analise : " + e.Node.Text);
            else
                MessageBox.Show("Projecto : " + e.Node.Text);
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

            MessageBox.Show("Analise : " + l.Text);
        }
    }
}
