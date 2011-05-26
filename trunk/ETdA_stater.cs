using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterfaceETdA;
using System.Windows.Forms;

namespace ETdA_starter
{
    static class ETdA_stater
    {
        [STAThread]
        static void Main()
        {
            InterfaceGuestaoProjectos igp = new InterfaceGuestaoProjectos();
            Application.Run(igp);
        }
    }
}
