using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdA.Camada_de_Dados.Classes
{
    class Zona
    {
        //Variaveis de Instancia
        private String codZona;
        private String nomeZona;

        //Constructores

        public Zona(String cod, String nome)
        {
            codZona = cod;
            nomeZona = nome;
        }

        public Zona()
        {
            codZona = "";
            nomeZona = "";
        }

        public Zona(Zona i)
        {
            codZona = i.Codigo;
            nomeZona = i.Nome;
        }

        //Métodos
        public String Codigo
        {
            get { return codZona; }
            set { codZona = value; }
        }
        public String Nome
        {
            get { return nomeZona; }
            set { nomeZona = value; }
        }
    }
}
