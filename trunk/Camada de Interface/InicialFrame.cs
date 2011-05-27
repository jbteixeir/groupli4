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
    public partial class InicialFrame : Form
    {
        public InicialFrame()
        {
            InitializeComponent();
            initThis();
        }

        public void initThis()
        {
            Boolean b = GestaodeInicio.loadConnection();

            if (b)
                close();
            else
                MessageBox.Show("Não foi possível Ligar à base de dados", "Connection ERROR",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void close(){
            Dispose();
            Close();
            InterfaceLogin.main();
        }

        public static void main()
        {
            InicialFrame f = new InicialFrame();
            Application.Run(f);
        }
    }
}
