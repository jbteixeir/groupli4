using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdA.Camada_de_Dados
{
    class Items
    {
        //Variaveis de Instancia
        private String codParametro;
        private String nomeParametro;
        private String descricaoParametro;

        //Constructores

        public Items(String cod, String nome, String descricao)
        {
            codParametro = cod;
            nomeParametro = nome;
            descricaoParametro = descricao;
        }

        public Items()
        {
            codParametro = "";
            nomeParametro = "";
            descricaoParametro = "";
        }
    }
}
