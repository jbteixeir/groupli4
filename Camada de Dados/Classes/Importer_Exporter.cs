using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ETdA.Camada_de_Negócio;

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

        /* Variáveis quando existe erro */
        private string erro;
        private int linha_erro;
        private float numero_pergunta_erro;

        /* Variáveis das observações */
        private int numero_formularios_ignorados;
        private int numero_perguntas_ignoradas;

        /* Variáveis estáticas dos erros */
        private string erro_nomes_colunas = "Erro ao ler ficheiro.\nFicheiro vazio.";

        private string erro_numero_respostas = "Formulário sem todas as respostas às perguntas.";
        private string erro_repostastas_vazias = "Formulário sem resposta a uma ou mais perguntas.";
        private string erro_valores_respostas = "Formulário com reposta cujo valor não está de acordo com a escala da pergunta.";

        private string erro_perguntas = "Não foram encontradas as perguntas de respectivo formulário para esta analise.";

        /* Variáveis dos resultados */
        List<Formulario> formularios;
        #endregion

        #region Enum

        public enum Numero_Respostas
        {
            Sair_Numero,
            Ignorar_Formulario
        };

        public enum Respostas_Vazias
        {
            Sair_Vazias,
            Ignorar_Formulario,
            Ignorar_Pergunta,
            Ignorar_Pergunta_Nao_QE
        };

        public enum Valores_Respostas
        {
            Sair_Valores,
            Ignorar_Formulario,
            Ignorar_Pergunta,
            Ignorar_Pergunta_Nao_QE
        };

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
        public int Formularios_Ignorados
        {
            get { return numero_formularios_ignorados; }
        }
        public int Perguntas_Ignoradas
        {
            get { return numero_perguntas_ignoradas; }
        }

        #endregion

        #region Métodos

        #region Ler Ficheiro
        public bool ler_ficheiro(bool tem_cabecalho)
        {
            valores = new Dictionary<int, string[]>();

            int num_linha;
            string linha;
            if ((linha = ficheiro.ReadLine()) != null)
            {
                if (tem_cabecalho)
                {
                    colunas_respostas = linha.Split(';');
                    num_linha = 1;
                }
                else
                {
                    string[] vs = linha.Split(';');
                    colunas_respostas = new string[vs.Length];
                    for (int i = 0; i < vs.Length; i++)
                        colunas_respostas[i] = vs[i];
                    valores.Add(1, vs);
                    num_linha = 2;
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
            Numero_Respostas _modo_num_respostas,
            Respostas_Vazias _modo_respostas_vazias,
            Valores_Respostas _modo_valores_respostas,
            List<Zona> _zonas,
            List<PerguntaQuestionario> _pergs,
            Dictionary<float, List<int>> _perguntas_colunas_ficheiro)
        {
            numero_formularios_ignorados = 0;
            numero_perguntas_ignoradas = 0;
            formularios = new List<Formulario>();
            bool continuar = true;
            bool continuar_formulario = true;

            for (int i = 0; i < valores.Keys.Count && continuar; i++)
            {
                continuar_formulario = true;
                int num_linha = valores.Keys.ElementAt(i);
                verifica_numero_colunas(num_linha, _perguntas_colunas_ficheiro, _pergs, _modo_num_respostas, ref continuar, ref continuar_formulario);

                if (continuar && continuar_formulario)
                {
                    List<Resposta> respostas = importar_linha_questionario(num_linha, _modo_respostas_vazias, _modo_valores_respostas, _zonas, _pergs, _perguntas_colunas_ficheiro, ref continuar, ref continuar_formulario);

                    if (continuar && continuar_formulario  && respostas.Count != 0)
                    {
                        Questionario q = new Questionario();
                        q.CodAnalise = cod_analise;
                        q.Cod_Questionario = GestaodeRespostas.insere_questionario(q);

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
            int num_linha,
            Respostas_Vazias _modo_respostas_vazias,
            Valores_Respostas _modo_valores_respostas,
            List<Zona> _zonas,
            List<PerguntaQuestionario> _pergs,
            Dictionary<float, List<int>> _perguntas_colunas_ficheiro,
            ref bool continuar,
            ref bool continuar_formulario)
        {
            List<Resposta> respostas = new List<Resposta>();
            string[] valores_linha = valores[num_linha];
            bool continuar_pergunta;

            for (int i = 0; i < _perguntas_colunas_ficheiro.Keys.Count && continuar && continuar_formulario; i++)
            {
                continuar_pergunta = true;
                PerguntaQuestionario perg_associada = getPerguntaByNum( _perguntas_colunas_ficheiro.Keys.ElementAt(i),_pergs);
                TipoEscala ti = GestaodeRespostas.getTipoEscala(perg_associada.Cod_TipoEscala);

                List<string> campos = new List<string>();

                foreach(int num_coluna in _perguntas_colunas_ficheiro[perg_associada.Num_Pergunta])
                    campos.Add( valores[num_linha][num_coluna]);

                for (int j = 0; j < campos.Count && continuar && continuar_formulario && continuar_pergunta; j++ )
                    verifica_campo(num_linha, campos[j], _modo_respostas_vazias, _modo_valores_respostas, perg_associada, ref continuar, ref continuar_formulario, ref continuar_pergunta);

                if (continuar && continuar_formulario && continuar_pergunta)
                {
                    long cod_zona = -1;
                    if (perg_associada.Cod_zona == 0)
                    {
                        string campo2 = valores[num_linha][_perguntas_colunas_ficheiro[perg_associada.Num_Pergunta][0]+1];
                        verifica_campo(num_linha, campo2, _modo_respostas_vazias, _modo_valores_respostas, perg_associada, ref continuar, ref continuar_formulario, ref continuar_pergunta);

                        if (continuar && continuar_formulario && continuar_pergunta)
                        {
                            cod_zona = long.Parse(campo2);
                            if (cod_zona == 0) cod_zona = 1;
                            else cod_zona = _zonas[int.Parse(campo2)].Codigo;
                        }
                    }
                    if (continuar && continuar_formulario && continuar_pergunta)
                    {
                        List<Resposta> resps = get_resposta(campos,cod_zona,perg_associada,ti);
                        foreach (Resposta resp in resps)
                            respostas.Add(resp);
                    }
                }
            }
            return respostas;
        }

        private List<Resposta> get_resposta(
            List<string> campos, 
            long cod_zona, 
            PerguntaQuestionario p, 
            TipoEscala ti)
        {
            List<Resposta> resps = new List<Resposta>();

            foreach (string campo in campos)
            {
                short i_valor;
                string s_valor;

                if (ti.Numero != 0)
                {
                    s_valor = "";
                    i_valor = (short)(short.Parse(campo) + 1);
                }
                else
                {
                    s_valor = campo;
                    i_valor = -1;
                }

                if (ti.Numero == -2 && i_valor == 2)
                {
                    Resposta resp = new Resposta(
                        cod_analise,
                        -1,
                        -1,
                        -1,
                        p.Num_Pergunta,
                        p.Cod_Item,
                        p.Cod_zona != 0 ? p.Cod_zona : cod_zona,
                        i_valor,
                        s_valor,
                        -1,
                        ti.Numero != 0 ? Resposta.TipoResposta.RespostaNum : Resposta.TipoResposta.RespostaStr);
                    resps.Add(resp);
                }
                else if (ti.Numero != -2)
                {
                    Resposta resp = new Resposta(
                        cod_analise,
                        -1,
                        -1,
                        -1,
                        p.Num_Pergunta,
                        p.Cod_Item,
                        p.Cod_zona != 0 ? p.Cod_zona : cod_zona,
                        i_valor,
                        s_valor,
                        -1,
                        ti.Numero != 0 ? Resposta.TipoResposta.RespostaNum : Resposta.TipoResposta.RespostaStr);
                    resps.Add(resp);
                }
            }
            return resps;
        }

        #region verificar Erros
        private void verifica_numero_colunas(
            int num_linha,
            Dictionary<float, List<int>> _perguntas_colunas_ficheiro,
            List<PerguntaQuestionario> _pergs,
            Numero_Respostas _modo_num_respostas,
            ref bool continuar,
            ref bool continuar_formulario)
        {
            if (!tem_colunas_necessarias(num_linha, _perguntas_colunas_ficheiro, _pergs))
            {
                if (_modo_num_respostas == Numero_Respostas.Sair_Numero)
                {
                    erro = erro_numero_respostas;
                    linha_erro = num_linha;
                    continuar = false;
                    continuar_formulario = false;
                }
                else
                {
                    numero_formularios_ignorados++;
                    continuar_formulario = false;
                }
            }
        }

        public void verifica_campo(
            int num_linha,
            string _campo,
            Respostas_Vazias _modo_respostas_vazias,
            Valores_Respostas _modo_valores_respostas,
            PerguntaQuestionario _perg,
            ref bool continuar,
            ref bool continuar_formulario,
            ref bool continuar_pergunta)
        {
            #region Teste Respostas Vazias
            /* Testar Repostas vazias */
            if (soEspacos(_campo))
            {
                if (_modo_respostas_vazias == Respostas_Vazias.Sair_Vazias)
                {
                    erro = erro_repostastas_vazias;
                    linha_erro = num_linha;
                    numero_pergunta_erro = _perg.Num_Pergunta;
                    continuar = false;
                }
                else if (_modo_respostas_vazias == Respostas_Vazias.Ignorar_Formulario)
                {
                    numero_formularios_ignorados++;
                    continuar_formulario = false;
                }
                else if (_modo_respostas_vazias == Respostas_Vazias.Ignorar_Pergunta_Nao_QE &&
                    _perg.TipoQuestao.Equals("qe"))
                {
                    numero_perguntas_ignoradas++;
                    continuar_pergunta = false;
                }
                else
                {
                    numero_perguntas_ignoradas++;
                    continuar_pergunta = false;
                }
            }
            #endregion

            TipoEscala ti = GestaodeRespostas.getTipoEscala(_perg.Cod_TipoEscala);

            #region Teste Valor Resposta

            if (ti.Numero != 0 &&
                ti.Numero != 1 &&
               (!soNumeros(_campo) ||
               (short.Parse(_campo) + 1) <= 0 ||
               (short.Parse(_campo) + 1) > ti.Respostas.Count))
            {
                if (_modo_valores_respostas == Valores_Respostas.Sair_Valores)
                {
                    erro = erro_valores_respostas;
                    linha_erro = num_linha;
                    numero_pergunta_erro = _perg.Num_Pergunta;
                    continuar = false;
                }
                else if (_modo_valores_respostas == Valores_Respostas.Ignorar_Formulario)
                {
                    numero_formularios_ignorados++;
                    continuar_formulario = false;
                }
                else if (_modo_valores_respostas == Valores_Respostas.Ignorar_Pergunta_Nao_QE &&
                    _perg.TipoQuestao.Equals("qe"))
                {
                    numero_perguntas_ignoradas++;
                    continuar_pergunta = false;
                }
                else
                {
                    numero_perguntas_ignoradas++;
                    continuar_pergunta = false;
                }
            }
            #endregion
        }
#endregion

        #endregion

        #endregion

        #region Outros
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

            for(int i = 0 ; i < _perguntas_colunas_ficheiro.Keys.Count ; i++)
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

        private bool soNumeros(string s)
        {
            if (s == "") return false;
            string possiveis = "0123456789.,";
            bool found = true;
            for (int i = 0; i < s.Length && found; i++)
                found = possiveis.Contains(s[i]);
            return found;
        }

        private bool soEspacos(string s)
        {
            if (s == "") return true;
            string possiveis = " \t\n";
            bool found = true;
            for (int i = 0; i < s.Length && found; i++)
                found = possiveis.Contains(s[i]);
            return found;
        }
        #endregion
    }
}
