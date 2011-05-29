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
        #region Analistas
        /**
         * Liga o analista à sua base de dados
         * @param username Usernmae do analista
         * @param password Password do analista
         */
        public static bool ligaAnalista(String server,
            String database, String username, String password)
        {
            return Camada_de_Dados.DataBaseCommunicator.
                DataBaseCommunicator.connect(server, username, password, database);
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
        #endregion
        /* ----------------------------------------------*/

        /* ----------------------------------------------*/
        /* Projectos */
        #region Projectos
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
                string cod = "" + (long)r["cod_projecto"];
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
                p = new Projecto("" + (long)r["cod_projecto"],
                    (string)r["estabelecimento"], (DateTime)r["ultimaActualizacao"],
                    new Dictionary<string, string>());
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
                " estabelecimento = '" + nomeEstabelecimento + "';";
            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.
                DataBaseCommunicator.readData(query);

            r.Read();

            return "" + (long)r["cod_projecto"];
        }

        /**
         * Insere um novo projecto na base de dados
         * @param p Novo projecto que irá ser inserido (não contém código)
         */
        public static void insertProjecto(Projecto p)
        {
            String query = "insert into projecto values('" +
            p.Nome + "'," + "CAST('" + p.Data.ToString("yyyyMMdd HH:mm:ss")
            + "' AS datetime));";

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
        public static void updateProjecto(Projecto p)
        {
            String query = "update projecto set " + "estabelecimento = " + p.Nome + ","
                + "ultimaActualizacao = " + p.Data + "where cod_projecto = " + p.Codigo + ";";

            Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.query(query);
        }
        #endregion
        /* ----------------------------------------------*/

        /* ----------------------------------------------*/
        /* Analises */
        #region Analises
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
                String cod = "" + (long)r["cod_analise"];
                String nome = (string)r["nomeAnalise"];
                cod_nome.Add(cod, nome);
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
                   (string)r["codProjecto"],
                   (DateTime)r["dataCriacao"],
                   (string)r["nomeAnalise"],
                   (string)r["tipoAnalise"],
                   new List<Zona>(),
                   new List<Item>(),
                   (int)r["estadoWebCheckList"] == 0 ? false : true,
                   (int)r["estadoWebFichaAvaliacao"] == 0 ? false : true,
                   (int)r["estadoWebQuestionario"] == 0 ? false : true);
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
                String query = "insert into item_analise values(" + i.CodigoItem + "," + i.NomeItem + "," + i.Default
                + "," + i.PonderacaoAnalista + "," + i.PonderacaoProfissional + "," + i.PonderacaoCliente + ";";

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

        public static void deleteItemsAnalise(List<Item> items, String codAnalise)
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
        public static void updateAnalise(Analise a)
        {
            String query = "update analise set " + "dataAnalise = " + a.Data + "," + "nomeAnalise = "
                + a.Nome + "," + "tipoAnalise = " + a.Tipo + "," + "estadoWebCheckList = "
                + a.EstadoWebCL + "," + "estadoWebFichaAvaliacao = " + a.EstadoWebFA + "," + "estadoWebQuestionario = "
                + a.EstadoWebQ + "where cod_analise = " + a.Codigo + ";";

            Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.query(query);
        }
        #endregion
        /* ----------------------------------------------*/

        /* ----------------------------------------------*/
        /* Zonas */
        #region Zonas

        public static List<Zona> selectZonas()
        {
            String query = "select * from zona;";
            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            List<Zona> zonas = new List<Zona>();

            while (r.Read())
            {
                Zona zona = new Zona((int)r["cod_zona"], (String)r["nome_zona"]);
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

        public static void updateZona(Zona z)
        {
            String query = "update zona set " + "nome_zona = " + z.Nome + ","
                + "where cod_zona = " + z.Codigo + ";";

            Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.query(query);
        }


        #endregion
        /* ----------------------------------------------*/

        /* ----------------------------------------------*/
        /* Zonas - Analise */
        #region Zonas-Analise
        public static List<Zona> selectZonasAnalise(String codAnalise)
        {
            String query = "select cod zona, nome_zona from zona_analise, zona where " + "cod_analise = " + codAnalise + "and zona_analise.cod_zona = zona.cod_zona" + ";";
            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            List<Zona> zonas = new List<Zona>();

            while (r.Read())
            {
                Zona zona = new Zona((int)r["cod_zona"], (String)r["nome_zona"]);
                zonas.Add(zona);

            }
            return zonas;
        }
        #endregion
        /* ----------------------------------------------*/

        /* ----------------------------------------------*/
        /* Itens */
        #region Itens
        public static Dictionary<string,string> selectItensDefault()
        {
            String query = "select cod_item, nome_item from item where default_item = 1;";
            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            Dictionary<string, string> itens_default = new Dictionary<string, string>();

            while (r.Read())
                itens_default.Add("" + (long)r["cod_item"], (string)r["nome_item"]);

            return itens_default;
        }

        public static Dictionary<string, string> selectAllItens()
        {
            String query = "select cod_item, nome_item from item order by default_item DESC;";
            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            Dictionary<string, string> itens_default = new Dictionary<string, string>();

            while (r.Read())
                itens_default.Add("" + (long)r["cod_item"], (string)r["nome_item"]);

            return itens_default;
        }

        public static List<Item> selectItens()
        {
            String query = "select * from item;";
            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            List<Item> items = new List<Item>();

            while (r.Read())
            {
                Item item = new Item((int)r["cod_item"],
                    (String)r["nome_item"],
                    (int)r["default_item"],
                    0.33f,
                    0.33f,
                    0.33f,
                    1,
                    2,
                    3,
                    4,
                    5,
                    1);
                items.Add(item);

            }
            return items;
        }

        public static void insertItem(Item i)
        {
            String query = "insert into item values(" + i.CodigoItem + "," + i.NomeItem + "," + i.Default 
                + "," + i.PonderacaoAnalista + "," + i.PonderacaoProfissional + "," + i.PonderacaoCliente + ";";

            Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.query(query);
        }

        public static void deleteItem(String codItem)
        {
            String query = "delete * from item where" + "cod_item = " + codItem + ";";

            Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.query(query);
        }

        public static void updateItem(Item i)
        {
            String query = "update item, item_analise set "
                + "nome_item = " + i.NomeItem + ","
                + "default_item" + i.Default + ","
                + "ponderacao_analista = " + i.PonderacaoAnalista + ","
                + "ponderacao_profissional = " + i.PonderacaoProfissional + ","
                + "ponderacao_cliente = " + i.PonderacaoCliente
                + "where cod_item = " + i.CodigoItem + ";";

            Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.query(query);
        }
        #endregion
        /* ----------------------------------------------*/

        /* ----------------------------------------------*/
        /* Itens - Analise */
        #region Itens-Analise
        public static List<Item> selectItensAnalise(String codAnalise)
        {
            String query = "select cod_item, nome_item from item_analise, item where " + "cod_analise = " + codAnalise + "and item_analise.cod_item = item.cod_item" + ";";
            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            List<Item> items = new List<Item>();

            while (r.Read())
            {
                Item item = new Item((int)r["cod_item"],
                    (String)r["nome_item"],
                    (int)r["default_item"],
                    (float)r["ponderacao_analista"],
                    (float)r["ponderacao_profissional"],
                    (float)r["ponderacao_cliente"],
                    (float)r["inter_vermelho"],
                    (float)r["inter_laranja"],
                    (float)r["inter_amarelo"],
                    (float)r["inter_verdelima"],
                    (float)r["inter_verde"],
                    (float)r[""]);
                items.Add(item);

            }
            return items;
        }
        #endregion
        /* ----------------------------------------------*/


        /* ----------------------------------------------*/
        /* Respostas */
        static public void selectRespostaQuestionario(int codigoAnalise, List<Resposta> respostas)
        {
            SqlDataReader reader, readerResposta;

            reader = DataBaseCommunicator.readData("SELECT cod_pergunta_questionario, numero_pergunta, TipoEscala.numeroEscalaResposta, cod_zona " +
                                                "FROM pergunta_questionario, TipoEscala " +
                                                "WHERE pergunta_questionario.cod_tipoEscala=TipoEscala.cod_tipoEscala " +
                                                "AND pergunta_questionario.cod_analise=" + codigoAnalise);

            while (reader.Read())
            {
                //String
                if (int.Parse(reader["numeroEscalaResposta"].ToString()) == 0)
                {
                    readerResposta = DataBaseCommunicator.readData("SELECT cod_questionario, valor, resposta_questionario_numero.cod_zona, resposta_questionario_string.cod_zona,resposta_questionario_string.numero_pergunta " +
                                                                       "FROM resposta_questionario_string, pergunta_questionario " +
                                                                       "WHERE resposta_questionario_string.numero_pergunta=" + reader["numero_pergunta"].ToString() +
                                                                       "AND resposta_questionario_string.numero_pergunta=pergunta_questionario.numero_pergunta" +
                                                                       "AND respostas_questionario.cod_analise=" + codigoAnalise);
                    while (readerResposta.Read())
                    {
                        respostas.Add(new Resposta(int.Parse(readerResposta["cod_questionario"].ToString()), -1, int.Parse(reader["numero_pergunta"].ToString()), int.Parse(reader["cod_item"].ToString()), int.Parse(readerResposta["cod_zona"].ToString()), -1, readerResposta["valor"].ToString(), 3));
                    }

                }
                //memo
                else if (int.Parse(reader["numeroEscalaResposta"].ToString()) == -1)
                {
                    readerResposta = DataBaseCommunicator.readData("SELECT resposta_questionario_memo.cod_questionario, resposta_questionario_memo.valor, resposta_questionario_memo.cod_zona,resposta_questionario_memo.numero_pergunta " +
                                                                       "FROM resposta_questionario_memo, pergunta_questionario " +
                                                                       "WHERE resposta_questionario_memo.numero_pergunta=" + reader["numero_pergunta"].ToString() +
                                                                       "AND resposta_questionario_memo.numero_pergunta=pergunta_questionario.numero_pergunta" +
                                                                       "AND respostas_questionario.cod_analise=" + codigoAnalise);
                    while (readerResposta.Read())
                    {
                        respostas.Add(new Resposta(int.Parse(readerResposta["cod_questionario"].ToString()), -1, int.Parse(reader["numero_pergunta"].ToString()), int.Parse(reader["cod_item"].ToString()), int.Parse(readerResposta["cod_zona"].ToString()), -1 ,readerResposta["valor"].ToString(), 3));
                    }
                }
                //numero
                else
                {
                    readerResposta = DataBaseCommunicator.readData("SELECT resposta_questionario_numero.cod_questionario, resposta_questionario_numero.valor, resposta_questionario_numero.cod_zona, resposta_questionario_numero.numero_pergunta " +
                                                                       "FROM resposta_questionario_numero, pergunta_questionario " +
                                                                       "WHERE resposta_questionario_numero.numero_pergunta=" + reader["numero_pergunta"].ToString() +
                                                                       "AND resposta_questionario_numero.numero_pergunta=pergunta_questionario.numero_pergunta" +
                                                                       "AND respostas_questionario.cod_analise=" + codigoAnalise);
                    while (readerResposta.Read())
                    {
                        respostas.Add(new Resposta(int.Parse(readerResposta["cod_questionario"].ToString()), -1, int.Parse(reader["numero_pergunta"].ToString()), int.Parse(reader["cod_item"].ToString()), int.Parse(readerResposta["cod_zona"].ToString()), int.Parse(readerResposta["valor"].ToString()), "", 3));
                    }
                }
                readerResposta.Close();
            }
            reader.Close();
        }
        /* -----------------------------*/
        /* Respostas CheckList */
        #region Respostas CheckList
        static public void selectRespostaCheckList(int codigoAnalise, List<Resposta> respostas)
        {
            SqlDataReader readerZona, readerItem, readerResposta;

            readerZona = DataBaseCommunicator.readData("SELECT Zona_analise.cod_zona FROM Zona_Analise WHERE Zona_Analise.cod_analise=" + codigoAnalise);
                

            while (readerZona.Read())
            {
                readerItem = DataBaseCommunicator.readData("SELECT Item.cod_item " +
                                                               "FROM Item, Item_Analise " +
                                                               "WHERE Item.cod_item = Item_Analise.cod_item " +
                                                                    "AND Item_Analise.cod_analise=" + codigoAnalise);
                while (readerItem.Read())
                {
                    readerResposta = DataBaseCommunicator.readData("SELECT cod_analise, cod_zona, cod_item, valor " +
                                                                        "FROM resposta_checkList " +
                                                                        "WHERE cod_analise=" + codigoAnalise +
                                                                            " AND cod_zona= " + readerZona["cod_zona"] +
                                                                            " AND cod_item= " + readerItem["cod_item"]);
                    readerResposta.Read();
                    respostas.Add(new Resposta(-1, -1, -1, int.Parse(readerItem["cod_item"].ToString()), int.Parse(readerZona["cod_zona"].ToString()), int.Parse(readerResposta["valor"].ToString()),"",1));
                    readerResposta.Close();
                }
                readerItem.Close();
            }
            readerZona.Close();
        }

        static public void selectRespostaFichaAvaliacao(int codigoAnalise, List<Resposta> respostas)
        {
             SqlDataReader readerPergunta, readerResposta;

             readerPergunta = DataBaseCommunicator.readData("SELECT ficha_avaliacao.cod_fichaAvaliacao " +
                                                 "FROM  ficha_avaliacao " +
                                                 "WHERE ficha_avaliacao.cod_analise=" + codigoAnalise);
             while (readerPergunta.Read())
             {
                 readerResposta = DataBaseCommunicator.readData("SELECT numero_pergunta, item.cod_item, cod_fichaAvaliacao, cod_zona, valor " +
                                                                    "FROM  resposta_ficha_avaliacao_numero " +
                                                                    "WHERE resposta_ficha_avaliacao_numero.cod_fichaAvaliacao="+ readerPergunta["cod_fichaAvaliacao"]);
                 while (readerResposta.Read())
                 {
                     respostas.Add(new Resposta(-1, int.Parse(readerResposta["cod_fichaAvaliacao"].ToString()), int.Parse(readerResposta["numero_pergunta"].ToString()), int.Parse(readerResposta["cod_item"].ToString()), int.Parse(readerResposta["cod_zona"].ToString()), int.Parse(readerResposta["valor"].ToString()),"",2));
                 }
                 readerResposta.Close();

                 readerResposta = DataBaseCommunicator.readData("SELECT item.cod_item " +
                                                                    "FROM  resposta_ficha_avaliacao_string " +
                                                                    "WHERE resposta_ficha_avaliacao_string.cod_fichaAvaliacao=" + readerPergunta["cod_fichaAvaliacao"]);

                 readerResposta.Read();
                 respostas.Add(new Resposta(-1, int.Parse(readerResposta["cod_fichaAvaliacao"].ToString()), int.Parse(readerResposta["numero_pergunta"].ToString()), int.Parse(readerResposta["cod_item"].ToString()), int.Parse(readerResposta["cod_zona"].ToString()), -1, readerResposta["valor"].ToString(), 2));
                 readerResposta.Close();
             }
        }
        #endregion
        /* ----------------------------------------------*/

        /* ----------------------------------------------*/
        /* Escala Resposta */

        public static EscalaResposta selectEscalaResposta(String codEscala)
        {
            String query = "select * from EscalaResposta where " + "cod_EscalaResposta = "
                + codEscala + ";";
            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            EscalaResposta e = null;

            while (r.Read())
            {
                e = new EscalaResposta((String)r["cod_EscalaResposta"],
                    (String)r["cod_TipoEscala"],
                    (String)r["descricaoEscalaResposta"],
                    (int)r["valorEscalaResposta"]);
            }

            return e;
        }

        public static void insertEscalaResposta(EscalaResposta e)
        {
            String query = "insert into EscalaResposta values (" + e.CodEscala + "," +
                e.CodTipo + "," + e.Descricao + "," + e.Valor + ";";

            Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.query(query);

        }

        public static void deleteEscalaResposta(String codEscala)
        {
            String query = "delete * from EscalaResposta where " + "cod_EscalaResposta = " +
                codEscala + ";";

            Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.query(query);
        }

        public static void updateEscalaResposta(EscalaResposta e)
        {
            String query = "update EscalaResposta set " + "cod_TipoEscala = " + e.CodTipo + ","
                + "descricaoEscalaResposta" + e.Descricao + ";" + "valorEscalaResposta" + e.Valor +
                ";" + "where cod_EscalaResposta = " + e.CodEscala + ";";

            Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.query(query);
        }

        /* ----------------------------------------------*/

        /* ----------------------------------------------*/
        /* Tipo Escala */

        public static TipoEscala selectTipoEscala(String codTipo)
        {
            String query = "select * from TipoEscala where " + "cod_tipoEscala = "
                + codTipo + ";";
            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            TipoEscala t = null;

            while (r.Read())
            {
                t = new TipoEscala((String)r["cod_TipoEscala"],
                    (String)r["tipoEscalaResposta"],
                    (int)r["numeroEscalaResposta"],
                    (int)r["default_tipoEscala"]);
            }

            return t;
        }

        public static void insertTipoEscala(TipoEscala t)
        {
            String query = "insert into TipoEscala values (" + t.Codigo + "," +
                t.Descricao + "," + t.Numero + "," + t.Default + ";";

            Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.query(query);

        }
    }

}


