using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;
using System.Reflection;
using System.Windows.Forms.DataVisualization.Charting;

namespace ETdA.Camada_de_Interface
{
    partial class Interface_Relatorio : Form
    {
        private long cod_projecto;
        private long cod_analise;
        private String nome_analise;
        private Camada_de_Dados.Classes.Relatorio relatorio;
        private List<Camada_de_Dados.Classes.Zona> zonas;
        private List<Camada_de_Dados.Classes.Item> itens;

        private Dictionary<long, Dictionary<long, RichTextBox>> obs;


        public static void main(long cod_projecto, long cod_analise, String nome_analise, Camada_de_Dados.Classes.Relatorio relatorio)
        {
            Interface_Relatorio i = new Interface_Relatorio(cod_projecto, cod_analise, nome_analise, relatorio);
            i.ShowDialog();
            //i.Visible = true;
        }

        public Interface_Relatorio(long cod_projecto, long cod_analise, String nome_analise, Camada_de_Dados.Classes.Relatorio relatorio)
        {

            this.zonas = ETdA.Camada_de_Dados.ETdA.ETdA.getProjecto(cod_projecto).Analises[cod_analise].Zonas;
            this.itens = ETdA.Camada_de_Dados.ETdA.ETdA.getProjecto(cod_projecto).Analises[cod_analise].Itens;
            relatorio.gerarResultadosRelatorio(cod_analise, new List<Camada_de_Dados.Classes.Resposta>(), zonas, itens);
            relatorio.gerarEstatisticasRelatorioSexo(cod_analise);
            

            this.cod_projecto = cod_projecto;
            this.cod_analise = cod_analise;
            this.nome_analise = ETdA.Camada_de_Dados.ETdA.ETdA.getProjecto(cod_projecto).Analises[cod_analise].Nome;
            this.relatorio = relatorio;

            InitializeComponent();
            showRelatorio();
        }

        public void showRelatorio()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode();
            treeViewZonaItem.TabStop = true;


            for (int i = 0; i < zonas.Count; i++)
            {
                treeNode1 = new System.Windows.Forms.TreeNode(zonas[i].Nome);
                for (int j = 0; j < itens.Count; j++)
                {
                    treeNode1.Nodes.Add(itens[j].NomeItem);
                }
                treeViewZonaItem.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            }
            
        }

        private void OpenAction(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Level == 1)
            {
                int i;
                for (i = 0; i < zonas.Count() && !zonas[i].Nome.Equals(e.Node.Parent.Text); i++) ;
                long czona = zonas[i].Codigo;

                int j;
                for (j = 0; j < itens.Count() && !itens[j].NomeItem.Equals(e.Node.Text); j++) ;
                long citem = itens[j].CodigoItem;

               // Console.WriteLine("zona: " + e.Node.Parent.Text + " item: " + e.Node.Text);
               // Console.WriteLine("zona: " + zonas[i].Nome + " item: " + itens[j].NomeItem);
               // Console.WriteLine("codzona: " + czona + "coditem: " + citem);

                //Titulo Zona
                Label Zona = new Label();
                Zona.Location = new System.Drawing.Point(12, 6);
                Zona.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular,
                    System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                Zona.Dock = DockStyle.Top;
                Zona.TabIndex = 1;
                //nome da zona
                Zona.Text = zonas[i].Nome;

                //Titulo Item
                Label Item = new Label();
                Item.Location = new System.Drawing.Point(17, 30);
                Item.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular,
                    System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                Item.Dock = DockStyle.Top;
                Item.TabIndex = 2;
                //nome do item
                Item.Text = itens[j].NomeItem;

                //desenhar o texto "Resultado"
                Label labelres = new Label();
                labelres.Text = "Resultado";
                labelres.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular,
                    System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                labelres.TabIndex = 3;
                labelres.Location = new System.Drawing.Point(0, 55);

                //desenhar o texto da imagem
                Label labeltxtimg = new Label();
                labeltxtimg.TabIndex = 4;
                labeltxtimg.Location = new System.Drawing.Point(50, 155);
                labeltxtimg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold,
                    System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                labeltxtimg.Size = new System.Drawing.Size(400, 25);

                //desenhar a imagem da cor
                System.Windows.Forms.PictureBox corItem = new System.Windows.Forms.PictureBox();
                if (relatorio.ListaResultados[czona][citem].ResultadoFinal <= itens[j].Inter_Vermelho)
                {
                    corItem.Image = global::ETdA.Properties.Resources.vermelho;
                    labeltxtimg.ForeColor = Color.Red;
                    labeltxtimg.Text = "VERMELHO (" + relatorio.ListaResultados[czona][citem].ResultadoFinal.ToString() + ")";
                }
                else if (relatorio.ListaResultados[czona][citem].ResultadoFinal <= itens[j].Inter_Laranja)
                {
                    corItem.Image = global::ETdA.Properties.Resources.laranja;
                    labeltxtimg.ForeColor = Color.Orange;
                    labeltxtimg.Text = "LARANJA (" + relatorio.ListaResultados[czona][citem].ResultadoFinal.ToString() + ")";
                }
                else if (relatorio.ListaResultados[czona][citem].ResultadoFinal <= itens[j].Inter_Amarelo)
                {
                    corItem.Image = global::ETdA.Properties.Resources.amarelo;
                    labeltxtimg.ForeColor = Color.Yellow;
                    labeltxtimg.Text = "AMARELO (" + relatorio.ListaResultados[czona][citem].ResultadoFinal.ToString() + ")";
                }
                else if (relatorio.ListaResultados[czona][citem].ResultadoFinal <= itens[j].Inter_Verde_Lima)
                {
                    corItem.Image = global::ETdA.Properties.Resources.lima;
                    labeltxtimg.ForeColor = Color.YellowGreen;
                    labeltxtimg.Text = "VERDE LIMA (" + relatorio.ListaResultados[czona][citem].ResultadoFinal.ToString() + ")";
                }
                else if (relatorio.ListaResultados[czona][citem].ResultadoFinal <= itens[j].Inter_Verde)
                {
                    corItem.Image = global::ETdA.Properties.Resources.verde;
                    labeltxtimg.ForeColor = Color.Green;
                    labeltxtimg.Text = "VERDE (" + relatorio.ListaResultados[czona][citem].ResultadoFinal.ToString() + ")";
                }

                corItem.Location = new System.Drawing.Point(10, 90);
                corItem.Name = "corItem";
                corItem.Size = new System.Drawing.Size(160, 60);
                corItem.TabIndex = 0;
                corItem.TabStop = false;

                //desenhar texto "Resultado Detalhado
                Label labelresdet = new Label();
                labelresdet.Text = "Resultado Detalhado";
                labelresdet.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular,
                    System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                labelresdet.TabIndex = 5;
                labelresdet.Location = new System.Drawing.Point(0, 200);
                labelresdet.Size = new System.Drawing.Size(200, 25);

                //desenhar tres labels uma para cada dimensão
                Label labeltxt = new Label();
                labeltxt.Text = "Dimensão                        Ponderação                        Resultado Parcial";
                labeltxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold,
                    System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                labeltxt.TabIndex = 5;
                labeltxt.Location = new System.Drawing.Point(10, 230);
                labeltxt.Size = new System.Drawing.Size(500, 25);

                Label labeldmn = new Label();
                labeldmn.Text = "Cliente\n\nProfissional\n\nAnalista";
                labeldmn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular,
                    System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                labeldmn.TabIndex = 5;
                labeldmn.Location = new System.Drawing.Point(10, 230+25);
                labeldmn.Size = new System.Drawing.Size(100, 75);

                //desenhar label as ponderações de cada dimensão
                Label labelpnd = new Label();
                labelpnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular,
                    System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                labelpnd.TabIndex = 6;
                labelpnd.Location = new System.Drawing.Point(190, 230 + 25);
                labelpnd.Size = new System.Drawing.Size(50, 75);
                labelpnd.Text = itens[j].PonderacaoCliente.ToString() + "\n\n" + itens[j].PonderacaoProfissional.ToString() + "\n\n" + itens[j].PonderacaoAnalista.ToString();


                //desenhar labels de resultados parciais

                #region Cliente
                Label labelcl = new Label();
                labelcl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold,
                    System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                labelcl.TabIndex = 7;
                labelcl.Location = new System.Drawing.Point(350, 230 + 25);
                labelcl.Size = new System.Drawing.Size(150, 15);

                if (relatorio.ListaResultados[czona][citem].ResultadoQuestionarioGeral <= itens[j].Inter_Vermelho)
                {
                    labelcl.Text = " VERMELHO (" + relatorio.ListaResultados[czona][citem].ResultadoQuestionarioGeral.ToString() + ")";
                    labelcl.ForeColor = Color.Red;
                }
                else if (relatorio.ListaResultados[czona][citem].ResultadoQuestionarioGeral <= itens[j].Inter_Laranja)
                {
                    labelcl.Text = " LARANJA (" + relatorio.ListaResultados[czona][citem].ResultadoQuestionarioGeral.ToString() + ")";
                    labelcl.ForeColor = Color.Orange;
                }
                else if (relatorio.ListaResultados[czona][citem].ResultadoQuestionarioGeral <= itens[j].Inter_Amarelo)
                {
                    labelcl.Text = " AMARELO (" + relatorio.ListaResultados[czona][citem].ResultadoQuestionarioGeral.ToString() + ")";
                    labelcl.ForeColor = Color.Yellow;
                }
                else if (relatorio.ListaResultados[czona][citem].ResultadoQuestionarioGeral <= itens[j].Inter_Verde_Lima)
                {
                    labelcl.Text = " VERDE LIMA (" + relatorio.ListaResultados[czona][citem].ResultadoQuestionarioGeral.ToString() + ")";
                    labelcl.ForeColor = Color.YellowGreen;
                }
                else if (relatorio.ListaResultados[czona][citem].ResultadoQuestionarioGeral <= itens[j].Inter_Verde)
                {
                    labelcl.Text = " VERDE (" + relatorio.ListaResultados[czona][citem].ResultadoQuestionarioGeral.ToString() + ")";
                    labelcl.BackColor = Color.Green;
                }
                #endregion

                #region Analista
                Label labelan = new Label();
                labelan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold,
                    System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                labelan.TabIndex = 7;
                labelan.Location = new System.Drawing.Point(350, 230 + 55);
                labelan.Size = new System.Drawing.Size(150, 15);

                if (relatorio.ListaResultados[czona][citem].ResultadoCheckListGeral <= itens[j].Inter_Vermelho)
                {
                    labelan.Text = " VERMELHO (" + relatorio.ListaResultados[czona][citem].ResultadoCheckListGeral.ToString() + ")";
                    labelan.ForeColor = Color.Red;
                }
                else if (relatorio.ListaResultados[czona][citem].ResultadoCheckListGeral <= itens[j].Inter_Laranja)
                {
                    labelan.Text = " LARANJA (" + relatorio.ListaResultados[czona][citem].ResultadoCheckListGeral.ToString() + ")";
                    labelan.ForeColor = Color.Orange;
                }
                else if (relatorio.ListaResultados[czona][citem].ResultadoCheckListGeral <= itens[j].Inter_Amarelo)
                {
                    labelan.Text = " AMARELO (" + relatorio.ListaResultados[czona][citem].ResultadoCheckListGeral.ToString() + ")";
                    labelan.ForeColor = Color.Yellow;
                }
                else if (relatorio.ListaResultados[czona][citem].ResultadoCheckListGeral <= itens[j].Inter_Verde_Lima)
                {
                    labelan.Text = " VERDE LIMA (" + relatorio.ListaResultados[czona][citem].ResultadoCheckListGeral.ToString() + ")";
                    labelan.ForeColor = Color.YellowGreen;
                }
                else if (relatorio.ListaResultados[czona][citem].ResultadoCheckListGeral <= itens[j].Inter_Verde)
                {
                    labelan.Text = " VERDE (" + relatorio.ListaResultados[czona][citem].ResultadoCheckListGeral.ToString() + ")";
                    labelan.ForeColor = Color.Green;
                }
                #endregion

                #region Profissional
                Label labelpf = new Label();
                labelpf.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold,
                    System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                labelpf.TabIndex = 7;
                labelpf.Location = new System.Drawing.Point(350, 230 + 87);
                labelpf.Size = new System.Drawing.Size(150, 15);

                if (relatorio.ListaResultados[czona][citem].ResultadoFichaAvaliacaoGeral <= itens[j].Inter_Vermelho)
                {
                    labelpf.Text = " VERMELHO (" + relatorio.ListaResultados[czona][citem].ResultadoFichaAvaliacaoGeral.ToString() + ")";
                    labelpf.ForeColor = Color.Red;
                }
                else if (relatorio.ListaResultados[czona][citem].ResultadoFichaAvaliacaoGeral <= itens[j].Inter_Laranja)
                {
                    labelpf.Text = " LARANJA (" + relatorio.ListaResultados[czona][citem].ResultadoFichaAvaliacaoGeral.ToString() + ")";
                    labelpf.ForeColor = Color.Orange;
                }
                else if (relatorio.ListaResultados[czona][citem].ResultadoFichaAvaliacaoGeral <= itens[j].Inter_Amarelo)
                {
                    labelpf.Text = " AMARELO (" + relatorio.ListaResultados[czona][citem].ResultadoFichaAvaliacaoGeral.ToString() + ")";
                    labelpf.ForeColor = Color.Yellow;
                }
                else if (relatorio.ListaResultados[czona][citem].ResultadoFichaAvaliacaoGeral <= itens[j].Inter_Verde_Lima)
                {
                    labelpf.Text = " VERDE LIMA (" + relatorio.ListaResultados[czona][citem].ResultadoFichaAvaliacaoGeral.ToString() + ")";
                    labelpf.ForeColor = Color.YellowGreen;
                }
                else if (relatorio.ListaResultados[czona][citem].ResultadoFichaAvaliacaoGeral <= itens[j].Inter_Verde)
                {
                    labelpf.Text = " VERDE (" + relatorio.ListaResultados[czona][citem].ResultadoFichaAvaliacaoGeral.ToString() + ")";
                    labelpf.ForeColor = Color.Green;
                }
                #endregion

                //desenhar label do titulo dos limites
               /* Label labeltitlim = new Label();
                labeltitlim.Text = "Limites das cores";
                labeltitlim.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular,
                    System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                labeltitlim.TabIndex = 10;
                labeltitlim.Location = new System.Drawing.Point(0, 200);
                labeltitlim.Size = new System.Drawing.Size(200, 25);*/

                //Desenhar os limites de cada cor

                //desenhar checkbox para incluir(ou nao) no relatorio o resultado detalhado
                CheckBox checkBoxInsDt = new CheckBox();
                checkBoxInsDt.Text = "Incluir resultado detalhado no relatório";
                checkBoxInsDt.Location = new System.Drawing.Point(10, 245+105);
                checkBoxInsDt.Size = new System.Drawing.Size(300, 30);
                checkBoxInsDt.TabIndex = 3;
                checkBoxInsDt.TabStop = true;

                //desenhar "label" de observações
                Label obslabel = new System.Windows.Forms.Label();
                obslabel.AutoSize = true;
                obslabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular,
                    System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                obslabel.Location = new System.Drawing.Point(0, 290+105);
                obslabel.Size = new System.Drawing.Size(50, 20);
                obslabel.Name = "label2";
                obslabel.Text = "Observações";

                //desenhar caixa de observações
                RichTextBox obstb = new System.Windows.Forms.RichTextBox();
                obstb.Location = new System.Drawing.Point(10, 335+105);
                obstb.Name = "obstb";
                obstb.Size = new System.Drawing.Size(500, 100);
                obstb.Margin = new Padding(0, 0, 0, 60);
                obstb.TabIndex = 4;
                obstb.TabStop = true;
                obstb.Text = "";
                
                panelrel.Controls.Clear();
                panelrel.Controls.Add(Item);
                panelrel.Controls.Add(Zona);
                panelrel.Controls.Add(labelres);
                panelrel.Controls.Add(labeltxtimg);
                panelrel.Controls.Add(corItem);
                panelrel.Controls.Add(labelresdet);
                panelrel.Controls.Add(labeltxt);
                panelrel.Controls.Add(labeldmn);
                panelrel.Controls.Add(labelpnd);
                panelrel.Controls.Add(labelcl);
                panelrel.Controls.Add(labelpf);
                panelrel.Controls.Add(labelan);
                panelrel.Controls.Add(obstb);
                panelrel.Controls.Add(obslabel);
                panelrel.Controls.Add(checkBoxInsDt);
                
            }
        }

        private void BotaoCancelar_Click(object sender, EventArgs e)
        {
            Interface_Relatorio.ActiveForm.Close();
        }

        private void BotaoGuardar_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter += "Word Document (*.doc)|";
            saveFileDialog1.ShowDialog();
        }

        private void saveFileFialog1_FileOk(object sender, EventArgs e)
        {
            relatorio.Filename = saveFileDialog1.FileName;
            //colocar aqui o codigo para gerar o documento word
        }

        private void button1_Click(object sender, EventArgs e)
        {
            object oMissing = System.Reflection.Missing.Value;
            object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */

            //Start Word and create a new document.
            Word._Application oWord;
            Word._Document oDoc;
            oWord = new Word.Application();
            oWord.Visible = true;
            oDoc = oWord.Documents.Add(ref oMissing, ref oMissing,
                ref oMissing, ref oMissing);

            //Título
            Word.Paragraph oPara1;
            oPara1 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oPara1.Range.Text = "Relatório" + this.nome_analise;
            oPara1.Range.Font.Bold = 1;
            oPara1.Format.SpaceAfter = 24;    //24 pt spacing after paragraph.
            oPara1.Range.InsertParagraphAfter();

            //Subtítulo
            Word.Paragraph oPara2;
            object oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oPara2 = oDoc.Content.Paragraphs.Add(ref oRng);
            oPara2.Range.Text = "Estatísticas Clientes";
            oPara2.Format.SpaceAfter = 6;
            oPara2.Range.InsertParagraphAfter();

            //Insert a 3 x 5 table, fill it with data, and make the first row
	        //bold and italic.
	        Word.Table oTable;
	        Word.Range wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
	        oTable = oDoc.Tables.Add(wrdRng, 2, 2, ref oMissing, ref oMissing);
	        oTable.Range.ParagraphFormat.SpaceAfter = 6;
	        oTable.Cell(1,1).Range.Text = "Feminino";
            oTable.Cell(1,2).Range.Text = "Masculino";
            oTable.Cell(2, 1).Range.Text = relatorio.ListaEstatisticasSexo[0] + "%";
            oTable.Cell(2, 2).Range.Text = relatorio.ListaEstatisticasSexo[1] + "%";

	        oTable.Rows[1].Range.Font.Bold = 1;
	        oTable.Rows[1].Range.Font.Italic = 1;
           
            //Pie Chart Sexo
           
           /* PieChart3D chart1 = new PieChart3D(); 
            chart1.PieChart3D_Load(values);

            object oMissing = System.Reflection.Missing.Value;
            object oEndOfDoc = "\\endofdoc"; 
            Microsoft.Office.Interop.Word.Range wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;

            wrdRng.InlineShapes.AddOLEControl(chart1);
            wrdRng.InlineShapes.AddChart(chart1);
            wrdRng.InlineShapes.AddOLEObject(chart1);

            */

            //Gráfico
            
            Word.InlineShape oShape;
            object oClassType = "MSGraph.Chart.8";
           // Microsoft.Office.Interop.Word.Range wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oShape = wrdRng.InlineShapes.AddOLEObject(ref oClassType, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing);

            //Demonstrate use of late bound oChart and oChartApp objects to
            //manipulate the chart object with MSGraph.
            object oChart;
            object oChartApp;
            oChart = oShape.OLEFormat.Object;
            oChartApp = oChart.GetType().InvokeMember("Application",
                BindingFlags.GetProperty, null, oChart, null);

            //Change the chart type to Line.
            object[] Parameters = new Object[1];
            Parameters[0] = 4; //xlLine = 4
            oChart.GetType().InvokeMember("ChartType", BindingFlags.SetProperty,
                null, oChart, Parameters);

            //Update the chart image and quit MSGraph.
            oChartApp.GetType().InvokeMember("Update",
                BindingFlags.InvokeMethod, null, oChartApp, null);
            oChartApp.GetType().InvokeMember("Quit",
                BindingFlags.InvokeMethod, null, oChartApp, null);
            //... If desired, you can proceed from here using the Microsoft Graph 
            //Object model on the oChart and oChartApp objects to make additional
            //changes to the chart.

            //Set the width of the chart.
            oShape.Width = oWord.InchesToPoints(6.25f);
            oShape.Height = oWord.InchesToPoints(3.57f);

            //Add text after the chart.
            wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            wrdRng.InsertParagraphAfter();
            wrdRng.InsertAfter("THE END.");

            //Close this form.
            this.Close();
        }
    }
}