using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdA.Camada_de_Dados.Classes
{
    class Resposta
    {
        //Variaveis de Instancia
        private long cod_questionario;
        private long cod_fichaAvaliacao;
        private float numero_pergunta;
        private long cod_item;
        private long cod_zona;
        private short valor;
        private string valor_string;
        /*
         * Variável tipo
         * 1 - checklist
         * 2 - ficha de avaliacao
         * 3 - questionario
         */
        private int tipo;
		private TipoResposta tipo_Resposta;

		public enum TipoResposta {RespostaNum,RespostaStr, RespostaMemo};

        //Constructores
        public Resposta(long cod_questionario, long cod_fichaAvaliacao, float numero_pergunta, long cod_item, long cod_zona, short valor, string valor_string, int tipo, TipoResposta tipo_Resposta)
        {
            this.cod_questionario = cod_questionario;
            this.cod_fichaAvaliacao = cod_fichaAvaliacao;
            this.numero_pergunta = numero_pergunta;
            this.cod_item = cod_item;
            this.cod_zona = cod_zona;
            this.valor = valor;
            this.valor_string = valor_string;
            this.tipo = tipo;
			this.tipo_Resposta = tipo_Resposta;
        }

        public Resposta()
        {
            this.cod_questionario = -1;
            this.cod_fichaAvaliacao = -1;
            this.numero_pergunta = -1;
            this.cod_item = -1;
            this.cod_zona = -1;
            this.valor = -1;
            this.valor_string = "";
            this.tipo = -1;
			this.tipo_Resposta = TipoResposta.RespostaNum;
        }

        //Métodos
		public TipoResposta tipoResposta
		{
			get { return tipo_Resposta; }
			set { }
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
        
    }
}
