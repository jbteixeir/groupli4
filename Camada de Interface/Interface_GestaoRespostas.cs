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
    public partial class Interface_GestaoRespostas : Form
    {
        private Dictionary<string, List<TipoEscala>> resps;

        public Interface_GestaoRespostas()
        {
            InitializeComponent();
            panel1.AutoScroll = true;

            resps = GestaodeAnalises.getTipResposta();
            initTree();
        }

        public static void main()
        {
            Interface_GestaoRespostas igr = new Interface_GestaoRespostas();
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
                    sub_nodo.Text = resps[tipo][j].Descricao + " " + resps[tipo][j].Numero;
                    sub_nodo.Name = "" + j;
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
                MessageBox.Show(value1 + " " + value2.ToString());
                int nu;
                switch (value2)
                {
                    case 0 : nu = 2; break;
                    case 1 : nu = -2; break;
                    case 2 : nu = 0; break;
                    default : nu = 1; break;
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
                initTree();
            }
        }

        private void SeleccionarMouseClicked(object sender, EventArgs e)
        {

        }

        private void show_respostas(TipoEscala te, string s)
        {
            panel1.Controls.Clear();
            Label l1 = new System.Windows.Forms.Label();
            l1.Text = s;
            l1.Width = 100;
            l1.Location = new System.Drawing.Point(10, 10);
            panel1.Controls.Add(l1);

            int y = 40;
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
                Label l5 = new System.Windows.Forms.Label();
                l5.Text = "Valor";
                l5.Width = 100;
                l5.Location = new System.Drawing.Point(110, 10);
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
                    panel1.Controls.Add(cb);

                    if (te.Default == 0)
                    {
                        cb.Enabled = true;
                        Label l2 = new System.Windows.Forms.Label();
                        l2.Text = "Eliminar";
                        l2.Name = s +"."+i.ToString();
                        l2.Cursor = System.Windows.Forms.Cursors.Hand;
                        l2.Click += new System.EventHandler(NovaRespostaMouseClicked);
                        l2.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                        l2.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
                        l2.Location = new System.Drawing.Point(160, y);
                        panel1.Controls.Add(l2);
                    }
                    y += 30;
                    i++;
                }

                if (te.Default == 1)
                {
                    Label l3 = new System.Windows.Forms.Label();
                    l3.Text = "Novo";
                    l3.Name = "check." + s;
                    //l2.Width = 60;
                    l3.Cursor = System.Windows.Forms.Cursors.Hand;
                    l3.Click += new System.EventHandler(NovaRespostaMouseClicked);
                    l3.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                    l3.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
                    l3.Location = new System.Drawing.Point(10, y);
                    panel1.Controls.Add(l3);
                }
            }
            else if (te.Numero > 1)
            {
                Label l5 = new System.Windows.Forms.Label();
                l5.Text = "Valor";
                l5.Width = 100;
                l5.Location = new System.Drawing.Point(110, 10);
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
                    panel1.Controls.Add(cb);

                    if (te.Default == 0)
                    {
                        cb.Enabled = true;

                        Label l2 = new System.Windows.Forms.Label();
                        l2.Text = "Eliminar";
                        l2.Name = s + "." + i.ToString();
                        l2.Cursor = System.Windows.Forms.Cursors.Hand;
                        l2.Click += new System.EventHandler(EliminarMouseClicked);
                        l2.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                        l2.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
                        l2.Location = new System.Drawing.Point(160, y);
                        panel1.Controls.Add(l2);
                    }
                    y += 30;
                    i++;
                }
                if (te.Default == 0)
                {
                    Label l3 = new System.Windows.Forms.Label();
                    l3.Text = "Novo";
                    l3.Name = "radio." + s;
                    //l2.Width = 10;
                    l3.Cursor = System.Windows.Forms.Cursors.Hand;
                    l3.Click += new System.EventHandler(NovaRespostaMouseClicked);
                    l3.MouseEnter += new System.EventHandler(this.MouseEnterAction);
                    l3.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
                    l3.Location = new System.Drawing.Point(10, y);
                    panel1.Controls.Add(l3);
                }
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
            string key = b.Name.Split('.')[1];
            TipoEscala te = resps[key.Split(' ')[0]][getIndice(key)];

            string value = "";
            if (InputBox("Nova Resposta", "Nome:", ref value) == DialogResult.OK)
            {
                if (!verificaExiste(te, value))
                {
                    EscalaResposta er = new EscalaResposta(te.Codigo, value, te.Respostas.Count + 1);
                    List<EscalaResposta> lst = te.Respostas;
                    lst.Add(er);
                    te.Respostas = lst;
                    show_respostas(te, te.Descricao + " " + te.Numero);
                }
                else
                {
                    MessageBox.Show("Resposta já existente neste tipo.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool verificaExiste(TipoEscala te, string s)
        {
            bool found = false;
            for (int i = 0; i < te.Respostas.Count && !found; i++)
                if (te.Respostas[i].Descricao == s)
                    found = true;
            return found;
        }

        private int getIndice(string s)
        {
            bool b = false;
            int i, num = int.Parse(s.Split(' ')[1]);
            for (i = 0; i < resps[s.Split(' ')[0]].Count && !b; i++)
                if (resps[s.Split(' ')[0]][i].Numero == num)
                    b = true;
            return i-1;
        }

        private void EliminarMouseClicked(object sender, EventArgs e)
        {
            Label l = (Label)sender;
            string key = l.Name.Split('.')[0];
            int i = int.Parse(l.Name.Split('.')[1]);
            TipoEscala te = resps[key.Split(' ')[0]][getIndice(key)];
            List<EscalaResposta> lst = te.Respostas;

            updateEscalasResposta(lst, i);

            lst.RemoveAt(i);
            te.Respostas = lst;

            show_respostas(te, key);
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
                    MenuItem mi = new MenuItem("Eliminar Nodo", new EventHandler(EliminarNodo));
                    mi.Name = e.Node.Text;
                    m.MenuItems.Add(mi);
                    e.Node.ContextMenu = m;
                }
            }
            else
            {
                if (e.Node.Level == 1)
                {
                    TipoEscala te = resps[e.Node.Parent.Text][int.Parse(e.Node.Name)];
                    MessageBox.Show(e.Node.Text);
                    show_respostas(te, e.Node.Text);
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
            cb2.Items.AddRange(new Object[] { "Uma opção em várias", "Várias opções", "Caixa de texto (letras)", "Caixa de texto (números)" });
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
            else value1 = cb1.Items[cb1.SelectedIndex].ToString();
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
            MessageBox.Show("Not implemented yet.\n"+sender.GetType().ToString());

            //MenuItem l = (MenuItem)sender;
            //string key = l.Name;
            //resps[key.Split(' ')[0]].RemoveAt(getIndice(key));

            //initTree();

            MessageBox.Show(treeView1.SelectedNode.Text);
        }
    }
}
