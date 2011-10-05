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
    public partial class Interface_ConfigurarLigacaoBD : Form
    {
        public static void main()
        {
            Interface_ConfigurarLigacaoBD icflbd = new Interface_ConfigurarLigacaoBD();
        }

        public Interface_ConfigurarLigacaoBD()
        {
            InitializeComponent();
            initTextBoxes();
            this.ShowDialog();
        }

        private void initTextBoxes()
        {
            try
            {
                System.IO.StreamReader sr = new System.IO.StreamReader("Config.cfg");

                string server = sr.ReadLine();
                string database = sr.ReadLine();
                sr.ReadLine();
                string username = sr.ReadLine();
                string password = sr.ReadLine();

                sr.Close();

                this.textBox1.Text = server;
                this.textBox2.Text = database;
                this.textBox3.Text = username;
                this.textBox4.Text = password;
            }
            catch
            {
            }
            //Disable das text boxes
            this.splitContainer1.Panel1.Enabled = false;
            this.splitContainer1.Panel2.Enabled = false;
            this.textBox1.Enabled = false;
            this.textBox2.Enabled = false;
            this.textBox3.Enabled = false;
            this.textBox4.Enabled = false;

        }

        private void alterarGuardarClick(object sender, EventArgs e)
        {
            if (button1.Text == "Guardar")
            {

                Camada_de_Negócio.GestaodeAnalistas.alterar_ligacaoBaseDados(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text);

                //realterar o estado das textboxes
                //Disable das text boxes
                this.textBox1.Enabled = false;
                this.textBox2.Enabled = false;
                this.textBox3.Enabled = false;
                this.textBox4.Enabled = false;

                this.button1.Text = "Alterar";

                //informar do sucesso da operação
                MessageBoxPortuguese.Show("", "Alteração efectuado com sucesso.",
                     MessageBoxPortuguese.Icon_Info);

            }
            else if (button1.Text == "Alterar")
            {
                if (MessageBoxPortuguese.Show("", "Tem a certeza que pretende alterar o servidor de base de dados com que o programa comunica?"+
                                                  "\nPoderá implicar ter que criar uma nova sessão "+
                                                  "no novo servidor.\n(Se não tem a certeza não altere)",
                     MessageBoxPortuguese.Button_YesNo, MessageBoxPortuguese.Icon_Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    //enable das textboxes
                    this.splitContainer1.Panel1.Enabled = true;
                    this.splitContainer1.Panel2.Enabled = true;
                    this.textBox1.Enabled = true;
                    this.textBox2.Enabled = true;
                    this.textBox3.Enabled = true;
                    this.textBox4.Enabled = true;

                    this.button1.Text = "Guardar";

                }
            }
        }

        private void clickAjuda(object sender, EventArgs e)
        {
            Interface_Ajuda.main();
        }

    }
}
