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
    public partial class Interface_Importer_Matching : Form
    {
        private List<Pergunta> ps;
        private Importer_Exporter ie;
        private int tipo;
        private long cod_analise;
        private List<Zona> zonas;
        private List<Item> itens;
        private Dictionary<DataGridViewCell,string> erros;

        public Interface_Importer_Matching(object _ps, object _ie, int _tipo, long _cod_analise, object _zonas, object _itens)
        {
            InitializeComponent();

            ps = (List<Pergunta>)_ps;
            ie = (Importer_Exporter)_ie;
            tipo = _tipo;
            cod_analise = _cod_analise;
            zonas = (List<Zona>)_zonas;
            itens = (List<Item>)_itens;
            erros = new Dictionary<DataGridViewCell, string>();

            if (tipo == 2)
                button3.Visible = false;

            radioButton1.Checked = true;
            radioButton3.Checked = true;
            radioButton7.Checked = true;

            init();
            toolStripStatusLabel2.Text = ie.Valores.Keys.Count.ToString();
            toolStripStatusLabel4.Visible = false;
            toolStripStatusLabel5.Visible = false;
        }

        public static void main(object _ps, object _ie, int _tipo, long _cod_analise, object _zonas, object _itens)
        {
            Interface_Importer_Matching iim= new Interface_Importer_Matching(_ps, _ie, _tipo, _cod_analise, _zonas, _itens);
            iim.ShowDialog();
        }

        private void init()
        {
            string[] pergs;
            int j = 1;
            if (tipo == 0)
                pergs = new string[ps.Count + 1];
            else
            {
                pergs = new string[ps.Count + 2];
                pergs[1] = "Zon/Actv";
                j = 2;
            }
            pergs[0] = "";
            if (tipo != 2)
                for (int i = 0; i < ps.Count; i++)
                    pergs[i + j] = "P:" + ps[i].Num_Pergunta.ToString();
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
            Interface_PerguntasQT.main(cod_analise, itens, zonas, true, false);
        }

        private void ContinuarClick(object sender, EventArgs e)
        {
            Dictionary<float, List<int>> perguntas_colunas_ficheiro = new Dictionary<float, List<int>>();
            Dictionary<long, int> itens_colunas_ficheiro = new Dictionary<long, int>();

            #region Modos Tratamento
            Importer_Exporter.Numero_Respostas n_r;
            Importer_Exporter.Respostas_Vazias r_v;
            Importer_Exporter.Valores_Respostas v_r;

            if (radioButton1.Checked)
                n_r = Importer_Exporter.Numero_Respostas.Sair_Numero;
            else
                n_r = Importer_Exporter.Numero_Respostas.Ignorar_Formulario;
            if (radioButton3.Checked)
                r_v = Importer_Exporter.Respostas_Vazias.Sair_Vazias;
            else if (radioButton4.Checked)
                r_v = Importer_Exporter.Respostas_Vazias.Ignorar_Formulario;
            else if (radioButton5.Checked)
                r_v = Importer_Exporter.Respostas_Vazias.Ignorar_Pergunta;
            else
                r_v = Importer_Exporter.Respostas_Vazias.Ignorar_Pergunta_Nao_QE;
            if (radioButton7.Checked)
                v_r = Importer_Exporter.Valores_Respostas.Sair_Valores;
            else if (radioButton8.Checked)
                v_r = Importer_Exporter.Valores_Respostas.Ignorar_Formulario;
            else if (radioButton9.Checked)
                v_r = Importer_Exporter.Valores_Respostas.Ignorar_Pergunta;
            else
                v_r = Importer_Exporter.Valores_Respostas.Ignorar_Pergunta_Nao_QE;
            #endregion

            #region qt e fa
            if (tipo !=3 && verifica_cabecalho_qt_fa(ref perguntas_colunas_ficheiro))
            {
                if (tipo == 0)
                {
                    List<PerguntaQuestionario> pergs = new List<PerguntaQuestionario>();
                    foreach (Pergunta p in ps)
                        pergs.Add((PerguntaQuestionario)p);

                    if (ie.importar_questionario(n_r, r_v, v_r, zonas, pergs, perguntas_colunas_ficheiro))
                    {
                        string result = "Resultados\nForam importados " + ie.Formularios.Count + " formulários com sucesso.\nForam ignorados " + ie.Formularios_Ignorados + " formulários.\nForam ignoradas " + ie.Perguntas_Ignoradas + " respostas.";
                        MessageBox.Show(result, "Resultados");
                    }
                    else
                    {
                        MessageBox.Show(ie.Erro + "\n" + "Número de linha de erro: " + ie.Linha_Erro);
                        dataGridView2.Rows[ie.Linha_Erro - 1].Selected = true;
                    }
                }
                else
                {
                    List<PerguntaFichaAvaliacao> pergs = new List<PerguntaFichaAvaliacao>();
                    foreach (Pergunta p in ps)
                        pergs.Add((PerguntaFichaAvaliacao)p);

                    if (ie.importar_ficha_avaliacao(n_r, r_v, v_r, zonas, pergs, perguntas_colunas_ficheiro))
                    {
                        string result = "Resultados\nForam importados " + ie.Formularios.Count + " formulários com sucesso.\nForam ignorados " + ie.Formularios_Ignorados + " formulários.\nForam ignoradas " + ie.Perguntas_Ignoradas + " respostas.";
                        MessageBox.Show(result, "Resultados");
                    }
                    else
                    {
                        MessageBox.Show(ie.Erro + "\n" + "Número de linha de erro: " + ie.Linha_Erro);
                        dataGridView2.Rows[ie.Linha_Erro - 1].Selected = true;
                    }
                }
            }
            #endregion 

            #region cl
            if (tipo == 3 && verifica_cabecalho_cl(ref itens_colunas_ficheiro))
            {
                if (ie.importar_checklist(n_r, r_v, v_r, zonas, itens,itens_colunas_ficheiro))
                {
                    string result = "Resultados\nForam importados " + ie.Formularios.Count + " formulários com sucesso.\nForam ignorados " + ie.Formularios_Ignorados + " formulários.\nForam ignoradas " + ie.Perguntas_Ignoradas + " respostas.";
                    MessageBox.Show(result, "Resultados");
                }
                else
                {
                    MessageBox.Show(ie.Erro + "\n" + "Número de linha de erro: " + ie.Linha_Erro);
                    dataGridView2.Rows[ie.Linha_Erro - 1].Selected = true;
                }
            }
            #endregion
        }

        private void CellValueChangedActionPerformed(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (erros.ContainsKey(cell))
                erros.Remove(cell);
            setErroStateBar();
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
                    if (!row.Cells[i].Value.Equals(""))
                    {
                        float num_pergunta = float.Parse(((string)row.Cells[i].Value).Split(':')[1]);
                        TipoEscala ti = GestaodeRespostas.getTipoEscala(getPerguntaByNum(num_pergunta).Cod_TipoEscala);

                        /* Perguntas cuja resposta tem o valor e o cod_zona */
                        if (((PerguntaQuestionario)getPerguntaByNum(num_pergunta)).Cod_zona == 0 &&
                            (i + 1 >= row.Cells.Count ||
                            !((string)row.Cells[i].Value).Equals("")))
                        {
                            if (i + 1 >= row.Cells.Count)
                            {
                                if (!erros.ContainsKey(row.Cells[i]))
                                    erros.Add(row.Cells[i], "Esta coluna não pode ser associada a pergunta escolhida, pois esta necessita de duas colunas de respostas consecutivas.");
                            }
                            else
                            {
                                if (!erros.ContainsKey(row.Cells[i + 1]))
                                    erros.Add(row.Cells[i + 1], "Esta coluna não pode ser associada a nenhuma pergunta pois a coluna anterior está associada a uma pergunta que necessita de duas colunas de respostas consecutivas.");
                            }
                            setErroStateBar();
                            return_value = false;
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
                    else
                    {
                        for (int j = 0; j < _perguntas_colunas_ficheiro.Keys.Count; j++)
                        {
                            float key = _perguntas_colunas_ficheiro.Keys.ElementAt(j);
                            if (_perguntas_colunas_ficheiro[key].Contains(i))
                            {
                                _perguntas_colunas_ficheiro[key].Remove(i);
                                if (_perguntas_colunas_ficheiro[key].Count == 0)
                                    _perguntas_colunas_ficheiro.Remove(key);
                            }
                        }
                    }
                }
            }
            return return_value;
        }

        private bool verifica_cabecalho_cl(ref Dictionary<long, int> _itens_colunas_ficheiro)
        {
            bool return_value = true;
            DataGridViewRow row = dataGridView1.Rows[0];
            for (int i = 0; i < row.Cells.Count; i++)
            {
                if (row.Cells[i].Value != null)
                {
                    if (!row.Cells[i].Value.Equals(""))
                    {
                        /* Perguntas nao pode ter 2 itens iguais */
                        long cod_item = getItemByNume((string)row.Cells[i].Value).CodigoItem;
                        if (_itens_colunas_ficheiro.ContainsKey(cod_item))
                        {
                            if (!erros.ContainsKey(row.Cells[i]))
                                erros.Add(row.Cells[i], "Pergunta já associada a uma coluna");
                            setErroStateBar();
                            return_value = false;
                        }
                        else
                            _itens_colunas_ficheiro.Add(cod_item, i);
                    }
                    else
                    {
                        for (int j = 0; j < _itens_colunas_ficheiro.Keys.Count; j++)
                        {
                            long key = _itens_colunas_ficheiro.Keys.ElementAt(j);
                            if (_itens_colunas_ficheiro[key] == i)
                            {
                                _itens_colunas_ficheiro.Remove(key);
                            }
                        }
                    }
                }
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
                if (ps[i].Num_Pergunta == num)
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
    }
}
