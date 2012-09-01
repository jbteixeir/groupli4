using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ETdAnalyser.CamadaDados.Classes;
using ETdAnalyser.CamadaDados.DataBaseCommunicator;

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
                    if (te.TipoPredefinido == 0)
                    {
                        te.CodigoTipo = FuncsToDataBase.insertTipoEscala(te);
                        te.TipoPredefinido = 1;
                        foreach (EscalaResposta er in te.Respostas)
                        {
                            er.CodigoTipo = te.CodigoTipo;
                            er.CodigoEscala = FuncsToDataBase.insertEscalaResposta(er);
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
                fa.CodigoPergunta = cod;
            }
            return lst;
        }

        public static List<PerguntaQuestionario> insert_PerguntasQT(List<PerguntaQuestionario> lst)
        {
            foreach (PerguntaQuestionario qt in lst)
            {
                long cod = FuncsToDataBase.insertPerguntaQuestionario(qt);
                qt.CodigoPergunta = cod;
            }
            return lst;
        }

        public static bool isFAcreated(long codigoAnalise)
        {
            return FuncsToDataBase.isFAcreated(codigoAnalise);
        }

        public static bool isQTcreated(long codigoAnalise)
        {
            return FuncsToDataBase.isQTcreated(codigoAnalise);
        }

        public static bool canEditFA(long codigoAnalise)
        {
            bool a = FuncsToDataBase.isFAOnline(codigoAnalise);
            bool b = FuncsToDataBase.haveAnswerFA(codigoAnalise);

            if (b) return false;
            else if (a) return false;
            else return true;
        }

        public static bool canEditQT(long codigoAnalise)
        {
            bool a = FuncsToDataBase.isQTOnline(codigoAnalise);
            bool b = FuncsToDataBase.haveAnswerQT(codigoAnalise);

            if (b) return false;
            else if (a) return false;
            else return true;
        }

        public static List<PerguntaFichaAvaliacao> getPerguntasFA(long codigoAnalise)
        {
            return FuncsToDataBase.selectPerguntasFA(codigoAnalise);
        }

        public static List<PerguntaQuestionario> getPerguntasQT(long codigoAnalise)
        {
            return FuncsToDataBase.selectPerguntasQT(codigoAnalise);
        }

        public static void modificaPerguntasFA(List<PerguntaFichaAvaliacao> pergs)
        {
            foreach (PerguntaFichaAvaliacao p in pergs)
                FuncsToDataBase.updatePerguntasFA(p);
        }

        public static void modificaPerguntasQT(List<PerguntaQuestionario> pergs, long codigoAnalise)
        {
            FuncsToDataBase.deletePerguntasQT(codigoAnalise);
            insert_PerguntasQT(pergs);
        }

        public static void insere_questionario(Questionario q)
        {
            q.CodigoQuestionario = FuncsToDataBase.insertQuestionario(q);

            foreach (Resposta r in q.RespostasMemo)
            {
                r.CodigoQuestionario = q.CodigoQuestionario;
                FuncsToDataBase.insertRespostaQuestionario(r);
            }
            foreach (Resposta r in q.RespostasNumero)
            {
                r.CodigoQuestionario = q.CodigoQuestionario;
                FuncsToDataBase.insertRespostaQuestionario(r);
            }
            foreach (Resposta r in q.RespostasString)
            {
                r.CodigoQuestionario = q.CodigoQuestionario;
                FuncsToDataBase.insertRespostaQuestionario(r);
            }
        }

        public static void insere_ficha_avaliacao(FichaAvaliacao fa)
        {
            fa.CodigoFichaAvaliacao = FuncsToDataBase.insertFichaAvaliacao(fa);

            foreach (Resposta r in fa.RespostasNumero)
            {
                r.CodigoFichaAvaliacao = fa.CodigoFichaAvaliacao;
                FuncsToDataBase.insertRespostaFichaAvaliacao(r);
            }
            foreach (Resposta r in fa.RespostasMemo)
            {
                r.CodigoFichaAvaliacao = fa.CodigoFichaAvaliacao;
                FuncsToDataBase.insertRespostaFichaAvaliacao(r);
            }
            foreach (Resposta r in fa.RespostasString)
            {
                r.CodigoFichaAvaliacao = fa.CodigoFichaAvaliacao;
                FuncsToDataBase.insertRespostaFichaAvaliacao(r);
            }
        }

        public static void insere_CheckList(CheckList c)
        {
            foreach (Resposta r in c.RespostasNumero)
                FuncsToDataBase.insertRespostaCheckList(r);
        }

        public static List<Questionario> getQuestionarios(long codAnalise)
        {
            List<Questionario> questionarios = new List<Questionario>();

            List<long> cods_questionarios = FuncsToDataBase.getCodsQuestionrarios(codAnalise);

            foreach (long codQuestionario in cods_questionarios)
                questionarios.Add(FuncsToDataBase.getQuestionario(codQuestionario,codAnalise));

            return questionarios;
        }

        public static List<FichaAvaliacao> getFichasAvaliacao(long codAnalise)
        {
            List<FichaAvaliacao> fichas_avaliacao = new List<FichaAvaliacao>();
            
            Dictionary<long,long> cods_fichas_avaliacao = FuncsToDataBase.getCodsFichasAvaliacao(codAnalise);

            foreach (long codFichaAvaliacao in cods_fichas_avaliacao.Keys)
                fichas_avaliacao.Add(FuncsToDataBase.getFichaAvaliacao(codFichaAvaliacao, 
                    codAnalise, cods_fichas_avaliacao[codFichaAvaliacao]));
            
            return fichas_avaliacao;
        }

        public static CheckList getChecklist(long codAnalise)
        {
            CheckList checklist = FuncsToDataBase.getCheckList(codAnalise);

            return checklist;
        }
    }
}
