using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ETdAnalyser.Camada_de_Negócio;
using System.Windows;
using ETdAnalyser.CamadaDados.Classes.Verificador;
using ETdAnalyser.CamadaDados.Classes.Estruturas;

namespace ETdAnalyser.CamadaDados.Classes
{
    class Importer
    {
        #region Variáveis de intância

        /* Varáveis para criar Classe */
        private long codigoAnalise;
        private string caminho;
        private StreamReader ficheiro;

        /* Variáveis para retirar as colunas das respostas (tanto do cabeçalho como da 1ª linha de respostas)*/
        private string[] colunasRespostas;
        private Dictionary<int, string[]> valores;

        /* Variáveis dos resultados */
        private List<Formulario> formularios;

        /* Erros dos resultados */
        private Dictionary<int,Enums.Resultado_Importacao[]> resultadosImportacao;

        /* Variáveis quando existe erro */
        private string erro;
        private int linhaErro;
        private float numeroPerguntaErro;

        /* Variáveis estáticas dos erros */
        private string erroNomesColunas = "Erro ao ler ficheiro.\nFicheiro vazio.";

        private string erroNumeroRespostas = "Formulário sem todas as respostas às perguntas.";
        private string erroRespostasVazias = "Formulário sem resposta a uma ou mais perguntas.";
        private string erroValoresRespostas = "Formulário com reposta cujo valor não está de acordo com a escala da pergunta.";

        private string erroPerguntas = "Não foram encontradas as perguntas de respectivo formulário para esta analise.";


        #endregion

        #region Construtores

        public Importer(long codigoAnalise, string caminho)
        {
            this.codigoAnalise = codigoAnalise;
            this.caminho = caminho;
            this.ficheiro = new StreamReader(caminho);
        }

        #endregion

        #region Gets escalaResposta Sets

        // Colunas das respostas
        public string[] Colunas
        {
            get { return colunasRespostas; }
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
        public int LinhaErro
        {
            get { return linhaErro; }
        }
        public float NumeroPerguntaErro
        {
            get { return numeroPerguntaErro; }
        }

        // Resultados
        public List<Formulario> Formularios
        {
            get { return formularios; }
        }
        public Dictionary<int, Enums.Resultado_Importacao[]> Resultados
        {
            get { return resultadosImportacao; }
        }

        #endregion

        #region Métodos

        #region Ler Ficheiro
        public bool LerFicheiro(bool temCabecalho)
        {
            // inicializar a variável onde vai ficar guardada a info
            valores = new Dictionary<int, string[]>();

            int num_linha;
            string linha;
            if ((linha = ficheiro.ReadLine()) != null)
            {
                if (temCabecalho)
                {
                    colunasRespostas = linha.Split(';');
                    num_linha = 0;
                }
                else
                {
                    string[] vs = linha.Split(';');
                    colunasRespostas = new string[vs.Length];
                    for (int i = 0; i < vs.Length; i++)
                        colunasRespostas[i] = vs[i];
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
                erro = erroNomesColunas;
                return false;
            }
        }
        #endregion

        #region Questionario

        public bool ImportarQuestionario(
            // modo como vai ser tratado se o numero de respostas nao esta correcto
            Enums.Numero_Respostas modoNumRespostas,
            // modo como vai ser tratado se existirem respostas sem resposta
            Enums.Respostas_Vazias modoRespostasVazias,
            // modo como vai ser tratado se os valores das respostas nao estarem de acordo com as respostas das perguntas
            Enums.Valores_Respostas modoValoresRespostas, 
            List<Zona> zonas,
            List<PerguntaQuestionario> perguntas,
            // assiciacao da pergunta à coluna (nao esquecer perguntas checkbox, que podem ser mais de uma coluna)
            Dictionary<float, List<int>> perguntasVolunasFicheiro,
            // Mapa dos codigos zonas
            Dictionary<int,Zona> mapaColunaZona) 
        {
            formularios = new List<Formulario>();
            bool continuar = true;
            bool continuarFormulario = true;

            // iniciar varável dos resultados
            resultadosImportacao = new Dictionary<int, Enums.Resultado_Importacao[]>();

            // para cada linha do ficheiro ...
            for (int i = 0; i < valores.Keys.Count && continuar; i++)
            {
                continuarFormulario = true;
                // retirar o numero da linha
                int num_linha = valores.Keys.ElementAt(i); 
                // verificar se o número de colunas está correcto
                VerificaNumeroColunas(num_linha, perguntasVolunasFicheiro, perguntas, modoNumRespostas, 
                    ref continuar, ref continuarFormulario);

                // se continuar a importacao escalaResposta a importacao do formulario ...
                if (continuar && continuarFormulario)
                {
                    // importar linha
                    List<Resposta> respostas = ImportarLinhaQuestionario(num_linha, modoRespostasVazias, modoValoresRespostas, zonas, perguntas, perguntasVolunasFicheiro, mapaColunaZona,ref continuar, ref continuarFormulario);

                    // se nao houveram erros escalaResposta se foram importadas algumas respostas
                    if (continuar && continuarFormulario  && respostas.Count != 0)
                    {
                        // Criar um questionario com essas respostas
                        Questionario q = new Questionario();
                        q.CodigoAnalise = codigoAnalise;
                        //q.CodigoQuestionario = GestaodeRespostas.insere_questionario(q);

                        foreach (Resposta r in respostas)
                        {
                            if (r.GetTipoResposta == Resposta.TipoResposta.RespostaNum)
                                q.AdicionarRespostaNumero(r);
                            else if (r.GetTipoResposta == Resposta.TipoResposta.RespostaStr)
                                q.AdicionarRespostaString(r);
                            else
                                q.AdicionarRespostaMemo(r);
                        }
                        formularios.Add(q);
                    }
                }
            }
            return continuar;
        }

        private List<Resposta> ImportarLinhaQuestionario(
            // numero da linha no ficheiro
            int numeroLinha,
            // como tratar respostas sem resposta
            Enums.Respostas_Vazias modoRespostasVazias,
            // como tratar respostas com valores absurdos
            Enums.Valores_Respostas modoValoresRespostas, 
            List<Zona> zonas,
            List<PerguntaQuestionario> perguntas,
            // associacao das perguntas com colunas
            Dictionary<float, List<int>> perguntasColunasFicheiro,
            // Mapa Coluna-Zona que corresponde 
            Dictionary<int,Zona> mapaColunaZona,
            // se cotinuar importacao
            ref bool continuar,
            // se continuar a importar este formulario
            ref bool continuarFormulario)
        {
            List<Resposta> respostas = new List<Resposta>();
            string[] valoresLinha = valores[numeroLinha];
            bool continuarPergunta;

            // Iniciar o array de resultados escalaResposta inicializa-los como SR
            Enums.Resultado_Importacao[] results = new Enums.Resultado_Importacao[valoresLinha.Length];
            for (int i=0;i<valoresLinha.Length;i++)
                results[i] = Enums.Resultado_Importacao.SR;


            for (int i = 0; i < perguntasColunasFicheiro.Keys.Count && continuar && continuarFormulario; i++)
            {
                continuarPergunta = true;
                PerguntaQuestionario perg_associada = GetPerguntaPorNumero( perguntasColunasFicheiro.Keys.ElementAt(i),perguntas);
                TipoEscala ti = GestaodeRespostas.getTipoEscala(perg_associada.CodigoTipoEscala);

                List<string> campos = new List<string>();

                foreach(int num_coluna in perguntasColunasFicheiro[perg_associada.NumeroPergunta])
                    campos.Add(valores[numeroLinha][num_coluna]); // retirar os varios campos da pergunta

                // verificar se estes campos estao correctos escalaResposta de acordo com o esperado
                for (int j = 0; j < campos.Count && continuar && continuarFormulario && continuarPergunta; j++ )
                   results[perguntasColunasFicheiro[perg_associada.NumeroPergunta].ElementAt(j)] = 
                       VerificaCampo(numeroLinha, campos[j], modoRespostasVazias, modoValoresRespostas, perg_associada, 
                       ref continuar, ref continuarFormulario, ref continuarPergunta);

                if (continuar && continuarFormulario && continuarPergunta)
                {
                    long cod_zona = -1;
                    if (perg_associada.CodigoZona == 0)
                    {
                        // se é preciso colocar a zona como resposta
                        string campo2 = valores[numeroLinha][perguntasColunasFicheiro[perg_associada.NumeroPergunta][0]+1];
                        
                        if ( !campo2.Equals("") && short.Parse(campo2) <= 0)
                            results[perguntasColunasFicheiro[perg_associada.NumeroPergunta].ElementAt(0) + 1] = 
                                Enums.Resultado_Importacao.Insucesso;
                        else
                            results[perguntasColunasFicheiro[perg_associada.NumeroPergunta].ElementAt(0) + 1] =
                                Enums.Resultado_Importacao.Sucesso;

                        if (campo2.Equals("")) cod_zona = 0;
                        else cod_zona = mapaColunaZona[short.Parse(campo2)].Codigo;
                    }
                    if (continuar && continuarFormulario && continuarPergunta)
                    {
                        List<Resposta> resps = GetResposta(campos,cod_zona,perg_associada,ti,zonas);
                        foreach (Resposta resp in resps)
                            respostas.Add(resp);
                    }
                }
            }

            if (respostas.Count == 0 || !continuarFormulario)
            {
                results = new Enums.Resultado_Importacao[1];
                results[0] = Enums.Resultado_Importacao.Insucesso;
            }
            resultadosImportacao[numeroLinha] = results;
            return respostas;
        }

        private List<Resposta> GetResposta(
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
                     (p.CodigoZona != 0 && p.CodigoZona != 1) ||
                     (p.CodigoZona == 0 && cod_zona != 0) )
                {
                    Resposta resp = new Resposta(
                    codigoAnalise,
                    -1, -1, -1,
                    p.NumeroPergunta,
                    p.CodigoItem,
                    p.CodigoZona != 0 ? p.CodigoZona : cod_zona,
                    i_valor,
                    s_valor,
                    -1, // Nao faço a mínima para que serve isto
                    ti.Numero != 0 ? Resposta.TipoResposta.RespostaNum : Resposta.TipoResposta.RespostaStr);

                    resp.CodigoPergunta = p.CodigoPergunta;

                    resps.Add(resp);
                }
                else if ( p.TipoQuestao.Equals("qe") && (p.CodigoZona == 1 || (p.CodigoZona == 0 && cod_zona == 0)))
                {
                    foreach (Zona z in zonas)
                    {
                        Resposta resp = new Resposta(
                            codigoAnalise,
                            -1, -1, -1,
                            p.NumeroPergunta,
                            p.CodigoItem,
                            z.Codigo,
                            i_valor,
                            s_valor,
                            -1, // Nao faço a mínima para que serve isto
                            ti.Numero != 0 ? Resposta.TipoResposta.RespostaNum : Resposta.TipoResposta.RespostaStr);

                        resp.CodigoPergunta = p.CodigoPergunta;

                        resps.Add(resp);
                    }
                }
            }
            return resps;
        }

        #region verificar Erros
        private void VerificaNumeroColunas(
            int num_linha,
            Dictionary<float, List<int>> _perguntas_colunas_ficheiro,
            List<PerguntaQuestionario> _pergs,
            Enums.Numero_Respostas _modo_num_respostas,
            ref bool continuar,
            ref bool continuar_formulario)
        {
            if (!TemColunasNecessarias(num_linha, _perguntas_colunas_ficheiro, _pergs))
            {
                if (_modo_num_respostas == Enums.Numero_Respostas.Sair_Numero)
                {
                    erro = erroNumeroRespostas;
                    linhaErro = num_linha;
                    continuar = false;
                    continuar_formulario = false;
                }
                else
                {
                    Enums.Resultado_Importacao[] results = new Enums.Resultado_Importacao[1];
                    results[0] = Enums.Resultado_Importacao.Insucesso;
                    resultadosImportacao.Add(num_linha,results);
                    continuar_formulario = false;
                }
           }
        }

        public Enums.Resultado_Importacao VerificaCampo(
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
            if (Input_Verifier.SoEspacos(_campo))
            {
                if (_modo_respostas_vazias == Enums.Respostas_Vazias.Sair_Vazias)
                {
                    erro = erroRespostasVazias;
                    linhaErro = num_linha;
                    numeroPerguntaErro = _perg.NumeroPergunta;
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

            TipoEscala ti = GestaodeRespostas.getTipoEscala(_perg.CodigoTipoEscala);

            #region Teste Valor Resposta

            if (ti.Numero != 0 &&
                ti.Numero != 1 &&
                (!Input_Verifier.SoEspacos(_campo) &&
                ( (short.Parse(_campo)) < 1 ||
                  (short.Parse(_campo)) > ti.Respostas.Count)))
            {
                if (_modo_valores_respostas == Enums.Valores_Respostas.Sair_Valores)
                {
                    erro = erroValoresRespostas;
                    linhaErro = num_linha;
                    numeroPerguntaErro = _perg.NumeroPergunta;
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
        private PerguntaQuestionario GetPerguntaPorNumero(float num, List<PerguntaQuestionario> ps)
        {
            for (int i = 0; i < ps.Count; i++)
                if (ps[i].NumeroPergunta == num)
                    return ps[i];
            return null;
        }

        private bool TemColunasNecessarias(int linha, Dictionary<float, List<int>> _perguntas_colunas_ficheiro, List<PerguntaQuestionario> _pergs)
        {
            int num_colunas_necessarias = 0;

            for (int i = 0; i < _perguntas_colunas_ficheiro.Keys.Count; i++)
            {
                PerguntaQuestionario p = GetPerguntaPorNumero(_perguntas_colunas_ficheiro.Keys.ElementAt(i), _pergs);
                TipoEscala ti = GestaodeRespostas.getTipoEscala(p.CodigoTipoEscala);

                if (ti.Numero == -2)
                    num_colunas_necessarias += ti.Respostas.Count;
                else
                    num_colunas_necessarias++;

                if (p.CodigoZona == 0)
                    num_colunas_necessarias++;
            }
            return valores[linha].Length >= num_colunas_necessarias;
        }
        #endregion

        public void SubmeteQuestionarios()
        {
            if (formularios.Count > 0)
                foreach (Questionario q in formularios)
                    GestaodeRespostas.insere_questionario(q);
        }

        #endregion

        #region Ficha Avaliacao

        public bool ImportarFichaAvaliacao(
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

            foreach (Zona z in _zonas)
                MessageBox.Show("Zona Cod: " + z.Codigo);

            // iniciar varável dos resultados
            resultadosImportacao = new Dictionary<int, Enums.Resultado_Importacao[]>();

            // para cada linha do ficheiro ...
            for (int i = 0; i < valores.Keys.Count && continuar; i++)
            {
                long cod_zona = 0;
                if (Input_Verifier.SoNumeros(valores[i][col_cods_zonas]))
                    cod_zona = mapa_coluna_zona[short.Parse(valores[i][col_cods_zonas])].Codigo;

                MessageBox.Show(cod_zona.ToString());

                continuar_formulario = true;
                // retirar o numero da linha
                int num_linha = valores.Keys.ElementAt(i);
                // verificar se o número de colunas está correcto
                VerificaNumeroColunasFichaAvaliacao(num_linha, _perguntas_colunas_ficheiro, _pergs, _modo_num_respostas,
                    ref continuar, ref continuar_formulario);

                // se continuar a importacao escalaResposta a importacao do formulario ...
                if (continuar && continuar_formulario)
                {
                    // importar linha
                    List<Resposta> respostas = ImportarLinhaFichaAvaliacao(num_linha, _modo_respostas_vazias, _modo_valores_respostas, _zonas, _pergs, _perguntas_colunas_ficheiro, cod_zona, ref continuar, ref continuar_formulario);

                    // se nao houveram erros escalaResposta se foram importadas algumas respostas
                    if (continuar && continuar_formulario && respostas.Count != 0)
                    {
                        // Criar um questionario com essas respostas
                        FichaAvaliacao fa = new FichaAvaliacao();
                        fa.CodigoAnalise = codigoAnalise;
                        fa.CodigoZona = cod_zona;
                        //q.CodigoQuestionario = GestaodeRespostas.insere_questionario(q);

                        foreach (Resposta r in respostas)
                        {
                            if (r.GetTipoResposta == Resposta.TipoResposta.RespostaNum)
                                fa.adicionarRespostaNumero(r);
                            else if (r.GetTipoResposta == Resposta.TipoResposta.RespostaStr)
                                fa.adicionarRespostaString(r);
                            else
                                fa.adicionarRespostaMemo(r);
                        }
                        formularios.Add(fa);
                    }
                }
            }
            return continuar;
        }

        private List<Resposta> ImportarLinhaFichaAvaliacao(
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

            // Iniciar o array de resultados escalaResposta inicializa-los como SR
            Enums.Resultado_Importacao[] results = new Enums.Resultado_Importacao[valores_linha.Length];
            for (int i = 0; i < valores_linha.Length; i++)
                results[i] = Enums.Resultado_Importacao.SR;


            for (int i = 0; i < _perguntas_colunas_ficheiro.Keys.Count && continuar && continuar_formulario; i++)
            {
                continuar_pergunta = true;
                PerguntaFichaAvaliacao perg_associada = GetPerguntaPorNumeroFichaAvaliacao(_perguntas_colunas_ficheiro.Keys.ElementAt(i), _pergs);
                TipoEscala ti = GestaodeRespostas.getTipoEscala(perg_associada.CodigoTipoEscala);

                List<string> campos = new List<string>();

                foreach (int num_coluna in _perguntas_colunas_ficheiro[perg_associada.NumeroPergunta])
                    campos.Add(valores[num_linha][num_coluna]); // retirar os varios campos da pergunta

                // verificar se estes campos estao correctos escalaResposta de acordo com o esperado
                for (int j = 0; j < campos.Count && continuar && continuar_formulario && continuar_pergunta; j++)
                    results[_perguntas_colunas_ficheiro[perg_associada.NumeroPergunta].ElementAt(j)] =
                        VerificaCampo(num_linha, campos[j], _modo_respostas_vazias, _modo_valores_respostas, perg_associada,
                        ref continuar, ref continuar_formulario, ref continuar_pergunta);

                if (continuar && continuar_formulario && continuar_pergunta)
                {
                    if (continuar && continuar_formulario && continuar_pergunta)
                    {
                        List<Resposta> resps = GetResposta(campos, cod_zona, perg_associada, ti, _zonas);
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
            resultadosImportacao[num_linha] = results;
            return respostas;
        }

        private List<Resposta> GetResposta(
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
                codigoAnalise,
                -1, -1, -1,
                p.NumeroPergunta,
                p.CodigoItem,
                cod_zona,
                i_valor,
                s_valor,
                -1, // Nao faço a mínima para que serve isto
                ti.Numero != 0 ? Resposta.TipoResposta.RespostaNum : Resposta.TipoResposta.RespostaStr);

                resp.CodigoPergunta = p.CodigoPergunta;

                resps.Add(resp);
            }
            return resps;
        }

        #region verificar Erros
        private void VerificaNumeroColunasFichaAvaliacao(
            int num_linha,
            Dictionary<float, List<int>> _perguntas_colunas_ficheiro,
            List<PerguntaFichaAvaliacao> _pergs,
            Enums.Numero_Respostas _modo_num_respostas,
            ref bool continuar,
            ref bool continuar_formulario)
        {
            if (!TemColunasNecessariasFichaAvaliacao(num_linha, _perguntas_colunas_ficheiro, _pergs))
            {
                if (_modo_num_respostas == Enums.Numero_Respostas.Sair_Numero)
                {
                    erro = erroNumeroRespostas;
                    linhaErro = num_linha;
                    continuar = false;
                    continuar_formulario = false;
                }
                else
                {
                    Enums.Resultado_Importacao[] results = new Enums.Resultado_Importacao[1];
                    results[0] = Enums.Resultado_Importacao.Insucesso;
                    resultadosImportacao.Add(num_linha, results);
                    continuar_formulario = false;
                }
            }
        }

        public Enums.Resultado_Importacao VerificaCampo(
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
            if (Input_Verifier.SoEspacos(_campo))
            {
                if (_modo_respostas_vazias == Enums.Respostas_Vazias.Sair_Vazias)
                {
                    erro = erroRespostasVazias;
                    linhaErro = num_linha;
                    numeroPerguntaErro = _perg.NumeroPergunta;
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

            TipoEscala ti = GestaodeRespostas.getTipoEscala(_perg.CodigoTipoEscala);

            #region Teste Valor Resposta

            if (ti.Numero != 0 &&
                ti.Numero != 1 &&
                (!Input_Verifier.SoEspacos(_campo) &&
                ((short.Parse(_campo)) < 1 ||
                  (short.Parse(_campo)) > ti.Respostas.Count)))
            {
                if (_modo_valores_respostas == Enums.Valores_Respostas.Sair_Valores)
                {
                    erro = erroValoresRespostas;
                    linhaErro = num_linha;
                    numeroPerguntaErro = _perg.NumeroPergunta;
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
        private PerguntaFichaAvaliacao GetPerguntaPorNumeroFichaAvaliacao(float num, List<PerguntaFichaAvaliacao> ps)
        {
            for (int i = 0; i < ps.Count; i++)
                if (ps[i].NumeroPergunta == num)
                    return ps[i];
            return null;
        }

        private bool TemColunasNecessariasFichaAvaliacao(int linha, Dictionary<float, List<int>> _perguntas_colunas_ficheiro, List<PerguntaFichaAvaliacao> _pergs)
        {
            int num_colunas_necessarias = 0;

            for (int i = 0; i < _perguntas_colunas_ficheiro.Keys.Count; i++)
            {
                PerguntaFichaAvaliacao p = GetPerguntaPorNumeroFichaAvaliacao(_perguntas_colunas_ficheiro.Keys.ElementAt(i), _pergs);
                TipoEscala ti = GestaodeRespostas.getTipoEscala(p.CodigoTipoEscala);

                if (ti.Numero == -2)
                    num_colunas_necessarias += ti.Respostas.Count;
                else
                    num_colunas_necessarias++;
            }
            return valores[linha].Length >= num_colunas_necessarias;
        }
        #endregion

        public void SubmeterFichasAvaliacao()
        {
            if (formularios.Count > 0)
                foreach (FichaAvaliacao q in formularios)
                    GestaodeRespostas.insere_ficha_avaliacao(q);
        }

        #endregion

        #region CheckList
        public bool ImportarChecklist(
            // modo como vai ser tratado se o numero de respostas nao esta correcto
            Enums.Numero_Respostas _modo_num_respostas,
            // modo como vai ser tratado se existirem respostas sem resposta
            Enums.Respostas_Vazias _modo_respostas_vazias,
            // modo como vai ser tratado se os valores das respostas nao estarem de acordo com as respostas das perguntas
            Enums.Valores_Respostas _modo_valores_respostas, 
            List<Zona> _zonas,
            List<Item> _itens,
            // associacao do codigo do item à coluna do ficheiro
            Dictionary<string, int> _itens_colunas_ficheiro,
            // mapeamento da zona/actividade para o codigo da zona
            Dictionary<int, Zona> mapa_coluna_zona,
            // coluna dos codigos da zona
            int col_cods_zonas)
        {
            formularios = new List<Formulario>();
            bool continuar = true;
            bool continuar_formulario = true;

            // iniciar varável dos resultados
            resultadosImportacao = new Dictionary<int, Enums.Resultado_Importacao[]>();

            // para cada linha do ficheiro ...
            for (int i = 0; i < valores.Keys.Count && continuar; i++)
            {
                long cod_zona = mapa_coluna_zona[short.Parse(valores[i][col_cods_zonas])].Codigo;

                continuar_formulario = true;
                // retirar o numero da linha
                int num_linha = valores.Keys.ElementAt(i);

                // importar linha
                List<Resposta> respostas = ImportarLinhaChecklist(num_linha, _modo_respostas_vazias, _modo_valores_respostas, _zonas,_itens,_itens_colunas_ficheiro, cod_zona, ref continuar, ref continuar_formulario);

                // se nao houveram erros escalaResposta se foram importadas algumas respostas
                if (continuar && continuar_formulario && respostas.Count != 0)
                {
                    // Criar um questionario com essas respostas
                    CheckList cl = new CheckList();
                    cl.CodigoAnalise = codigoAnalise;
                    //q.CodigoQuestionario = GestaodeRespostas.insere_questionario(q);

                    foreach (Resposta r in respostas)
                        cl.adicionarRespostaNumero(r);
                    formularios.Add(cl);
                }
            }
            return continuar;
        }

        private List<Resposta> ImportarLinhaChecklist(
            // numero da linha no ficheiro
            int num_linha,
            // como tratar respostas sem resposta
            Enums.Respostas_Vazias _modo_respostas_vazias,
            // como tratar respostas com valores absurdos
            Enums.Valores_Respostas _modo_valores_respostas,
            List<Zona> _zonas,
            List<Item> _itens,
            // associacao das perguntas com colunas
            Dictionary<string, int> _itens_colunas_ficheiro,
            long cod_zona,
            // se cotinuar importacao
            ref bool continuar,
            // se continuar a importar este formulario
            ref bool continuar_formulario)
        {
            List<Resposta> respostas = new List<Resposta>();
            string[] valores_linha = valores[num_linha];
            bool continuar_pergunta;

            // Iniciar o array de resultados escalaResposta inicializa-los como SR
            Enums.Resultado_Importacao[] results = new Enums.Resultado_Importacao[valores_linha.Length];
            for (int i = 0; i < valores_linha.Length; i++)
                results[i] = Enums.Resultado_Importacao.SR;


            for (int i = 0; i < _itens_colunas_ficheiro.Keys.Count && continuar && continuar_formulario; i++)
            {
                continuar_pergunta = true;
                Item item_associado = GetItemPorNome(_itens_colunas_ficheiro.Keys.ElementAt(i), _itens);

                // retirar o campo
                string campo = valores[num_linha][_itens_colunas_ficheiro[item_associado.NomeItem]];

                // verificar se estes campos estao correctos escalaResposta de acordo com o esperado
                results[_itens_colunas_ficheiro[item_associado.NomeItem]] =
                VerificaCampo(num_linha, campo, _modo_respostas_vazias, _modo_valores_respostas,
                        ref continuar, ref continuar_formulario, ref continuar_pergunta);

                if (continuar && continuar_formulario && continuar_pergunta)
                {
                    if (continuar && continuar_formulario && continuar_pergunta)
                    {
                        Resposta resp = GetResposta(campo, cod_zona, item_associado, _zonas);
                        respostas.Add(resp);
                    }
                }
            }

            if (respostas.Count == 0 || !continuar_formulario)
            {
                results = new Enums.Resultado_Importacao[1];
                results[0] = Enums.Resultado_Importacao.Insucesso;
            }
            resultadosImportacao[num_linha] = results;
            return respostas;
        }

        private Resposta GetResposta(
            string campo,
            long cod_zona,
            Item item,
            List<Zona> zonas)
        {
            Resposta resp = new Resposta(
            codigoAnalise,
            -1, -1, -1, -1,
            item.CodigoItem,
            cod_zona,
            short.Parse(campo),
            "",
            -1, // Nao faço a mínima para que serve isto
            Resposta.TipoResposta.RespostaNum);

            return resp;
        }

        #endregion 

        #region verificar Erros
        public Enums.Resultado_Importacao VerificaCampo(
            int num_linha,
            string _campo,
            Enums.Respostas_Vazias _modo_respostas_vazias,
            Enums.Valores_Respostas _modo_valores_respostas,
            ref bool continuar,
            ref bool continuar_formulario,
            ref bool continuar_pergunta)
        {
            Enums.Resultado_Importacao result = Enums.Resultado_Importacao.Sucesso;

            #region Teste Respostas Vazias
            /* Testar Repostas vazias */
            if (Input_Verifier.SoEspacos(_campo))
            {
                if (_modo_respostas_vazias == Enums.Respostas_Vazias.Sair_Vazias)
                {
                    erro = erroRespostasVazias;
                    linhaErro = num_linha;
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

            #region Teste Valor Resposta

            if ((!Input_Verifier.SoEspacos(_campo) &&
                ((short.Parse(_campo)) < 1 ||
                  (short.Parse(_campo)) > 5)))
            {
                if (_modo_valores_respostas == Enums.Valores_Respostas.Sair_Valores)
                {
                    erro = erroValoresRespostas;
                    linhaErro = num_linha;
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

        public Item GetItemPorNome(string nome, List<Item> itens)
        {
            for (int i = 0; i < itens.Count; i++)
                if (itens[i].NomeItem == nome)
                    return itens[i];
            return null;
        }

        #endregion

        public void SubmeterCheckList()
        {
            if (formularios.Count > 0)
                foreach (CheckList c in formularios)
                    GestaodeRespostas.insere_CheckList(c);
        }

        #endregion
    }
}
