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
    public partial class Interface_CriarAnaliseItens : Form
    {
        private delegate void eventoEventHandler(object sender, EventArgs e);

        //[Category(""), Description("Ocorre sempre ...")]
        private static event eventoEventHandler done_action;

        private string s1 = "Ponderacao Analista";
        private string s2 = "Ponderacao do Profissional";
        private string s3 = "Ponderacao do Cliente";
        private string s4 = "Vermelho";
        private string s5 = "Laranja";
        private string s6 = "Amarelo";
        private string s7 = "Verde Lima";
        private string s8 = "Verde";
        private string s9 = "Limite Inferior Analista";

        private Dictionary<long, string> defaults;
        private Dictionary<long, string> alls;
        private Dictionary<string, Dictionary<string,object>> panels;

        public Interface_CriarAnaliseItens()
        {
            InitializeComponent();

            done_action += new eventoEventHandler(
               Camada_de_Interface.Interface_CriarAnalise.ItensOkReenc);

            defaults = GestaodeAnalises.getItensDefault();
            alls = defaults;
            int i = 0;
            foreach (string s in defaults.Values)
            {
                checkedListBox1.Items.Add(s);
                checkedListBox1.SetItemChecked(i++, true);
            }

            ponderacao(-1);
        }

        private void ponderacao(int index)
        {
            panel1.AutoScroll = true;
            panel1.Controls.Clear();
            panels = new Dictionary<string, Dictionary<string, object>>();

            int yy = 7,y;
            for (int i = 0; i < checkedListBox1.CheckedItems.Count; i++)
            {
                if (index < 0 ||  checkedListBox1.Items[index] != checkedListBox1.CheckedItems[i])
                {
                    Dictionary<string, object> controls = new Dictionary<string, object>();
                    y = 7;
                    Panel p = new System.Windows.Forms.Panel();
                    p.Name = checkedListBox1.CheckedItems[i].ToString();
                    p.Height = 355;
                    p.Width = 320;
                    p.BorderStyle = BorderStyle.FixedSingle;

                    Label l = new System.Windows.Forms.Label();
                    l.Width = 250;
                    l.Text = checkedListBox1.CheckedItems[i].ToString();
                    l.Location = new System.Drawing.Point(7, y);
                    y += 30;
                    p.Controls.Add(l);

                    Label l2 = new System.Windows.Forms.Label();
                    l2.Width = 150;
                    l2.Text = "Ponderacao do Analista:";
                    l2.Location = new System.Drawing.Point(7, y);
                    p.Controls.Add(l2);

                    NumericUpDown n = new System.Windows.Forms.NumericUpDown();
                    n.Increment = new Decimal(0.1);
                    n.Maximum = 1;
                    n.DecimalPlaces = 3;
                    n.Value = new decimal(0.333);
                    n.Location = new System.Drawing.Point(167, y);
                    y += 30;
                    controls.Add(s1, n);
                    p.Controls.Add(n);

                    Label l3 = new System.Windows.Forms.Label();
                    l3.Width = 150;
                    l3.Text = "Ponderacao do Profissional:";
                    l3.Location = new System.Drawing.Point(7, y);
                    p.Controls.Add(l3);

                    NumericUpDown n2 = new System.Windows.Forms.NumericUpDown();
                    n2.Increment = new Decimal(0.1);
                    n2.Maximum = 1;
                    n2.DecimalPlaces = 3;
                    n2.Value = new decimal(0.333);
                    n2.Location = new System.Drawing.Point(167, y);
                    y += 30;
                    controls.Add(s2,n2);
                    p.Controls.Add(n2);

                    Label l4 = new System.Windows.Forms.Label();
                    l4.Width = 150;
                    l4.Text = "Ponderacao do Cliente:";
                    l4.Location = new System.Drawing.Point(7, y);
                    p.Controls.Add(l4);

                    NumericUpDown n3 = new System.Windows.Forms.NumericUpDown();
                    n3.Increment = new Decimal(0.1);
                    n3.Maximum = 1;
                    n3.DecimalPlaces = 3;
                    n3.Value = new decimal(0.333);
                    n3.Location = new System.Drawing.Point(167, y);
                    y += 40;
                    controls.Add(s3,n3);
                    p.Controls.Add(n3);

                    Label l5 = new System.Windows.Forms.Label();
                    l5.Width = 150;
                    l5.Text = "Escalas";
                    l5.Location = new System.Drawing.Point(7, y);
                    y += 25;
                    p.Controls.Add(l5);

                    Label l6 = new System.Windows.Forms.Label();
                    l6.Width = 100;
                    l6.Text = "Vermelho Max:";
                    l6.Location = new System.Drawing.Point(7, y);
                    p.Controls.Add(l6);

                    TextBox b1 = new System.Windows.Forms.TextBox();
                    b1.Height = 20;
                    b1.Width = 40;
                    b1.Text = "1";
                    b1.Location = new System.Drawing.Point(107, y);
                    y += 25;
                    controls.Add(s4,b1);
                    p.Controls.Add(b1);

                    Label l8 = new System.Windows.Forms.Label();
                    l8.Width = 100;
                    l8.Text = "Laranja Max:";
                    l8.Location = new System.Drawing.Point(7, y);
                    p.Controls.Add(l8);

                    TextBox b3 = new System.Windows.Forms.TextBox();
                    b3.Height = 20;
                    b3.Width = 40;
                    b3.Text = "2";
                    b3.Location = new System.Drawing.Point(107, y);
                    y += 25;
                    controls.Add(s5,b3);
                    p.Controls.Add(b3);

                    Label l10 = new System.Windows.Forms.Label();
                    l10.Width = 100;
                    l10.Text = "Amarelo Max:";
                    l10.Location = new System.Drawing.Point(7, y);
                    p.Controls.Add(l10);

                    TextBox b5 = new System.Windows.Forms.TextBox();
                    b5.Height = 20;
                    b5.Width = 40;
                    b5.Text = "3";
                    b5.Location = new System.Drawing.Point(107, y);
                    y += 25;
                    controls.Add(s6,b5);
                    p.Controls.Add(b5);

                    Label l12 = new System.Windows.Forms.Label();
                    l12.Width = 100;
                    l12.Text = "Verde Lima Max:";
                    l12.Location = new System.Drawing.Point(7, y);
                    p.Controls.Add(l12);

                    TextBox b7 = new System.Windows.Forms.TextBox();
                    b7.Height = 20;
                    b7.Width = 40;
                    b7.Text = "4";
                    b7.Location = new System.Drawing.Point(107, y);
                    y += 25;
                    controls.Add(s7,b7);
                    p.Controls.Add(b7);

                    Label l14 = new System.Windows.Forms.Label();
                    l14.Width = 100;
                    l14.Text = "Verde Max:";
                    l14.Location = new System.Drawing.Point(7, y);
                    p.Controls.Add(l14);

                    TextBox b9 = new System.Windows.Forms.TextBox();
                    b9.Height = 20;
                    b9.Width = 40;
                    b9.Text = "5";
                    b9.Enabled = false;
                    b9.Location = new System.Drawing.Point(107, y);
                    y +=40;
                    controls.Add(s8,b9);
                    p.Controls.Add(b9);

                    Label l15 = new System.Windows.Forms.Label();
                    l14.Width = 150;
                    l14.Text = "Limite Inferior Analista:";
                    l14.Location = new System.Drawing.Point(7, y);
                    p.Controls.Add(l14);

                    NumericUpDown n4 = new System.Windows.Forms.NumericUpDown();
                    n4.Increment = new Decimal(0.1);
                    n4.Maximum = 5;
                    n4.DecimalPlaces = 3;
                    n4.Value = new decimal(0);
                    n4.Location = new System.Drawing.Point(157, y);
                    controls.Add(s9,n4);
                    p.Controls.Add(n4);

                    p.Location = new System.Drawing.Point(7, yy);
                    yy += 355;
                    panels.Add(p.Name, controls);
                    panel1.Controls.Add(p);
                }

            }
        }

        public static void main()
        {
            Interface_CriarAnaliseItens icai = new Interface_CriarAnaliseItens();
            icai.Visible = true;
        }

        private void AdicionarActionPerformed(object sender, EventArgs e)
        {
            String cont = "abcdefghijklmnopqrstuvwxyz" +
              "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            string s = textBox1.Text;

            bool found = true;
            for ( int i = 0 ; i < s.Length && found; i++ )
                found = cont.Contains(s[i]);

            if (s == "" || !found)
                MessageBox.Show("Nome do item inválido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (checkedListBox1.Items.Contains(s))
                MessageBox.Show("Já existe um item " + s + ".", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                checkedListBox1.Items.Add(s);
                checkedListBox1.SetItemChecked(checkedListBox1.Items.Count - 1, true);

                textBox1.Text = "";

                ponderacao(-1);
            }
        }

        private void MostrarTodosActionPerformed(object sender, EventArgs e)
        {
            checkedListBox1.Items.Clear();
            alls = GestaodeAnalises.getTodosItens();

            int i = 0;
            foreach (string s in alls.Values)
            {
                checkedListBox1.Items.Add(s);
                if (i < 14)
                    checkedListBox1.SetItemChecked(i++, true);
            }

            ponderacao(-1);
        }

        private void CancelarActionPerformed(object sender, EventArgs e)
        {
            end_Frame();
        }

        private void OK_ActionPerformed(object sender, EventArgs e)
        {
            List<string> nome_novos = new List<string>();
            List<Item> itens = new List<Item>();
            foreach (string s in checkedListBox1.CheckedItems)
            {
                Item i = new Item();
                long cod = -1;
                bool found = false; 
                for (int j = 0 ; j < alls.Values.Count && !found; j++ )
                    if(alls.Values.ElementAt(j) == s)
                    {
                        cod = alls.Keys.ElementAt(j);
                        found = true;
                    }

                if (found)
                {
                    i.CodigoItem = cod;
                    i.Default = 1;
                }
                else
                {
                    nome_novos.Add(s);
                    i.Default = 0;
                }

                i.NomeItem = s;
                Dictionary<string, object> bs = panels[s];
                NumericUpDown n1 = (NumericUpDown)bs[s1];
                NumericUpDown n2 = (NumericUpDown)bs[s2];
                NumericUpDown n3 = (NumericUpDown)bs[s3];
                TextBox b1 = (TextBox)bs[s4];
                TextBox b2 = (TextBox)bs[s5];
                TextBox b3 = (TextBox)bs[s6];
                TextBox b4 = (TextBox)bs[s7];
                TextBox b5 = (TextBox)bs[s8];
                NumericUpDown n4 = (NumericUpDown)bs[s9];

                i.PonderacaoAnalista = float.Parse(n1.Value.ToString());
                i.PonderacaoProfissional = float.Parse(n2.Value.ToString());
                i.PonderacaoCliente = float.Parse(n3.Value.ToString());
                i.Inter_Vermelho= float.Parse(b1.Text);
                i.Inter_Laranja= float.Parse(b2.Text);
                i.Inter_Amarelo= float.Parse(b3.Text);
                i.Inter_Verde_Lima= float.Parse(b4.Text);
                i.Inter_Verde= float.Parse(b5.Text);
                i.LimiteInferiorAnalista= float.Parse(n4.Value.ToString());

                itens.Add(i);
            }
            List<object> obs = new List<object>();
            obs.Add(itens);
            obs.Add(nome_novos);

            done_action(obs,new EventArgs());
            end_Frame();
        }

        private void end_Frame()
        {
            Dispose();
            Close();
        }

        private void CheckListChangedAcionPerformed(object sender, EventArgs e)
        {
            ponderacao(-1);
        }
    }
}
