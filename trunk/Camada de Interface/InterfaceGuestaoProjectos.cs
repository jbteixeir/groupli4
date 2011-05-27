using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ETdA.Camada_de_Negócio;

namespace InterfaceETdA
{
    partial class InterfaceGuestaoProjectos : Form
    {
        private GestaodeProjectos gp;
        public InterfaceGuestaoProjectos(GestaodeProjectos gp)
        {
            this.gp = gp;
            InitializeComponent();
        }

        private void MouseEnterAction(object sender, EventArgs e)
        {
            Label t = (Label)sender;
            t.Font = new Font(t.Font,FontStyle.Underline);
        }

        private void MouseLeaveAction(object sender, EventArgs e)
        {
            Label t = (Label)sender;
            t.Font = new Font(t.Font, FontStyle.Regular);
        }

        private void OpenProjectClick(object sender, EventArgs e)
        {
            Label l = (Label) sender;
            gp.abreProjecto(l.Text);
            endFrame();
        }

        private void endFrame()
        {
            this.Dispose();
        }
    }
}
