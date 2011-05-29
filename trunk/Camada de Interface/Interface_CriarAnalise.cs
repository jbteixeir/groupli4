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
    public partial class Interface_CriarAnalise : Form
    {
        public Interface_CriarAnalise()
        {
            InitializeComponent();
        }

        public static void main()
        {
            Interface_CriarAnalise ica = new Interface_CriarAnalise();
            ica.Visible = true;
        }

        private void endFrame()
        {
            Dispose();
            Close();
        }

        private void CancelarActionPerformed(object sender, EventArgs e)
        {
            endFrame();
        }

        private void NovoZonaActionPerformed(object sender, EventArgs e)
        {
            string zona = textBox2.Text;

            listBox1.Items.Add(zona);
        }

        private void NovoItemActionPerformed(object sender, EventArgs e)
        {
            string item = textBox3.Text;

            checkedListBox1.Items.Add(item);
        }

        private void MostrarTodosActionPerformed(object sender, EventArgs e)
        {

        }

        private void AdicionarActionPerfermed(object sender, EventArgs e)
        {

        }
    }
}
