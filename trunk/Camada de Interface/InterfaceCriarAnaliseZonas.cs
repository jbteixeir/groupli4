using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ETdAnalyser.CamadaInterface
{
    public partial class InterfaceCriarAnaliseZonas : Form
    {
        private delegate void eventoEventHandler(object sender, EventArgs e);

        //[Category(""), Description("Ocorre sempre ...")]
        private static event eventoEventHandler done_action;
        string tipo;

        // rdone
        public InterfaceCriarAnaliseZonas(List<string> zonas, string tipo, bool b)
        {
            done_action += new eventoEventHandler(
                CamadaInterface.InterfaceCriarAnalise.ZonasOkReenc);

            InitializeComponent();
            this.tipo = tipo;
            this.Text = "Criar Análise - " + tipo;
            this.label1.Text = tipo;

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
                MessageBox.Show("Nome inválido\n\n(Apenas letras, números e \"_\")", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (listBox1.Items.Contains(s))
                MessageBox.Show("Já existe o nome " + s + ".", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            InterfaceCriarAnaliseZonas icaz = new InterfaceCriarAnaliseZonas(zonas,tipo,b);
            icaz.ShowDialog();
        }
    }
}
