using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdAnalyser.CamadaDados.Classes
{
    class EscalaResposta
    {
        //Variáveis de Instância
        private long codigoEscala;
        private long codigoTipo;
        private String descricao;
        private int valorResposta;
 
        //Constructores
        public EscalaResposta(long codigo, long tipoEscala, String descricao, int valor)
        {
            this.codigoEscala = codigo;
            this.codigoTipo = tipoEscala;
            this.descricao = descricao;
            this.valorResposta = valor;
        }

        public EscalaResposta(long tipoEscala, string descricao, int valor)
        {
            this.codigoTipo = tipoEscala;
            this.descricao = descricao;
            this.valorResposta = valor;
        }

        public EscalaResposta(EscalaResposta escalaResposta)
        {
            codigoEscala = escalaResposta.CodigoEscala;
            codigoTipo = escalaResposta.CodigoTipo;
            descricao = escalaResposta.Descricao;
            valorResposta = escalaResposta.Valor;
        }

        //Métodos
        public long CodigoEscala
        {
            get { return codigoEscala; }
            set { codigoEscala = value; }
        }

        public long CodigoTipo
        {
            get { return codigoTipo; }
            set { codigoTipo = value; }
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

        public EscalaResposta clone()
        {
            return new EscalaResposta(this);
        }
    }
}
