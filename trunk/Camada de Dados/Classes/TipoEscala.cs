using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdA.Camada_de_Dados.Classes
{
    class TipoEscala
    {
        //Variáveis de instância
        String codTipo;
        String descricao;
        int numero;
        int defaultTipo;

        //Constructores
        public TipoEscala(String cod, String desc, int num, int def)
        {
            codTipo = cod;
            descricao = desc;
            numero = num;
            defaultTipo = def;
        }

        public TipoEscala()
        {
            codTipo = "";
            descricao = "";
            numero = 0;
            defaultTipo = 0;
        }

        public TipoEscala(TipoEscala t)
        {
            codTipo = t.Codigo;
            descricao = t.Descricao;
            numero = t.Numero;
            defaultTipo = t.Default;
        }

        //Métodos
        public String Codigo
        {
            get { return codTipo; }
            set { codTipo = value; }
        }

        public String Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }
        public int Numero
        {
            get { return numero; }
            set { numero= value; }
        }

        public int Default
        {
            get { return defaultTipo; }
            set { defaultTipo = value; }
        }
    }
}
