using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdA.Camada_de_Dados.Classes
{
    class Analista
    {
        //Variáveis de Instância
        private String password;
        private String username;

        //Constructores

        public Analista(String user,String pass)
        {
            password = pass;
            username = user;
        }

        public Analista()
        {
            password = "";
            username = "";
        }

        //Métodos

        public String Password
        {
            get { return password; }
            set { password = value; }
        }

        public String Username
        {
            get { return username; }
            set { username = value; }
        }

        
    }

}
