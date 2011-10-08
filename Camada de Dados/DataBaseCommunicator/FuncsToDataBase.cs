﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ETdA.Camada_de_Dados.Classes;
using ETdA.Camada_de_Dados.Classes.Estruturas;
using System.Windows;
using System.Data.SqlTypes;

namespace ETdA.Camada_de_Dados.DataBaseCommunicator
{
    class FuncsToDataBase
    {
        #region Analistas
        /**
         * Liga o analista à sua base de dados
         * @param server Nome do servidor
         * @param database Nome da Base de dados
         * @param username Usernmae do analista
         * @param password Password do analista
         * @return bool Sucesso da ligacao
         */
        public static bool ligaAnalista(String server,
            String database, String username, String password)
        {
            return Camada_de_Dados.DataBaseCommunicator.
                DataBaseCommunicator.connect(server, username, password, database);
        }

        /**
         * Insere um novo analista na base de dados (e cria as tabelas)
         * @param username Username do analista
         * @param password Password do analista
         * @return bool Sucesso da inserção
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

        #endregion

        #region Projectos
        /**
         * Retorna os codigos e nomes de todos os Projectos por ordem de data
         * @return Dictionary<long, string> Codigo e Nomes dos estabecimentos dos projectos
         */
        public static Dictionary<long, string> selectNomeProjectos()
        {
            String query = "select cod_projecto, estabelecimento, activo from projecto order by ultimaActualizacao DESC;";
            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            Dictionary<long, string> cod_nome = new Dictionary<long, string>();

            while (r.Read())
            {
                if (int.Parse(r["activo"].ToString()) == 1)
                {
                    long cod = (long)r["cod_projecto"];
                    string nome = (string)r["estabelecimento"];
                    cod_nome.Add(cod, nome);
                }
            }

            return cod_nome;
        }

        /**
         * Devolve um Projecto do analista com o codigo recebido
         * @param cod Codigo do projecto que é requerido 
         * @return Projecto Projecto requerido
         */
        public static Projecto selectProjecto(long cod)
        {
            String query = "select * from projecto where cod_projecto = "
                + cod + ";";

            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.
                DataBaseCommunicator.readData(query);

            Projecto p = null;

            while (r.Read())
            {
                p = new Projecto((long)r["cod_projecto"],
                    (string)r["estabelecimento"], (DateTime)r["ultimaActualizacao"],
                    selectNomesAnalises(cod));
            }

            return p;
        }

        /**
         * Insere um novo projecto na base de dados
         * @param p Novo projecto que irá ser inserido (não contém código)
         * @return long Código do projecto que foi inserido
         */
        public static long insertProjecto(Projecto p)
        {
            String query = "insert into projecto values('" +
            p.Nome + "'," + "CAST('" + p.Data.ToString("yyyyMMdd HH:mm:ss")
            + "' AS datetime),1);" +
            "SELECT SCOPE_IDENTITY();";

            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            r.Read();
            return long.Parse(r[0].ToString());
        }

        /**
         * Elimina o projecto da base de dados com o código inserido
         * Elimina as análises do projecto
         * @param codProjecto Código do projecto que irá ser eliminado
         */
        public static void deleteProjecto(long codProjecto)
        {
            String query = "select cod_analise from analise where cod_procjecto = " + codProjecto;
            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            while (r.Read())
                deleteAnalise((long)r["cod_analise"]);

            query = "delete * from projecto where cod_projecto = " + codProjecto + ";";

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

        /**
         * Descativa o projecto na base de dados
         * @param codProjecto Código do projecto a ser desactivado
         */
        public static void desactivarProjecto(long codProjecto)
        {
            String query = "update projecto set activo = 0 where cod_projecto = " + codProjecto;
            Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.query(query);
        }
        #endregion

        #region Analises
        /**
         * Retorna os codigos e nomes das analises
         * @param codProjecto O Codigo do projecto à qual as analises fazem parte
         * @return Dictionary<long, string> Os codigos e os nomes das analises
         */
        public static Dictionary<long, string> selectNomesAnalises(long codProjecto)
        {
            String query = "select cod_analise, nomeAnalise, activo from analise where cod_projecto = " + codProjecto + ";";
            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            Dictionary<long, string> cod_nome = new Dictionary<long, string>();

            while (r.Read())
            {
                if (int.Parse(r["activo"].ToString()) == 1)
                {
                    long cod = (long)r["cod_analise"];
                    String nome = (string)r["nomeAnalise"];
                    cod_nome.Add(cod, nome);
                }
            }

            return cod_nome;
        }

        /**
         * Retorna a análise com o código recebido
         * @param codAnalise O Codigo da analise
         * @return Analise A analise requerida
         */
        public static Analise selectAnalise(long codAnalise)
        {
            String query = "select * from analise where cod_analise = "
                + codAnalise + ";";

            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.
                DataBaseCommunicator.readData(query);

            Analise a = null;

            r.Read();
            a = new Analise(codAnalise,
               (long)r["cod_projecto"],
               (DateTime)r["dataCriacao"],
               (string)r["nomeAnalise"],
               (string)r["tipoAnalise"],
               selectZonasAnalise(codAnalise),
               selectItensAnalise(codAnalise),
               int.Parse(r["estadoWebCheckList"].ToString()) == 0 ? false : true,
               int.Parse(r["estadoWebFichaAvaliacao"].ToString()) == 0 ? false : true,
               int.Parse(r["estadoWebQuestionario"].ToString()) == 0 ? false : true);

            return a;
        }

        /**
         * Insere uma nova analise na base de dados
         * Insere os itens da analise na tabela dos item_analise
         * Insere as zonas da analise na tabela dos zona_analise
         * @param codProjecto O Codigo do projecto em que faz parte a analise
         * @param a Analise que será inserida
         * @return long O código da análise que foi inserida
         */
        public static long insertAnalise(long codProjecto, Analise a)
        {
            String query = "insert into analise values(" + codProjecto + ","
            + "CAST('" + a.Data.ToString("yyyyMMdd HH:mm:ss") + "' AS datetime)"
            + ",'" + a.Nome + "','" + a.Tipo + "'," + (a.EstadoWebCL ? 1 : 0) + "," + (a.EstadoWebFA ? 1 : 0)
            + "," + (a.EstadoWebQ ? 1 : 0) + ",1);" + " SELECT SCOPE_IDENTITY();";

            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            r.Read();
            long cod = long.Parse(r[0].ToString());

            insertZonasAnalise(a.Zonas, cod);
            insertItensAnalise(a.Itens, cod);

            return cod;
        }

        /**
         * Elimina uma analise da base de dados
         * Elimina os itens da analise na tabela dos item_analise
         * Elimina as zonas da analise na tabela dos zona_analise
         * Elimina as Perguntas e Respostas dos Formularios desta Analise (se existirem)
         * @param codAnalise O codigo da analise que sera eliminada
         */
        public static void deleteAnalise(long codAnalise)
        {
            deleteZonasAnalise(codAnalise);
            deleteItemsAnalise(codAnalise);
            //deleteFormularios(codAnalise);
            String query = "delete * from analise where cod_analise = " + codAnalise + ";";

            Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.query(query);

        }

        /**
         * Modifica a informacao da analise passada como argumento
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

        /**
         * Descativa a analise na base de dados
         * @param codProjecto Código do projecto a ser desactivado
         * @param
         */
        public static void desactivarAnalise(long codAnalise)
        {
            String query = "update analise set activo = 0 where cod_analise = " + codAnalise;
            Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.query(query);
        }
        #endregion

        #region Zonas
        /**
         * Retorna as zonas/actividades/área comum que já foram adicionadas anteriormente
         * @return List<Zona> zonas/actividades/área comum
         */
        public static List<Zona> selectZonas(string tipo)
        {
            String query = "select cod_zona, nome_zona from zona, zona_analise, analise where analise" +
                ".tipo = '" + tipo + "' and zona_analise.cod_analise = analise.cod_analise and zona_analise" +
                ".cod_zona = zona.cod_zona";
            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            List<Zona> zonas = new List<Zona>();

            while (r.Read())
            {
                Zona zona = new Zona((long)r["cod_zona"], (String)r["nome_zona"]);
                zonas.Add(zona);
            }
            return zonas;
        }

        /**
         * Insere uma nova Zona na base de dados
         * @param z Zona que se pretende adicionar
         * @return long Código da zona que foi inserido
         */
        public static long insertZona(string nome)
        {
            String query = "insert into zona values('" + nome + "');" +
                "SELECT SCOPE_IDENTITY();";

            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            r.Read();
            return long.Parse(r[0].ToString());
        }

        /**
         * Remove uma Zona da base de dados
         * @param codZona Codigo da zona
         */
        public static void deleteZona(long codZona)
        {
            String query = "delete * from zona where cod_zona = " + codZona + ";";

            Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.query(query);
        }

        /**
         * Modifica uma zona
         * @param z Zona que se pretende modificar
         */
        public static void updateZona(Zona z)
        {
            String query = "update zona set nome_zona = '" + z.Nome + "' "
                + "where cod_zona = " + z.Codigo + ";";

            Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.query(query);
        }
        #endregion

        #region Zonas-Analise
        /**
         * Retorna as Zonas de uma dada análise
         * @param codAnalise Código da análise
         * @return List<Zona> Zonas da análise
         */
        public static List<Zona> selectZonasAnalise(long codAnalise)
        {
            String query = "select zona.cod_zona, nome_zona from zona_analise, zona where " +
                "zona_analise.cod_analise = " + codAnalise + " and zona_analise.cod_zona = zona.cod_zona;";
            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            List<Zona> zonas = new List<Zona>();

            while (r.Read())
            {
                Zona zona = new Zona((long)r["cod_zona"], (String)r["nome_zona"]);
                zonas.Add(zona);

            }
            return zonas;
        }

        //Revisto
        public static void insertZonasAnalise(List<Zona> zonas, long codAnalise)
        {
            foreach (Zona z in zonas)
            {
                String query = "insert into zona_analise values(" + z.Codigo + "," + codAnalise + ");";

                Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.query(query);
            }

        }

        //Revisto
        public static void deleteZonasAnalise(long codAnalise)
        {
            String query = "delete * from zona_analise where cod_analise = " + codAnalise + ";";
            Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.query(query);
        }
        #endregion

        #region Itens
        //Revisto
        public static Dictionary<long, string> selectItensDefault()
        {
            String query = "select cod_item, nome_item from item where default_item = 1;";
            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            Dictionary<long, string> itens_default = new Dictionary<long, string>();

            while (r.Read())
                itens_default.Add((long)r["cod_item"], (string)r["nome_item"]);

            return itens_default;
        }

        //Revisto
        public static Dictionary<long, string> selectAllItens()
        {
            String query = "select cod_item, nome_item from item order by default_item DESC;";
            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            Dictionary<long, string> itens = new Dictionary<long, string>();

            while (r.Read())
                itens.Add((long)r["cod_item"], (string)r["nome_item"]);

            return itens;
        }

        //Revisto
        public static long insertItem(string nome_item)
        {
            String query = "insert into item values('" + nome_item + "',0); " +
                           "SELECT SCOPE_IDENTITY();";

            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            r.Read();
            return long.Parse(r[0].ToString());
        }

        //Revisto
        public static void deleteItem(long codItem)
        {
            String query = "delete * from item where cod_item = " + codItem + ";";

            Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.query(query);
        }

        //Revisto
        public static void updateItem(long codItem, string novo_nome)
        {
            String query = "update item set nome_item = '" + novo_nome + "' "
                + "where cod_item = " + codItem + ";";

            Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.query(query);
        }
        #endregion

        #region Itens-Analise
        //Revisto
        public static List<Item> selectItensAnalise(long codAnalise)
        {
            String query = "select item.cod_item, nome_item, default_item, ponderacao_analista, " +
                "ponderacao_profissional, ponderacao_cliente, inter_vermelho, inter_laranja, inter_amarelo, " +
                "inter_verdelima, inter_verde, limite_inferior_analista from item_analise, item where cod_analise = " +
                codAnalise + " and item_analise.cod_item = item.cod_item ;";
            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            List<Item> items = new List<Item>();

            while (r.Read())
            {
                Item item = new Item((long)r["cod_item"],
                    r["nome_item"].ToString(),
                    r["default_item"].ToString() == "True" ? 1 : 0,
                    double.Parse(r["ponderacao_analista"].ToString()),
                    double.Parse(r["ponderacao_profissional"].ToString()),
                    double.Parse(r["ponderacao_cliente"].ToString()),
                    double.Parse(r["inter_vermelho"].ToString()),
                    double.Parse(r["inter_laranja"].ToString()),
                    double.Parse(r["inter_amarelo"].ToString()),
                    double.Parse(r["inter_verdelima"].ToString()),
                    double.Parse(r["inter_verde"].ToString()),
                    double.Parse(r["limite_inferior_analista"].ToString()));
                items.Add(item);
            }
            return items;
        }

        //Revisto
        public static void insertItensAnalise(List<Item> itens, long codAnalise)
        {
            foreach (Item i in itens)
            {
                double b = i.PonderacaoAnalista;
                String query = "insert into item_analise values(" + 
                    i.CodigoItem + "," + 
                    codAnalise + "," + 
                    (i.PonderacaoAnalista * 1000).ToString("0,000") + "," + 
                    (i.PonderacaoProfissional * 1000).ToString("0,000") + "," + 
                    (i.PonderacaoCliente * 1000).ToString("0,000") + "," + 
                    (i.Inter_Vermelho * 1000).ToString("0,000") + "," + 
                    (i.Inter_Laranja * 1000).ToString("0,000") + "," + 
                    (i.Inter_Amarelo * 1000).ToString("0,000") + "," +
                    (i.Inter_Verde_Lima * 1000).ToString("0,000") + "," + 
                    (i.Inter_Verde * 1000).ToString("0,000") + "," +
                    (i.LimiteInferiorAnalista * 1000).ToString("0,000") + ");";

                Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.query(query);
            }
        }

        //Revisto
        public static void deleteItemsAnalise(long codAnalise)
        {
            String query = "delete * from item_analise where cod_analise = " + codAnalise + ";";
            Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.query(query);
        }

        //Revisto
        public static void updateItemAnalise(Item i)
        {
            String query = "update item_analise set "
                + "ponderacao_analista = " + i.PonderacaoAnalista + ","
                + "ponderacao_profissional = " + i.PonderacaoProfissional + ","
                + "ponderacao_cliente = " + i.PonderacaoCliente
                + "inter_vermelho = " + i.Inter_Vermelho
                + "inter_laranja = " + i.Inter_Laranja
                + "inter_amarelo = " + i.Inter_Amarelo
                + "inter_verdelima = " + i.Inter_Verde_Lima
                + "inter_verde = " + i.Inter_Verde
                + "limite_inferior_analista = " + i.LimiteInferiorAnalista
                + "where cod_item = " + i.CodigoItem + ";";

            Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.query(query);
        }
        #endregion

        #region Questionario
        static public long insertQuestionario(Questionario q)
        {
            string query = "INSERT INTO questionario VALUES (" + q.CodAnalise + ");" +
                "SELECT SCOPE_IDENTITY();";

            SqlDataReader reader = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            reader.Read();
            return long.Parse(reader[0].ToString());

        }
        #endregion

        #region Ficha Avaliacao
        static public void insertFichaAvaliacao(FichaAvaliacao fa)
        {
            DataBaseCommunicator.query("INSERT INTO ficha_avaliacao VALUES (" +
                fa.CodZona + ", " + fa.CodAnalise + ");");
        }
        #endregion

        #region Respostas

        #region Respostas CheckList
        static public void selectRespostaCheckList(long codigoAnalise, List<Resposta> respostas)
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
                    readerResposta = DataBaseCommunicator.readData("SELECT cod_resposta_checkList, cod_analise, cod_zona, cod_item, valor " +
                                                                        "FROM resposta_checkList " +
                                                                        "WHERE cod_analise=" + codigoAnalise +
                                                                            " AND cod_zona= " + readerZona["cod_zona"] +
                                                                            " AND cod_item= " + readerItem["cod_item"]);
                    while (readerResposta.Read())
                    {
                        if (readerResposta["valor"].ToString() != "")
                        {
                            //Console.WriteLine("CHECKLIST: coda " + codigoAnalise + "codr " + (long)readerResposta["cod_resposta_checkList"] + "-1" + "-1" + "-1" + (long)readerItem["cod_item"] + (long)readerZona["cod_zona"] + short.Parse(readerResposta["valor"].ToString()) + " " + 1 + Classes.Resposta.TipoResposta.RespostaNum);
                            respostas.Add(new Resposta(codigoAnalise, (long)readerResposta["cod_resposta_checkList"], -1, -1, -1, (long)readerItem["cod_item"], (long)readerZona["cod_zona"], short.Parse(readerResposta["valor"].ToString()), "", 1, Classes.Resposta.TipoResposta.RespostaNum));
                        }
                    }
                    readerResposta.Close();
                }
                readerItem.Close();
            }
            readerZona.Close();
        }
        #endregion

        #region Respostas CheckList
        static public void insertRespostaCheckList(Resposta r)
        {
            DataBaseCommunicator.query("INSERT INTO resposta_checkList VALUES (" +
                    r.Valor + ", " + r.CodigoItem + ", " + r.CodigoZona + ", " + r.Cod_analise + ");");
        }
        #endregion

        #region Inserir Respostas CheckList
        static public void insertRespostaCheckList(int codigoAnalise, List<Resposta> respostas)
        {
            foreach (Resposta resposta in respostas)
            {
                switch (resposta.Tipo_Resposta)
                {
                    case (Resposta.TipoResposta.RespostaNum):
                        DataBaseCommunicator.query(
                            "INSERT INTO resposta_questionario_numero VALUES (" +
                            resposta.NumeroPergunta + ", " + resposta.Valor + ", " +
                            resposta.CodigoQuestionario + ", " + resposta.Cod_analise + ", " +
                            resposta.CodigoZona + ", " + resposta.Cod_pergunta + ");"
                            );
                        break;
                    case (Resposta.TipoResposta.RespostaStr):
                    case (Resposta.TipoResposta.RespostaMemo):

                        DataBaseCommunicator.query(
                            "INSERT INTO resposta_questionario_string VALUES (" +
                            resposta.NumeroPergunta + ", " + resposta.ValorString + ", " +
                            resposta.CodigoQuestionario + ", " + resposta.Cod_analise + ", " +
                            resposta.CodigoZona + ", " + resposta.Cod_pergunta + ");"
                            );
                        break;
                }
            }
        }
        #endregion

        #region Respostas Ficha de Avaliação
        static public void selectRespostaFichaAvaliacao(long codigoAnalise, List<Resposta> respostas)
        {
            SqlDataReader readerPergunta, readerResposta;

            readerPergunta = DataBaseCommunicator.readData("SELECT ficha_avaliacao.cod_fichaAvaliacao " +
                                                "FROM  ficha_avaliacao " +
                                                "WHERE ficha_avaliacao.cod_analise=" + codigoAnalise);
            while (readerPergunta.Read())
            {
                readerResposta = DataBaseCommunicator.readData("SELECT resposta_ficha_avaliacao_numero.numero_pergunta, pergunta_ficha_avaliacao.cod_item, cod_fichaAvaliacao, cod_zona, valor " +
                                                                   "FROM  resposta_ficha_avaliacao_numero, pergunta_ficha_avaliacao " +
                                                                   "WHERE resposta_ficha_avaliacao_numero.cod_fichaAvaliacao=" + readerPergunta["cod_fichaAvaliacao"] +
                                                                   "AND pergunta_ficha_avaliacao.numero_pergunta = resposta_ficha_avaliacao_numero.numero_pergunta");
                while (readerResposta.Read())
                {
                    //Console.WriteLine("FICHA AVALIACAO:" + codigoAnalise + "/-1" + "/-1" + "/" + (long)readerResposta["cod_fichaAvaliacao"] + "/" + short.Parse(readerResposta["numero_pergunta"].ToString()) + "/" + (long)readerResposta["cod_item"] + "/" + (long)readerResposta["cod_zona"] + "/" + short.Parse(readerResposta["valor"].ToString()) + "/" + "" + "/" + 2 + "/" + Classes.Resposta.TipoResposta.RespostaNum);
                    respostas.Add(new Resposta(codigoAnalise, -1, -1, (long)readerResposta["cod_fichaAvaliacao"], short.Parse(readerResposta["numero_pergunta"].ToString()), (long)readerResposta["cod_item"], (long)readerResposta["cod_zona"], short.Parse(readerResposta["valor"].ToString()), "", 2, Classes.Resposta.TipoResposta.RespostaNum));
                }
                readerResposta.Close();
                readerResposta = DataBaseCommunicator.readData("SELECT resposta_ficha_avaliacao_string.numero_pergunta, cod_fichaAvaliacao, cod_zona, valor " +
                                                                   "FROM resposta_ficha_avaliacao_string " +
                                                                   "WHERE resposta_ficha_avaliacao_string.cod_fichaAvaliacao=" + readerPergunta["cod_fichaAvaliacao"]);

                if (readerResposta.Read())
                    respostas.Add(new Resposta(codigoAnalise, -1, -1, int.Parse(readerResposta["cod_fichaAvaliacao"].ToString()), -1, -1, int.Parse(readerResposta["cod_zona"].ToString()), -1, readerResposta["valor"].ToString(), 2, Classes.Resposta.TipoResposta.RespostaMemo));
                readerResposta.Close();
            }
        }
        #endregion

		#region Inserir Respostas Ficha de Avaliacao
		static public void insertRespostaFichaAvaliacao(Resposta r)
        {
            switch (r.Tipo_Resposta)
            {
                case Resposta.TipoResposta.RespostaNum:
                    DataBaseCommunicator.query("INSERT INTO resposta_ficha_avaliacao_numero VALUES (" +
                        r.NumeroPergunta + ", " + r.Valor + ", " + r.Cod_fichaAvaliacao + ", " +
                        r.Cod_analise + ");");
                    break;
                case Resposta.TipoResposta.RespostaStr:
                    DataBaseCommunicator.query("INSERT INTO resposta_ficha_avaliacao_string VALUES (" +
                        r.NumeroPergunta + ", " + r.ValorString + ", " + r.Cod_fichaAvaliacao + ", "
                        + r.Cod_analise + ");");
                    break;
            }

        }
        #endregion

        #region Respostas Questionario
        static public void selectRespostaQuestionario(long codigoAnalise, List<Resposta> respostas)
        {
            SqlDataReader reader, readerResposta;
            int cod_item, cod_zona;

            reader = DataBaseCommunicator.readData("SELECT cod_pergunta_questionario, numero_pergunta, TipoEscala.numeroEscalaResposta, cod_zona, cod_item " +
                                                "FROM pergunta_questionario, TipoEscala " +
                                                "WHERE pergunta_questionario.cod_tipoEscala=TipoEscala.cod_tipoEscala " +
                                                "AND pergunta_questionario.cod_analise=" + codigoAnalise);


            while (reader.Read())
            {
                if (reader["cod_item"].ToString() == "")
                    cod_item = -1;
                else
                    cod_item = int.Parse(reader["cod_item"].ToString());

                if (reader["cod_zona"].ToString() == "")
                    cod_zona = -1;
                else
                    cod_zona = int.Parse(reader["cod_zona"].ToString());

                //String
                if (int.Parse(reader["numeroEscalaResposta"].ToString()) == 0)
                {
                    readerResposta = DataBaseCommunicator.readData("SELECT cod_questionario, valor, resposta_questionario_string.cod_zona, resposta_questionario_string.cod_zona,resposta_questionario_string.numero_pergunta " +
                                                                       "FROM resposta_questionario_string, pergunta_questionario " +
                                                                       "WHERE resposta_questionario_string.numero_pergunta=" + reader["numero_pergunta"].ToString() +
                                                                       "AND resposta_questionario_string.numero_pergunta=pergunta_questionario.numero_pergunta" +
                                                                       "AND respostas_questionario.cod_analise=" + codigoAnalise);
                    while (readerResposta.Read())
                    {
                        //Console.WriteLine("QUESTIONARIO:" + codigoAnalise + "-1" + int.Parse(readerResposta["cod_questionario"].ToString()) + "-1" + int.Parse(reader["numero_pergunta"].ToString()) + cod_item + cod_zona + "-1" + readerResposta["valor"].ToString() + 3 + Classes.Resposta.TipoResposta.RespostaStr);
                        respostas.Add(new Resposta(codigoAnalise, -1, int.Parse(readerResposta["cod_questionario"].ToString()), -1, int.Parse(reader["numero_pergunta"].ToString()), cod_item, cod_zona, -1, readerResposta["valor"].ToString(), 3, Classes.Resposta.TipoResposta.RespostaStr));
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
                        respostas.Add(new Resposta(codigoAnalise, -1, int.Parse(readerResposta["cod_questionario"].ToString()), -1, int.Parse(reader["numero_pergunta"].ToString()), cod_item, cod_zona, -1, readerResposta["valor"].ToString(), 3, Classes.Resposta.TipoResposta.RespostaMemo));
                    }
                    readerResposta.Close();
                }
                //numero
                else
                {
                    readerResposta = DataBaseCommunicator.readData("SELECT resposta_questionario_numero.cod_questionario, resposta_questionario_numero.valor, resposta_questionario_numero.cod_zona, resposta_questionario_numero.numero_pergunta " +
                                                                       " FROM resposta_questionario_numero, pergunta_questionario " +
                                                                       " WHERE resposta_questionario_numero.numero_pergunta=" + reader["numero_pergunta"].ToString() +
                                                                       " AND resposta_questionario_numero.numero_pergunta=pergunta_questionario.numero_pergunta" +
                                                                       " AND resposta_questionario_numero.cod_analise=" + codigoAnalise);
                    while (readerResposta.Read())
                    {
                        if (readerResposta["valor"].ToString() != "" && reader["numero_pergunta"].ToString() != "")
                        {
                            //Console.WriteLine("QUESTIONARIO:" + codigoAnalise + -1 + long.Parse(readerResposta["cod_questionario"].ToString()) + -1 + long.Parse(reader["numero_pergunta"].ToString()) + cod_item + cod_zona + short.Parse(readerResposta["valor"].ToString()) + "" + 3 + Classes.Resposta.TipoResposta.RespostaNum);
                            respostas.Add(new Resposta(codigoAnalise, -1, long.Parse(readerResposta["cod_questionario"].ToString()), -1, long.Parse(reader["numero_pergunta"].ToString()), cod_item, cod_zona, short.Parse(readerResposta["valor"].ToString()), "", 3, Classes.Resposta.TipoResposta.RespostaNum));
                        }
                    }
                    readerResposta.Close();
                }

            }
            reader.Close();
        }
        #endregion

        #region INserir Resposta Questionário
        static public long insertRespostaQuestionario(Resposta r)
        {
            string query;
            switch (r.Tipo_Resposta)
            {
                case Resposta.TipoResposta.RespostaNum:
                    query = "INSERT INTO resposta_questionario_numero VALUES (" +
                        r.NumeroPergunta + ", " + r.Valor + ", " + r.Cod_questionario + ", " +
                        r.Cod_analise + ", " + r.CodigoZona + ", " + r.Cod_pergunta + ");" +
                        "SELECT SCOPE_IDENTITY();";
                    break;
                case Resposta.TipoResposta.RespostaStr:
                    query = "INSERT INTO resposta_questionario_string VALUES (" +
                        r.NumeroPergunta + ", " + r.ValorString + ", " + r.Cod_questionario + ", " +
                        r.Cod_analise + ", " + r.CodigoZona + ", " + r.Cod_pergunta + ");" +
                        "SELECT SCOPE_IDENTITY();";
                    break;
                default:
                    query = "INSERT INTO resposta_questionario_memo VALUES (" +
                        r.NumeroPergunta + ", " + r.ValorString + ", " + r.Cod_questionario + ", " +
                        r.Cod_analise + ", " + r.CodigoZona + ", " + r.Cod_pergunta + ");"+
                        "SELECT SCOPE_IDENTITY();";
                    break;
            }

            MessageBox.Show(query);
            //SqlDataReader reader = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            //reader.Read();
            //return long.Parse(reader[0].ToString());
            return 1;
        }
        #endregion

        #region Select Resposta Sexo

        static public List<int> selectRespostasSexo (long codigoAnalise)
        {
            SqlDataReader reader;

            reader = DataBaseCommunicator.readData("SELECT valor, cod_resposta_questionario_numero " +
                                                "FROM pergunta_questionario, resposta_questionario_numero " +
                                                "WHERE pergunta_questionario.cod_pergunta_questionario = resposta_questionario_numero.cod_pergunta_questionario " +
                                                "AND pergunta_questionario.numero_pergunta = 1 " +
                                                "AND pergunta_questionario.cod_analise=" + codigoAnalise);

            List<int> respostas = new List<int>();
            String aux = null;

            while (reader.Read())
            {
                aux = reader["valor"].ToString();
                int resposta = Convert.ToInt32(aux);
                respostas.Add(resposta);
            }
           
            return respostas;
        }

        #endregion

        #region Idade
        static public List<int> selectIdades (long codigoAnalise)
        {
            List<int> idades = new List<int>();

            string query = "SELECT valor, cod_resposta_questionario_numero " +
                           "FROM pergunta_questionario, resposta_questionario_numero " +
                           "WHERE pergunta_questionario.cod_pergunta_questionario = resposta_questionario_numero.cod_pergunta_questionario " +
                           "AND pergunta_questionario.numero_pergunta = 16 " +
                           "AND pergunta_questionario.cod_analise=" + codigoAnalise; 
            
           SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

           String aux = null;

           while (r.Read())
           {
               aux = r["valor"].ToString();
               int resposta = Convert.ToInt32(aux);
               //Console.WriteLine("idade :" + resposta);
               idades.Add(resposta);
           }

           return idades;
        }

        #endregion

        #region Cliente Habitual

        static public List<int> selectRespostasHabitual(long codigoAnalise)
        {
            SqlDataReader reader;

            reader = DataBaseCommunicator.readData("SELECT valor, cod_resposta_questionario_numero " +
                                                "FROM pergunta_questionario, resposta_questionario_numero " +
                                                "WHERE pergunta_questionario.cod_pergunta_questionario = resposta_questionario_numero.cod_pergunta_questionario " +
                                                "AND pergunta_questionario.numero_pergunta = 2 " +
                                                "AND pergunta_questionario.cod_analise=" + codigoAnalise);

            List<int> respostas = new List<int>();
            String aux = null;

            while (reader.Read())
            {
                aux = reader["valor"].ToString();
                int resposta = Convert.ToInt32(aux);
                respostas.Add(resposta);
            }

            return respostas;
        }
        #endregion

        #region Escala Resposta
        public static List<EscalaResposta> selectRespostas(long cod_tipoEscala)
        {
            List<EscalaResposta> resps = new List<EscalaResposta>();

            String query = "select * from EscalaResposta where cod_TipoEscala = "
                + cod_tipoEscala + ";";

            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            while (r.Read())
            {
                EscalaResposta e = new EscalaResposta(
                    (long)r["cod_EscalaResposta"],
                    (long)r["cod_TipoEscala"],
                    (String)r["descricaoEscalaResposta"],
                    (int)r["valorEscalaResposta"]);

                resps.Add(e);
            }

            return resps;
        }

        public static EscalaResposta selectEscalaResposta(long codEscala)
        {
            String query = "select * from EscalaResposta where cod_EscalaResposta = "
                + codEscala + ";";
            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            EscalaResposta e = null;

            while (r.Read())
            {
                e = new EscalaResposta((long)r["cod_EscalaResposta"],
                    (long)r["cod_TipoEscala"],
                    (String)r["descricaoEscalaResposta"],
                    (int)r["valorEscalaResposta"]);
            }

            return e;
        }

        // usada 
        public static long insertEscalaResposta(EscalaResposta e)
        {
            String query = "insert into EscalaResposta values (" + 
                e.CodTipo + ",'" + 
                e.Descricao + "'," + 
                e.Valor + ");" +
                "SELECT SCOPE_IDENTITY();";
            MessageBox.Show(query);
            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            r.Read();
            return long.Parse(r[0].ToString());
        }

        public static void deleteEscalaResposta(String codEscala)
        {
            String query = "delete * from EscalaResposta where cod_EscalaResposta = " +
                codEscala + ";";

            Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.query(query);
        }

        public static void updateEscalaResposta(EscalaResposta e)
        {
            String query = "update EscalaResposta set cod_TipoEscala = " + e.CodTipo + ","
                + "descricaoEscalaResposta" + e.Descricao + ";" + "valorEscalaResposta" + e.Valor +
                ";" + "where cod_EscalaResposta = " + e.CodEscala + ";";

            Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.query(query);
        }
        #endregion

        #region TipoEscala
        public static Dictionary<string, List<TipoEscala>> getTiposResposta()
        {
            Dictionary<string, List<TipoEscala>> resps = new Dictionary<string, List<TipoEscala>>();

            string query = "select * from TipoEscala";
            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);
            
            while (r.Read())
            {
                TipoEscala te = new TipoEscala(
                    (long)r["cod_tipoEscala"],
                    (string)r["tipoEscalaResposta"],
                    (int)r["numeroEscalaResposta"],
                    r["default_tipoEscala"].ToString() == "True" ? 1 : 0,
                    selectRespostas((long)r["cod_tipoEscala"]));

                if (resps.ContainsKey(te.Descricao))
                {
                    resps[te.Descricao].Add(te);
                }
                else
                {
                    List<TipoEscala> ti = new List<TipoEscala>();
                    ti.Add(te); 
                    resps.Add(te.Descricao, ti);
                }
                
            }

            return resps;
        }

        public static TipoEscala selectTipoEscala(long codTipo)
        {
            String query = "select * from TipoEscala where cod_tipoEscala = "
                + codTipo + ";";
            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            TipoEscala t = null;

            while (r.Read())
            {
                t = new TipoEscala((long)r["cod_tipoEscala"],
                    (String)r["tipoEscalaResposta"],
                    (int)r["numeroEscalaResposta"],
                    r["default_tipoEscala"].ToString() == "true" ? 1 : 0,
                    selectRespostas((long)r["cod_tipoEscala"]));
            }

            return t;
        }

        // usada
        public static long insertTipoEscala(TipoEscala t)
        {
            String query = "insert into TipoEscala values ('" + 
                t.Descricao + "'," +
                t.Numero + ",1);" +
                "SELECT SCOPE_IDENTITY();";

            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            r.Read();
            return long.Parse(r[0].ToString());

        }
        #endregion

        #region NumeroEscala
        //Desvolve a cardinalidade das escala de reposta para um dada analise e para um dada pergunta dum tipo de resposta
        static public int numeroEscalaResposta(long analise, float num_pergunta, int tipoResposta)
        {
            SqlDataReader reader, readerTipoEscala;

            //Questionario
            if (tipoResposta == 3)
            {
                /*Console.WriteLine("SELECT cod_tipoEscala " +
                                                        "FROM pergunta_questionario " +
                                                        "WHERE cod_analise=" + analise +
                                                            " AND numero_pergunta=" + num_pergunta);
                 */
                reader = DataBaseCommunicator.readData("SELECT cod_tipoEscala " +
                                                        "FROM pergunta_questionario " +
                                                        "WHERE cod_analise=" + analise +
                                                            " AND numero_pergunta=" + num_pergunta);
            }
            //FIcha de Avalicao -if(tipoResposta==2)
            else
            {
                /*Console.WriteLine("SELECT cod_tipoEscala " +
                                                        "FROM pergunta_ficha_avaliacao " +
                                                        "WHERE cod_analise=" + analise +
                                                            " AND numero_pergunta=" + num_pergunta);
                 */
                reader = DataBaseCommunicator.readData("SELECT cod_tipoEscala " +
                                                        "FROM pergunta_ficha_avaliacao " +
                                                        "WHERE cod_analise=" + analise +
                                                            " AND numero_pergunta=" + num_pergunta);
            }

            if (reader.Read())
            {
               // if (reader["cod_tipoEscala"].ToString()!="")
                //{
                    readerTipoEscala = DataBaseCommunicator.readData("SELECT numeroEscalaResposta " +
                                                                        "FROM TipoEscala " +
                                                                            "WHERE cod_tipoEscala=" + reader["cod_tipoEscala"]);

                    reader.Close();
                    if (readerTipoEscala.Read())
                        return (int.Parse(readerTipoEscala["numeroEscalaResposta"].ToString()));
               // }
            }
            return -1;
        }
        #endregion

        #endregion

        #region Perguntas

        public static long insertPerguntaFichaAvaliacao(PerguntaFichaAvaliacao p)
        {
            StringBuilder a = new StringBuilder();
            a.Append("insert into pergunta_ficha_avaliacao VALUES (");
            a.Append(p.Cod_Analise.ToString() + ",");
            a.Append(p.Num_Pergunta.ToString() + ",");
            a.Append(p.Cod_Item.ToString() + ",'");
            a.Append(p.Texto + "',");
            a.Append(p.Cod_TipoEscala.ToString() + ");");
            a.Append("SELECT SCOPE_IDENTITY();");

            string query = a.ToString();

            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            r.Read();
            return long.Parse(r[0].ToString());
        }

        public static long insertPerguntaQuestionario(PerguntaQuestionario p)
        {
            StringBuilder a = new StringBuilder();
            a.Append("insert into pergunta_questionario VALUES (");
            a.Append(p.Cod_Analise.ToString() + ",");
            a.Append((p.Num_Pergunta > (int)p.Num_Pergunta) ? (p.Num_Pergunta.ToString().Split(',')[0] + "." + p.Num_Pergunta.ToString().Split(',')[1] + ",") : p.Num_Pergunta.ToString() + ",");
            a.Append(((p.Cod_zona==0)?"null":p.Cod_zona.ToString()) + ",");
            a.Append(((p.Cod_Item==-1)?"null":p.Cod_Item.ToString()) + ",");
            a.Append(p.Cod_TipoEscala.ToString() + ",'");
            a.Append(p.Texto + "','");
            a.Append(p.TipoQuestao + "');");
            a.Append("SELECT SCOPE_IDENTITY();");

            string query = a.ToString();

            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            r.Read();
            return long.Parse(r[0].ToString());
        }

        public static bool isFAcreated(long codAnalise)
        {
            string query = "select count(*) from pergunta_ficha_avaliacao where cod_analise = " + codAnalise + ";";

            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            r.Read();
            return (int.Parse(r[0].ToString()) > 0) ? true : false; 
        }

        public static bool isQTcreated(long codAnalise)
        {
            string query = "select count(*) from pergunta_questionario where cod_analise = " + codAnalise + ";";

            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            r.Read();
            return (int.Parse(r[0].ToString()) > 0) ? true : false; 
        }

        public static bool isFAOnline(long codAnalise)
        {
            string query = "select estadoWebFichaAvaliacao from analise where cod_analise = " + codAnalise + ";";

            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            r.Read();
            return (int.Parse(r[0].ToString()) == 1) ? true : false; 
        }

        public static bool isQTOnline(long codAnalise)
        {
            string query = "select estadoWebQuestionario from analise where cod_analise = " + codAnalise + ";";

            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            r.Read();
            return (int.Parse(r[0].ToString()) == 1) ? true : false;
        }

        public static bool haveAnswerFA(long codAnalise)
        {
            string query = "select count(*) from resposta_ficha_avaliacao_numero where cod_analise = " + codAnalise + ";";

            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            r.Read();
            return (int.Parse(r[0].ToString()) > 0) ? true : false; 
        }

        public static bool haveAnswerQT(long codAnalise)
        {
            bool b;
            string query = "select count(*) from resposta_questionario_numero where cod_analise = " + codAnalise + ";";

            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            r.Read();
            b = (int.Parse(r[0].ToString()) > 0) ? true : false;
            if (b) return b;

            query = "select count(*) from resposta_questionario_string where cod_analise = " + codAnalise + ";";

            r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            r.Read();
            b = (int.Parse(r[0].ToString()) > 0) ? true : false;
            if (b) return b;

            query = "select count(*) from resposta_questionario_memo where cod_analise = " + codAnalise + ";";

            r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            r.Read();
            b = (int.Parse(r[0].ToString()) > 0) ? true : false;
            return b;
        }

        public static List<PerguntaFichaAvaliacao> selectPerguntasFA(long codAnalise)
        {
            string query = "select * from pergunta_ficha_avaliacao where cod_analise = " + codAnalise.ToString() + ";";

            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            List<PerguntaFichaAvaliacao> pergs = new List<PerguntaFichaAvaliacao>();

            while (r.Read())
            {
                PerguntaFichaAvaliacao p = new PerguntaFichaAvaliacao(
                    (long)r["cod_pergunta_ficha_avaliacao"],
                    (long)r["cod_analise"],
                    (int)r["numero_pergunta"],
                    (long)r["cod_item"],
                    (string)r["texto"],
                    (long)r["cod_tipoEscala"]);
                pergs.Add(p);
            }

            return pergs;
        }

        public static List<PerguntaQuestionario> selectPerguntasQT(long codAnalise)
        {
            string query = "select * from pergunta_questionario where cod_analise = " + codAnalise.ToString() + ";";

            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            List<PerguntaQuestionario> pergs = new List<PerguntaQuestionario>();

            while (r.Read())
            {
                PerguntaQuestionario p = new PerguntaQuestionario(
                    (long)r["cod_pergunta_questionario"],
                    (long)r["cod_analise"],
                    (float)r["numero_pergunta"],
                    ((r["cod_zona"].ToString() == "") ? 0 : (long)r["cod_zona"]),
                    ((r["cod_item"].ToString() == "") ? -1 : (long)r["cod_item"]),
                    (string)r["texto"],
                    (long)r["cod_tipoEscala"],
                    (string)r["tipo_questao"]);
                pergs.Add(p);
            }

            return pergs;
        }

        public static void updatePerguntasFA(PerguntaFichaAvaliacao p)
        {
            String query = "update pergunta_ficha_avaliacao set " +
                "cod_item = " + p.Cod_Item + "," +
                "texto = '" + p.Texto + "'," +
                "cod_tipoEscala = " + p.Cod_TipoEscala +
                " where cod_analise = " + p.Cod_Analise + " and " +
                "numero_pergunta = " + p.Num_Pergunta + ";";

            Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.query(query);
        }

        // not used
        public static void updatePerguntasQT(PerguntaQuestionario p)
        {
            String query = "update pergunta_questionario set " +
                "cod_zona = " + ((p.Cod_zona == -1) ? "null" : p.Cod_zona.ToString()) + "," +
                "cod_item = " + ((p.Cod_Item == -1) ? "null" : p.Cod_Item.ToString()) + "," +
                "texto = '" + p.Texto + "'," +
                "cod_tipoEscala = " + p.Cod_TipoEscala + "," +
                "tipo_questao = '" + p.TipoQuestao +
                "' where cod_analise = " + p.Cod_Analise + " and " +
                "numero_pergunta = " + p.Num_Pergunta + ";";

            Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.query(query);
        }

        public static void deletePerguntasQT(long codAnalise)
        {
            string query = "delete from pergunta_questionario where cod_analise = " + codAnalise + ";";

            Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.query(query);
        }
        #endregion

        #region Website
        #region Get Estado Formulários

        public static bool getEstadoCheckListOnline(long codProjecto, long codAnalise)
        {
            string query = "SELECT analise.estadoWebCheckList FROM analise WHERE analise.cod_analise = " + codAnalise + 
                            " AND analise.cod_projecto = " + codProjecto;

            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            if (r.Read())
            {
                if (int.Parse(r["estadoWebCheckList"].ToString()) == 1)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public static bool getEstadoFichaAvaliacaoOnline(long codProjecto, long codAnalise)
        {
            string query = "SELECT analise.estadoWebFichaAvaliacao FROM analise WHERE analise.cod_analise = " + codAnalise +
                            " AND analise.cod_projecto = " + codProjecto;

            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            r.Read();
            if (int.Parse(r["estadoWebFichaAvaliacao"].ToString()) == 1)
                return true;
            else
                return false;
        }

        public static bool getEstadoQuestionariosOnline(long codProjecto, long codAnalise)
        {
            string query = "SELECT analise.estadoWebQuestionario FROM analise WHERE analise.cod_analise = " + codAnalise +
                            " AND analise.cod_projecto = " + codProjecto;

            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.readData(query);

            r.Read();
            if (int.Parse(r["estadoWebQuestionario"].ToString()) == 1)
                return true;
            else
                return false;
        }
        #endregion
        #region Set Estado Formulários
        public static void setEstadoCheckListOnline(long codProjecto, long codAnalise, bool estado)
        {
            int value;
            if(estado)
                value=1;
            else
                value=0;

            DataBaseCommunicator.query("Update analise "+
                                       "Set estadoWebCheckList=" + value +
                                        "where analise.cod_projecto = "+codProjecto+
                                        " and analise.cod_analise = "+codAnalise);
        }

        public static void setEstadoFichaAvaliacaoOnline(long codProjecto, long codAnalise, bool estado)
        {
            int value;
            if(estado)
                value=1;
            else
                value=0;

            DataBaseCommunicator.query("Update analise " +
                                       "Set estadoWebFichaAvaliacao="+ value +
                                        "where analise.cod_projecto = " + codProjecto +
                                        " and analise.cod_analise = " + codAnalise);
        }

        public static void setEstadoQuestionarioOnline(long codProjecto, long codAnalise, bool estado)
        {
            int value;
            if(estado)
                value=1;
            else
                value=0;

            DataBaseCommunicator.query("Update analise " +
                                       "Set estadoWebQuestionario=" + value +
                                        "where analise.cod_projecto = " + codProjecto +
                                        " and analise.cod_analise = " + codAnalise);
        }
        #endregion
        #endregion
    }
}