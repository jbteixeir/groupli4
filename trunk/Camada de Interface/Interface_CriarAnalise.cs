using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ETdA.Camada_de_Negócio;

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

        private void MouseEnterAction(object sender, EventArgs e)
        {
            Label t = (Label)sender;
            t.Font = new Font(t.Font, FontStyle.Underline);
        }

        private void MouseLeaveAction(object sender, EventArgs e)
        {
            Label t = (Label)sender;
            t.Font = new Font(t.Font, FontStyle.Regular);
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

        private void AdicionarActionPerfermed(object sender, EventArgs e)
        {

        }

        private void ZonasActionPerformed(object sender, EventArgs e)
        {

        }

        private void ItensActionPerformed(object sender, EventArgs e)
        {
            Interface_CriarAnaliseItens.main();
        }
    }
}
