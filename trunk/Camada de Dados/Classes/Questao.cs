using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdA.Camada_de_Dados
{
    class Questao
    {
        //Variaveis de Instancia
        private short numeroPergunta;
        private String descricaoPergunta;

        //Constructores
        public Questao(short numero, String descricao)
        {
            numeroPergunta = numero;
            descricaoPergunta = descricao;
        }

        public Questao()
        {
            numeroPergunta = 0;
            descricaoPergunta = "";
        }

        //Métodos

        public short Numero
        {
            get { return numeroPergunta; }
            set { numeroPergunta = value; }
        }

        public String Descricao
        {
            get { return descricaoPergunta; }
            set { descricaoPergunta = value; }
        }
    }
}
