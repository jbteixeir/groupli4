using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ETdAnalyser.Camada_de_Interface
{
    public partial class InterfaceCreditos : Form
    {
        public InterfaceCreditos()
        {
            InitializeComponent();
            this.ShowDialog();
        }

        public static void main()
        {
            InterfaceCreditos i = new InterfaceCreditos();
        }
    }
}
