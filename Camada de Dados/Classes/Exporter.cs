using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ETdAnalyser.Camada_de_Negócio;
using System.Windows;

namespace ETdAnalyser.Camada_de_Dados.Classes
{
    class Exporter
    {
        #region Variaveis de instancia

        /* Localização onde vai ser gravado o ficheiro */
        private string caminho;
        /* Ficheiro que vai ser gravado */
        StreamWriter ficheiro;

        /* Perguntas dos questionários para construir o cabeçalho nos questionários */
        private List<PerguntaQuestionario> perguntas_questionarios;
        /* Itens para construir o cabeçaho nas fichas de avaliação e checklist */
        private List<Item> items;
        /* Zonas para se forem necessários nos questionários */
        private List<Zona> zonas;

        // matriz com valores de cada formulario-item/zona-actividade
        private Dictionary<int, string[]> valores;

        // correspondência das perguntas ou itens para a coluna da matriz
        private Dictionary<long, int> coluna_pergunta;

        /* Formulários a serem gravados */
        // Questionários
        private List<Questionario> questionarios;
        // Fichas de avaliação
        private List<FichaAvaliacao> fichas_avaliacao;
        // CheckList
        private CheckList checklist;

        #endregion

        #region Construtores

        /* Construtor para questionários */
        public Exporter(string _caminho, List<Questionario> _questionarios, List<PerguntaQuestionario> _perguntas_questionarios, List<Zona> _zonas)
        {
            caminho = _caminho;
            questionarios = _questionarios;
            perguntas_questionarios = _perguntas_questionarios;
            zonas = _zonas;
            items = null;
            valores = null;
            fichas_avaliacao = null;
            checklist = null;
        }

        /* Construtor para fichas de avaliação */
        public Exporter(string _caminho, List<FichaAvaliacao> _fichas_avaliacao, List<Item> _items, List<Zona> _zonas)
        {
            caminho = _caminho;
            fichas_avaliacao = _fichas_avaliacao;
            items = _items;
            zonas = _zonas;
            perguntas_questionarios = null;
            valores = null;
            questionarios = null;
            checklist = null;
        }

        /* Construtor para a checklist */
        public Exporter(string _caminho, CheckList _checklist, List<Item> _items, List<Zona> _zonas)
        {
            caminho = _caminho;
            checklist = _checklist;
            items = _items;
            zonas = _zonas;
            perguntas_questionarios = null;
            valores = null;
            questionarios = null;
            fichas_avaliacao = null;
        }

        #endregion

        #region Métodos

        /* Verifica se é possível criar o ficheiro */
        //@param erro Mensagem do erro caso existente
        //@return Boolean Se o ficheiro foi criado ou não
        public Boolean verifica_criacao_ficheiro(ref string erro)
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
        public void grava_questionarios()
        {
            valores = new Dictionary<int, string[]>();

            int num_colunas = getNumeroColunasQuestionario();
            valores[0] = getCabecalhoQuestionario(num_colunas);

            for (int i = 0; i < questionarios.Count; i++)
                valores.Add(i + 1, getCamposQuestionario(questionarios[i],num_colunas));

            gravaFicheiro();
        }

        /* Retorna o número de colunas/respostas que irá ter no ficheiro */
        //@return int Número de colunas necessárias
        private int getNumeroColunasQuestionario()
        {
            int conta = 0;
            List<int> grupos_perguntas_classificacao = new List<int>();
            foreach (PerguntaQuestionario p in perguntas_questionarios)
            {
                TipoEscala ti = GestaodeRespostas.getTipoEscala(p.Cod_TipoEscala);
                if (ti.Numero >= 0) // apenas 1 resposta
                    conta++;
                else if (ti.Numero == -2) // várias opções de resposta
                    conta+=ti.Respostas.Count;
                else // classificacao
                {
                    if (!grupos_perguntas_classificacao.Contains((int)p.Num_Pergunta))
                        grupos_perguntas_classificacao.Add((int)p.Num_Pergunta);
                    else
                        conta++;
                }

                // Zona é colocada na pergunta
                if (p.Cod_zona <= 0)
                    conta++;
            }
            return conta;
        }

        /* Retorna a primeira linha do ficheiro - o cabecalho para questionarios
         * Também preenche o dictionary com a correspondencia da pergunta para a coluna 
         */
        // @param num_colunas Numero de colunas que o ficheiro irá ter
        // @return string[] Os valores de cada coluna da primeira linha do ficheiro
        private string[] getCabecalhoQuestionario(int num_colunas)
        {
            string[] cabecalho = new string[num_colunas];
            coluna_pergunta = new Dictionary<long, int>();

            List<PerguntaQuestionario> pergs = new List<PerguntaQuestionario>();
            foreach (PerguntaQuestionario p in perguntas_questionarios)
                pergs.Add(p);

            for (int index = 0 ; index < num_colunas ; index++)
            {
                PerguntaQuestionario mais_pequena = pergs[0];
                for (int i = 1 ; i < pergs.Count ; i++)
                {
                    if (pergs[i].Num_Pergunta < mais_pequena.Num_Pergunta)
                        mais_pequena = pergs[i];
                }

                TipoEscala ti = GestaodeRespostas.getTipoEscala(mais_pequena.Cod_TipoEscala);
                if (ti.Numero >= 0) // apenas 1 resposta
                {
                    cabecalho[index] = mais_pequena.TipoQuestao + mais_pequena.Num_Pergunta;
                    coluna_pergunta.Add(mais_pequena.Cod_Pergunta, index);
                }
                else if (ti.Numero == -2) // várias opções de resposta
                {
                    coluna_pergunta.Add(mais_pequena.Cod_Pergunta, index);
                    for (int resp = 0; resp < ti.Respostas.Count; resp++, index++)
                        cabecalho[index] = mais_pequena.TipoQuestao + (mais_pequena.Num_Pergunta + ((1 + resp) * 0.01)).ToString();
                    index--;
                }
                else // classificacao
                {
                    if (mais_pequena.Num_Pergunta > (int)mais_pequena.Num_Pergunta) // tem uma resposta
                    {
                        cabecalho[index] = mais_pequena.TipoQuestao + mais_pequena.Num_Pergunta;
                        coluna_pergunta.Add(mais_pequena.Cod_Pergunta, index);
                    }
                    else
                        index--;
                }

                // Zona é colocada na resposta
                if (mais_pequena.Cod_zona <= 0)
                {
                    index++;
                    cabecalho[index] = (mais_pequena.Num_Pergunta + 0.1).ToString() + "-Zona";
                }

                pergs.Remove(mais_pequena);
            }

            return cabecalho;
        }

        /* Retorna as várias repostas do questionario */
        // @param q Questionário de onde se retiram as respostas
        // @param num_colunas Numero de colunas que o ficheiro irá ter
        // @return string[] As várias respostas do questionário
        private string[] getCamposQuestionario(Questionario q, int num_colunas)
        {
            string[] campos = new string[num_colunas];

            #region Respostas Numero
            foreach (Resposta r in q.Respostas_Numero)
            {
                if (coluna_pergunta.Keys.Contains(r.Cod_pergunta))
                {
                    int coluna_correspondente = coluna_pergunta[r.Cod_pergunta];
                    PerguntaQuestionario pergunta_correspondente = getPerguntaByCod(r.Cod_pergunta);
                    TipoEscala ti = GestaodeRespostas.getTipoEscala(pergunta_correspondente.Cod_TipoEscala);

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
                    if (pergunta_correspondente.Cod_zona <= 0)
                    {
                        string nome = "";
                        campos[coluna_correspondente + 1] = getIndexZona(r.CodigoZona, ref nome).ToString() + "-" + nome;
                    }
                }
            }
            #endregion

            #region Respostas String
            foreach (Resposta r in q.Respostas_String)
            {
                if (coluna_pergunta.Keys.Contains(r.Cod_pergunta))
                {
                    int coluna_correspondente = coluna_pergunta[r.Cod_pergunta];
                    campos[coluna_correspondente] = r.ValorString;
                }
            }
            #endregion

            #region Respostas Memo
            foreach (Resposta r in q.Respostas_Memo)
            {
                if (coluna_pergunta.Keys.Contains(r.Cod_pergunta))
                {
                    int coluna_correspondente = coluna_pergunta[r.Cod_pergunta];
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
        public void grava_ficha_avaliacao()
        {
            valores = new Dictionary<int, string[]>();

            int num_colunas = items.Count + 2;
            valores[0] = getCabecalhoFA(num_colunas);

            for (int i = 0; i < fichas_avaliacao.Count; i++)
                valores[i + 1] = getCamposFA(fichas_avaliacao[i], num_colunas);

            gravaFicheiro();
        }

        /* Retorna a primeira linha do ficheiro - o cabecalho para fichas de avaliacao
         * Também preenche o dictionary com a correspondencia do item para a coluna 
         */
        // @param num_colunas Numero de colunas que o ficheiro irá ter
        // @return string[] Os valores de cada coluna da primeira linha do ficheiro
        private string[] getCabecalhoFA(int num_colunas)
        {
            string[] cabecalho = new string[num_colunas];
            coluna_pergunta = new Dictionary<long, int>();

            cabecalho[0] = "Zonas";
            for (int i = 0; i < items.Count; i++)
            {
                cabecalho[i + 1] = i.ToString() + "-" + items[i].NomeItem;
                coluna_pergunta.Add(items[i].CodigoItem, i + 1);
            }
            cabecalho[num_colunas - 1] = "Sugestões";

            return cabecalho;
        }

        /* Retorna as várias repostas da ficha de avaliacao */
        // @param fa Ficha de avaliacao de onde se retiram as respostas
        // @param num_colunas Numero de colunas que o ficheiro irá ter
        // @return string[] As várias respostas da ficha de avaliacao
        private string[] getCamposFA(FichaAvaliacao fa, int num_colunas)
        {
            string[] campos = new string[num_colunas];

            string nome_zona = "";
            campos[0] = getIndexZona(fa.CodZona, ref nome_zona).ToString() + "-" + nome_zona;

            #region Respostas Numero
            foreach (Resposta r in fa.Respostas_Numero)
            {
                if (coluna_pergunta.Keys.Contains(r.CodigoItem))
                {
                    int coluna_correspondente = coluna_pergunta[r.CodigoItem];
                    campos[coluna_correspondente] = r.Valor.ToString();
                }
            }
            #endregion

            #region Respostas String
            foreach (Resposta r in fa.Respostas_String)
            {
                if (coluna_pergunta.Keys.Contains(r.CodigoItem))
                {
                    int coluna_correspondente = coluna_pergunta[r.Cod_pergunta];
                    campos[coluna_correspondente] = r.ValorString;
                }
            }
            #endregion

            #region Respostas Memo
            foreach (Resposta r in fa.Respostas_Memo)
            {
                if (coluna_pergunta.Keys.Contains(r.CodigoItem))
                {
                    int coluna_correspondente = coluna_pergunta[r.Cod_pergunta];
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
        public void grava_checklist()
        {
            valores = new Dictionary<int, string[]>();

            valores[0] = getCabecalhoCL();

            string[][] matriz_checklist = getCamposCL();

            for (int i = 0; i < matriz_checklist.Length; i++)
                valores[i + 1] = matriz_checklist[i];

            gravaFicheiro();
        }

        /* Retorna a primeira linha do ficheiro - o cabecalho para fichas de avaliacao
         * Também preenche o dictionary com a correspondencia do item para a coluna 
         */
        // @return string[] Os valores de cada coluna da primeira linha do ficheiro
        private string[] getCabecalhoCL()
        {
            string[] cabecalho = new string[items.Count+1];
            coluna_pergunta = new Dictionary<long, int>();

            cabecalho[0] = "Zonas";
            for (int i = 0; i < items.Count; i++)
            {
                cabecalho[i + 1] = i.ToString() + "-" + items[i].NomeItem;
                coluna_pergunta.Add(items[i].CodigoItem, i + 1);
            }

            return cabecalho;
        }
    
        /* Retorna a matriz com todas as repostas de cada item para cada zona da checklist */
        // @return As várias respostas da checklist
        private string[][] getCamposCL()
        {
            /* Inicializar o array */
            string[][] campos = new string[zonas.Count][];
            for (int i = 0; i < campos.Length; i++)
                campos[i] = new string[items.Count + 1];

            /* Colocar na primeira coluna da matriz a zona e o seu nome */
            string nome_zona = "";
            for (int i = 0 ; i < zonas.Count ; i++)
                campos[i][0] = getIndexZona(zonas[i].Codigo, ref nome_zona).ToString() + "-" + nome_zona;

            foreach (Resposta r in checklist.Respostas_Numero)
            {
                if (coluna_pergunta.Keys.Contains(r.CodigoItem))
                {
                    int coluna_correspondente = coluna_pergunta[r.CodigoItem];
                    campos[getIndexZona(r.CodigoZona, ref nome_zona)-1][coluna_correspondente] = r.Valor.ToString();
                }
            }

            return campos;
        }

        #endregion

        /* Funcao que grava o ficheiro 
         * A matriz já está toda preenchida é só necessário colocar ';' entre os campos
         */
        private void gravaFicheiro()
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

        private PerguntaQuestionario getPerguntaByCod(long cod)
        {
            PerguntaQuestionario p = null;
            bool encontrado = false;
            for (int i = 0; i < perguntas_questionarios.Count && !encontrado; i++)
                if (perguntas_questionarios[i].Cod_Pergunta == cod)
                {
                    p = perguntas_questionarios[i];
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
