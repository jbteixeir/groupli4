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

                GroupBox groupBoxZona = new GroupBox();
                groupBoxZona.BackColor = System.Drawing.SystemColors.AppWorkspace;
                groupBoxZona.AutoSize = true;
                //groupBoxZona.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                groupBoxZona.Dock = System.Windows.Forms.DockStyle.Fill;
                groupBoxZona.Location = new System.Drawing.Point(12, 6);
                groupBoxZona.Name = "groupBoxZona";
                groupBoxZona.Size = new System.Drawing.Size(943, 215);
                groupBoxZona.TabIndex = 1;
                groupBoxZona.TabStop = false;
                //nome da zona
                groupBoxZona.Text = zonas[i].Nome;

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
                corItem.Location = new System.Drawing.Point(600, 20);
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


                GroupBox groupBoxItem = new GroupBox();
                groupBoxItem.AutoSize = true;
                groupBoxItem.Controls.Add(corItem);
                groupBoxItem.Controls.Add(obstb);
                groupBoxItem.Controls.Add(obslabel);
                groupBoxItem.BackColor = System.Drawing.SystemColors.Control;
                //groupBoxItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                groupBoxItem.Dock = System.Windows.Forms.DockStyle.Fill;
                groupBoxItem.Location = new System.Drawing.Point(12, 6);
                groupBoxItem.Name = "GroupBoxItem";
                groupBoxItem.Size = new System.Drawing.Size(943, 215);
                groupBoxItem.TabIndex = 1;
                groupBoxItem.TabStop = false;
                //nome do item
                groupBoxItem.Text = itens[j].NomeItem;

                groupBoxZona.Controls.Add(groupBoxItem);

                panelZonaItem.Controls.Clear();
                
                panelZonaItem.Controls.Add(groupBoxZona);
             
            }
        }
    }
}
