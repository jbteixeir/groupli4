using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdA.Camada_de_Dados.Classes
{
	class Pergunta
	{
		private long cod_Pergunta;
		private double num_Pergunta;
		private string texto;
		private long cod_Analise;
		private long cod_Item;
		private long cod_TipoEscala;

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
	}
}
