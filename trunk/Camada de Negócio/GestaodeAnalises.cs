using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ETdAnalyser.CamadaDados.Classes;
using ETdAnalyser.CamadaDados.DataBaseCommunicator;

namespace ETdAnalyser.Camada_de_Negócio
{
    class GestaodeAnalises
    {
        enum TipoAnalise { AreaComum, Actividade, Zona };

        //Métodos

        // s_final
        public static Dictionary<long,string> getCodeNomeAnalises(long codigoProjectoroj)
        {
            if (CamadaDados.ETdA.ETdA.getProjecto(codigoProjectoroj) == null)
                GestaodeProjectos.abreProjecto(codigoProjectoroj);

            return CamadaDados.ETdA.ETdA.getProjecto(codigoProjectoroj).CodigoNomeAnalise;
        }

        // s_final
        public static void abreAnalise(long cp, long ca)
        {
            CamadaDados.ETdA.ETdA.getProjecto(cp).AbrirAnalise(ca);
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
                FuncsToDataBase.UpdateItemAnalise(i);
        }

        public static void adicionaAnalise(Analise a, long codigoProjecto){
            CamadaDados.ETdA.ETdA.getProjecto(codigoProjecto).AdicionarAnalise(a.Tipo, a.Nome, a.Zonas, a.Itens);
        }

        public static Dictionary<long, string> getItensDefault()
        {
            return CamadaDados.DataBaseCommunicator.FuncsToDataBase.SelectItensDefault();
        }

        public static Dictionary<long, string> getTodosItens()
        {
            return CamadaDados.DataBaseCommunicator.FuncsToDataBase.SelectAllItens();
        }

        public static Dictionary<long,string> adicionaItensNovos(List<string> it)
        {
            Dictionary<long,string> ss = new Dictionary<long,string>();
            foreach (string s in it)
            {
                long cod = CamadaDados.DataBaseCommunicator.FuncsToDataBase.insertItem(s);
                ss.Add(cod,s);
            }
            return ss;
        }

        public static List<Zona> adicionaZonasNovas(List<string> zn)
        {
            List<Zona> zonas = new List<Zona>();
            foreach (string s in zn)
            {
                long cod = CamadaDados.DataBaseCommunicator.FuncsToDataBase.InsertZona(s);
                Zona z = new Zona(cod, s);
                zonas.Add(z);
            }
            return zonas;
        }

        public static bool podeAdicionarAnalise(long codigoProjecto, string nomeAnalise)
        {
            return CamadaDados.ETdA.ETdA.getProjecto(codigoProjecto).PodeAdicionarAnalise(nomeAnalise);
        }

        public static List<Item> getItensAnalise(long codigoProjecto, long codigoAnalise)
        {
            return CamadaDados.ETdA.ETdA.getProjecto(codigoProjecto).Analises[codigoAnalise].Itens;
        }

        public static List<Zona> getZonasAnalise(long codigoProjecto, long codigoAnalise)
        {
            return CamadaDados.ETdA.ETdA.getProjecto(codigoProjecto).Analises[codigoAnalise].Zonas;
        }

        public static List<string> getNomeZonasAnalise(long codigoProjecto, long codigoAnalise)
        {
            List<Zona> zs = CamadaDados.ETdA.ETdA.getProjecto(codigoProjecto).Analises[codigoAnalise].Zonas;
            List<string> ss = new List<string>();
            foreach (Zona z in zs)
                ss.Add(z.Nome);
            return ss;
        }

        public static string getTipoAnalise(long codigoProjecto, long codigoAnalise)
        {
            return CamadaDados.ETdA.ETdA.getProjecto(codigoProjecto).Analises[codigoAnalise].Tipo;
        }

        public static void removerAnalise(long codigoProjecto, long codigoAnalise)
        {
           CamadaDados.ETdA.ETdA.getProjecto(codigoProjecto).RemoveAnalise(codigoAnalise);
        }

    }
}
