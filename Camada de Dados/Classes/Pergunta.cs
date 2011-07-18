using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdA.Camada_de_Dados.Classes
{
	class Pergunta
	{
		private long cod_Pergunta;
        private long cod_Analise;
		private double num_Pergunta;
        private long cod_Item;
		private string texto;
		private long cod_TipoEscala;

        public Pergunta(long cod_Analise, double num_Pergunta, long cod_Item, string texto, long cod_TipoEscala)
        {
            this.cod_Analise = cod_Analise;
            this.num_Pergunta = num_Pergunta;
            this.cod_Item = cod_Item;
            this.texto = texto;
            this.cod_TipoEscala = cod_TipoEscala;
        }

        public Pergunta(long cod_Pergunta,long cod_Analise, double num_Pergunta, long cod_Item, string texto, long cod_TipoEscala)
        {
            this.cod_Pergunta = cod_Pergunta;
            this.cod_Analise = cod_Analise;
            this.num_Pergunta = num_Pergunta;
            this.cod_Item = cod_Item;
            this.texto = texto;
            this.cod_TipoEscala = cod_TipoEscala;
        }

        public Pergunta()
        {
            cod_Pergunta = -1;
            cod_Analise = -1;
            num_Pergunta = -1;
            cod_Item = -1;
            texto = "";
            cod_TipoEscala = -1;
        }

		public long Cod_Pergunta
		{
			get { return cod_Pergunta; }
			set { cod_Pergunta = value; }
		}
		public double Num_Pergunta
		{
			get { return num_Pergunta; }
			set { num_Pergunta = value; }
		}
		public string Texto
		{
			get { return texto; }
			set { texto = value; }
		}
		public long Cod_Analise
		{
			get { return cod_Analise; }
			set { cod_Analise = value; }
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
            a.Append("Cod_Analise: " + cod_Analise.ToString() + "\n");
            a.Append("Número Pergunta: " + num_Pergunta.ToString() + "\n");
            a.Append("Cod_Item: " + cod_Item.ToString() + "\n");
            a.Append("Texto: " + texto + "\n");
            a.Append("Cod_TipoEscala: " + cod_TipoEscala.ToString() + "\n");
            return a.ToString();
        }
	}
}
