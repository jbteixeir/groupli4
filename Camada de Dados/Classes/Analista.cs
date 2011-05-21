using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdA.Camada_de_Dados
{
    class Analista
    {
        //Variáveis de Instância
        private String nome;
        private String password;
        private String username;

        //Constructores

        public Analista(String name, String pass, String user)
        {
            nome = name;
            password = pass;
            username = user;
        }

        public Analista()
        {
            nome = "";
            password = "";
            username = "";
        }
    }

}
