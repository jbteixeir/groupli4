//using System;
//using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System;

namespace ETdA.Camada_de_Dados
{
    class Relatorio
    {
        private Dictionary<int, Dictionary<int, Classes.ResultadoItem>> listaResultados;

        //Constructores
        public Relatorio(Dictionary<int, Dictionary<int, Classes.ResultadoItem>> listaResultados)
        {
            this.listaResultados = listaResultados;
        }

        public Relatorio()
        {
            this.listaResultados = new Dictionary<int, Dictionary<int, Classes.ResultadoItem>>();
        }


        //Métodos
        public Dictionary<int, Dictionary<int, Classes.ResultadoItem>> ListaResultados
        {
            get { return listaResultados; }
            set { listaResultados = value; }
        }


        public void gerarResultadosRelatorio(int codigoAnalise, List<Classes.Resposta> respostas, List<Classes.Zona> zonas, List<Classes.Item> itens)
        {
            DataBaseCommunicator.FuncsToDataBase.selectRespostaCheckList(codigoAnalise, respostas);
            DataBaseCommunicator.FuncsToDataBase.selectRespostaFichaAvaliacao(codigoAnalise, respostas);
            DataBaseCommunicator.FuncsToDataBase.selectRespostaQuestionario(codigoAnalise, respostas);

            //para cada zona
            foreach (Classes.Zona zona in zonas)
            {
                //para cada item
                foreach (Classes.Item item in itens)
                {
                    //para cada resposta
                    foreach (Classes.Resposta resposta in respostas)
                    {
                        //se a resposta é relativa a esta zona e a este item
                        if (zona.Codigo == resposta.CodigoZona && item.CodigoItem == resposta.CodigoItem)
                        {
                            //se a resposta é relativa a um questionário
                            if (resposta.CodigoQuestionario != -1)
                            {

                            }
                            //se a resposta é relativa a uma ficha de avaliação
                            if (resposta.CodigoFichaAvaliacao != -1)
                            {

                            }
                            //se a resposta é relativa a um checklist
                            else
                            {

                            }

                        }
                    }

                }
            }
        }
    }
}
