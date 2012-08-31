using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ETdAnalyser.Camada_de_Dados.Classes;
using ETdAnalyser.Camada_de_Dados.DataBaseCommunicator;

namespace ETdAnalyser.Camada_de_Negócio
{
    class GestaodeAnalises
    {
        enum TipoAnalise { AreaComum, Actividade, Zona };

        //Métodos

        // s_final
        public static Dictionary<long,string> getCodeNomeAnalises(long codigoProjectoroj)
        {
            if (Camada_de_Dados.ETdA.ETdA.getProjecto(codigoProjectoroj) == null)
                GestaodeProjectos.abreProjecto(codigoProjectoroj);

            return Camada_de_Dados.ETdA.ETdA.getProjecto(codigoProjectoroj).Cod_Name_Analise;
        }

        // s_final
        public static void abreAnalise(long cp, long ca)
        {
            Camada_de_Dados.ETdA.ETdA.getProjecto(cp).abreAnalise(ca);
        }







        private string getDescricao(TipoAnalise t)	{
			switch (t)
			{
				case TipoAnalise.AreaComum:
					return "'Area Comum'";
				case TipoAnalise.Actividade:
					return "'Actividade'";
				case TipoAnalise.Zona:
					return "'Zona'";
				default:
					return "";
			}
		}



        public static void modificaPonderacoes(List<Item> itens)
        {
            foreach (Item i in itens)
                FuncsToDataBase.updateItemAnalise(i);
        }

        public static void adicionaAnalise(Analise a, long codigoProjecto){
            Camada_de_Dados.ETdA.ETdA.getProjecto(codigoProjecto).adicionaNovaAnalise(a.Tipo, a.Nome, a.Zonas, a.Itens);
        }

        public static Dictionary<long, string> getItensDefault()
        {
            return Camada_de_Dados.DataBaseCommunicator.FuncsToDataBase.selectItensDefault();
        }

        public static Dictionary<long, string> getTodosItens()
        {
            return Camada_de_Dados.DataBaseCommunicator.FuncsToDataBase.selectAllItens();
        }

        public static Dictionary<long,string> adicionaItensNovos(List<string> it)
        {
            Dictionary<long,string> ss = new Dictionary<long,string>();
            foreach (string s in it)
            {
                long cod = Camada_de_Dados.DataBaseCommunicator.FuncsToDataBase.insertItem(s);
                ss.Add(cod,s);
            }
            return ss;
        }

        public static List<Zona> adicionaZonasNovas(List<string> zn)
        {
            List<Zona> zonas = new List<Zona>();
            foreach (string s in zn)
            {
                long cod = Camada_de_Dados.DataBaseCommunicator.FuncsToDataBase.insertZona(s);
                Zona z = new Zona(cod, s);
                zonas.Add(z);
            }
            return zonas;
        }

        public static bool podeAdicionarAnalise(long codigoProjecto, string nomeAnalise)
        {
            return Camada_de_Dados.ETdA.ETdA.getProjecto(codigoProjecto).podeAdicionarAnalise(nomeAnalise);
        }

        public static List<Item> getItensAnalise(long codigoProjecto, long codigoAnalise)
        {
            return Camada_de_Dados.ETdA.ETdA.getProjecto(codigoProjecto).Analises[codigoAnalise].Itens;
        }

        public static List<Zona> getZonasAnalise(long codigoProjecto, long codigoAnalise)
        {
            return Camada_de_Dados.ETdA.ETdA.getProjecto(codigoProjecto).Analises[codigoAnalise].Zonas;
        }

        public static List<string> getNomeZonasAnalise(long codigoProjecto, long codigoAnalise)
        {
            List<Zona> zs = Camada_de_Dados.ETdA.ETdA.getProjecto(codigoProjecto).Analises[codigoAnalise].Zonas;
            List<string> ss = new List<string>();
            foreach (Zona z in zs)
                ss.Add(z.Nome);
            return ss;
        }

        public static string getTipoAnalise(long codigoProjecto, long codigoAnalise)
        {
            return Camada_de_Dados.ETdA.ETdA.getProjecto(codigoProjecto).Analises[codigoAnalise].Tipo;
        }

        public static void removerAnalise(long codigoProjecto, long codigoAnalise)
        {
           Camada_de_Dados.ETdA.ETdA.getProjecto(codigoProjecto).removeAnalise(codigoAnalise);
        }

    }
}
