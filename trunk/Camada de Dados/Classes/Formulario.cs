using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdAnalyser.CamadaDados.Classes
{
    class Formulario
    {
        private long codigoAnalise;

        public Formulario()
        {
            codigoAnalise = -1;
        }

        public Formulario(long codigoAnalise)
        {
            codigoAnalise = codigoAnalise;
        }

        public long CodigoAnalise
        {
            get { return codigoAnalise; }
            set { codigoAnalise = value; }
        }
    }
}
