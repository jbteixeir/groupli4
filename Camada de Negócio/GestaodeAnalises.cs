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

        public static List<string> getNomeAnalises(){
            return Camada_de_Dados.ETdA.ETdA.Projecto.Analises;
        }
		public void criaAnalise(long codProjecto, String nomeAnalise, TipoAnalise tipo)
		{
			DateTime now = DateTime.Now;
			DataBaseCommunicator.query("INSERT INTO analise VALUES ("+codProjecto+","
				+ "CAST('"+now.Year+now.Month+now.Day+" 00:00:00' AS datetime), '"+
				nomeAnalise+"', "+getDescricao(tipo)+",0,0,0);");
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

        public static List<Item> getListaItens()
        {
            return new List<Item>();
        }

        public static List<Zona> getListaZona()
        {
            return new List<Zona>();
        }

        public static Dictionary<string, string> getItensDefault()
        {
            return Camada_de_Dados.DataBaseCommunicator.FuncsToDataBase.selectItensDefault();
        }

        public static Dictionary<string, string> getTodosItens()
        {
            return Camada_de_Dados.DataBaseCommunicator.FuncsToDataBase.selectAllItens();
        }
    }
}
