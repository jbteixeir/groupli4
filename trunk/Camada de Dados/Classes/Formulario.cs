using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdA.Camada_de_Dados.Classes
{
    class Formulario
    {
        private long cod_analise;

        public Formulario()
        {
            cod_analise = -1;
        }

        public Formulario(long _cod_analise)
        {
            cod_analise = _cod_analise;
        }

        public long CodAnalise
        {
            get { return cod_analise; }
            set { cod_analise = value; }
        }
    }
}
