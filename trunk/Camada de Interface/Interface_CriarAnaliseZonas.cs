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

        // rdone
        public Interface_CriarAnaliseZonas(List<string> zonas, string tipo, bool b)
        {
            done_action += new eventoEventHandler(
                Camada_de_Interface.Interface_CriarAnalise.ZonasOkReenc);

            InitializeComponent();
            this.tipo = tipo;

            foreach (string s in zonas)
                listBox1.Items.Add(s);

            if (!b)
            {
                textBox1.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
            }
        }

        // rdone
        private void CancelarActionPerformed(object sender, EventArgs e)
        {
            end_Frame();
        }

        // rdone
        private void AdicionarActionPerformed(object sender, EventArgs e)
        {
            string s = textBox1.Text;

            bool valido = nomeZonaValido(s);

            if (!valido)
                MessageBox.Show("Nome da " + tipo + " inválido\n\n(Anpeas letras, números e \"_\")", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (listBox1.Items.Contains(s))
                MessageBox.Show("Já existe uma " + tipo + " " + s + ".", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                listBox1.Items.Add(s);
                textBox1.Text = "";
            }      
        }

        // rdone
        private bool nomeZonaValido(string p)
        {
            if (p == "") return false;
            string possiveis = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVKWXYZ0123456789" +
                             "áàãâéèêíìîóòôõúùûçÁÀÂÃÉÈÊÍÌÎÓÒÕÔÚÙÛÇ ,.;:/()[]{}'?!_-|\\+ºª'";
            bool found = true;
            for (int i = 0; i < p.Length && found; i++)
                found = possiveis.Contains(p[i]);
            return found;
        }

        // rdone
        private void OK_ActionPerformed(object sender, EventArgs e)
        {
            List<string> ss = new List<string>();

            for (int i = 0; i < listBox1.Items.Count; i++)
                ss.Add(listBox1.Items[i].ToString());

            done_action(ss, new EventArgs());
            end_Frame();
        }

        // rdone
        private void end_Frame()
        {
            Dispose();
            Close();
        }

        // rdone
        public static void main(List<string> zonas, string tipo, bool b)
        {
            Interface_CriarAnaliseZonas icaz = new Interface_CriarAnaliseZonas(zonas,tipo,b);
            icaz.ShowDialog();
        }
    }
}
