using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ETdAnalyser.Camada_de_Negócio;
using System.Windows;

namespace ETdAnalyser.CamadaDados.Classes
{
    class Exporter
    {
        #region Variaveis de instancia

        /* Localização onde vai ser gravado o ficheiro */
        private string caminho;
        /* Ficheiro que vai ser gravado */
        StreamWriter ficheiro;

        /* Perguntas dos questionários para construir o cabeçalho nos questionários */
        private List<PerguntaQuestionario> perguntasQuestionarios;
        /* Itens para construir o cabeçaho nas fichas de avaliação escalaResposta checklist */
        private List<Item> items;
        /* Zonas para se forem necessários nos questionários */
        private List<Zona> zonas;

        // matriz com valores de cada formulario-item/zona-actividade
        private Dictionary<int, string[]> valores;

        // correspondência das perguntas ou itens para a coluna da matriz
        private Dictionary<long, int> colunaPergunta;

        /* Formulários a serem gravados */
        // Questionários
        private List<Questionario> questionarios;
        // Fichas de avaliação
        private List<FichaAvaliacao> fichasAvaliacao;
        // CheckList
        private CheckList checklist;

        #endregion

        #region Construtores

        /* Construtor para questionários */
        public Exporter(string caminho, List<Questionario> questionarios, List<PerguntaQuestionario> perguntasQuestionarios, List<Zona> zonas)
        {
            this.caminho = caminho;
            this.questionarios = questionarios;
            this.perguntasQuestionarios = perguntasQuestionarios;
            this.zonas = zonas;
            this.items = null;
            this.valores = null;
            this.fichasAvaliacao = null;
            this.checklist = null;
        }

        /* Construtor para fichas de avaliação */
        public Exporter(string caminho, List<FichaAvaliacao> fichasAvaliacao, List<Item> items, List<Zona> zonas)
        {
            this.caminho = caminho;
            this.fichasAvaliacao = fichasAvaliacao;
            this.items = items;
            this.zonas = zonas;
            this.perguntasQuestionarios = null;
            this.valores = null;
            this.questionarios = null;
            this.checklist = null;
        }

        /* Construtor para a checklist */
        public Exporter(string caminho, CheckList checklist, List<Item> items, List<Zona> zonas)
        {
            this.caminho = caminho;
            this.checklist = checklist;
            this.items = items;
            this.zonas = zonas;
            this.perguntasQuestionarios = null;
            this.valores = null;
            this.questionarios = null;
            this.fichasAvaliacao = null;
        }

        #endregion

        #region Métodos

        /* Verifica se é possível criar o ficheiro */
        //@param erro Mensagem do erro caso existente
        //@return Boolean Se o ficheiro foi criado ou não
        public Boolean VerificaCriacaoFicheiro(ref string erro)
        {
            try
            {
                ficheiro = new StreamWriter(caminho);
                return true;
            }
            catch (Exception e)
            {
                erro = e.Message;
                return false;
            }
        }

        #region Questionario

        /* Função que grava os questionários num ficheiro 
         * primeiro é necessário obter o número de colunas da matriz
         * segundo é necessário obter a primeira linha da matriz - o cabecalho do ficheiro 
         * terceiro preencher a matriz
         * por fim gravar o ficheiro
         */
        public void GravaQuestionarios()
        {
            valores = new Dictionary<int, string[]>();

            int num_colunas = getNumeroColunasQuestionario();
            valores[0] = getCabecalhoQuestionario(num_colunas);

            for (int i = 0; i < questionarios.Count; i++)
                valores.Add(i + 1, getCamposQuestionario(questionarios[i],num_colunas));

            GravaFicheiro();
        }

        /* Retorna o número de colunas/respostas que irá ter no ficheiro */
        //@return int Número de colunas necessárias
        private int getNumeroColunasQuestionario()
        {
            int conta = 0;
            List<int> grupos_perguntas_classificacao = new List<int>();
            foreach (PerguntaQuestionario p in perguntasQuestionarios)
            {
                TipoEscala ti = GestaodeRespostas.getTipoEscala(p.CodigoTipoEscala);
                if (ti.Numero >= 0) // apenas 1 resposta
                    conta++;
                else if (ti.Numero == -2) // várias opções de resposta
                    conta+=ti.Respostas.Count;
                else // classificacao
                {
                    if (!grupos_perguntas_classificacao.Contains((int)p.NumeroPergunta))
                        grupos_perguntas_classificacao.Add((int)p.NumeroPergunta);
                    else
                        conta++;
                }

                // Zona é colocada na pergunta
                if (p.CodigoZona <= 0)
                    conta++;
            }
            return conta;
        }

        /* Retorna a primeira linha do ficheiro - o cabecalho para questionarios
         * Também preenche o dictionary com a correspondencia da pergunta para a coluna 
         */
        // @param numeroColunas Numero de colunas que o ficheiro irá ter
        // @return string[] Os valores de cada coluna da primeira linha do ficheiro
        private string[] getCabecalhoQuestionario(int num_colunas)
        {
            string[] cabecalho = new string[num_colunas];
            colunaPergunta = new Dictionary<long, int>();

            List<PerguntaQuestionario> pergs = new List<PerguntaQuestionario>();
            foreach (PerguntaQuestionario p in perguntasQuestionarios)
                pergs.Add(p);

            for (int index = 0 ; index < num_colunas ; index++)
            {
                PerguntaQuestionario mais_pequena = pergs[0];
                for (int i = 1 ; i < pergs.Count ; i++)
                {
                    if (pergs[i].NumeroPergunta < mais_pequena.NumeroPergunta)
                        mais_pequena = pergs[i];
                }

                TipoEscala ti = GestaodeRespostas.getTipoEscala(mais_pequena.CodigoTipoEscala);
                if (ti.Numero >= 0) // apenas 1 resposta
                {
                    cabecalho[index] = mais_pequena.TipoQuestao + mais_pequena.NumeroPergunta;
                    colunaPergunta.Add(mais_pequena.CodigoPergunta, index);
                }
                else if (ti.Numero == -2) // várias opções de resposta
                {
                    colunaPergunta.Add(mais_pequena.CodigoPergunta, index);
                    for (int resp = 0; resp < ti.Respostas.Count; resp++, index++)
                        cabecalho[index] = mais_pequena.TipoQuestao + (mais_pequena.NumeroPergunta + ((1 + resp) * 0.01)).ToString();
                    index--;
                }
                else // classificacao
                {
                    if (mais_pequena.NumeroPergunta > (int)mais_pequena.NumeroPergunta) // tem uma resposta
                    {
                        cabecalho[index] = mais_pequena.TipoQuestao + mais_pequena.NumeroPergunta;
                        colunaPergunta.Add(mais_pequena.CodigoPergunta, index);
                    }
                    else
                        index--;
                }

                // Zona é colocada na resposta
                if (mais_pequena.CodigoZona <= 0)
                {
                    index++;
                    cabecalho[index] = (mais_pequena.NumeroPergunta + 0.1).ToString() + "-Zona";
                }

                pergs.Remove(mais_pequena);
            }

            return cabecalho;
        }

        /* Retorna as várias repostas do questionario */
        // @param q Questionário de onde se retiram as respostas
        // @param numeroColunas Numero de colunas que o ficheiro irá ter
        // @return string[] As várias respostas do questionário
        private string[] getCamposQuestionario(Questionario q, int num_colunas)
        {
            string[] campos = new string[num_colunas];

            #region Respostas Numero
            foreach (Resposta r in q.RespostasNumero)
            {
                if (colunaPergunta.Keys.Contains(r.CodigoPergunta))
                {
                    int coluna_correspondente = colunaPergunta[r.CodigoPergunta];
                    PerguntaQuestionario pergunta_correspondente = getPerguntaPorCodigo(r.CodigoPergunta);
                    TipoEscala ti = GestaodeRespostas.getTipoEscala(pergunta_correspondente.CodigoTipoEscala);

                    if (ti.Numero >= 0) // só uma resposta
                        campos[coluna_correspondente] = r.Valor.ToString();
                    else if (ti.Numero == -2) // várias opçoes
                    {
                        coluna_correspondente += (r.Valor - 1);
                        campos[coluna_correspondente] = "1";
                    }
                    else // classificacao
                        campos[coluna_correspondente] = r.Valor.ToString();

                    // Zona é colocada na resposta
                    if (pergunta_correspondente.CodigoZona <= 0)
                    {
                        string nome = "";
                        campos[coluna_correspondente + 1] = getIndexZona(r.CodigoZona, ref nome).ToString() + "-" + nome;
                    }
                }
            }
            #endregion

            #region Respostas String
            foreach (Resposta r in q.RespostasString)
            {
                if (colunaPergunta.Keys.Contains(r.CodigoPergunta))
                {
                    int coluna_correspondente = colunaPergunta[r.CodigoPergunta];
                    campos[coluna_correspondente] = r.ValorString;
                }
            }
            #endregion

            #region Respostas Memo
            foreach (Resposta r in q.RespostasMemo)
            {
                if (colunaPergunta.Keys.Contains(r.CodigoPergunta))
                {
                    int coluna_correspondente = colunaPergunta[r.CodigoPergunta];
                    campos[coluna_correspondente] = r.ValorString;
                }
            }
            #endregion

            return campos;
        }

        #endregion

        #region Ficha Avaliacao

        /* Função que grava as fichas de avaliacao num ficheiro 
         * primeiro é necessário obter o número de colunas da matriz
         * segundo é necessário obter a primeira linha da matriz - o cabecalho do ficheiro 
         * terceiro preencher a matriz
         * por fim gravar o ficheiro
         */
        public void GravaFichaAvaliacao()
        {
            valores = new Dictionary<int, string[]>();

            int num_colunas = items.Count + 2;
            valores[0] = getCabecalhoFichaAvaliacao(num_colunas);

            for (int i = 0; i < fichasAvaliacao.Count; i++)
                valores[i + 1] = getCamposFichaAvaliacao(fichasAvaliacao[i], num_colunas);

            GravaFicheiro();
        }

        /* Retorna a primeira linha do ficheiro - o cabecalho para fichas de avaliacao
         * Também preenche o dictionary com a correspondencia do item para a coluna 
         */
        // @param numeroColunas Numero de colunas que o ficheiro irá ter
        // @return string[] Os valores de cada coluna da primeira linha do ficheiro
        private string[] getCabecalhoFichaAvaliacao(int numeroColunas)
        {
            string[] cabecalho = new string[numeroColunas];
            colunaPergunta = new Dictionary<long, int>();

            cabecalho[0] = "Zonas";
            for (int i = 0; i < items.Count; i++)
            {
                cabecalho[i + 1] = i.ToString() + "-" + items[i].NomeItem;
                colunaPergunta.Add(items[i].CodigoItem, i + 1);
            }
            cabecalho[numeroColunas - 1] = "Sugestões";

            return cabecalho;
        }

        /* Retorna as várias repostas da ficha de avaliacao */
        // @param fa Ficha de avaliacao de onde se retiram as respostas
        // @param numeroColunas Numero de colunas que o ficheiro irá ter
        // @return string[] As várias respostas da ficha de avaliacao
        private string[] getCamposFichaAvaliacao(FichaAvaliacao fa, int num_colunas)
        {
            string[] campos = new string[num_colunas];

            string nome_zona = "";
            campos[0] = getIndexZona(fa.CodigoZona, ref nome_zona).ToString() + "-" + nome_zona;

            #region Respostas Numero
            foreach (Resposta r in fa.RespostasNumero)
            {
                if (colunaPergunta.Keys.Contains(r.CodigoItem))
                {
                    int coluna_correspondente = colunaPergunta[r.CodigoItem];
                    campos[coluna_correspondente] = r.Valor.ToString();
                }
            }
            #endregion

            #region Respostas String
            foreach (Resposta r in fa.RespostasString)
            {
                if (colunaPergunta.Keys.Contains(r.CodigoItem))
                {
                    int coluna_correspondente = colunaPergunta[r.CodigoPergunta];
                    campos[coluna_correspondente] = r.ValorString;
                }
            }
            #endregion

            #region Respostas Memo
            foreach (Resposta r in fa.RespostasMemo)
            {
                if (colunaPergunta.Keys.Contains(r.CodigoItem))
                {
                    int coluna_correspondente = colunaPergunta[r.CodigoPergunta];
                    campos[coluna_correspondente] = r.ValorString;
                }
            }
            #endregion

            return campos;
        }

        #endregion

        #region CheckList

        /* Função que grava a checklist num ficheiro 
         * primeiro é necessário obter o número de colunas da matriz
         * segundo é necessário obter a primeira linha da matriz - o cabecalho do ficheiro 
         * terceiro preencher a matriz
         * por fim gravar o ficheiro
         */
        public void GravaChecklist()
        {
            valores = new Dictionary<int, string[]>();

            valores[0] = getCabecalhoCheckList();

            string[][] matriz_checklist = getCamposCheckList();

            for (int i = 0; i < matriz_checklist.Length; i++)
                valores[i + 1] = matriz_checklist[i];

            GravaFicheiro();
        }

        /* Retorna a primeira linha do ficheiro - o cabecalho para fichas de avaliacao
         * Também preenche o dictionary com a correspondencia do item para a coluna 
         */
        // @return string[] Os valores de cada coluna da primeira linha do ficheiro
        private string[] getCabecalhoCheckList()
        {
            string[] cabecalho = new string[items.Count+1];
            colunaPergunta = new Dictionary<long, int>();

            cabecalho[0] = "Zonas";
            for (int i = 0; i < items.Count; i++)
            {
                cabecalho[i + 1] = i.ToString() + "-" + items[i].NomeItem;
                colunaPergunta.Add(items[i].CodigoItem, i + 1);
            }

            return cabecalho;
        }
    
        /* Retorna a matriz com todas as repostas de cada item para cada zona da checklist */
        // @return As várias respostas da checklist
        private string[][] getCamposCheckList()
        {
            /* Inicializar o array */
            string[][] campos = new string[zonas.Count][];
            for (int i = 0; i < campos.Length; i++)
                campos[i] = new string[items.Count + 1];

            /* Colocar na primeira coluna da matriz a zona escalaResposta o seu nome */
            string nome_zona = "";
            for (int i = 0 ; i < zonas.Count ; i++)
                campos[i][0] = getIndexZona(zonas[i].Codigo, ref nome_zona).ToString() + "-" + nome_zona;

            foreach (Resposta r in checklist.RespostasNumero)
            {
                if (colunaPergunta.Keys.Contains(r.CodigoItem))
                {
                    int coluna_correspondente = colunaPergunta[r.CodigoItem];
                    campos[getIndexZona(r.CodigoZona, ref nome_zona)-1][coluna_correspondente] = r.Valor.ToString();
                }
            }

            return campos;
        }

        #endregion

        /* Funcao que grava o ficheiro 
         * A matriz já está toda preenchida é só necessário colocar ';' entre os campos
         */
        private void GravaFicheiro()
        {
            for (int i = 0; i < valores.Keys.Count; i++)
            {
                string[] linha = valores[i];
                int j = 0;
                string linha_ficheiro = "";
                for (; j < linha.Length - 1; j++)
                {
                    if (linha[j] != null)
                       linha_ficheiro += (linha[j] + ";");
                    else
                        linha_ficheiro += (";");
                }
                ficheiro.WriteLine(linha_ficheiro + linha[j]);
            }
            ficheiro.Close();
        }

        #region Cenas

        private PerguntaQuestionario getPerguntaPorCodigo(long codigo)
        {
            PerguntaQuestionario p = null;
            bool encontrado = false;
            for (int i = 0; i < perguntasQuestionarios.Count && !encontrado; i++)
                if (perguntasQuestionarios[i].CodigoPergunta == codigo)
                {
                    p = perguntasQuestionarios[i];
                    encontrado = true;
                }
            return p;
        }

        private int getIndexZona(long codZona, ref string nomeZona)
        {
            int index = 0;
            bool encontrado = false;
            for (int i = 0 ; i < zonas.Count && !encontrado ; i++ )
                if (zonas[i].Codigo == codZona)
                {
                    encontrado = true;
                    index = i + 1;
                    nomeZona = zonas[i].Nome;
                }
            return index;
        }

        #endregion

        #endregion
    }
}
