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

namespace ETdAnalyser.CamadaInterface
{
    partial class InterfaceRelatorio : Form
    {
        private long codigoProjecto;
        private long codigoAnalise;
        private String nome_analise;
        private CamadaDados.Classes.Relatorio relatorio;
        private List<CamadaDados.Classes.Zona> zonas;
        private List<CamadaDados.Classes.Item> itens;
        private long czona = -1, citem = -1;

        private Dictionary<long, Dictionary<long, RichTextBox>> obs;

        private bool altpnd = false;
        private NumericUpDown nudpndcliente;
        private NumericUpDown nudpndprofissional;
        private NumericUpDown nudpndanalista;
        private Label labeltxtrespnd;
        private PictureBox corItem;
        private Label labeltxtimg;
        private CamadaDados.Classes.Item curitem;


        public static void main(long codigoProjecto, long codigoAnalise, String nome_analise, CamadaDados.Classes.Relatorio relatorio)
        {
            InterfaceRelatorio i = new InterfaceRelatorio(codigoProjecto, codigoAnalise, nome_analise, relatorio);
        }

        public InterfaceRelatorio(long codigoProjecto, long codigoAnalise, String nome_analise, CamadaDados.Classes.Relatorio relatorio)
        {

            this.codigoProjecto = codigoProjecto;
            this.codigoAnalise = codigoAnalise;
            this.nome_analise = ETdAnalyser.CamadaDados.ETdA.ETdA.getProjecto(codigoProjecto).Analises[codigoAnalise].Nome;
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

                // Console.WriteLine("zona: " + escalaResposta.Node.Parent.Text + " item: " + escalaResposta.Node.Text);
                // Console.WriteLine("zona: " + zonas[i].Nome + " item: " + itens[j].NomeItem);
                // Console.WriteLine("codzona: " + czona + "coditem: " + citem);

                int xmargin = 10;

                altpnd = false;

                #region Titulo Zona
                Label Zona = new Label();
                Zona.Location = new System.Drawing.Point(xmargin, 6);
                Zona.Size = new System.Drawing.Size(0, 0);
                Zona.AutoSize = true;
                Zona.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular,
                    System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                Zona.TabIndex = 1;
                Zona.Size = new System.Drawing.Size(0, 0);
                Zona.AutoSize = true;
                //nome da zona
                Zona.Text = zonas[i].Nome;
                #endregion

                #region Titulo Item
                Label Item = new Label();
                Item.Location = new System.Drawing.Point(xmargin, 30);
                Item.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular,
                    System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                Item.TabIndex = 2;
                Item.Size = new System.Drawing.Size(0, 0);
                Item.AutoSize = true;
                //nome do item
                Item.Text = itens[j].NomeItem;
                #endregion

                #region desenhar texto Resultado Parcial
                Label labelresdet = new Label();
                labelresdet.Text = "Resultado Parcial";
                labelresdet.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular,
                    System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                labelresdet.TabIndex = 5;
                labelresdet.Location = new System.Drawing.Point(xmargin, 75);
                labelresdet.Size = new System.Drawing.Size(300, 20);
                labelresdet.ForeColor = Color.Gray;

                #endregion

                #region desenhar colunas escalaResposta as tres labels uma para cada dimensão

                int xcolunadim = 5;
                int xcolunarespar = 155;
                int xcolunacor = 355;

                int ycoluna = 105;

                Label labeltxtdim = new Label();
                labeltxtdim.Text = "Dimensão";
                labeltxtdim.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                labeltxtdim.TabIndex = 5;
                labeltxtdim.Location = new System.Drawing.Point(xmargin + xcolunadim, ycoluna);
                labeltxtdim.Size = new System.Drawing.Size(150, 20);

                Label labeltxtrespar = new Label();
                labeltxtrespar.Text = "Resultado Parcial";
                labeltxtrespar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                labeltxtrespar.TabIndex = 5;
                labeltxtrespar.Location = new System.Drawing.Point(xmargin + xcolunarespar, ycoluna);
                labeltxtrespar.Size = new System.Drawing.Size(150, 20);

                Label labeltxtcor = new Label();
                labeltxtcor.Text = "Cor";
                labeltxtcor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                labeltxtcor.TabIndex = 5;
                labeltxtcor.Location = new System.Drawing.Point(xmargin + xcolunacor, ycoluna);
                labeltxtcor.Size = new System.Drawing.Size(150, 20);
                
                #region Dimensões
                
                Label labeldmncliente = new Label();
                labeldmncliente.Text = "Cliente";
                labeldmncliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular,
                    System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                labeldmncliente.TabIndex = 5;
                labeldmncliente.Location = new System.Drawing.Point(xmargin + xcolunadim + 5, ycoluna + 20);
                labeldmncliente.Size = new System.Drawing.Size(100, 20);
                
                Label labeldmnprofissional = new Label();
                labeldmnprofissional.Text = "Profissional";
                labeldmnprofissional.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular,
                    System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                labeldmnprofissional.TabIndex = 5;
                labeldmnprofissional.Location = new System.Drawing.Point(xmargin + xcolunadim + 5, ycoluna + 45);
                labeldmnprofissional.Size = new System.Drawing.Size(100, 20);

                Label labeldmnanalista = new Label();
                labeldmnanalista.Text = "Analista";
                labeldmnanalista.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular,
                    System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                labeldmnanalista.TabIndex = 5;
                labeldmnanalista.Location = new System.Drawing.Point(xmargin + xcolunadim + 5, ycoluna + 70);
                labeldmnanalista.Size = new System.Drawing.Size(100, 20);
                #endregion

                #endregion

                #region desenhar labels de resultados parciais escalaResposta cores
                #region Cliente
                #region Resultado Parcial
                Label labelclrp = new Label();
                labelclrp.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular,
                    System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                labelclrp.TabIndex = 7;
                labelclrp.Location = new System.Drawing.Point(xmargin + xcolunarespar + 5, ycoluna + 20);
                labelclrp.Size = new System.Drawing.Size(150, 15);

                labelclrp.Text = String.Format("{0:0.##}",  relatorio.ListaResultados[czona][citem].ResultadoQuestionarioGeral);
                
                #endregion
                #region Cor

                Label labelclcor = new Label();
                labelclcor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular,
                    System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                labelclcor.TabIndex = 7;
                labelclcor.Location = new System.Drawing.Point(xmargin + xcolunacor - 5, ycoluna + 20);
                labelclcor.Size = new System.Drawing.Size(150, 15);

                if (relatorio.ListaResultados[czona][citem].ResultadoQuestionarioGeral <= itens[j].IntervaloVermelho)
                {
                    labelclcor.Text = "Vermelho";
                    //labelcl.ForeColor = Color.Red;
                }
                else if (relatorio.ListaResultados[czona][citem].ResultadoQuestionarioGeral <= itens[j].IntervaloLaranja)
                {
                    labelclcor.Text = "Laranja";
                    //labelcl.ForeColor = Color.Orange;
                }
                else if (relatorio.ListaResultados[czona][citem].ResultadoQuestionarioGeral <= itens[j].IntervaloAmarelo)
                {
                    labelclcor.Text = " Amarelo";
                    //labelcl.ForeColor = Color.Yellow;
                }
                else if (relatorio.ListaResultados[czona][citem].ResultadoQuestionarioGeral <= itens[j].IntervaloVerdeLima)
                {
                    labelclcor.Text = "Verde Lima";
                    //labelcl.ForeColor = Color.YellowGreen;
                }
                else if (relatorio.ListaResultados[czona][citem].ResultadoQuestionarioGeral <= itens[j].IntervaloVerde)
                {
                    labelclcor.Text = "Verde";
                    //labelcl.ForeColor = Color.Green;
                }

                #endregion

                #endregion

                #region Profissional
                #region Resultado Parcial
                Label labelpfrp = new Label();
                labelpfrp.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular,
                    System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                labelpfrp.TabIndex = 7;
                labelpfrp.Location = new System.Drawing.Point(xmargin+xcolunarespar+5, ycoluna + 45);
                labelpfrp.Size = new System.Drawing.Size(150, 15);

                labelpfrp.Text = String.Format("{0:0.##}",relatorio.ListaResultados[czona][citem].ResultadoFichaAvaliacaoGeral);
                                 
                #endregion 
                #region Cor
                Label labelpfcor = new Label();
                labelpfcor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular,
                System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                labelpfcor.TabIndex = 7;
                labelpfcor.Location = new System.Drawing.Point(xmargin+xcolunacor - 5, ycoluna + 45);
                labelpfcor.Size = new System.Drawing.Size(150, 15);

                //double x = relatorio.ListaResultados[czona][citem].ResultadoFichaAvaliacaoGeral;
                if (relatorio.ListaResultados[czona][citem].ResultadoFichaAvaliacaoGeral <= itens[j].IntervaloVermelho)
                {
                    labelpfcor.Text = "Vermelho";
                    //labelpfcor.ForeColor = Color.Red;
                }
                else if (relatorio.ListaResultados[czona][citem].ResultadoFichaAvaliacaoGeral <= itens[j].IntervaloLaranja)
                {
                    labelpfcor.Text = "Laranja";
                    //labelpfcor.ForeColor = Color.Orange;
                }
                else if (relatorio.ListaResultados[czona][citem].ResultadoFichaAvaliacaoGeral <= itens[j].IntervaloAmarelo)
                {
                    labelpfcor.Text = "Amarelo";
                    //labelpfcor.ForeColor = Color.Yellow;
                }
                else if (relatorio.ListaResultados[czona][citem].ResultadoFichaAvaliacaoGeral <= itens[j].IntervaloVerdeLima)
                {
                    labelpfcor.Text = "Verde Lima";
                    //labelpfcor.ForeColor = Color.YellowGreen;
                }
                else if (relatorio.ListaResultados[czona][citem].ResultadoFichaAvaliacaoGeral <= itens[j].IntervaloVerde)
                {
                    labelpfcor.Text = "Verde";
                    //labelpfcor.ForeColor = Color.Green;
                }
                #endregion
                #endregion

                #region Analista
                #region Resultado Parcial
                Label labelanrp = new Label();
                labelanrp.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular,
                    System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                labelanrp.TabIndex = 7;
                labelanrp.Location = new System.Drawing.Point(xmargin + xcolunarespar + 5, ycoluna + 70);
                labelanrp.Size = new System.Drawing.Size(150, 15);

                labelanrp.Text = String.Format("{0:0.##}",relatorio.ListaResultados[czona][citem].ResultadoCheckListGeral);
                 
                #endregion
                #region Cor
                Label labelancor = new Label();
                labelancor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular,
                System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                labelancor.TabIndex = 7;
                labelancor.Location = new System.Drawing.Point(xmargin + xcolunacor - 5, ycoluna + 70);
                labelancor.Size = new System.Drawing.Size(150, 15);

                if (relatorio.ListaResultados[czona][citem].ResultadoCheckListGeral <= itens[j].IntervaloVermelho)
                {
                    labelancor.Text = "Vermelho";
                    //labelancor.ForeColor = Color.Red;
                }
                else if (relatorio.ListaResultados[czona][citem].ResultadoCheckListGeral <= itens[j].IntervaloLaranja)
                {
                    labelancor.Text = "Laranja";
                    //labelancor.ForeColor = Color.Orange;
                }
                else if (relatorio.ListaResultados[czona][citem].ResultadoCheckListGeral <= itens[j].IntervaloAmarelo)
                {
                    labelancor.Text = "Amarelo";
                    //labelancor.ForeColor = Color.Yellow;
                }
                else if (relatorio.ListaResultados[czona][citem].ResultadoCheckListGeral <= itens[j].IntervaloVerdeLima)
                {
                    labelancor.Text = "Verde Lima";
                    //labelancor.ForeColor = Color.YellowGreen;
                }
                else if (relatorio.ListaResultados[czona][citem].ResultadoCheckListGeral <= itens[j].IntervaloVerde)
                {
                    labelancor.Text = "Verde";
                    //labelancor.ForeColor = Color.Green;
                }
                #endregion
                #endregion
                #endregion

                #region desenhar checkbox para incluir(ou nao) no relatorio o resultado detalhado
                //verificar se já foi preenchido se foi, por o que já foi preenchido
                checkBoxInsDt.Location = new System.Drawing.Point(xmargin + xcolunadim + 5, 210);
                checkBoxInsDt.Size = new System.Drawing.Size(300, 30);
                checkBoxInsDt.TabIndex = 3;
                checkBoxInsDt.TabStop = true;
                checkBoxInsDt.Text = "Incluir resultado parcial no relatório";
                if (relatorio.ListaResultados[czona][citem].MostrarResultadoParcial)
                    checkBoxInsDt.Checked = true;
                else
                    checkBoxInsDt.Checked = false;
                #endregion

                #region desenhar o texto "Resultado Ponderado"
                Label labelres = new Label();
                labelres.Text = "Resultado Ponderado";
                labelres.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular,
                    System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                labelres.TabIndex = 3;
                labelres.Location = new System.Drawing.Point(xmargin, 250);
                labelres.Size = new System.Drawing.Size(300, 20);
                //labelres.ForeColor = Color.Gray;
                #endregion
            
                #region desenhar label 'Ponderações'
                int yrp = labelres.Location.Y + 10;

                Label labeltxtpnd = new Label();
                labeltxtpnd.Text = "Ponderações";
                labeltxtpnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular,
                System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                labeltxtpnd.TabIndex = 5;
                labeltxtpnd.Location = new System.Drawing.Point(xmargin + xcolunadim, yrp + 20);
                labeltxtpnd.Size = new System.Drawing.Size(150, 20);
                labeltxtpnd.ForeColor = Color.DarkSlateGray;
                #endregion

                #region desenhar label as ponderações de cada dimensão
                
                Label labeltxtpndcliente = new Label();
                labeltxtpndcliente.Text = "Cliente";
                labeltxtpndcliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                labeltxtpndcliente.TabIndex = 5;
                labeltxtpndcliente.Location = new System.Drawing.Point(xmargin + xcolunadim, yrp+40);
                labeltxtpndcliente.Size = new System.Drawing.Size(150, 20);

                Label labeltxtpndprofissional = new Label();
                labeltxtpndprofissional.Text = "Profissional";
                labeltxtpndprofissional.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                labeltxtpndprofissional.TabIndex = 5;
                labeltxtpndprofissional.Location = new System.Drawing.Point(xmargin + xcolunarespar, yrp + 40);
                labeltxtpndprofissional.Size = new System.Drawing.Size(150, 20);

                Label labeltxtpndanalista = new Label();
                labeltxtpndanalista.Text = "Analista";
                labeltxtpndanalista.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                labeltxtpndanalista.TabIndex = 5;
                labeltxtpndanalista.Location = new System.Drawing.Point(xmargin + xcolunacor, yrp + 40);
                labeltxtpndanalista.Size = new System.Drawing.Size(150, 20);
                #endregion

                #region desenhar os valores das ponderações de cada dimensão

                nudpndcliente = new System.Windows.Forms.NumericUpDown();
                nudpndcliente.Increment = new Decimal(0.1);
                nudpndcliente.Maximum = 1;
                nudpndcliente.DecimalPlaces = 3;
                nudpndcliente.Location = new System.Drawing.Point(xmargin + xcolunadim, yrp + 60);
                if (altpnd == false)
                {
                    nudpndcliente.Value = new decimal(itens[j].PonderacaoCliente);
                    nudpndcliente.Enabled = false;
                }
                else
                    nudpndcliente.Enabled = true;

                nudpndprofissional = new System.Windows.Forms.NumericUpDown();
                nudpndprofissional.Increment = new Decimal(0.1);
                nudpndprofissional.Maximum = 1;
                nudpndprofissional.DecimalPlaces = 3;
                nudpndprofissional.Location = new System.Drawing.Point(xmargin + xcolunarespar, yrp + 60);
                if (altpnd == false)
                {
                    nudpndprofissional.Value = new decimal(itens[j].PonderacaoProfissional);
                    nudpndprofissional.Enabled = false;
                }
                else
                    nudpndprofissional.Enabled = true;

                nudpndanalista = new System.Windows.Forms.NumericUpDown();
                nudpndanalista.Increment = new Decimal(0.1);
                nudpndanalista.Maximum = 1;
                nudpndanalista.DecimalPlaces = 3;
                nudpndanalista.Location = new System.Drawing.Point(xmargin + xcolunacor, yrp + 60);
                if (altpnd == false)
                {
                    nudpndanalista.Value = new decimal(itens[j].PonderacaoAnalista);
                    nudpndanalista.Enabled = false;
                }
                else
                    nudpndanalista.Enabled = true;
                #endregion

                #region Botões para alteração das ponderações escalaResposta recálculo do resultado final
                Button buttonpnd;
                buttonpnd = new System.Windows.Forms.Button();
                buttonpnd.BackColor = System.Drawing.SystemColors.Control;
                buttonpnd.Name = "buttonpnd";
                buttonpnd.Text = "Alterar Ponderações";
                buttonpnd.UseVisualStyleBackColor = false;
                buttonpnd.Location = new System.Drawing.Point(xmargin + xcolunadim, yrp + 100);
                buttonpnd.Size = new System.Drawing.Size(150, 20);
                buttonpnd.Click += new System.EventHandler(this.buttonpnd_Click);

                Button buttonrr;
                buttonrr = new System.Windows.Forms.Button();
                buttonrr.BackColor = System.Drawing.SystemColors.Control;
                buttonrr.Name = "buttonrr";
                buttonrr.Text = "Recalcular Resultado";
                buttonrr.UseVisualStyleBackColor = false;
                buttonrr.Location = new System.Drawing.Point(xmargin + xcolunadim + buttonpnd.Size.Width + 30, yrp + 100);
                buttonrr.Size = new System.Drawing.Size(150, 20);
                buttonrr.Click += new System.EventHandler(this.buttonrr_Click);
                #endregion

                #region desenhar label 'Limite inferior do analista'
                Double liminfanalista = 0;
                for (int z = 0; z < itens.Count(); z++)
                    if (itens[z].CodigoItem == citem)
                        liminfanalista = itens[z].LimiteInferiorAnalista;

                Label labeltxtliminfanl = new Label();
                labeltxtliminfanl.Text = "Limite Inferior do Analista = " + String.Format("{0:0.##}", liminfanalista);
                labeltxtliminfanl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular,
                System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                labeltxtliminfanl.TabIndex = 5;
                labeltxtliminfanl.Location = new System.Drawing.Point(xmargin + xcolunadim, yrp + 140);
                labeltxtliminfanl.Size = new System.Drawing.Size(300, 20);

                #endregion

                #region desenhar label 'Resultado Ponderado'

                labeltxtrespnd = new Label();
                labeltxtrespnd.Text = "Resultado Ponderado = " + String.Format("{0:0.##}",relatorio.ListaResultados[czona][citem].ResultadoFinal);
                labeltxtrespnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                labeltxtrespnd.TabIndex = 5;
                labeltxtrespnd.Location = new System.Drawing.Point(xmargin + xcolunadim, yrp + 165);
                labeltxtrespnd.Size = new System.Drawing.Size(300, 20);
                labeltxtrespnd.ForeColor = Color.Black;
                #endregion

                #region desenhar label 'Escala de Cores'
                Label labeltxtec = new Label();
                labeltxtec.Text = "Escala de Cores";
                labeltxtec.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular,
                System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                labeltxtec.TabIndex = 5;
                labeltxtec.Location = new System.Drawing.Point(xmargin + xcolunadim, yrp + 200);
                labeltxtec.Size = new System.Drawing.Size(300, 20);
                labeltxtec.ForeColor = Color.DarkSlateGray;
                #endregion
                
                #region desenhar a imagem da cor
                corItem = new System.Windows.Forms.PictureBox();
                corItem.Location = new System.Drawing.Point(xmargin + xcolunadim, yrp + 230);
                corItem.Name = "corItem";
                corItem.Size = new System.Drawing.Size(160, 60);
                corItem.TabIndex = 0;
                corItem.TabStop = false;

                #region desenhar o texto da imagem
                labeltxtimg = new Label();
                labeltxtimg.TabIndex = 4;
                labeltxtimg.Location = new System.Drawing.Point(xmargin + xcolunadim + 5, yrp + 295);
                labeltxtimg.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold,
                    System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                labeltxtimg.Size = new System.Drawing.Size(400, 25);
                #endregion

                curitem = itens[j];

                if (relatorio.ListaResultados[czona][citem].ResultadoFinal <= itens[j].IntervaloVermelho)
                {
                    corItem.Image = global::ETdAnalyser.Properties.Resources.vermelho;
                    //labeltxtimg.ForeColor = Color.Red;
                    labeltxtimg.Text = "Cor = Vermelho";
                }
                else if (relatorio.ListaResultados[czona][citem].ResultadoFinal <= itens[j].IntervaloLaranja)
                {
                    corItem.Image = global::ETdAnalyser.Properties.Resources.laranja;
                    //labeltxtimg.ForeColor = Color.Orange;
                    labeltxtimg.Text = "Cor = Laranja";
                }
                else if (relatorio.ListaResultados[czona][citem].ResultadoFinal <= itens[j].IntervaloAmarelo)
                {
                    corItem.Image = global::ETdAnalyser.Properties.Resources.amarelo;
                    //labeltxtimg.ForeColor = Color.Yellow;
                    labeltxtimg.Text = "Cor = Amarelo";
                }
                else if (relatorio.ListaResultados[czona][citem].ResultadoFinal <= itens[j].IntervaloVerdeLima)
                {
                    corItem.Image = global::ETdAnalyser.Properties.Resources.lima;
                    //labeltxtimg.ForeColor = Color.YellowGreen;
                    labeltxtimg.Text = "Cor = Verde Lima";
                }
                else if (relatorio.ListaResultados[czona][citem].ResultadoFinal <= itens[j].IntervaloVerde)
                {
                    corItem.Image = global::ETdAnalyser.Properties.Resources.verde;
                    //labeltxtimg.ForeColor = Color.Green;
                    labeltxtimg.Text = "Cor = Verde";
                }

                #endregion


                #region desenhar "label" de observações
                Label obslabel = new System.Windows.Forms.Label();
                obslabel.Size = new System.Drawing.Size(0, 0);
                obslabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular,
                    System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                obslabel.Location = new System.Drawing.Point(xmargin + xcolunadim, yrp + 345);
                obslabel.ForeColor = Color.Gray;
                obslabel.Name = "obslabel";
                obslabel.Text = "Observações";
                obslabel.AutoSize = true;
                //obslabel.Dock = DockStyle.Bottom;
                #endregion

                #region desenhar caixa de observações
                //verificar se já foi preenchido se foi, por o que já foi preenchido
                obstb.Location = new System.Drawing.Point(xmargin + xcolunadim + 5, yrp + 380);
                obstb.BringToFront();
                obstb.Name = "obstb";
                //obstb.Dock = DockStyle.Bottom;
                //obstb.Anchor =  AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Top;
                obstb.Size = new System.Drawing.Size(panelrel.Size.Width - 60, 50);
                obstb.TabIndex = 4;
                obstb.TabStop = true;
                if(relatorio.ListaResultados[czona][citem].ObservacaoPreenchida)
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
                panelrel.Controls.Add(labeltxtdim);
                panelrel.Controls.Add(labeltxtrespar);
                panelrel.Controls.Add(labeltxtcor);
                panelrel.Controls.Add(labeldmncliente);
                panelrel.Controls.Add(labeldmnprofissional);
                panelrel.Controls.Add(labeldmnanalista);
                panelrel.Controls.Add(labelres);
                panelrel.Controls.Add(nudpndcliente);
                panelrel.Controls.Add(nudpndprofissional);
                panelrel.Controls.Add(nudpndanalista);
                panelrel.Controls.Add(labeltxtliminfanl);
                panelrel.Controls.Add(labeltxtpnd);
                panelrel.Controls.Add(labeltxtpndcliente);
                panelrel.Controls.Add(labeltxtpndprofissional);
                panelrel.Controls.Add(labeltxtpndanalista);
                panelrel.Controls.Add(labeltxtrespnd);
                panelrel.Controls.Add(buttonpnd);
                panelrel.Controls.Add(buttonrr);
                panelrel.Controls.Add(labeltxtec);
                panelrel.Controls.Add(labelclrp);
                panelrel.Controls.Add(labelclcor);
                panelrel.Controls.Add(labelpfrp);
                panelrel.Controls.Add(labelpfcor);
                panelrel.Controls.Add(labelanrp);
                panelrel.Controls.Add(labelancor);
                panelrel.Controls.Add(obstb);
                panelrel.Controls.Add(obslabel);
                panelrel.Controls.Add(checkBoxInsDt);
                
                
                #endregion

            }
        }

        private void checkBoxInsDt_CheckedChanged(object sender, EventArgs e)
        {
            relatorio.ListaResultados[czona][citem].MostrarResultadoParcial = checkBoxInsDt.Checked;
        }

        private void obstb_TextChanged(object sender, EventArgs e)
        {
            relatorio.ListaResultados[czona][citem].Observacao = obstb.Text;
            if (obstb.Text == "")
                relatorio.ListaResultados[czona][citem].ObservacaoPreenchida = false;
            else
                relatorio.ListaResultados[czona][citem].ObservacaoPreenchida = true;
        }

        private void BotaoCancelar_Click(object sender, EventArgs e)
        {
            InterfaceRelatorio.ActiveForm.Close();
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
            oWord.Visible = true;
            oDoc = oWord.Documents.Add(ref oMissing, ref oMissing,
                ref oMissing, ref oMissing);

            #region Página principal


            #endregion

            #region Indice
            /*
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
             */
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
                    if (relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ResultadoFinal <= itens[j].IntervaloVermelho)
                    {
                        Clipboard.SetImage(global::ETdAnalyser.Properties.Resources.vermelhoWord);
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
                    else if (relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ResultadoFinal <= itens[j].IntervaloLaranja)
                    {
                        Clipboard.SetImage(global::ETdAnalyser.Properties.Resources.laranjaWord);
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
                    else if (relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ResultadoFinal <= itens[j].IntervaloAmarelo)
                    {
                        Clipboard.SetImage(global::ETdAnalyser.Properties.Resources.amareloWord);
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
                    else if (relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ResultadoFinal <= itens[j].IntervaloVerdeLima)
                    {
                        Clipboard.SetImage(global::ETdAnalyser.Properties.Resources.limaWord);
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
                    else if (relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ResultadoFinal <= itens[j].IntervaloVerde)
                    {
                        Clipboard.SetImage(global::ETdAnalyser.Properties.Resources.verdeWord);
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
                    if (relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].MostrarResultadoParcial)
                    {
                        oWord.ActiveWindow.Selection.Font.Name = "Calibri (Body)";
                        oWord.ActiveWindow.Selection.Font.Size = 14;
                        oWord.ActiveWindow.Selection.TypeText("Resultado Detalhado");
                        oWord.Selection.TypeParagraph();

                        
                        Word.Table rdTabela = oWord.Selection.Tables.Add(oWord.Selection.Range, 4, 4, 
                            Microsoft.Office.Interop.Word.WdDefaultTableBehavior.wdWord8TableBehavior, Microsoft.Office.Interop.Word.WdAutoFitBehavior.wdAutoFitFixed);
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

                        if (relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ResultadoQuestionarioGeral <= itens[j].IntervaloVermelho)
                    {
                        rdTabela.Cell(2, 4).Range.Font.Color = Word.WdColor.wdColorRed;
                        rdTabela.Cell(2, 4).Range.Text = "Vermelho";
                    }
                        else if (relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ResultadoQuestionarioGeral <= itens[j].IntervaloLaranja)
                    {
                        rdTabela.Cell(2, 4).Range.Font.Color = Word.WdColor.wdColorOrange;
                        rdTabela.Cell(2, 4).Range.Text = "Laranja";
                    }
                        else if (relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ResultadoQuestionarioGeral <= itens[j].IntervaloAmarelo)
                    {
                        rdTabela.Cell(2, 4).Range.Font.Color = Word.WdColor.wdColorYellow;
                        rdTabela.Cell(2, 4).Range.Text = "Amarelo";
                    }
                        else if (relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ResultadoQuestionarioGeral <= itens[j].IntervaloVerdeLima)
                    {
                        rdTabela.Cell(2, 4).Range.Font.Color = Word.WdColor.wdColorLime;
                        rdTabela.Cell(2, 4).Range.Text = "Verde Lima";
                    }
                        else if (relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ResultadoQuestionarioGeral <= itens[j].IntervaloVerde)
                    {
                        rdTabela.Cell(2, 4).Range.Font.Color = Word.WdColor.wdColorGreen;
                        rdTabela.Cell(2, 4).Range.Text = "Verde";
                    }
                        #endregion

                        #region Profissional
                        if (relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ResultadoFichaAvaliacaoGeral <= itens[j].IntervaloVermelho)
                        {
                            rdTabela.Cell(3, 4).Range.Font.Color = Word.WdColor.wdColorRed;
                            rdTabela.Cell(3, 4).Range.Text = "Vermelho";
                        }
                        else if (relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ResultadoFichaAvaliacaoGeral <= itens[j].IntervaloLaranja)
                        {
                            rdTabela.Cell(3, 4).Range.Font.Color = Word.WdColor.wdColorOrange;
                            rdTabela.Cell(3, 4).Range.Text = "Laranja";
                        }
                        else if (relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ResultadoFichaAvaliacaoGeral <= itens[j].IntervaloAmarelo)
                        {
                            rdTabela.Cell(3, 4).Range.Font.Color = Word.WdColor.wdColorYellow;
                            rdTabela.Cell(3, 4).Range.Text = "Amarelo";
                        }
                        else if (relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ResultadoFichaAvaliacaoGeral <= itens[j].IntervaloVerdeLima)
                        {
                            rdTabela.Cell(3, 4).Range.Font.Color = Word.WdColor.wdColorLime;
                            rdTabela.Cell(3, 4).Range.Text = "Verde Lima";
                        }
                        else if (relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ResultadoFichaAvaliacaoGeral <= itens[j].IntervaloVerde)
                        {
                            rdTabela.Cell(3, 4).Range.Font.Color = Word.WdColor.wdColorGreen;
                            rdTabela.Cell(3, 4).Range.Text = "Verde";
                        }

                        #endregion

                        #region Analista
                        if (relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ResultadoCheckListGeral <= itens[j].IntervaloVermelho)
                        {
                            rdTabela.Cell(4, 4).Range.Font.Color = Word.WdColor.wdColorRed;
                            rdTabela.Cell(4, 4).Range.Text = "Vermelho";
                        }
                        else if (relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ResultadoCheckListGeral <= itens[j].IntervaloLaranja)
                        {
                            rdTabela.Cell(4, 4).Range.Font.Color = Word.WdColor.wdColorOrange;
                            rdTabela.Cell(4, 4).Range.Text = "Laranja";
                        }
                        else if (relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ResultadoCheckListGeral <= itens[j].IntervaloAmarelo)
                        {
                            rdTabela.Cell(4, 4).Range.Font.Color = Word.WdColor.wdColorYellow;
                            rdTabela.Cell(4, 4).Range.Text = "Amarelo";
                        }
                        else if (relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ResultadoCheckListGeral <= itens[j].IntervaloVerdeLima)
                        {
                            rdTabela.Cell(4, 4).Range.Font.Color = Word.WdColor.wdColorLime;
                            rdTabela.Cell(4, 4).Range.Text = "Verde Lima";
                        }
                        else if (relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ResultadoCheckListGeral <= itens[j].IntervaloVerde)
                        {
                            rdTabela.Cell(4, 4).Range.Font.Color = Word.WdColor.wdColorGreen;
                            rdTabela.Cell(4, 4).Range.Text = "Verde";
                        }

                        #endregion

                        #endregion

                        //object count = 4;
                        //oWord.Selection.MoveDown(ref oMissing, ref count, oMissing);
                        for (int w = 0; w < 21; w++)
                            oWord.Selection.MoveDown();
                        oWord.Selection.ClearFormatting();
                        oWord.Selection.TypeParagraph();
                    }
                    
                    #endregion

                    #region Observações
                    if (relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ObservacaoPreenchida)
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
            //oDoc.TablesOfContents[1].Update();

            //UPDATING THE TABLE OF CONTENTS
            //oDoc.TablesOfContents[1].UpdatePageNumbers();

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

        //quando carrega no botao de alterar ponderações
        private void buttonpnd_Click(object sender, EventArgs e)
        {
            if (altpnd == false)
            {
                altpnd = true;
                nudpndcliente.Enabled = true;
                nudpndprofissional.Enabled = true;
                nudpndanalista.Enabled = true;
            }
            else
            {
                altpnd = false;
                nudpndcliente.Enabled = false;
                nudpndprofissional.Enabled = false;
                nudpndanalista.Enabled = false;
            }
        }

        //quando carrega em recalcular resultado ponderado
        private void buttonrr_Click(object sender, EventArgs e)
        {
            if ((float.Parse(nudpndcliente.Value.ToString()) + float.Parse(nudpndprofissional.Value.ToString()) + float.Parse(nudpndanalista.Value.ToString())) != 1)
                //mostrar mensagem de erro;
                MessageBoxPortuguese.Show("Aviso", "A soma das ponderações é \"" + (float.Parse(nudpndcliente.Value.ToString()) +
                    float.Parse(nudpndprofissional.Value.ToString()) + float.Parse(nudpndanalista.Value.ToString())) + "\" , quando deveria ser igual a 1.\n"
                    +"Por favor verifique cada um dos valores e volte a recalcular o resultado.",MessageBoxPortuguese.Icon_Warning);
            else
            {
                double resfinal = ((relatorio.ListaResultados[czona][citem].ResultadoQuestionarioGeral * float.Parse(nudpndcliente.Value.ToString())) +
                            (relatorio.ListaResultados[czona][citem].ResultadoFichaAvaliacaoGeral * float.Parse(nudpndprofissional.Value.ToString()))
                             + (relatorio.ListaResultados[czona][citem].ResultadoCheckListGeral * float.Parse(nudpndanalista.Value.ToString())));

                double liminfanalista = -1;

                for (int i = 0; i < itens.Count(); i++)
                    if (itens[i].CodigoItem == citem)
                        liminfanalista = itens[i].LimiteInferiorAnalista;

                if (liminfanalista != -1 && liminfanalista >= relatorio.ListaResultados[czona][citem].ResultadoCheckListGeral)
                    labeltxtrespnd.Text = "Resultado Ponderado = " + String.Format("{0:0.##}", liminfanalista);
                else
                    labeltxtrespnd.Text = "Resultado Ponderado = " + String.Format("{0:0.##}", resfinal);

                if (resfinal <= curitem.IntervaloVermelho)
                {
                    corItem.Image = global::ETdAnalyser.Properties.Resources.vermelho;
                    //labeltxtimg.ForeColor = Color.Red;
                    labeltxtimg.Text = "Cor = Vermelho";
                }
                else if (resfinal <= curitem.IntervaloLaranja)
                {
                    corItem.Image = global::ETdAnalyser.Properties.Resources.laranja;
                    //labeltxtimg.ForeColor = Color.Orange;
                    labeltxtimg.Text = "Cor = Laranja";
                }
                else if (resfinal <= curitem.IntervaloAmarelo)
                {
                    corItem.Image = global::ETdAnalyser.Properties.Resources.amarelo;
                    //labeltxtimg.ForeColor = Color.Yellow;
                    labeltxtimg.Text = "Cor = Amarelo";
                }
                else if (resfinal <= curitem.IntervaloVerdeLima)
                {
                    corItem.Image = global::ETdAnalyser.Properties.Resources.lima;
                    //labeltxtimg.ForeColor = Color.YellowGreen;
                    labeltxtimg.Text = "Cor = Verde Lima";
                }
                else if (resfinal <= curitem.IntervaloVerde)
                {
                    corItem.Image = global::ETdAnalyser.Properties.Resources.verde;
                    //labeltxtimg.ForeColor = Color.Green;
                    labeltxtimg.Text = "Cor = Verde";
                }
            }
        }
    }
}