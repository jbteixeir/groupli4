using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdAnalyser.Camada_de_Dados.Classes
{
    class Formulario
    {
        private long codigoAnalise;

        public Formulario()
        {
            codigoAnalise = -1;
        }

        public Formulario(long _codigoAnalise)
        {
            codigoAnalise = _codigoAnalise;
        }

        public long CodigoAnalise
        {
            get { return codigoAnalise; }
            set { codigoAnalise = value; }
        }
    }
}
