using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdA.Camada_de_Dados.Classes
{
    class Resposta
    {
        //Variaveis de Instancia
        private float numero_pergunta;
        private int cod_item;
        private int cod_zona;
        private int valor;
        /*
         * Variável tipo
         * 1 - checklist
         * 2 - ficha de avaliacao
         * 3 - questionario
         */
        private int tipo;


        //Constructores
        public Resposta(float numero_pergunta, int cod_item, int cod_zona, int valor, int tipo)
        {
            this.numero_pergunta = numero_pergunta;
            this.cod_item = cod_item;
            this.cod_zona = cod_zona;
            this.valor = valor;
            this.tipo = tipo;
        }

        public Resposta()
        {
            this.numero_pergunta = -1;
            this.cod_item = -1;
            this.cod_zona = -1;
            this.valor = -1;
            this.tipo = -1;
        }

        //Métodos
        public float NumeroPergunta
        {
            get { return numero_pergunta; }
            set { numero_pergunta = value; }
        }

        public int CodigoItem
        {
            get { return cod_item; }
            set { cod_item = value; }
        }

        public int CodigoZona
        {
            get { return cod_zona; }
            set { cod_zona = value; }
        }

        public int Valor
        {
            get { return valor; }
            set { valor = value; }
        }

        public int Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

    }
}
