using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdA.Camada_de_Dados.Classes
{
	class PerguntaQuestionario : Pergunta
	{
		private long cod_zona;
		private string tipoResposta;

		public long Cod_zona { get; set; }
		public string TipoResposta { get; set; }
	}
}
