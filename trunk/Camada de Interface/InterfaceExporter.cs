using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Linq;
using System.IO;
using ETdAnalyser.CamadaDados.Classes;
using ETdAnalyser.Camada_de_Negócio;
using ETdAnalyser.CamadaDados.Classes.Estruturas;

namespace ETdAnalyser.CamadaInterface
{
    public partial class InterfaceExporter : Form
    {
        private long cod_analise;
        private Dictionary<object, ErrorProvider> erros;
        private List<Zona> zonas;
        private List<Item> itens;

        public InterfaceExporter(long _cod_analise, object _zonas, object _itens)
        {
            cod_analise = _cod_analise;
            zonas = (List<Zona>)_zonas;
            itens = (List<Item>)_itens;
            InitializeComponent();
            erros = new Dictionary<object, ErrorProvider>();
            toolStripProgressBar1.Visible = false;
            toolStripStatusLabel1.Visible = false;
            toolStripStatusLabel2.Visible = false;
        }

        public static void main(long _cod_analise, object _zonas, object _itens)
        {
            InterfaceExporter i = new InterfaceExporter(_cod_analise, _zonas, _itens);
            i.ShowDialog();
        }

        private void importar(object sender, EventArgs e)
        {
            if (verificaErros())
            {
                Exporter exporter = null;
                string erro = "";
                switch (comboBox1.SelectedIndex)
                {
                    case 0: // Questionario
                        List<Questionario> questionarios = GestaodeRespostas.getQuestionarios(cod_analise);
                        List<PerguntaQuestionario> perguntas_questionario = GestaodeRespostas.getPerguntasQT(cod_analise);

                        exporter = new Exporter(textBox1.Text, questionarios, perguntas_questionario, zonas);

                        if (!exporter.VerificaCriacaoFicheiro(ref erro))
                            MessageBoxPortuguese.Show("Erro", erro, MessageBoxPortuguese.Icon_Error);
                        else
                        {
                            exporter.GravaQuestionarios();
                            MessageBoxPortuguese.Show("Info", "A exportação foi efectuada com sucesso", MessageBoxPortuguese.Icon_Info);
                        }
                        break;
                    case 1: // ficha avaliacao
                        List<FichaAvaliacao> fichas_avaliacao = GestaodeRespostas.getFichasAvaliacao(cod_analise);

                        exporter = new Exporter(textBox1.Text, fichas_avaliacao, itens, zonas);

                        if (!exporter.VerificaCriacaoFicheiro(ref erro))
                            MessageBoxPortuguese.Show("Erro", erro, MessageBoxPortuguese.Icon_Error);
                        else
                        {
                            exporter.GravaFichaAvaliacao();
                            MessageBoxPortuguese.Show("Info", "A exportação foi efectuada com sucesso", MessageBoxPortuguese.Icon_Info);
                        }
                        break;
                    case 2:
                        CheckList cl = GestaodeRespostas.getChecklist(cod_analise);

                        exporter = new Exporter(textBox1.Text, cl, itens, zonas);

                        if (!exporter.VerificaCriacaoFicheiro(ref erro))
                            MessageBoxPortuguese.Show("Erro", erro, MessageBoxPortuguese.Icon_Error);
                        else
                        {
                            exporter.GravaChecklist();
                            MessageBoxPortuguese.Show("Info", "A exportação foi efectuada com sucesso", MessageBoxPortuguese.Icon_Info);
                        }
                        break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog fd = new SaveFileDialog();
            fd.Filter = "Ficheiros SPSS (*.csv)|*.csv";

            if (fd.ShowDialog() == DialogResult.OK)
                textBox1.Text = fd.FileName;
        }

        private void end_Frame()
        {
            Dispose();
            Close();
        }

        private void CancelarActionPerformed(object sender, EventArgs e)
        {
            end_Frame();
        }

        #region Verificação Erros
        private bool verificaErros()
        {
            bool podeAdicionar = true;
            if (textBox1.Text.Equals(""))
            {
                if (!erros.Keys.Contains(textBox1))
                {
                    ErrorProvider err = new ErrorProvider();
                    err.Icon = global::ETdAnalyser.Properties.Resources.notification_warning_ico;
                    err.SetError(textBox1, "É necessário introduzir o nome do ficheiro a exportar.");

                    erros.Add(textBox1, err);
                }
                podeAdicionar = false;
            }
            if (comboBox1.SelectedIndex < 0 || comboBox1.SelectedIndex > 2)
            {
                if (!erros.Keys.Contains(comboBox1))
                {
                    ErrorProvider err = new ErrorProvider();
                    err.Icon = global::ETdAnalyser.Properties.Resources.notification_warning_ico;
                    err.SetError(comboBox1, "É necessário escolher o formulário.");

                    erros.Add(comboBox1, err);
                }
                podeAdicionar = false;
            }
            else if (comboBox1.SelectedIndex == 0 && !GestaodeRespostas.isQTcreated(cod_analise))
            {
                podeAdicionar = false;
                DialogResult resultado = MessageBoxPortuguese.Show("Erro", "É necessário criar o Questionário.\nPretende fazer isso agora?", MessageBoxPortuguese.Button_YesNo, MessageBoxPortuguese.Icon_Question);
                if (resultado == DialogResult.Yes)
                    InterfaceGestaoFormulariosOnline.main(cod_analise, itens, zonas);
            }
            else if (comboBox1.SelectedIndex == 1 && !GestaodeRespostas.isFAcreated(cod_analise))
            {
                podeAdicionar = false;
                DialogResult resultado = MessageBoxPortuguese.Show("Erro", "É necessário criar a Ficha de Avaliação.\nPretende fazer isso agora?", MessageBoxPortuguese.Button_YesNo, MessageBoxPortuguese.Icon_Question);
                if (resultado == DialogResult.Yes)
                    InterfaceGestaoFormulariosOnline.main(cod_analise, itens, zonas);
            }

            setErroStatusBar();
            return podeAdicionar;
        }

        private void setErroStatusBar()
        {
            if (erros.Count != 0)
            {
                TextBox tb = new TextBox();

                object p = erros.Keys.ElementAt(0);
                ErrorProvider err = (ErrorProvider)erros[p];

                if (p.GetType() == tb.GetType())
                {
                    tb = (TextBox)p;
                    toolStripStatusLabel2.Text = err.GetError(tb);
                }
                else
                {
                    ComboBox cb = (ComboBox)p;
                    toolStripStatusLabel2.Text = err.GetError(cb);
                }
                toolStripStatusLabel1.Visible = true;
                toolStripStatusLabel2.Visible = true;
            }
            else
            {
                toolStripStatusLabel1.Visible = false;
                toolStripStatusLabel2.Visible = false;
            }
        }
        #endregion

        #region Limpa Erros
        private void KeyPressActionPerformed(object sender, KeyPressEventArgs e)
        {
            if (erros.Keys.Contains(sender))
            {
                ErrorProvider err = erros[sender];
                err.Clear();
                erros.Remove(sender);
                setErroStatusBar();
            }
        }

        private void MouseClickActionPerformed(object sender, EventArgs e)
        {
            if (erros.Keys.Contains(sender))
            {
                ErrorProvider err = erros[sender];
                err.Clear();
                erros.Remove(sender);
                setErroStatusBar();
            }
        }
        #endregion
    }
}