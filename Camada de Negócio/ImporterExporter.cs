
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ETdAnalyser.Camada_de_Dados.DataBaseCommunicator;
using ETdAnalyser.Camada_de_Dados.Classes;
using System.Windows.Forms;

namespace ETdAnalyser.Camada_de_Negócio
{
    class ImporterExporter
    {
		static public bool importarCheckList(String path,bool temCabecalho, long cod_analise, long[] items, long[] zonas)	{
			Resposta modelo = new Resposta();
			modelo.Cod_analise = cod_analise;
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
		 * Le um ficheiro e coloca os dados na base de dados
		 * Se nao conseguir retorna false, se conseguir retorna true
		 * 
		 * Na resosta modelo devem ser preenchidos TODOS os campos relativos à resposta,
		 * todos os outros devem ser -1 ou null
		 */
		private static bool importaFicheiro(String datapathFile, bool temCabecalho, List<PerguntaQuestionario> perguntasQ,
			Resposta modelo, long[] items, long[] zonas, List<PerguntaFichaAvaliacao> perguntasFA)
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
					int i;
					bool jump = false; // para salta campos
					foreach (string[] linhaD in dados)
					{
                        i = 0;
						Questionario q = new Questionario();
						q.CodAnalise = modelo.Cod_analise;
						//q.CodQuestionario = FuncsToDataBase.insertQuestionario(q);

						foreach (string campo in linhaD)
						{
							if (!jump)
							{
								
								PerguntaQuestionario perguntaReferente = perguntasQ.ElementAt(i);

								Resposta resposta = new Resposta(modelo);
								//resposta.CodigoQuestionario = q.CodQuestionario;
								resposta.NumeroPergunta = (float)perguntaReferente.Num_Pergunta;
								resposta.CodigoZona = perguntaReferente.Cod_zona;
								// Aqui tem q se colocar a zona especial(reservada) que diz q a zona esta 
								// na resposta do cliente, e no caso do ficheiro, vem no campo seguinte
								if (perguntaReferente.Cod_zona == -1)
								{
									resposta.CodigoZona = Convert.ToInt32(linhaD[i + 1]);
									jump = true;
								}

								resposta.Cod_pergunta = perguntaReferente.Cod_Pergunta;
								resposta.Valor = Convert.ToInt16(linhaD[i]);

                                MessageBox.Show(resposta.ToString());
								FuncsToDataBase.insertRespostaQuestionario(resposta);
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
						FichaAvaliacao fa = new FichaAvaliacao();
						fa.CodZona = zonas[j];
						fa.CodAnalise = modelo.Cod_analise;
						FuncsToDataBase.insertFichaAvaliacao(fa);

						i = 0;
						foreach (string campo in linhaD)
						{
							PerguntaFichaAvaliacao perguntaReferente = perguntasFA.ElementAt<PerguntaFichaAvaliacao>(i);
							Resposta resposta = new Resposta(modelo);
							resposta.CodigoFichaAvaliacao = fa.CodFichaAvaliacao;
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
					i = 0;
					j = 0;
					jump = false; // para salta campos
					foreach (string[] linhaD in dados)
					{
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
		
		#endregion

		#region exporter!!
		protected static float extractNrPergunta(Resposta r)	{
			return r.NumeroPergunta;
		}
		protected static long extractCodQuestionario(Resposta r)
		{
			return r.Cod_questionario;
		}

		public static void exportaQuestionariosParaFicheiro(String datapathFile, long codAnalise)
		{
			StreamWriter outputStream = new StreamWriter(datapathFile);

			List<Resposta> respostas = new List<Resposta>();
			FuncsToDataBase.selectRespostaQuestionario(codAnalise, respostas);

			respostas.OrderBy<Resposta, float>(extractNrPergunta);
			respostas.OrderBy<Resposta, long>(extractCodQuestionario);
			long ultimoCodQuestionario = 0;
			bool fst = true;
			foreach (Resposta r in respostas)
			{
				if (fst)
				{
					ultimoCodQuestionario = r.Cod_questionario;
					fst = false;
				}
				if (ultimoCodQuestionario == r.Cod_questionario) // se for um elemento intermedio
				{
					switch (r.Tipo_Resposta)
					{
						case Resposta.TipoResposta.RespostaNum:
							outputStream.Write(r.Valor + ";");
							break;
						case Resposta.TipoResposta.RespostaStr:
						case Resposta.TipoResposta.RespostaMemo:
							outputStream.Write(r.ValorString + ";");
							break;
					}
				}
				else
				{
					switch (r.Tipo_Resposta)
					{
						case Resposta.TipoResposta.RespostaNum:
							outputStream.WriteLine(r.Valor);
							break;
						case Resposta.TipoResposta.RespostaStr:
						case Resposta.TipoResposta.RespostaMemo:
							outputStream.WriteLine(r.ValorString);
							break;
					}
				}
			}
		}
		protected static long extractCodFA(Resposta r)
		{
			return r.Cod_fichaAvaliacao;
		}
		public static void exportaFichasDeAvaliacaoParaFicheiro(String datapathFile, long codAnalise)
		{
			StreamWriter outputStream = new StreamWriter(datapathFile);

			List<Resposta> respostas = new List<Resposta>();
			FuncsToDataBase.selectRespostaFichaAvaliacao(codAnalise, respostas);

			respostas.OrderBy<Resposta, float>(extractNrPergunta);
			respostas.OrderBy<Resposta, long>(extractCodFA);
			long ultimoCodFichaAvaliacao = 0;
			bool fst = true;
			foreach (Resposta r in respostas)
			{
				if (fst)
				{
					ultimoCodFichaAvaliacao = r.Cod_fichaAvaliacao;
					fst = false;
				}
				if (ultimoCodFichaAvaliacao == r.Cod_fichaAvaliacao) // se for um elemento intermedio
				{
					switch (r.Tipo_Resposta)
					{
						case Resposta.TipoResposta.RespostaNum:
							outputStream.Write(r.Valor + ";");
							break;
						case Resposta.TipoResposta.RespostaStr:
						case Resposta.TipoResposta.RespostaMemo:
							outputStream.Write(r.ValorString + ";");
							break;
					}
				}
				else
				{
					switch (r.Tipo_Resposta)
					{
						case Resposta.TipoResposta.RespostaNum:
							outputStream.WriteLine(r.Valor);
							break;
						case Resposta.TipoResposta.RespostaStr:
						case Resposta.TipoResposta.RespostaMemo:
							outputStream.WriteLine(r.ValorString);
							break;
					}
				}
			}
		}
		protected static long extractItem(Resposta r)
		{
			return r.CodigoItem;
		}
		protected static long extractZona(Resposta r)
		{
			return r.CodigoZona;
		}
		public static void exportaCheckListParaFicheiro(String datapathFile, long codAnalise)
		{
			StreamWriter outputStream = new StreamWriter(datapathFile);

			List<Resposta> respostas = new List<Resposta>();
			FuncsToDataBase.selectRespostaCheckList(codAnalise, respostas);

			respostas.OrderBy<Resposta, long>(extractItem);
			respostas.OrderBy<Resposta, long>(extractZona);
			long ultimoItem = 0;
			bool fst = true;
			foreach (Resposta r in respostas)
			{
				if (fst)
				{
					ultimoItem = r.CodigoItem;
					fst = false;
				}
				if (ultimoItem == r.CodigoItem) // se for um elemento intermedio
				{
					switch (r.Tipo_Resposta)
					{
						case Resposta.TipoResposta.RespostaNum:
							outputStream.Write(r.Valor + ";");
							break;
						default:
							throw new Exception();
					}
				}
				else
				{
					switch (r.Tipo_Resposta)
					{
						case Resposta.TipoResposta.RespostaNum:
							outputStream.WriteLine(r.Valor);
							break;
					}
				}
			}
		}

		#endregion
	}
}
