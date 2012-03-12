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
    public partial class Interface_Perguntas : Form
    {
        private static Interface_Perguntas ip;
        private long codAnalise;
        private List<TextBox> perguntas;
        private List<ComboBox> itens_pergunta;
        private Dictionary<object, object> erros;
        private List<Item> itens;
        private List<PerguntaFichaAvaliacao> ficha_avaliacao;
        private bool already_created;
        private bool enabled;

        private delegate void eventoEventHandler(object sender, EventArgs e);
        private static event eventoEventHandler evento_FA_Done;

        public Interface_Perguntas(long codAnalise, object itens, bool created, bool enabled)
        {
            InitializeComponent();
            toolStripStatusLabel4.Visible = false;
            toolStripStatusLabel5.Visible = false;
            toolStripStatusLabel6.Visible = false;
            this.enabled = enabled;

            already_created = created;
            this.codAnalise = codAnalise;
            this.itens = (List<Item>)itens;
            evento_FA_Done += new eventoEventHandler(Camada_de_Interface.Interface_GestaoFormulariosOnline.done_FA_Reenc);

            erros = new Dictionary<object, object>();
            init_perg_fa();

            toolStripStatusLabel2.Text = ficha_avaliacao.Count.ToString();

            init();
        }

        public static void main(long codAnalise, object itens, bool created, bool enabled)
        {
            ip = new Interface_Perguntas(codAnalise, itens, created, enabled);
            ip.ShowDialog();
        }

        #region Começo
        /*
         * Devolve o nome dos itens
         */
        private string[] nomes_itens()
        {
            string[] nomes = new string[itens.Count];
            for (int i = 0; i < itens.Count; i++)
                nomes[i] = itens[i].NomeItem;
            return nomes;
        }

        /*
         * Retorna o indice do item recebido na lista
         */
        private int numero_item(Item item)
        {
            int i;
            bool found = false;
            for (i = 0; i < itens.Count && !found; i++)
                if (item.NomeItem == itens[i].NomeItem)
                    found = true;
            return i - 1;
        }

        /*
         * Devolve um item com o cod recebido
         */
        private Item item(long cod_item)
        {
            int i;
            bool found = false;
            for (i = 0; i < itens.Count && !found; i++)
                if (cod_item == itens[i].CodigoItem)
                    found = true;
            return itens[i-1];
        }

        /*
         * Retorna o texto defualt da pergunta para o item recebido
         */
        private string fa_return_quest(Item i)
        {
            string ruido = "Analise se o ruído existente interfere com a comunicação ou com a sua concentração no trabalho e, avalie.";
            string iluminacao = "Observe se a iluminação existente é suficiente (zonas de encadeamento e/ou ofuscamento, zonas escuras e/ou demasiado iluminadas) e, avalie.";
            string risco_acidente = "Considere para a análise a probabilidade de ocorrência de um acidente e a gravidade do mesmo caso ocorra e, avalie.";
            string temperatura = "Considere como referência uma situação de conforto térmico e por comparação, avalie.";
            string espaco_trabalho = "Observe se o espaço de trabalho é suficiente para adoptar posturas adequadas e realizar os movimentos livremente e se permite ajustar sempre que possível os equipamentos e materiais. Avalie.";
            string posturas = "Reflicta sobre a posição do pescoço e ombros, braços (cotovelo e pulso), tronco, ancas e pernas durante a sua actividade profissional e, avalie.";
            string elevacao = "Considere o peso que tem de levantar, a postura adoptada e o número de elevações efectuadas e, avalie.";
            string restritividade = "Verifique se as condições de desempenho das suas tarefas (equipamentos disponíveis, método de trabalho, tempo para realizar a tarefa) dificultam o seu desempenho e, avalie.";
            string repetitividade = "Considere a duração e a forma de como são repetidas do mesmo modo as tarefas que desempenha e, avalie.";
            string decicoes = "Considere se a informação disponível é suficiente e de qualidade, quando necessita de tomar uma decisão e, avalie.";
            string conteudo_trabalho = "Considere para a análise, o número e a qualidade de tarefas individuais que tem de executar e, avalie.";
            string atencao = "Considere para a análise a percentagem de tempo que tem de estar atento e o grau de exigência da tarefa e, avalie.";
            string actividade_fisica = "Considere as tarefas, métodos de trabalho e os equipamentos disponíveis para determinar o grau de exigência da sua actividade profissional e, avalie.";
            string comunicacao = "Reflicta sobre o seu relacionamento com colegas e superiores, e disponibilidade dos mesmos e, avalie.";

            if (i.NomeItem == "Ruido") return ruido;
            else if (i.NomeItem == "Iluminação") return iluminacao;
            else if (i.NomeItem == "Risco Acidente") return risco_acidente;
            else if (i.NomeItem == "Temperatura") return temperatura;
            else if (i.NomeItem == "Espaço de Trabalho") return espaco_trabalho;
            else if (i.NomeItem == "Posturas / Movimento") return posturas;
            else if (i.NomeItem == "Tarefas de elevação") return elevacao;
            else if (i.NomeItem == "Restritividade") return restritividade;
            else if (i.NomeItem == "Repetitividade") return repetitividade;
            else if (i.NomeItem == "Tomada de decisões") return decicoes;
            else if (i.NomeItem == "Conteúdo") return conteudo_trabalho;
            else if (i.NomeItem == "Nível de Atenção Requerido") return atencao;
            else if (i.NomeItem == "Actividade Física") return actividade_fisica;
            else if (i.NomeItem == "Comunicação Inter-relação") return comunicacao;
            else return "";
        }

        /*
         * Inicia o texto as perguntas
         */
        private void init_perg_fa()
        {
            if (!already_created)
            {
                ficha_avaliacao = new List<PerguntaFichaAvaliacao>();

                for (int i = 0; i < itens.Count ; i++)
                {
                    PerguntaFichaAvaliacao p = new PerguntaFichaAvaliacao(
                        codAnalise,
                        i,
                        itens[i].CodigoItem,
                        fa_return_quest(itens[i]),
                        7);
                    ficha_avaliacao.Add(p);
                }
            }
            else
            {
                ficha_avaliacao = GestaodeRespostas.getPerguntasFA(codAnalise);
            }
        }

        private void init()
        {
            panel.Controls.Clear();
            perguntas = new List<TextBox>();
            itens_pergunta = new List<ComboBox>();

            foreach(PerguntaFichaAvaliacao fa in ficha_avaliacao)
                show_pergunta(fa);
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
            l1.Text = "Pergunta " + (number +1);
            l1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            l1.Location = new Point(10, 0);
            p.Controls.Add(l1);

            return p;
        }

        private void show_pergunta(Pergunta perg)
        {
            Panel p = new System.Windows.Forms.Panel();
            p.Name = perg.Num_Pergunta.ToString();
            p.AutoSize = true;
            p.BorderStyle = BorderStyle.FixedSingle;
            p.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            p.Dock = DockStyle.Top;
            panel.Controls.Add(p);
            panel.Controls.SetChildIndex(p, 0);

            Panel barra = pergunta_barra_titulo(perg.Num_Pergunta);
            p.Controls.Add(barra);

            TextBox t1 = new System.Windows.Forms.TextBox();
            t1.Width = p.Width - 30;
            t1.Text = perg.Texto;
            t1.Name = perg.Num_Pergunta.ToString();
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
            l2.Text = "Item: ";
            l2.Location = new System.Drawing.Point(10, 70);
            p.Controls.Add(l2);

            ComboBox c1 = new System.Windows.Forms.ComboBox();
            c1.Width = 200;
            c1.Name = perg.Num_Pergunta.ToString();
            c1.Items.AddRange(nomes_itens());
            c1.SelectedIndex = numero_item(item(perg.Cod_Item));
            c1.Location = new System.Drawing.Point(65, 70);
            c1.SelectedIndexChanged += new EventHandler(MouseClickActionPerformed);
            c1.DropDownStyle = ComboBoxStyle.DropDownList;
            if (!enabled)
                c1.Enabled = false;
            p.Controls.Add(c1);
            itens_pergunta.Add(c1);

            Label l3 = new System.Windows.Forms.Label();
            l3.Width = 80;
            l3.Text = "Respostas: ";
            l3.Location = new System.Drawing.Point(10, 110);
            p.Controls.Add(l3);

            Label l4 = new System.Windows.Forms.Label();
            l4.Text = "Mudar Tipo Resposta";
            l4.AutoSize = true;
            l4.Name = perg.Num_Pergunta.ToString();
            l4.Location = new System.Drawing.Point(95, 110);
            l4.Cursor = System.Windows.Forms.Cursors.Hand;
            l4.Click += new System.EventHandler(mudarTipoRespostaClick);
            l4.MouseEnter += new System.EventHandler(this.MouseEnterAction);
            l4.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
            if (!enabled)
                l4.Enabled = false;
            p.Controls.Add(l4);

            Panel p2 = getRespostasPanel(GestaodeRespostas.getTipoEscala(perg.Cod_TipoEscala));
            p.Controls.Add(p2);
        }

        private Panel getRespostasPanel(TipoEscala ti)
        {
            Panel p = new Panel();
            p.Name = "Respostas";
            p.Height = 0;
            p.AutoSize = true;
            p.Location = new System.Drawing.Point(10, 140);

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
            }

            return p;
        }

        #endregion

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
        #endregion

        #region Tipo de Resposta Mudada
        public static void reenc_New_Anser(object sender, EventArgs e)
        {
            ip.new_Anser(sender,e);
        }
        private void new_Anser(object sender, EventArgs e)
        {
            List<object> lst = (List<object>)sender;

            int index_pergunta = (int)lst[0];
            long cod_tipoResposta = (long)lst[1];

            ficha_avaliacao[index_pergunta].Cod_TipoEscala = cod_tipoResposta;
            Panel perg = (Panel)panel.Controls[panel.Controls.IndexOfKey(index_pergunta.ToString())];
            perg.Controls.RemoveAt(6);
            Panel novo = getRespostasPanel(GestaodeRespostas.getTipoEscala(cod_tipoResposta));
            perg.Controls.Add(novo);
        }
        #endregion

        private void mudarTipoRespostaClick(object sender, EventArgs e)
        {
            Label l = (Label)sender;
            Interface_GestaoRespostas.main(int.Parse(l.Name), true);
        }

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
                for (int i = 0; i < ficha_avaliacao.Count; i++)
                {
                    ficha_avaliacao[i].Texto = perguntas[i].Text;
                    ficha_avaliacao[i].Cod_Item = itens[itens_pergunta[i].SelectedIndex].CodigoItem;
                }

                if (!already_created)
                    GestaodeRespostas.insert_PerguntasFA(ficha_avaliacao);
                else
                    GestaodeRespostas.modificaPerguntasFA(ficha_avaliacao);

                evento_FA_Done(null, new EventArgs());
                end_Frame();
            }
        }

        #region verificação Erros
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

            for (int i = 0; i < itens_pergunta.Count && !erros.ContainsKey(itens_pergunta[i]); i++)
            {
                List<object> objs = new List<object>();
                List<ErrorProvider> lst = new List<ErrorProvider>();
                for (int j = i + 1; j < itens_pergunta.Count && !erros.ContainsKey(itens_pergunta[j]); j++)
                    if (itens_pergunta[i].SelectedIndex == itens_pergunta[j].SelectedIndex)
                    {
                        if (!objs.Contains(itens_pergunta[i]))
                        {
                            ErrorProvider err1 = new ErrorProvider();
                            err1.Icon = global::ETdA.Properties.Resources.notification_warning_ico;
                            err1.SetError(itens_pergunta[i], "Existem duas perguntas com o mesmo item.");
                                lst.Add(err1);
                                objs.Add(itens_pergunta[i]);
                        }

                        ErrorProvider err2 = new ErrorProvider();
                        err2.Icon = global::ETdA.Properties.Resources.notification_warning_ico;
                        err2.SetError(itens_pergunta[j], "Existem duas perguntas com o mesmo item.");
                        lst.Add(err2);
                        objs.Add(itens_pergunta[j]);

                        ok = false;
                    }
                List<object> listas = new List<object>();
                listas.Add(lst);
                listas.Add(objs);
                foreach (object b in objs)
                    if (!erros.ContainsKey(b))
                        erros.Add(b, listas);
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
                ComboBox cb = new ComboBox();
                object p = erros.Keys.ElementAt(0);
                if (p.GetType() == tb.GetType())
                {
                    ErrorProvider err = (ErrorProvider)erros[p];
                    tb = (TextBox)p;
                    toolStripStatusLabel5.Text = "Perg. " + tb.Name;
                    toolStripStatusLabel6.Text = err.GetError(tb);
                }
                else
                {
                    List<object> errr = (List<object>)erros[p];
                    List<ErrorProvider> err = (List<ErrorProvider>)errr[0];
                    cb = (ComboBox)p;
                    toolStripStatusLabel5.Text = "Perg. " + cb.Name;
                    toolStripStatusLabel6.Text = err[0].GetError(cb);
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

        #region Limpar errorProvider
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
                if (sender.GetType().ToString() == new ComboBox().GetType().ToString())
                {
                    List<object> listas = (List<object>)erros[sender];
                    List<ErrorProvider> es = (List<ErrorProvider>)listas[0];
                    List<object> objs = (List<object>)listas[1];

                    foreach (ErrorProvider ep in es)
                        ep.Clear();
                    foreach (object o in objs)
                        erros.Remove(o);
                }
                else
                {
                    ErrorProvider err = (ErrorProvider)erros[sender];
                    err.Clear();
                    erros.Remove(sender);
                }
                setErroStatusBar();
            }
        }
        #endregion
    }
}
