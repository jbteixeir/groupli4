using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdA.Camada_de_Dados.Classes
{
    class EscalaResposta
    {
        //Variáveis de Instância
        private String cod_escala;
        private String cod_tipo;
        private String descricao;
        private int valorResposta;
 
        //Constructores
        public EscalaResposta(String cod, String tipoE, String desc, int valor)
        {
            cod_escala = cod;
            cod_tipo = tipoE;
            descricao = desc;
            valorResposta = valor;
        }

        public EscalaResposta()
        {
            cod_escala = "";
            cod_tipo = "";
            descricao = "";
            valorResposta = 0;
        }

        public EscalaResposta(EscalaResposta e)
        {
            cod_escala = e.CodEscala;
            cod_tipo = e.CodTipo;
            descricao = e.Descricao;
            valorResposta = e.Valor;
        }

        //Métodos
        public String CodEscala
        {
            get { return cod_escala; }
            set { cod_escala = value; }
        }

        public String CodTipo
        {
            get { return cod_tipo; }
            set { cod_tipo = value; }
        }

        public String Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

        public int Valor
        {
            get { return valorResposta; }
            set { valorResposta = value; }
        }
    }
}
