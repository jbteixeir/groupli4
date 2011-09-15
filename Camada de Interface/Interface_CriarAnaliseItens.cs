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
        private bool adding; // se esta a adicionar ou a ver os itens

        public Interface_CriarAnaliseItens(object o, bool b)
        {
            InitializeComponent();
            toolStripStatusLabel4.Visible = false;
            toolStripStatusLabel5.Visible = false;
            toolStripStatusLabel6.Visible = false;

            erros = new Dictionary<object, ErrorProvider>();
            itens = (List<Item>) o;
            adding = b;

            done_action += new eventoEventHandler(
               Camada_de_Interface.Interface_CriarAnalise.ItensOkReenc);

            defaults = GestaodeAnalises.getItensDefault();
            alls = GestaodeAnalises.getTodosItens();

            panel1.AutoScroll = true;
            panel1.Controls.Clear();
            panels = new Dictionary<string, Dictionary<string, object>>();

            init_checked_list();
        }

        public static void main(object o, bool b)
        {
            Interface_CriarAnaliseItens icai = new Interface_CriarAnaliseItens(o, b);
            icai.ShowDialog();
        }

        #region Começo
        private void init_checked_list()
        {
            if (itens.Count == 0 && adding) // Está a criar a primeira vez
                for (int i = 0; i < defaults.Count; i++)
                    checkedListBox1.Items.Add(defaults.Values.ElementAt(i), CheckState.Checked);
            else if (itens.Count != 0 && adding){ // Está a mudar o que já tinha criado
                int i = 0;
                for (i = 0; i < defaults.Count; i++)
                { 
                    if (contemNaListaItens(defaults.Values.ElementAt(i)))
                        checkedListBox1.Items.Add(defaults.Values.ElementAt(i), CheckState.Checked);
                    else
                        checkedListBox1.Items.Add(defaults.Values.ElementAt(i), CheckState.Unchecked);
                }
                for (int j = 0; j < itens.Count; j++ )
                    if (!defaults.Values.Contains(itens[j].NomeItem))
                        checkedListBox1.Items.Add(itens[j].NomeItem, CheckState.Checked);
            }
            else
            {
                for (int i = 0; i < itens.Count; i++ )
                    checkedListBox1.Items.Add(itens[i].NomeItem, CheckState.Checked);
                button1.Visible = false;
                button2.Visible = false;
                button4.Visible = false;
                textBox1.Visible = false;
                checkedListBox1.Enabled = false;
            }
        }

        private bool contemNaListaItens(string s)
        {
            bool found = false;
            for(int i = 0 ; i < itens.Count && !found ; i++)
                if (itens[i].NomeItem == s)
                    found = true;
            return found;
        }

        private void show_panel_item(Item item, int i)
        {
            Dictionary<string, object> controls = new Dictionary<string, object>();
            Panel p = new System.Windows.Forms.Panel();
            p.Name = checkedListBox1.Items[i].ToString();
            p.TabIndex = i;
            p.AutoSize = true;
            p.BorderStyle = BorderStyle.FixedSingle;
            p.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            p.Dock = DockStyle.Top;
            panel1.Controls.Add(p);
            panel1.Controls.SetChildIndex(p, getNumberCheckedItensDown(i));

            Label l = new System.Windows.Forms.Label();
            l.AutoSize = true;
            l.Text = checkedListBox1.Items[i].ToString();
            l.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F,System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            l.Location = new System.Drawing.Point(7, 7);
            p.Controls.Add(l);

            Label l2 = new System.Windows.Forms.Label();
            l2.Width = 150;
            l2.Text = "Ponderacao do Analista:";
            l2.Location = new System.Drawing.Point(7, 37);
            p.Controls.Add(l2);

            NumericUpDown n = new System.Windows.Forms.NumericUpDown();
            n.Increment = new Decimal(0.1);
            n.Maximum = 1;
            n.DecimalPlaces = 3;
            n.Value = new decimal((item != null) ? item.PonderacaoAnalista : 0.334);
            n.Location = new System.Drawing.Point(167, 37);
            n.KeyPress += new KeyPressEventHandler(KeyPressActionPerformed);
            n.Click += new EventHandler(MouseClickActionPerformed);
            if (!adding) n.Enabled = false;
            controls.Add(s1, n);
            p.Controls.Add(n);

            Label l3 = new System.Windows.Forms.Label();
            l3.Width = 150;
            l3.Text = "Ponderacao do Profissional:";
            l3.Location = new System.Drawing.Point(7, 67);
            p.Controls.Add(l3);

            NumericUpDown n2 = new System.Windows.Forms.NumericUpDown();
            n2.Increment = new Decimal(0.1);
            n2.Maximum = 1;
            n2.DecimalPlaces = 3;
            n2.Value = new decimal((item != null) ? item.PonderacaoProfissional : 0.333);
            n2.Location = new System.Drawing.Point(167, 67);
            n2.KeyPress += new KeyPressEventHandler(KeyPressActionPerformed);
            n2.Click += new EventHandler(MouseClickActionPerformed);
            if (!adding) n2.Enabled = false;
            controls.Add(s2, n2);
            p.Controls.Add(n2);

            Label l4 = new System.Windows.Forms.Label();
            l4.Width = 150;
            l4.Text = "Ponderacao do Cliente:";
            l4.Location = new System.Drawing.Point(7, 97);
            p.Controls.Add(l4);

            NumericUpDown n3 = new System.Windows.Forms.NumericUpDown();
            n3.Increment = new Decimal(0.1);
            n3.Maximum = 1;
            n3.DecimalPlaces = 3;
            n3.Value = new decimal((item != null) ? item.PonderacaoCliente : 0.333);
            n3.Location = new System.Drawing.Point(167, 97);
            n3.KeyPress += new KeyPressEventHandler(KeyPressActionPerformed);
            n3.Click += new EventHandler(MouseClickActionPerformed);
            if (!adding) n3.Enabled = false;
            controls.Add(s3, n3);
            p.Controls.Add(n3);

            Label l5 = new System.Windows.Forms.Label();
            l5.Width = 150;
            l5.Text = "Escalas";
            l5.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F,
                System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            l5.Location = new System.Drawing.Point(7, 137);
            p.Controls.Add(l5);

            Label l6 = new System.Windows.Forms.Label();
            l6.Width = 100;
            l6.Text = "Vermelho Max:";
            l6.Location = new System.Drawing.Point(7, 162);
            p.Controls.Add(l6);

            TextBox b1 = new System.Windows.Forms.TextBox();
            b1.Height = 20;
            b1.Width = 40;
            b1.Text = (item != null) ? "" + item.Inter_Vermelho : "1";
            b1.Location = new System.Drawing.Point(107, 162);
            b1.KeyPress += new KeyPressEventHandler(KeyPressActionPerformed);
            b1.Click += new EventHandler(MouseClickActionPerformed);
            if (!adding) b1.Enabled = false;
            controls.Add(s4, b1);
            p.Controls.Add(b1);

            Label l8 = new System.Windows.Forms.Label();
            l8.Width = 100;
            l8.Text = "Laranja Max:";
            l8.Location = new System.Drawing.Point(7, 187);
            p.Controls.Add(l8);

            TextBox b3 = new System.Windows.Forms.TextBox();
            b3.Height = 20;
            b3.Width = 40;
            b3.Text = (item != null) ? "" + item.Inter_Laranja : "2";
            b3.Location = new System.Drawing.Point(107, 187);
            b3.KeyPress += new KeyPressEventHandler(KeyPressActionPerformed);
            b3.Click += new EventHandler(MouseClickActionPerformed);
            if (!adding) b3.Enabled = false;
            controls.Add(s5, b3);
            p.Controls.Add(b3);

            Label l10 = new System.Windows.Forms.Label();
            l10.Width = 100;
            l10.Text = "Amarelo Max:";
            l10.Location = new System.Drawing.Point(7, 212);
            p.Controls.Add(l10);

            TextBox b5 = new System.Windows.Forms.TextBox();
            b5.Height = 20;
            b5.Width = 40;
            b5.Text = (item != null) ? "" + item.Inter_Amarelo : "3";
            b5.Location = new System.Drawing.Point(107, 212);
            b5.KeyPress += new KeyPressEventHandler(KeyPressActionPerformed);
            b5.Click += new EventHandler(MouseClickActionPerformed);
            if (!adding) b5.Enabled = false;
            controls.Add(s6, b5);
            p.Controls.Add(b5);

            Label l12 = new System.Windows.Forms.Label();
            l12.Width = 100;
            l12.Text = "Verde Lima Max:";
            l12.Location = new System.Drawing.Point(7, 237);
            p.Controls.Add(l12);

            TextBox b7 = new System.Windows.Forms.TextBox();
            b7.Height = 20;
            b7.Width = 40;
            b7.Text = (item != null) ? "" + item.Inter_Verde_Lima : "4";
            b7.Location = new System.Drawing.Point(107, 237);
            b7.KeyPress += new KeyPressEventHandler(KeyPressActionPerformed);
            b7.Click += new EventHandler(MouseClickActionPerformed);
            if (!adding) b7.Enabled = false;
            controls.Add(s7, b7);
            p.Controls.Add(b7);

            Label l14 = new System.Windows.Forms.Label();
            l14.Width = 100;
            l14.Text = "Verde Max:";
            l14.Location = new System.Drawing.Point(7, 262);
            p.Controls.Add(l14);

            TextBox b9 = new System.Windows.Forms.TextBox();
            b9.Height = 20;
            b9.Width = 40;
            b9.Text = "5";
            b9.Enabled = false;
            b9.Location = new System.Drawing.Point(107, 262);
            controls.Add(s8, b9);
            p.Controls.Add(b9);

            Label l15 = new System.Windows.Forms.Label();
            l14.Width = 150;
            l14.Text = "Limite Inferior Analista:";
            l14.Location = new System.Drawing.Point(7, 302);
            p.Controls.Add(l14);

            NumericUpDown n4 = new System.Windows.Forms.NumericUpDown();
            n4.Increment = new Decimal(0.1);
            n4.Maximum = 5;
            n4.DecimalPlaces = 3;
            n4.Value = new Decimal((item != null) ? item.LimiteInferiorAnalista : 0);
            n4.Location = new System.Drawing.Point(157, 302);
            if (!adding) n4.Enabled = false;
            controls.Add(s9, n4);
            p.Controls.Add(n4);

            panels.Add(p.Name, controls);
        }

        private int getNumberCheckedItensDown(int index)
        {
            int num = 0;
            for (int i = index + 1; i < checkedListBox1.Items.Count; i++)
                if (checkedListBox1.CheckedItems.Contains(checkedListBox1.Items[i]))
                    num++;
            return num;
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
        #endregion

        #region Eventos Gestao
        private void AdicionarActionPerformed(object sender, EventArgs e)
        {
            string s = textBox1.Text;

            if (!nomeItemValido(s))
            {
                if (!erros.ContainsKey(textBox1))
                {
                    ErrorProvider err = new ErrorProvider();
                    err.Icon = global::ETdA.Properties.Resources.notification_warning_ico;
                    err.SetError(textBox1, "O nome do item não é válido.");

                    erros.Add(textBox1, err);
                }
            }
            else if (alls.Values.Contains(s))
            {
                if (!erros.ContainsKey(textBox1))
                {
                    ErrorProvider err = new ErrorProvider();
                    err.Icon = global::ETdA.Properties.Resources.notification_warning_ico;
                    err.SetError(textBox1, "Já existe um item com o nome " + s + ".");

                    erros.Add(textBox1, err);
                }
            }
            else
            {
                checkedListBox1.Items.Add(s, CheckState.Checked);
                textBox1.Text = "";
            }
            setErroStatusBar();
        }

        private bool nomeItemValido(string s)
        {
            if (s == "") return false;
            string possiveis = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVKWXYZ0123456789" +
                            "áàãâéèêíìîóòôõúùûçÁÀÂÃÉÈÊÍÌÎÓÒÕÔÚÙÛÇ ,.;:/()[]{}'?!_-|\\+ºª'";
            bool found = true;
            for (int i = 0; i < s.Length && found; i++)
                found = possiveis.Contains(s[i]);
            return found;
        }

        private void ItemCheckActionPerformed(object sender, ItemCheckEventArgs e)
        {
            if (e.CurrentValue == CheckState.Checked)
            {
                panel1.Controls.RemoveByKey(checkedListBox1.Items[e.Index].ToString());
                panels.Remove(checkedListBox1.Items[e.Index].ToString());
                toolStripStatusLabel2.Text = (checkedListBox1.CheckedIndices.Count - 1).ToString();
            }
            else
            {
                if (itens.Count == 0 || e.Index >= itens.Count)
                    show_panel_item(null, e.Index);
                else
                    show_panel_item(itens[e.Index], e.Index);
                toolStripStatusLabel2.Text = (checkedListBox1.CheckedIndices.Count + 1).ToString();
            }
        }

        private void MostrarTodosActionPerformed(object sender, EventArgs e)
        {
            foreach (string s in alls.Values)
                if (!checkedListBox1.Items.Contains(s))
                    checkedListBox1.Items.Add(s);
        }
        #endregion

        private void CancelarActionPerformed(object sender, EventArgs e)
        {
            end_Frame();
        }

        private void OK_ActionPerformed(object sender, EventArgs e)
        {
            if (verificaErros())
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
                    i.Inter_Vermelho = double.Parse(b1.Text);
                    i.Inter_Laranja = double.Parse(b2.Text);
                    i.Inter_Amarelo = double.Parse(b3.Text);
                    i.Inter_Verde_Lima = double.Parse(b4.Text);
                    i.Inter_Verde = double.Parse(b5.Text);
                    i.LimiteInferiorAnalista = double.Parse(n4.Value.ToString());

                    itens.Add(i);
                }

                List<object> obs = new List<object>();
                obs.Add(itens);
                obs.Add(nome_novos);

                done_action(obs, new EventArgs());
                end_Frame();
            }
        }

        #region verificação de erros
        private bool verificaErros()
        {
            bool podeAdicionar = true;
            foreach(Dictionary<string, object> bs in panels.Values)
            {
                NumericUpDown n1 = (NumericUpDown)bs[s1];
                NumericUpDown n2 = (NumericUpDown)bs[s2];
                NumericUpDown n3 = (NumericUpDown)bs[s3];
                TextBox b1 = (TextBox)bs[s4];
                TextBox b2 = (TextBox)bs[s5];
                TextBox b3 = (TextBox)bs[s6];
                TextBox b4 = (TextBox)bs[s7];
                TextBox b5 = (TextBox)bs[s8];
                NumericUpDown n4 = (NumericUpDown)bs[s9];

                #region Verificação das ponderações
                if (double.Parse(n1.Value.ToString()) +
                    double.Parse(n2.Value.ToString()) +
                    double.Parse(n3.Value.ToString()) != 1)
                {
                    if (!erros.Keys.Contains(n1))
                    {
                        ErrorProvider err = new ErrorProvider();
                        err.Icon = global::ETdA.Properties.Resources.notification_warning_ico;
                        err.SetError(n1, "O total das ponderações é diferente de 1.");
                        err.SetError(n2, "O total das ponderações é diferente de 1.");
                        err.SetError(n3, "O total das ponderações é diferente de 1.");

                        erros.Add(n1, err);
                    }
                    podeAdicionar = false;
                }
                #endregion
                #region Verificação dos máximos
                if (!maximo_valido(b1.Text) || double.Parse(b1.Text) > double.Parse(b2.Text) || double.Parse(b1.Text) > 5)
                {
                    if (!erros.Keys.Contains(b1))
                    {
                        ErrorProvider err = new ErrorProvider();
                        err.Icon = global::ETdA.Properties.Resources.notification_warning_ico;
                        err.SetError(b1, "Máximo da cor Vermelha inválida.");

                        erros.Add(b1, err);
                    }
                    podeAdicionar = false;
                }
                if (!maximo_valido(b2.Text) || double.Parse(b2.Text) > double.Parse(b3.Text) || double.Parse(b2.Text) > 5)
                {
                    if (!erros.Keys.Contains(b2))
                    {
                        ErrorProvider err = new ErrorProvider();
                        err.Icon = global::ETdA.Properties.Resources.notification_warning_ico;
                        err.SetError(b2, "Máximo da cor Laranja inválida.");

                        erros.Add(b2, err);
                    }
                    podeAdicionar = false;
                }
                if (!maximo_valido(b3.Text) || double.Parse(b3.Text) > double.Parse(b4.Text) || double.Parse(b3.Text) > 5)
                {
                    if (!erros.Keys.Contains(b3))
                    {
                        ErrorProvider err = new ErrorProvider();
                        err.Icon = global::ETdA.Properties.Resources.notification_warning_ico;
                        err.SetError(b3, "Máximo da cor Amarela inválida.");

                        erros.Add(b3, err);
                    }
                    podeAdicionar = false;
                }
                if (!maximo_valido(b4.Text) || double.Parse(b4.Text) > double.Parse(b5.Text) || double.Parse(b4.Text) > 5)
                {
                    if (!erros.Keys.Contains(b4))
                    {
                        ErrorProvider err = new ErrorProvider();
                        err.Icon = global::ETdA.Properties.Resources.notification_warning_ico;
                        err.SetError(b4, "Máximo da cor Verde Lima inválida.");

                        erros.Add(b4, err);
                    }
                    podeAdicionar = false;
                }
                #endregion
            }
            setErroStatusBar();
            return podeAdicionar;
        }

        private bool maximo_valido(string s)
        {
            if (s == "") return false;
            string possiveis = "0123456789,";
            bool found = true;
            for (int i = 0; i < s.Length && found; i++)
                found = possiveis.Contains(s[i]);
            return found;
        }

        private void setErroStatusBar()
        {
            if (erros.Count != 0)
            {
                TextBox tb = new TextBox();

                object p = erros.Keys.ElementAt(0);
                ErrorProvider err = (ErrorProvider)erros[p];

                if (p.GetType() == tb.GetType())
                {
                    tb = (TextBox)p;
                    if (tb.Equals(textBox1))
                        toolStripStatusLabel5.Text = "Adicionar: ";
                    else
                        toolStripStatusLabel5.Text = "Item: " + tb.Parent.Name;
                    toolStripStatusLabel6.Text = err.GetError(tb);
                }
                else
                {
                    NumericUpDown nud = (NumericUpDown)p;
                    toolStripStatusLabel5.Text = "Item: " + nud.Parent.Name;
                    toolStripStatusLabel6.Text = err.GetError(nud);
                }
                toolStripStatusLabel4.Visible = true;
                toolStripStatusLabel5.Visible = true;
                toolStripStatusLabel6.Visible = true;
            }
            else
            {
                toolStripStatusLabel4.Visible = false;
                toolStripStatusLabel5.Visible = false;
                toolStripStatusLabel6.Visible = false;
            }
        }
        #endregion

        private void end_Frame()
        {
            Dispose();
            Close();
        }

        #region Limpa Erros
        private void KeyPressActionPerformed(object sender, KeyPressEventArgs e)
        {
            if (erros.Keys.Contains(sender))
            {
                ErrorProvider err = erros[sender];
                err.Clear();
                erros.Remove(sender);
                setErroStatusBar();
            }
        }

        private void MouseClickActionPerformed(object sender, EventArgs e)
        {
            if (erros.Keys.Contains(sender))
            {
                ErrorProvider err = erros[sender];
                err.Clear();
                erros.Remove(sender);

                setErroStatusBar();
            }
            else
            {
                NumericUpDown num = new NumericUpDown();
                if (sender.GetType() == num.GetType())
                {
                    num = (NumericUpDown)sender;
                    if (num.Parent.Controls[num.Parent.Controls.Count - 1] != num &&
                        erros.Keys.Contains(num.Parent.Controls[2]))
                    {
                        ErrorProvider err = erros[num.Parent.Controls[2]];
                        err.Clear();
                        erros.Remove(num.Parent.Controls[2]);

                        setErroStatusBar();
                    }
                }
            }
        }
        #endregion
    }
}
