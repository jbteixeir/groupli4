using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdA.Camada_de_Dados.Classes
{
    class Resposta
    {
        //Variaveis de Instancia
        private String numeroPergunta;
        private String descricaoItem;

        //Constructores
        public Resposta(String numero, String descricao)
        {
            numeroPergunta = numero;
            descricaoItem = descricao;
        }

        public Resposta()
        {
            numeroPergunta = "";
            descricaoItem = "";
        }

        //Métodos
        public String Numero
        {
            get { return numeroPergunta; }
            set { numeroPergunta = value; }
        }

        public String Descricao
        {
            get { return descricaoItem; }
            set { descricaoItem = value; }
        }

    }
}
