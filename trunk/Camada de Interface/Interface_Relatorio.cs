using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ETdA.Camada_de_Interface
{
    partial class Interface_Relatorio : Form
    {
        private long cod_projecto;
        private long cod_analise;
        private Camada_de_Dados.Classes.Relatorio relatorio;
        private List<Camada_de_Dados.Classes.Zona> zonas;
        private List<Camada_de_Dados.Classes.Item> itens;

        private Dictionary<long,Dictionary<long,RichTextBox>> obs;


        public static void main(long cod_projecto, long cod_analise, Camada_de_Dados.Classes.Relatorio relatorio)
        {
            Interface_Relatorio i = new Interface_Relatorio(cod_projecto, cod_analise, relatorio);
            Application.Run(i);
            //i.Visible = true;
        }

        public Interface_Relatorio(long cod_projecto, long cod_analise, Camada_de_Dados.Classes.Relatorio relatorio)
        {
            this.zonas = ETdA.Camada_de_Dados.ETdA.ETdA.getProjecto(cod_projecto).Analises[cod_analise].Zonas;
            this.itens = ETdA.Camada_de_Dados.ETdA.ETdA.getProjecto(cod_projecto).Analises[cod_analise].Itens;
            relatorio.gerarResultadosRelatorio(cod_analise, new List<Camada_de_Dados.Classes.Resposta>(), zonas, itens);

            this.cod_projecto = cod_projecto;
            this.cod_analise = cod_analise;
            this.relatorio = relatorio;

            InitializeComponent();
            showRelatorio();
        }

        public void showRelatorio()
        {

            /*
                        System.Windows.Forms.Panel painelGeralItens;
                        System.Windows.Forms.Panel painelItem;
                        System.Windows.Forms.Label labelItem;
                        System.Windows.Forms.Label labelZona;

                        painelHeaderlZonas.SuspendLayout();
            
                        painelGeralItens.SuspendLayout();
                        painelItem.SuspendLayout();
                        painelGeralZonas.SuspendLayout();
                        SuspendLayout();
                        */
            obs= new Dictionary<long,Dictionary<long,RichTextBox>>();

            for (int i = 0; i < zonas.Count; i++)
            {
                Panel painelGeralItens = new Panel();
                painelGeralItens.SuspendLayout();
                //falta adicionar cada panel de cada item
                painelGeralItens.Dock = System.Windows.Forms.DockStyle.Bottom;
                painelGeralItens.Location = new System.Drawing.Point(0, 45);
                painelGeralItens.Name = "painelGeralItens";
                painelGeralItens.Size = new System.Drawing.Size(943, 170);
                painelGeralItens.TabIndex = 1;

                GroupBox groupBoxZona = new GroupBox();
                groupBoxZona.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
                groupBoxZona.AutoSize = true;
                groupBoxZona.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                groupBoxZona.Dock = System.Windows.Forms.DockStyle.Top;
                groupBoxZona.Location = new System.Drawing.Point(12, 6);
                groupBoxZona.Name = "groupBoxZona";
                groupBoxZona.Size = new System.Drawing.Size(943, 215);
                groupBoxZona.TabIndex = 1;
                groupBoxZona.TabStop = false;
                //nome da zona
                groupBoxZona.Text = zonas[i].Nome;

                painelGeralZonas.Controls.Add(groupBoxZona);
                Dictionary<long,RichTextBox> obsitem = new Dictionary<long,RichTextBox>();

                for (int j = 0; j < itens.Count; j++)
                {

                    System.Windows.Forms.PictureBox corItem = new System.Windows.Forms.PictureBox();
                    if (relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ResultadoFinal <= itens[j].Inter_Vermelho)
                        corItem.Image = global::ETdA.Properties.Resources.vermelho;
                    else if (relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ResultadoFinal <= itens[j].Inter_Laranja)
                        corItem.Image = global::ETdA.Properties.Resources.laranja;
                    else if (relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ResultadoFinal <= itens[j].Inter_Amarelo)
                        corItem.Image = global::ETdA.Properties.Resources.amarelo;
                    else if (relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ResultadoFinal <= itens[j].Inter_Verde_Lima)
                        corItem.Image = global::ETdA.Properties.Resources.lima;
                    else if (relatorio.ListaResultados[zonas[i].Codigo][itens[j].CodigoItem].ResultadoFinal <= itens[j].Inter_Verde)
                        corItem.Image = global::ETdA.Properties.Resources.verde;


                    corItem.Dock = System.Windows.Forms.DockStyle.Right;
                    corItem.Location = new System.Drawing.Point(600, 20);
                    corItem.Name = "corItem";
                    corItem.Size = new System.Drawing.Size(280, 98);
                    corItem.TabIndex = 0;
                    corItem.TabStop = false;

                    RichTextBox obstb = new System.Windows.Forms.RichTextBox();
                    Label obslabel = new System.Windows.Forms.Label();

                    obstb.Location = new System.Drawing.Point(264, 231);
                    obstb.Name = "obstb";
                    obstb.Size = new System.Drawing.Size(360, 100);
                    obstb.TabIndex = 1;
                    obstb.Text = "";

                    obslabel.AutoSize = true;
                    obslabel.Location = new System.Drawing.Point(261, 215);
                    obslabel.Name = "label2";
                    obslabel.Size = new System.Drawing.Size(70, 13);
                    obslabel.TabIndex = 2;
                    obslabel.Text = "Observações";

                    obsitem.Add(itens[j].CodigoItem, obstb);

                    GroupBox groupBoxItem = new GroupBox();
                    //groupBoxItem.AutoSize = true;
                    groupBoxItem.Controls.Add(corItem);
                    groupBoxItem.Controls.Add(obstb);
                    groupBoxItem.Controls.Add(obslabel);
                    groupBoxItem.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
                    groupBoxItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    groupBoxItem.Dock = System.Windows.Forms.DockStyle.Top;
                    groupBoxItem.Location = new System.Drawing.Point(12, 6);
                    groupBoxItem.Name = "GroupBoxItem";
                    groupBoxItem.Size = new System.Drawing.Size(943, 215);
                    groupBoxItem.TabIndex = 1;
                    groupBoxItem.TabStop = false;
                    //nome do item
                    groupBoxItem.Text = itens[j].NomeItem;


                    groupBoxZona.Controls.Add(groupBoxItem);
                }
                obs.Add(zonas[i].Codigo, obsitem);
            }
        }
    }
}
