using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdAnalyser.Camada_de_Dados.Classes
{
	class PerguntaFichaAvaliacao : Pergunta
	{
        public PerguntaFichaAvaliacao(long cod_Analise, float num_Pergunta, long cod_Item, string texto, long cod_TipoEscala) :
        base(cod_Analise, num_Pergunta, cod_Item, texto, cod_TipoEscala){}

        public PerguntaFichaAvaliacao(long cod_Pergunta,long cod_Analise, float num_Pergunta, long cod_Item, string texto, long cod_TipoEscala) :
        base(cod_Pergunta, cod_Analise, num_Pergunta, cod_Item, texto, cod_TipoEscala){}

        public PerguntaFichaAvaliacao() : base() { }

        public string ToString()
        {
            StringBuilder a = new StringBuilder();
            a.Append("Pergunta Ficha Avaliação:\n");
            a.Append(base.ToString());
            return a.ToString();
        }
	}
}
