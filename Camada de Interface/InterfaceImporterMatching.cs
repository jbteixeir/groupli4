using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ETdAnalyser.CamadaDados.Classes.Estruturas;
using ETdAnalyser.CamadaDados.Classes;
using ETdAnalyser.Camada_de_Negócio;
using ETdAnalyser.CamadaDados.Classes.Verificador;


namespace ETdAnalyser.CamadaInterface
{
    public partial class InterfaceImporterMatching : Form
    {
        private static InterfaceImporterMatching iim;
        private List<Pergunta> ps;
        private Importer ie;
        private Enums.Tipo_Formulário tipo;
        private long codigoAnalise;
        private List<Zona> zonas;
        private List<Item> itens;
        private Dictionary<DataGridViewCell,string> erros;
        private Dictionary<int, Zona> mapa_zona_coluna;

        public InterfaceImporterMatching(object _ps, object _ie, object _tipo, long _codigoAnalise, object _zonas, object _itens)
        {
            InitializeComponent();

            ps = (List<Pergunta>)_ps;
            ie = (Importer)_ie;
            tipo = (Enums.Tipo_Formulário)_tipo;
            codigoAnalise = _codigoAnalise;
            zonas = (List<Zona>)_zonas;
            itens = (List<Item>)_itens;
            erros = new Dictionary<DataGridViewCell, string>();

            if (tipo == Enums.Tipo_Formulário.CheckList)
                button3.Visible = false;

            radioButton2.Checked = true;
            radioButton5.Checked = true;
            radioButton9.Checked = true;

            if (tipo != Enums.Tipo_Formulário.Questionario)
            {
                radioButton6.Visible = false;
                radioButton10.Visible = false;
            }

            init();
            toolStripStatusLabel2.Text = ie.Valores.Keys.Count.ToString();
            toolStripStatusLabel4.Visible = false;
            toolStripStatusLabel5.Visible = false;
        }

        public static void main(object _ps, object _ie, object _tipo, long _codigoAnalise, object _zonas, object _itens)
        {
            var ci = System.Globalization.CultureInfo.InvariantCulture.Clone() as System.Globalization.CultureInfo;
            ci.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            iim = new InterfaceImporterMatching(_ps, _ie, _tipo, _codigoAnalise, _zonas, _itens);
            iim.ShowDialog();
        }

        private void init()
        {
            string[] pergs;
            int j = 1;
            if (tipo == Enums.Tipo_Formulário.Questionario)
                pergs = new string[ps.Count + 1];
            else if (tipo == Enums.Tipo_Formulário.Ficha_Avaliacao)
            {
                pergs = new string[ps.Count + 2];
                pergs[1] = "Zn/Ac";
                j = 2;
            }
            else
            {
                pergs = new string[itens.Count + 2];
                pergs[1] = "Zn/Ac";
                j = 2;
            }
            pergs[0] = "";
            if (tipo != Enums.Tipo_Formulário.CheckList)
                for (int i = 0; i < ps.Count; i++)
                    pergs[i + j] = "P:" + ps[i].NumeroPergunta.ToString();
            else
                for (int i = 0; i < itens.Count; i++)
                    pergs[i + j] = itens[i].NomeItem;

            foreach (string s in ie.Colunas)
            {
                DataGridViewComboBoxColumn coluna = new DataGridViewComboBoxColumn();
                DataGridViewCell cell = new DataGridViewComboBoxCell();
                coluna.CellTemplate = cell;
                coluna.HeaderText = s;
                coluna.Width = 60;
                coluna.Items.AddRange(pergs);
                dataGridView1.Columns.Add(coluna);
            }

            dataGridView1.Rows.Add(new string[ie.Colunas.Length]);
            dataGridView2.ColumnHeadersVisible = false;

            foreach (string s in ie.Colunas)
            {
                DataGridViewColumn coluna = new DataGridViewColumn();
                DataGridViewCell cell = new DataGridViewTextBoxCell();
                coluna.CellTemplate = cell;
                coluna.HeaderText = s;
                coluna.Width = 60;
                dataGridView2.Columns.Add(coluna);
            }

            foreach (int i in ie.Valores.Keys)
                dataGridView2.Rows.Add(ie.Valores[i]);
        }

        #region Eventos
        private void ScrollEvent(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
                dataGridView1.HorizontalScrollingOffset = e.NewValue;
        }

        private void VoltarClick(object sender, EventArgs e)
        {
            end_Frame();
        }

        private void MostrarPerguntasClick(object sender, EventArgs e)
        {
            InterfacePerguntasQT.main(codigoAnalise, itens, zonas, true, false);
        }

        private void ContinuarClick(object sender, EventArgs e)
        {
            Dictionary<float, List<int>> perguntas_colunas_ficheiro = new Dictionary<float, List<int>>();
            Dictionary<string, int> itens_colunas_ficheiro = new Dictionary<string, int>();

            foreach (DataGridViewRow r in dataGridView2.Rows)
            {
                r.DefaultCellStyle.BackColor = Color.White;
                foreach (DataGridViewCell c in r.Cells)
                    c.Style.BackColor = Color.White;
            }

            #region Modos Tratamento
            Enums.Numero_Respostas n_r;
            Enums.Respostas_Vazias r_v;
            Enums.Valores_Respostas v_r;

            if (radioButton1.Checked)
                n_r = Enums.Numero_Respostas.Sair_Numero;
            else
                n_r = Enums.Numero_Respostas.Ignorar_Formulario;
            if (radioButton3.Checked)
                r_v = Enums.Respostas_Vazias.Sair_Vazias;
            else if (radioButton4.Checked)
                r_v = Enums.Respostas_Vazias.Ignorar_Formulario;
            else if (radioButton5.Checked)
                r_v = Enums.Respostas_Vazias.Ignorar_Pergunta;
            else
                r_v = Enums.Respostas_Vazias.Ignorar_Pergunta_Nao_QE;
            if (radioButton7.Checked)
                v_r = Enums.Valores_Respostas.Sair_Valores;
            else if (radioButton8.Checked)
                v_r = Enums.Valores_Respostas.Ignorar_Formulario;
            else if (radioButton9.Checked)
                v_r = Enums.Valores_Respostas.Ignorar_Pergunta;
            else
                v_r = Enums.Valores_Respostas.Ignorar_Pergunta_Nao_QE;
            #endregion

            if (tipo != Enums.Tipo_Formulário.CheckList && verifica_cabecalho_qt_fa(ref perguntas_colunas_ficheiro))
            {
                MessageBox.Show("cabecalho ok");
                if (tipo == Enums.Tipo_Formulário.Questionario)
                {
                    #region QT
                    List<PerguntaQuestionario> pergs = new List<PerguntaQuestionario>();
                    foreach (Pergunta p in ps)
                        pergs.Add((PerguntaQuestionario)p);
                    
                    if (ie.ImportarQuestionario(n_r, r_v, v_r, zonas, pergs, perguntas_colunas_ficheiro,mapa_zona_coluna))
                    {
                        int num_ignorados = 0;
                        int num_resp_ignoradas = 0;
                        foreach (int linha in ie.Resultados.Keys)
                        {
                            if (ie.Resultados[linha].Length > 1)
                            {
                                for (int i = 0; i < ie.Resultados[linha].Length; i++)
                                {
                                    if (ie.Resultados[linha][i] == Enums.Resultado_Importacao.Sucesso)
                                    {
                                        dataGridView2.Rows[linha].Cells[i].Style.BackColor = Color.Green;
                                    }
                                    else if (ie.Resultados[linha][i] == Enums.Resultado_Importacao.Insucesso)
                                    {
                                        dataGridView2.Rows[linha].Cells[i].Style.BackColor = Color.Red;
                                        num_resp_ignoradas++;
                                    }
                                }
                            }
                            else
                            {
                                for (int i = 0 ; i < dataGridView2.Rows[linha].Cells.Count ; i++)
                                    dataGridView2.Rows[linha].Cells[i].Style.BackColor = Color.Red;
                                num_ignorados++;
                            }
                        }

                        string result = "Resultados\nForam importados " + ie.Formularios.Count + " formulários com sucesso.\n" + 
                            "Foram ignorados " + num_ignorados + " formulários.\n" + 
                            "Foram ignoradas " + num_resp_ignoradas + " respostas.";
                        MessageBox.Show(result, "Resultados");

                        if (MessageBoxPortuguese.Show("Pergunta", "Deseja submeter estes qustionários na base de dados?", MessageBoxPortuguese.Button_YesNo, MessageBoxPortuguese.Icon_Question) == System.Windows.Forms.DialogResult.Yes)
                            ie.SubmeteQuestionarios();
                    }
                    else
                    {
                        MessageBox.Show(ie.Erro + "\n" + "Número de linha de erro: " + ie.LinhaErro);
                        dataGridView2.Rows[ie.LinhaErro].Selected = true;
                    }
                    #endregion
                }
                else
                {
                    #region FA
                    List<PerguntaFichaAvaliacao> pergs = new List<PerguntaFichaAvaliacao>();
                    foreach (Pergunta p in ps)
                        pergs.Add((PerguntaFichaAvaliacao)p);

                    bool found = false;
                    int index = 0;
                    for (int i = 0 ; i < dataGridView1.Rows[0].Cells.Count && !found ; i++)
                        if (dataGridView1.Rows[0].Cells[i].Value != null && dataGridView1.Rows[0].Cells[i].ToString().Equals("Zn/Ac"))
                        {
                            index = i;
                            found = true;
                        }

                    if (ie.ImportarFichaAvaliacao(n_r, r_v, v_r, zonas, pergs, perguntas_colunas_ficheiro, mapa_zona_coluna, index))
                    {
                        int num_ignorados = 0;
                        int num_resp_ignoradas = 0;
                        foreach (int linha in ie.Resultados.Keys)
                        {
                            if (ie.Resultados[linha].Length > 1)
                            {
                                for (int i = 0; i < ie.Resultados[linha].Length; i++)
                                {
                                    if (ie.Resultados[linha][i] == Enums.Resultado_Importacao.Sucesso)
                                    {
                                        dataGridView2.Rows[linha].Cells[i].Style.BackColor = Color.Green;
                                    }
                                    else if (ie.Resultados[linha][i] == Enums.Resultado_Importacao.Insucesso)
                                    {
                                        dataGridView2.Rows[linha].Cells[i].Style.BackColor = Color.Red;
                                        num_resp_ignoradas++;
                                    }
                                }
                            }
                            else
                            {
                                for (int i = 0; i < dataGridView2.Rows[linha].Cells.Count; i++)
                                    dataGridView2.Rows[linha].Cells[i].Style.BackColor = Color.Red;
                                num_ignorados++;
                            }
                        }

                        string result = "Resultados\nForam importados " + ie.Formularios.Count + " formulários com sucesso.\n" +
                            "Foram ignorados " + num_ignorados + " formulários.\n" +
                            "Foram ignoradas " + num_resp_ignoradas + " respostas.";
                        MessageBox.Show(result, "Resultados");

                        if (MessageBoxPortuguese.Show("Pergunta", "Deseja submeter estas fichas de avaliação na base de dados?", MessageBoxPortuguese.Button_YesNo, MessageBoxPortuguese.Icon_Question) == System.Windows.Forms.DialogResult.Yes)
                            ie.SubmeterFichasAvaliacao();
                    }
                    else
                    {
                        MessageBox.Show(ie.Erro + "\n" + "Número de linha de erro: " + ie.LinhaErro);
                        dataGridView2.Rows[ie.LinhaErro].Selected = true;
                    }
                    #endregion
                }
            }
            else if (tipo == Enums.Tipo_Formulário.CheckList && verifica_cabecalho_cl(ref itens_colunas_ficheiro))
            {
                #region CL

                bool found = false;
                int index = 0;
                for (int i = 0; i < dataGridView1.Rows[0].Cells.Count && !found; i++)
                    if (dataGridView1.Rows[0].Cells[i].Value != null && dataGridView1.Rows[0].Cells[i].ToString().Equals("Zn/Ac"))
                    {
                        index = i;
                        found = true;
                    }

                if (ie.ImportarChecklist(n_r, r_v, v_r, zonas, itens,itens_colunas_ficheiro,mapa_zona_coluna,index))
                {
                    int num_ignorados = 0;
                    int num_resp_ignoradas = 0;
                    foreach (int linha in ie.Resultados.Keys)
                    {
                        if (ie.Resultados[linha].Length > 1)
                        {
                            for (int i = 0; i < ie.Resultados[linha].Length; i++)
                            {
                                if (ie.Resultados[linha][i] == Enums.Resultado_Importacao.Sucesso)
                                {
                                    dataGridView2.Rows[linha].Cells[i].Style.BackColor = Color.Green;
                                }
                                else if (ie.Resultados[linha][i] == Enums.Resultado_Importacao.Insucesso)
                                {
                                    dataGridView2.Rows[linha].Cells[i].Style.BackColor = Color.Red;
                                    num_resp_ignoradas++;
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < dataGridView2.Rows[linha].Cells.Count; i++)
                                dataGridView2.Rows[linha].Cells[i].Style.BackColor = Color.Red;
                            num_ignorados++;
                        }
                    }

                    string result = "Resultados\nForam importados " + ie.Formularios.Count + " Zonas/Actividades com sucesso.\n" +
                        "Foram ignoradas " + num_ignorados + " Zonas/Actividadas.\n" +
                        "Foram ignoradas " + num_resp_ignoradas + " respostas.";
                    MessageBox.Show(result, "Resultados");

                    if (MessageBoxPortuguese.Show("Pergunta", "Deseja submeter a Checklist na base de dados?", MessageBoxPortuguese.Button_YesNo, MessageBoxPortuguese.Icon_Question) == System.Windows.Forms.DialogResult.Yes)
                        ie.SubmeterCheckList();
                    }
                else
                {
                    MessageBox.Show(ie.Erro + "\n" + "Número de linha de erro: " + ie.LinhaErro);
                    dataGridView2.Rows[ie.LinhaErro].Selected = true;
                }
                #endregion
            }
        }

        private void CellValueChangedActionPerformed(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (erros.ContainsKey(cell))
            {
                string erro = erros[cell];
                for (int i = 0 ; i < erros.Keys.Count ; i++)
                    if (erros[erros.Keys.ElementAt(i)].Equals(erro))
                        erros.Remove(cell);
                
            }
            else if (e.ColumnIndex + 1 < dataGridView1.Rows[e.RowIndex].Cells.Count)
            {
                cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 1];
                string erro = "Esta coluna não pode ser associada a nenhuma pergunta pois a coluna anterior" +
                    "está associada a uma pergunta que necessita de duas colunas de respostas consecutivas.";
                if (erros.ContainsKey(cell) && erros[cell].Equals(erro))
                    erros.Remove(cell);
            }
            setErroStateBar();
        }

        private void Mapear_Zonas(object sender, EventArgs e)
        {
            for (int i = erros.Keys.Count - 1; i >= 0 ; i--)
            {
                if (erros[erros.Keys.ElementAt(i)].Equals("É necessário fazer o mapeamento das zonas."))
                    erros.Remove(erros.Keys.ElementAt(i));
            }
            setErroStateBar();

            List<int> codigos_zona = new List<int>();
            if (getAssociacaoColunaZona_Actividade(ref codigos_zona))
            {
                InterfaceAssociacaoZonaColuna.main(zonas, codigos_zona);
            }
            else
            {
                MessageBox.Show("Não existe nenhuma coluna associada à Zona/Actividade","Erro");
            }
        }

        public static void reencMapeamento(object sender, EventArgs e)
        {
            iim.mapeamento((Dictionary<int,Zona>)sender);
        }
        private void mapeamento(Dictionary<int, Zona> mapa)
        {
            mapa_zona_coluna = mapa;
        }
        #endregion

        private void end_Frame()
        {
            Dispose();
            Close();
        }

        #region Verificação de erros

        private bool verifica_cabecalho_qt_fa(ref Dictionary<float, List<int>> _perguntas_colunas_ficheiro)
        {
            bool return_value = true;
            DataGridViewRow row = dataGridView1.Rows[0];
            for (int i = 0; i < row.Cells.Count; i++)
            {
                if (row.Cells[i].Value != null)
                {
                    if (!row.Cells[i].Value.ToString().Equals("") && !row.Cells[i].Value.ToString().Equals("Zn/Ac"))
                    {
                        // retirar a pergunta a que esta coluna esta associada
                        float num_pergunta = float.Parse(((string)row.Cells[i].Value).Split(':')[1]);
                        TipoEscala ti = GestaodeRespostas.getTipoEscala(getPerguntaByNum(num_pergunta).CodigoTipoEscala);

                        /* Perguntas cuja resposta tem o valor escalaResposta o codigo */
                        if ( tipo == Enums.Tipo_Formulário.Questionario && ((PerguntaQuestionario)getPerguntaByNum(num_pergunta)).CodigoZona == 0 )
                        {
                            if (mapa_zona_coluna == null)
                            {
                                if (!erros.ContainsKey(row.Cells[i]))
                                    erros.Add(row.Cells[i], "É necessário fazer o mapeamento das zonas.");
                                setErroStateBar();
                                return_value = false;
                            }
                            else if (i + 1 >= row.Cells.Count)
                            {
                                if (!erros.ContainsKey(row.Cells[i]))
                                    erros.Add(row.Cells[i], "Esta coluna não pode ser associada a pergunta escolhida, pois esta necessita de duas colunas de respostas consecutivas.");
                                setErroStateBar();
                                return_value = false;
                            }
                            else if ( i + 1 < row.Cells.Count && row.Cells[i + 1].Value != null && !((string)row.Cells[i + 1].Value).Equals(""))
                            {
                                if (!erros.ContainsKey(row.Cells[i + 1]))
                                    erros.Add(row.Cells[i + 1], "Esta coluna não pode ser associada a nenhuma pergunta pois a coluna anterior está associada a uma pergunta que necessita de duas colunas de respostas consecutivas.");
                                setErroStateBar();
                                return_value = false;
                            }
                        }

                        /* Perguntas cujo tipo de resposta tem apenas um valor nao pode ter duas colunas */
                        if (_perguntas_colunas_ficheiro.ContainsKey(num_pergunta))
                        {
                            if (ti.Numero == -2)
                                _perguntas_colunas_ficheiro[num_pergunta].Add(i);
                            else
                            {
                                if (!erros.ContainsKey(row.Cells[i]))
                                    erros.Add(row.Cells[i], "Pergunta já associada a uma coluna");
                                setErroStateBar();
                                return_value = false;
                            }
                        }
                        else
                            _perguntas_colunas_ficheiro.Add(num_pergunta, new List<int>() { i });
                    }
                    else if (tipo == Enums.Tipo_Formulário.Ficha_Avaliacao && mapa_zona_coluna == null)
                    {
                        if (!erros.ContainsKey(row.Cells[i]))
                            erros.Add(row.Cells[i], "É necessário fazer o mapeamento das zonas.");
                        setErroStateBar();
                        return_value = false;
                    }
                    else
                    {
                        // supostamente esta célula não está assiciada a nenuma pergunta
                        // para cada associacao pergunta->colunas
                        for (int j = 0; j < _perguntas_colunas_ficheiro.Keys.Count; j++)
                        {
                            float key = _perguntas_colunas_ficheiro.Keys.ElementAt(j);
                            // se alguma associacao tiver esta coluna
                            if (_perguntas_colunas_ficheiro[key].Contains(i))
                            {
                                // elimina-a (esta não esta associada a nada)
                                _perguntas_colunas_ficheiro[key].Remove(i);
                                // se nao tiver mais elemento nenhum
                                if (_perguntas_colunas_ficheiro[key].Count == 0)
                                    // elimina a entrada na tabela
                                    _perguntas_colunas_ficheiro.Remove(key);
                            }
                        }
                    }
                }
            }

            // Verificar se perguntas com várias respostas tem as colunas necessárias
            for (int j = 0; j < _perguntas_colunas_ficheiro.Keys.Count; j++)
            {
                float num_pergunta = _perguntas_colunas_ficheiro.Keys.ElementAt(j);
                TipoEscala ti = GestaodeRespostas.getTipoEscala(getPerguntaByNum(num_pergunta).CodigoTipoEscala);
                
                // Varias opcoes
                if (ti.Numero == -2 && ti.Respostas.Count != _perguntas_colunas_ficheiro[num_pergunta].Count)
                {
                    // Numero de respostas nao corresponde ao número de colunas 
                    for (int h = 0; h < _perguntas_colunas_ficheiro[num_pergunta].Count; h++)
                    {
                        if (!erros.ContainsKey(row.Cells[h]))
                            erros.Add(row.Cells[h], "Pergunta " + num_pergunta + " necessita de " + ti.Respostas.Count + "colunas de respsota.");
                    }
                    setErroStateBar();
                    return_value = false;
                }
            }

            if (_perguntas_colunas_ficheiro.Keys.Count == 0)
            {
                MessageBoxPortuguese.Show("Erro", "Não existem associacoes de perguntas/colunas.", MessageBoxPortuguese.Icon_Error);
                return_value = false;
            }
            return return_value;
        }

        private bool verifica_cabecalho_cl(ref Dictionary<string, int> _itens_colunas_ficheiro)
        {
            bool return_value = true;
            DataGridViewRow row = dataGridView1.Rows[0];
            for (int i = 0; i < row.Cells.Count; i++)
            {
                if (row.Cells[i].Value != null)
                {
                    if (!row.Cells[i].Value.ToString().Equals("") && !row.Cells[i].Value.ToString().Equals("Zn/Ac"))
                    {
                        /* Perguntas nao pode ter 2 itens iguais */
                        if (_itens_colunas_ficheiro.ContainsKey((string)row.Cells[i].Value.ToString()))
                        {
                            if (!erros.ContainsKey(row.Cells[i]))
                                erros.Add(row.Cells[i], "Item já associada a uma coluna");
                            setErroStateBar();
                            return_value = false;
                        }
                        else
                            _itens_colunas_ficheiro.Add((string)row.Cells[i].Value.ToString(), i);
                    }
                    else if (mapa_zona_coluna == null)
                    {
                        if (!erros.ContainsKey(row.Cells[i]))
                            erros.Add(row.Cells[i], "É necessário fazer o mapeamento das zonas.");
                        setErroStateBar();
                        return_value = false;
                    }
                    else
                    {
                        for (int j = 0; j < _itens_colunas_ficheiro.Keys.Count; j++)
                        {
                            string key = _itens_colunas_ficheiro.Keys.ElementAt(j);
                            if (_itens_colunas_ficheiro[key] == i)
                            {
                                _itens_colunas_ficheiro.Remove(key);
                            }
                        }
                    }
                }
            }
            if (_itens_colunas_ficheiro.Keys.Count == 0)
            {
                MessageBoxPortuguese.Show("Erro", "Não existem associacoes de itens/colunas.", MessageBoxPortuguese.Icon_Error);
                return_value = false;
            }
            return return_value;
        }

        private void setErroStateBar()
        {
            if (erros.Count != 0)
            {
                DataGridViewCell cell = erros.Keys.ElementAt(0);
                cell.DataGridView.ClearSelection();
                cell.Selected = true;
                toolStripStatusLabel5.Text = erros[cell];
                toolStripStatusLabel4.Visible = true;
                toolStripStatusLabel5.Visible = true;
            }
            else
            {
                toolStripStatusLabel4.Visible = false;
                toolStripStatusLabel5.Visible = false;
            }
        }

        #endregion

        private Pergunta getPerguntaByNum(float num)
        {
            for (int i = 0; i < ps.Count; i++)
                if (ps[i].NumeroPergunta == num)
                    return ps[i];
            return null;
        }

        private Item getItemByNume(string name)
        {
            for (int i = 0; i < ps.Count; i++)
                if (itens[i].NomeItem == name)
                    return itens[i];
            return null;
        }

        private bool getAssociacaoColunaZona_Actividade(ref List<int> valores_zonas)
        {
            DataGridViewRow row = dataGridView1.Rows[0];

            bool tem_associacao = false;
            for (int i = 0; i < row.Cells.Count; i++)
            {
                if (((string)row.Cells[i].Value) != null && ((string)row.Cells[i].Value).Equals("Zn/Ac") && (tipo == Enums.Tipo_Formulário.Ficha_Avaliacao || tipo == Enums.Tipo_Formulário.CheckList))
                {
                    for (int j = 0; j < dataGridView2.Rows.Count; j++)
                        if (Input_Verifier.SoNumeros(dataGridView2.Rows[j].Cells[i].Value.ToString()) &&
                            !valores_zonas.Contains(int.Parse(dataGridView2.Rows[j].Cells[i].Value.ToString())))
                        {
                            valores_zonas.Add(int.Parse(dataGridView2.Rows[j].Cells[i].Value.ToString()));
                            tem_associacao = true;
                        }
                }
                else if ((string)row.Cells[i].Value != null && tipo == Enums.Tipo_Formulário.Questionario)
                {
                    float num_pergunta = float.Parse(((string)row.Cells[i].Value).Split(':')[1]);
                    if (((PerguntaQuestionario)getPerguntaByNum(num_pergunta)).CodigoZona == 0)
                        for (int j = 0; j < dataGridView2.Rows.Count; j++)
                           if (Input_Verifier.SoNumeros(dataGridView2.Rows[j].Cells[i + 1].Value.ToString()) &&
                                !valores_zonas.Contains(int.Parse(dataGridView2.Rows[j].Cells[i + 1].Value.ToString())))
                            {
                                valores_zonas.Add(int.Parse(dataGridView2.Rows[j].Cells[i + 1].Value.ToString()));
                                tem_associacao = true;
                            }
                }
            }
            return tem_associacao;
        }

        private void DataValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            string novo_valor = dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null ? "" :
                dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            ie.Valores[e.RowIndex][e.ColumnIndex] = novo_valor;
        }
    }
}
