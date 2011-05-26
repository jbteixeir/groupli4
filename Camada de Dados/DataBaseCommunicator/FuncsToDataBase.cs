using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ETdA.Camada_de_Dados.Classes;
using ETdA.Camada_de_Dados.Classes.Estruturas;

namespace ETdA.Camada_de_Dados.DataBaseCommunicator
{
    class FuncsToDataBase
    {
        /* ----------------------------------------------*/
        /* Analistas */

        /**
         * Verifica se um existe um analista na base de dados
         * @param username Username do analista
         * @param password Password do analista
         */
        public static Boolean selectAnalista(String username, String password);

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
         * Retorna os nomes de todos os Projectos por ordem de data
         * @return List<String> Nome dos estabecimentos dos projectos
         */
        public static List<Tuplo<String,String>> selectNomeProjectos()
        {
            String query = "select cod_projecto,estabelecimento from projectos orderby data DESC";
            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            List<Tuplo<String,String>> cod_nome = new List<Tuplo<String,String>>();

            while (r.Read())
            {
                String cod = (string)r["cod_projecto"];
                String nome = (string)r["estabelecimento"];
                Tuplo<String, String> t = new Tuplo<String,String>(cod, nome);
                cod_nome.Add(t);
            }

            return cod_nome;
        }

        /**
         * Devolve um Projecto do analisa com o nome recebido
         * @param nomeEstabelecimeto Nome do estabeldecimento do projecto que é requerido 
         * @return Projecto Projecto requerido
         */
        public static Projecto selectProjecto(String nomeEstabelecimeto)
        {
            String query = "select * form projectos where estabelecimento = " 
                + nomeEstabelecimeto;
            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.
                DataBaseCommunicator.readData(query);

            Projecto p = null;

            while (r.Read())
            {
                p = new Projecto((string)r["codProjecto"],
                    (string)r["estabelecimento"], (DateTime)r["data"],
                    new List<String>());
            }

            return p;
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
        public static void insertProjecto(Projecto p)
        {
            String query = "insert into projectos values(" +
            p.Nome + "," + "CAST('" + p.Data.ToString("yyyymmdd hh:mm:ss") 
            + "' AS datetime)";

            Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.query(query);
        }

        /**
         * Elimina o projecto da base de dados com o código inserido
         * Elimina as análises do projecto
         * @param codProjecto Código do projecto que irá ser eliminado
         */
        public static void deleteProjecto(String codProjecto);

        /**
         * Modifica o projecto na base de dados com o código do projecto recebido
         * @param p Projecto que irá ser editado
         */
        public static void updateProjecto(Projecto p);

        /* ----------------------------------------------*/

        /* Analises */

        /**
         * Retorna os nomes de todas as análises por ordem de criacao
         * @param codProjecto O Codigo do projecto em que faz parte a analise
         * @return List<String> nomes das analises
         */
        public static List<String> selectNomesAnalises(String codProjecto);

        /**
         * Retorna a analise com o codigo recebido
         * @param codAnalise O Codigo da analise
         * @return Analise A analise requerida
         */
        public static Analise selectAnalise(String codAnalise);

        /**
         * Retorna os nomes de todas as análises por ordem de criacao
         * @param codProjecto O Codigo do projecto em que faz parte a analise
         * @return List<String> nomes das analises
         */
        public static String selectCodigoAnalise(DateTime data);

        /**
         * Insere uma nova analise na base de dados
         * Insere os itens da analise na tabela dos item_analise
         * Insere as zonas da analise na tabela dos zona_analise
         * @param codProjecto O Codigo do projecto em que faz parte a analise
         * @param a Analise que será inserida
         */
        public static void insertAnalise(String codProjecto, Analise a);

        /**
         * Elimina uma analise da base de dados
         * Elimina os itens da analise na tabela dos item_analise
         * Elimina as zonas da analise na tabela dos zona_analise
         * Elimina as Perguntas e Respostas dos Formularios desta Analise (se existirem)
         * @param codAnalise O codigo da analise que sera eliminada
         */
        public static void deleteAnalise(String codAnalise);

        public static void updateAnalise(Analise a);

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
