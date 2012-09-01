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
    public partial class InterfaceImporter : Form
    {
        private long codigoAnalise;
        private Dictionary<object, ErrorProvider> erros;
        private List<Zona> zonas;
        private List<Item> itens;

        public InterfaceImporter(long _codigoAnalise, object _zonas, object _itens)
        {
            codigoAnalise = _codigoAnalise;
            zonas = (List<Zona>)_zonas;
            itens = (List<Item>)_itens;
            InitializeComponent();
            erros = new Dictionary<object, ErrorProvider>();
            toolStripProgressBar1.Visible = false;
            toolStripStatusLabel1.Visible = false;
            toolStripStatusLabel2.Visible = false;
        }

        public static void main(long _codigoAnalise, object _zonas, object _itens)
        {
            InterfaceImporter i = new InterfaceImporter(_codigoAnalise, _zonas, _itens);
            i.ShowDialog();
        }

        private void importar(object sender, EventArgs e)
        {
            if (verificaErros())
            {
                Importer ie = new Importer(codigoAnalise, textBox1.Text);
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        if (!ie.LerFicheiro(checkBox1.Checked))
                            MessageBoxPortuguese.Show("Erro", ie.Erro, MessageBoxPortuguese.Icon_Error);
                        else
                        {
                            List<PerguntaQuestionario> pqs = GestaodeRespostas.getPerguntasQT(codigoAnalise);
                            List<Pergunta> lst_pqs = new List<Pergunta>();
                            foreach (PerguntaQuestionario p in pqs)
                                lst_pqs.Add(p);
                            InterfaceImporterMatching.main(lst_pqs, ie, Enums.Tipo_Formulário.Questionario, codigoAnalise, zonas, itens);
                        }
                        break;
                    case 1:
                        if (!ie.LerFicheiro(checkBox1.Checked))
                            MessageBoxPortuguese.Show("Erro", ie.Erro, MessageBoxPortuguese.Icon_Error);
                        else
                        {
                            List<PerguntaFichaAvaliacao> pas = GestaodeRespostas.getPerguntasFA(codigoAnalise);
                            List<Pergunta> lst_pas = new List<Pergunta>();
                            foreach (PerguntaFichaAvaliacao p in pas)
                                lst_pas.Add(p);
                            InterfaceImporterMatching.main(lst_pas, ie, Enums.Tipo_Formulário.Ficha_Avaliacao, codigoAnalise, zonas, itens);
                        }
                        break;
                    case 2:
                        if (!ie.LerFicheiro(checkBox1.Checked))
                            MessageBoxPortuguese.Show("Erro", ie.Erro, MessageBoxPortuguese.Icon_Error);
                        else
                        {
                            InterfaceImporterMatching.main(null, ie, Enums.Tipo_Formulário.CheckList, codigoAnalise, zonas, itens);
                        }
                        break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
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
                    err.SetError(textBox1, "É necessário introduzir o ficheiro a importar.");

                    erros.Add(textBox1, err);
                }
                podeAdicionar = false;
            }
            else if (!File.Exists(textBox1.Text))
            {
                if (!erros.Keys.Contains(textBox1))
                {
                    ErrorProvider err = new ErrorProvider();
                    err.Icon = global::ETdAnalyser.Properties.Resources.notification_warning_ico;
                    err.SetError(textBox1, "O ficheiro introduzido não existe.");

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
            else if (comboBox1.SelectedIndex == 0 && !GestaodeRespostas.isQTcreated(codigoAnalise))
            {
                podeAdicionar = false;
                DialogResult resultado = MessageBoxPortuguese.Show("Erro", "É necessário criar o Questionário.\nPretende fazer isso agora?", MessageBoxPortuguese.Button_YesNo, MessageBoxPortuguese.Icon_Question);
                if (resultado == DialogResult.Yes)
                    InterfaceGestaoFormulariosOnline.main(codigoAnalise, itens, zonas);
            }
            else if (comboBox1.SelectedIndex == 1 && !GestaodeRespostas.isFAcreated(codigoAnalise))
            {
                podeAdicionar = false;
                DialogResult resultado = MessageBoxPortuguese.Show("Erro", "É necessário criar a Ficha de Avaliação.\nPretende fazer isso agora?", MessageBoxPortuguese.Button_YesNo, MessageBoxPortuguese.Icon_Question);
                if (resultado == DialogResult.Yes)
                    InterfaceGestaoFormulariosOnline.main(codigoAnalise, itens, zonas);
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