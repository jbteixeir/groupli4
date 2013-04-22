using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ETdAnalyser.Camada_de_Negócio;

namespace ETdAnalyser.CamadaInterface
{
    public partial class InterfaceLogin : Form
    {

        private static InterfaceLogin il;

        public InterfaceLogin()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, EventArgs e)
        {
            string user = textBox1.Text;
            string pass = maskedTextBox1.Text;

            if (GestaodeAnalistas.login(user, pass))
            {	//  Se consegue ligar a base de dados
                if (checkBox1.Checked)
                    GestaodeAnalistas.guarda_dados(user, pass);
                else
                    GestaodeAnalistas.remove_dados();

                closeFrame();
                //umaClassePorreira.Main();
            }
            else
            {	//  Se nao tem connectividade a internet, ou o username ou a pass
                // estao mal

            }

        }

        private void SairActionPerformed(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void RegistarActionPerformed(object sender, EventArgs e)
        {
            InterfaceRegisto.main();
        }

        public static void closeFrame()
        {
            il.Visible = false;
            InterfaceGuestaoProjectos.main(false);
        }

        public static void main()
        {
            var ci = System.Globalization.CultureInfo.InvariantCulture.Clone() as System.Globalization.CultureInfo;
            ci.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            il = new InterfaceLogin();
            il.showNaoExisteFicheiroConfiguracao();
            Application.Run(il);
            
        }

        private void showNaoExisteFicheiroConfiguracao()
        {
            if (!GestaodeAnalistas.existeFicheiroConfiguracao())
                if (MessageBoxPortuguese.Show("Configuração Servidor de Base de Dados", "Ainda não existe uma configuração de ligação ao servidor de base de dados.\n Sem esta o ETdAnalyser não pode ser utilizado. \nPretende configurar agora?",
                     MessageBoxPortuguese.Button_OKCancel, MessageBoxPortuguese.Icon_Question) == System.Windows.Forms.DialogResult.OK)
                {
                    InterfaceConfigurarLigacaoBD.main();
                }
                else
                    Application.Exit();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            string user = textBox1.Text;
            string pass = maskedTextBox1.Text;

            if (checkBox1.Checked)
            {
                if (user != "" && pass != "")
                    GestaodeAnalistas.guarda_dados(user, pass);
            }
            else
                GestaodeAnalistas.remove_dados();
        }

        private void alterarLigacaoBD(object sender, LinkLabelLinkClickedEventArgs e)
        {
            InterfaceConfigurarLigacaoBD.main();
        }

        private void clickAjuda(object sender, EventArgs e)
        {
            InterfaceAjuda.main();
        }

    }
}
