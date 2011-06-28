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

        public static Boolean podeCriarProjecto(String nomeEst)
        {
            return Camada_de_Dados.ETdA.ETdA.podeAdicionarProjecto(nomeEst);
        }

        public static void criaProjecto(String nomeEstabelecimento)
        {
            Camada_de_Dados.ETdA.ETdA.adicionaNovoProjecto(nomeEstabelecimento);
        }

        public static long abreProjecto(String nomeEstabelecimento)
        {
            return Camada_de_Dados.ETdA.ETdA.abreProjecto(nomeEstabelecimento);
        }

        public static List<string> nomesProjectos()
        {
            return Camada_de_Dados.ETdA.ETdA.Nomes_Estabelecimentos;
        }

        public static List<string> projectosRecentes()
        {
            List<string> ps = Camada_de_Dados.ETdA.ETdA.Nomes_Estabelecimentos;
            List<string> rs = new List<string>();

            for (int i = 0; i < 5 && i < ps.Count ; i++)
                rs.Add(ps[i]);

            return rs;
        }

        public static long getCodProjecto(string nome)
        {
            return Camada_de_Dados.ETdA.ETdA.getCodProjecto(nome);
        }
    }
}
