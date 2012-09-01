using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdAnalyser.CamadaDados.Classes
{
    class Resposta
    {
        //Variaveis de Instancia
		private long codigoAnalise;
		// ID do formulario correspondente
		private long codigoQuestionario;
		private long codigoFichaAvaliacao;
		private long codigoCheckList;
		// ID da pergunta corresponde
		private long codigoPerguntaQuestionario;
        private float numeroPergunta;

        private long codigoItem;
        private long codigoZona;
		// Resposta à pergunta
        private short valor;
        private string valorString;
        /* Variável tipo
         * 1 - checklist
         * 2 - ficha de avaliacao
         * 3 - questionario
         */
        private int tipo;
		private TipoResposta tipoResposta;

		public enum TipoResposta {RespostaNum,RespostaStr, RespostaMemo};

        //NOTA: na ficha de avaliacao no caso da sugestao, o item escalaResposta o numero da pergunta são -1
        //Constructores
        public Resposta(long codigoAnalise, long cod_checklist, long cod_questionario, long cod_fichaAvaliacao, float numero_pergunta, long cod_item, long cod_zona, short valor, string valor_string, int tipo, TipoResposta tipo_Resposta)
        {
            this.codigoAnalise = codigoAnalise;
            this.codigoCheckList = cod_checklist;
            this.codigoQuestionario = cod_questionario;
            this.codigoFichaAvaliacao = cod_fichaAvaliacao;
            //this.codigoPerguntaQuestionario = codigoPerguntaQuestionario;
            this.numeroPergunta = numero_pergunta;
            this.codigoItem = cod_item;
            this.codigoZona = cod_zona;
            this.valor = valor;
            this.valorString = valor_string;
            this.tipo = tipo;
			this.tipoResposta = tipo_Resposta;
        }

        public Resposta()
        {
			this.codigoQuestionario = -1;
			this.codigoFichaAvaliacao = -1;
            this.codigoPerguntaQuestionario = -1;
			this.numeroPergunta = -1;
			this.codigoItem = -1;
			this.codigoZona = -1;
			this.valor = -1;
			this.valorString = "";
			this.tipo = -1;
			this.tipoResposta = TipoResposta.RespostaNum;
        }

		public Resposta(Resposta modelo)
		{
			// TODO: Complete member initialization
			this.codigoAnalise = modelo.codigoAnalise;
			this.CodigoChecklist = modelo.CodigoChecklist;
			this.CodigoPergunta = modelo.CodigoPergunta;
			this.CodigoFichaAvaliacao = modelo.CodigoFichaAvaliacao;
			this.CodigoItem = CodigoItem;
			this.CodigoQuestionario = modelo.CodigoQuestionario;
			this.CodigoZona = modelo.CodigoZona;
			this.NumeroPergunta = modelo.NumeroPergunta;
			this.Tipo = modelo.Tipo;
			this.tipoResposta = modelo.tipoResposta;
			this.Valor = modelo.Valor;
			this.ValorString = modelo.ValorString;
		}

		//Métodos
		public long CodigoFichaAvaliacao
		{
			get { return codigoFichaAvaliacao; }
			set { codigoFichaAvaliacao = value; }
		}
		public long CodigoChecklist
		{
			get { return codigoCheckList; }
			set { codigoCheckList = value; }
		}
		public TipoResposta GetTipoResposta
		{
			get { return tipoResposta; }
			set { tipoResposta = value; }
		}
		public long CodigoPergunta
		{
			get { return codigoPerguntaQuestionario; }
			set { codigoPerguntaQuestionario = value; }
		}
		public long CodigoAnalise
		{
			get { return codigoAnalise; }
			set { codigoAnalise = value; }
		}
        public long CodigoQuestionario
        {
            get { return codigoQuestionario; }
            set { codigoQuestionario = value; }
        }

        public long CodigoCheckList
        {
            get { return codigoCheckList; }
            set { codigoCheckList = value; }
        }

        public float NumeroPergunta
        {
            get { return numeroPergunta; }
            set { numeroPergunta = value; }
        }

        public long CodigoItem
        {
            get { return codigoItem; }
            set { codigoItem = value; }
        }

        public long CodigoZona
        {
            get { return codigoZona; }
            set { codigoZona = value; }
        }

        public short Valor
        {
            get { return valor; }
            set { valor = value; }
        }

         public String ValorString
        {
            get { return valorString; }
            set { valorString = value; }
        }

        public int Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        public string ToString()
        {
            StringBuilder sb = new StringBuilder("Resposta:\n");
            sb.Append("codigoAnalise: " + codigoAnalise + "\n");
            sb.Append("Cod_QT: " + codigoQuestionario + "\n");
            sb.Append("Cod_FA: " + codigoFichaAvaliacao + "\n");
            sb.Append("Cod_CL: " + codigoCheckList + "\n");
            sb.Append("Numero Pergunta: " + numeroPergunta + "\n");
            sb.Append("Cod_Item: " + codigoItem + "\n");
            sb.Append("Cod_Zona: " + codigoZona + "\n");
            sb.Append("Valor: " + valor + "\n");
            sb.Append("Valor string: " + valorString + "\n");
            sb.Append("Tipo: " + tipo + "\n");
            sb.Append("Tipo Resposta: " + tipoResposta + "\n");

            return sb.ToString();
        }
	}
}
