using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdAnalyser.CamadaDados.Classes
{
	class PerguntaQuestionario : Pergunta
	{
		private long codigoZona;
		private string tipoQuestao;

        public PerguntaQuestionario(long codigoAnalise, float num_Pergunta, 
            long cod_zona, long cod_Item, string texto, long cod_TipoEscala, string tipoQuestao) :
        base(codigoAnalise, num_Pergunta, cod_Item, texto, cod_TipoEscala)
        {
            this.codigoZona = cod_zona;
            this.tipoQuestao = tipoQuestao;
        }

        public PerguntaQuestionario(long cod_Pergunta,long codigoAnalise,
            float num_Pergunta, long cod_zona, long cod_Item, string texto, long cod_TipoEscala, string tipoQuestao) :
        base(cod_Pergunta, codigoAnalise, num_Pergunta, cod_Item, texto, cod_TipoEscala)
        {
            this.codigoZona = cod_zona;
            this.tipoQuestao = tipoQuestao;
        }

        public PerguntaQuestionario() : base()
        {
            codigoZona = -1;
            tipoQuestao = "";
        }

        public PerguntaQuestionario(PerguntaQuestionario p)
            : base(p)
        {
            codigoZona = p.codigoZona;
            tipoQuestao = p.tipoQuestao;
        }

        public long CodigoZona
        {
            get { return codigoZona; }
            set { codigoZona = value; } 
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
            sb.Append("Cod_Zona: " + codigoZona + "\n");
            sb.Append("Tipo de Questão: " + tipoQuestao + "\n");
            return sb.ToString();
        }
	}
}
