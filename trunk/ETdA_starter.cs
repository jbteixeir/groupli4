using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ETdAnalyser.Camada_de_Negócio;
using ETdAnalyser.CamadaDados.ETdA;
using ETdAnalyser.CamadaInterface;
using ETdAnalyser.CamadaDados.DataBaseCommunicator;
using System.Globalization;
using System.Text;

namespace ETdA_starter
{
    static class ETdA_starter
    {
        [STAThread]
        static void Main()
        {
            var ci = System.Globalization.CultureInfo.InvariantCulture.Clone() as System.Globalization.CultureInfo;
            ci.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            InterfaceStarter.main();
        }
    }
}
