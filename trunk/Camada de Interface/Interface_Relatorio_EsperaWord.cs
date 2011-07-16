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
    public partial class Interface_Relatorio_EsperaWord : Form
    {
        private static Interface_Relatorio_EsperaWord irew;
        int contagem;
        int max_progress_bar;

        public static void main(int max_progress_bar)
        {
            irew = new Interface_Relatorio_EsperaWord(max_progress_bar);
            irew.Visible = true;
            irew.contagem = 0;
            irew.max_progress_bar = max_progress_bar;
        }
        public Interface_Relatorio_EsperaWord(int max_progress_bar)
        {

            InitializeComponent();
            progressBar1.Maximum = max_progress_bar;
            progressBar1.Minimum = 0;
        }

        public static void StatIncrementar_Progressbar()
        {
            irew.Incrementar_Progressbar();
            
        }

        public void Incrementar_Progressbar()
        {
            progressBar1.Increment(1);
            double temp = (double)(((double) contagem / max_progress_bar) * 100);

            if (contagem != 0)
                Text = "A Gerar Relatorio... (" + (int) temp +"%)";
            contagem++;
        }

        public static void StatZona(string zona)
        {
            irew.StatDefZona(zona);
        }

        public void StatDefZona(string zona)
        {
            label3.Text = zona;
        }

        public static void StatItem(string item)
        {
            irew.StatDefItem(item);
        }

        public void StatDefItem(string item)
        {
            label4.Text = item;
        }
        
    }
}
