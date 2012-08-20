using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ETdAnalyser.Camada_de_Dados.ETdA;

namespace ETdAnalyser.Camada_de_Negócio
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
            return Camada_de_Dados.ETdA.ETdA.Codes_Nomes_Projectos;
        }

        // s_final
        public static Dictionary<long, string> projectosRecentes()
        {
            return Camada_de_Dados.ETdA.ETdA.projectosRecentes();
        }

        // s_final
        public static void abreProjecto(long codProjecto)
        {
            Camada_de_Dados.ETdA.ETdA.abreProjecto(codProjecto);
        }

        // s_final
        public static Boolean podeCriarProjecto(String nomeProjecto)
        {
            return Camada_de_Dados.ETdA.ETdA.podeAdicionarProjecto(nomeProjecto);
        }

        // s_final
        public static void criaProjecto(String nomeProjecto)
        {
            Camada_de_Dados.ETdA.ETdA.adicionaNovoProjecto(nomeProjecto);
        }
    }
}
