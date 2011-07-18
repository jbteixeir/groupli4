using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdA.Camada_de_Dados.Classes
{
	class FichaAvaliacao : Formulario
	{
		private long codFichaAvaliacao;
		private long codZona;


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
		

	}
}
