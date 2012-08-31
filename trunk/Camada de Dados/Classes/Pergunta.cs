using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdAnalyser.Camada_de_Dados.Classes
{
	class Pergunta
	{
		private long cod_Pergunta;
        private long codigoAnalise;
		private float num_Pergunta;
        private long cod_Item;
		private string texto;
		private long cod_TipoEscala;

        public Pergunta(long codigoAnalise, float num_Pergunta, long cod_Item, string texto, long cod_TipoEscala)
        {
            this.codigoAnalise = codigoAnalise;
            this.num_Pergunta = num_Pergunta;
            this.cod_Item = cod_Item;
            this.texto = texto;
            this.cod_TipoEscala = cod_TipoEscala;
        }

        public Pergunta(long cod_Pergunta,long codigoAnalise, float num_Pergunta, long cod_Item, string texto, long cod_TipoEscala)
        {
            this.cod_Pergunta = cod_Pergunta;
            this.codigoAnalise = codigoAnalise;
            this.num_Pergunta = num_Pergunta;
            this.cod_Item = cod_Item;
            this.texto = texto;
            this.cod_TipoEscala = cod_TipoEscala;
        }

        public Pergunta()
        {
            cod_Pergunta = -1;
            codigoAnalise = -1;
            num_Pergunta = -1;
            cod_Item = -1;
            texto = "";
            cod_TipoEscala = -1;
        }

        public Pergunta(Pergunta p)
        {
            cod_Pergunta = p.cod_Pergunta;
            codigoAnalise = p.codigoAnalise;
            num_Pergunta = p.num_Pergunta;
            cod_Item = p.cod_Item;
            texto = p.texto;
            cod_TipoEscala = p.cod_TipoEscala;
        }

		public long Cod_Pergunta
		{
			get { return cod_Pergunta; }
			set { cod_Pergunta = value; }
		}
		public float Num_Pergunta
		{
			get { return num_Pergunta; }
			set { num_Pergunta = value; }
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
		public long Cod_Item
		{
			get { return cod_Item; }
			set { cod_Item = value; }
		}
		public long Cod_TipoEscala
		{
			get { return cod_TipoEscala; }
			set { cod_TipoEscala = value; }
		}

        public string ToString()
        {
            StringBuilder a = new StringBuilder();
            a.Append("Pergunta:\n");
            a.Append("Cod_Pergunta: " + cod_Pergunta.ToString() + "\n");
            a.Append("codigoAnalise: " + codigoAnalise.ToString() + "\n");
            a.Append("Número Pergunta: " + num_Pergunta.ToString() + "\n");
            a.Append("Cod_Item: " + cod_Item.ToString() + "\n");
            a.Append("Texto: " + texto + "\n");
            a.Append("Cod_TipoEscala: " + cod_TipoEscala.ToString() + "\n");
            return a.ToString();
        }

        public Pergunta Clone()
        {
            return new Pergunta(this);
        }
	}
}
