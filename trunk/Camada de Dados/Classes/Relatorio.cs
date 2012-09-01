//using System;
//using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System;


namespace ETdAnalyser.CamadaDados.Classes
{
    class Relatorio
    {
        private Dictionary<long, Dictionary<long, Classes.ResultadoItem>> listaResultados;
        private Double[] listaEstatisticas;
        private string filename;
        private int numeroRespostas;

        //Constructores
        public Relatorio(Dictionary<long, Dictionary<long, Classes.ResultadoItem>> listaResultados, Double[] listaEstatisticas)
        {
            this.listaResultados = listaResultados;
            this.listaEstatisticas = listaEstatisticas;

        }

        public Relatorio()
        {
            this.listaResultados = new Dictionary<long, Dictionary<long, Classes.ResultadoItem>>();
            this.listaEstatisticas = new Double[10];
        }


        //Métodos
        public Dictionary<long, Dictionary<long, Classes.ResultadoItem>> ListaResultados
        {
            get { return listaResultados; }
            set { listaResultados = value; }
        }

        public Double[] ListaEstatisticas
        {
            get { return listaEstatisticas; }
            set { listaEstatisticas = value; }
        }

        public String Filename
        {
            get { return filename; }
            set { filename = value; }
        }
        
        public int NumeroRespostas
        {
            get { return numeroRespostas; }
            set { numeroRespostas = value; }
        }

        public void GerarResultadosRelatorio(long codigoAnalise, List<Classes.Resposta> respostas, List<Classes.Zona> zonas, List<Classes.Item> itens)
        {
            DataBaseCommunicator.FuncsToDataBase.SelectRespostaCheckList(codigoAnalise, respostas);
            numeroRespostas += respostas.Count;
            DataBaseCommunicator.FuncsToDataBase.SelectRespostaFichaAvaliacao(codigoAnalise, respostas);
            numeroRespostas += respostas.Count;
            DataBaseCommunicator.FuncsToDataBase.SelectRespostaQuestionario(codigoAnalise, respostas);
            numeroRespostas += respostas.Count;

            Dictionary<int, double> resultado_questionario_parcial;
            int numero_total_questionario;
            Dictionary<int, double> resultado_fichaAvaliacao_parcial;
            int numero_total_fichaAvaliacao;
            Dictionary<int, double> resultado_checklist_parcial;
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
                    
                    numero_total_questionario = 0;
                    numero_total_fichaAvaliacao = 0;
                    numero_total_checklist = 0;

                    Dictionary<int, double> resultadoItem_questionario_parcial = new Dictionary<int, double>();
                    double resultadoItem_questionario_total = 0;
                    Dictionary<int, double> resultadoItem_fichaAvaliacao_parcial = new Dictionary<int, double>();
                    double resultadoItem_fichaAvaliacao_total = 0;
                    Dictionary<int, double> resultadoItem_checklist_parcial = new Dictionary<int, double>();
                    double resultadoItem_checklist_total = 0;

                    for (int i = 1; i < 6; i++)
                    {
                        resultadoItem_questionario_parcial.Add(i, 0);
                        resultadoItem_fichaAvaliacao_parcial.Add(i, 0);
                        resultadoItem_checklist_parcial.Add(i, 0);
                    }


                    double resultadoItem_geral = -1;

                    //para cada resposta
                    foreach (Classes.Resposta resposta in respostas)
                    {
                        //se a resposta é relativa a esta zona escalaResposta a este item escalaResposta se é do tipo inteiro(os outros nao sao relevantes para o calculo das cores)
                        if (zona.Codigo == resposta.CodigoZona && item.CodigoItem == resposta.CodigoItem && resposta.GetTipoResposta == CamadaDados.Classes.Resposta.TipoResposta.RespostaNum)
                        {
                            //se a resposta é relativa a um questionário
                            if (resposta.CodigoFichaAvaliacao == -1 && resposta.CodigoCheckList ==-1)
                            {
                                //numero total de respostas possiveis
                                int total = CamadaDados.DataBaseCommunicator.FuncsToDataBase.NumeroEscalaResposta(codigoAnalise, resposta.NumeroPergunta, 3);

                                //incremento do numero de resposta da cor respectiva
                                if (resposta.Valor <= item.IntervaloVermelho)
                                    resultadoItem_questionario_parcial[1]++;
                                else if (resposta.Valor <= item.IntervaloLaranja)
                                    resultadoItem_questionario_parcial[2]++;
                                else if (resposta.Valor <= item.IntervaloAmarelo)
                                    resultadoItem_questionario_parcial[3]++;
                                else if (resposta.Valor <= item.IntervaloVerdeLima)
                                    resultadoItem_questionario_parcial[4]++;
                                else if (resposta.Valor <= item.IntervaloVerde)
                                    resultadoItem_questionario_parcial[5]++;
                                
                                //incremento do valor da resposta
                                resultadoItem_questionario_total += (5 * resposta.Valor) / total;

                                //incremento do numero de respostas do tipo questionario
                                numero_total_questionario++;
                            }
                            //se a resposta é relativa a uma ficha de avaliação
                            else if (resposta.CodigoCheckList == -1 && resposta.CodigoQuestionario ==-1)
                            {
                                //numero total de respostas possiveis
                                int total = CamadaDados.DataBaseCommunicator.FuncsToDataBase.NumeroEscalaResposta(codigoAnalise, resposta.NumeroPergunta, 2);

                                //incremento do numero de resposta da cor respectiva
                                if (resposta.Valor <= item.IntervaloVermelho)
                                    resultadoItem_fichaAvaliacao_parcial[1]++;
                                else if (resposta.Valor <= item.IntervaloLaranja)
                                    resultadoItem_fichaAvaliacao_parcial[2]++;
                                else if (resposta.Valor <= item.IntervaloAmarelo)
                                    resultadoItem_fichaAvaliacao_parcial[3]++;
                                else if (resposta.Valor <= item.IntervaloVerdeLima)
                                    resultadoItem_fichaAvaliacao_parcial[4]++;
                                else if (resposta.Valor <= item.IntervaloVerde)
                                    resultadoItem_fichaAvaliacao_parcial[5]++;

                                //incremento do valor da resposta
                                resultadoItem_fichaAvaliacao_total += (5 * resposta.Valor) / total;

                                //incremento do numero de respostas do tipo ficha de avaliacao
                                numero_total_fichaAvaliacao++;
                            }
                            //se a resposta é relativa a um checklist
                            else if (resposta.CodigoFichaAvaliacao == -1 && resposta.CodigoQuestionario == -1)
                            {
                                resultadoItem_checklist_total = resposta.Valor;
                            }

                        }

                    }
                    

                    //atribuição das percentagens das respostas a cada um dos valores da escala em cada um dos tipos de formulário
                    for (int i = 1; i < 6; i++)
                    {
                        if (resultadoItem_questionario_parcial[i] != 0)
                            resultadoItem_questionario_parcial[i] = (resultadoItem_questionario_parcial[i] / numero_total_questionario);

                        if (resultadoItem_fichaAvaliacao_parcial[i] != 0)
                            resultadoItem_fichaAvaliacao_parcial[i]= ( resultadoItem_fichaAvaliacao_parcial[i] / numero_total_fichaAvaliacao);
                    }

                    if(numero_total_questionario!=0)
                        resultadoItem_questionario_total = (resultadoItem_questionario_total / numero_total_questionario);
                    if(numero_total_fichaAvaliacao!=0)
                        resultadoItem_fichaAvaliacao_total = (resultadoItem_fichaAvaliacao_total / numero_total_fichaAvaliacao);

                    //caso a nota do analista seja inferior ao limite imposto por este o resultado é a nota do analista
                    if (resultadoItem_checklist_total <= item.LimiteInferiorAnalista)
                        resultadoItem_geral = resultadoItem_checklist_total;
                    // caso contrario é a ponderacao de todas as dimensoes
                    else
                        resultadoItem_geral = (resultadoItem_questionario_total * item.PonderacaoCliente) + (resultadoItem_fichaAvaliacao_total * item.PonderacaoProfissional) + (resultadoItem_checklist_total * item.PonderacaoAnalista);

                    resultadoItem = new Classes.ResultadoItem(resultadoItem_questionario_total, resultadoItem_questionario_parcial, resultadoItem_fichaAvaliacao_total, resultadoItem_fichaAvaliacao_parcial, resultadoItem_checklist_total, resultadoItem_checklist_parcial, "", resultadoItem_geral);
                    listaItens.Add(item.CodigoItem, resultadoItem);

                    //Console.WriteLine(item.NomeItem + "codigo: " + item.CodigoItem);
                    //Console.WriteLine("RESULTADO: " + resultadoItem_questionario_total.ToString() + " -" + " -" + resultadoItem_fichaAvaliacao_total.ToString() + " -" + " -" + resultadoItem_checklist_total.ToString() + " -" + " -" + resultadoItem_geral.ToString());
                    //Console.WriteLine("RESULTADO ITEM GERAL = " + "(" + resultadoItem_questionario_total.ToString() + " * " + item.PonderacaoCliente.ToString() + ")" + " (" + resultadoItem_fichaAvaliacao_total.ToString() + " * " + item.PonderacaoProfissional.ToString() + ") " + " (" + resultadoItem_checklist_total.ToString() + " * " + item.PonderacaoAnalista.ToString() + ")");
                }
                listaResultados.Add(zona.Codigo, listaItens);

                //Console.WriteLine(zona.Nome + "codigo: " + zona.CodigoTipo);
            }
        }

        public void GerarEstatisticasRelatorioSexo(long codigoAnalise)
        {
            int respostasF = 0, respostasM = 0;
            double percentagemF = 0.0, percentagemM = 0.0;
            List<int> respostas = CamadaDados.DataBaseCommunicator.FuncsToDataBase.SelectRespostasSexo(codigoAnalise);
            for (int i = 0; i < respostas.Count(); i++)
            {
                if (respostas[i] == 1) respostasF++;
                else if (respostas[i] == 2) respostasM++;
            }

            if (respostas.Count() != 0)
            {
                percentagemF = ((double)respostasF / (double)respostas.Count()) * 100; 
                percentagemM = ((double)respostasM / (double)respostas.Count()) * 100; 
            }
            listaEstatisticas[0] = (percentagemF);
            listaEstatisticas[1] = (percentagemM);
                       

        }

        public void GerarEstatisticasIdade(long codigoAnalise)
        {
            int menores20 = 0; int entre20e30 = 0; int entre30e40 = 0; int entre40e50 = 0; int entre50e60 = 0; int maioresde60 = 0;
            List<int> idades = CamadaDados.DataBaseCommunicator.FuncsToDataBase.SelectIdades(codigoAnalise);
            for (int i = 0; i < idades.Count(); i++)
            {
                if (idades[i] < 20) menores20++;
                else if (idades[i] >= 20 && idades[i] < 30) entre20e30++;
                else if (idades[i] >= 30 && idades[i] < 40) entre30e40++;
                else if (idades[i] >= 40 && idades[i] < 50) entre40e50++;
                else if (idades[i] >= 50 && idades[i] < 60) entre50e60++;
                else if (idades[i] >= 60 ) maioresde60++;
            }

            listaEstatisticas[2] = (menores20);
            listaEstatisticas[3] = (entre20e30);
            listaEstatisticas[4] = (entre30e40);
            listaEstatisticas[5] = (entre40e50);
            listaEstatisticas[6] = (entre50e60);
            listaEstatisticas[7] = (maioresde60);
            
        }

        public void GerarEstatisticasClienteHabitual(long codigoAnalise)
        {
            int sim = 0; int nao = 0;
            double percentagemsim = 0.0; double percentagemnao = 0.0;
            List<int> respostas = CamadaDados.DataBaseCommunicator.FuncsToDataBase.SelectRespostasHabitual(codigoAnalise);

            for (int i = 0; i < respostas.Count(); i++)
            {
                if (respostas[i] == 1) sim++;
                else if (respostas[i] == 2) nao++;
            }

            percentagemsim = ((double)sim / (double)respostas.Count());
            percentagemnao = ((double)nao / (double)respostas.Count());

            listaEstatisticas[8] = percentagemsim;
            listaEstatisticas[9] = percentagemnao;
        }
    }
}
