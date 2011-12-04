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
    static class ETdA_starter
    {
        [STAThread]
        static void Main()
        {
            if (GestaodeAnalistas.existeFicheiroConfiguracao())
            {
                Boolean b = GestaodeAnalistas.loadConnectionUtilizadorLogado();

                if (b)
                {
                    InterfaceGuestaoProjectos.main(true);
                }
                else
                {
                    b = GestaodeAnalistas.loadConnectionSuper();

                    if (b)
                    {
                        InterfaceLogin.main();
                    }
                    else
                    {
                        Interface_ConfigurarLigacaoBD.main();
                        InterfaceLogin.main();
                    }
                }
            }
            else
            {
                InterfaceLogin.main();
            }
        }
    }
}
