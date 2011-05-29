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
    public partial class Interface_CriarAnaliseItens : Form
    {
        Dictionary<string, string> defaults;
        Dictionary<string, string> alls;

        public Interface_CriarAnaliseItens()
        {
            InitializeComponent();

            defaults = GestaodeAnalises.getItensDefault();
            int i = 0;
            foreach (string s in defaults.Values)
            {
                checkedListBox1.Items.Add(s);
                checkedListBox1.SetItemChecked(i++, true);
            }
        }

        private void ponderacao()
        {
            for ( int i = 0 ; i < checkedListBox1.Items ; i++ )
                if ()
            List<string> ss = new List<string>();
        }

        public static void main()
        {
            Interface_CriarAnaliseItens icai = new Interface_CriarAnaliseItens();
            icai.Visible = true;
        }
    }
}
