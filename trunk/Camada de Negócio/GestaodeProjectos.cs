using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ETdA.Camada_de_Dados.ETdA;

namespace ETdA.Camada_de_Negócio
{
    class GestaodeProjectos
    {
        // s_final
        public static void init()
        {
            Camada_de_Dados.ETdA.ETdA.init();
        }

        // s_final
        public static Dictionary<long, string> Cod_names_Projects()
        {
            return Camada_de_Dados.ETdA.ETdA.Codes_Nomes_Estabelecimentos;
        }

        // s_final
        public static Dictionary<long, string> projectosRecentes()
        {
            return Camada_de_Dados.ETdA.ETdA.projectosRecentes();
        }

        // s_final
        public static void abreProjecto(long codEstabelecimento)
        {
            Camada_de_Dados.ETdA.ETdA.abreProjecto(codEstabelecimento);
        }

        // s_final
        public static Boolean podeCriarProjecto(String nomeEst)
        {
            return Camada_de_Dados.ETdA.ETdA.podeAdicionarProjecto(nomeEst);
        }

        // s_final
        public static void criaProjecto(String nomeEstabelecimento)
        {
            Camada_de_Dados.ETdA.ETdA.adicionaNovoProjecto(nomeEstabelecimento);
        }
    }
}
