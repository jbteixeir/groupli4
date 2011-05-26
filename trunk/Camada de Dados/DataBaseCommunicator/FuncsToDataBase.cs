using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ETdA.Camada_de_Dados.DataBaseCommunicator
{
    class FuncsToDataBase
    {
        /* Tabelas */
        /*
        public static String PROJECTOS = "Projectos";
        public static String ANALISE = "Analise";
        public static String FORMULARIOS = "Formulario";
        public static String PERGUNTAS = "Perguntas";
        public static String RESPOSTAS = "Respostas";
        public static String ITENS = "Itens";
        public static String ESCALA_RESPOSTA = "EscalaResposta";
        */

        /* ----------------------------------------------*/
        /* Analistas */

        /**
         * Verifica se um existe um analista na base de dados
         * @param username Username do analista
         * @param password Password do analista
         */
        public static void selectAnalista(String username, String password);

        /**
         * Liga o analista à sua base de dados
         * @param username Usernmae do analista
         * @param password Password do analista
         */
        public static void ligaAnalista(String username, String password);

        /**
         * Insere um novo analista na base de dados (e cria as tabelas?)
         * @param username Username do analista
         * @param password Password do analista
         */
        public static void insertAnalista(String username, String password);

        /**
         * Remove um analista e as suas tabelas na base de dados
         * @param username Username do analista
         */
        public static void deleteAnalista(String username);

        /**
         * Altera a password de um analista
         * @param username Username do analista
         * @param password Nova password do analista
         */
        public static void updateAnalista(String username, String password);

        /* ----------------------------------------------*/

        /* ----------------------------------------------*/
        /* Projectos */

        /**
         * Devolve todos os projectos do analista
         * @return List<Projecto> Projectos do analista 
         */
        /*
        public static List<Projecto> selectProjectos()
        {
            List<Projecto> projectos = new List<Projecto>();

            List<String> dadosProcura = new List<String>();
            dadosProcura.Add("*");
            List<String> tabelas = new List<String>();
            tabelas.Add(PROJECTOS);
            String orderby = "data";
            String order = "desc";

            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.search(
                dadosProcura, null, null, null, null, orderby, order, tabelas);

            while (r.Read())
            {
                Projecto p = new Projecto((string)r["codProjecto"],
                    (string)r["estabelecimento"], (DateTime)r["data"],
                    new List<Analise>());

                projectos.Add(p);
            }

            return projectos;
        }*/
        public static List<Projecto> selectProjectos()
        {
            String query = "select * form projectos orderby data desc";
            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.
                DataBaseCommunicator.readData(query);

            List<Projecto> projectos = new List<Projecto>();

            while (r.Read())
            {
                Projecto p = new Projecto((string)r["codProjecto"],
                    (string)r["estabelecimento"], (DateTime)r["data"],
                    new List<Analise>());

                projectos.Add(p);
            }

            return projectos;
        }

        /**
         * Devolve o código do projecto com o nome do estabelecimento recebido
         * @param nomeEstabelecimento Nome do estabelecimento
         * @return String Codigo do projecto
         */
        public static String selectCodigoProjecto(String nomeEstabelecimento)
        {
            String query = "select codProjecto from Projectos where" +
                "estabelecimento = " + nomeEstabelecimento;
            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.
                DataBaseCommunicator.readData(query);

            return (string)r["codProjecto"];
        }

        /**
         * Insere um novo projecto na base de dados
         * @param p Novo projecto que irá ser inserido (não contém código)
         */
        /*
        public static void insertProjecto(Projecto p)
        {
            List<String> values = new List<string>();
            values.Add(p.Nome);
            values.Add("CAST('" + p.Data.ToString("yyyymmdd hh:mm:ss") + "' AS datetime");
            String tabela = PROJECTOS;

            Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.insert(
                values, tabela);
        }
        */
        public static void insertProjecto(Projecto p)
        {
            String query = "insert into projectos values(" +
            p.Nome + "," + "CAST('" + p.Data.ToString("yyyymmdd hh:mm:ss") 
            + "' AS datetime)";

            Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.query(query);
        }

        /**
         * Eliminao projecto da base de dados com o código inserido
         * @param codProjecto Código do projecto que irá ser eliminado
         */
        public static void deleteProjecto(String codProjecto);

        /**
         * Modifica o projecto na base de dados com o código do projecto recebido
         * @param p Projecto que irá ser editado
         */
        public static void updateProjecto(Projecto p);

        /**
         * Retorna os nomes de todos os Projectos
         * @return List<String> Nome dos estabecimentos dos projectos
         */
        /*
        public static List<String> selectNomeProjectos()
        {
            List<String> nomes = new List<String>();

            List<String> dadosProcura = new List<String>();
            dadosProcura.Add("estabelecimento");
            List<String> tabelas = new List<String>();
            tabelas.Add(PROJECTOS);

            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.search(
                dadosProcura, null, null, null, null, null, null, tabelas);

            while (r.Read())
            {
                String s = (string)r["estabelecimento"];
                nomes.Add(s);
            }

            return nomes;
        }*/
        public static List<String> selectNomeProjectos()
        {
            String query = "select estabelecimento from projectos";
            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

             List<String> nomes = new List<String>();

            while (r.Read())
            {
                String s = (string)r["estabelecimento"];
                nomes.Add(s);
            }

            return nomes;
        }

        /* ----------------------------------------------*/

        /* Analises */

        public static List<Analise> selectAnalises();

        public static void insertAnalise();

        public static void deleteAnalise();

        public static void updateAnalise();

        /* Itens */

        public static List<Item> selectItens();

        public static void insertItem();

        public static void deleteItem();

        public static void updateItem();

        /* Questoes */

        public static List<Item> selectQuestoes();

        public static void insertQuestao();

        public static void deleteQuestao();

        public static void updateQuestao();

        /* Escala Resposta */

        public static List<Item> selectEscalaRespostas();

        public static void insertEscalaResposta();

        public static void deleteEscalaResposta();

        public static void updateEscalaResposta();

        /* Respostas */

        public static List<Item> selectRespostas();

        public static void insertResposta();

        public static void deleteResposta();

        public static void updateResposta();

    }
}
