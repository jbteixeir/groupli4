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
    }
}
