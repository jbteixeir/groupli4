using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

    public class DataBaseCommunicator
    {
        //Variaveis de Instancia
        private String server;
        private SqlConnection connection;
        private String database;

        //Construtores
        public DataBaseCommunicator(String server, SqlConnection connection)
        {
            this.server = server;
            this.connection = connection;
        }

        //Métodos

        public String Server
        {
            get { return server; }
            set { server = value; }
        }

        public SqlConnection Connection
        {
            get { return connection; }
            set { connection = value; }
        }

        public String DataBase
        {
            get { return database; }
            set { database = value; }
        }

        /*
         * Abre uma ligação com a base de dados
         */
        void connect(String server, String username, String password, String database)
        {
            connection = new SqlConnection( "user id=" + username + ";" +
                                            "password=" + password + ";" +
                                            "server=" + server + ";" +
                                            "Trusted_Connection=yes;" +
                                            "database=" + database + ";" +
                                            "connection timeout=30");
        }

        /*
         * Fecha uma ligação com a base de dados
         */
        void disconnect()
        {
            try
            {
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        /*
         * Executa query's sem ser de leitura à base de dados
         */
        void query(string query)
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
        }

        /*
         * Executa query's de leitura à base de dados
         */
        SqlDataReader readData(string query)
        {
            SqlDataReader reader = null;
            SqlCommand command = new SqlCommand("select * from table",
                                                     connection);
            reader = command.ExecuteReader();
            return reader;
        }

        void insert(List<String> values, List<int> tableNumber);
        void remove(List<String> colunasRestricoes, List<String> itemsRestricoes, int tableNumber);
        void edit(List<String> colunaItem, List<String> items, List<String> colunasRestricoes, List<String> itemsRestricoes, int tableNumber);
        SqlCommand search(List<String> dadosProcura, List<String> dadosRestricoes1, List<String> dadosRestricoes2, List<String> modoRestricao, List<String> dadosGroupBy, List<String> dadosOrderBy, String order, List<int> tableNumber);
    }
