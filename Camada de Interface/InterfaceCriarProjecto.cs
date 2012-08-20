using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ETdAnalyser.Camada_de_Negócio;

namespace ETdAnalyser.Camada_de_Interface
{
    public partial class InterfaceCriarProjecto : Form
    {
        // s_final
        public InterfaceCriarProjecto()
        {
            InitializeComponent();
        }

        // s_final
        private void KeyPressActionPerformed(object sender, KeyPressEventArgs e)
        {
            TextBox t = (TextBox)sender;

            errorProvider1.Clear();
        }

        // s_final
        private void CriarProjectoAction(object sender, EventArgs e)
        {
            string nomeEstabelecimento = textBox1.Text;
            bool valido = nomeValido(nomeEstabelecimento);

            if (valido && GestaodeProjectos.podeCriarProjecto(nomeEstabelecimento))
            {
                GestaodeProjectos.criaProjecto(nomeEstabelecimento);
                endFrame();
            }
            else if (!valido)
                errorProvider1.SetError(this.textBox1, "Nome do projecto inválido.\n\n(Anpeas letras, números e \"_-/ e espaços\").");
            else
                errorProvider1.SetError(this.textBox1, "Projecto já existente.");
        }

        // s_final
        private bool nomeValido(string p)
        {
            if (p == "") return false;
            string possiveis = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVKWXYZ0123456789_-/ áàãâéêíóõúÁÀÃÂÉÊÍÓÕÚçÇ";
            bool found = true;
            for (int i = 0; i < p.Length && found; i++)
                found = possiveis.Contains(p[i]);
            return found;
        }

        // s_final
        private void CancelarActionPerformed(object sender, EventArgs e)
        {
            endFrame();
        }

        // s_final
        private void endFrame()
        {
            Dispose();
            Close();
        }

        // s_final
        public static void main()
        {
            InterfaceCriarProjecto icp = new InterfaceCriarProjecto();
            //icp.Visible = true;
            icp.ShowDialog();
        }
    }
}
