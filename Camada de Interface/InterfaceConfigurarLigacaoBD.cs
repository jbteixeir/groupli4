using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ETdAnalyser.CamadaInterface
{
    public partial class InterfaceConfigurarLigacaoBD : Form
    {
        public static void main()
        {
            InterfaceConfigurarLigacaoBD icflbd = new InterfaceConfigurarLigacaoBD();
        }

        public InterfaceConfigurarLigacaoBD()
        {
            InitializeComponent();
            initTextBoxes();
            this.ShowDialog();
        }

        private void initTextBoxes()
        {
            try
            {
                FileStream fs = new FileStream("Config.cfg", FileMode.Open, FileAccess.Read);
                BinaryReader r = new BinaryReader(fs);

                this.textBox1.Text = r.ReadString();
                this.textBox2.Text = r.ReadString();
                this.textBox6.Text = r.ReadString();
                this.textBox5.Text = r.ReadString();
                r.ReadString();
                this.textBox3.Text = r.ReadString();
                this.textBox4.Text = r.ReadString();

                r.Close();
                fs.Close();
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
            this.textBox5.Enabled = false;
            this.textBox6.Enabled = false;

        }

        private void alterarGuardarClick(object sender, EventArgs e)
        {
            if (button1.Text == "Guardar")
            {

                Camada_de_Negócio.GestaodeAnalistas.alterar_ligacaoBaseDados(textBox1.Text, textBox2.Text, textBox6.Text, textBox5.Text, textBox3.Text, textBox4.Text);

                //realterar o estado das textboxes
                //Disable das text boxes
                this.textBox1.Enabled = false;
                this.textBox2.Enabled = false;
                this.textBox3.Enabled = false;
                this.textBox4.Enabled = false;
                this.textBox5.Enabled = false;
                this.textBox6.Enabled = false;

                this.button1.Text = "Alterar";
                this.button3.Enabled = false;

                //informar do sucesso da operação
                MessageBoxPortuguese.Show("", "Alteração efectuado com sucesso.",
                     MessageBoxPortuguese.Icon_Info);

            }
            else if (button1.Text == "Alterar")
            {
                if (MessageBoxPortuguese.Show("", "Tem a certeza que pretende alterar o servidor de base de dados e web com que o programa comunica?"+
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
                    this.textBox5.Enabled = true;
                    this.textBox6.Enabled = true;

                    this.button1.Text = "Guardar";
                    this.button3.Enabled = true;

                }
            }
        }

        private void clickAjuda(object sender, EventArgs e)
        {
            InterfaceAjuda.main();
        }

        private void testarLigacao(object sender, EventArgs e)
        {
            if (CamadaDados.DataBaseCommunicator.DataBaseCommunicator.connectToSuper(textBox1.Text, textBox3.Text, textBox4.Text, textBox2.Text))
                MessageBox.Show("Configuração da ligação ao servidor SQL server efectuada com sucesso!");
        }

    }
}
