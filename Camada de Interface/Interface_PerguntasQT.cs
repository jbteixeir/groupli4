using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ETdA.Camada_de_Dados.Classes;
using ETdA.Camada_de_Negócio;

namespace ETdA.Camada_de_Interface
{
    public partial class Interface_PerguntasQT : Form
    {
        private static Interface_PerguntasQT ip;
        private long codAnalise;
        private List<TextBox> perguntas;
        private List<Label> tipos_resposta;
        private List<ComboBox> itens_pergunta;
        private List<ComboBox> zonas_pergunta;
        private List<ComboBox> tipos_pergunta;
        private Dictionary<object, ErrorProvider> erros;
        private List<Item> itens;
        private List<Zona> zonas;
        private List<PerguntaQuestionario> questionario;
        private bool already_created;

        private delegate void eventoEventHandler(object sender, EventArgs e);
        private static event eventoEventHandler evento_QT_Done;

        // revisto
        public Interface_PerguntasQT(long codAnalise, object itens, object zonas, bool created)
        {
            InitializeComponent();
            toolStripStatusLabel4.Visible = false;
            toolStripStatusLabel5.Visible = false;
            toolStripStatusLabel6.Visible = false;

            already_created = created;
            this.codAnalise = codAnalise;

            this.itens = (List<Item>)itens;
            this.zonas = (List<Zona>)zonas;
            evento_QT_Done += new eventoEventHandler(Camada_de_Interface.Interface_GestaoFormulariosOnline.done_QT_Reenc);

            erros = new Dictionary<object, ErrorProvider>();
            init_perg_q();

            init();
        }

        // revisto
        public static void main(long codAnalise, object itens, object zonas, bool created)
        {
            ip = new Interface_PerguntasQT(codAnalise, itens, zonas, created);
            ip.ShowDialog();
        }

        #region Começo
        // revisto
        private string[] nomes_itens()
        {
            string[] nomes = new string[itens.Count + 1];
            nomes[0] = "Nenhum";
            for (int i = 0; i < itens.Count; i++)
                nomes[i+1] = itens[i].NomeItem;
            return nomes;
        }

        // revisto
        private string[] nomes_zonas()
        {
            string[] nomes = new string[zonas.Count + 1];
            nomes[0] = "Todas";
            for (int i = 0; i < zonas.Count; i++)
                nomes[i+1] = zonas[i].Nome;
            return nomes;
        }

        // revisto
        private int numero_item(long cod_item)
        {
            if (cod_item == -1) return 0;
            int i;
            bool found = false;
            for (i = 0; i < itens.Count && !found; i++)
                if (cod_item == itens[i].CodigoItem)
                    found = true;
            return i;
        }

        // revisto
        private int numero_zona(long cod_zona)
        {
            if (cod_zona == -1) return 0;
            int i;
            bool found = false;
            for (i = 0; i < zonas.Count && !found; i++)
                if (cod_zona == zonas[i].Codigo)
                    found = true;
            return i;
        }

        // revisto
        private void init_perg_q()
        {
            if (!already_created)
            {
                questionario = new List<PerguntaQuestionario>();
                #region pergunta 6
                PerguntaQuestionario p = new PerguntaQuestionario(
                    codAnalise,
                    5,
                    1,
                    -1,
                    "É cliente habitual deste estabelecimento?",
                    6,
                    "qc");
                questionario.Add(p);
                #endregion
                #region pergunta 5
                p = new PerguntaQuestionario(
                    codAnalise,
                    4,
                    1,
                    -1,
                    "Qual a importância que dá às considerações ergonómicas na concepção de espaços de trabalho?",
                    3,
                    "ql");
                questionario.Add(p);
                #endregion
                #region pergunta 4
                p = new PerguntaQuestionario(
                    codAnalise,
                    3,
                    1,
                    -1,
                    "Habilitações Literárias",
                    1,
                    "qc");
                questionario.Add(p);
                #endregion
                #region pergunta 3
                p = new PerguntaQuestionario(
                    codAnalise,
                    2,
                    1,
                    -1,
                    "Profissão",
                    10,
                    "qc");
                questionario.Add(p);
                #endregion
                #region pergunta 2
                p = new PerguntaQuestionario(
                    codAnalise,
                    1,
                    1,
                    -1,
                    "Género",
                    8,
                    "qc");
                questionario.Add(p);
                #endregion
                #region pergunta 1
                p = new PerguntaQuestionario(
                    codAnalise,
                    0,
                    1,
                    -1,
                    "Idade:",
                    9,
                    "qc");
                questionario.Add(p);
                #endregion
            }
            else
            {
                questionario = GestaodeRespostas.getPerguntasQT(codAnalise);
            }
            foreach (PerguntaQuestionario p in questionario)
                p.ToString();
            toolStripStatusLabel2.Text = questionario.Count.ToString();
        }

        // revisto
        private void init()
        {
            panel.Controls.Clear();
            perguntas = new List<TextBox>();
            tipos_resposta = new List<Label>();
            itens_pergunta = new List<ComboBox>();
            zonas_pergunta = new List<ComboBox>();
            tipos_pergunta = new List<ComboBox>();

            show_add();
            foreach(PerguntaQuestionario q in questionario)
                show_pergutna(q,false);
        }

        // revisto
        private void show_pergutna(PerguntaQuestionario perg, bool nova_p)
        {
            Panel p = new System.Windows.Forms.Panel();
            p.Name = perg.Num_Pergunta.ToString();
            p.TabIndex = (int)perg.Num_Pergunta;
            p.AutoSize = true;
            p.BorderStyle = BorderStyle.FixedSingle;
            p.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            p.Dock = DockStyle.Top;
            panel.Controls.Add(p);

            Label l1 = new System.Windows.Forms.Label();
            l1.Width = 50;
            l1.Text = "Perg. " + perg.Num_Pergunta;
            l1.Location = new System.Drawing.Point(10, 10);
            p.Controls.Add(l1);

            TextBox t1 = new System.Windows.Forms.TextBox();
            t1.Width = p.Width - 85;
            t1.Text = perg.Texto;
            t1.Name = perg.Num_Pergunta.ToString();
            t1.Location = new System.Drawing.Point(65, 10);
            t1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            t1.KeyPress += new KeyPressEventHandler(KeyPressActionPerformed);
            t1.Click += new EventHandler(MouseClickActionPerformed);
            p.Controls.Add(t1);
            if (nova_p)
                perguntas.Insert(0, t1);
            else
                perguntas.Add(t1);

            Label l2 = new System.Windows.Forms.Label();
            l2.Width = 50;
            l2.Text = "Tipo:";
            l2.Location = new System.Drawing.Point(10, 40);
            p.Controls.Add(l2);

            ComboBox c1 = new System.Windows.Forms.ComboBox();
            c1.DropDownStyle = ComboBoxStyle.DropDownList;
            c1.Name = perg.Num_Pergunta.ToString();
            c1.Width = 200;
            c1.Items.AddRange(new string[]{"qc","qe","ql"});
            c1.SelectedItem = perg.TipoQuestao;
            c1.Location = new System.Drawing.Point(65, 40);
            c1.SelectedIndexChanged += new EventHandler(MouseClickActionPerformed);
            p.Controls.Add(c1);
            if (nova_p)
                tipos_pergunta.Insert(0, c1);
            else
                tipos_pergunta.Add(c1);

            Label l3 = new System.Windows.Forms.Label();
            l3.Width = 50;
            l3.Text = "Item: ";
            l3.Location = new System.Drawing.Point(10, 70);
            p.Controls.Add(l3);

            ComboBox c2 = new System.Windows.Forms.ComboBox();
            c2.Width = 200;
            c2.DropDownStyle = ComboBoxStyle.DropDownList;
            c2.Name = perg.Num_Pergunta.ToString();
            c2.Items.AddRange(nomes_itens());
            c2.SelectedIndex = numero_item(perg.Cod_Item);
            c2.Location = new System.Drawing.Point(65, 70);
            c2.SelectedIndexChanged += new EventHandler(MouseClickActionPerformed);
            p.Controls.Add(c2);
            if (nova_p)
                itens_pergunta.Insert(0, c2);
            else
                itens_pergunta.Add(c2);

            Label l4 = new System.Windows.Forms.Label();
            l4.Width = 50;
            l4.Text = "Zona: ";
            l4.Location = new System.Drawing.Point(10, 100);
            p.Controls.Add(l4);

            ComboBox c3 = new System.Windows.Forms.ComboBox();
            c3.Width = 200;
            c3.DropDownStyle = ComboBoxStyle.DropDownList;
            c3.Name = perg.Num_Pergunta.ToString();
            c3.Items.AddRange(nomes_zonas());
            c3.SelectedIndex = numero_zona(perg.Cod_zona);
            c3.Location = new System.Drawing.Point(65, 100);
            c3.SelectedIndexChanged += new EventHandler(MouseClickActionPerformed);
            p.Controls.Add(c3);
            if (nova_p)
                zonas_pergunta.Insert(0,c3);
            else
                zonas_pergunta.Add(c3);

            Label l5 = new System.Windows.Forms.Label();
            l5.Width = 80;
            l5.Text = "Respostas: ";
            l5.Location = new System.Drawing.Point(10, 140);
            p.Controls.Add(l5);

            Label l6 = new System.Windows.Forms.Label();
            l6.AutoSize = true;
            l6.Text = "Mudar Tipo Resposta";
            l6.Name = perg.Num_Pergunta.ToString();
            l6.Location = new System.Drawing.Point(95, 140);
            l6.Cursor = System.Windows.Forms.Cursors.Hand;
            l6.Click += new System.EventHandler(mudarTipoRespostaClick);
            l6.MouseEnter += new System.EventHandler(this.MouseEnterAction);
            l6.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
            p.Controls.Add(l6);
            tipos_resposta.Add(l6);

            Panel p2 = getRespostasPanel(GestaodeRespostas.getTipoEscala(perg.Cod_TipoEscala));
            p.Controls.Add(p2);

            Label l7 = new System.Windows.Forms.Label();
            l7.AutoSize = true;
            l7.Text = "Eliminar Pergunta";
            l7.Name = perg.Num_Pergunta.ToString() + ".eliminar";
            l7.Location = new System.Drawing.Point(10, 170+p2.Height);
            l7.Cursor = System.Windows.Forms.Cursors.Hand;
            l7.Click += new System.EventHandler(EliminarPerguntaActionPerformed);
            l7.MouseEnter += new System.EventHandler(this.MouseEnterAction);
            l7.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
            p.Controls.Add(l7);
        }

        private Panel getRespostasPanel(TipoEscala ti)
        {
            Panel p2 = new Panel();
            p2.Size = new Size(0, 0);
            p2.AutoSize = true;
            p2.Location = new System.Drawing.Point(0, 160);

            if (ti != null)
            {
                int y = 10;
                p2.Name = "Respostas";
                if (ti.Numero >= -1 && ti.Numero <= 1)
                {
                    TextBox t2 = new System.Windows.Forms.TextBox();
                    t2.Name = "t_box";
                    t2.Location = new System.Drawing.Point(10, 10);
                    t2.Enabled = false;
                    p2.Controls.Add(t2);
                }
                else if (ti.Numero == -2)
                {
                    foreach (EscalaResposta er in ti.Respostas)
                    {
                        CheckBox c = new System.Windows.Forms.CheckBox();
                        c.Text = er.Descricao;
                        c.Enabled = false;
                        c.Location = new System.Drawing.Point(10, y);
                        p2.Controls.Add(c);
                        y += 30;
                    }
                }
                else if (ti.Numero > 1)
                {
                    foreach (EscalaResposta er in ti.Respostas)
                    {
                        RadioButton r = new System.Windows.Forms.RadioButton();
                        r.Text = er.Descricao;
                        r.Enabled = false;
                        r.Location = new System.Drawing.Point(10, y);
                        p2.Controls.Add(r);
                        y += 30;
                    }
                }
            }
            return p2;
        }

        // revisto
        private void show_add()
        {
            Panel p = new System.Windows.Forms.Panel();
            p.Size = new Size(0, 0);
            p.Name = "Add";
            p.AutoSize = true;
            p.BorderStyle = BorderStyle.FixedSingle;
            p.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            p.Dock = DockStyle.Top;
            //p.TabIndex = questionario.Count;
            panel.Controls.Add(p);

            Label l = new System.Windows.Forms.Label();
            l.AutoSize = true;
            l.Text = "Adicionar Pergunta";
            l.Location = new System.Drawing.Point(10, 10);
            l.Cursor = System.Windows.Forms.Cursors.Hand;
            l.Click += new System.EventHandler(adcionarPerguntaClick);
            l.MouseEnter += new System.EventHandler(this.MouseEnterAction);
            l.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
            p.Controls.Add(l);
        }

        #endregion

        #region Eventos de Gestao

        // revisto
        private void EliminarPerguntaActionPerformed(object sender, EventArgs e)
        {
            Label l = (Label) sender;
            int number = int.Parse(l.Name.Split('.')[0]);
            update_num_pergunta(number);

            questionario.RemoveAt(questionario.Count-number-1);
            init();
        }

        // revisto
        private void update_num_pergunta(int num_eliminado)
        {
            for (int i = questionario.Count - 2 - num_eliminado; i >= 0; i--)
                questionario[i].Num_Pergunta--;
        }

        // revisto
        private void adcionarPerguntaClick(object sender, EventArgs e)
        {
            PerguntaQuestionario p = new PerguntaQuestionario(
                codAnalise,
                questionario.Count,
                -1,
                -1,
                "",
                -3,
                "qe");
            questionario.Insert(0,p);

            show_pergutna(p,true);
            panel.Controls.SetChildIndex(panel.Controls[panel.Controls.Count - 1], 1);
        }

        #endregion

        // revisto
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

        #region Eventos

        // revisto
        private void MouseEnterAction(object sender, EventArgs e)
        {
            Label t = (Label)sender;
            t.Font = new Font(t.Font, FontStyle.Underline);
            t.BackColor = Color.LightGray;
        }

        // revisto
        private void MouseLeaveAction(object sender, EventArgs e)
        {
            Label t = (Label)sender;
            t.Font = new Font(t.Font, FontStyle.Regular);
            t.BackColor = Color.Empty;
        }

        #endregion

        // revisto
        private void mudarTipoRespostaClick(object sender, EventArgs e)
        {
            Label l = (Label)sender;
            Interface_GestaoRespostas.main(int.Parse(l.Name),false);

            if (erros.Keys.Contains(sender))
            {
                ErrorProvider err = (ErrorProvider)erros[sender];
                err.Clear();
                erros.Remove(sender);
                setErroStatusBar();
            }
        }
        
        #region Tipos de Resposta Mudada
        
        // revisto
        public static void reenc_New_Anser(object sender, EventArgs e)
        {
            ip.new_Anser(sender,e);
        }
        private void new_Anser(object sender, EventArgs e)
        {
            List<object> lst = (List<object>)sender;

            int index_pergunta = questionario.Count - 1 - (int)lst[0];
            long cod_tipoResposta = (long)lst[1];

            questionario[index_pergunta].Cod_TipoEscala = cod_tipoResposta;
            Panel perg = (Panel)panel.Controls[index_pergunta + 1];
            perg.Controls.RemoveByKey("Respostas");
            Panel novo = getRespostasPanel(GestaodeRespostas.getTipoEscala(cod_tipoResposta));
            perg.Controls.Add(novo);
            Label l = (Label)perg.Controls[perg.Controls.IndexOfKey(lst[0].ToString() + ".eliminar")];
            l.Location = new Point(10,170+novo.Height);
        }
        
        #endregion

        // revisto
        private void CancelarActionPerformed(object sender, EventArgs e)
        {
            end_Frame();
        }

        // revisto
        private void end_Frame()
        {
            Dispose();
            Close();
        }

        private void Done_ActionPerformed(object sender, EventArgs e)
        {
            if (verifica_Erros())
            {
                for (int i = 0; i < questionario.Count; i++)
                {
                    int item_index = itens_pergunta[i].SelectedIndex;
                    int zona_index = zonas_pergunta[i].SelectedIndex;

                    questionario[i].Texto = perguntas[i].Text;
                    questionario[i].Cod_Item = (item_index == 0) ? -1 : itens[item_index-1].CodigoItem;
                    questionario[i].Cod_zona = (zona_index == 0) ? -1 : zonas[zona_index-1].Codigo;
                    questionario[i].TipoQuestao = tipos_pergunta[i].SelectedItem.ToString();
                }

                if (!already_created)
                    GestaodeRespostas.insert_PerguntasQT(questionario);
                else
                    GestaodeRespostas.modificaPerguntasQT(questionario, codAnalise);

                evento_QT_Done(null, new EventArgs());
                end_Frame();
            }
        }

        #region Erros
        // revisto
        private bool verifica_Erros()
        {

            bool ok = true;
            for (int i = 0; i < perguntas.Count ; i++)
                if (!pergunta_valida(perguntas[i].Text))
                {
                    if (!erros.ContainsKey(perguntas[i]))
                    {
                        ErrorProvider err = new ErrorProvider();
                        err.Icon = global::ETdA.Properties.Resources.notification_warning_ico;
                        err.SetError(perguntas[i], "O texto desta pergunta não é válido.");

                        erros.Add(perguntas[i], err);
                    }
                    ok = false;
                }

            for (int i = 0; i < tipos_resposta.Count ; i++)
                if (!tipos_resposta[i].Parent.Controls.ContainsKey("Respostas"))
                {
                    if (!erros.ContainsKey(tipos_resposta[i]))
                    {
                        ErrorProvider err = new ErrorProvider();
                        err.Icon = global::ETdA.Properties.Resources.notification_warning_ico;
                        err.SetError(tipos_resposta[i], "Tem de escolher as opções de resposta a esta pergunta.");

                        erros.Add(tipos_resposta[i], err);
                    }
                    ok = false;
                }

            for (int i = 0; i < tipos_pergunta.Count; i++)
                if (tipos_pergunta[i].SelectedIndex == 1 &&
                    itens_pergunta[i].SelectedIndex == 0)
                {
                    if (!erros.ContainsKey(itens_pergunta[i]))
                    {
                        ErrorProvider err = new ErrorProvider();
                        err.Icon = global::ETdA.Properties.Resources.notification_warning_ico;
                        err.SetError(itens_pergunta[i], "Questões do tipo qe tem de se referir a um item.");
                        erros.Add(itens_pergunta[i], err);
                    }
                    ok = false;
                }

            setErroStatusBar();
            return ok;
        }

        // revisto
        private bool pergunta_valida(string s)
        {
            if (s == "") return false;
            string possiveis = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVKWXYZ0123456789" +
                            "áàãâéèêíìîóòôõúùûçÁÀÂÃÉÈÊÍÌÎÓÒÕÔÚÙÛÇ ,.;:/()[]{}'?!_-|\\+ºª'";
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
                Label l = new Label();
                object p = erros.Keys.ElementAt(0);
                ErrorProvider err = (ErrorProvider)erros[p];
                if (p.GetType() == tb.GetType())
                {
                    tb = (TextBox)p;
                    toolStripStatusLabel5.Text = "Perg. " + tb.Name;
                    toolStripStatusLabel6.Text = err.GetError(tb);
                }
                else if (p.GetType() == l.GetType())
                {
                    l = (Label)p;
                    toolStripStatusLabel5.Text = "Perg. " + l.Name;
                    toolStripStatusLabel6.Text = err.GetError(l);
                }
                else
                {
                    ComboBox cb =(ComboBox)p;
                    toolStripStatusLabel5.Text = "Perg. " + cb.Name;
                    toolStripStatusLabel6.Text = err.GetError(cb);
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

        #region Limpa Erros
        // revisto
        private void KeyPressActionPerformed(object sender, KeyPressEventArgs e)
        {
            if (erros.Keys.Contains(sender))
            {
                ErrorProvider err = (ErrorProvider)erros[sender];
                err.Clear();
                erros.Remove(sender);
                setErroStatusBar();
            }
        }

        // revisto
        private void MouseClickActionPerformed(object sender, EventArgs e)
        {
            if (erros.Keys.Contains(sender))
            {
                ErrorProvider err = (ErrorProvider)erros[sender];
                err.Clear();
                erros.Remove(sender);
                setErroStatusBar();
            }
        }
        #endregion
    }
}
