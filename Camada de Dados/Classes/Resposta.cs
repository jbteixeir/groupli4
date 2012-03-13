using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdA.Camada_de_Dados.Classes
{
    class Resposta
    {
        //Variaveis de Instancia
		private long cod_analise;
		// ID do formulario correspondente
		private long cod_questionario;
		private long cod_fichaAvaliacao;
		private long cod_checklist;
		// ID da pergunta corresponde
		private long cod_pergunta_questionario;
        private float numero_pergunta;

        private long cod_item;
        private long cod_zona;
		// Resposta à pergunta
        private short valor;
        private string valor_string;
        /* Variável tipo
         * 1 - checklist
         * 2 - ficha de avaliacao
         * 3 - questionario
         */
        private int tipo;
		private TipoResposta tipoResposta;

		public enum TipoResposta {RespostaNum,RespostaStr, RespostaMemo};

        //NOTA: na ficha de avaliacao no caso da sugestao, o item e o numero da pergunta são -1
        //Constructores
        public Resposta(long cod_analise, long cod_checklist, long cod_questionario, long cod_fichaAvaliacao, float numero_pergunta, long cod_item, long cod_zona, short valor, string valor_string, int tipo, TipoResposta tipo_Resposta)
        {
            this.cod_analise = cod_analise;
            this.cod_checklist = cod_checklist;
            this.cod_questionario = cod_questionario;
            this.cod_fichaAvaliacao = cod_fichaAvaliacao;
            //this.cod_pergunta_questionario = cod_pergunta_questionario;
            this.numero_pergunta = numero_pergunta;
            this.cod_item = cod_item;
            this.cod_zona = cod_zona;
            this.valor = valor;
            this.valor_string = valor_string;
            this.tipo = tipo;
			this.tipoResposta = tipo_Resposta;
        }

        public Resposta()
        {
			this.cod_questionario = -1;
			this.cod_fichaAvaliacao = -1;
            this.cod_pergunta_questionario = -1;
			this.numero_pergunta = -1;
			this.cod_item = -1;
			this.cod_zona = -1;
			this.valor = -1;
			this.valor_string = "";
			this.tipo = -1;
			this.tipoResposta = TipoResposta.RespostaNum;
        }

		public Resposta(Resposta modelo)
		{
			// TODO: Complete member initialization
			this.Cod_analise = modelo.Cod_analise;
			this.Cod_checklist = modelo.Cod_checklist;
			this.Cod_pergunta = modelo.Cod_pergunta;
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
		public long Cod_questionario
		{
			get { return cod_questionario; }
			set { cod_questionario = value; }
		}


		public long Cod_fichaAvaliacao
		{
			get { return cod_fichaAvaliacao; }
			set { cod_fichaAvaliacao = value; }
		}
		public long Cod_checklist
		{
			get { return cod_checklist; }
			set { cod_checklist = value; }
		}
		public TipoResposta Tipo_Resposta
		{
			get { return tipoResposta; }
			set { tipoResposta = value; }
		}
		public long Cod_pergunta
		{
			get { return cod_pergunta_questionario; }
			set { cod_pergunta_questionario = value; }
		}
		public long Cod_analise
		{
			get { return cod_analise; }
			set { cod_analise = value; }
		}
        public long CodigoQuestionario
        {
            get { return cod_questionario; }
            set { cod_questionario = value; }
        }

        public long CodigoFichaAvaliacao
        {
            get { return cod_fichaAvaliacao; }
            set { cod_fichaAvaliacao = value; }
        }

        public long CodigoCheckList
        {
            get { return cod_checklist; }
            set { cod_checklist = value; }
        }

        public float NumeroPergunta
        {
            get { return numero_pergunta; }
            set { numero_pergunta = value; }
        }

        public long CodigoItem
        {
            get { return cod_item; }
            set { cod_item = value; }
        }

        public long CodigoZona
        {
            get { return cod_zona; }
            set { cod_zona = value; }
        }

        public short Valor
        {
            get { return valor; }
            set { valor = value; }
        }

         public String ValorString
        {
            get { return valor_string; }
            set { valor_string = value; }
        }

        public int Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        public string ToString()
        {
            StringBuilder sb = new StringBuilder("Resposta:\n");
            sb.Append("Cod_analise: " + cod_analise + "\n");
            sb.Append("Cod_QT: " + cod_questionario + "\n");
            sb.Append("Cod_FA: " + cod_fichaAvaliacao + "\n");
            sb.Append("Cod_CL: " + cod_checklist + "\n");
            sb.Append("Numero Pergunta: " + numero_pergunta + "\n");
            sb.Append("Cod_Item: " + cod_item + "\n");
            sb.Append("Cod_Zona: " + cod_zona + "\n");
            sb.Append("Valor: " + valor + "\n");
            sb.Append("Valor string: " + valor_string + "\n");
            sb.Append("Tipo: " + tipo + "\n");
            sb.Append("Tipo Resposta: " + tipoResposta + "\n");

            return sb.ToString();
        }
	}
}
