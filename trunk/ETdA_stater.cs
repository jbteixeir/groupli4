using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterfaceETdA;
using System.Windows.Forms;
using ETdA.Camada_de_Negócio;
using ETdA.Camada_de_Dados.ETdA;

namespace ETdA_starter
{
    static class ETdA_stater
    {
        private static ETdA_main etda;

        private static InterfaceGuestaoProjectos igp;
        private static GestaodeProjectos gp;

        [STAThread]
        static void Main()
        {
            etda = new ETdA_main();

            gp = new GestaodeProjectos(etda);
            igp = new InterfaceGuestaoProjectos(gp);
            Application.Run(igp);
        }
    }
}
