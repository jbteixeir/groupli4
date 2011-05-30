using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ETdA.Camada_de_Interface
{
    public partial class Interface_CriarAnaliseZonas : Form
    {
        private delegate void eventoEventHandler(object sender, EventArgs e);

        //[Category(""), Description("Ocorre sempre ...")]
        private static event eventoEventHandler done_action;
        string tipo;

        public Interface_CriarAnaliseZonas(List<string> zonas, string tipo)
        {
            done_action += new eventoEventHandler(
                Camada_de_Interface.Interface_CriarAnalise.ZonasOkReenc);

            InitializeComponent();
            this.tipo = tipo;

            label1.Text = (tipo.Split(' ')[1]);

            foreach (string s in zonas)
                listBox1.Items.Add(s);
        }

        private void CancelarActionPerformed(object sender, EventArgs e)
        {
            end_Frame();
        }

        private void AdicionarActionPerformed(object sender, EventArgs e)
        {
            string s = textBox1.Text;

            String cont = "abcdefghijklmnopqrstuvwxyz" +
            "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            bool found = true;
            for ( int i = 0 ; i < s.Length && found; i++ )
                found = cont.Contains(s[i]);

            if (s == "" || !found)
                MessageBox.Show("Nome da " + tipo + " inválido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (listBox1.Items.Contains(s))
                MessageBox.Show("Já existe uma " + tipo + " " + s + ".", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                listBox1.Items.Add(s);
                textBox1.Text = "";
            }      
        }

        private void OK_ActionPerformed(object sender, EventArgs e)
        {
            List<string> ss = new List<string>();

            for (int i = 0; i < listBox1.Items.Count; i++)
                ss.Add(listBox1.Items[i].ToString());

            done_action(ss, new EventArgs());
            end_Frame();
        }

        private void end_Frame()
        {
            Dispose();
            Close();
        }

        public static void main(List<string> zonas, string tipo)
        {
            Interface_CriarAnaliseZonas icaz = new Interface_CriarAnaliseZonas(zonas,tipo);
            icaz.Visible = true;
        }
    }
}
