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
			label1.Enabled = false;
			label2.Enabled = false;
			label3.Enabled = false;
        }

		private void Login_Click(object sender, EventArgs e)
		{
			string user = textBox1.Text;
			string pass = maskedTextBox1.Text;

			if (GestaodeAnalistas.login(user, pass))
			{	//  Se consegue ligar a base de dados
				this.Close();
				//umaClassePorreira.Main();
			}
			else
			{	//  Se nao tem connectividade a internet, ou o username ou a pass
				// estao mal
				label1.Enabled = true;
				label2.Enabled = true;
				label3.Enabled = true;
			}

		}


		//private void InterfaceLogin_Load(object sender, EventArgs e)
		//{

		//}
    }
}
