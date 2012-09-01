
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ETdAnalyser.CamadaDados.DataBaseCommunicator;
using ETdAnalyser.CamadaDados.Classes;
using System.Windows.Forms;

namespace ETdAnalyser.Camada_de_Negócio
{
    class ImporterExporter
    {
		static public bool importarCheckList(String path,bool temCabecalho, long codigoAnalise, long[] items, long[] zonas)	{
			Resposta modelo = new Resposta();
			modelo.CodigoAnalise = codigoAnalise;
			return importaFicheiro(path,temCabecalho,null, modelo,items,zonas,null);
		}

		/**
		 * @param zonas das fichas de avaliação pela ordem do ficheiro
		 */
		static public bool importarFichaAvaliacao(String path, bool temCabecalho, Resposta modelo, List<PerguntaFichaAvaliacao> perguntas, long[] zonas)
		{
			return importaFicheiro(path, temCabecalho, null, modelo, null, zonas, perguntas);
		}

		static public bool importarQuestionarios(String path, bool temCabecalho, Resposta modelo,
			List<PerguntaQuestionario> perguntas)
		{
			return importaFicheiro(path, temCabecalho, perguntas, modelo, null, null, null);
		}

		#region importer!
		/**
		 * Le um ficheiro escalaResposta coloca os dados na base de dados
		 * Se nao conseguir retorna false, se conseguir retorna true
		 * 
		 * Na resosta modelo devem ser preenchidos TODOS os campos relativos à resposta,
		 * todos os outros devem ser -1 ou null
		 */
		private static bool importaFicheiro(String datapathFile, bool temCabecalho, List<PerguntaQuestionario> perguntasQuestionario,
			Resposta modelo, long[] items, long[] zonas, List<PerguntaFichaAvaliacao> perguntasFA)
        {
            #region normalAproach
			
			LinkedList<string[]> dados = new LinkedList<string[]>();
			string linha;
			StreamReader streamReader = new StreamReader(datapathFile);

			char[] separator = { ';' };
			if (temCabecalho)
			{
				// le a primeira linha
				linha = streamReader.ReadLine();
			}

			// le a primeira linha de dados
			while((linha = streamReader.ReadLine()) != null)
			{
				string[] terms = linha.Split(separator);
				string[] aux = new string[terms.Length];
				for (int i = 0; i < terms.Length; i++)
					aux[i] = terms[i];

				dados.AddLast(aux);
			}

			// close the stream
			streamReader.Close();

            #endregion
			List<Resposta> respostas = new List<Resposta>();

			switch (modelo.Tipo)	{
				case 3: // Questionario
					int i;
					bool jump = false; // para salta campos
					foreach (string[] linhaD in dados)
					{
                        i = 0;
						Questionario questionario = new Questionario();
						questionario.CodigoAnalise = modelo.CodigoAnalise;
						//questionario.CodQuestionario = FuncsToDataBase.InsertQuestionario(questionario);

						foreach (string campo in linhaD)
						{
							if (!jump)
							{
								
								PerguntaQuestionario perguntaReferente = perguntasQuestionario.ElementAt(i);

								Resposta resposta = new Resposta(modelo);
								//resposta.CodigoQuestionario = questionario.CodQuestionario;
								resposta.NumeroPergunta = (float)perguntaReferente.NumeroPergunta;
								resposta.CodigoZona = perguntaReferente.CodigoZona;
								// Aqui tem questionario se colocar a zona especial(reservada) que diz questionario a zona esta 
								// na resposta do cliente, escalaResposta no caso do ficheiro, vem no campo seguinte
								if (perguntaReferente.CodigoZona == -1)
								{
									resposta.CodigoZona = Convert.ToInt32(linhaD[i + 1]);
									jump = true;
								}

								resposta.CodigoPergunta = perguntaReferente.CodigoPergunta;
								resposta.Valor = Convert.ToInt16(linhaD[i]);

                                MessageBox.Show(resposta.ToString());
								FuncsToDataBase.InsertRespostaQuestionario(resposta);
							}
							else jump = false;
							i++;
						}
					}
					break;
				case 2: // Ficha Avaliacao
					int j = 0;
					jump = false; // para salta campos
					foreach (string[] linhaD in dados)
					{
						FichaAvaliacao fichaAvaliacao = new FichaAvaliacao();
						fichaAvaliacao.CodigoZona = zonas[j];
						fichaAvaliacao.CodigoAnalise = modelo.CodigoAnalise;
						FuncsToDataBase.InsertFichaAvaliacao(fichaAvaliacao);

						i = 0;
						foreach (string campo in linhaD)
						{
							PerguntaFichaAvaliacao perguntaReferente = perguntasFA.ElementAt<PerguntaFichaAvaliacao>(i);
							Resposta resposta = new Resposta(modelo);
							resposta.CodigoFichaAvaliacao = fichaAvaliacao.CodigoFichaAvaliacao;
							resposta.NumeroPergunta = (float)perguntaReferente.NumeroPergunta;

							resposta.CodigoPergunta = perguntaReferente.CodigoPergunta;
							resposta.Valor = Convert.ToInt16(linhaD[i]);

							FuncsToDataBase.InsertRespostaFichaAvaliacao(resposta);
							i++;
						}
						j++;
					}
					break;
				case 1: // CheckList
					i = 0;
					j = 0;
					jump = false; // para salta campos
					foreach (string[] linhaD in dados)
					{
						foreach (string campo in linhaD)
						{
							Resposta resposta = new Resposta();
							resposta.CodigoAnalise = modelo.CodigoAnalise;
							resposta.CodigoZona = zonas[j];
							resposta.CodigoItem = items[i];

							resposta.Valor = Convert.ToInt16(linhaD[i]);

							FuncsToDataBase.InsertRespostaCheckList(resposta);

							i++;
						}
						j++;
					}
					break;
			}

			return true;
        }
		
		#endregion

		#region exporter!!
		protected static float extrairNumeroPergunta(Resposta resposta)	{
			return resposta.NumeroPergunta;
		}
		protected static long extractCodQuestionario(Resposta resposta)
		{
			return resposta.CodigoQuestionario;
		}

		public static void exportaQuestionariosParaFicheiro(String datapathFile, long codigoAnalise)
		{
			StreamWriter outputStream = new StreamWriter(datapathFile);

			List<Resposta> respostas = new List<Resposta>();
			FuncsToDataBase.SelectRespostaQuestionario(codigoAnalise, respostas);

			respostas.OrderBy<Resposta, float>(extrairNumeroPergunta);
			respostas.OrderBy<Resposta, long>(extractCodQuestionario);
			long ultimoCodQuestionario = 0;
			bool fst = true;
			foreach (Resposta resposta in respostas)
			{
				if (fst)
				{
					ultimoCodQuestionario = resposta.CodigoQuestionario;
					fst = false;
				}
				if (ultimoCodQuestionario == resposta.CodigoQuestionario) // se for um elemento intermedio
				{
					switch (resposta.GetTipoResposta)
					{
						case Resposta.TipoResposta.RespostaNum:
							outputStream.Write(resposta.Valor + ";");
							break;
						case Resposta.TipoResposta.RespostaStr:
						case Resposta.TipoResposta.RespostaMemo:
							outputStream.Write(resposta.ValorString + ";");
							break;
					}
				}
				else
				{
					switch (resposta.GetTipoResposta)
					{
						case Resposta.TipoResposta.RespostaNum:
							outputStream.WriteLine(resposta.Valor);
							break;
						case Resposta.TipoResposta.RespostaStr:
						case Resposta.TipoResposta.RespostaMemo:
							outputStream.WriteLine(resposta.ValorString);
							break;
					}
				}
			}
		}
		protected static long extrairCodigoFichaAvaliacao(Resposta resposta)
		{
			return resposta.CodigoFichaAvaliacao;
		}
		public static void exportaFichasDeAvaliacaoParaFicheiro(String datapathFile, long codigoAnalise)
		{
			StreamWriter outputStream = new StreamWriter(datapathFile);

			List<Resposta> respostas = new List<Resposta>();
			FuncsToDataBase.SelectRespostaFichaAvaliacao(codigoAnalise, respostas);

			respostas.OrderBy<Resposta, float>(extrairNumeroPergunta);
			respostas.OrderBy<Resposta, long>(extrairCodigoFichaAvaliacao);
			long ultimoCodigoFichaAvaliacao = 0;
			bool fst = true;
			foreach (Resposta resposta in respostas)
			{
				if (fst)
				{
					ultimoCodigoFichaAvaliacao = resposta.CodigoFichaAvaliacao;
					fst = false;
				}
				if (ultimoCodigoFichaAvaliacao == resposta.CodigoFichaAvaliacao) // se for um elemento intermedio
				{
					switch (resposta.GetTipoResposta)
					{
						case Resposta.TipoResposta.RespostaNum:
							outputStream.Write(resposta.Valor + ";");
							break;
						case Resposta.TipoResposta.RespostaStr:
						case Resposta.TipoResposta.RespostaMemo:
							outputStream.Write(resposta.ValorString + ";");
							break;
					}
				}
				else
				{
					switch (resposta.GetTipoResposta)
					{
						case Resposta.TipoResposta.RespostaNum:
							outputStream.WriteLine(resposta.Valor);
							break;
						case Resposta.TipoResposta.RespostaStr:
						case Resposta.TipoResposta.RespostaMemo:
							outputStream.WriteLine(resposta.ValorString);
							break;
					}
				}
			}
		}
		protected static long extractItem(Resposta resposta)
		{
			return resposta.CodigoItem;
		}
		protected static long extractZona(Resposta resposta)
		{
			return resposta.CodigoZona;
		}
		public static void exportaCheckListParaFicheiro(String datapathFile, long codigoAnalise)
		{
			StreamWriter streamWriter = new StreamWriter(datapathFile);

			List<Resposta> respostas = new List<Resposta>();
			FuncsToDataBase.SelectRespostaCheckList(codigoAnalise, respostas);

			respostas.OrderBy<Resposta, long>(extractItem);
			respostas.OrderBy<Resposta, long>(extractZona);
			long ultimoItem = 0;
			bool fst = true;
			foreach (Resposta resposta in respostas)
			{
				if (fst)
				{
					ultimoItem = resposta.CodigoItem;
					fst = false;
				}
				if (ultimoItem == resposta.CodigoItem) // se for um elemento intermedio
				{
					switch (resposta.GetTipoResposta)
					{
						case Resposta.TipoResposta.RespostaNum:
							streamWriter.Write(resposta.Valor + ";");
							break;
						default:
							throw new Exception();
					}
				}
				else
				{
					switch (resposta.GetTipoResposta)
					{
						case Resposta.TipoResposta.RespostaNum:
							streamWriter.WriteLine(resposta.Valor);
							break;
					}
				}
			}
		}

		#endregion
	}
}
