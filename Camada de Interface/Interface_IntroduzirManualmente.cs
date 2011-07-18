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
    public partial class Interface_IntroduzirManualmente : Form
    {
        public static long cod_Projecto;
        public static long cod_Analise;

        public static void main(long codProjecto, long codAnalise)
        {
            cod_Projecto = codProjecto;
            cod_Analise = codAnalise;

            Interface_IntroduzirManualmente i = new Interface_IntroduzirManualmente();
            i.Visible = true;
        }

        public Interface_IntroduzirManualmente()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Interface_Questionario newInterfaceQuestionario = new Interface_Questionario(cod_Projecto, cod_Analise);
            newInterfaceQuestionario.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Interface_FichaAvaliacao newInterfaceFA = new Interface_FichaAvaliacao(cod_Projecto, cod_Analise);
            newInterfaceFA.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Interface_CheckList newInterfaceChecklist = new Interface_CheckList(cod_Projecto, cod_Analise);
            newInterfaceChecklist.ShowDialog();
       }
    }
}
