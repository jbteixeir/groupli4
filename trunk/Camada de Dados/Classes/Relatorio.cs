//using System;
//using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System;
using Word = Microsoft.Office.Interop.Word;
using System.Reflection;

namespace ETdA.Camada_de_Dados.Classes
{
    class Relatorio
    {
        private Dictionary<long, Dictionary<long, Classes.ResultadoItem>> listaResultados;
        private List<double> listaEstatisticas;

        //Constructores
        public Relatorio(Dictionary<long, Dictionary<long, Classes.ResultadoItem>> listaResultados, List<double> listaEstatisticas)
        {
            this.listaResultados = listaResultados;
            this.listaEstatisticas = listaEstatisticas;
        }

        public Relatorio()
        {
            this.listaResultados = new Dictionary<long, Dictionary<long, Classes.ResultadoItem>>();
            this.listaEstatisticas = new List<double>();
        }


        //Métodos
        public Dictionary<long, Dictionary<long, Classes.ResultadoItem>> ListaResultados
        {
            get { return listaResultados; }
            set { listaResultados = value; }
        }

        public List<double> ListaEstatisticas
        {
            get { return listaEstatisticas; }
            set { listaEstatisticas = value; }
        }


        public void gerarResultadosRelatorio(int codigoAnalise, List<Classes.Resposta> respostas, List<Classes.Zona> zonas, List<Classes.Item> itens)
        {
            DataBaseCommunicator.FuncsToDataBase.selectRespostaCheckList(codigoAnalise, respostas);
            DataBaseCommunicator.FuncsToDataBase.selectRespostaFichaAvaliacao(codigoAnalise, respostas);
            DataBaseCommunicator.FuncsToDataBase.selectRespostaQuestionario(codigoAnalise, respostas);

            Dictionary<int, int> resultado_questionario_parcial;
            int numero_total_questionario;
            Dictionary<int, int> resultado_fichaAvaliacao_parcial;
            int numero_total_fichaAvaliacao;
            Dictionary<int, int> resultado_checklist_parcial;
            int numero_total_checklist;


            //para cada zona
            foreach (Classes.Zona zona in zonas)
            {
                Dictionary<long, Classes.ResultadoItem> listaItens = new Dictionary<long, Classes.ResultadoItem>();
                //para cada item
                foreach (Classes.Item item in itens)
                {
                    Classes.ResultadoItem resultadoItem;
                    //inicializar contagens
                    resultado_questionario_parcial = new Dictionary<int, int>();
                    numero_total_questionario = 0;
                    resultado_fichaAvaliacao_parcial = new Dictionary<int, int>();
                    numero_total_fichaAvaliacao = 0;
                    resultado_checklist_parcial = new Dictionary<int, int>();
                    numero_total_checklist = 0;
                    for (int i = 0; i < 5; i++)
                    {
                        resultado_questionario_parcial.Add(i, 0);
                        resultado_fichaAvaliacao_parcial.Add(i, 0);
                        resultado_checklist_parcial.Add(i, 0);
                    }

                    //para cada resposta
                    foreach (Classes.Resposta resposta in respostas)
                    {
                        //se a resposta é relativa a esta zona e a este item
                        if (zona.Codigo == resposta.CodigoZona && item.CodigoItem == resposta.CodigoItem)
                        {
                            //se a resposta é relativa a um questionário
                            if (resposta.CodigoQuestionario != -1)
                            {
                                resultado_questionario_parcial[resposta.Valor] = resultado_questionario_parcial[resposta.Valor]++;
                                numero_total_questionario++;
                            }
                            //se a resposta é relativa a uma ficha de avaliação
                            if (resposta.CodigoFichaAvaliacao != -1)
                            {
                                resultado_fichaAvaliacao_parcial[resposta.Valor] = resultado_fichaAvaliacao_parcial[resposta.Valor]++;
                                numero_total_fichaAvaliacao++;
                            }
                            //se a resposta é relativa a um checklist
                            else
                            {
                                resultado_checklist_parcial[resposta.Valor] = resultado_checklist_parcial[resposta.Valor]++;
                                numero_total_checklist++;
                            }

                        }

                    }
                    Dictionary<int, float> resultadoItem_questionario_parcial = new Dictionary<int, float>();
                    float resultadoItem_questionario_total = -1;
                    Dictionary<int, float> resultadoItem_fichaAvaliacao_parcial = new Dictionary<int, float>();
                    float resultadoItem_fichaAvaliacao_total = -1;
                    Dictionary<int, float> resultadoItem_checklist_parcial = new Dictionary<int, float>();
                    float resultadoItem_checklist_total = -1;

                    float resultadoItem_geral = -1;

                    //atribuição das percentagens das respostas a cada um dos valores da escala em cada um dos tipos de formulário
                    for (int i = 0; i < 5; i++)
                    {
                        resultadoItem_questionario_parcial.Add(i, resultado_questionario_parcial[i] / numero_total_questionario);
                        resultadoItem_questionario_total += i * (resultado_questionario_parcial[i] / numero_total_questionario);

                        resultadoItem_fichaAvaliacao_parcial.Add(i, resultado_fichaAvaliacao_parcial[i] / numero_total_fichaAvaliacao);
                        resultadoItem_fichaAvaliacao_total += i * (resultado_fichaAvaliacao_parcial[i] / numero_total_fichaAvaliacao);

                        resultadoItem_checklist_parcial.Add(i, resultado_checklist_parcial[i] / numero_total_checklist);
                        resultadoItem_checklist_total += i * (resultado_checklist_parcial[i] / numero_total_checklist);
                    }

                    //caso a nota do analista seja inferior ao limite imposto por este o resultado é a nota do analista
                    if(resultadoItem_checklist_total <= item.LimiteInferiorAnalista)
                        resultadoItem_geral =(resultadoItem_checklist_total*item.PonderacaoAnalista);
                    // caso contrario é a ponderacao de todas as dimensoes
                    else
                        resultadoItem_geral = (resultadoItem_questionario_total * item.PonderacaoCliente) + (resultadoItem_fichaAvaliacao_total * item.PonderacaoProfissional) + (resultadoItem_checklist_total * item.PonderacaoAnalista);


                    resultadoItem = new Classes.ResultadoItem(resultadoItem_questionario_total,resultadoItem_questionario_parcial,resultadoItem_questionario_total,resultadoItem_questionario_parcial,resultadoItem_checklist_total,resultadoItem_checklist_parcial,"",resultadoItem_geral);
                    listaItens.Add(item.CodigoItem, resultadoItem);
                    listaResultados.Add(zona.Codigo, listaItens);
                }
            }
        }
    }
}
