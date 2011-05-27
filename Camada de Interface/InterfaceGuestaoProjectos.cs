using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace InterfaceETdA
{
    public partial class InterfaceGuestaoProjectos : Form
    {
        public InterfaceGuestaoProjectos()
        {
            InitializeComponent();
        }

        private void MouseEnterAction(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            t.Font = new Font(t.Font,FontStyle.Underline);
        }
    }
}
