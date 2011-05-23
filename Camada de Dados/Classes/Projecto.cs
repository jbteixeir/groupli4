using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdA
{
    class Projecto
    {

        //Variaveis de instância
        private String codProjecto;
        private String nomeEstabelecimento;

        //Constructores
        public Projecto(String codProj, String nomeEst)
        {
            codProjecto = codProj;
            nomeEstabelecimento = nomeEst;
        }

        public Projecto ()
        {
            codProjecto = "";
            nomeEstabelecimento = "";
        }

        //Métodos

        public String Codigo
        {
            get { return codProjecto; }
            set { codProjecto = value; }
        }
        public String Nome
        {
            get { return nomeEstabelecimento; }
            set { nomeEstabelecimento = value; }
        }


    }
}
