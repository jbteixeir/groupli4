using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ETdA.Camada_de_Negócio;
using System.Windows;
using ETdA.Camada_de_Dados.Classes.Verificador;
using ETdA.Camada_de_Dados.Classes.Estruturas;

namespace ETdA.Camada_de_Dados.Classes
{
    class Importer_Exporter
    {
        #region Variáveis de intância

        /* Varáveis para criar Classe */
        private long cod_analise;
        private string caminho;
        private StreamReader ficheiro;

        /* Variáveis para retirar as colunas das respostas (tanto do cabeçalho como da 1ª linha de respostas)*/
        private string[] colunas_respostas;
        private Dictionary<int, string[]> valores;

        /* Variáveis dos resultados */
        private List<Formulario> formularios;

        /* Erros dos resultados */
        private Dictionary<int,Enums.Resultado_Importacao[]> resultados_importacao;

        /* Variáveis quando existe erro */
        private string erro;
        private int linha_erro;
        private float numero_pergunta_erro;

        /* Variáveis estáticas dos erros */
        private string erro_nomes_colunas = "Erro ao ler ficheiro.\nFicheiro vazio.";

        private string erro_numero_respostas = "Formulário sem todas as respostas às perguntas.";
        private string erro_repostastas_vazias = "Formulário sem resposta a uma ou mais perguntas.";
        private string erro_valores_respostas = "Formulário com reposta cujo valor não está de acordo com a escala da pergunta.";

        private string erro_perguntas = "Não foram encontradas as perguntas de respectivo formulário para esta analise.";


        #endregion

        #region Construtores

        public Importer_Exporter(long _cod_analise, string _caminho)
        {
            cod_analise = _cod_analise;
            caminho = _caminho;
            ficheiro = new StreamReader(_caminho);
        }

        #endregion

        #region Gets e Sets

        // Colunas das respostas
        public string[] Colunas
        {
            get { return colunas_respostas; }
        }
        public Dictionary<int, string[]> Valores
        {
            get { return valores; }
        }

        // Erros
        public string Erro
        {
            get { return erro; }
        }
        public int Linha_Erro
        {
            get { return linha_erro; }
        }
        public float Numero_Pergunta_Erro
        {
            get { return numero_pergunta_erro; }
        }

        // Resultados
        public List<Formulario> Formularios
        {
            get { return formularios; }
        }
        public Dictionary<int, Enums.Resultado_Importacao[]> Resultados
        {
            get { return resultados_importacao; }
        }

        #endregion

        #region Métodos

        #region Ler Ficheiro
        public bool ler_ficheiro(bool tem_cabecalho)
        {
            // inicializar a variável onde vai ficar guardada a info
            valores = new Dictionary<int, string[]>();

            int num_linha;
            string linha;
            if ((linha = ficheiro.ReadLine()) != null)
            {
                if (tem_cabecalho)
                {
                    colunas_respostas = linha.Split(';');
                    num_linha = 0;
                }
                else
                {
                    string[] vs = linha.Split(';');
                    colunas_respostas = new string[vs.Length];
                    for (int i = 0; i < vs.Length; i++)
                        colunas_respostas[i] = vs[i];
                    valores.Add(1, vs);
                    num_linha = 1;
                }

                while ((linha = ficheiro.ReadLine()) != null)
                {
                    string[] vs = linha.Split(';');
                    valores.Add(num_linha, vs);
                    num_linha++;
                }
                return true;
            }
            else
            {
                erro = erro_nomes_colunas;
                return false;
            }
        }
        #endregion

        #region Questionario

        public bool importar_questionario(
            // modo como vai ser tratado se o numero de respostas nao esta correcto
            Enums.Numero_Respostas _modo_num_respostas,
            // modo como vai ser tratado se existirem respostas sem resposta
            Enums.Respostas_Vazias _modo_respostas_vazias,
            // modo como vai ser tratado se os valores das respostas nao estarem de acordo com as respostas das perguntas
            Enums.Valores_Respostas _modo_valores_respostas, 
            List<Zona> _zonas,
            List<PerguntaQuestionario> _pergs,
            // assiciacao da pergunta à coluna (nao esquecer perguntas checkbox, que podem ser mais de uma coluna)
            Dictionary<float, List<int>> _perguntas_colunas_ficheiro,
            // Mapa dos codigos zonas
            Dictionary<int,Zona> mapa_coluna_zona) 
        {
            formularios = new List<Formulario>();
            bool continuar = true;
            bool continuar_formulario = true;

            // iniciar varável dos resultados
            resultados_importacao = new Dictionary<int, Enums.Resultado_Importacao[]>();

            // para cada linha do ficheiro ...
            for (int i = 0; i < valores.Keys.Count && continuar; i++)
            {
                continuar_formulario = true;
                // retirar o numero da linha
                int num_linha = valores.Keys.ElementAt(i); 
                // verificar se o número de colunas está correcto
                verifica_numero_colunas(num_linha, _perguntas_colunas_ficheiro, _pergs, _modo_num_respostas, 
                    ref continuar, ref continuar_formulario);

                // se continuar a importacao e a importacao do formulario ...
                if (continuar && continuar_formulario)
                {
                    // importar linha
                    List<Resposta> respostas = importar_linha_questionario(num_linha, _modo_respostas_vazias, _modo_valores_respostas, _zonas, _pergs, _perguntas_colunas_ficheiro, mapa_coluna_zona,ref continuar, ref continuar_formulario);

                    // se nao houveram erros e se foram importadas algumas respostas
                    if (continuar && continuar_formulario  && respostas.Count != 0)
                    {
                        // Criar um questionario com essas respostas
                        Questionario q = new Questionario();
                        q.CodAnalise = cod_analise;
                        //q.Cod_Questionario = GestaodeRespostas.insere_questionario(q);

                        foreach (Resposta r in respostas)
                        {
                            if (r.Tipo_Resposta == Resposta.TipoResposta.RespostaNum)
                                q.add_resposta_numero(r);
                            else if (r.Tipo_Resposta == Resposta.TipoResposta.RespostaStr)
                                q.add_resposta_string(r);
                            else
                                q.add_resposta_memo(r);
                        }
                        formularios.Add(q);
                    }
                }
            }
            return continuar;
        }

        private List<Resposta> importar_linha_questionario(
            // numero da linha no ficheiro
            int num_linha,
            // como tratar respostas sem resposta
            Enums.Respostas_Vazias _modo_respostas_vazias,
            // como tratar respostas com valores absurdos
            Enums.Valores_Respostas _modo_valores_respostas, 
            List<Zona> _zonas,
            List<PerguntaQuestionario> _pergs,
            // associacao das perguntas com colunas
            Dictionary<float, List<int>> _perguntas_colunas_ficheiro,
            // Mapa Coluna-Zona que corresponde 
            Dictionary<int,Zona> mapa_coluna_zona,
            // se cotinuar importacao
            ref bool continuar,
            // se continuar a importar este formulario
            ref bool continuar_formulario)
        {
            List<Resposta> respostas = new List<Resposta>();
            string[] valores_linha = valores[num_linha];
            bool continuar_pergunta;

            // Iniciar o array de resultados e inicializa-los como SR
            Enums.Resultado_Importacao[] results = new Enums.Resultado_Importacao[valores_linha.Length];
            for (int i=0;i<valores_linha.Length;i++)
                results[i] = Enums.Resultado_Importacao.SR;


            for (int i = 0; i < _perguntas_colunas_ficheiro.Keys.Count && continuar && continuar_formulario; i++)
            {
                continuar_pergunta = true;
                PerguntaQuestionario perg_associada = getPerguntaByNum( _perguntas_colunas_ficheiro.Keys.ElementAt(i),_pergs);
                TipoEscala ti = GestaodeRespostas.getTipoEscala(perg_associada.Cod_TipoEscala);

                List<string> campos = new List<string>();

                foreach(int num_coluna in _perguntas_colunas_ficheiro[perg_associada.Num_Pergunta])
                    campos.Add(valores[num_linha][num_coluna]); // retirar os varios campos da pergunta

                // verificar se estes campos estao correctos e de acordo com o esperado
                for (int j = 0; j < campos.Count && continuar && continuar_formulario && continuar_pergunta; j++ )
                   results[_perguntas_colunas_ficheiro[perg_associada.Num_Pergunta].ElementAt(j)] = 
                       verifica_campo(num_linha, campos[j], _modo_respostas_vazias, _modo_valores_respostas, perg_associada, 
                       ref continuar, ref continuar_formulario, ref continuar_pergunta);

                if (continuar && continuar_formulario && continuar_pergunta)
                {
                    long cod_zona = -1;
                    if (perg_associada.Cod_zona == 0)
                    {
                        // se é preciso colocar a zona como resposta
                        string campo2 = valores[num_linha][_perguntas_colunas_ficheiro[perg_associada.Num_Pergunta][0]+1];
                        
                        if ( !campo2.Equals("") && short.Parse(campo2) <= 0)
                            results[_perguntas_colunas_ficheiro[perg_associada.Num_Pergunta].ElementAt(0) + 1] = 
                                Enums.Resultado_Importacao.Insucesso;
                        else
                            results[_perguntas_colunas_ficheiro[perg_associada.Num_Pergunta].ElementAt(0) + 1] =
                                Enums.Resultado_Importacao.Sucesso;

                        if (campo2.Equals("")) cod_zona = 0;
                        else cod_zona = mapa_coluna_zona[short.Parse(campo2)].Codigo;
                    }
                    if (continuar && continuar_formulario && continuar_pergunta)
                    {
                        List<Resposta> resps = get_resposta(campos,cod_zona,perg_associada,ti,_zonas);
                        foreach (Resposta resp in resps)
                            respostas.Add(resp);
                    }
                }
            }

            if (respostas.Count == 0 || !continuar_formulario)
            {
                results = new Enums.Resultado_Importacao[1];
                results[0] = Enums.Resultado_Importacao.Insucesso;
            }
            resultados_importacao[num_linha] = results;
            return respostas;
        }

        private List<Resposta> get_resposta(
            List<string> campos, 
            long cod_zona, 
            PerguntaQuestionario p, 
            TipoEscala ti,
            List<Zona> zonas)
        {
            List<Resposta> resps = new List<Resposta>();

            foreach (string campo in campos)
            {
                short i_valor;
                string s_valor;

                // sao numeros
                if (ti.Numero != 0)
                {
                    s_valor = "";
                    i_valor = (short)(short.Parse(campo));
                }
                else
                {
                    s_valor = campo;
                    i_valor = -1;
                }

                if ( (!p.TipoQuestao.Equals("qe")) ||
                     (p.Cod_zona != 0 && p.Cod_zona != 1) ||
                     (p.Cod_zona == 0 && cod_zona != 0) )
                {
                    Resposta resp = new Resposta(
                    cod_analise,
                    -1, -1, -1,
                    p.Num_Pergunta,
                    p.Cod_Item,
                    p.Cod_zona != 0 ? p.Cod_zona : cod_zona,
                    i_valor,
                    s_valor,
                    -1, // Nao faço a mínima para que serve isto
                    ti.Numero != 0 ? Resposta.TipoResposta.RespostaNum : Resposta.TipoResposta.RespostaStr);

                    resp.Cod_pergunta = p.Cod_Pergunta;

                    resps.Add(resp);
                }
                else if ( p.TipoQuestao.Equals("qe") && (p.Cod_zona == 1 || (p.Cod_zona == 0 && cod_zona == 0)))
                {
                    foreach (Zona z in zonas)
                    {
                        Resposta resp = new Resposta(
                            cod_analise,
                            -1, -1, -1,
                            p.Num_Pergunta,
                            p.Cod_Item,
                            z.Codigo,
                            i_valor,
                            s_valor,
                            -1, // Nao faço a mínima para que serve isto
                            ti.Numero != 0 ? Resposta.TipoResposta.RespostaNum : Resposta.TipoResposta.RespostaStr);

                        resp.Cod_pergunta = p.Cod_Pergunta;

                        resps.Add(resp);
                    }
                }
            }
            return resps;
        }

        #region verificar Erros
        private void verifica_numero_colunas(
            int num_linha,
            Dictionary<float, List<int>> _perguntas_colunas_ficheiro,
            List<PerguntaQuestionario> _pergs,
            Enums.Numero_Respostas _modo_num_respostas,
            ref bool continuar,
            ref bool continuar_formulario)
        {
            if (!tem_colunas_necessarias(num_linha, _perguntas_colunas_ficheiro, _pergs))
            {
                if (_modo_num_respostas == Enums.Numero_Respostas.Sair_Numero)
                {
                    erro = erro_numero_respostas;
                    linha_erro = num_linha;
                    continuar = false;
                    continuar_formulario = false;
                }
                else
                {
                    Enums.Resultado_Importacao[] results = new Enums.Resultado_Importacao[1];
                    results[0] = Enums.Resultado_Importacao.Insucesso;
                    resultados_importacao.Add(num_linha,results);
                    continuar_formulario = false;
                }
           }
        }

        public Enums.Resultado_Importacao verifica_campo(
            int num_linha,
            string _campo,
            Enums.Respostas_Vazias _modo_respostas_vazias,
            Enums.Valores_Respostas _modo_valores_respostas,
            PerguntaQuestionario _perg,
            ref bool continuar,
            ref bool continuar_formulario,
            ref bool continuar_pergunta)
        {
            Enums.Resultado_Importacao result = Enums.Resultado_Importacao.Sucesso;

            #region Teste Respostas Vazias
            /* Testar Repostas vazias */
            if (Input_Verifier.soEspacos(_campo))
            {
                if (_modo_respostas_vazias == Enums.Respostas_Vazias.Sair_Vazias)
                {
                    erro = erro_repostastas_vazias;
                    linha_erro = num_linha;
                    numero_pergunta_erro = _perg.Num_Pergunta;
                    result = Enums.Resultado_Importacao.Insucesso;
                    continuar = false;
                }
                else if (_modo_respostas_vazias == Enums.Respostas_Vazias.Ignorar_Formulario)
                {
                    result = Enums.Resultado_Importacao.Insucesso;
                    continuar_formulario = false;
                }
                else if (_modo_respostas_vazias == Enums.Respostas_Vazias.Ignorar_Pergunta_Nao_QE &&
                    _perg.TipoQuestao.Equals("qe"))
                {
                    result = Enums.Resultado_Importacao.Insucesso;
                    continuar_pergunta = false;
                }
                else
                {
                    result = Enums.Resultado_Importacao.Insucesso;
                    continuar_pergunta = false;
                }
            }
            #endregion

            TipoEscala ti = GestaodeRespostas.getTipoEscala(_perg.Cod_TipoEscala);

            #region Teste Valor Resposta

            if (ti.Numero != 0 &&
                ti.Numero != 1 &&
                (!Input_Verifier.soEspacos(_campo) &&
                ( (short.Parse(_campo)) < 1 ||
                  (short.Parse(_campo)) > ti.Respostas.Count)))
            {
                if (_modo_valores_respostas == Enums.Valores_Respostas.Sair_Valores)
                {
                    erro = erro_valores_respostas;
                    linha_erro = num_linha;
                    numero_pergunta_erro = _perg.Num_Pergunta;
                    result = Enums.Resultado_Importacao.Insucesso;
                    continuar = false;
                }
                else if (_modo_valores_respostas == Enums.Valores_Respostas.Ignorar_Formulario)
                {
                    result = Enums.Resultado_Importacao.Insucesso;
                    continuar_formulario = false;
                }
                else if (_modo_valores_respostas == Enums.Valores_Respostas.Ignorar_Pergunta_Nao_QE &&
                    _perg.TipoQuestao.Equals("qe"))
                {
                    result = Enums.Resultado_Importacao.Insucesso;
                    continuar_pergunta = false;
                }
                else
                {
                    result = Enums.Resultado_Importacao.Insucesso;
                    continuar_pergunta = false;
                }
            }
            #endregion

            return result;
        }
        #endregion

        #region Cenas
        private PerguntaQuestionario getPerguntaByNum(float num, List<PerguntaQuestionario> ps)
        {
            for (int i = 0; i < ps.Count; i++)
                if (ps[i].Num_Pergunta == num)
                    return ps[i];
            return null;
        }

        private bool tem_colunas_necessarias(int linha, Dictionary<float, List<int>> _perguntas_colunas_ficheiro, List<PerguntaQuestionario> _pergs)
        {
            int num_colunas_necessarias = 0;

            for (int i = 0; i < _perguntas_colunas_ficheiro.Keys.Count; i++)
            {
                PerguntaQuestionario p = getPerguntaByNum(_perguntas_colunas_ficheiro.Keys.ElementAt(i), _pergs);
                TipoEscala ti = GestaodeRespostas.getTipoEscala(p.Cod_TipoEscala);

                if (ti.Numero == -2)
                    num_colunas_necessarias += ti.Respostas.Count;
                else
                    num_colunas_necessarias++;

                if (p.Cod_zona == 0)
                    num_colunas_necessarias++;
            }
            return valores[linha].Length >= num_colunas_necessarias;
        }
        #endregion

        public void submeteQuestionarios()
        {
            if (formularios.Count > 0)
                foreach (Questionario q in formularios)
                    GestaodeRespostas.insere_questionario(q);
        }

        #endregion

        #region Ficha Avaliacao

        public bool importar_ficha_avaliacao(
            // modo como vai ser tratado se o numero de respostas nao esta correcto
            Enums.Numero_Respostas _modo_num_respostas, 
            // modo como vai ser tratado se existirem respostas sem resposta
            Enums.Respostas_Vazias _modo_respostas_vazias, 
            // modo como vai ser tratado se os valores das respostas nao estarem de acordo com as respostas das perguntas
            Enums.Valores_Respostas _modo_valores_respostas, 
            List<Zona> _zonas,
            List<PerguntaFichaAvaliacao> _pergs,
            // assiciacao da pergunta à coluna (nao esquecer perguntas checkbox, que podem ser mais de uma coluna)
            Dictionary<float, List<int>> _perguntas_colunas_ficheiro,
            // Mapa dos codigos zonas
            Dictionary<int, Zona> mapa_coluna_zona,
            // Coluna dos codigos das zonas
            int col_cods_zonas)
        {
            formularios = new List<Formulario>();
            bool continuar = true;
            bool continuar_formulario = true;

            // iniciar varável dos resultados
            resultados_importacao = new Dictionary<int, Enums.Resultado_Importacao[]>();

            // para cada linha do ficheiro ...
            for (int i = 0; i < valores.Keys.Count && continuar; i++)
            {
                long cod_zona = mapa_coluna_zona[short.Parse(valores[i][col_cods_zonas])].Codigo;

                continuar_formulario = true;
                // retirar o numero da linha
                int num_linha = valores.Keys.ElementAt(i);
                // verificar se o número de colunas está correcto
                verifica_numero_colunas_fa(num_linha, _perguntas_colunas_ficheiro, _pergs, _modo_num_respostas,
                    ref continuar, ref continuar_formulario);

                // se continuar a importacao e a importacao do formulario ...
                if (continuar && continuar_formulario)
                {
                    // importar linha
                    List<Resposta> respostas = importar_linha_ficha_avaliacao(num_linha, _modo_respostas_vazias, _modo_valores_respostas, _zonas, _pergs, _perguntas_colunas_ficheiro, cod_zona, ref continuar, ref continuar_formulario);

                    // se nao houveram erros e se foram importadas algumas respostas
                    if (continuar && continuar_formulario && respostas.Count != 0)
                    {
                        // Criar um questionario com essas respostas
                        FichaAvaliacao fa = new FichaAvaliacao();
                        fa.CodAnalise = cod_analise;
                        fa.CodZona = cod_zona;
                        //q.Cod_Questionario = GestaodeRespostas.insere_questionario(q);

                        foreach (Resposta r in respostas)
                        {
                            if (r.Tipo_Resposta == Resposta.TipoResposta.RespostaNum)
                                fa.add_resposta_numero(r);
                            else if (r.Tipo_Resposta == Resposta.TipoResposta.RespostaStr)
                                fa.add_resposta_string(r);
                            else
                                fa.add_resposta_memo(r);
                        }
                        formularios.Add(fa);
                    }
                }
            }
            return continuar;
        }

        private List<Resposta> importar_linha_ficha_avaliacao(
            // numero da linha no ficheiro
            int num_linha,
            // como tratar respostas sem resposta
            Enums.Respostas_Vazias _modo_respostas_vazias,
            // como tratar respostas com valores absurdos
            Enums.Valores_Respostas _modo_valores_respostas,
            List<Zona> _zonas,
            List<PerguntaFichaAvaliacao> _pergs,
            // associacao das perguntas com colunas
            Dictionary<float, List<int>> _perguntas_colunas_ficheiro,
            long cod_zona,
            // se cotinuar importacao
            ref bool continuar,
            // se continuar a importar este formulario
            ref bool continuar_formulario)
        {
            List<Resposta> respostas = new List<Resposta>();
            string[] valores_linha = valores[num_linha];
            bool continuar_pergunta;

            // Iniciar o array de resultados e inicializa-los como SR
            Enums.Resultado_Importacao[] results = new Enums.Resultado_Importacao[valores_linha.Length];
            for (int i = 0; i < valores_linha.Length; i++)
                results[i] = Enums.Resultado_Importacao.SR;


            for (int i = 0; i < _perguntas_colunas_ficheiro.Keys.Count && continuar && continuar_formulario; i++)
            {
                continuar_pergunta = true;
                PerguntaFichaAvaliacao perg_associada = getPerguntaByNum_fa(_perguntas_colunas_ficheiro.Keys.ElementAt(i), _pergs);
                TipoEscala ti = GestaodeRespostas.getTipoEscala(perg_associada.Cod_TipoEscala);

                List<string> campos = new List<string>();

                foreach (int num_coluna in _perguntas_colunas_ficheiro[perg_associada.Num_Pergunta])
                    campos.Add(valores[num_linha][num_coluna]); // retirar os varios campos da pergunta

                // verificar se estes campos estao correctos e de acordo com o esperado
                for (int j = 0; j < campos.Count && continuar && continuar_formulario && continuar_pergunta; j++)
                    results[_perguntas_colunas_ficheiro[perg_associada.Num_Pergunta].ElementAt(j)] =
                        verifica_campo(num_linha, campos[j], _modo_respostas_vazias, _modo_valores_respostas, perg_associada,
                        ref continuar, ref continuar_formulario, ref continuar_pergunta);

                if (continuar && continuar_formulario && continuar_pergunta)
                {
                    if (continuar && continuar_formulario && continuar_pergunta)
                    {
                        List<Resposta> resps = get_resposta(campos, cod_zona, perg_associada, ti, _zonas);
                        foreach (Resposta resp in resps)
                            respostas.Add(resp);
                    }
                }
            }

            if (respostas.Count == 0 || !continuar_formulario)
            {
                results = new Enums.Resultado_Importacao[1];
                results[0] = Enums.Resultado_Importacao.Insucesso;
            }
            resultados_importacao[num_linha] = results;
            return respostas;
        }

        private List<Resposta> get_resposta(
            List<string> campos,
            long cod_zona,
            PerguntaFichaAvaliacao p,
            TipoEscala ti,
            List<Zona> zonas)
        {
            List<Resposta> resps = new List<Resposta>();

            foreach (string campo in campos)
            {
                short i_valor;
                string s_valor;

                // sao numeros
                if (ti.Numero != 0)
                {
                    s_valor = "";
                    i_valor = (short)(short.Parse(campo));
                }
                else
                {
                    s_valor = campo;
                    i_valor = -1;
                }

                Resposta resp = new Resposta(
                cod_analise,
                -1, -1, -1,
                p.Num_Pergunta,
                p.Cod_Item,
                cod_zona,
                i_valor,
                s_valor,
                -1, // Nao faço a mínima para que serve isto
                ti.Numero != 0 ? Resposta.TipoResposta.RespostaNum : Resposta.TipoResposta.RespostaStr);

                resp.Cod_pergunta = p.Cod_Pergunta;

                resps.Add(resp);
            }
            return resps;
        }

        #region verificar Erros
        private void verifica_numero_colunas_fa(
            int num_linha,
            Dictionary<float, List<int>> _perguntas_colunas_ficheiro,
            List<PerguntaFichaAvaliacao> _pergs,
            Enums.Numero_Respostas _modo_num_respostas,
            ref bool continuar,
            ref bool continuar_formulario)
        {
            if (!tem_colunas_necessarias_fa(num_linha, _perguntas_colunas_ficheiro, _pergs))
            {
                if (_modo_num_respostas == Enums.Numero_Respostas.Sair_Numero)
                {
                    erro = erro_numero_respostas;
                    linha_erro = num_linha;
                    continuar = false;
                    continuar_formulario = false;
                }
                else
                {
                    Enums.Resultado_Importacao[] results = new Enums.Resultado_Importacao[1];
                    results[0] = Enums.Resultado_Importacao.Insucesso;
                    resultados_importacao.Add(num_linha, results);
                    continuar_formulario = false;
                }
            }
        }

        public Enums.Resultado_Importacao verifica_campo(
            int num_linha,
            string _campo,
            Enums.Respostas_Vazias _modo_respostas_vazias,
            Enums.Valores_Respostas _modo_valores_respostas,
            PerguntaFichaAvaliacao _perg,
            ref bool continuar,
            ref bool continuar_formulario,
            ref bool continuar_pergunta)
        {
            Enums.Resultado_Importacao result = Enums.Resultado_Importacao.Sucesso;

            #region Teste Respostas Vazias
            /* Testar Repostas vazias */
            if (Input_Verifier.soEspacos(_campo))
            {
                if (_modo_respostas_vazias == Enums.Respostas_Vazias.Sair_Vazias)
                {
                    erro = erro_repostastas_vazias;
                    linha_erro = num_linha;
                    numero_pergunta_erro = _perg.Num_Pergunta;
                    result = Enums.Resultado_Importacao.Insucesso;
                    continuar = false;
                }
                else if (_modo_respostas_vazias == Enums.Respostas_Vazias.Ignorar_Formulario)
                {
                    result = Enums.Resultado_Importacao.Insucesso;
                    continuar_formulario = false;
                }
                else
                {
                    result = Enums.Resultado_Importacao.Insucesso;
                    continuar_pergunta = false;
                }
            }
            #endregion

            TipoEscala ti = GestaodeRespostas.getTipoEscala(_perg.Cod_TipoEscala);

            #region Teste Valor Resposta

            if (ti.Numero != 0 &&
                ti.Numero != 1 &&
                (!Input_Verifier.soEspacos(_campo) &&
                ((short.Parse(_campo)) < 1 ||
                  (short.Parse(_campo)) > ti.Respostas.Count)))
            {
                if (_modo_valores_respostas == Enums.Valores_Respostas.Sair_Valores)
                {
                    erro = erro_valores_respostas;
                    linha_erro = num_linha;
                    numero_pergunta_erro = _perg.Num_Pergunta;
                    result = Enums.Resultado_Importacao.Insucesso;
                    continuar = false;
                }
                else if (_modo_valores_respostas == Enums.Valores_Respostas.Ignorar_Formulario)
                {
                    result = Enums.Resultado_Importacao.Insucesso;
                    continuar_formulario = false;
                }
                else
                {
                    result = Enums.Resultado_Importacao.Insucesso;
                    continuar_pergunta = false;
                }
            }
            #endregion

            return result;
        }
        #endregion

        #region Cenas
        private PerguntaFichaAvaliacao getPerguntaByNum_fa(float num, List<PerguntaFichaAvaliacao> ps)
        {
            for (int i = 0; i < ps.Count; i++)
                if (ps[i].Num_Pergunta == num)
                    return ps[i];
            return null;
        }

        private bool tem_colunas_necessarias_fa(int linha, Dictionary<float, List<int>> _perguntas_colunas_ficheiro, List<PerguntaFichaAvaliacao> _pergs)
        {
            int num_colunas_necessarias = 0;

            for (int i = 0; i < _perguntas_colunas_ficheiro.Keys.Count; i++)
            {
                PerguntaFichaAvaliacao p = getPerguntaByNum_fa(_perguntas_colunas_ficheiro.Keys.ElementAt(i), _pergs);
                TipoEscala ti = GestaodeRespostas.getTipoEscala(p.Cod_TipoEscala);

                if (ti.Numero == -2)
                    num_colunas_necessarias += ti.Respostas.Count;
                else
                    num_colunas_necessarias++;
            }
            return valores[linha].Length >= num_colunas_necessarias;
        }
        #endregion

        public void submeteFichasAvaliacao()
        {
            if (formularios.Count > 0)
                foreach (FichaAvaliacao q in formularios)
                    GestaodeRespostas.insere_ficha_avaliacao(q);
        }

        #endregion

        #region CheckList
        public bool importar_checklist(
            // modo como vai ser tratado se o numero de respostas nao esta correcto
            Enums.Numero_Respostas _modo_num_respostas,
            // modo como vai ser tratado se existirem respostas sem resposta
            Enums.Respostas_Vazias _modo_respostas_vazias,
            // modo como vai ser tratado se os valores das respostas nao estarem de acordo com as respostas das perguntas
            Enums.Valores_Respostas _modo_valores_respostas, 
            List<Zona> _zonas,
            List<Item> _itens,
            // associacao do codigo do item à coluna do ficheiro
            Dictionary<int, Item> _itens_colunas_ficheiro,
            // mapeamento da zona/actividade para o codigo da zona
            Dictionary<int,Zona> mapeamento_zona_coluna)
        {
            return false;
        }
        #endregion 

        #endregion
    }
}
