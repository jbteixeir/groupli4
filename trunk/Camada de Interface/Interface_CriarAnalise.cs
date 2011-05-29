using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ETdA.Camada_de_Negócio;
using ETdA.Camada_de_Dados.Classes;

namespace ETdA.Camada_de_Interface
{
    public partial class Interface_CriarAnalise : Form
    {
        List<string> zonas;
        List<Item> itens;
        private static Interface_CriarAnalise ica;

        public Interface_CriarAnalise()
        {
            InitializeComponent();
            zonas = new List<string>();
            itens = new List<Item>();
        }

        public static void main()
        {
            ica = new Interface_CriarAnalise();
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
            Interface_CriarAnaliseZonas.main(zonas);
        }

        private void ItensActionPerformed(object sender, EventArgs e)
        {
            Interface_CriarAnaliseItens.main();
        }

        public static void ZonasOkReenc(object sender, EventArgs e)
        {
            ica.ZonasOk(sender, e);
        }
        private void ZonasOk(object sender, EventArgs e)
        {
            zonas = (List<string>)sender;
        }
    }
}
