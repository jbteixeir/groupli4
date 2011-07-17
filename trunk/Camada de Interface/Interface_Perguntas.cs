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
        private Dictionary<string, Dictionary<string, object>> panels;
        private List<Item> itens;
        private List<PerguntaFichaAvaliacao> ficha_avaliacao;
        private List<PerguntaQuestionario> questionario;

        private string[] nomes_itens()
        {
            string[] nomes = new string[itens.Count];
            for (int i = 0; i < itens.Count; i++)
                nomes[i] = itens[i].NomeItem;
            return nomes;
        }

        private int numero_item(Item item)
        {
            int i;
            bool found = false;
            for (i = 0; i < itens.Count && !found; i++)
                if (item.NomeItem == itens[i].NomeItem)
                    found = true;
            return i - 1;
        }

        private Item item(long cod_item)
        {
            int i;
            bool found = false;
            for (i = 0; i < itens.Count && !found; i++)
                if (cod_item == itens[i].CodigoItem)
                    found = true;
            return itens[i-1];
        }

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
            else if (i.NomeItem == "Tomada de Decisões") return decicoes;
            else if (i.NomeItem == "Conteúdo") return conteudo_trabalho;
            else if (i.NomeItem == "Nível de Atenção Requerido") return atencao;
            else if (i.NomeItem == "Actividade Física") return actividade_fisica;
            else if (i.NomeItem == "Comunicação Inter-relação") return comunicacao;
            else return "";
        }

        private void init_perg_fa()
        {
            ficha_avaliacao = new List<PerguntaFichaAvaliacao>();

            for ( int i = 0 ; i < itens.Count ; i++ )  
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

        private void init_perg_q()
        {
            questionario = new List<PerguntaQuestionario>();

            PerguntaQuestionario p = new PerguntaQuestionario(
                codAnalise,
                1,
                1,
                -1,
                "Idade:",
                9,
                "qc");
            questionario.Add(p);
            p = new PerguntaQuestionario(
                codAnalise,
                2,
                1,
                -1,
                "Género",
                8,
                "qc");
            questionario.Add(p);
            p = new PerguntaQuestionario(
                codAnalise,
                3,
                1,
                -1,
                "Profissão",
                10,
                "qc");
            questionario.Add(p);
            p = new PerguntaQuestionario(
                codAnalise,
                4,
                1,
                -1,
                "Habilitações Literárias",
                -1,
                "qc");
            questionario.Add(p);
            p = new PerguntaQuestionario(
                codAnalise,
                5,
                1,
                -1,
                "Qual a importância que dá às considerações ergonómicas na concepção de espaços de trabalho?",
                3,
                "ql");
            questionario.Add(p);
            p = new PerguntaQuestionario(
                codAnalise,
                6,
                1,
                -1,
                "É cliente habitual deste estabelecimento?",
                6,
                "ql");
             questionario.Add(p);
        }

        private void init()
        {
            panel.Controls.Clear();

            int y = 10;
            foreach(PerguntaFichaAvaliacao fa in ficha_avaliacao)
                y += show_pergutna(fa, y);
        }

        private int show_pergutna(Pergunta perg, int location_y)
        {
            Dictionary<string, object> itens_panel = new Dictionary<string, object>();

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
            t1.Width = p.Width - 75;
            t1.Text = perg.Texto;
            t1.Name = "t_box";
            t1.Location = new System.Drawing.Point(65, 10);
            t1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            p.Controls.Add(t1);
            itens_panel.Add("t_box", t1);

            Label l2 = new System.Windows.Forms.Label();
            l2.Width = 50;
            l2.Text = "Item: ";
            l2.Location = new System.Drawing.Point(10, 40);
            p.Controls.Add(l2);

            ComboBox c1 = new System.Windows.Forms.ComboBox();
            c1.Width = 200;
            c1.Items.AddRange(nomes_itens());
            c1.SelectedIndex = numero_item(item(perg.Cod_Item));
            c1.Location = new System.Drawing.Point(65, 40);
            p.Controls.Add(c1);
            itens_panel.Add("t_combo", c1);

            Panel p2 = new System.Windows.Forms.Panel();
            p2.Name = "Respostas";
            p2.Width = 320;
            p2.Location = new System.Drawing.Point(0, 70);
            p.Controls.Add(p2);

            Label l3 = new System.Windows.Forms.Label();
            l3.Width = 80;
            l3.Text = "Respostas: ";
            l3.Location = new System.Drawing.Point(10, 10);
            p2.Controls.Add(l3);

            Label l4 = new System.Windows.Forms.Label();
            l4.Width = 200;
            l4.Text = "Mudar Tipo Resposta";
            l4.Name = perg.Num_Pergunta.ToString();
            l4.Location = new System.Drawing.Point(95, 10);
            l4.Cursor = System.Windows.Forms.Cursors.Hand;
            l4.Click += new System.EventHandler(mudarTipoRespostaClick);
            l4.MouseEnter += new System.EventHandler(this.MouseEnterAction);
            l4.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
            p2.Controls.Add(l4);

            TipoEscala ti = GestaodeRespostas.getTipoEscala(perg.Cod_TipoEscala);
            
            int y = 40;
            if (ti.Numero >= -1 && ti.Numero <= 1)
            {
                TextBox t2 = new System.Windows.Forms.TextBox();
                t2.Width = 100;
                t2.Name = "t_box";
                t2.Location = new System.Drawing.Point(10, 40);
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
            p2.Height = y ;

            p.Height = 70+p2.Height;
            panel.Controls.Add(p);

            return p.Height;
        }

        /*
        private void show_add(int location_y)
        {
            Panel p = new System.Windows.Forms.Panel();
            p.Width = 500;
            p.BorderStyle = BorderStyle.FixedSingle;
            p.Location = new System.Drawing.Point(7, location_y);

            Label l = new System.Windows.Forms.Label();
            l.Width = 200;
            l.Text = "Mudar Tipo Resposta";
            l.Location = new System.Drawing.Point(95, 10);
            l.Cursor = System.Windows.Forms.Cursors.Hand;
            l.Click += new System.EventHandler(adcionarPerguntaClick);
            l.MouseEnter += new System.EventHandler(this.MouseEnterAction);
            l.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
            p.Controls.Add(l);
        }
        */

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

        private void mudarTipoRespostaClick(object sender, EventArgs e)
        {
            Label l = (Label)sender;
            Interface_GestaoRespostas.main(int.Parse(l.Name));
        }

        public Interface_Perguntas(long codAnalise, object itens)
        {
            InitializeComponent();
            this.codAnalise = codAnalise;
            this.itens = (List<Item>)itens;

            init_perg_fa();
            init_perg_q();

            toolStripStatusLabel2.Text = ficha_avaliacao.Count.ToString();

            init();
        }

        public static void main(long codAnalise, object itens)
        {
            ip = new Interface_Perguntas(codAnalise, itens);
            ip.Visible = true;
        }

        public static void reenc_New_Anser(object sender, EventArgs e)
        {
            ip.new_Anser(sender,e);
        }
        private void new_Anser(object sender, EventArgs e)
        {
            List<object> lst = (List<object>)sender;
            int num_pergunta = (int)lst[0];
            long cod_tipoResposta = (long)lst[1];

            ficha_avaliacao[num_pergunta].Cod_TipoEscala = cod_tipoResposta;
            init();
        }
    }
}
