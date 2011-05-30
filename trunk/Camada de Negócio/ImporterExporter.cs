
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ETdA.Camada_de_Dados.DataBaseCommunicator;
using ETdA.Camada_de_Dados.Classes;

namespace CamadaNegocio
{
    class ImporterExporter
    {
		static public bool importarCheckList(String path,bool temCabecalho, long cod_analise, int[] items, int[] zonas)	{
			Resposta modelo = new Resposta();
			modelo.Cod_analise = cod_analise;
			return importaFicheiro(path,temCabecalho,null, modelo,items,zonas,null);
		}
		/**
		 * @param zonas das fichas de avaliação pela ordem do ficheiro
		 */
		static public bool importarFichaAvaliacao(String path, bool temCabecalho, Resposta modelo, List<PerguntaFichaAvaliacao> perguntas, int[] zonas)
		{
			return importaFicheiro(path, temCabecalho, null, modelo, null, zonas, perguntas);
		}

		static public bool importarQuestionarios(String path, bool temCabecalho, Resposta modelo,
			List<PerguntaQuestionario> perguntas)
		{
			return importaFicheiro(path, temCabecalho, perguntas, modelo, null, null, null);
		}
		/**
		 * Le um ficheiro e coloca os dados na base de dados
		 * Se nao conseguir retorna false, se conseguir retorna true
		 * 
		 * Na resosta modelo devem ser preenchidos TODOS os campos relativos à resposta,
		 * todos os outros devem ser -1 ou null
		 */
		private static bool importaFicheiro(String datapathFile, bool temCabecalho, List<PerguntaQuestionario> perguntasQ,
			Resposta modelo, int[] items, int[] zonas, List<PerguntaFichaAvaliacao> perguntasFA)
        {
            #region normalAproach
			
			LinkedList<string[]> dados = new LinkedList<string[]>();
			string linha;
			StreamReader tr = new StreamReader(datapathFile);

			char[] separator = { ';' };
			if (temCabecalho)
			{
				// le a primeira linha
				linha = tr.ReadLine();
			}

			// le a primeira linha de dados
			while((linha = tr.ReadLine()) != null)
			{
				string[] terms = linha.Split(separator);
				string[] aux = new string[terms.Length];
				for (int i = 0; i < terms.Length; i++)
					aux[i] = terms[i];

				dados.AddLast(aux);
			}

			// close the stream
			tr.Close();

            #endregion
			List<Resposta> respostas = new List<Resposta>();

			switch (modelo.Tipo)	{
				case 3: // Questionario
					long codFormulario = modelo.CodigoQuestionario;
					int i = 0;
					bool jump = false; // para salta campos
					foreach (string[] linhaD in dados)
					{
						codFormulario++;

						Questionario q = new Questionario();
						q.CodAnalise = modelo.Cod_analise;
						q.CodQuestionario = codFormulario;
						FuncsToDataBase.insertQuestionario(q);

						foreach (string campo in linhaD)
						{
							if (!jump)
							{
								
								PerguntaQuestionario perguntaReferente = perguntasQ.ElementAt<PerguntaQuestionario>(i);

								Resposta resposta = new Resposta(modelo);
								resposta.CodigoQuestionario = codFormulario;
								resposta.NumeroPergunta = (float)perguntaReferente.Num_Pergunta;
								resposta.CodigoZona = perguntaReferente.Cod_zona;
								// Aqui tem q se colocar a zona especial(reservada) que diz q a zona esta 
								// na resposta do cliente, e no caso do ficheiro, vem no campo seguinte
								if (perguntaReferente.Cod_zona == null)
								{
									resposta.CodigoZona = Convert.ToInt32(linhaD[i + 1]);
									jump = true;
								}

								resposta.Cod_pergunta = perguntaReferente.Cod_Pergunta;
								resposta.Valor = Convert.ToInt16(linhaD[i]);

								FuncsToDataBase.insertRespostaQuestionario(resposta);
							}
							else jump = false;
							i++;
						}
					}
					break;
				case 2: // Ficha Avaliacao
					codFormulario = modelo.CodigoFichaAvaliacao;
					int j = 0;
					jump = false; // para salta campos
					foreach (string[] linhaD in dados)
					{
						FichaAvaliacao fa = new FichaAvaliacao();
						fa.CodFichaAvaliacao = codFormulario;
						fa.CodZona = zonas[j];
						fa.CodAnalise = modelo.Cod_analise;
						FuncsToDataBase.insertFichaAvaliacao(fa);

						codFormulario++;
						i = 0;
						foreach (string campo in linhaD)
						{
							PerguntaFichaAvaliacao perguntaReferente = perguntasFA.ElementAt<PerguntaFichaAvaliacao>(i);
							Resposta resposta = new Resposta(modelo);
							resposta.CodigoFichaAvaliacao = (int)codFormulario;
							resposta.NumeroPergunta = (float)perguntaReferente.Num_Pergunta;

							resposta.Cod_pergunta = perguntaReferente.Cod_Pergunta;
							resposta.Valor = Convert.ToInt16(linhaD[i]);

							FuncsToDataBase.insertRespostaFichaAvaliacao(resposta);
							i++;
						}
						j++;
					}
					break;
				case 1: // CheckList
					codFormulario = (int) modelo.Cod_checklist;
					i = 0;
					j = 0;
					jump = false; // para salta campos
					foreach (string[] linhaD in dados)
					{
						codFormulario++;
						foreach (string campo in linhaD)
						{
							Resposta resposta = new Resposta();
							resposta.Cod_analise = modelo.Cod_analise;
							resposta.CodigoZona = zonas[j];
							resposta.CodigoItem = items[i];

							resposta.Valor = Convert.ToInt16(linhaD[i]);

							FuncsToDataBase.insertRespostaCheckList(resposta);

							i++;
						}
						j++;
					}
					break;
			}

			return true;
        }

		/*public static void exportaParaFicheiro(String datapathFile, List<Formulario> formulario)
		{

		}*/
    }
}
