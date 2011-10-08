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
        private long czona = -1, citem = -1;

        private Dictionary<long, Dictionary<long, RichTextBox>> obs;


        public static void main(long cod_projecto, long cod_analise, String nome_analise, Camada_de_Dados.Classes.Relatorio relatorio)
        {
            Interface_Relatorio i = new Interface_Relatorio(cod_projecto, cod_analise, nome_analise, relatorio);
        }

        public Interface_Relatorio(long cod_projecto, long cod_analise, String nome_analise, Camada_de_Dados.Classes.Relatorio relatorio)
        {

            this.zonas = ETdA.Camada_de_Dados.ETdA.ETdA.getProjecto(cod_projecto).Analises[cod_analise].Zonas;
            this.itens = ETdA.Camada_de_Dados.ETdA.ETdA.getProjecto(cod_projecto).Analises[cod_analise].Itens;
            relatorio.gerarResultadosRelatorio(cod_analise, new List<Camada_de_Dados.Classes.Resposta>(), zonas, itens);
            relatorio.gerarEstatisticasRelatorioSexo(cod_analise);
            relatorio.gerarEstatisticasIdade(cod_analise);
            relatorio.gerarEstatisticasClienteHabitual(cod_analise);

            this.cod_projecto = cod_projecto;
            this.cod_analise = cod_analise;
            this.nome_analise = ETdA.Camada_de_Dados.ETdA.ETdA.getProjecto(cod_projecto).Analises[cod_analise].Nome;
            this.relatorio = relatorio;

            if (relatorio.NumeroRespostas == 0)
            {
                if (MessageBox.Show("Ainda não existem respostas. Tem a certeza que pretende continuar?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    InitializeComponent();
                    showRelatorio();
                    this.ShowDialog();
                }
            }
            else
            {
                InitializeComponent();
                showRelatorio();
                this.ShowDialog();
            }
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
                czona = zonas[i].Codigo;

                int j;
                for (j = 0; j < itens.Count() && !itens[j].NomeItem.Equals(e.Node.Text); j++) ;
                citem = itens[j].CodigoItem;

                // Console.WriteLine("zona: " + e.Node.Parent.Text + " item: " + e.Node.Text);
                // Console.WriteLine("zona: " + zonas[i].Nome + " item: " + itens[j].NomeItem);
                // Console.WriteLine("codzona: " + czona + "coditem: " + citem);

                #region Titulo Zona
                Label Zona = new Label();
                Zona.Location = new System.Drawing.Point(12, 6);
                Zona.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular,
                    System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                Zona.Dock = DockStyle.Top;
                Zona.TabIndex = 1;
                //nome da zona
                Zona.Text = zonas[i].Nome;
                #endregion

                #region Titulo Item
                Label Item = new Label();
                Item.Location = new System.Drawing.Point(17, 30);
                Item.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular,
                    System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                Item.Dock = DockStyle.Top;
                Item.TabIndex = 2;
                //nome do item
                Item.Text = itens[j].NomeItem;
                #endregion

                #region desenhar o texto "Resultado"
                Label labelres = new Label();
                labelres.Text = "Resultado";
                labelres.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular,
                    System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                labelres.TabIndex = 3;
                labelres.Location = new System.Drawing.Point(0, 55);
                #endregion

                #region desenhar o texto da imagem
                Label labeltxtimg = new Label();
                labeltxtimg.TabIndex = 4;
                labeltxtimg.Location = new System.Drawing.Point(50, 155);
                labeltxtimg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold,
                    System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                labeltxtimg.Size = new System.Drawing.Size(400, 25);
                #endregion

                #region desenhar a imagem da cor
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
                #endregion

                #region desenhar texto "Resultado Detalhado
                Label labelresdet = new Label();
                labelresdet.Text = "Resultado Detalhado";
                labelresdet.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular,
                    System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                labelresdet.TabIndex = 5;
                labelresdet.Location = new System.Drawing.Point(0, 200);
                labelresdet.Size = new System.Drawing.Size(200, 25);
                #endregion

                #region desenhar tres labels uma para cada dimensão
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
                labeldmn.Location = new System.Drawing.Point(10, 230 + 25);
                labeldmn.Size = new System.Drawing.Size(100, 75);
                #endregion

                #region desenhar label as ponderações de cada dimensão
                Label labelpnd = new Label();
                labelpnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular,
                    System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                labelpnd.TabIndex = 6;
                labelpnd.Location = new System.Drawing.Point(190, 230 + 25);
                labelpnd.Size = new System.Drawing.Size(50, 75);
                labelpnd.Text = itens[j].PonderacaoCliente.ToString() + "\n\n" + itens[j].PonderacaoProfissional.ToString() + "\n\n" + itens[j].PonderacaoAnalista.ToString();
                #endregion

                #region desenhar labels de resultados parciais
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
                    labelcl.ForeColor = Color.Green;
                }
                #endregion

                #region Profissional
                Label labelpf = new Label();
                labelpf.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold,
                    System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                labelpf.TabIndex = 7;
                labelpf.Location = new System.Drawing.Point(350, 230 + 55);
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

                #region Analista
                Label labelan = new Label();
                labelan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold,
                    System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                labelan.TabIndex = 7;
                labelan.Location = new System.Drawing.Point(350, 230 + 87);
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
                #endregion

                #region [FALTA] desenhar label do titulo dos limites
                /* Label labeltitlim = new Label();
                labeltitlim.Text = "Limites das cores";
                labeltitlim.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular,
                    System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                labeltitlim.TabIndex = 10;
                labeltitlim.Location = new System.Drawing.Point(0, 200);
                labeltitlim.Size = new System.Drawing.Size(200, 25);*/
                #endregion

                #region [FALTA] Desenhar os limites de cada cor

                #endregion

                #region desenhar checkbox para incluir(ou nao) no relatorio o resultado detalhado
                //verificar se já foi preenchido se foi, por o que já foi preenchido
                checkBoxInsDt.Location = new System.Drawing.Point(10, 245 + 105);
                checkBoxInsDt.Size = new System.Drawing.Size(300, 30);
                checkBoxInsDt.TabIndex = 3;
                checkBoxInsDt.TabStop = true;
                if(relatorio.ListaResultados[czona][citem].mostraResultadosParciais)
                    checkBoxInsDt.Checked = true;
                else
                    checkBoxInsDt.Checked = false;
                #endregion

                #region desenhar "label" de observações
                Label obslabel = new System.Windows.Forms.Label();
                obslabel.AutoSize = true;
                obslabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular,
                    System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                obslabel.Location = new System.Drawing.Point(0, 290 + 105);
                obslabel.Size = new System.Drawing.Size(50, 20);
                obslabel.Name = "label2";
                obslabel.Text = "Observações";
                #endregion

                #region desenhar caixa de observações
                //verificar se já foi preenchido se foi, por o que já foi preenchido
                obstb.Location = new System.Drawing.Point(10, 335 + 105);
                obstb.Name = "obstb";
                obstb.Size = new System.Drawing.Size(500, 100);
                obstb.Margin = new System.Windows.Forms.Padding(0, 0, 0, 60);
                obstb.TabIndex = 4;
                obstb.TabStop = true;
                if(relatorio.ListaResultados[czona][citem].CheckObservacoes)
                    obstb.Text = relatorio.ListaResultados[czona][citem].Observacao;
                else
                    obstb.Text = "";

                #endregion

                #region Adicionar Controlos ao painel
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
                #endregion

            }
        }

        private void checkBoxInsDt_CheckedChanged(object sender, EventArgs e)
        {
            relatorio.ListaResultados[czona][citem].mostraResultadosParciais = checkBoxInsDt.Checked;
        }

        private void obstb_TextChanged(object sender, EventArgs e)
        {
            relatorio.ListaResultados[czona][citem].Observacao = obstb.Text;
            if (obstb.Text == "")
                relatorio.ListaResultados[czona][citem].CheckObservacoes = false;
            else
                relatorio.ListaResultados[czona][citem].CheckObservacoes = true;
        }

        private void BotaoCancelar_Click(object sender, EventArgs e)
        {
            Interface_Relatorio.ActiveForm.Close();
        }

        //quando carrega no buttao de gerar relatorio
        private void button1_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            //mostrar a barra de estados
            ZonaActividadelabel.Visible = false;
            ZonaActividadetextlabel.Visible = false;
            Itemtextlabel.Visible = false;
            Itemlabel.Text = "A preparar relatório...";
            Itemlabel.Visible = true;
            statusStrip1.Visible = true;

            object oMissing = System.Reflection.Missing.Value;
            object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */

            //Start Word and create a new document.
            Word._Application oWord;
            Word._Document oDoc;
            oWord = new Word.Application();
            oWord.Visible = false;
            oDoc = oWord.Documents.Add(ref oMissing, ref oMissing,
                ref oMissing, ref oMissing);

            #region Página principal


            #endregion

            #region Indice
            
            //SETTING THE FORMAT TYPE
            //SELECT THE CONTENST TO BE FORMATTED AND SET THE VALUE

            Object styleHeading2 = "Heading 2";
            Object styleHeading3 = "Heading 3";

            //oWord.Selection.Range.set_Style(ref styleHeading2);
            //oWord.Selection.Range.set_Style(ref styleHeading3);

            //SETTING THE OUTLINE LEVEL
            //SELECT THE CONTENTS WHOSE OUTLINE LEVEL NEEDS TO BE CHANGED AND
            //SET THE VALUE

            //oWord.Selection.Paragraphs.OutlineLevel = Word.WdOutlineLevel.wdOutlineLevel2;
            //oWord.Selection.Paragraphs.OutlineLevel = Word.WdOutlineLevel.wdOutlineLevel3;
            oWord.Selection.Paragraphs.OutlineLevel = Word.WdOutlineLevel.wdOutlineLevelBodyText;

            Object trueo = true;
            // INCLUDING THE TABLE OF CONTENTS
            Object oUpperHeadingLevel = "1";
            Object oLowerHeadingLevel = "3";
            Object oTOCTableID = "Indice";
            oDoc.TablesOfContents.Add(oWord.Selection.Range, ref trueo, ref oUpperHeadingLevel,
                ref oLowerHeadingLevel, ref oMissing, ref oTOCTableID, ref trueo,
                ref trueo, ref oMissing, ref trueo, ref trueo, ref trueo);
            oWord.Selection.InsertNewPage();
            #endregion

            #region Numeração das Páginas

            oWord.ActiveWindow.ActivePane.View.SeekView = Word.WdSeekView.wdSeekCurrentPageFooter;

            //ENTERING A PARAGRAPH BREAK "ENTER"
            oWord.Selection.TypeParagraph();

            //INSERTING THE PAGE NUMBERS CENTRALLY ALIGNED IN THE PAGE FOOTER
            oWord.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            oWord.ActiveWindow.Selection.Font.Name = "Arial";
            oWord.ActiveWindow.Selection.Font.Size = 8;
            oWord.ActiveWindow.Selection.TypeText("ETdAnalyser - Documentos");

            //INSERTING TAB CHARACTERS
            oWord.ActiveWindow.Selection.TypeText("\t");
            oWord.ActiveWindow.Selection.TypeText("\t");

            oWord.ActiveWindow.Selection.TypeText("Página ");
            Object CurrentPage = Word.WdFieldType.wdFieldPage;
            oWord.ActiveWindow.Selection.Fields.Add(oWord.Selection.Range, ref CurrentPage, ref oMissing, ref oMissing);
            oWord.ActiveWindow.Selection.TypeText(" de ");
            Object TotalPages = Word.WdFieldType.wdFieldNumPages;
            oWord.ActiveWindow.Selection.Fields.Add(oWord.Selection.Range, ref TotalPages, ref oMissing, ref oMissing);

            //SETTING FOCUES BACK TO DOCUMENT
            oWord.ActiveWindow.ActivePane.View.SeekView = Word.WdSeekView.wdSeekMainDocument;

            #endregion

            #region Estatísticas

            

            //Título
            Word.Paragraph oPara1;
            oPara1 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oPara1.Range.Text = "Relatório " + this.nome_analise;
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

            //Tabela Sexo

	        Word.Table oTable;
	        Word.Range wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
	        oTable = oDoc.Tables.Add(wrdRng, 2, 2, ref oMissing, ref oMissing);
	        oTable.Range.ParagraphFormat.SpaceAfter = 6;
	        oTable.Cell(1,1).Range.Text = "Feminino";
            oTable.Cell(1,2).Range.Text = "Masculino";
            oTable.Cell(2, 1).Range.Text = relatorio.ListaEstatisticas[0] + "%";
            oTable.Cell(2, 2).Range.Text = relatorio.ListaEstatisticas[1] + "%";

	        oTable.Rows[1].Range.Font.Bold = 1;
	        oTable.Rows[1].Range.Font.Italic = 1;

            //Add text after the chart.
            wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            wrdRng.InsertParagraphAfter();
            wrdRng.InsertAfter("Tabela Idades");


            //Tabela Idades

            Word.Table oTable1;
            wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oTable1 = oDoc.Tables.Add(wrdRng, 7, 2, ref oMissing, ref oMissing);
            oTable1.Range.ParagraphFormat.SpaceAfter = 6;
            oTable1.Cell(1, 1).Range.Text = "Intervalo de Idades";
            oTable1.Cell(1, 2).Range.Text = "Total por Intervalo";
            oTable1.Cell(2, 1).Range.Text = "Menores de 20";
            oTable1.Cell(2, 2).Range.Text = relatorio.ListaEstatisticas[2] + "";
            oTable1.Cell(3, 1).Range.Text = "Entre 20 e 30";
            oTable1.Cell(3, 2).Range.Text = relatorio.ListaEstatisticas[3] + "";
            oTable1.Cell(4, 1).Range.Text = "Entre 30 e 40";
            oTable1.Cell(4, 2).Range.Text = relatorio.ListaEstatisticas[4] + "";
            oTable1.Cell(5, 1).Range.Text = "Entre 40 e 50";
            oTable1.Cell(5, 2).Range.Text = relatorio.ListaEstatisticas[5] + "";
            oTable1.Cell(6, 1).Range.Text = "Entre 50 e 60";
            oTable1.Cell(6, 2).Range.Text = relatorio.ListaEstatisticas[6] + "";
            oTable1.Cell(7, 1).Range.Text = "Maiores de 60";
            oTable1.Cell(7, 2).Range.Text = relatorio.ListaEstatisticas[7] + "";

            oTable1.Rows[1].Range.Font.Bold = 1;
            oTable1.Rows[1].Range.Font.Italic = 1;

            //Add text after the chart.
            wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            wrdRng.InsertParagraphAfter();
            wrdRng.InsertAfter("Tabela Clientes Habituais");

            //Tabela Clientes Habituais
            Word.Table oTable2;
            wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oTable2 = oDoc.Tables.Add(wrdRng, 2, 2, ref oMissing, ref oMissing);
            oTable2.Range.ParagraphFormat.SpaceAfter = 6;
            oTable2.Cell(1, 1).Range.Text = "Clientes Habituais";
            oTable2.Cell(1, 2).Range.Text = "Clientes Ocasionais";
            oTable2.Cell(2, 1).Range.Text = relatorio.ListaEstatisticas[8] + "%";
            oTable2.Cell(2, 2).Range.Text = relatorio.ListaEstatisticas[9] + "%";

            oTable2.Rows[1].Range.Font.Bold = 1;
            oTable2.Rows[1].Range.Font.Italic = 1;
                              
            //Add text after the chart.
            wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            wrdRng.InsertParagraphAfter();

            for(int w=0; w<21; w++)
                oWord.Selection.MoveDown();

            
            #endregion

            #region Resultados
            for (int i = 0; i < zonas.Count; i++)
            {

                oWord.Selection.ClearFormatting();
                //Zonas
                oWord.ActiveWindow.Selection.Font.Name = "Calibri (Body)";
                oWord.ActiveWindow.Selection.Font.Size = 16;
                oWord.ActiveWindow.Selection.TypeText((i + 1) + ". " + zonas[i].Nome);
                oWord.Selection.TypeParagraph();
                oWord.Selection.ClearFormatting();
                //zona a gerar - aparece na barra
                ZonaActividadelabel.Text = zonas[i].Nome;
                for (int j = 0; j < itens.Count; j++)
                {
                    //barra de progresso
                    GerarDocumentoBar.Value = (int)(((double)((i + 1) * (j + 1)) / (double) (zonas.Count * itens.Count)) * (double)100);
                    //item a gerar - aparece na barra
                    Itemlabel.Text = itens[j].NomeItem;
                    if (ZonaActividadelabel.Visible == false)
                    {
                        ZonaActividadelabel.Visible = true;
                        ZonaActividadetextlabel.Visible = true;
                        Itemlabel.Visible = true;
                        Itemtextlabel.Visible = true;
                    }
                    //Itens
                    oWord.ActiveWindow.Selection.Font.Name = "Calibri (Body)";
                    oWord.ActiveWindow.Selection.Font.Size = 14;
                    oWord.ActiveWindow.Selection.TypeText((i + 1) + "." + (j + 1) + ". " + itens[j].NomeItem);
                    oWord.Selection.TypeParagraph();

                    #region Resultado Geral
                    oWord.ActiveWindow.Selection.Font.Name = "Calibri (Body)";
                    oWord.ActiveWindow.Selection.Font.Size = 12;
                    oWord.ActiveWindow.Selection.Font.Bold = 1;
                    oWord.ActiveWindow.Selection.TypeText("Resultado");
                    oWord.ActiveWindow.Selection.Font.Bold = 0;
                    oWord.Selection.TypeParagraph();

                    #region Cor Resultado
                    oWord.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    if (relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ResultadoFinal <= itens[j].Inter_Vermelho)
                    {
                        Clipboard.SetImage(global::ETdA.Properties.Resources.vermelhoWord);
                        oWord.Selection.Paste();
                        oWord.Selection.TypeParagraph();
                        oWord.ActiveWindow.Selection.Font.Name = "Calibri (Body)";
                        oWord.ActiveWindow.Selection.Font.Size = 12;
                        oWord.ActiveWindow.Selection.Font.Bold = 1;
                        oWord.ActiveWindow.Selection.TypeText("Cor: ");
                        oWord.ActiveWindow.Selection.Font.Bold = 0;
                        oWord.ActiveWindow.Selection.TypeText("Vermelho    ");
                        oWord.ActiveWindow.Selection.Font.Bold = 1;
                        oWord.ActiveWindow.Selection.TypeText("Resultado: ");
                        oWord.ActiveWindow.Selection.Font.Bold = 0;
                        oWord.ActiveWindow.Selection.TypeText(relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ResultadoFinal.ToString());
                        oWord.Selection.TypeParagraph();
                    }
                    else if (relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ResultadoFinal <= itens[j].Inter_Laranja)
                    {
                        Clipboard.SetImage(global::ETdA.Properties.Resources.laranjaWord);
                        oWord.Selection.Paste();
                        oWord.Selection.TypeParagraph();
                        oWord.ActiveWindow.Selection.Font.Name = "Calibri (Body)";
                        oWord.ActiveWindow.Selection.Font.Size = 12;
                        oWord.ActiveWindow.Selection.Font.Bold = 1;
                        oWord.ActiveWindow.Selection.TypeText("Cor: ");
                        oWord.ActiveWindow.Selection.Font.Bold = 0;
                        oWord.ActiveWindow.Selection.TypeText("Laranja    ");
                        oWord.ActiveWindow.Selection.Font.Bold = 1;
                        oWord.ActiveWindow.Selection.TypeText("Resultado: ");
                        oWord.ActiveWindow.Selection.Font.Bold = 0;
                        oWord.ActiveWindow.Selection.TypeText(relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ResultadoFinal.ToString());
                        oWord.Selection.TypeParagraph();
                    }
                    else if (relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ResultadoFinal <= itens[j].Inter_Amarelo)
                    {
                        Clipboard.SetImage(global::ETdA.Properties.Resources.amareloWord);
                        oWord.Selection.Paste();
                        oWord.Selection.TypeParagraph();
                        oWord.ActiveWindow.Selection.Font.Name = "Calibri (Body)";
                        oWord.ActiveWindow.Selection.Font.Size = 12;
                        oWord.ActiveWindow.Selection.Font.Bold = 1;
                        oWord.ActiveWindow.Selection.TypeText("Cor: ");
                        oWord.ActiveWindow.Selection.Font.Bold = 0;
                        oWord.ActiveWindow.Selection.TypeText("Amarelo    ");
                        oWord.ActiveWindow.Selection.Font.Bold = 1;
                        oWord.ActiveWindow.Selection.TypeText("Resultado: ");
                        oWord.ActiveWindow.Selection.Font.Bold = 0;
                        oWord.ActiveWindow.Selection.TypeText(relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ResultadoFinal.ToString());
                        oWord.Selection.TypeParagraph();
                    }
                    else if (relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ResultadoFinal <= itens[j].Inter_Verde_Lima)
                    {
                        Clipboard.SetImage(global::ETdA.Properties.Resources.limaWord);
                        oWord.Selection.Paste();
                        oWord.Selection.TypeParagraph();
                        oWord.ActiveWindow.Selection.Font.Name = "Calibri (Body)";
                        oWord.ActiveWindow.Selection.Font.Size = 12;
                        oWord.ActiveWindow.Selection.Font.Bold = 1;
                        oWord.ActiveWindow.Selection.TypeText("Cor: ");
                        oWord.ActiveWindow.Selection.Font.Bold = 0;
                        oWord.ActiveWindow.Selection.TypeText("Verde-Lima    ");
                        oWord.ActiveWindow.Selection.Font.Bold = 1;
                        oWord.ActiveWindow.Selection.TypeText("Resultado: ");
                        oWord.ActiveWindow.Selection.Font.Bold = 0;
                        oWord.ActiveWindow.Selection.TypeText(relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ResultadoFinal.ToString());
                        oWord.Selection.TypeParagraph();
                    }
                    else if (relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ResultadoFinal <= itens[j].Inter_Verde)
                    {
                        Clipboard.SetImage(global::ETdA.Properties.Resources.verdeWord);
                        oWord.Selection.Paste();
                        oWord.Selection.TypeParagraph();
                        oWord.ActiveWindow.Selection.Font.Name = "Calibri (Body)";
                        oWord.ActiveWindow.Selection.Font.Size = 12;
                        oWord.ActiveWindow.Selection.Font.Bold = 1;
                        oWord.ActiveWindow.Selection.TypeText("Cor: ");
                        oWord.ActiveWindow.Selection.Font.Bold = 0;
                        oWord.ActiveWindow.Selection.TypeText("Verde    ");
                        oWord.ActiveWindow.Selection.Font.Bold = 1;
                        oWord.ActiveWindow.Selection.TypeText("Resultado: ");
                        oWord.ActiveWindow.Selection.Font.Bold = 0;
                        oWord.ActiveWindow.Selection.TypeText(relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ResultadoFinal.ToString());
                        oWord.Selection.TypeParagraph();
                        oWord.Selection.TypeParagraph();
                    }
                    #endregion

                    //linhar texto à esquerda
                    oWord.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    #endregion
                    
                    #region Resultado Detalhado
                    if (relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].mostraResultadosParciais)
                    {
                        oWord.ActiveWindow.Selection.Font.Name = "Calibri (Body)";
                        oWord.ActiveWindow.Selection.Font.Size = 14;
                        oWord.ActiveWindow.Selection.TypeText("Resultado Detalhado");
                        oWord.Selection.TypeParagraph();

                        
                        Word.Table rdTabela = oWord.Selection.Tables.Add(oWord.Selection.Range, 4, 4, Microsoft.Office.Interop.Word.WdDefaultTableBehavior.wdWord8TableBehavior, Microsoft.Office.Interop.Word.WdAutoFitBehavior.wdAutoFitFixed);
                        rdTabela.Range.Font.Size = 12;
                        rdTabela.Rows[1].Range.Font.Bold = 1;
                        rdTabela.Rows.Alignment = Word.WdRowAlignment.wdAlignRowCenter;

                        rdTabela.Range.Cells.Borders.Enable = 1;
                        rdTabela.Range.Cells.Borders.InsideColor = Word.WdColor.wdColorBlack;
                        rdTabela.Range.Cells.Borders.OutsideColor = Word.WdColor.wdColorBlack;

                        rdTabela.Range.Cells.Height = 13;

                        rdTabela.Cell(1, 1).Range.Text = "Dimensão";
                        rdTabela.Cell(1, 2).Range.Text = "Ponderação";
                        rdTabela.Cell(1, 3).Range.Text = "Resultado Parcial";
                        rdTabela.Cell(1, 4).Range.Text = "Cor";

                        #region dimensões
                        rdTabela.Cell(2, 1).Range.Text = "Cliente";
                        rdTabela.Cell(3, 1).Range.Text = "Profissional";
                        rdTabela.Cell(4, 1).Range.Text = "Analista";
                        #endregion

                        #region Ponderações
                        rdTabela.Cell(2, 2).Range.Text = itens[j].PonderacaoCliente.ToString();
                        rdTabela.Cell(3, 2).Range.Text = itens[j].PonderacaoProfissional.ToString();
                        rdTabela.Cell(4, 2).Range.Text = itens[j].PonderacaoAnalista.ToString();

                        #endregion

                        #region Resultados Parciais
                        rdTabela.Cell(2, 3).Range.Text = relatorio.ListaResultados[czona][citem].ResultadoQuestionarioGeral.ToString();
                        rdTabela.Cell(3, 3).Range.Text = relatorio.ListaResultados[czona][citem].ResultadoFichaAvaliacaoGeral.ToString();
                        rdTabela.Cell(4, 3).Range.Text = relatorio.ListaResultados[czona][citem].ResultadoCheckListGeral.ToString();
                        #endregion

                        #region Cores
                        #region Cliente

                        if (relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ResultadoQuestionarioGeral <= itens[j].Inter_Vermelho)
                    {
                        rdTabela.Cell(2, 4).Range.Font.Color = Word.WdColor.wdColorRed;
                        rdTabela.Cell(2, 4).Range.Text = "Vermelho";
                    }
                        else if (relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ResultadoQuestionarioGeral <= itens[j].Inter_Laranja)
                    {
                        rdTabela.Cell(2, 4).Range.Font.Color = Word.WdColor.wdColorOrange;
                        rdTabela.Cell(2, 4).Range.Text = "Laranja";
                    }
                        else if (relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ResultadoQuestionarioGeral <= itens[j].Inter_Amarelo)
                    {
                        rdTabela.Cell(2, 4).Range.Font.Color = Word.WdColor.wdColorYellow;
                        rdTabela.Cell(2, 4).Range.Text = "Amarelo";
                    }
                        else if (relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ResultadoQuestionarioGeral <= itens[j].Inter_Verde_Lima)
                    {
                        rdTabela.Cell(2, 4).Range.Font.Color = Word.WdColor.wdColorLime;
                        rdTabela.Cell(2, 4).Range.Text = "Verde Lima";
                    }
                        else if (relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ResultadoQuestionarioGeral <= itens[j].Inter_Verde)
                    {
                        rdTabela.Cell(2, 4).Range.Font.Color = Word.WdColor.wdColorGreen;
                        rdTabela.Cell(2, 4).Range.Text = "Verdemelho";
                    }
                        #endregion

                        #region Profissional
                        if (relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ResultadoFichaAvaliacaoGeral <= itens[j].Inter_Vermelho)
                        {
                            rdTabela.Cell(3, 4).Range.Font.Color = Word.WdColor.wdColorRed;
                            rdTabela.Cell(3, 4).Range.Text = "Vermelho";
                        }
                        else if (relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ResultadoFichaAvaliacaoGeral <= itens[j].Inter_Laranja)
                        {
                            rdTabela.Cell(3, 4).Range.Font.Color = Word.WdColor.wdColorOrange;
                            rdTabela.Cell(3, 4).Range.Text = "Laranja";
                        }
                        else if (relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ResultadoFichaAvaliacaoGeral <= itens[j].Inter_Amarelo)
                        {
                            rdTabela.Cell(3, 4).Range.Font.Color = Word.WdColor.wdColorYellow;
                            rdTabela.Cell(3, 4).Range.Text = "Amarelo";
                        }
                        else if (relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ResultadoFichaAvaliacaoGeral <= itens[j].Inter_Verde_Lima)
                        {
                            rdTabela.Cell(3, 4).Range.Font.Color = Word.WdColor.wdColorLime;
                            rdTabela.Cell(3, 4).Range.Text = "Verde Lima";
                        }
                        else if (relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ResultadoFichaAvaliacaoGeral <= itens[j].Inter_Verde)
                        {
                            rdTabela.Cell(3, 4).Range.Font.Color = Word.WdColor.wdColorGreen;
                            rdTabela.Cell(3, 4).Range.Text = "Verdemelho";
                        }

                        #endregion

                        #region Analista
                        if (relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ResultadoCheckListGeral <= itens[j].Inter_Vermelho)
                        {
                            rdTabela.Cell(4, 4).Range.Font.Color = Word.WdColor.wdColorRed;
                            rdTabela.Cell(4, 4).Range.Text = "Vermelho";
                        }
                        else if (relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ResultadoCheckListGeral <= itens[j].Inter_Laranja)
                        {
                            rdTabela.Cell(4, 4).Range.Font.Color = Word.WdColor.wdColorOrange;
                            rdTabela.Cell(4, 4).Range.Text = "Laranja";
                        }
                        else if (relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ResultadoCheckListGeral <= itens[j].Inter_Amarelo)
                        {
                            rdTabela.Cell(4, 4).Range.Font.Color = Word.WdColor.wdColorYellow;
                            rdTabela.Cell(4, 4).Range.Text = "Amarelo";
                        }
                        else if (relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ResultadoCheckListGeral <= itens[j].Inter_Verde_Lima)
                        {
                            rdTabela.Cell(4, 4).Range.Font.Color = Word.WdColor.wdColorLime;
                            rdTabela.Cell(4, 4).Range.Text = "Verde Lima";
                        }
                        else if (relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ResultadoCheckListGeral <= itens[j].Inter_Verde)
                        {
                            rdTabela.Cell(4, 4).Range.Font.Color = Word.WdColor.wdColorGreen;
                            rdTabela.Cell(4, 4).Range.Text = "Verdemelho";
                        }

                        #endregion

                        #endregion
                        object count = 4;
                        oWord.Selection.MoveDown(ref oMissing, ref count, oMissing);
                        oWord.Selection.ClearFormatting();
                        oWord.Selection.TypeParagraph();
                    }
                    
                    #endregion

                    #region Observações
                    if (relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].CheckObservacoes)
                    {
                        oWord.ActiveWindow.Selection.Font.Name = "Calibri (Body)";
                        oWord.ActiveWindow.Selection.Font.Size = 16;
                        oWord.ActiveWindow.Selection.TypeText("Observações");
                        oWord.Selection.TypeParagraph();
                        oWord.ActiveWindow.Selection.Font.Size = 14;
                        oWord.ActiveWindow.Selection.TypeText(relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].Observacao);

                        oWord.Selection.TypeParagraph();
                    }
                    #endregion

                    oWord.Selection.TypeParagraph();
                    oWord.Selection.ClearFormatting();

                }
                oWord.Selection.TypeParagraph();
            }

            
            //Quebra de Página
            oWord.Selection.InsertNewPage();
            //ENTERING A PARAGRAPH BREAK "ENTER"
            oWord.Selection.TypeParagraph();
            #endregion

            #region Actualizar Indice
            //UPDATING THE TABLE OF CONTENTS
            oDoc.TablesOfContents[1].Update();

            //UPDATING THE TABLE OF CONTENTS
            oDoc.TablesOfContents[1].UpdatePageNumbers();

            #endregion

            oWord.Visible = true;

            //Indicar a conclusão do gerar do relatório
            ZonaActividadelabel.Visible = false;
            ZonaActividadetextlabel.Visible = false;
            Itemlabel.Visible = false;
            Itemtextlabel.Text = "Relatório gerado com sucesso";
            button1.Enabled = false;

            //Close this form.
            //this.Close();
            this.Cursor = Cursors.Default;
            BotaoCancelar.Text = "Fechar";
            
        }
    }
}