using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdAnalyser.Camada_de_Dados.Classes
{
	class FichaAvaliacao : Formulario
	{
		private long codFichaAvaliacao;
		private long codZona;
        private List<Resposta> respostas_numero;
        private List<Resposta> respostas_string;
        private List<Resposta> respostas_memo;

        /* Construtores */
        public FichaAvaliacao() :
            base()
        {
            codFichaAvaliacao = -1;
            codZona = -1;
            respostas_memo = new List<Resposta>();
            respostas_string = new List<Resposta>();
            respostas_numero = new List<Resposta>();
        }

        public FichaAvaliacao(long _cod_fa, long _cod_analise, long _cod_zona) :
            base(_cod_analise)
        {
            codFichaAvaliacao = _cod_fa;
            codZona = _cod_zona;
            respostas_memo = new List<Resposta>();
            respostas_string = new List<Resposta>();
            respostas_numero = new List<Resposta>();
        }

        /* Gets\Sets */

		public long CodFichaAvaliacao
		{
			get { return codFichaAvaliacao; }
			set { codFichaAvaliacao = value; }
		}
		public long CodZona
		{
			get { return codZona; }
			set { codZona = value; }
		}

        public List<Resposta> Respostas_Numero
        {
            get
            {
                List<Resposta> novo = new List<Resposta>();
                foreach (Resposta r in respostas_numero)
                    novo.Add(r);
                return novo;
            }
            set
            {
                respostas_numero = new List<Resposta>();
                foreach (Resposta r in value)
                    respostas_numero.Add(r);
            }
        }

        public List<Resposta> Respostas_String
        {
            get
            {
                List<Resposta> novo = new List<Resposta>();
                foreach (Resposta r in respostas_string)
                    novo.Add(r);
                return novo;
            }
            set
            {
                respostas_string = new List<Resposta>();
                foreach (Resposta r in value)
                    respostas_string.Add(r);
            }
        }

        public List<Resposta> Respostas_Memo
        {
            get
            {
                List<Resposta> novo = new List<Resposta>();
                foreach (Resposta r in respostas_memo)
                    novo.Add(r);
                return novo;
            }
            set
            {
                respostas_memo = new List<Resposta>();
                foreach (Resposta r in value)
                    respostas_memo.Add(r);
            }
        }

        /* Metodos */

        public void add_resposta_numero(Resposta r)
        {
            respostas_numero.Add(r);
        }

        public void add_resposta_string(Resposta r)
        {
            respostas_string.Add(r);
        }

        public void add_resposta_memo(Resposta r)
        {
            respostas_memo.Add(r);
        }
	}
}
