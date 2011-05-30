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
        private int cod_projecto;
        private int cod_analise;
        private Camada_de_Dados.Classes.Relatorio relatorio;
        private List<Camada_de_Dados.Classes.Zona> zonas;
        private List<Camada_de_Dados.Classes.Item> itens;

        public static void main(int cod_projecto, int cod_analise, Camada_de_Dados.Classes.Relatorio relatorio)
        {
            Interface_Relatorio i = new Interface_Relatorio(cod_projecto, cod_analise, relatorio);
            i.Visible = true;
        }

        public Interface_Relatorio(int projecto, int cod_analise, Camada_de_Dados.Classes.Relatorio relatorio)
        {
            this.zonas = Camada_de_Negócio.GestaodeAnalises.getListaZona();
            this.itens = Camada_de_Negócio.GestaodeAnalises.getListaItens();
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


            for (int i = 0; i < 5; i++)
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
                groupBoxZona.Text = "GroupBoxZona1";

                painelGeralZonas.Controls.Add(groupBoxZona);

                for (int j = 0; j < 5; j++)
                {
                    
                    System.Windows.Forms.PictureBox corItem = new System.Windows.Forms.PictureBox();
                    if(true)
                    corItem.Image = global::ETdA.Properties.Resources.vermelho;

                    corItem.Dock = System.Windows.Forms.DockStyle.Right;
                    corItem.Location = new System.Drawing.Point(600, 20);
                    corItem.Name = "corItem";
                    corItem.Size = new System.Drawing.Size(280, 98);
                    corItem.TabIndex = 0;
                    corItem.TabStop = false;


                    GroupBox groupBoxItem = new GroupBox();
                    //groupBoxItem.AutoSize = true;
                    groupBoxItem.Controls.Add(corItem);
                    groupBoxItem.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
                    groupBoxItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    groupBoxItem.Dock = System.Windows.Forms.DockStyle.Top;
                    groupBoxItem.Location = new System.Drawing.Point(12, 6);
                    groupBoxItem.Name = "GroupBoxItem";
                    groupBoxItem.Size = new System.Drawing.Size(943, 215);
                    groupBoxItem.TabIndex = 1;
                    groupBoxItem.TabStop = false;
                    //nome do item
                    groupBoxItem.Text = "GroupBoxItem1";


                    groupBoxZona.Controls.Add(groupBoxItem);
                }

            }
        }
    }
}
