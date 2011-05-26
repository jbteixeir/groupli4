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
        //public static Boolean selectAnalista(String username, String password);

        /**
         * Liga o analista à sua base de dados
         * @param username Usernmae do analista
         * @param password Password do analista
         */
        //public static void ligaAnalista(String username, String password);

        /**
         * Insere um novo analista na base de dados (e cria as tabelas?)
         * @param username Username do analista
         * @param password Password do analista
         */
        //public static void insertAnalista(String username, String password);
        

        /**
         * Remove um analista e as suas tabelas na base de dados
         * @param username Username do analista
         */
        //public static void deleteAnalista(String username);

        /**
         * Altera a password de um analista
         * @param username Username do analista
         * @param password Nova password do analista
         */
        //public static void updateAnalista(String username, String password);

        /* ----------------------------------------------*/

        /* ----------------------------------------------*/
        /* Projectos */

        /**
         * Retorna os codigos e nomes de todos os Projectos por ordem de data
         * @return List<Tuplo<String,String>> Codigo e Nomes dos estabecimentos dos projectos
         */
        public static List<Tuplo<String,String>> selectNomeProjectos()
        {
            String query = "select cod_projecto,estabelecimento from projecto orderby data DESC";
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
         * Devolve um Projecto do analista com o nome recebido
         * @param nomeEstabelecimeto Nome do estabeldecimento do projecto que é requerido 
         * @return Projecto Projecto requerido
         */
        public static Projecto selectProjecto(String cod)
        {
            String query = "select * from projecto where cod_projecto = " 
                + cod;

            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.
                DataBaseCommunicator.readData(query);

            Projecto p = null;

            while (r.Read())
            {
                p = new Projecto((string)r["cod_projecto"],
                    (string)r["estabelecimento"], (DateTime)r["ultimaActualizacao"],
                    new List<Tuplo<String,String>>());
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
            String query = "select cod_projecto from projecto where" +
                "estabelecimento = " + nomeEstabelecimento;
            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.
                DataBaseCommunicator.readData(query);

            return (string)r["cod_projecto"];
        }

        /**
         * Insere um novo projecto na base de dados
         * @param p Novo projecto que irá ser inserido (não contém código)
         */
        public static void insertProjecto(Projecto p)
        {
            String query = "insert into projecto values(" +
            p.Nome + "," + "CAST('" + p.Data.ToString("yyyymmdd hh:mm:ss") 
            + "' AS datetime)";

            Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.query(query);
        }

        /**
         * Elimina o projecto da base de dados com o código inserido
         * Elimina as análises do projecto
         * @param codProjecto Código do projecto que irá ser eliminado
         */

        public static void deleteProjecto(String codProjecto)
        {
            String query = "delete * from projecto where" + "cod_projecto = " + codProjecto; 

            Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.query(query);
                
        }

        /**
         * Modifica o projecto na base de dados com o código do projecto recebido
         * @param p Projecto que irá ser editado
         */
        //public static void updateProjecto(Projecto p);

        /* ----------------------------------------------*/
        /* Analises */

        public static List<Tuplo<String, String>> selectNomesAnalises(String codProjecto)
        {
            String query = "select cod_projecto,cod_analise,nomeAnalise from analise where" + "cod_projecto = " + codProjecto + "orderby data DESC";
            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            List<Tuplo<String,String>> cod_nome = new List<Tuplo<String,String>>();

            while (r.Read())
            {
                String cod = (string)r["cod_analise"];
                String nome = (string)r["nomeAnalise"];
                Tuplo<String, String> t = new Tuplo<String,String>(cod, nome);
                cod_nome.Add(t);
            }

            return cod_nome;
        }

        /**
         * Retorna a analise com o codigo recebido
         * @param codAnalise O Codigo da analise
         * @return Analise A analise requerida
         */
       public static Analise selectAnalise(String codAnalise)
        {
            String query = "select * from analise where cod_analise = " 
                + codAnalise;

            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.
                DataBaseCommunicator.readData(query);

            Analise a = null;

            while (r.Read())
            {
               /* a = new Analise((string)r["cod_analise"],
                    (string)r["cod_projecto"], 
                    (DateTime)r["dataCriacao"],
                    (string)r["nomeAnalise"],
                    (string)r["tipoAnalise"],
                    (int)r["estadoWebCheckList"],
                    (int)r["estadoWebFichaAvaliacao"],
                    (int)r["estadoWebQuestionario"],
                    new List<Tuple<String,String, DateTime,String,String,int,int,int>>());
                */
            }

            return a;
        }

        public static Analise selectZonasAnalise(String codAnalise)
        {
            String query = "select cod_zona,cod_analise,nome_zona from zona_analise, zona where"
                + "cod_analise = " + codAnalise + "and zona_analise.cod_zona = zona.cod_zona group by nome_zona";

            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.
                DataBaseCommunicator.readData(query);

            Analise a = null;

            while (r.Read())
            {
                
            }

            return a;

        }

        public static Analise selectItemsAnalise(String codAnalise)
        {
            String query = "select cod_item,cod_analise,nome_item from item_analise, item where"
                + "cod_analise = " + codAnalise + "and item_analise.cod_item = item.cod_item group by nome_item";

            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.
                DataBaseCommunicator.readData(query);

            Analise a = null;

            while (r.Read())
            {

            }

            return a;

        }


        public static String selectCodigoAnalise(DateTime data)
        {
            String query = "select cod_analise from analise where" +
                "dataCriacao = " + data;
            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.
                DataBaseCommunicator.readData(query);

            return (string)r["cod_analise"];
        }

        /**
         * Insere uma nova analise na base de dados
         * Insere os itens da analise na tabela dos item_analise
         * Insere as zonas da analise na tabela dos zona_analise
         * @param codProjecto O Codigo do projecto em que faz parte a analise
         * @param a Analise que será inserida
         */
        public static void insertAnalise(String codProjecto, Analise a)
        {
            String query = "insert into analise values(" + codProjecto + "," 
            + "CAST('" + a.Data.ToString("yyyymmdd hh:mm:ss") + "' AS datetime)" 
            + "," + a.Nome + "," + a.Tipo + "," + a.EstadoWebCL + "," + a.EstadoWebFA
            + "," + a.EstadoWebQ;

            Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.query(query);
        }

        public static void insertZonaAnalise(List<Zona> codZona, List<Analise> codAnalise)
        {

        }

        public static void insertItemsAnalise(List<Item> codItem, List<Analise> codAnalise)
        {
            
        }


        /**
         * Elimina uma analise da base de dados
         * Elimina os itens da analise na tabela dos item_analise
         * Elimina as zonas da analise na tabela dos zona_analise
         * Elimina as Perguntas e Respostas dos Formularios desta Analise (se existirem)
         * @param codAnalise O codigo da analise que sera eliminada
         */
        public static void deleteAnalise(String codAnalise)
        {
            String query = "delete * from analise where" + "cod_analise = " + codAnalise;

            Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.query(query);

        }


        /**
         * Modifica a informacao da analise passada como argumento
         * @param codProjecto codProjecto ao qual a analise faz parte
         * @param a A analise que ira ser modificada
         */
        //public static void updateAnalise(String codProjecto, Analise a);

        /* ----------------------------------------------*/

        /* ----------------------------------------------*/
        /* Zonas */

        //public static List<Zona> selectZonas();

        //public static void insertZona(Zona z);

        //public static void deleteZona(String codZona);

        //public static void updateZona(Zona z);

        /* ----------------------------------------------*/

        /* ----------------------------------------------*/
        /* Zonas - Analise */

        //public static List<Zona> selectZonasAnalise(String codAnalise);

        //public static void insertZonaAnalise(String codZona, String codAnalise);

        //public static void deleteZonaAnalise(String codZona, String codAnalise);

        /* ----------------------------------------------*/

        /* ----------------------------------------------*/
        /* Itens */

        //public static List<Item> selectItensDefault();

        //public static List<Item> selectItens();

        //public static void insertItem(Item i);

        //public static void deleteItem(String codItem);

        //public static void updateItem(Item i);

        /* ----------------------------------------------*/

        /* ----------------------------------------------*/
        /* Itens - Analise */

        //public static List<Item> selectItensAnalise(String codAnalise);

        //public static void insertItemAnalise(String codItem, String codAnalise);

        //public static void deleteItemAnalise(String codItem, String codAnalise);

        /* ----------------------------------------------*/


        /* ----------------------------------------------*/
        /* Respostas */

        /* -----------------------------*/
        /* Respostas CheckList */

        /* -----------------------------*/

        /* -----------------------------*/
        /* Respostas Formulario numero */

        /* ---------------------------- */

        /* -----------------------------*/
        /* Respostas Formulario string */

        /* -----------------------------*/

        /* -----------------------------*/
        /* Respostas Questinario numero */

        /* ------------------------------*/

        /* ------------------------------*/
        /* Respostas Questionario String */

        /* ------------------------------*/

        /* ------------------------------*/
        /* Respostas Questionario Memo */

        /* ------------------------------*/

        /* ----------------------------------------------*/
}

}


