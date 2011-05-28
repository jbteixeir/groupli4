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
         * Liga o analista à sua base de dados
         * @param username Usernmae do analista
         * @param password Password do analista
         */
        public static bool ligaAnalista(String server, 
            String database, String username, String password)
        {
            return Camada_de_Dados.DataBaseCommunicator.
                DataBaseCommunicator.connect(server,username,password,database);
        }

        /**
         * Insere um novo analista na base de dados (e cria as tabelas?)
         * @param username Username do analista
         * @param password Password do analista
         */
        public static bool insertAnalista(String username, String password)
        {
            try
            {
                List<string> querys = ParserCreateAnalista.devolveQuery(username, password);

                foreach (string s in querys)
                    Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.query(s);

                return true;
            }
            catch 
            {
                return false;
            }
        }
        

        /**
         * Remove um analista e as suas tabelas na base de dados
         * @param username Username do analista
         */
        //public static void deleteAnalista(String username);

        /* ----------------------------------------------*/

        /* ----------------------------------------------*/
        /* Projectos */

        /**
         * Retorna os codigos e nomes de todos os Projectos por ordem de data
         * @return Dictionary<string, string> Codigo e Nomes dos estabecimentos dos projectos
         */
        public static Dictionary<string, string> selectNomeProjectos()
        {
            String query = "select cod_projecto,estabelecimento from projecto order by ultimaActualizacao DESC;";
            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            Dictionary<string, string> cod_nome = new Dictionary<string, string>();

            while (r.Read())
            {
                string cod = ""+(long)r["cod_projecto"];
                string nome = (string)r["estabelecimento"];
                cod_nome.Add(cod, nome);
            }

            return cod_nome;
        }

        /**
         * Devolve um Projecto do analista com o codigo recebido
         * @param cod Codigo do projecto que é requerido 
         * @return Projecto Projecto requerido
         */
        public static Projecto selectProjecto(String cod)
        {
            String query = "select * from projecto where cod_projecto = " 
                + cod + ";";

            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.
                DataBaseCommunicator.readData(query);

            Projecto p = null;

            while (r.Read())
            {
                p = new Projecto(""+(long)r["cod_projecto"],
                    (string)r["estabelecimento"], (DateTime)r["ultimaActualizacao"],
                    new Dictionary<string,string>());
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
                "estabelecimento = " + nomeEstabelecimento + ";";
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
            + "' AS datetime);";

            Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.query(query);
        }

        /**
         * Elimina o projecto da base de dados com o código inserido
         * Elimina as análises do projecto
         * @param codProjecto Código do projecto que irá ser eliminado
         */

        public static void deleteProjecto(String codProjecto)
        {
            String query = "delete * from projecto where" + "cod_projecto = " + codProjecto + ";"; 

            Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.query(query);

                
        }

        /**
         * Modifica o projecto na base de dados com o código do projecto recebido
         * @param p Projecto que irá ser editado
         */
        //public static void updateProjecto(Projecto p);

        /* ----------------------------------------------*/
        /* Analises */

        /**
         * Retorna os codigos e nomes das analises
         * @param codProjecto O Codigo do projecto à qual as analises fazem parte
         * @return Dictionary<string, string> Os codigos e os nomes das analises
         */
        public static Dictionary<string, string> selectNomesAnalises(String codProjecto)
        {
            String query = "select cod_analise,nomeAnalise from analise where cod_projecto = " + codProjecto + ";";
            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            Dictionary<string, string> cod_nome = new Dictionary<string, string>();

            while (r.Read())
            {
                String cod = ""+(long)r["cod_analise"];
                String nome = (string)r["nomeAnalise"];
                cod_nome.Add(cod,nome);
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
                + codAnalise + ";";

            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.
                DataBaseCommunicator.readData(query);

            Analise a = null;

            while (r.Read())
            {
                a = new Analise((string)r["cod_analise"],
                    (string)r["cod_projecto"], 
                    (DateTime)r["dataCriacao"],
                    (string)r["nomeAnalise"],
                    (string)r["tipoAnalise"],
                    (List<Zona>)r["zonas"],
                    (List<Item>)r["items"],
                    (int)r["estadoWebCheckList"],
                    (int)r["estadoWebFichaAvaliacao"],
                    (int)r["estadoWebQuestionario"],
                    new List<Tuple<String,String,DateTime,String,String,List<Zona>, List<Item>,int,int,int>>());
                
            }

            return a;
        }

      // public static Analise selectZonasAnalise(String codAnalise);

     //  public static Analise selectItemsAnalise(String codAnalise):


        public static String selectCodigoAnalise(DateTime data)
        {
            String query = "select cod_analise from analise where" +
                "dataCriacao = " + data + ";";
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
            + "," + a.EstadoWebQ + ";";

            Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.query(query);
        }

        public static void insertZonasAnalise(List<Zona> zonas, String codAnalise)
        {
            foreach (Zona z in zonas)
            {
                String query = "insert into zona_analise values(" + z.Codigo + "," + codAnalise + ";";

                Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.query(query);
            }

        }

        public static void insertItemsAnalise(List<Item> items, String codAnalise)
        {
            foreach (Item i in items)
            {
                String query = "insert into item_analise values(" + i.Codigo + "," + codAnalise + ";";

                Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.query(query);
            }

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
            String query = "delete * from analise where" + "cod_analise = " + codAnalise + ";";

            Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.query(query);

        }

        public static void deleteZonasAnalise(List<Zona> zonas, String codAnalise)
        {
            foreach (Zona z in zonas)
            {
                String query = "delete * from zona_analise where " + "cod_analise = " + codAnalise + ";";
                Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.query(query);
            }
        }

        public static void deleteItemsAnalise (List<Item> items, String codAnalise)
        {
            foreach (Item i in items)
            {
                String query = "delete * from item_analise where " + "cod_analise = " + codAnalise + ";";
                Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.query(query);
            }
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

        public static List<Zona> selectZonas()
        {
            String query = "select * from zona;";
            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            List<Zona> zonas = new List<Zona>();

            while (r.Read())
            {
               Zona zona = new Zona ((String)r["cod_zona"], (String)r["nome_zona"]);
               zonas.Add(zona);

            }
            return zonas;
        }

        public static void insertZona(Zona z)
        {
            String query = "insert into zona values(" + z.Codigo + "," + z.Nome + ";";

            Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.query(query);
        }

        public static void deleteZona(String codZona)
        {
            String query = "delete * from zona where" + "cod_zona = " + codZona + ";";

            Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.query(query);
        }

        //public static void updateZona(Zona z);

        /* ----------------------------------------------*/

        /* ----------------------------------------------*/
        /* Zonas - Analise */

        public static List<Zona> selectZonasAnalise(String codAnalise)
        {
            String query = "select cod zona, nome_zona from zona_analise, zona where " + "cod_analise = " + codAnalise + "and zona_analise.cod_zona = zona.cod_zona" + ";";
            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            List<Zona> zonas = new List<Zona>();

            while (r.Read())
            {
                Zona zona = new Zona((String)r["cod_zona"], (String)r["nome_zona"]);
                zonas.Add(zona);

            }
            return zonas;
        }



        /* ----------------------------------------------*/

        /* ----------------------------------------------*/
        /* Itens */

        public static List<Item> selectItensDefault()
        {
            String query = "select * from item where item_default = 1;";
            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            List<Item> items = new List<Item>();

            while (r.Read())
            {
                Item item = new Item((String)r["cod_item"], (String)r["nome_item"], (int)r["default_item"]);
                items.Add(item);

            }
            return items;
        }

        public static List<Item> selectItens()
        {
            String query = "select * from item;";
            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            List<Item> items = new List<Item>();

            while (r.Read())
            {
                Item item = new Item((String)r["cod_item"], (String)r["nome_item"], (int)r["default_item"]);
                items.Add(item);

            }
            return items;
        }

        public static void insertItem(Item i)
        {
            String query = "insert into item values(" + i.Codigo + "," + i.Nome + "," + i.Default + ";";

            Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.query(query);
        }

        public static void deleteItem(String codItem)
        {
            String query = "delete * from item where" + "cod_item = " + codItem + ";";

            Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.query(query);
        }


        //public static void updateItem(Item i);

        /* ----------------------------------------------*/

        /* ----------------------------------------------*/
        /* Itens - Analise */

        public static List<Item> selectItensAnalise(String codAnalise)
        {
            String query = "select cod_item, nome_item from item_analise, item where " + "cod_analise = " + codAnalise + "and item_analise.cod_item = item.cod_item" + ";";
            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            List<Item> items = new List<Item>();

            while (r.Read())
            {
                Item item = new Item((String)r["cod_item"], (String)r["nome_item"], (int)r["default_item"]);
                items.Add(item);

            }
            return items;
        }


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


