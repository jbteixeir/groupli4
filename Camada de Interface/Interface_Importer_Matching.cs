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
            string[] pergs = new string[ps.Count];
            for (int i = 0; i < ps.Count; i++)
                pergs[i] = "P:" + ps[i].Num_Pergunta.ToString();

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

            if (verifica_cabecalho(ref perguntas_colunas_ficheiro))
            {

            }
        }

        private void CellValueChangedActionPerformed(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show(sender.GetType().ToString());
            
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

        private bool verifica_cabecalho(ref Dictionary<float, List<int>> _perguntas_colunas_ficheiro)
        {
            bool return_value = true;
            DataGridViewRow row = dataGridView1.Rows[0];
            for (int i = 0; i < row.Cells.Count; i++)
            {
                if (!((string)row.Cells[i].Value).Equals(""))
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
                        return_value =  false;
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
    }
}
