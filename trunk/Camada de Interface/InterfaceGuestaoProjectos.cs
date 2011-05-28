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
        List<TabPage> tabPages;

        public InterfaceGuestaoProjectos()
        {
            InitializeComponent();

            GestaodeProjectos.init();
            initTabPage();

            indexes = new List<int>();
            tabPages = new List<TabPage>();
            initTree();
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

        private void initTabPage()
        {
            System.Windows.Forms.TabPage p =
                new System.Windows.Forms.TabPage();

            this.tabControl1.Controls.Add(p);

            p.Name = "Analises";
            p.Size = new System.Drawing.Size(218, 385);

            System.Windows.Forms.Label Projecto1 = 
                new System.Windows.Forms.Label();

            p.Controls.Add(Projecto1);
        }

        private void ProjectoSelectedAction(object sender, TreeViewEventArgs e)
        {
            int index = 0;
            for (int i = 0; i < treeView_Projectos.Nodes.Count ; i++ )
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

        private void OpenProjectClick(object sender, EventArgs e)
        {
            Label l = (Label) sender;
            //gp.abreProjecto(l.Text);
            endFrame();
        }

        private void endFrame()
        {
            this.Dispose();
        }

        public static void main(Boolean start)
        {
            InterfaceGuestaoProjectos igp = new InterfaceGuestaoProjectos();
            if (start)
                Application.Run(igp);
            else
                igp.Visible = true;
        }

        private void OpenAnaliseAction(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Level == 1)
                MessageBox.Show("Analise : " + e.Node.Text);
            else
                MessageBox.Show("Projecto : " + e.Node.Text);
        }
    }
}
