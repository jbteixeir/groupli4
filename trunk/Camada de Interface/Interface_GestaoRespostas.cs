using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ETdA.Camada_de_Negócio;
using ETdA.Camada_de_Dados.Classes;

namespace ETdA.Camada_de_Interface
{
    public partial class Interface_GestaoRespostas : Form
    {
        private Dictionary<string, List<TipoEscala>> resps;

        public Interface_GestaoRespostas()
        {
            InitializeComponent();
            initTree();
        }

        public static void main()
        {
            Interface_GestaoRespostas igr = new Interface_GestaoRespostas();
            igr.Visible = true;
        }

        private void initTree()
        {
            resps = GestaodeAnalises.getTipResposta();

            for (int i = 0; i < resps.Keys.Count ; i++)
            {
                TreeNode nodo = new TreeNode();
                string tipo = resps.Keys.ElementAt(i);
                nodo.Text = tipo;
                for (int j = 0; j < resps[tipo].Count; j++)
                    nodo.Nodes.Add(resps[tipo][i].Descricao + " " + resps[tipo][i].Numero);
                treeView1.Nodes.Add(nodo);
            }
        }

    }
}
