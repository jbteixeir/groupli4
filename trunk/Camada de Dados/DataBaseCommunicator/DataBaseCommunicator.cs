using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ETdA.Camada_de_Dados.DataBaseCommunicator
{
    class DataBaseCommunicator
    {
        //Variaveis de Instancia
        private String serverName;
        SqlConnection connection;

        //Construtores
        public DataBaseCommunicator(String server, SqlConnection con)
        {
            serverName = server;
            connection = con;
        }

        //Métodos

        void connect(String serverName, String userName, String password);
        void disconnect();
        void insert(List<String> values, List<int> tableNumber);

    }
}
