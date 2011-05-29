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

        public static void main(int cod_projecto, int cod_analise, Camada_de_Dados.Classes.Relatorio relatorio)
        {
            Interface_Relatorio i = new Interface_Relatorio(cod_projecto, cod_analise, relatorio);
            Application.Run(i);
        }

        public Interface_Relatorio(int projecto, int cod_analise, Camada_de_Dados.Classes.Relatorio relatorio)
        {
            InitializeComponent();
            this.cod_projecto = cod_projecto;
            this.cod_analise = cod_analise;
            this.relatorio = relatorio;

            //showRelatorio();
        }

        public void showRelatorio()
        {
            List<Camada_de_Dados.Classes.Zona> zonas = Camada_de_Negócio.GestaodeAnalises.getListaZona();
            List<Camada_de_Dados.Classes.Item> ites = Camada_de_Negócio.GestaodeAnalises.getListaItens();

            //header Titulo Zonas
            Label TituloZonas = new System.Windows.Forms.Label();
            TituloZonas.SuspendLayout();
            TituloZonas.AutoSize = true;
            TituloZonas.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            TituloZonas.Location = new System.Drawing.Point(13, 9);
            TituloZonas.Name = "TituloZonas";
            TituloZonas.Size = new System.Drawing.Size(77, 25);
            TituloZonas.TabIndex = 1;
            TituloZonas.Text = "Zonas";

            TituloZonas.ResumeLayout(false);
            TituloZonas.PerformLayout();

            //header Panel
            Panel painelHeaderZonas = new System.Windows.Forms.Panel();
            painelHeaderZonas.SuspendLayout();
            painelHeaderZonas.Controls.Add(TituloZonas);
            painelHeaderZonas.Dock = System.Windows.Forms.DockStyle.Top;
            painelHeaderZonas.Location = new System.Drawing.Point(0, 0);
            painelHeaderZonas.Name = "painelHeaderZonas";
            painelHeaderZonas.Size = new System.Drawing.Size(943, 45);
            painelHeaderZonas.TabIndex = 0;

            painelHeaderZonas.ResumeLayout(false);
            painelHeaderZonas.PerformLayout();

            //Painel Geral com todas as zonas
            Panel painelGeralZonas = new System.Windows.Forms.Panel();
            painelGeralZonas.SuspendLayout();
            painelGeralZonas.Dock = System.Windows.Forms.DockStyle.Top;
            painelGeralZonas.Location = new System.Drawing.Point(0, 45);
            painelGeralZonas.Name = "painelGeralZonas";
            painelGeralZonas.Size = new System.Drawing.Size(943, 218);
            painelGeralZonas.TabIndex = 1;

            painelGeralZonas.ResumeLayout(false);
            painelGeralZonas.PerformLayout();
            

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

/*
            for (int i = 0; i < 5; i++)
            {
                Panel painelGeralItens = new Panel();
                painelGeralItens.SuspendLayout();
                painelGeralItens.Dock = System.Windows.Forms.DockStyle.Bottom;
                painelGeralItens.Location = new System.Drawing.Point(0, 45);
                painelGeralItens.Name = "painelGeralItens";
                painelGeralItens.Size = new System.Drawing.Size(943, 170);
                painelGeralItens.TabIndex = 1;

                Label labelZona = new Label();
                labelZona.SuspendLayout();

                labelZona.AutoSize = true;
                labelZona.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                labelZona.Location = new System.Drawing.Point(24, 0);
                labelZona.Name = "labelZona";
                labelZona.Size = new System.Drawing.Size(66, 24);
                labelZona.TabIndex = 0;
                labelZona.Text = "maria";

                Panel painelZona = new Panel();
                painelZona.SuspendLayout();

                painelZona.Controls.Add(painelGeralItens);
                painelZona.Controls.Add(labelZona);
                painelZona.Dock = System.Windows.Forms.DockStyle.Top;
                painelZona.Location = new System.Drawing.Point(0, 0);
                painelZona.Name = "painelZona";
                painelZona.Size = new System.Drawing.Size(943, 215);
                painelZona.TabIndex = 1;

                painelGeralZonas.Controls.Add(painelZona);

                


                
            }
 * */
        }
    }
}
