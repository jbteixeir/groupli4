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
            Camada_de_Dados.ETdA.ETdA.abreProjecto(nomeEstabelecimento);
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
    }
}
