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
    public partial class Interface_Creditos : Form
    {
        public Interface_Creditos()
        {
            InitializeComponent();
            this.ShowDialog();
        }

        public static void main()
        {
            Interface_Creditos i = new Interface_Creditos();
        }
    }
}
