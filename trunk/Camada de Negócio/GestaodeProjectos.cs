using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ETdAnalyser.CamadaDados.ETdA;

namespace ETdAnalyser.Camada_de_Negócio
{
    class GestaodeProjectos
    {
        // s_final
        public static void init()
        {
            CamadaDados.ETdA.ETdA.init();
        }

        // s_final
        public static Dictionary<long, string> Cod_names_Projects()
        {
            return CamadaDados.ETdA.ETdA.Codes_Nomes_Projectos;
        }

        // s_final
        public static Dictionary<long, string> projectosRecentes()
        {
            return CamadaDados.ETdA.ETdA.projectosRecentes();
        }

        // s_final
        public static void abreProjecto(long codigoProjecto)
        {
            CamadaDados.ETdA.ETdA.abreProjecto(codigoProjecto);
        }

        // s_final
        public static Boolean podeCriarProjecto(String nomeProjecto)
        {
            return CamadaDados.ETdA.ETdA.podeAdicionarProjecto(nomeProjecto);
        }

        // s_final
        public static void criaProjecto(String nomeProjecto)
        {
            CamadaDados.ETdA.ETdA.adicionaNovoProjecto(nomeProjecto);
        }
    }
}
