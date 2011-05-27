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
        private static String server;
        private static SqlConnection connection;
        private static String database;

        //Construtores
        /*public DataBaseCommunicator(String server, SqlConnection connection)
        {
            this.server = server;
            this.connection = connection;
        }*/

        //Métodos

        public static String Server
        {
            get { return server; }
            set { server = value; }
        }

        public static SqlConnection Connection
        {
            get { return connection; }
            set { connection = value; }
        }

        public static String DataBase
        {
            get { return database; }
            set { database = value; }
        }

        /* ----------------------------------------------*/

        /*
         * Abre uma ligação com a base de dados Para Registar Analista
         */
        public static bool connectToSuper(String server, String username, String password, String database)
        {
            try
            {
                string con = "Data Source=" + server + ";" +
                             "Initial Catalog=" + database + ";" +
                             "User ID=" + username + ";" +
                             "Password=" + password + ";" +
                             "MultipleActiveResultSets = True";

                Console.WriteLine(con);

                connection = new SqlConnection(con);
                connection.Open();
                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        /*
         * Abre uma ligação com a base de dados
         */
        public static bool connect(String server, String username, String password, String database)
        {
			try
			{
                string con = "Data Source=" + server + ";" +
                             "Initial Catalog=" + database +
                             "_" + username + ";" +
                             "User ID=" + username + ";" +
                             "Password=" + password + ";" +
                             "MultipleActiveResultSets = True";

				connection = new SqlConnection(con);
				connection.Open();
				return true;
			}
			catch (SqlException ex)
			{
				return false;
			}
        }

        /*
         * Fecha uma ligação com a base de dados
         */
        public static void disconnect()
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
        public static void query(string query)
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
			command.Dispose();
        }

        /*
         * Executa query's de leitura à base de dados
         */
        public static SqlDataReader readData(string query)
        {
            SqlDataReader reader = null;
            SqlCommand command = new SqlCommand(query, connection);
            reader = command.ExecuteReader();
            return reader;
        }

        /*
         * Insere dados na Base de Dados
         */
        /*
        public static void insert(List<String> values, String table)
        {
            /* colocar parâmetros que irão ser submetidos na base de dados * /
            int max = values.Count;
            String objects = values[0];
            for ( int i = 1 ; i < max ; i++ )
                objects = objects + "," + values[i];

            String s_query = "insert into "+ table + " values (" + objects + ")";
            query( s_query );
        }*/

        /*
         * Remove dados na Base de Dados
         */
        /*
        public static void remove(List<String> colunasRestricoes,
            List<String> itensRestricoes, List<String> operadores, 
            String table)
        {
            /* colocar os campos que irão ser alterados * /
            int max1 = colunasRestricoes.Count;
            String objects1 = colunasRestricoes[0] + " = " + itensRestricoes[0];
            for (int i = 1; i < max1; i++)
                objects1 = objects1 + " " + operadores[i-1] + " " + colunasRestricoes[i] + " = " + itensRestricoes[i];

            String s_query = "delete from " + table + "where" + objects1;
            query( s_query );
        }*/

        /*
         * Edita dados na base de dados
         */
        /*
        public static void edit(List<String> colunaItem, List<String> itens,
            List<String> colunasRestricoes, List<String> itensRestricoes,
            String table)
        {
            /* colocar os campos que irão ser alterados * /
            int i, max1 = colunaItem.Count;
            String objects1 = colunaItem[0] + " = " + itens[0];
            for ( i = 1 ; i < max1 ; i++ )
                objects1 = objects1 + "," + colunaItem[i] + " = " + itens[i];

            /* colocar os campos das restrições * / 
            String objects2 = colunasRestricoes[0] + " = " + itensRestricoes[0];
            if (!objects2.Equals("")) 
            {
                int max2 = colunasRestricoes.Count;
                objects2 = " where " + objects2;
                for ( i = 1 ; i < max2 ; i++ )
                    objects2 = objects2 + " AND " + colunasRestricoes[i] + " = " + itensRestricoes[i];
            }

            String s_query = "UPDATE " + table + " SET " + objects1 + objects2;
            query(s_query);
        }*/

        /*
         * Pesquisa na base de dados
         */
        /*
        public static SqlDataReader search(List<String> dadosProcura,
            List<String> dadosRestricoes1, List<String> dadosRestricoes2,
            List<String> modoRestricao, List<String> dadosGroupBy,
            String dadosOrderBy, String order, List<String> tables)
        {
            /* colocar nome das tabelas separadas por ',' * /
            String table = tables[0];
            int max = tables.Count;
            for ( int i = 1 ; i < max ; i++ )
                table = table + "," + tables[i];

            /* colocar nome dos campos de procura separadas por ',' * /
            String objectosProcura = dadosProcura[0];
            max = dadosProcura.Count;
            for ( int i = 1 ; i < max ; i++ )
                objectosProcura = objectosProcura + "," + dadosProcura[i];

            /* colocar nome dos campos das restricoes separadas por 'AND' * /
            String objectosResticoes = (dadosRestricoes1 == null) ? 
                "" : dadosRestricoes1[0];
            if (!objectosResticoes.Equals("")){
                objectosResticoes = " where " + objectosResticoes + 
                    " " + modoRestricao[0] + " " + dadosRestricoes2[0];
                int max2 = dadosRestricoes1.Count;
                for (int i = 1 ; i < max2 ; i++ )
                    objectosResticoes = objectosResticoes + " AND " + 
                        dadosRestricoes1[i] + " " + modoRestricao[i] + " " 
                        + dadosRestricoes2[i];
            }

            /* colocar nome dos campos dos groupby separadas por ',' * /
            String objectosGroupBy = (dadosGroupBy == null) ? "" : dadosGroupBy[0];
            if (!objectosGroupBy.Equals("")){
                objectosGroupBy = " group by " + objectosGroupBy;
                int max3 = dadosGroupBy.Count;
                for (int i = 1 ; i < max3 ; i++ )
                    objectosGroupBy = objectosGroupBy + "," + dadosGroupBy[i];
            }

            /* colocar nome do campo do orderby * /
            String objectos_orderBy = (dadosOrderBy == null) ? "" : dadosOrderBy;
            if (!objectos_orderBy.Equals(""))
                objectos_orderBy = " order by " + objectos_orderBy + " " + order;

            String query = "select " + objectosProcura + " FROM " + table + objectosResticoes + objectosGroupBy + objectos_orderBy;
            return readData ( query );
        }*/
    }
}
