using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdA.Camada_de_Dados.Classes
{
	class Questionario : Formulario
	{
		private long codQuestionario;

		public long CodQuestionario
		{
			get { return codQuestionario; }
			set { codQuestionario = value; }
		}
	}
}
