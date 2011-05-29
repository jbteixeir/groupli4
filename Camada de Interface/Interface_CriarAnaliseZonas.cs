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

        public Interface_CriarAnaliseZonas(List<string> zonas)
        {
            done_action += new eventoEventHandler(
                Camada_de_Interface.Interface_CriarAnalise.ZonasOkReenc);

            InitializeComponent();

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

            listBox1.Items.Add(s);
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

        public static void main(List<string> zonas)
        {
            Interface_CriarAnaliseZonas icaz = new Interface_CriarAnaliseZonas(zonas);
            icaz.Visible = true;
        }
    }
}
