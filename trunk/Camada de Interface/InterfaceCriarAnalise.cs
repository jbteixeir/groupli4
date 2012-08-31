using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ETdAnalyser.Camada_de_Negócio;
using ETdAnalyser.Camada_de_Dados.Classes;

namespace ETdAnalyser.Camada_de_Interface
{
    public partial class InterfaceCriarAnalise : Form
    {
        private List<string> zonas;
        private List<Item> itens;
        private List<string> itens_novos;
        private static InterfaceCriarAnalise ica;
        private long codigoProjecto;
        private bool done;
        private string tipo;
        private int wh;

        // rdone
        public InterfaceCriarAnalise(long codigoProjecto, string nomeProjecto)
        {
            InitializeComponent();
            zonas = new List<string>();
            itens = new List<Item>();

            this.codigoProjecto = codigoProjecto;

            label3.Visible = false;
            done = false;

            textBox1.Text = DateTime.Now.Date.ToString().Split(' ')[0];
        }

        // rdone
        public static void main(long codigoProjecto, string nomeProjecto)
        {
            ica = new InterfaceCriarAnalise(codigoProjecto,nomeProjecto);
            ica.ShowDialog();
        }

        // rdone
        private void MouseEnterAction(object sender, EventArgs e)
        {
            Label t = (Label)sender;
            t.Font = new Font(t.Font, FontStyle.Underline);
        }

        // rdone
        private void MouseLeaveAction(object sender, EventArgs e)
        {
            Label t = (Label)sender;
            t.Font = new Font(t.Font, FontStyle.Regular);
        }

        // rdone
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

        // rdone
        private void AdicionarActionPerfermed(object sender, EventArgs e)
        {
            string nome = textBox1.Text;

            bool valido = nomeValido(nome);
            if (comboBox1.SelectedIndex == 2) done = true;

            if (!valido)
                MessageBox.Show("Nome da análise inválida\n(Apenas letras, números e \"_-/\")", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (!GestaodeAnalises.podeAdicionarAnalise(codigoProjecto, nome))
                MessageBox.Show("Nome da análise já existente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (comboBox1.SelectedIndex < 0 || comboBox1.SelectedIndex >2 )
                MessageBox.Show("Deve escolher o tipo de analise", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (itens.Count == 0 || zonas.Count == 0 || !done)
            {
                nome = label3.Text;
                if (itens.Count == 0)
                {
                    errorProvider2.Icon = global::ETdAnalyser.Properties.Resources.notification_warning_ico;
                    errorProvider2.SetError(label4, "Itens têm de estar preenchido");
                }
                if (zonas.Count == 0 || !done)
                {
                    errorProvider1.Icon = global::ETdAnalyser.Properties.Resources.notification_warning_ico;
                    errorProvider1.SetError(label3, nome + " têm de estar preenchido");
                }
            }
            else
            {
                if (itens_novos.Count != 0)
                {
                    Dictionary<long, string> codes = GestaodeAnalises.adicionaItensNovos(itens_novos);
                    foreach (long l in codes.Keys)
                    {
                        bool found = false;
                        for (int i = 0; i < itens.Count; i++)
                            if (itens[i].NomeItem == codes[l])
                            {
                                itens[i].CodigoItem = l;
                                found = true;
                            }
                    }
                }

                List<Zona> zs = GestaodeAnalises.adicionaZonasNovas(zonas);
                string tipo = comboBox1.SelectedItem.ToString();

                Analise a = new Analise();
                a.CodigoProj = codigoProjecto;
                a.Nome = nome;
                a.Tipo = tipo;
                a.Zonas = zs;
                a.Itens = itens;

                GestaodeAnalises.adicionaAnalise(a, codigoProjecto);
                endFrame();
            }
        }

        // rdone
        private bool nomeValido(string p)
        {
            if (p == "") return false;
            string possiveis = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVKWXYZ0123456789_-/ áàãâéêíóõúÁÀÃÂÉÊÍÓÕÚçÇ";
            bool found = true;
            for (int i = 0; i < p.Length && found; i++)
                found = possiveis.Contains(p[i]);
            return found;
        }

        // rdone
        private void ZonasActionPerformed(object sender, EventArgs e)
        {
            if (wh != comboBox1.SelectedIndex)
            {
                zonas = new List<string>();
                done = false;
                wh = comboBox1.SelectedIndex;
            }
            InterfaceCriarAnaliseZonas.main(zonas, tipo,true);
        }

        // rdone
        private void ItensActionPerformed(object sender, EventArgs e)
        {
            InterfaceCriarAnaliseItens.main(itens,true);
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
            {
                zonas = (List<string>)sender;
                done = true;
            }
            errorProvider1.Clear();
            errorProvider1.Icon = global::ETdAnalyser.Properties.Resources.notification_done_ico;
            string nome = label3.Text;
            errorProvider1.SetError(label3, nome + " OK");
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

            errorProvider2.Clear();
            errorProvider2.Icon = global::ETdAnalyser.Properties.Resources.notification_done_ico;
            errorProvider2.SetError(label4, "Itens OK");
        }

        // rdone
        private void ComboBoxClick(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (comboBox1.SelectedIndex >= 0 &&
                comboBox1.SelectedIndex <= 2)
            {
                string nome;
                if (comboBox1.SelectedIndex == 0)
                {
                    nome = "Zonas";
                    tipo = nome;
                    label3.Enabled = true;
                    if (wh == 0 && done)
                    {
                        errorProvider1.Icon = global::ETdAnalyser.Properties.Resources.notification_done_ico;
                        errorProvider1.SetError(label3, nome + " OK");
                    }
                    else
                        done = false;
                }
                else if (comboBox1.SelectedIndex == 1)
                {
                    nome = "Actividades";
                    tipo = nome;
                    label3.Enabled = true;
                    if (wh == 1 && done)
                    {
                        errorProvider1.Icon = global::ETdAnalyser.Properties.Resources.notification_done_ico;
                        errorProvider1.SetError(label3, nome + " OK");
                    }
                    else
                        done = false;
                }
                else
                {
                    nome = "Área Comum";
                    ZonasOk(sender, e);
                    label3.Enabled = false;
                    wh = 2;
                    done = false;
                }
                label3.Text = nome;
                label3.Visible = true;
            }
            else
                label3.Visible = false;
        }
    }
}
