using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ETdA.Camada_de_Dados.ETdA;

namespace ETdA.Camada_de_Negócio
{
    class GestaodeProjectos
    {
        private ETdA_main etda;
        public GestaodeProjectos(ETdA_main etda)
        {
            this.etda = etda;
        }

        //void criaProjecto(String nomeEstabelecimento);
        //void editaProjecto(String nomeEstabelecimento);
        //void removeProjecto(String nomeEstabelecimento);
        public void abreProjecto(String nomeEstabelecimento)
        {
            etda.abreProjecto(nomeEstabelecimento);
        }
    }
}
