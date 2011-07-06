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

        private Dictionary<long, Dictionary<long, RichTextBox>> obs;


        public static void main(long cod_projecto, long cod_analise, Camada_de_Dados.Classes.Relatorio relatorio)
        {
            Interface_Relatorio i = new Interface_Relatorio(cod_projecto, cod_analise, relatorio);
            i.ShowDialog();
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode();


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

                Label Zona = new Label();
                Zona.Location = new System.Drawing.Point(12, 6);
                Zona.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular,
                    System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                Zona.Dock = DockStyle.Top;
                Zona.TabIndex = 3;
                //nome da zona
                Zona.Text = zonas[i].Nome;

                Console.WriteLine("red " + itens[j].Inter_Vermelho + " " + itens[j].Inter_Laranja + " " + itens[j].Inter_Amarelo + " " + itens[j].Inter_Verde_Lima + " " + itens[j].Inter_Verde);

                System.Windows.Forms.PictureBox corItem = new System.Windows.Forms.PictureBox();
                if (relatorio.ListaResultados[czona][citem].ResultadoFinal <= itens[j].Inter_Vermelho)
                    corItem.Image = global::ETdA.Properties.Resources.vermelho;
                else if (relatorio.ListaResultados[czona][citem].ResultadoFinal <= itens[j].Inter_Laranja)
                    corItem.Image = global::ETdA.Properties.Resources.laranja;
                else if (relatorio.ListaResultados[czona][citem].ResultadoFinal <= itens[j].Inter_Amarelo)
                    corItem.Image = global::ETdA.Properties.Resources.amarelo;
                else if (relatorio.ListaResultados[czona][citem].ResultadoFinal <= itens[j].Inter_Verde_Lima)
                    corItem.Image = global::ETdA.Properties.Resources.lima;
                else if (relatorio.ListaResultados[czona][citem].ResultadoFinal <= itens[j].Inter_Verde)
                    corItem.Image = global::ETdA.Properties.Resources.verde;


                corItem.Dock = System.Windows.Forms.DockStyle.Right;
                corItem.Location = new System.Drawing.Point(300, 60);
                corItem.Name = "corItem";
                corItem.Size = new System.Drawing.Size(280, 98);
                corItem.TabIndex = 0;
                corItem.TabStop = false;

                RichTextBox obstb = new System.Windows.Forms.RichTextBox();
                Label obslabel = new System.Windows.Forms.Label();

                obstb.Location = new System.Drawing.Point(20, 231);
                obstb.Name = "obstb";
                obstb.Size = new System.Drawing.Size(360, 100);
                obstb.TabIndex = 1;
                obstb.Text = "";

                obslabel.AutoSize = true;
                obslabel.Location = new System.Drawing.Point(20, 215);
                obslabel.Name = "label2";
                obslabel.Size = new System.Drawing.Size(70, 13);
                obslabel.TabIndex = 2;
                obslabel.Text = "Observações";


                Label Item = new Label();
                Item.Location = new System.Drawing.Point(12, 30);
                Item.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular,
                    System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                Item.Dock = DockStyle.Top;
                Item.TabIndex = 2;
                //nome do item
                Item.Text = itens[j].NomeItem;

                Panel panelrel = new Panel();
                panelrel.Dock = DockStyle.Fill;
                panelrel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 40);
                panelrel.Controls.Add(Item);
                panelrel.Controls.Add(Zona);
                panelrel.Controls.Add(corItem);
                panelrel.Controls.Add(obstb);
                panelrel.Controls.Add(obslabel);

                panelZonaItem.Controls.Clear();
                panelZonaItem.Controls.Add(panelrel);
                panelZonaItem.Controls.Add(BotaoCancelar);
                panelZonaItem.Controls.Add(BotaoGuardar);
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
    }
}