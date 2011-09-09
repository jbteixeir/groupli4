using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdA.Camada_de_Dados.Classes
{
	class PerguntaQuestionario : Pergunta
	{
		private long cod_zona;
		private string tipoQuestao;

        public PerguntaQuestionario(long cod_Analise, double num_Pergunta, 
            long cod_zona, long cod_Item, string texto, long cod_TipoEscala, string tipoQuestao) :
        base(cod_Analise, num_Pergunta, cod_Item, texto, cod_TipoEscala)
        {
            this.cod_zona = cod_zona;
            this.tipoQuestao = tipoQuestao;
        }

        public PerguntaQuestionario(long cod_Pergunta,long cod_Analise,
            double num_Pergunta, long cod_zona, long cod_Item, string texto, long cod_TipoEscala, string tipoQuestao) :
        base(cod_Pergunta, cod_Analise, num_Pergunta, cod_Item, texto, cod_TipoEscala)
        {
            this.cod_zona = cod_zona;
            this.tipoQuestao = tipoQuestao;
        }

        public PerguntaQuestionario() : base()
        {
            cod_zona = -1;
            tipoQuestao = "";
        }

        public long Cod_zona
        {
            get { return cod_zona; }
            set { cod_zona = value; } 
        }

		public string TipoQuestao 
        {
            get { return tipoQuestao; }
            set { tipoQuestao = value; }
        }
	}
}
