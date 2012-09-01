using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdAnalyser.CamadaDados.Classes
{
	class Pergunta
	{
		private long codigoPergunta;
        private long codigoAnalise;
		private float numeroPergunta;
        private long codigoItem;
		private string texto;
		private long codigoTipoEscala;

        public Pergunta(long codigoAnalise, float num_Pergunta, long cod_Item, string texto, long cod_TipoEscala)
        {
            this.codigoAnalise = codigoAnalise;
            this.numeroPergunta = num_Pergunta;
            this.codigoItem = cod_Item;
            this.texto = texto;
            this.codigoTipoEscala = cod_TipoEscala;
        }

        public Pergunta(long cod_Pergunta,long codigoAnalise, float num_Pergunta, long cod_Item, string texto, long cod_TipoEscala)
        {
            this.codigoPergunta = cod_Pergunta;
            this.codigoAnalise = codigoAnalise;
            this.numeroPergunta = num_Pergunta;
            this.codigoItem = cod_Item;
            this.texto = texto;
            this.codigoTipoEscala = cod_TipoEscala;
        }

        public Pergunta()
        {
            codigoPergunta = -1;
            codigoAnalise = -1;
            numeroPergunta = -1;
            codigoItem = -1;
            texto = "";
            codigoTipoEscala = -1;
        }

        public Pergunta(Pergunta p)
        {
            codigoPergunta = p.codigoPergunta;
            codigoAnalise = p.codigoAnalise;
            numeroPergunta = p.numeroPergunta;
            codigoItem = p.codigoItem;
            texto = p.texto;
            codigoTipoEscala = p.codigoTipoEscala;
        }

		public long CodigoPergunta
		{
			get { return codigoPergunta; }
			set { codigoPergunta = value; }
		}
		public float NumeroPergunta
		{
			get { return numeroPergunta; }
			set { numeroPergunta = value; }
		}
		public string Texto
		{
			get { return texto; }
			set { texto = value; }
		}
		public long CodigoAnalise
		{
			get { return codigoAnalise; }
			set { codigoAnalise = value; }
		}
		public long CodigoItem
		{
			get { return codigoItem; }
			set { codigoItem = value; }
		}
		public long CodigoTipoEscala
		{
			get { return codigoTipoEscala; }
			set { codigoTipoEscala = value; }
		}

        public string ToString()
        {
            StringBuilder a = new StringBuilder();
            a.Append("Pergunta:\n");
            a.Append("Cod_Pergunta: " + codigoPergunta.ToString() + "\n");
            a.Append("codigoAnalise: " + codigoAnalise.ToString() + "\n");
            a.Append("Número Pergunta: " + numeroPergunta.ToString() + "\n");
            a.Append("Cod_Item: " + codigoItem.ToString() + "\n");
            a.Append("Texto: " + texto + "\n");
            a.Append("Cod_TipoEscala: " + codigoTipoEscala.ToString() + "\n");
            return a.ToString();
        }

        public Pergunta Clone()
        {
            return new Pergunta(this);
        }
	}
}
