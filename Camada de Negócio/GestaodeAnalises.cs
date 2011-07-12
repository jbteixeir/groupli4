using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ETdA.Camada_de_Dados.Classes;
using ETdA.Camada_de_Dados.DataBaseCommunicator;

namespace ETdA.Camada_de_Negócio
{
    class GestaodeAnalises
    {
		enum TipoAnalise { AreaComum, Actividade, Zona };

		//Métodos
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

        public static long abreAnalise(long cp, string na){
            return Camada_de_Dados.ETdA.ETdA.getProjecto(cp).abreAnalise(na);
        }

        public static List<string> getNomeAnalises(long cod)
        {
            return new List<string>(Camada_de_Dados.ETdA.ETdA.getProjecto(cod).Cod_Name_Analise.Values);
        }

        public static void adicionaAnalise(Analise a, long codProjecto){
            Camada_de_Dados.ETdA.ETdA.getProjecto(codProjecto).adicionaNovaAnalise(a.Tipo, a.Nome, a.Zonas, a.Itens);
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

        public static long getCodAnalise(long codProjecto,string nomeAnalise)
        {
            return Camada_de_Dados.ETdA.ETdA.getProjecto(codProjecto).getCodigoAnalise(nomeAnalise);
        }

        public static bool podeAdicionarAnalise(long codProjecto, string nomeAnalise)
        {
            return Camada_de_Dados.ETdA.ETdA.getProjecto(codProjecto).podeAdicionarAnalise(nomeAnalise);
        }

        public static List<Item> getItensAnalise(long codProjecto, long codAnalise)
        {
            return Camada_de_Dados.ETdA.ETdA.getProjecto(codProjecto).Analises[codAnalise].Itens;
        }

        public static List<Zona> getZonasAnalise(long codProjecto, long codAnalise)
        {
            return Camada_de_Dados.ETdA.ETdA.getProjecto(codProjecto).Analises[codAnalise].Zonas;
        }

        public static List<string> getNomeZonasAnalise(long codProjecto, long codAnalise)
        {
            List<Zona> zs = Camada_de_Dados.ETdA.ETdA.getProjecto(codProjecto).Analises[codAnalise].Zonas;
            List<string> ss = new List<string>();
            foreach (Zona z in zs)
                ss.Add(z.Nome);
            return ss;
        }

        public static string getTipoAnalise(long codProjecto, long codAnalise)
        {
            return Camada_de_Dados.ETdA.ETdA.getProjecto(codProjecto).Analises[codAnalise].Tipo;
        }

        public static Dictionary<string, TipoEscala> getHipResposta()
        {
            return FuncsToDataBase.getTiposResposta();
        }
    }
}
