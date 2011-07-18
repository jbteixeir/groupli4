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
        private Dictionary<object, ErrorProvider> erros;
        private List<Item> itens;
        private bool adding;

        public Interface_CriarAnaliseItens(object o, bool b)
        {
            InitializeComponent();

            erros = new Dictionary<object, ErrorProvider>();
            itens = (List<Item>) o;
            adding = b;

            done_action += new eventoEventHandler(
               Camada_de_Interface.Interface_CriarAnalise.ItensOkReenc);

            defaults = GestaodeAnalises.getItensDefault();
            alls = defaults;

            int i = 0;
            if (itens.Count == 0 && b)
            {
                foreach (string s in defaults.Values)
                {
                    checkedListBox1.Items.Add(s);
                    checkedListBox1.SetItemChecked(i++, true);
                }
            }
            else if (itens.Count != 0 && b)
            {
                foreach (string s in defaults.Values)
                {
                    checkedListBox1.Items.Add(s);
                    if (contemNaLista(s))
                        checkedListBox1.SetItemChecked(i, true);
                    i++;
                }
                foreach (Item item in itens)
                {
                    if (!defaults.Values.Contains(item.NomeItem))
                    {
                        checkedListBox1.Items.Add(item.NomeItem);
                        checkedListBox1.SetItemChecked(i++, true);
                    }
                }
            }
            else
            {
                foreach (Item item in itens)
                {
                    checkedListBox1.Items.Add(item.NomeItem);
                    checkedListBox1.SetItemChecked(i++, true);
                }
                button1.Visible = false;
                button2.Visible = false;
                button4.Visible = false;
                textBox1.Visible = false;
                checkedListBox1.Enabled = false;
            }

            ponderacao(-1);
        }

        private bool contemNaLista(string s)
        {
            bool found = false;
            for(int i = 0 ; i < itens.Count && !found ; i++)
                if (itens[i].NomeItem == s)
                    found = true;
            return found;
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
                    Item item = getItem(checkedListBox1.CheckedItems[i].ToString());
                    Dictionary<string, object> controls = new Dictionary<string, object>();
                    y = 7;
                    Panel p = new System.Windows.Forms.Panel();
                    p.Name = checkedListBox1.CheckedItems[i].ToString();
                    p.Height = 355;
                    p.Width = panel1.Width - 14;
                    p.BorderStyle = BorderStyle.FixedSingle;
                    p.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

                    Label l = new System.Windows.Forms.Label();
                    l.Width = 250;
                    l.Text = checkedListBox1.CheckedItems[i].ToString();
                    l.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, 
                        System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
                    n.Value = new decimal((item!=null) ? item.PonderacaoAnalista : 0.334);
                    n.Location = new System.Drawing.Point(167, y);
                    y += 30;
                    n.KeyPress += new KeyPressEventHandler(KeyPressActionPerformed);
                    n.Click += new EventHandler(MouseClickActionPerformed);
                    if (!adding) n.Enabled = false;
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
                    n2.Value = new decimal((item != null) ? item.PonderacaoProfissional : 0.333);
                    n2.Location = new System.Drawing.Point(167, y);
                    y += 30;
                    n2.KeyPress += new KeyPressEventHandler(KeyPressActionPerformed);
                    n2.Click += new EventHandler(MouseClickActionPerformed);
                    if (!adding) n2.Enabled = false;
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
                    n3.Value = new decimal((item != null) ? item.PonderacaoCliente : 0.333);
                    n3.Location = new System.Drawing.Point(167, y);
                    y += 40;
                    n3.KeyPress += new KeyPressEventHandler(KeyPressActionPerformed);
                    n3.Click += new EventHandler(MouseClickActionPerformed);
                    if (!adding) n3.Enabled = false;
                    controls.Add(s3,n3);
                    p.Controls.Add(n3);

                    Label l5 = new System.Windows.Forms.Label();
                    l5.Width = 150;
                    l5.Text = "Escalas";
                    l5.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F,
                        System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
                    b1.Text = (item!=null) ? ""+item.Inter_Vermelho : "1";
                    b1.Location = new System.Drawing.Point(107, y);
                    y += 25;
                    b1.KeyPress += new KeyPressEventHandler(KeyPressActionPerformed);
                    b1.Click += new EventHandler(MouseClickActionPerformed);
                    if (!adding) b1.Enabled = false;
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
                    b3.Text = (item != null) ? "" + item.Inter_Laranja : "2";
                    b3.Location = new System.Drawing.Point(107, y);
                    y += 25;
                    b3.KeyPress += new KeyPressEventHandler(KeyPressActionPerformed);
                    b3.Click += new EventHandler(MouseClickActionPerformed);
                    if (!adding) b3.Enabled = false;
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
                    b5.Text = (item != null) ? "" + item.Inter_Amarelo : "3";
                    b5.Location = new System.Drawing.Point(107, y);
                    y += 25;
                    b5.KeyPress += new KeyPressEventHandler(KeyPressActionPerformed);
                    b5.Click += new EventHandler(MouseClickActionPerformed);
                    if (!adding) b5.Enabled = false;
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
                    b7.Text = (item != null) ? "" + item.Inter_Verde_Lima : "4";
                    b7.Location = new System.Drawing.Point(107, y);
                    y += 25;
                    b7.KeyPress += new KeyPressEventHandler(KeyPressActionPerformed);
                    b7.Click += new EventHandler(MouseClickActionPerformed);
                    if (!adding) b7.Enabled = false;
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
                    n4.Value = new Decimal((item != null) ? item.LimiteInferiorAnalista : 0);
                    n4.Location = new System.Drawing.Point(157, y);
                    if (!adding) n4.Enabled = false;
                    controls.Add(s9,n4);
                    p.Controls.Add(n4);

                    p.Location = new System.Drawing.Point(7, yy);
                    yy += 355;
                    panels.Add(p.Name, controls);
                    panel1.Controls.Add(p);
                }

            }
        }

        private Item getItem(string nome)
        {
            Item item = null;
            bool found = false;
            for (int i = 0; i < itens.Count && !found; i++)
                if (itens[i].NomeItem == nome)
                {
                    item = itens[i];
                    found = true;
                }
            return item;
        }

        public static void main(object o, bool b)
        {
            Interface_CriarAnaliseItens icai = new Interface_CriarAnaliseItens(o,b);
            icai.Visible = true;
        }

        private void AdicionarActionPerformed(object sender, EventArgs e)
        {
            string s = textBox1.Text;

            bool valido = nomeItemValido(s);

            if (!valido)
                MessageBox.Show("Nome do item inválido\n(Anpeas letras, números e \"_-\")", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private bool nomeItemValido(string p)
        {
            if (p == "") return false;
            string possiveis = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVKWXYZ0123456789_-";
            bool found = true;
            for (int i = 0; i < p.Length && found; i++)
                found = possiveis.Contains(p[i]);
            return found;
        }

        private void MostrarTodosActionPerformed(object sender, EventArgs e)
        {
            //checkedListBox1.Items.Clear();
            alls = GestaodeAnalises.getTodosItens();

            int i = 0;
            if (itens.Count == 0)
            {
                foreach (string s in alls.Values)
                {
                    if (!checkedListBox1.Items.Contains(s))
                        checkedListBox1.Items.Add(s);
                    if (i < 14)
                        checkedListBox1.SetItemChecked(i++, true);
                }
            }
            else
            {
                foreach (string s in alls.Values)
                {
                    if (!checkedListBox1.Items.Contains(s))
                        checkedListBox1.Items.Add(s);
                    if (contemNaLista(s))
                        checkedListBox1.SetItemChecked(i, true);
                    i++;
                }
            }

            ponderacao(-1);
        }

        private void CancelarActionPerformed(object sender, EventArgs e)
        {
            end_Frame();
        }

        private void OK_ActionPerformed(object sender, EventArgs e)
        {
            bool podeAdicionar = true;
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

                i.PonderacaoAnalista = double.Parse(n1.Value.ToString());
                i.PonderacaoProfissional = double.Parse(n2.Value.ToString());
                i.PonderacaoCliente = double.Parse(n3.Value.ToString());
                i.Inter_Vermelho= double.Parse(b1.Text);
                i.Inter_Laranja= double.Parse(b2.Text);
                i.Inter_Amarelo= double.Parse(b3.Text);
                i.Inter_Verde_Lima= double.Parse(b4.Text);
                i.Inter_Verde= double.Parse(b5.Text);
                i.LimiteInferiorAnalista= double.Parse(n4.Value.ToString());

                podeAdicionar = podeAdicionar & verificaErros(i);

                itens.Add(i);
            }
            List<object> obs = new List<object>();
            obs.Add(itens);
            obs.Add(nome_novos);

            if (podeAdicionar)
            {
                done_action(obs, new EventArgs());
                end_Frame();
            }
            else 
                MessageBox.Show("Alguns pârametros estão inválidos.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private bool verificaErros(Item i)
        {
            bool podeAdicionar = true;
            Dictionary<string, object> bs = panels[i.NomeItem];
            NumericUpDown n1 = (NumericUpDown)bs[s1];
            NumericUpDown n2 = (NumericUpDown)bs[s2];
            NumericUpDown n3 = (NumericUpDown)bs[s3];
            TextBox b1 = (TextBox)bs[s4];
            TextBox b2 = (TextBox)bs[s5];
            TextBox b3 = (TextBox)bs[s6];
            TextBox b4 = (TextBox)bs[s7];
            TextBox b5 = (TextBox)bs[s8];
            NumericUpDown n4 = (NumericUpDown)bs[s9];

            if (i.PonderacaoProfissional + i.PonderacaoCliente + i.PonderacaoAnalista != 1)
            {
                ErrorProvider err = new ErrorProvider();
                err.Icon = global::ETdA.Properties.Resources.notification_warning_ico;
                err.SetError(n1, "O total das ponderações é diferente de 1.");
                err.SetError(n2, "O total das ponderações é diferente de 1.");
                err.SetError(n3, "O total das ponderações é diferente de 1.");
                podeAdicionar = false;
                if (!erros.Keys.Contains(n1))
                    erros.Add(n1,err);
            }
            if (i.Inter_Vermelho > i.Inter_Laranja || i.Inter_Vermelho > 5)
            {
                ErrorProvider err = new ErrorProvider();
                err.Icon = global::ETdA.Properties.Resources.notification_warning_ico;
                err.SetError(b1, "Máximo da cor Vermelha inválida.");
                podeAdicionar = false;
                if (!erros.Keys.Contains(b1))
                    erros.Add(b1, err);
            }
            if (i.Inter_Laranja > i.Inter_Amarelo || i.Inter_Laranja > 5)
            {
                ErrorProvider err = new ErrorProvider();
                err.Icon = global::ETdA.Properties.Resources.notification_warning_ico;
                err.SetError(b2, "Máximo da cor Laranja inválida.");
                podeAdicionar = false;
                if (!erros.Keys.Contains(b2))
                    erros.Add(b2, err);
            }
            if (i.Inter_Amarelo > i.Inter_Verde_Lima || i.Inter_Amarelo > 5)
            {
                ErrorProvider err = new ErrorProvider();
                err.Icon = global::ETdA.Properties.Resources.notification_warning_ico;
                err.SetError(b3, "Máximo da cor Amarela inválida.");
                podeAdicionar = false;
                if (!erros.Keys.Contains(b3))
                    erros.Add(b3, err);
            }
            if (i.Inter_Verde_Lima > i.Inter_Verde || i.Inter_Verde_Lima > 5)
            {
                ErrorProvider err = new ErrorProvider();
                err.Icon = global::ETdA.Properties.Resources.notification_warning_ico;
                err.SetError(b4, "Máximo da cor Verde Lima inválida.");
                podeAdicionar = false;
                if (!erros.Keys.Contains(b4))
                    erros.Add(b4, err);
            }

            return podeAdicionar;
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

        private void KeyPressActionPerformed(object sender, KeyPressEventArgs e)
        {
            if (erros.Keys.Contains(sender))
            {
                ErrorProvider err = erros[sender];
                err.Clear();
                erros.Remove(sender);
            }
        }

        private void MouseClickActionPerformed(object sender, EventArgs e)
        {
            if (erros.Keys.Contains(sender))
            {
                ErrorProvider err = erros[sender];
                err.Clear();
                erros.Remove(sender);
            }
        }
    }
}
