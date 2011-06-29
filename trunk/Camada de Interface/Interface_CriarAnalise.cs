using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ETdA.Camada_de_Negócio;
using ETdA.Camada_de_Dados.Classes;

namespace ETdA.Camada_de_Interface
{
    public partial class Interface_CriarAnalise : Form
    {
        private List<string> zonas;
        private List<Item> itens;
        private List<string> itens_novos;
        private static Interface_CriarAnalise ica;
        private long codProjecto;

        public Interface_CriarAnalise(long codProjecto)
        {
            InitializeComponent();
            zonas = new List<string>();
            itens = new List<Item>();

            this.codProjecto = codProjecto;

            label3.Visible = false;
        }

        public static void main(long codProjecto)
        {
            ica = new Interface_CriarAnalise(codProjecto);
            ica.Visible = true;
        }

        private void MouseEnterAction(object sender, EventArgs e)
        {
            Label t = (Label)sender;
            t.Font = new Font(t.Font, FontStyle.Underline);
        }

        private void MouseLeaveAction(object sender, EventArgs e)
        {
            Label t = (Label)sender;
            t.Font = new Font(t.Font, FontStyle.Regular);
        }

        private void endFrame()
        {
            Dispose();
            Close();
        }

        // rdone
        private void CancelarActionPerformed(object sender, EventArgs e)
        {
            endFrame();
        }

        private void AdicionarActionPerfermed(object sender, EventArgs e)
        {
            string nome = textBox1.Text;
            string tipo = comboBox1.SelectedItem.ToString();

            String cont = "abcdefghijklmnopqrstuvwxyz" + 
                          "ABCDEFGHIJKLMNOPQRSTUVWXYZ" + 
                          "0123456789" + 
                          "_";

            MessageBox.Show(tipo);

            bool found = true;
            for ( int i = 0 ; i < nome.Length && found; i++ )
                found = cont.Contains(nome[i]);

            if (nome == "" || !found)
                MessageBox.Show("Nome da análise inválida","Erro",MessageBoxButtons.OK,MessageBoxIcon.Error);
            else if (itens.Count == 0 || zonas.Count == 0)
                MessageBox.Show("Zonas e Itens têm de estar preenchidos", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                if (itens_novos.Count != 0)
                {
                    Dictionary<long,string> codes = GestaodeAnalises.adicionaItensNovos(itens_novos);
                    foreach (long l in codes.Keys)
                    {
                        found = false;
                        for(int i = 0 ; i < itens.Count ; i++)
                            if (itens[i].NomeItem == codes[l])
                            {
                                itens[i].CodigoItem = l;
                                found = true;
                            }
                    }
                }

                List<Zona> zs = GestaodeAnalises.adicionaZonasNovas(zonas);

                Analise a = new Analise();
                a.CodigoProj = codProjecto;
                a.Nome = nome;
                a.Tipo = tipo;
                a.Zonas = zs;
                a.Itens = itens;

                GestaodeAnalises.adicionaAnalise(a, codProjecto);
                endFrame();
            }
        }

		private bool nomeAnaliseValido(string p)
		{
			string possiveis = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVKWXYZ0123456789_";
			bool valid;
			foreach(char c in p)	{

				valid = false;
				for (int i = 0; i < possiveis.Length; i++)
				{
					if (c == possiveis[i])
					{
						valid = true;
						break;
					}
				}
				if (valid == false)
					return false;
			}
			return true;
		}

        private void ZonasActionPerformed(object sender, EventArgs e)
        {
            Interface_CriarAnaliseZonas.main(zonas, comboBox1.SelectedItem.ToString());
        }

        private void ItensActionPerformed(object sender, EventArgs e)
        {
            Interface_CriarAnaliseItens.main();
        }

        // rdone
        public static void ZonasOkReenc(object sender, EventArgs e)
        {
            ica.ZonasOk(sender, e);
        }
        private void ZonasOk(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 2)
            {
                zonas = new List<string>();
                zonas.Add("Area Comum");
            }
            else
                zonas = (List<string>)sender;
            errorProvider1.Icon = global::ETdA.Properties.Resources._1309271491_notification_done;
            errorProvider1.SetError(label3, "Zonas OK");
        }

        // rdone
        public static void ItensOkReenc(object sender, EventArgs e)
        {
            ica.ItensOk(sender, e);
        }
        private void ItensOk(object sender, EventArgs e)
        {
            List<object> l = (List<object>)sender;

            itens = (List<Item>) l[0];
            itens_novos = (List<string>)l[1];

            errorProvider2.Icon = global::ETdA.Properties.Resources._1309271491_notification_done;
            errorProvider2.SetError(label4, "Itens OK");
        }

        // rdone
        private void ComboBoxClick(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0 &&
                comboBox1.SelectedIndex <= 2)
            {
                string nome;
                if (comboBox1.SelectedIndex == 0)
                    nome = "Zonas";
                else if (comboBox1.SelectedIndex == 1)
                    nome = "Actividades";
                else
                {
                    nome = "Área Comum";
                    ZonasOk(sender, e);
                }
                label3.Text = nome;
                label3.Visible = true;
            }
            else
                label3.Visible = false;
        }
    }
}
