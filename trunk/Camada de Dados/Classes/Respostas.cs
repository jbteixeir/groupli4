using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdA.Camada_de_Dados
{
    class Respostas
    {
        //Variaveis de Instancia
        private String numeroPergunta;
        private String descricaoItem;

        //Constructores
        public Respostas(String numero, String descricao)
        {
            numeroPergunta = numero;
            descricaoItem = descricao;
        }

        public Respostas()
        {
            numeroPergunta = "";
            descricaoItem = "";
        }
    }
}
