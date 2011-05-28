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
        public InterfaceGuestaoProjectos()
        {
            InitializeComponent();

            GestaodeProjectos.init();

            initTree();
        }

        private void initTree()
        {
            List<string> nomes = GestaodeProjectos.nomesProjectos();
            TreeNode nodos = new TreeNode();
            this.treeView_Projectos.Nodes.Add(nodos);

            for (int i = 0; i < nomes.Count; i++)
            {
                nodos.Nodes.Add(nomes[i]);
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
    }
}
