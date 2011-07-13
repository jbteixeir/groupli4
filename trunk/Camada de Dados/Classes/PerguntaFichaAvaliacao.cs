using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdA.Camada_de_Dados.Classes
{
	class PerguntaFichaAvaliacao : Pergunta
	{
        public PerguntaFichaAvaliacao(long cod_Analise, double num_Pergunta, long cod_Item, string texto, long cod_TipoEscala) :
        base(cod_Analise, num_Pergunta, cod_Item, texto, cod_TipoEscala){}

        public PerguntaFichaAvaliacao(long cod_Pergunta,long cod_Analise, double num_Pergunta, long cod_Item, string texto, long cod_TipoEscala) :
        base(cod_Pergunta, num_Pergunta, cod_Item, texto, cod_TipoEscala){}

        public PerguntaFichaAvaliacao() : base() { }
	}
}
