using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdAnalyser.CamadaDados.Classes
{
	class FichaAvaliacao : Formulario
	{
		private long codigoFichaAvaliacao;
		private long codigoZona;
        private List<Resposta> respostasNumero;
        private List<Resposta> respostasString;
        private List<Resposta> respostasMemo;

        /* Construtores */
        public FichaAvaliacao() :
            base()
        {
            this.codigoFichaAvaliacao = -1;
            this.codigoZona = -1;
            this.respostasMemo = new List<Resposta>();
            this.respostasString = new List<Resposta>();
            this.respostasNumero = new List<Resposta>();
        }

        public FichaAvaliacao(long codigoFichaAvaliacao, long codigoAnalise, long codigoZona) :
            base(codigoAnalise)
        {
            this.codigoFichaAvaliacao = codigoFichaAvaliacao;
            this.codigoZona = codigoZona;
            this.respostasMemo = new List<Resposta>();
            this.respostasString = new List<Resposta>();
            this.respostasNumero = new List<Resposta>();
        }

        /* Gets\Sets */

		public long CodigoFichaAvaliacao
		{
			get { return codigoFichaAvaliacao; }
			set { codigoFichaAvaliacao = value; }
		}
		public long CodigoZona
		{
			get { return codigoZona; }
			set { codigoZona = value; }
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

        /* Metodos */

        public void adicionarRespostaNumero(Resposta r)
        {
            respostasNumero.Add(r);
        }

        public void adicionarRespostaString(Resposta r)
        {
            respostasString.Add(r);
        }

        public void adicionarRespostaMemo(Resposta r)
        {
            respostasMemo.Add(r);
        }
	}
}
