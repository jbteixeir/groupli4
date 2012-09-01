using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ETdAnalyser.Camada_de_Negócio;
using ETdAnalyser.CamadaDados.Classes;

namespace ETdAnalyser.CamadaInterface
{
    public partial class InterfaceGestaoRespostas : Form
    {
        private int num_pergunta;
        private Dictionary<string, List<TipoEscala>> resps;
        private List<TipoEscala> novos;
        private bool fa;

        private delegate void eventoEventHandler(object sender, EventArgs e);
        private static event eventoEventHandler done_action;

        public InterfaceGestaoRespostas(int num_perg, bool fa)
        {
            this.fa = fa;
            InitializeComponent();
            panel1.AutoScroll = true;

            if (fa)
                done_action += new eventoEventHandler(
                   CamadaInterface.InterfacePerguntas.reenc_New_Anser);
            else
                done_action += new eventoEventHandler(
                   CamadaInterface.InterfacePerguntasQT.reenc_New_Anser);

            num_pergunta = num_perg;
            resps = GestaodeRespostas.getTipResposta();
            initTree();
            novos = new List<TipoEscala>();
        }

        public static void main(int num_perg, bool fa)
        {
            InterfaceGestaoRespostas igr = new InterfaceGestaoRespostas(num_perg, fa);
            igr.Visible = true;
        }

        private void initTree()
        {
            treeView1.Nodes.Clear();

            for (int i = 0; i < resps.Keys.Count ; i++)
            {
                TreeNode nodo = new TreeNode();
                string tipo = resps.Keys.ElementAt(i);
                nodo.Text = tipo;
                for (int j = 0; j < resps[tipo].Count; j++)
                {
                    TreeNode sub_nodo = new TreeNode();
                    sub_nodo.Text = "Tipo " + j;
                    sub_nodo.Name = tipo;
                    nodo.Nodes.Add(sub_nodo);
                }
                treeView1.Nodes.Add(nodo);
            }
        }

        private void NovoMouseClicked(object sender, EventArgs e)
        {
            string value1 = "";
            int value2 = 0;
            if (DualInputBox("Novo Tipo", "Nome:", ref value1, ref value2) == DialogResult.OK)
            {
                int nu ;
                switch (value2)
                {
                    case 0 : nu = 2; break;
                    case 1 : nu = -2; break;
                    case 2 : nu = 0; break;
                    case 3 : nu = 1; break;
                    default: nu = -3; break;
                }
                TipoEscala te_novo = new TipoEscala(value1, nu);
                if (resps.ContainsKey(value1))
                    resps[value1].Add(te_novo);
                else
                {
                    List<TipoEscala> lst = new List<TipoEscala>();
                    lst.Add(te_novo);
                    resps.Add(value1, lst);
                }
                novos.Add(te_novo);
                panel1.Controls.Clear();
                initTree();
            }
        }

        private void SeleccionarMouseClicked(object sender, EventArgs e)
        {
            string erro = "";
            if (verifica(ref erro))
            {
                resps = GestaodeRespostas.insertNovosTipos(resps);

                string key = panel1.Controls[0].Text;
                int indice = int.Parse(panel1.Controls[1].Text.Split(' ')[1]);

                long cod = resps[key][indice].CodigoTipo;

                List<object> sender2 = new List<object>();
                sender2.Add(num_pergunta);
                sender2.Add(cod);

                done_action(sender2, new EventArgs());
                end_Frame();
            }
            else
            {
                MessageBox.Show(erro);
            }
        }

        private bool verifica(ref string erro)
        {
            bool ok = true;

            if (panel1.Controls.Count != 0)
            {
                string key = panel1.Controls[0].Text;
                int indice = int.Parse(panel1.Controls[1].Text.Split(' ')[1]);
                if (fa && resps[key][indice].Numero < 2)
                {
                    ok = false;
                    erro += "Apenas pode seleccionar respostas que sejam do tipo 'Uma Opção'.";
                }

                for (int i = 0; i < novos.Count && ok; i++)
                {
                    erro = "Tipo de Resposta em " + novos[i].Descricao + "\n";
                    if (novos[i].Numero == -2 && novos[i].Respostas.Count < 1)
                    {
                        ok = false;
                        erro += "Erro: É necessário colocar pelo menos uma hipótese de resposta.";
                    }
                    else if (novos[i].Numero == 2 && novos[i].Respostas.Count < 2 && ok)
                    {
                        ok = false;
                        erro += "Erro: É necessário colocar pelo menos duas hipóteses de resposta.";
                    }

                    if ((novos[i].Numero == -2 || novos[i].Numero == 2) && ok)
                        for (int j = 0; j < novos[i].Respostas.Count && ok; j++)
                            for (int z = j + 1; z < novos[i].Respostas.Count && ok; z++)
                                if (novos[i].Respostas[j].Valor == novos[i].Respostas[z].Valor)
                                {
                                    ok = false;
                                    erro += "Erro: Existem duas hipóteses de resposta com o mesmo valor.";
                                }
                }
            }
            else
            {
                ok = false;
                erro += "Deve seleccionar um tipo de resposta.";
            }
                return ok;
        }

        private void show_respostas(TipoEscala te, string s, string tip1, string tip2)
        {
            panel1.Controls.Clear();
            Label l1 = new System.Windows.Forms.Label();
            l1.Text = tip1;
            l1.Width = 100;
            l1.Location = new System.Drawing.Point(10, 10);
            panel1.Controls.Add(l1);

            Label l2 = new System.Windows.Forms.Label();
            l2.Text = s;
            l2.Location = new System.Drawing.Point(10, 40);
            panel1.Controls.Add(l2);

            Label l6 = new System.Windows.Forms.Label();
            l6.Text = "(" + tip2 + ")";
            l6.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            l6.Location = new System.Drawing.Point(10, 40+l2.Height);
            panel1.Controls.Add(l6);

            int y = 90;
            if (te.Numero >= -1 && te.Numero <= 1)
            {
                TextBox t2 = new System.Windows.Forms.TextBox();
                t2.Width = 100;
                t2.Name = "t_box";
                t2.Enabled = false;
                t2.Location = new System.Drawing.Point(10, y);
                panel1.Controls.Add(t2);
            }
            else if (te.Numero == -2)
            {
                #region Check
                Label l5 = new System.Windows.Forms.Label();
                l5.Text = "Valor";
                l5.Width = 100;
                l5.Location = new System.Drawing.Point(110, 40);
                panel1.Controls.Add(l5);

                int i = 0;
                foreach (EscalaResposta er in te.Respostas)
                {
                    CheckBox c = new System.Windows.Forms.CheckBox();
                    c.Text = er.Descricao;
                    c.Enabled = false;
                    c.Location = new System.Drawing.Point(10, y);
                    panel1.Controls.Add(c);

                    ComboBox cb = new System.Windows.Forms.ComboBox();
                    cb.Items.AddRange(getRange(te));
                    cb.SelectedItem = er.Valor;
                    cb.Width = 40;
                    cb.Location = new System.Drawing.Point(115, y);
                    cb.Enabled = false;
                    cb.Name = te.Descricao + "." + s.Split(' ')[1] + "." + i.ToString();
                    cb.SelectedIndexChanged += new System.EventHandler(IndexChanchedEvent);
                    panel1.Controls.Add(cb);

                    if (te.TipoPredefinido == 0)
                    {
                        cb.Enabled = true;
                        Label l3 = new System.Windows.Forms.Label();
                        l3.Text = "Eliminar";
                        l3.Name = te.Descricao + "." + s.Split(' ')[1] + "." + i.ToString();
                        l3.Cursor = System.Windows.Forms.Cursors.Hand;
                        l3.Click += new System.EventHandler(EliminarMouseClicked);
                        l3.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                        l3.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
                        l3.Location = new System.Drawing.Point(160, y);
                        panel1.Controls.Add(l3);
                    }
                    y += 30;
                    i++;
                }

                if (te.TipoPredefinido == 0)
                {
                    Label l3 = new System.Windows.Forms.Label();
                    l3.Text = "Novo";
                    l3.AutoSize = true;
                    l3.Name = te.Descricao + "." + s.Split(' ')[1];
                    l3.Cursor = System.Windows.Forms.Cursors.Hand;
                    l3.Click += new System.EventHandler(NovaRespostaMouseClicked);
                    l3.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                    l3.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
                    l3.Location = new System.Drawing.Point(10, y);
                    panel1.Controls.Add(l3);
                }
                #endregion
            }
            else if (te.Numero > 1 || te.Numero == -3)
            {
                #region Radio
                Label l5 = new System.Windows.Forms.Label();
                l5.Text = "Valor";
                l5.Width = 100;
                l5.Location = new System.Drawing.Point(110, 40);
                panel1.Controls.Add(l5);

                int i = 0;
                foreach (EscalaResposta er in te.Respostas)
                {
                    RadioButton r = new System.Windows.Forms.RadioButton();
                    r.Text = er.Descricao;
                    r.Enabled = false;
                    r.Location = new System.Drawing.Point(10, y);
                    r.Width = 100;
                    panel1.Controls.Add(r);

                    ComboBox cb = new System.Windows.Forms.ComboBox();
                    cb.Items.AddRange(getRange(te));
                    cb.SelectedItem = er.Valor;
                    cb.Width = 40;
                    cb.Location = new System.Drawing.Point(115, y);
                    cb.Enabled = false;
                    cb.Name = te.Descricao + "." + s.Split(' ')[1] + "." + i.ToString();
                    cb.SelectedIndexChanged += new System.EventHandler(IndexChanchedEvent);
                    panel1.Controls.Add(cb);

                    if (te.TipoPredefinido == 0)
                    {
                        cb.Enabled = true;

                        Label l3 = new System.Windows.Forms.Label();
                        l3.Text = "Eliminar";
                        l3.Name = te.Descricao + "." + s.Split(' ')[1] + "." + i.ToString();
                        l3.Cursor = System.Windows.Forms.Cursors.Hand;
                        l3.Click += new System.EventHandler(EliminarMouseClicked);
                        l3.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                        l3.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
                        l3.Location = new System.Drawing.Point(160, y);
                        panel1.Controls.Add(l3);
                    }
                    y += 30;
                    i++;
                }
                if (te.TipoPredefinido == 0)
                {
                    Label l3 = new System.Windows.Forms.Label();
                    l3.Text = "Novo";
                    l3.AutoSize = true;
                    l3.Name = te.Descricao + "." + s.Split(' ')[1];
                    l3.Cursor = System.Windows.Forms.Cursors.Hand;
                    l3.Click += new System.EventHandler(NovaRespostaMouseClicked);
                    l3.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                    l3.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
                    l3.Location = new System.Drawing.Point(10, y);
                    panel1.Controls.Add(l3);
                }
                #endregion
            }
        }

        private object[] getRange(TipoEscala te)
        {
            object[] ret = new object[te.Respostas.Count];
            for (int i = 0; i < te.Respostas.Count; i++)
                ret[i] = te.Respostas[i].Valor;
            return ret;
        }

        private void MouseEnterAction(object sender, EventArgs e)
        {
            Label t = (Label)sender;
            t.Font = new Font(t.Font, FontStyle.Underline);
            t.BackColor = Color.LightGray;
        }

        private void MouseLeaveAction(object sender, EventArgs e)
        {
            Label t = (Label)sender;
            t.Font = new Font(t.Font, FontStyle.Regular);
            t.BackColor = Color.Empty;
        }

        private void NovaRespostaMouseClicked(object sender, EventArgs e)
        {
            Label b = (Label)sender;
            string key = b.Name.Split('.')[0];
            int indice = int.Parse(b.Name.Split('.')[1]);
            TipoEscala te = resps[key][indice];

            string value = "";
            if (InputBox("Nova Resposta", "Nome:", ref value) == DialogResult.OK)
            {
                if (!verificaExiste(te, value))
                {
                    EscalaResposta er = new EscalaResposta(te.CodigoTipo, value, te.Respostas.Count + 1);
                    List<EscalaResposta> lst = te.Respostas;
                    lst.Add(er);
                    te.Respostas = lst;
                    string x;
                    if (te.Numero == 0) x = "Letras";
                    else if (te.Numero == 1) x = "Número";
                    else if (te.Numero == -2) x = "Várias Opções";
                    else x = "Uma Opção";
                    show_respostas(te, "Tipo " + indice.ToString(), te.Descricao, x);
                }
                else
                {
                    MessageBox.Show("Resposta já existente neste tipo.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool nomeValido(string p)
        {
            if (p == "") return false;
            string possiveis = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVKWXYZ0123456789_-/";
            bool found = true;
            for (int i = 0; i < p.Length && found; i++)
                found = possiveis.Contains(p[i]);
            return found;
        }

        private bool verificaExiste(TipoEscala te, string s)
        {
            bool found = false;
            for (int i = 0; i < te.Respostas.Count && !found; i++)
                if (te.Respostas[i].Descricao == s)
                    found = true;
            return found;
        }

        private void EliminarMouseClicked(object sender, EventArgs e)
        {
            Label l = (Label)sender;
            string key = l.Name.Split('.')[0];
            int indice1 = int.Parse(l.Name.Split('.')[1]);
            int indice2 = int.Parse(l.Name.Split('.')[2]);

            TipoEscala te = resps[key][indice1];
            List<EscalaResposta> lst = te.Respostas;

            updateEscalasResposta(lst, indice2);

            lst.RemoveAt(indice2);
            te.Respostas = lst;

            string x;
            if (te.Numero == 0) x = "Letras";
            else if (te.Numero == 1) x = "Número";
            else if (te.Numero == -2) x = "Várias Opções";
            else x = "Uma Opção";
            show_respostas(te, "Tipo " + indice1.ToString(), te.Descricao, x);
        }

        private void updateEscalasResposta(List<EscalaResposta> lst, int i)
        {
            for (int j = i + 1; j < lst.Count; j++)
                lst[j].Valor--;  
        }

        private void TreeMouseClicked(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.Node.Level == 1)
                {
                    ContextMenu m = new ContextMenu();
                    MenuItem mi = new MenuItem("Eliminar", new EventHandler(EliminarNodo));
                    mi.Name = e.Node.Name + " " + e.Node.Text.Split(' ')[1];
                    m.MenuItems.Add(mi);
                    e.Node.ContextMenu = m;
                }
            }
            else
            {
                if (e.Node.Level == 1)
                {
                    TipoEscala te = resps[e.Node.Name][int.Parse(e.Node.Text.Split(' ')[1])];
                    string x;
                    if (te.Numero == 0) x = "Letras";
                    else if (te.Numero == 1) x = "Número";
                    else if (te.Numero == -2) x = "Várias Opções";
                    else x = "Uma Opção";
                    show_respostas(te, e.Node.Text,te.Descricao,x);
                }
            }
        }

        private DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }

        private DialogResult DualInputBox(string title, string promptText, ref string value1, ref int value2)
        {
            Form form = new Form();
            Label label1 = new Label();
            Label label2 = new Label();
            Label label3 = new Label();
            ComboBox cb1 = new ComboBox();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();
            ComboBox cb2 = new ComboBox();

            form.Text = title;
            label1.Text = promptText;

            label2.Text = "Nome:";
            label3.Text = "Tipo de Resposta:";
            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";

            int i;
            object[] its = new object[resps.Keys.Count + 1];
            for (i = 0; i < resps.Keys.Count; i++)
                its[i] = resps.Keys.ElementAt(i);
            its[i] = "Outro";

            cb1.Items.AddRange(its);
            cb1.SelectedIndex = 0;
            cb2.Items.AddRange(new Object[] { "Uma opção", "Várias opções", "Caixa de texto (letras)", "Caixa de texto (números)", "Classificação" });
            cb2.SelectedIndex = 0;
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label1.Height = 13;
            label1.Width = 230;
            label1.Location = new System.Drawing.Point(10, 10);
            cb1.Height = 20;
            cb1.Width = 230;
            cb1.Location = new System.Drawing.Point(10, 26);
            cb1.SelectedIndexChanged += new EventHandler(cb1_SelectedIndexChanged);
            label2.Height = 13;
            label2.Width = 40;
            label2.Location = new System.Drawing.Point(10, 49);
            label2.Visible = false;
            textBox.Height = 20;
            textBox.Width = 185;
            textBox.Location = new System.Drawing.Point(55, 49);
            textBox.Visible = false;
            label3.Height = 13;
            label3.Width = 230;
            label3.Location = new System.Drawing.Point(10, 72);
            cb2.Height = 20;
            cb2.Width = 230;
            cb2.Location = new System.Drawing.Point(10, 88);
            buttonOk.Height = 23;
            buttonOk.Width = 75;
            buttonOk.Location = new System.Drawing.Point(165, 118);
            buttonCancel.Height = 23;
            buttonCancel.Width = 75;
            buttonCancel.Location = new System.Drawing.Point(80, 118);

            form.ClientSize = new Size(250, 144);
            form.Controls.AddRange(new Control[] { label1, cb1, label2, textBox, label3, cb2, buttonOk, buttonCancel });
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            if (cb1.SelectedIndex == its.Length - 1) value1 = textBox.Text;
            else if (cb1.SelectedIndex >= 0 &&
                cb1.SelectedIndex <= its.Length - 1)
                value1 = cb1.Items[cb1.SelectedIndex].ToString();
            else value1 = cb1.Text;
            value2 = cb2.SelectedIndex;
            return dialogResult;
        }

        private void cb1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox b = (ComboBox)sender;
            if (b.SelectedIndex == b.Items.Count - 1)
            {
                b.FindForm().Controls[2].Visible = true;
                b.FindForm().Controls[3].Visible = true;
            }
            else
            {
                b.FindForm().Controls[2].Visible = false;
                b.FindForm().Controls[3].Visible = false;
            }
        }

        private void end_Frame()
        {
            Dispose();
            Close();
        }

        private void CancelarMouseClicked(object sender, EventArgs e)
        {
            end_Frame();
        }

        private void EliminarNodo(object sender, EventArgs e)
        {
            MenuItem l = (MenuItem)sender;
            string key = l.Name;
            string k = key.Split(' ')[0];
            int ind = int.Parse(key.Split(' ')[1]);
            if (resps[k][ind].TipoPredefinido == 0)
            {
                resps[k].RemoveAt(ind);
                if (resps[k].Count == 0)
                    resps.Remove(k);
                initTree();
                panel1.Controls.Clear();
            }
            else
            {
                MessageBox.Show("Não é possível elimnar este tipo.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void IndexChanchedEvent(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            string key = cb.Name.Split('.')[0];
            int indice1 = int.Parse(cb.Name.Split('.')[1]);
            int indice2 = int.Parse(cb.Name.Split('.')[2]);

            List<EscalaResposta> er_lst = resps[key][indice1].Respostas;
            er_lst[indice2].Valor = (int)cb.SelectedItem;
            resps[key][indice1].Respostas = er_lst;
        }
    }
}
