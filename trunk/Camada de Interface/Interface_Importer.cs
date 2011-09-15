using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ETdA.Camada_de_Interface
{
    public partial class Interface_Importer : Form
    {
        private long cod_analise;

        public Interface_Importer(long _cod_analise)
        {
            cod_analise = _cod_analise;
            InitializeComponent();
        }

        public static void main(long _cod_analise)
        {
            Interface_Importer i = new Interface_Importer(_cod_analise);
            i.ShowDialog();
        }

        private void importarFichaAvaliacao()
        {

            //SqlDataReader items = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData("SELECT cod_item FROM item_analise WHERE cod_analise=" + cod_analise);
            SqlDataReader _zonas = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(
                "SELECT cod_zona FROM zona_analise WHERE cod_analise=" + cod_analise);
            if (!_zonas.HasRows)
            {
                //MessageBox ficheiroVazio = new MessageBox();
            }

            long[] zonas = new long[100];
            int i = 0;

            do
            {
                zonas[i++] = _zonas.GetInt64(0);
            } while (_zonas.NextResult());

            SqlDataReader _last_fa = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(
                "SELECT TOP 1 cod_fichaAvaliacao FROM ficha_avaliacao ORDER BY cod_fichaAvaliacao DESC;");
            long last_fa = _last_fa.GetInt64(0);

            Camada_de_Dados.Classes.Resposta modelo = new Camada_de_Dados.Classes.Resposta(
                cod_analise, 0, 0, last_fa, 0, 0, 0, 0, "", 2, new Camada_de_Dados.Classes.Resposta.TipoResposta());
            //Camada_de_Dados.Classes.Resposta(cod_analise,
            //null,null,null,null,null,null,null,null,2,null);

            List<Camada_de_Dados.Classes.PerguntaFichaAvaliacao> perguntas = new List<Camada_de_Dados.Classes.PerguntaFichaAvaliacao>();
            SqlDataReader _perguntas = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(
                "SELECT ");


            bool temCabecalho = checkBox1.Checked;
            Camada_de_Negócio.ImporterExporter.importarFichaAvaliacao(
                textBox1.Text, temCabecalho, modelo, perguntas, zonas);

        }

        private void importar(object sender, EventArgs e)
        {
            if (verificaErros())
            {

                switch (comboBox1.SelectedItem.ToString())
                {
                    case "Questionario":
                        break;
                    case "Ficha de Avaliação":
                        importarFichaAvaliacao();
                        break;
                    case "Check List":
                        break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "Ficheiros SPSS (*.csv)|*.csv|All Files|*.*";

            if (fd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = fd.FileName;
            }
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
            if (textBox1.Text.Equals(""))
            {
                return false;
            }
            OpenFileDialog fd = new OpenFileDialog();
            if (!fd.CheckFileExists)
            {
                return false;
            }
            if (comboBox1.SelectedIndex == 0)
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}