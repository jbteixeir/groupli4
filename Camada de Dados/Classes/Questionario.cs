using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdAnalyser.CamadaDados.Classes
{
	class Questionario : Formulario
	{
        private long codigoQuestionario;
        private List<Resposta> respostasNumero;
        private List<Resposta> respostasString;
        private List<Resposta> respostasMemo;

        public Questionario() :
            base()
        {
            codigoQuestionario = -1;
            respostasMemo = new List<Resposta>();
            respostasString = new List<Resposta>();
            respostasNumero = new List<Resposta>();
        }

        public Questionario(long _cod_questionario, long _codigoAnalise) :
            base(_codigoAnalise)
        {
            codigoQuestionario = _cod_questionario;
            respostasMemo = new List<Resposta>();
            respostasString = new List<Resposta>();
            respostasNumero = new List<Resposta>();
        }

        // Gets / Sets

        public long CodigoQuestionario
        {
            get { return codigoQuestionario; }
            set { codigoQuestionario = value; }
        }

        public List<Resposta> RespostasNumero
        {
            get
            {
                List<Resposta> novo = new List<Resposta>();
                foreach (Resposta r in respostasNumero)
                    novo.Add(r);
                return novo;
            }
            set
            {
                respostasNumero = new List<Resposta>();
                foreach (Resposta r in value)
                    respostasNumero.Add(r);
            }
        }

        public List<Resposta> RespostasString
        {
            get
            {
                List<Resposta> novo = new List<Resposta>();
                foreach (Resposta r in respostasString)
                    novo.Add(r);
                return novo;
            }
            set
            {
                respostasString = new List<Resposta>();
                foreach (Resposta r in value)
                    respostasString.Add(r);
            }
        }

        public List<Resposta> RespostasMemo
        {
            get
            {
                List<Resposta> novo = new List<Resposta>();
                foreach (Resposta r in respostasMemo)
                    novo.Add(r);
                return novo;
            }
            set
            {
                respostasMemo = new List<Resposta>();
                foreach (Resposta r in value)
                    respostasMemo.Add(r);
            }
        }

        // Metodos 

        public void AdicionarRespostaNumero(Resposta r)
        {
            respostasNumero.Add(r);
        }

        public void AdicionarRespostaString(Resposta r)
        {
            respostasString.Add(r);
        }

        public void AdicionarRespostaMemo(Resposta r)
        {
            respostasMemo.Add(r);
        }
	}
}
