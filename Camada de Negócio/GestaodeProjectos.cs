using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ETdA.Camada_de_Dados.ETdA;

namespace ETdA.Camada_de_Negócio
{
    class GestaodeProjectos
    {
        public static void init()
        {
            Camada_de_Dados.ETdA.ETdA.init();
        }

        //void criaProjecto(String nomeEstabelecimento);
        //void editaProjecto(String nomeEstabelecimento);
        //void removeProjecto(String nomeEstabelecimento);
        public static void abreProjecto(String nomeEstabelecimento)
        {

        }

        public static List<string> nomesProjectos()
        {
            return Camada_de_Dados.ETdA.ETdA.Nomes_Estabelecimentos;
        }
    }
}
