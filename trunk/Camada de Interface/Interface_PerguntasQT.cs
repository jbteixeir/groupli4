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
        List<TextBox> perguntas;
        List<ComboBox> itens_pergunta;
        List<ComboBox> zonas_pergunta;
        List<ComboBox> tipos_pergunta;
        Dictionary<object, ErrorProvider> erros;
        private List<Item> itens;
        private List<Zona> zonas;
        private List<PerguntaQuestionario> questionario;

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
            return i - 1;
        }

        // revisto
        private int numero_zona(long cod_zona)
        {
            if (cod_zona == 1) return 0;
            int i;
            bool found = false;
            for (i = 0; i < zonas.Count && !found; i++)
                if (cod_zona == zonas[i].Codigo)
                    found = true;
            return i - 1;
        }

        // revisto
        private void init_perg_q()
        {
            questionario = new List<PerguntaQuestionario>();

            PerguntaQuestionario p = new PerguntaQuestionario(
                codAnalise,
                0,
                1,
                -1,
                "Idade:",
                9,
                "QC");
            questionario.Add(p);
            p = new PerguntaQuestionario(
                codAnalise,
                1,
                1,
                -1,
                "Género",
                8,
                "QC");
            questionario.Add(p);
            p = new PerguntaQuestionario(
                codAnalise,
                2,
                1,
                -1,
                "Profissão",
                10,
                "QC");
            questionario.Add(p);
            p = new PerguntaQuestionario(
                codAnalise,
                3,
                1,
                -1,
                "Habilitações Literárias",
                1,
                "QC");
            questionario.Add(p);
            p = new PerguntaQuestionario(
                codAnalise,
                4,
                1,
                -1,
                "Qual a importância que dá às considerações ergonómicas na concepção de espaços de trabalho?",
                3,
                "QL");
            questionario.Add(p);
            p = new PerguntaQuestionario(
                codAnalise,
                5,
                1,
                -1,
                "É cliente habitual deste estabelecimento?",
                6,
                "QC");
             questionario.Add(p);
        }

        // revisto
        private void init()
        {
            toolStripStatusLabel2.Text = questionario.Count.ToString();
            panel.Controls.Clear();
            perguntas = new List<TextBox>();
            itens_pergunta = new List<ComboBox>();
            zonas_pergunta = new List<ComboBox>();
            tipos_pergunta = new List<ComboBox>();

            int y = 10;
            foreach(PerguntaQuestionario q in questionario)
                y += show_pergutna(q, y);
            show_add(y);
        }

        // revisto
        private int show_pergutna(PerguntaQuestionario perg, int location_y)
        {
            Panel p = new System.Windows.Forms.Panel();
            p.Name = "" + perg.Num_Pergunta;
            p.Width = panel.Width - 14;
            p.BorderStyle = BorderStyle.FixedSingle;
            p.Location = new System.Drawing.Point(7, location_y);
            p.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

            Label l1 = new System.Windows.Forms.Label();
            l1.Width = 50;
            l1.Text = "Per. " + perg.Num_Pergunta;
            l1.Location = new System.Drawing.Point(10, 10);
            p.Controls.Add(l1);

            TextBox t1 = new System.Windows.Forms.TextBox();
            t1.Width = p.Width - 85;
            t1.Text = perg.Texto;
            t1.Name = "t_box";
            t1.Location = new System.Drawing.Point(65, 10);
            t1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            t1.KeyPress += new KeyPressEventHandler(KeyPressActionPerformed);
            t1.Click += new EventHandler(MouseClickActionPerformed);
            p.Controls.Add(t1);
            perguntas.Add(t1);

            Label l2 = new System.Windows.Forms.Label();
            l2.Width = 50;
            l2.Text = "Tipo:";
            l2.Location = new System.Drawing.Point(10, 40);
            p.Controls.Add(l2);

            ComboBox c1 = new System.Windows.Forms.ComboBox();
            c1.Width = 200;
            c1.Items.AddRange(new string[]{"QC","QE","QL"});
            c1.SelectedItem = perg.TipoQuestao;
            c1.Location = new System.Drawing.Point(65, 40);
            c1.SelectedIndexChanged += new EventHandler(MouseClickActionPerformed);
            p.Controls.Add(c1);
            tipos_pergunta.Add(c1);

            Label l3 = new System.Windows.Forms.Label();
            l3.Width = 50;
            l3.Text = "Item: ";
            l3.Location = new System.Drawing.Point(10, 70);
            p.Controls.Add(l3);

            ComboBox c2 = new System.Windows.Forms.ComboBox();
            c2.Width = 200;
            c2.Items.AddRange(nomes_itens());
            c2.SelectedIndex = numero_item(perg.Cod_Item);
            c2.Location = new System.Drawing.Point(65, 70);
            c2.SelectedIndexChanged += new EventHandler(MouseClickActionPerformed);
            p.Controls.Add(c2);
            itens_pergunta.Add(c2);

            Label l4 = new System.Windows.Forms.Label();
            l4.Width = 50;
            l4.Text = "Zona: ";
            l4.Location = new System.Drawing.Point(10, 100);
            p.Controls.Add(l4);

            ComboBox c3 = new System.Windows.Forms.ComboBox();
            c3.Width = 200;
            c3.Items.AddRange(nomes_zonas());
            c3.SelectedIndex = numero_zona(perg.Cod_zona);
            c3.Location = new System.Drawing.Point(65, 100);
            c3.SelectedIndexChanged += new EventHandler(MouseClickActionPerformed);
            p.Controls.Add(c3);
            zonas_pergunta.Add(c3);

            Panel p2 = new System.Windows.Forms.Panel();
            p2.Name = "Respostas";
            p2.Width = 320;
            p2.Location = new System.Drawing.Point(0, 130);
            p.Controls.Add(p2);

            Label l5 = new System.Windows.Forms.Label();
            l5.Width = 80;
            l5.Text = "Respostas: ";
            l5.Location = new System.Drawing.Point(10, 10);
            p2.Controls.Add(l5);

            Label l6 = new System.Windows.Forms.Label();
            l6.Width = 130;
            l6.Text = "Mudar Tipo Resposta";
            l6.Name = perg.Num_Pergunta.ToString();
            l6.Location = new System.Drawing.Point(95, 10);
            l6.Cursor = System.Windows.Forms.Cursors.Hand;
            l6.Click += new System.EventHandler(mudarTipoRespostaClick);
            l6.MouseEnter += new System.EventHandler(this.MouseEnterAction);
            l6.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
            p2.Controls.Add(l6);

            int y = 40;
            TipoEscala ti = null;
            if (perg.Cod_TipoEscala != -3)
            {
                ti = GestaodeRespostas.getTipoEscala(perg.Cod_TipoEscala);

                if (ti.Numero >= -1 && ti.Numero <= 1)
                {
                    TextBox t2 = new System.Windows.Forms.TextBox();
                    t2.Width = 100;
                    t2.Name = "t_box";
                    t2.Enabled = false;
                    t2.Location = new System.Drawing.Point(10, 40);
                    p2.Controls.Add(t2);
                    y += 30;
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
            p2.Height = y;

            Label l7 = new System.Windows.Forms.Label();
            l7.Width = 120;
            l7.Text = "Eliminar Pergunta";
            l7.Name = perg.Num_Pergunta.ToString();
            l7.Location = new System.Drawing.Point(10, 130+y);
            l7.Cursor = System.Windows.Forms.Cursors.Hand;
            l7.Click += new System.EventHandler(EliminarPerguntaActionPerformed);
            l7.MouseEnter += new System.EventHandler(this.MouseEnterAction);
            l7.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
            p.Controls.Add(l7);

            p.Height = 160+p2.Height;
            panel.Controls.Add(p);

            return p.Height;
        }

        // revisto
        private void EliminarPerguntaActionPerformed(object sender, EventArgs e)
        {
            Label l = (Label) sender;
            update_num_pergunta(int.Parse(l.Name));

            questionario.RemoveAt(int.Parse(l.Name));
            init();
        }

        // revisto
        private void update_num_pergunta(int num_eliminado)
        {
            for (int i = num_eliminado + 1; i < questionario.Count; i++)
                questionario[i].Num_Pergunta--; 
        }

        // revisto
        private void show_add(int location_y)
        {
            Panel p = new System.Windows.Forms.Panel();
            p.Name = "Add";
            p.Width = panel.Width - 14;
            p.Height = 40;
            p.BorderStyle = BorderStyle.FixedSingle;
            p.Location = new System.Drawing.Point(7, location_y);
            p.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

            Label l = new System.Windows.Forms.Label();
            l.Text = "Adicionar Pergunta";
            l.Location = new System.Drawing.Point(10, 10);
            l.Cursor = System.Windows.Forms.Cursors.Hand;
            l.Click += new System.EventHandler(adcionarPerguntaClick);
            l.MouseEnter += new System.EventHandler(this.MouseEnterAction);
            l.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
            p.Controls.Add(l);

            panel.Controls.Add(p);
        }

        // revisto
        private void adcionarPerguntaClick(object sender, EventArgs e)
        {
            PerguntaQuestionario p = new PerguntaQuestionario(
                codAnalise,
                questionario.Count,
                1,
                -1,
                "",
                -3,
                "QE");
            questionario.Add(p);

            init();
        }

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

        // revisto
        private void mudarTipoRespostaClick(object sender, EventArgs e)
        {
            Label l = (Label)sender;
            Interface_GestaoRespostas.main(int.Parse(l.Name),false);
        }

        // revisto
        public Interface_PerguntasQT(long codAnalise, object itens, object zonas)
        {
            InitializeComponent();
            this.codAnalise = codAnalise;
            this.itens = (List<Item>)itens;
            this.zonas = (List<Zona>)zonas;

            erros = new Dictionary<object, ErrorProvider>();
            init_perg_q();

            init();
        }

        // revisto
        public static void main(long codAnalise, object itens, object zonas)
        {
            ip = new Interface_PerguntasQT(codAnalise, itens, zonas);
            ip.Visible = true;
        }

        // revisto
        public static void reenc_New_Anser(object sender, EventArgs e)
        {
            ip.new_Anser(sender,e);
        }
        private void new_Anser(object sender, EventArgs e)
        {
            List<object> lst = (List<object>)sender;
            int num_pergunta = (int)lst[0];
            long cod_tipoResposta = (long)lst[1];

            questionario[num_pergunta].Cod_TipoEscala = cod_tipoResposta;
            init();
        }

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
            if (!verifica_Erros())
            {
                MessageBox.Show("É necessário corrigir os erros.");
            }
            else
            {
                for (int i = 0; i < questionario.Count; i++)
                {
                    questionario[i].Texto = perguntas[i].Text;
                    questionario[i].Cod_Item = itens[itens_pergunta[i].SelectedIndex].CodigoItem;
                    questionario[i].Cod_zona = zonas[zonas_pergunta[i].SelectedIndex].Codigo;
                    questionario[i].TipoQuestao = tipos_pergunta[i].SelectedItem.ToString();
                }

                GestaodeRespostas.insert_PerguntasQT(questionario);
                end_Frame();
            }
        }

        // revisto
        private bool verifica_Erros()
        {

            bool ok = true;
            for (int i = 0; i < perguntas.Count ; i++)
                if (!pergunta_valida(perguntas[i].Text))
                {
                    ErrorProvider err = new ErrorProvider();
                    err.Icon = global::ETdA.Properties.Resources.notification_warning_ico;
                    err.SetError(perguntas[i], "O texto desta pergunta não é válido.");
                    if (!erros.ContainsKey(perguntas[i]))
                        erros.Add(perguntas[i], err);
                    ok = false;
                }

            for (int i = 0; i < itens_pergunta.Count ; i++)
                if (itens_pergunta[i].SelectedIndex < 0 ||
                    itens_pergunta[i].SelectedIndex >= itens_pergunta[i].Items.Count)
                {
                    ErrorProvider err = new ErrorProvider();
                    err.Icon = global::ETdA.Properties.Resources.notification_warning_ico;
                    err.SetError(itens_pergunta[i], "É necessário escolher uma opção.");
                    if (!erros.ContainsKey(itens_pergunta[i]))
                        erros.Add(itens_pergunta[i], err);
                    ok = false;
                }

            for (int i = 0; i < zonas_pergunta.Count; i++)
                if (zonas_pergunta[i].SelectedIndex < 0 ||
                    zonas_pergunta[i].SelectedIndex >= zonas_pergunta[i].Items.Count)
                {
                    ErrorProvider err = new ErrorProvider();
                    err.Icon = global::ETdA.Properties.Resources.notification_warning_ico;
                    err.SetError(zonas_pergunta[i], "É necessário escolher uma opção.");
                    if (!erros.ContainsKey(zonas_pergunta[i]))
                        erros.Add(zonas_pergunta[i], err);
                    ok = false;
                }

            for (int i = 0; i < tipos_pergunta.Count; i++)
                if (tipos_pergunta[i].SelectedIndex < 0 ||
                    tipos_pergunta[i].SelectedIndex >= tipos_pergunta[i].Items.Count)
                {
                    ErrorProvider err = new ErrorProvider();
                    err.Icon = global::ETdA.Properties.Resources.notification_warning_ico;
                    err.SetError(tipos_pergunta[i], "É necessário escolher uma opção.");
                    if (!erros.ContainsKey(tipos_pergunta[i]))
                        erros.Add(tipos_pergunta[i], err);
                    ok = false;
                }

            return ok;
        }

        // revisto
        private bool pergunta_valida(string s)
        {
            if (s == "") return false;
            string possiveis = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVKWXYZ0123456789" + 
                "áàãâéèêíìîóòôõúùûçÁÀÂÃÉÈÊÍÌÎÓÒÕÔÚÙÛ \t,.;:/()[]{}'?!";
            bool found = true;
            for (int i = 0; i < s.Length && found; i++)
                found = possiveis.Contains(s[i]);
            return found;
        }

        // revisto
        private void KeyPressActionPerformed(object sender, KeyPressEventArgs e)
        {
            if (erros.Keys.Contains(sender))
            {
                ErrorProvider err = (ErrorProvider)erros[sender];
                err.Clear();
                erros.Remove(sender);
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
            }
        }
    }
}
