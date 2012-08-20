using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ETdAnalyser.Camada_de_Negócio;
using ETdAnalyser.Camada_de_Dados.ETdA;
using ETdAnalyser.Camada_de_Interface;
using ETdAnalyser.Camada_de_Dados.DataBaseCommunicator;

namespace ETdA_starter
{
    static class ETdA_starter
    {
        [STAThread]
        static void Main()
        {
            InterfaceStarter.main();
        }
    }
}
