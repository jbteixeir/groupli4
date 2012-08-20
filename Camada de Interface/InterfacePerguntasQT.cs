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
    public partial class InterfacePerguntasQT : Form
    {
        private static InterfacePerguntasQT ip;
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
        private Dictionary<int, List<PerguntaQuestionario>> p_classificacao;
        private bool already_created;

        private bool enabled;

        private delegate void eventoEventHandler(object sender, EventArgs e);
        private static event eventoEventHandler evento_QT_Done;

        public InterfacePerguntasQT(long codAnalise, object itens, object zonas, bool created, bool enabled)
        {
            InitializeComponent();
            toolStripStatusLabel4.Visible = false;
            toolStripStatusLabel5.Visible = false;
            toolStripStatusLabel6.Visible = false;

            already_created = created;
            this.codAnalise = codAnalise;

            this.enabled = enabled;
            if (!enabled)
                button2.Visible = false;

            this.itens = (List<Item>)itens;
            this.zonas = (List<Zona>)zonas;
            evento_QT_Done += new eventoEventHandler(Camada_de_Interface.InterfaceGestaoFormulariosOnline.done_QT_Reenc);

            erros = new Dictionary<object, ErrorProvider>();
            init_perg_q();

            init();
        }

        public static void main(long codAnalise, object itens, object zonas, bool created, bool enabled)
        {
            ip = new InterfacePerguntasQT(codAnalise, itens, zonas, created, enabled);
            ip.ShowDialog();
        }

        #region Começo

        private string[] nomes_itens()
        {
            string[] nomes = new string[itens.Count + 1];
            nomes[0] = "Nenhum";
            for (int i = 0; i < itens.Count; i++)
                nomes[i+1] = itens[i].NomeItem;
            return nomes;
        }

        private string[] nomes_zonas()
        {
            string[] nomes = new string[zonas.Count + 2];
            nomes[0] = "";
            nomes[1] = "Todas";
            for (int i = 0; i < zonas.Count; i++)
                nomes[i+2] = zonas[i].Nome;
            return nomes;
        }

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

        private int numero_zona(long cod_zona)
        {
            if (cod_zona == 1) return 1;
            if (cod_zona == 0) return 0;
            int i;
            bool found = false;
            for (i = 0; i < zonas.Count && !found; i++)
                if (cod_zona == zonas[i].Codigo)
                    found = true;
            return i+1;
        }

        private void init_perg_q()
        {
            if (!already_created)
            {
                questionario = new List<PerguntaQuestionario>();
                p_classificacao = new Dictionary<int, List<PerguntaQuestionario>>();
                #region pergunta 1
                PerguntaQuestionario p = new PerguntaQuestionario(
                    codAnalise,
                    1,
                    1,
                    -1,
                    "Idade:",
                    9,
                    "qc");
                questionario.Add(p);
                #endregion
                #region pergunta 2
                p = new PerguntaQuestionario(
                    codAnalise,
                    2,
                    1,
                    -1,
                    "Género",
                    8,
                    "qc");
                questionario.Add(p);
                #endregion
                #region pergunta 3
                p = new PerguntaQuestionario(
                    codAnalise,
                    3,
                    1,
                    -1,
                    "Profissão",
                    10,
                    "qc");
                questionario.Add(p);
                #endregion
                #region pergunta 4
                p = new PerguntaQuestionario(
                    codAnalise,
                    4,
                    1,
                    -1,
                    "Habilitações Literárias",
                    11,
                    "qc");
                questionario.Add(p);
                #endregion
                #region pergunta 5
                p = new PerguntaQuestionario(
                    codAnalise,
                    5,
                    1,
                    -1,
                    "Qual a importância que dá às considerações ergonómicas na concepção de espaços de trabalho?",
                    12,
                    "ql");
                questionario.Add(p);
                #endregion
                #region pergunta 6
                p = new PerguntaQuestionario(
                    codAnalise,
                    6,
                    1,
                    -1,
                    "É cliente habitual deste estabelecimento?",
                    6,
                    "qc");
                questionario.Add(p);
                #endregion
            }
            else
            {
                questionario = GestaodeRespostas.getPerguntasQT(codAnalise);
                cria_p_classificacao();
            }
            toolStripStatusLabel2.Text = questionario.Count.ToString();
        }

        private void cria_p_classificacao()
        {
            p_classificacao = new Dictionary<int, List<PerguntaQuestionario>>();
            List<PerguntaQuestionario> novo = new List<PerguntaQuestionario>();
            foreach (PerguntaQuestionario p in questionario)
            {
                if ((int)p.Num_Pergunta - p.Num_Pergunta != 0)
                {
                    if (p_classificacao.ContainsKey((int)p.Num_Pergunta))
                        p_classificacao[(int)p.Num_Pergunta].Add(p.Clone());
                    else
                    {
                        List<PerguntaQuestionario> lst = new List<PerguntaQuestionario>();
                        lst.Add(p.Clone());
                        p_classificacao.Add((int)p.Num_Pergunta, lst);
                    }
                }
                else
                    novo.Add(p.Clone());
            }
            questionario = novo;
        }

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
                show_pergunta(q);
        }

        private Panel pergunta_barra_titulo(float number)
        {
            Panel p = new Panel();
            p.Size = new Size(0, 0);
            p.AutoSize = true;
            p.Dock = DockStyle.Top;

            Label l1 = new System.Windows.Forms.Label();
            l1.AutoSize = true;
            l1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            l1.Text = "Pergunta " + number;
            l1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            l1.Location = new Point(10, 0);
            p.Controls.Add(l1);

            Panel p2 = new Panel();
            p2.Size = new Size(27, 27);
            p2.Dock = DockStyle.Right;
            p.Controls.Add(p2);

            PictureBox pb = new PictureBox();
            pb.Name = number.ToString() + ".eliminar";
            pb.Size = new Size(17, 17);
            pb.BackgroundImage = global::ETdA.Properties.Resources._4697;
            pb.BackgroundImageLayout = ImageLayout.Center;
            pb.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            pb.Location = new Point(5,5);
            pb.MouseEnter += new EventHandler(PictureMouseEnterAction);
            pb.MouseLeave += new EventHandler(PictureMouseLeaveAction);
            pb.MouseClick +=new MouseEventHandler(EliminarPerguntaActionPerformed);
            if (!enabled)
                pb.Visible = false;
            p2.Controls.Add(pb);

            return p;
        }

        private void show_pergunta(PerguntaQuestionario perg)
        {
            Panel p = new System.Windows.Forms.Panel();
            p.Name = perg.Num_Pergunta.ToString();
            p.AutoSize = true;
            p.BorderStyle = BorderStyle.FixedSingle;
            p.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            p.Dock = DockStyle.Top;
            panel.Controls.Add(p);
            panel.Controls.SetChildIndex(p, 1);

            Panel barra = pergunta_barra_titulo(perg.Num_Pergunta);
            p.Controls.Add(barra);

            TextBox t1 = new System.Windows.Forms.TextBox();
            t1.Width = p.Width - 30;
            t1.Text = perg.Texto;
            t1.Location = new System.Drawing.Point(10, 40);
            t1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            t1.KeyPress += new KeyPressEventHandler(KeyPressActionPerformed);
            t1.Click += new EventHandler(MouseClickActionPerformed);
            if (!enabled)
                t1.Enabled = false;
            p.Controls.Add(t1);
            perguntas.Add(t1);

            Label l2 = new System.Windows.Forms.Label();
            l2.Width = 50;
            l2.Text = "Tipo:";
            l2.Location = new System.Drawing.Point(10, 70);
            p.Controls.Add(l2);

            ComboBox c1 = new System.Windows.Forms.ComboBox();
            c1.DropDownStyle = ComboBoxStyle.DropDownList;
            c1.Width = 200;
            c1.Items.AddRange(new string[]{"qc","qe","ql"});
            c1.SelectedItem = perg.TipoQuestao;
            c1.Location = new System.Drawing.Point(65, 70);
            c1.SelectedIndexChanged += new EventHandler(MouseClickActionPerformed);
            if (!enabled)
                c1.Enabled = false;
            p.Controls.Add(c1);
            tipos_pergunta.Add(c1);

            Label l3 = new System.Windows.Forms.Label();
            l3.Width = 50;
            l3.Text = "Item: ";
            l3.Location = new System.Drawing.Point(10, 100);
            p.Controls.Add(l3);

            ComboBox c2 = new System.Windows.Forms.ComboBox();
            c2.Width = 200;
            c2.DropDownStyle = ComboBoxStyle.DropDownList;
            c2.Items.AddRange(nomes_itens());
            c2.SelectedIndex = numero_item(perg.Cod_Item);
            c2.Location = new System.Drawing.Point(65, 100);
            c2.SelectedIndexChanged += new EventHandler(MouseClickActionPerformed);
            if (!enabled)
                c2.Enabled = false;
            p.Controls.Add(c2);
            itens_pergunta.Add(c2);

            Label l4 = new System.Windows.Forms.Label();
            l4.Width = 50;
            l4.Text = "Zona: ";
            l4.Location = new System.Drawing.Point(10, 130);
            p.Controls.Add(l4);

            ComboBox c3 = new System.Windows.Forms.ComboBox();
            c3.Width = 200;
            c3.DropDownStyle = ComboBoxStyle.DropDownList;
            c3.Items.AddRange(nomes_zonas());
            c3.SelectedIndex = numero_zona(perg.Cod_zona);
            c3.Location = new System.Drawing.Point(65, 130);
            c3.SelectedIndexChanged += new EventHandler(MouseClickActionPerformed);
            if (!enabled)
                c3.Enabled = false;
            p.Controls.Add(c3);
            zonas_pergunta.Add(c3);

            Label l5 = new System.Windows.Forms.Label();
            l5.Width = 80;
            l5.Text = "Respostas: ";
            l5.Location = new System.Drawing.Point(10, 170);
            p.Controls.Add(l5);

            Label l6 = new System.Windows.Forms.Label();
            l6.AutoSize = true;
            l6.Text = "Mudar Tipo Resposta";
            l6.Name = perg.Num_Pergunta.ToString();
            l6.Location = new System.Drawing.Point(95, 170);
            l6.Cursor = System.Windows.Forms.Cursors.Hand;
            l6.Click += new System.EventHandler(mudarTipoRespostaClick);
            l6.MouseEnter += new System.EventHandler(this.MouseEnterAction);
            l6.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
            if (!enabled)
                l6.Visible = false;
            p.Controls.Add(l6);
            tipos_resposta.Add(l6);

            Panel p2 = getRespostasPanel(GestaodeRespostas.getTipoEscala(perg.Cod_TipoEscala), (int)perg.Num_Pergunta);
            p.Controls.Add(p2);
        }

        private Panel getRespostasPanel(TipoEscala ti, int numero_pergunta)
        {
            Panel p = new Panel();
            p.Name = "Respostas";
            p.Height = 0;
            p.AutoSize = true;
            p.Location = new System.Drawing.Point(10, 200);

            if (ti != null)
            {
                if (ti.Numero == 0 || ti.Numero == 1)
                {
                    #region Box
                    TextBox t2 = new System.Windows.Forms.TextBox();
                    t2.Name = "t_box";
                    t2.Enabled = false;
                    t2.Location = new Point(0, 0);
                    p.Controls.Add(t2);
                    #endregion
                }
                else if (ti.Numero == -2)
                {
                    #region CheckBox
                    foreach (EscalaResposta er in ti.Respostas)
                    {
                        CheckBox c = new System.Windows.Forms.CheckBox();
                        c.Text = er.Descricao;
                        c.AutoSize = true;
                        c.Enabled = false;
                        c.Dock = DockStyle.Top;
                        p.Controls.Add(c);
                        p.Controls.SetChildIndex(c, 0);
                    }
                    #endregion
                }
                else if (ti.Numero > 1)
                {
                    #region RadioButton
                    foreach (EscalaResposta er in ti.Respostas)
                    {
                        RadioButton r = new System.Windows.Forms.RadioButton();
                        r.Text = er.Descricao;
                        r.AutoSize = true;
                        r.Enabled = false;
                        r.Dock = DockStyle.Top;
                        p.Controls.Add(r);
                        p.Controls.SetChildIndex(r, 0);
                    }
                    #endregion
                }
                else if (ti.Numero == -3)
                {
                    #region Classificação
                    #region Número Classificação
                    Panel p1 = new Panel();
                    p1.Height = 0;
                    p1.AutoSize = true;
                    p1.Dock = DockStyle.Top;
                    p.Controls.Add(p1);

                    foreach (EscalaResposta er in ti.Respostas)
                    {
                        RadioButton r = new System.Windows.Forms.RadioButton();
                        r.Text = er.Descricao;
                        r.AutoSize = true;
                        r.Enabled = false;
                        r.Dock = DockStyle.Top;
                        p1.Controls.Add(r);
                        p1.Controls.SetChildIndex(r, 0);
                    }
                    #endregion
                    #region Panel da Label
                    Panel p2 = new Panel();
                    p2.Height = 0;
                    p2.AutoSize = true;
                    p2.Dock = DockStyle.Top;
                    p.Controls.Add(p2);
                    p.Controls.SetChildIndex(p2, 0);

                    Label b = new Label();
                    b.Location = new Point(7,10);
                    b.Text = "Itens:";
                    b.Height = 30;
                    p2.Controls.Add(b);
                    #endregion
                    #region Panel para colocar os itens
                    Panel p3 = get_panel_classificacao(numero_pergunta);
                    p.Controls.Add(p3);
                    p.Controls.SetChildIndex(p3, 0);
                    #endregion
                    #endregion
                }
            }
            return p;
        }

        private Panel get_panel_classificacao(int numero_pergunta)
        {
            Panel p = new Panel();
            p.Height = 0;
            p.AutoSize = true;
            p.Dock = DockStyle.Top;

            #region Panel Adicionar
            Panel add = new Panel();
            add.Height = 0;
            add.AutoSize = true;
            add.Dock = DockStyle.Top;
            if (!enabled)
                add.Visible = false;
            p.Controls.Add(add);

            PictureBox pb = new PictureBox();
            pb.Name = numero_pergunta.ToString();
            pb.Size = new Size(20, 20);
            pb.BackgroundImage = global::ETdA.Properties.Resources.icon_add;
            pb.BackgroundImageLayout = ImageLayout.Center;
            pb.Location = new Point(0,0);
            pb.MouseEnter += new EventHandler(PictureMouseEnterAction);
            pb.MouseLeave += new EventHandler(PictureMouseLeaveAction);
            pb.MouseClick +=new MouseEventHandler(AdicionarSubPerguntaActionPerformed);
            add.Controls.Add(pb);
            #endregion

            List<PerguntaQuestionario> ps;
            if (p_classificacao.ContainsKey(numero_pergunta))
                ps = p_classificacao[numero_pergunta];
            else
                ps = new List<PerguntaQuestionario>();

            foreach (PerguntaQuestionario perg in ps)
                show_item_sub_pergunta(perg, p);

            return p;
        }

        private void show_item_sub_pergunta(PerguntaQuestionario perg, Panel p)
        {
            Panel panel = new Panel();
            panel.Height = 0;
            panel.AutoSize = true;
            panel.Dock = DockStyle.Top;
            p.Controls.Add(panel);
            p.Controls.SetChildIndex(panel, 1);

            TextBox b = new TextBox();
            b.Width = panel.Width - 40;
            b.Text = perg.Texto;
            b.Location = new Point(0, 0);
            b.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Top;
            if (!enabled)
                b.Enabled = false;
            panel.Controls.Add(b);

            Panel p2 = new Panel();
            p2.Name = "p_box";
            p2.Size = new Size(b.Height, b.Height);
            p2.Location = new Point(b.Width + 20,0);
            panel.Controls.Add(p2);

            PictureBox pb1 = new PictureBox();
            pb1.Name = perg.Num_Pergunta.ToString();
            pb1.Size = new Size(18,18);
            pb1.BackgroundImage = global::ETdA.Properties.Resources._116740_34218_16_delete_exit_remove_icon;
            pb1.BackgroundImageLayout = ImageLayout.Center;
            pb1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            pb1.Location = new Point((p2.Width - 18) / 2, (p2.Width - 18) / 2);
            pb1.MouseEnter += new EventHandler(PictureMouseEnterAction);
            pb1.MouseLeave += new EventHandler(PictureMouseLeaveAction);
            pb1.MouseClick += new MouseEventHandler(EliminarSubPerguntaActionPerformed);
            if (!enabled)
                pb1.Visible = false;
            p2.Controls.Add(pb1);
        }

        private void show_add()
        {
            Panel p = new System.Windows.Forms.Panel();
            p.Size = new Size(0, 0);
            p.Name = "Add";
            p.AutoSize = true;
            p.BorderStyle = BorderStyle.FixedSingle;
            p.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            p.Dock = DockStyle.Top;
            if (!enabled)
                p.Visible = false;
            panel.Controls.Add(p);
            panel.Controls.SetChildIndex(p, 0);

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

        private void EliminarPerguntaActionPerformed(object sender, EventArgs e)
        {
            PictureBox l = (PictureBox) sender;
            int number = int.Parse(l.Name.Split('.')[0]);

            panel.Controls.RemoveByKey(number.ToString());
            questionario.RemoveAt(number - 1);

            update_num_pergunta(number-1);

            perguntas.RemoveAt(number - 1);
            tipos_resposta.RemoveAt(number - 1);
            itens_pergunta.RemoveAt(number - 1);
            zonas_pergunta.RemoveAt(number - 1);
            tipos_pergunta.RemoveAt(number - 1);
            toolStripStatusLabel2.Text = questionario.Count.ToString();
        }

        private void update_num_pergunta(int num_eliminado)
        {
            for (int i = num_eliminado; i < questionario.Count ; i++)
                questionario[i].Num_Pergunta--;
            for (int i = 1; i < panel.Controls.Count - num_eliminado; i++ )
            {
                Panel perg = (Panel)panel.Controls[i];
                int num_novo = int.Parse(perg.Name) - 1;

                perg.Name = num_novo.ToString();
                Panel p1 = (Panel)perg.Controls[0];
                Label l1 = (Label)p1.Controls[0];
                l1.Text = "Pergunta " + num_novo.ToString();
                Panel p2 = (Panel)p1.Controls[1];
                PictureBox pb = (PictureBox)p2.Controls[0];
                pb.Name = num_novo.ToString() + ".eliminar";
                Label l2 = (Label)perg.Controls[9];
                l2.Name = num_novo.ToString();
            }

            /* actualizar as perguntas */
            if (p_classificacao.ContainsKey(num_eliminado+1))
                p_classificacao.Remove(num_eliminado + 1);
            Dictionary<int, List<PerguntaQuestionario>> novo = new Dictionary<int, List<PerguntaQuestionario>>();
            foreach (int i in p_classificacao.Keys)
                if (i > num_eliminado + 1)
                {
                    List<PerguntaQuestionario> lst = new List<PerguntaQuestionario>();
                    foreach (PerguntaQuestionario q in p_classificacao[i])
                    {
                        q.Num_Pergunta -= 1;
                        lst.Add(q.Clone());
                    }
                    novo.Add(i - 1, lst);

                    /* Actualizar as picturesBox */
                    Panel respostas = (Panel)panel.Controls[(num_eliminado + 1).ToString()].Controls["Respostas"];
                    Panel subs = (Panel)respostas.Controls[0];
                    foreach (Panel p in subs.Controls)
                        if (p.Controls.ContainsKey("p_box"))
                        {
                            PictureBox pb = (PictureBox)p.Controls["p_box"].Controls[0];
                            pb.Name = (float.Parse(pb.Name) - 1).ToString();
                        }
                }
            p_classificacao = novo;

        }

        private void adcionarPerguntaClick(object sender, EventArgs e)
        {
            PerguntaQuestionario p = new PerguntaQuestionario(
                codAnalise,
                questionario.Count + 1,
                1,
                -1,
                "",
                -4,
                "qe");
            questionario.Add(p);

            show_pergunta(p);
            toolStripStatusLabel2.Text = questionario.Count.ToString();
        }

        private void AdicionarSubPerguntaActionPerformed(object sender, EventArgs e)
        {
            int num_Parent = int.Parse(((PictureBox)sender).Name);
            float num_pergunta = (float)(num_Parent + 0.01 * ((PictureBox)sender).Parent.Parent.Controls.Count);

            PerguntaQuestionario parent = questionario[num_Parent - 1];
            PerguntaQuestionario p = new PerguntaQuestionario(
                codAnalise,
                num_pergunta,
                parent.Cod_zona,
                parent.Cod_Item,
                "",
                parent.Cod_TipoEscala,
                parent.TipoQuestao);

            if (p_classificacao.ContainsKey(num_Parent))
                p_classificacao[num_Parent].Add(p.Clone());
            else
            {
                List<PerguntaQuestionario> lst = new List<PerguntaQuestionario>();
                lst.Add(p.Clone());
                p_classificacao.Add(num_Parent, lst);
            }

            Panel subs = (Panel)((PictureBox)sender).Parent.Parent;

            show_item_sub_pergunta(p, subs);
        }

        private void EliminarSubPerguntaActionPerformed(object sender, EventArgs e)
        {
            float num_pergunta = float.Parse(((PictureBox)sender).Name);

            Panel subs = (Panel)((PictureBox)sender).Parent.Parent.Parent;
            subs.Controls.Remove((Panel)((PictureBox)sender).Parent.Parent);

            EliminarSubPergunta(num_pergunta);
            if (p_classificacao.ContainsKey((int)num_pergunta))
                update_sub_pergunta(num_pergunta, subs);
        }

        private void EliminarSubPergunta(float num_pergunta)
        {
            List<PerguntaQuestionario> p_subs = p_classificacao[(int)num_pergunta];

            bool found = false;
            for (int i = 0; i < p_subs.Count && !found; i++)
                if (p_subs[i].Num_Pergunta.Equals(num_pergunta))
                {
                    p_subs.RemoveAt(i);
                    MessageBox.Show("Apagou.\nCount: " + p_subs.Count);
                    found = true;
                }
            if (p_subs.Count == 0)
                p_classificacao.Remove((int)num_pergunta);
        }

        private void update_sub_pergunta(float num_pergunta_eliminado, Panel subs)
        {
            /* update do p_classificacao */
            List<PerguntaQuestionario> p_subs = p_classificacao[(int)num_pergunta_eliminado];

            foreach (PerguntaQuestionario q in p_subs)
                if (q.Num_Pergunta > num_pergunta_eliminado)
                    q.Num_Pergunta -= 0.01f;

            /* update dos numeros perguntas picturesBox */
            foreach (Panel p in subs.Controls)
                if (p.Controls.ContainsKey("p_box"))
                {
                    PictureBox pb = (PictureBox)p.Controls["p_box"].Controls[0];
                    if (float.Parse(pb.Name) > num_pergunta_eliminado)
                        pb.Name = (float.Parse(pb.Name) - 0.01).ToString();
                }
        }

        #endregion

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

        private void PictureMouseEnterAction(object sender, EventArgs e)
        {
            PictureBox t = (PictureBox)sender;
            t.BackColor = Color.LightBlue;
        }

        private void PictureMouseLeaveAction(object sender, EventArgs e)
        {
            PictureBox t = (PictureBox)sender;
            t.BackColor = Color.Empty;
        }
        #endregion

        private void mudarTipoRespostaClick(object sender, EventArgs e)
        {
            Label l = (Label)sender;

            if (p_classificacao.ContainsKey(int.Parse(l.Name)))
                p_classificacao.Remove(int.Parse(l.Name));

            InterfaceGestaoRespostas.main(int.Parse(l.Name),false);

            if (erros.Keys.Contains(sender))
            {
                ErrorProvider err = (ErrorProvider)erros[sender];
                err.Clear();
                erros.Remove(sender);
                setErroStatusBar();
            }
        }

        #region Tipos de Resposta Mudada

        public static void reenc_New_Anser(object sender, EventArgs e)
        {
            ip.new_Anser(sender,e);
        }
        private void new_Anser(object sender, EventArgs e)
        {
            List<object> lst = (List<object>)sender;

            int num_pergunta = (int)lst[0];
            long cod_tipoResposta = (long)lst[1];

            questionario[num_pergunta - 1].Cod_TipoEscala = cod_tipoResposta;
            Panel perg = (Panel)panel.Controls[panel.Controls.IndexOfKey(num_pergunta.ToString())];
            perg.Controls.RemoveByKey("Respostas");
            Panel novo = getRespostasPanel(GestaodeRespostas.getTipoEscala(cod_tipoResposta),num_pergunta);
            perg.Controls.Add(novo);
        }
        
        #endregion

        private void CancelarActionPerformed(object sender, EventArgs e)
        {
            end_Frame();
        }

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
                    questionario[i].Cod_zona = (zona_index <= 1) ? zona_index : zonas[zona_index-2].Codigo;
                    questionario[i].TipoQuestao = tipos_pergunta[i].SelectedItem.ToString();
                }

                foreach (int i in p_classificacao.Keys)
                {
                    int num_pergunta = i;
                    Panel respostas = (Panel)panel.Controls[num_pergunta.ToString()].Controls["Respostas"];
                    Panel subs = (Panel)respostas.Controls[0];
                    List<PerguntaQuestionario> pergs = p_classificacao[num_pergunta];
                    for (int j = 0; j < pergs.Count; j++)
                    {
                        pergs[j].Texto = ((TextBox)subs.Controls[pergs.Count - j].Controls[0]).Text;
                        questionario.Add(pergs[j]);
                    }
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
                if (tipos_resposta[i].Parent.Controls["Respostas"].Height == 0)
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
