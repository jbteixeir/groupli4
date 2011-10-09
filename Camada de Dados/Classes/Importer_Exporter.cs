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
        private int numero_pergunta_erro;

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
        public int Numero_Pergunta_Erro
        {
            get { return numero_pergunta_erro; }
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
            List<Item> _itens,
            List<PerguntaQuestionario> _pergs,
            Dictionary<float,int> _perguntas_colunas_ficheiro)
        {
            bool valor_retorno = true;
            formularios = new List<Formulario>();

            for (int i = 0; i < valores.Keys.Count && valor_retorno; i++ )
            {
                Questionario questionario = new Questionario();
                questionario.Cod_Questionario = GestaodeRespostas.insere_questionario(questionario);

                /* Testar nuúmero de perguntas */
                //if (_perguntas_colunas_ficheiro.Keys.Count != )

                for (int j = 0; j < _perguntas_colunas_ficheiro.Keys.Count; j++)
                {
                    PerguntaQuestionario perg = getPerguntaByNum(j,_pergs);
                    TipoEscala ti = GestaodeRespostas.getTipoEscala(perg.Cod_TipoEscala);

                    int coluna_valor = _perguntas_colunas_ficheiro[j];

                    short int_valor;
                    string string_valor;



                    if (ti.Numero != 0)
                    {
                        if ((valores[i])[coluna_valor].Equals("") &&
                            _modo_respostas_vazias.Equals(Respostas_Vazias.Sair_Vazias))
                            valor_retorno = false;

                        int_valor = short.Parse((valores[i])[coluna_valor]);

                        if (int_valor <= 0 || int_valor > ti.Respostas.Count);
                    }
                    else
                        string_valor = (valores[i])[coluna_valor];
                    /*
                    Resposta resp = new Resposta(
                        cod_analise,
                        -1,
                        questionario.Cod_Questionario,
                        -1,
                        j,
                        perg.Cod_Item,
                        perg.Cod_zona != 0 ? perg.Cod_zona : 1,
                        ti.Numero != 0 ? short.Parse(""),
                        ti.Numero != 0 ? "",
                        -1,
                        ti.Numero != 0 ? Resposta.TipoResposta.RespostaNum : Resposta.TipoResposta.RespostaStr);
                    
                    if (ti.Numero != 0)
                        questionario.add_resposta_numero(resp);
                    else
                        questionario.add_resposta_string(resp);
                     * */
                }
            }

            return true;
        }

        #endregion

        #endregion

        private PerguntaQuestionario getPerguntaByNum(float num, List<PerguntaQuestionario> ps)
        {
            for (int i = 0; i < ps.Count; i++)
                if (ps[i].Num_Pergunta == num)
                    return ps[i];
            return null;
        }

        /*
        private bool tem_colunas_necessarias(Dictionary<float, int> _perguntas_colunas_ficheiro)
        {

            int num_colunas_necessarias = 0;

            for(int i = 0 ; i < _perguntas_colunas_ficheiro.Keys.Count ; i++)
            {
                num_colunas_necessarias++;
                PerguntaQuestionario p = getPerguntaByNum(_perguntas_colunas_ficheiro.Keys.ElementAt(i);
                if (_perguntas_colunas_ficheiro[i].Cod_zona == 0)
                    num_colunas_necessarias++;
            }

        }
         */

        /*
        #region Questionario
        public List<Questionario> importar_questionario()
        {
            List<Questionario> questionarios = new List<Questionario>();
            List<PerguntaQue

nario> pergs1 = GestaodeRespostas.getPerguntasQT(cod_analise);
            List<Pergunta> pergs2 = new List<Pergunta>();
            List<TipoEscala> tes = new List<TipoEscala>();

            foreach (PerguntaQuestionario p in pergs1)
            {
                pergs2.Add(p);
                tes.Add(GestaodeRespostas.getTipoEscala(p.Cod_TipoEscala));
            }

            string linha = null;
            if ((linha = ficheiro.ReadLine()) != null)
            {
                if (!cabecalho_correcto(linha.Split(';'), Cabecalho.Sair_Numero_Tipo, pergs2))
                {
                    linha_erro = 1;
                    return null;
                }
            }

            int num_linha = 2;
            while ((linha = ficheiro.ReadLine()) != null)
            {
                string[] linhaSeparada = linha.Split(';');

                Questionario q = compoe_questionario(linhaSeparada,tes, pergs1);
                if (q == null)
                {
                    linha_erro = num_linha;
                    return null;
                }
                questionarios.Add(q);
                num_linha++;
            }
            return questionarios;
        }

        public List<Questionario> importar_questionario(
            Cabecalho _modo_cabecalho,
            Numero_Respostas _modo_num_respostas,
            Respostas_Vazias _modo_respostas_vazias,
            Valores_Respostas _modo_valores_respostas)
        {
            // TODO
            return null;
        }

        private Questionario compoe_questionario(String[] _rs, List<TipoEscala> _tes, List<PerguntaQuestionario> _pergs)
        {
            if (_tes.Count != _rs.Length){
                erro = erro_numero_respostas;
                return null;
            }

            Questionario q = new Questionario();
            for (int i = 0; i < _rs.Length; i++)
            {
                if (soEspacos(_rs[i]))
                {
                    erro = erro_repostastas_vazias;
                    numero_pergunta_erro = i;
                    return null;
                }
                switch (_tes.ElementAt(i).Numero)
                {
                    // string
                    case 0:
                        RespostaQuestionarioString r1 = 
                            new RespostaQuestionarioString(
                                -1,
                                _rs[i],
                                _pergs.ElementAt(i).Cod_Pergunta,
                                i);
                        q.add_resposta_string(r1);
                        break;
                    // strings grandes
                    case 1:
                        RespostaQuestionarioString r2 = 
                            new RespostaQuestionarioString(
                                -1,
                                _rs[i],
                                _pergs.ElementAt(i).Cod_Pergunta,
                                i);
                        q.add_resposta_memo(r2);
                        break;
                    // numeros
                    default:
                        if (!soNumeros(_rs[i]))
                        {
                            erro = erro_valores_respostas;
                            numero_pergunta_erro = i;
                            return null;
                        }

                        int res = int.Parse(_rs[i]);
                        if (_tes.ElementAt(i).Numero != 1 &&
                            res < 1 || res > _tes.ElementAt(i).Respostas.Count)
                        {
                            erro = erro_valores_respostas;
                            numero_pergunta_erro = i;
                            return null;
                        }
                        RespostaQuestionarioNumero r3 = 
                            new RespostaQuestionarioNumero(
                                -1,
                                res,
                                _pergs.ElementAt(i).Cod_Pergunta,
                                i);
                        q.add_resposta_numero(r3);
                        break;
                }
            }
            return q;
        }

        private bool soEspacos(string s)
        {
            if (s == "") return true;
            string possiveis = " \n\t";
            bool found = true;
            for (int i = 0; i < s.Length && found; i++)
                found = possiveis.Contains(s[i]);
            return found;
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


        #endregion
         * */
    }
}
