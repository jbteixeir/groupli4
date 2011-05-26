using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdA.Camada_de_Dados.Classes
{
    class Item
    {
        //Variaveis de Instancia
        private String codParametro;
        private String nomeParametro;

        //Constructores

        public Item(String cod, String nome)
        {
            codParametro = cod;
            nomeParametro = nome;
        }

        public Item()
        {
            codParametro = "";
            nomeParametro = "";
        }

        public Item(Item i)
        {
            codParametro = i.Codigo;
            nomeParametro = i.Nome;
        }

        //Métodos
        public String Codigo
        {
            get { return codParametro; }
            set { codParametro = value; }
        }
        public String Nome
        {
            get { return nomeParametro; }
            set { nomeParametro = value; }
        }
    }
}
