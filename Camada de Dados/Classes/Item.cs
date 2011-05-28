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
        private int defaultItem;

        //Constructores

        public Item(String cod, String nome, int def)
        {
            codParametro = cod;
            nomeParametro = nome;
            defaultItem = def;
        }

        public Item()
        {
            codParametro = "";
            nomeParametro = "";
            defaultItem = 0;
        }

        public Item(Item i)
        {
            codParametro = i.Codigo;
            nomeParametro = i.Nome;
            defaultItem = i.Default;
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
        public int Default
        {
            get { return defaultItem; }
            set { defaultItem = value; }
        }
    }
}
