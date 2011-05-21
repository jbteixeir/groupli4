using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdA.Camada_de_Dados
{
    class EscalaResposta
    {
        //Variáveis de Instância
        private String tipo;
        private List<String> OpcoesResposta;
        private List<short> valorResposta;
 
        //Constructores
        public EscalaResposta(String tipoE, List<String> opcoes, List<short> valor)
        {
            tipo = tipoE;
            OpcoesResposta = opcoes;
            valorResposta = valor;
        }

        public EscalaResposta()
        {
            tipo = "";
            OpcoesResposta = new List<string> ();
            valorResposta = new List<short> ();
        }
    }
}
