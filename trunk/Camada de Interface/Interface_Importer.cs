using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Linq;
using System.IO;
using ETdA.Camada_de_Dados.Classes;
using ETdA.Camada_de_Negócio;

namespace ETdA.Camada_de_Interface
{
    public partial class Interface_Importer : Form
    {
        private long cod_analise;
        private Dictionary<object, ErrorProvider> erros;
        private List<Zona> zonas;
        private List<Item> itens;

        public Interface_Importer(long _cod_analise, object _zonas, object _itens)
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
            Interface_Importer i = new Interface_Importer(_cod_analise, _zonas, _itens);
            i.ShowDialog();
        }

        private void importar(object sender, EventArgs e)
        {
            if (verificaErros())
            {
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        Importer_Exporter ie = new Importer_Exporter(cod_analise, textBox1.Text);
                        if (!ie.ler_ficheiro(checkBox1.Checked))
                            MessageBoxPortuguese.Show("Erro", ie.Erro, MessageBoxPortuguese.Icon_Error);
                        else
                        {
                            List<PerguntaQuestionario> ps = GestaodeRespostas.getPerguntasQT(cod_analise);
                            List<Pergunta> lst = new List<Pergunta>();
                            foreach (PerguntaQuestionario p in ps)
                                lst.Add(p);
                            Interface_Importer_Matching.main(lst, ie, 0, cod_analise, zonas, itens);
                        }
                        break;
                    case 1:
                        break;
                    case 2:
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
                    err.Icon = global::ETdA.Properties.Resources.notification_warning_ico;
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
                    err.Icon = global::ETdA.Properties.Resources.notification_warning_ico;
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
                    err.Icon = global::ETdA.Properties.Resources.notification_warning_ico;
                    err.SetError(comboBox1, "É necessário escolher o formulário.");

                    erros.Add(comboBox1, err);
                }
                podeAdicionar = false;
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