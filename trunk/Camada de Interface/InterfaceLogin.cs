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
    public partial class InterfaceLogin : Form 
    {
        public InterfaceLogin()
        {
            InitializeComponent();
			label1.Visible = false;
			label2.Visible = false;
			label3.Visible = false;
        }

		private void Login_Click(object sender, EventArgs e)
		{
			string user = textBox1.Text;
			string pass = maskedTextBox1.Text;

			if (GestaodeAnalistas.login(user, pass))
			{	//  Se consegue ligar a base de dados
				closeFrame();
				//umaClassePorreira.Main();
			}
			else
			{	//  Se nao tem connectividade a internet, ou o username ou a pass
				// estao mal
				label1.Visible = true;
				label2.Visible = true;
				label3.Visible = true;
			}

		}

        private void SairActionPerformed(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void RegistarActionPerformed(object sender, EventArgs e)
        {
            //InterfaceRegisto ir = new InterfaceRegisto();
            InterfaceRegisto.main();
        }

        private void closeFrame()
        {
            Dispose();
            Close();
            InterfaceGuestaoProjectos.main(false);
        }

        public static void main()
        {
            InterfaceLogin il = new InterfaceLogin();
            Application.Run(il);
        }
    }
}
