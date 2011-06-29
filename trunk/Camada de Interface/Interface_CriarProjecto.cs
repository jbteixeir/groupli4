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
    public partial class Interface_CriarProjecto : Form
    {
        public Interface_CriarProjecto()
        {
            InitializeComponent();
        }

        private void KeyPressActionPerformed(object sender, KeyPressEventArgs e)
        {
            TextBox t = (TextBox)sender;

            errorProvider1.Clear();
        }

        private void CriarProjectoAction(object sender, EventArgs e)
        {
            string nomeEstabelecimento = textBox1.Text;

            if (GestaodeProjectos.podeCriarProjecto(nomeEstabelecimento))
            {
                GestaodeProjectos.criaProjecto(nomeEstabelecimento);
                endFrame();
            }
            else
                errorProvider1.SetError(this.textBox1, "Projecto já existente.");
        }

        private void CancelarActionPerformed(object sender, EventArgs e)
        {
            endFrame();
        }

        private void endFrame()
        {
            Dispose();
            Close();
        }

        public static void main()
        {
            Interface_CriarProjecto icp = new Interface_CriarProjecto();
            icp.Visible = true;
        }
    }
}
