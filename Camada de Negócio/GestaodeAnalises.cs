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

//        void editaAnalise(String codAnalise, string nomeAnalise);
//        void removeAnalise(String codAnalise);
//        void adicionaItemAnalise(String codParametro);
//        void removeItemAnalise(String codParametro);
//        void adicionaPerguntaFormulario(String codAnalise, String codFormulario, short numPergunta, String codParametro, String descricao, String escalaResp);
//        void editaPerguntaFormulario(String codAnalise, String codFormulario, short numPergunta, String codParametro, String descricao, String escalaResp);
//        void adicionaFormulario(String codAnalise, Formulario form);
//        void editaFormulario(String codAnalise, String codFormulario, Formulario form);
//        void removeFormulario(String codAnalise, String codFormulario);
//        void geraWebsite();
//        void geraFormularioOnline();

        public static void abreAnalise(string np, string na){
            long codp = GestaodeProjectos.getCodProjecto(np);

            Camada_de_Dados.ETdA.ETdA.getProjecto(codp).abreAnalise(na);
        }

        public static List<string> getNomeAnalises(string nomeProjecto)
        {
            long cod = Camada_de_Dados.ETdA.ETdA.abreProjecto(nomeProjecto);
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
    }
}
