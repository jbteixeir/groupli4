using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdA.Camada_de_Dados.Classes
{
	class Questionario
	{
		private long codQuestionario;
		private long codAnalise;

		public long CodAnalise
		{
			get { return codAnalise; }
			set { codAnalise = value; }
		}
		public long CodQuestionario
		{
			get { return codQuestionario; }
			set { codQuestionario = value; }
		}
	}
}
