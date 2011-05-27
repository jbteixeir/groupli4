using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ETdA.Camada_de_Negócio;
using ETdA.Camada_de_Dados.ETdA;
using ETdA.Camada_de_Interface;
using ETdA.Camada_de_Dados.DataBaseCommunicator;

namespace ETdA_starter
{
    static class ETdA_stater
    {
        [STAThread]
        static void Main()
        {
            //InicialFrame.main();
            InterfaceLogin.main();
        }
    }
}
