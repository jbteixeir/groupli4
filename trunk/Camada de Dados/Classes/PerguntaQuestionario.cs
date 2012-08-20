using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdAnalyser.Camada_de_Dados.Classes
{
	class PerguntaQuestionario : Pergunta
	{
		private long cod_zona;
		private string tipoQuestao;

        public PerguntaQuestionario(long cod_Analise, float num_Pergunta, 
            long cod_zona, long cod_Item, string texto, long cod_TipoEscala, string tipoQuestao) :
        base(cod_Analise, num_Pergunta, cod_Item, texto, cod_TipoEscala)
        {
            this.cod_zona = cod_zona;
            this.tipoQuestao = tipoQuestao;
        }

        public PerguntaQuestionario(long cod_Pergunta,long cod_Analise,
            float num_Pergunta, long cod_zona, long cod_Item, string texto, long cod_TipoEscala, string tipoQuestao) :
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

        public PerguntaQuestionario(PerguntaQuestionario p)
            : base(p)
        {
            cod_zona = p.cod_zona;
            tipoQuestao = p.tipoQuestao;
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

        public PerguntaQuestionario Clone()
        {
            return new PerguntaQuestionario(this);
        }

        public string ToString()
        {
            StringBuilder sb = new StringBuilder(base.ToString());
            sb.Append("Pergunta Questionário\n");
            sb.Append("Cod_Zona: " + cod_zona + "\n");
            sb.Append("Tipo de Questão: " + tipoQuestao + "\n");
            return sb.ToString();
        }
	}
}
