using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ETdAnalyser.Camada_de_Dados.Classes;
using ETdAnalyser.Camada_de_Dados.DataBaseCommunicator;

namespace ETdAnalyser.Camada_de_Negócio
{
    class GestaodeRespostas
    {
        public static Dictionary<string, List<TipoEscala>> getTipResposta()
        {
            return FuncsToDataBase.getTiposResposta();
        }

        public static TipoEscala getTipoEscala(long codTipoEscala)
        {
            return FuncsToDataBase.selectTipoEscala(codTipoEscala);
        }

        public static Dictionary<string, List<TipoEscala>> insertNovosTipos(Dictionary<string, List<TipoEscala>> lst)
        {
            foreach (List<TipoEscala> ll in lst.Values)
                foreach (TipoEscala te in ll)
                {
                    if (te.Default == 0)
                    {
                        te.Codigo = FuncsToDataBase.insertTipoEscala(te);
                        te.Default = 1;
                        foreach (EscalaResposta er in te.Respostas)
                        {
                            er.CodTipo = te.Codigo;
                            er.CodEscala = FuncsToDataBase.insertEscalaResposta(er);
                        }
                    }
                }
            return lst;
        }

        public static List<PerguntaFichaAvaliacao> insert_PerguntasFA(List<PerguntaFichaAvaliacao> lst)
        {
            foreach (PerguntaFichaAvaliacao fa in lst)
            {
                long cod = FuncsToDataBase.insertPerguntaFichaAvaliacao(fa);
                fa.Cod_Pergunta = cod;
            }
            return lst;
        }

        public static List<PerguntaQuestionario> insert_PerguntasQT(List<PerguntaQuestionario> lst)
        {
            foreach (PerguntaQuestionario qt in lst)
            {
                long cod = FuncsToDataBase.insertPerguntaQuestionario(qt);
                qt.Cod_Pergunta = cod;
            }
            return lst;
        }

        public static bool isFAcreated(long codAnalise)
        {
            return FuncsToDataBase.isFAcreated(codAnalise);
        }

        public static bool isQTcreated(long codAnalise)
        {
            return FuncsToDataBase.isQTcreated(codAnalise);
        }

        public static bool canEditFA(long codAnalise)
        {
            bool a = FuncsToDataBase.isFAOnline(codAnalise);
            bool b = FuncsToDataBase.haveAnswerFA(codAnalise);

            if (b) return false;
            else if (a) return false;
            else return true;
        }

        public static bool canEditQT(long codAnalise)
        {
            bool a = FuncsToDataBase.isQTOnline(codAnalise);
            bool b = FuncsToDataBase.haveAnswerQT(codAnalise);

            if (b) return false;
            else if (a) return false;
            else return true;
        }

        public static List<PerguntaFichaAvaliacao> getPerguntasFA(long codAnalise)
        {
            return FuncsToDataBase.selectPerguntasFA(codAnalise);
        }

        public static List<PerguntaQuestionario> getPerguntasQT(long codAnalise)
        {
            return FuncsToDataBase.selectPerguntasQT(codAnalise);
        }

        public static void modificaPerguntasFA(List<PerguntaFichaAvaliacao> pergs)
        {
            foreach (PerguntaFichaAvaliacao p in pergs)
                FuncsToDataBase.updatePerguntasFA(p);
        }

        public static void modificaPerguntasQT(List<PerguntaQuestionario> pergs, long codAnalise)
        {
            FuncsToDataBase.deletePerguntasQT(codAnalise);
            insert_PerguntasQT(pergs);
        }

        public static void insere_questionario(Questionario q)
        {
            q.Cod_Questionario = FuncsToDataBase.insertQuestionario(q);

            foreach (Resposta r in q.Respostas_Memo)
            {
                r.CodigoQuestionario = q.Cod_Questionario;
                FuncsToDataBase.insertRespostaQuestionario(r);
            }
            foreach (Resposta r in q.Respostas_Numero)
            {
                r.CodigoQuestionario = q.Cod_Questionario;
                FuncsToDataBase.insertRespostaQuestionario(r);
            }
            foreach (Resposta r in q.Respostas_String)
            {
                r.CodigoQuestionario = q.Cod_Questionario;
                FuncsToDataBase.insertRespostaQuestionario(r);
            }
        }

        public static void insere_ficha_avaliacao(FichaAvaliacao fa)
        {
            fa.CodFichaAvaliacao = FuncsToDataBase.insertFichaAvaliacao(fa);

            foreach (Resposta r in fa.Respostas_Numero)
            {
                r.CodigoFichaAvaliacao = fa.CodFichaAvaliacao;
                FuncsToDataBase.insertRespostaFichaAvaliacao(r);
            }
            foreach (Resposta r in fa.Respostas_Memo)
            {
                r.CodigoFichaAvaliacao = fa.CodFichaAvaliacao;
                FuncsToDataBase.insertRespostaFichaAvaliacao(r);
            }
            foreach (Resposta r in fa.Respostas_String)
            {
                r.CodigoFichaAvaliacao = fa.CodFichaAvaliacao;
                FuncsToDataBase.insertRespostaFichaAvaliacao(r);
            }
        }

        public static void insere_CheckList(CheckList c)
        {
            foreach (Resposta r in c.Respostas_Numero)
                FuncsToDataBase.insertRespostaCheckList(r);
        }
    }
}
