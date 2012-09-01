using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ETdAnalyser.CamadaDados.Classes;
using ETdAnalyser.CamadaDados.Classes.Estruturas;
using System.Windows;
using System.Data.SqlTypes;

namespace ETdAnalyser.CamadaDados.DataBaseCommunicator
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
        public static bool LigaAnalista(String server,
            String database, String username, String password)
        {
            return CamadaDados.DataBaseCommunicator.
                DataBaseCommunicator.Connect(server, username, password, database);
        }

        /**
         * Insere um novo analista na base de dados (escalaResposta cria as tabelas)
         * @param username Username do analista
         * @param password Password do analista
         * @return bool Sucesso da inserção
         */
        public static bool InsertAnalista(String username, String password)
        {
            try
            {
                List<string> querys = ParserCreateAnalista.DevolveQuery(username, password);

                foreach (string s in querys)
                    CamadaDados.DataBaseCommunicator.DataBaseCommunicator.Query(s);

                return true;
            }
            catch(SqlException ex)
            {
                if (ex.Number == 1801 || ex.Number == 15025 || ex.Number == 2714)
                {
                    MessageBox.Show("Nome de utilizador já existente. Por favor escolha um nome diferente.");
                }
                return false;
            }
        }

        #endregion

        #region Projectos
        /**
         * Retorna os codigos escalaResposta nomes de todos os Projectos por ordem de data
         * @return Dictionary<long, string> CodigoTipo escalaResposta Nomes dos estabecimentos dos projectos
         */
        public static Dictionary<long, string> SelectNomeProjectos()
        {
            String query = "select cod_projecto, estabelecimento, activo from projecto order by ultimaActualizacao DESC;";
            SqlDataReader sqlDataReader = CamadaDados.DataBaseCommunicator.DataBaseCommunicator.ReadData(query);
            
            Dictionary<long, string> cod_nome = new Dictionary<long, string>();

            while (sqlDataReader.Read())
            {
                if (int.Parse(sqlDataReader["activo"].ToString()) == 1)
                {
                    long cod = (long)sqlDataReader["cod_projecto"];
                    string nome = (string)sqlDataReader["estabelecimento"];
                    cod_nome.Add(cod, nome);
                }
            }

            return cod_nome;
        }

        /**
         * Devolve um Projecto do analista com o codigo recebido
         * @param codigo CodigoTipo do projecto que é requerido 
         * @return Projecto Projecto requerido
         */
        public static Projecto SelectProjecto(long cod)
        {
            String query = "select * from projecto where cod_projecto = "
                + cod + ";";

            SqlDataReader sqlDataReader = CamadaDados.DataBaseCommunicator.
                DataBaseCommunicator.ReadData(query);

            Projecto p = null;

            while (sqlDataReader.Read())
            {
                p = new Projecto((long)sqlDataReader["cod_projecto"],
                    (string)sqlDataReader["estabelecimento"], (DateTime)sqlDataReader["ultimaActualizacao"],
                    SelectNomesAnalises(cod));
            }

            return p;
        }

        /**
         * Insere um novo projecto na base de dados
         * @param perguntaQuestionario Novo projecto que irá ser inserido (não contém código)
         * @return long Código do projecto que foi inserido
         */
        public static long InsertProjecto(Projecto p)
        {
            String query = "insert into projecto values('" +
            p.Nome + "'," + "CAST('" + p.Data.ToString("yyyyMMdd HH:mm:ss")
            + "' AS datetime),1);" +
            "SELECT SCOPE_IDENTITY();";

            SqlDataReader sqlDataReader = CamadaDados.DataBaseCommunicator.DataBaseCommunicator.ReadData(query);

            sqlDataReader.Read();
            return long.Parse(sqlDataReader[0].ToString());
        }

        /**
         * Elimina o projecto da base de dados com o código inserido
         * Elimina as análises do projecto
         * @param cod_projecto Código do projecto que irá ser eliminado
         */
        public static void DeleteProjecto(long codigoProjecto)
        {
            String query = "select cod_analise from analise where cod_procjecto = " + codigoProjecto;
            SqlDataReader sqlDataReader = CamadaDados.DataBaseCommunicator.DataBaseCommunicator.ReadData(query);

            while (sqlDataReader.Read())
                DeleteAnalise((long)sqlDataReader["cod_analise"]);

            query = "delete * from projecto where cod_projecto = " + codigoProjecto + ";";

            CamadaDados.DataBaseCommunicator.DataBaseCommunicator.Query(query);
        }

        /**
         * Modifica o projecto na base de dados com o código do projecto recebido
         * @param perguntaQuestionario Projecto que irá ser editado
         */
        public static void UpdateProjecto(Projecto p)
        {
            String query = "update projecto set " + "estabelecimento = " + p.Nome + ","
                + "ultimaActualizacao = " + p.Data + "where cod_projecto = " + p.Codigo + ";";

            CamadaDados.DataBaseCommunicator.DataBaseCommunicator.Query(query);
        }

        /**
         * Descativa o projecto na base de dados
         * @param cod_projecto Código do projecto a ser desactivado
         */
        public static void DesactivarProjecto(long codigoProjecto)
        {
            String query = "update projecto set activo = 0 where cod_projecto = " + codigoProjecto;
            CamadaDados.DataBaseCommunicator.DataBaseCommunicator.Query(query);
        }
        #endregion

        #region Analises
        /**
         * Retorna os codigos escalaResposta nomes das analises
         * @param cod_projecto O CodigoTipo do projecto à qual as analises fazem parte
         * @return Dictionary<long, string> Os codigos escalaResposta os nomes das analises
         */
        public static Dictionary<long, string> SelectNomesAnalises(long codigoProjecto)
        {
            String query = "select cod_analise, nomeAnalise, activo from analise where cod_projecto = " + codigoProjecto + ";";
            SqlDataReader sqlDataReader = CamadaDados.DataBaseCommunicator.DataBaseCommunicator.ReadData(query);

            Dictionary<long, string> cod_nome = new Dictionary<long, string>();

            while (sqlDataReader.Read())
            {
                if (int.Parse(sqlDataReader["activo"].ToString()) == 1)
                {
                    long cod = (long)sqlDataReader["cod_analise"];
                    String nome = (string)sqlDataReader["nomeAnalise"];
                    cod_nome.Add(cod, nome);
                }
            }

            return cod_nome;
        }

        /**
         * Retorna a análise com o código recebido
         * @param codigoAnalise O CodigoTipo da analise
         * @return Analise A analise requerida
         */
        public static Analise SelectAnalise(long codigoAnalise)
        {
            String query = "select * from analise where cod_analise = "
                + codigoAnalise + ";";

            SqlDataReader sqlDataReader = CamadaDados.DataBaseCommunicator.
                DataBaseCommunicator.ReadData(query);

            Analise a = null;

            sqlDataReader.Read();
            a = new Analise(codigoAnalise,
               (long)sqlDataReader["cod_projecto"],
               (DateTime)sqlDataReader["dataCriacao"],
               (string)sqlDataReader["nomeAnalise"],
               (string)sqlDataReader["tipoAnalise"],
               SelectZonasAnalise(codigoAnalise),
               SelectItensAnalise(codigoAnalise),
               int.Parse(sqlDataReader["estadoWebCheckList"].ToString()) == 0 ? false : true,
               int.Parse(sqlDataReader["estadoWebFichaAvaliacao"].ToString()) == 0 ? false : true,
               int.Parse(sqlDataReader["estadoWebQuestionario"].ToString()) == 0 ? false : true);

            return a;
        }

        /**
         * Insere uma nova analise na base de dados
         * Insere os itens da analise na tabela dos item_analise
         * Insere as zonas da analise na tabela dos zona_analise
         * @param cod_projecto O CodigoTipo do projecto em que faz parte a analise
         * @param a Analise que será inserida
         * @return long O código da análise que foi inserida
         */
        public static long InsertAnalise(long codigoProjecto, Analise a)
        {
            String query = "insert into analise values(" + codigoProjecto + ","
            + "CAST('" + a.Data.ToString("yyyyMMdd HH:mm:ss") + "' AS datetime)"
            + ",'" + a.Nome + "','" + a.Tipo + "'," + (a.EstadoWebCheckList ? 1 : 0) + "," + (a.EstadoWebFichaAvaliacao ? 1 : 0)
            + "," + (a.EstadoWebQuestionario ? 1 : 0) + ",1);" + " SELECT SCOPE_IDENTITY();";

            SqlDataReader sqlDataReader = CamadaDados.DataBaseCommunicator.DataBaseCommunicator.ReadData(query);

            sqlDataReader.Read();
            long cod = long.Parse(sqlDataReader[0].ToString());

            InsertZonasAnalise(a.Zonas, cod);
            InsertItensAnalise(a.Itens, cod);

            return cod;
        }

        /**
         * Elimina uma analise da base de dados
         * Elimina os itens da analise na tabela dos item_analise
         * Elimina as zonas da analise na tabela dos zona_analise
         * Elimina as Perguntas escalaResposta Respostas dos Formularios desta Analise (se existirem)
         * @param codigoAnalise O codigo da analise que sera eliminada
         */
        public static void DeleteAnalise(long codigoAnalise)
        {
            DeleteZonasAnalise(codigoAnalise);
            DeleteItemsAnalise(codigoAnalise);
            //deleteFormularios(codigoAnalise);
            String query = "delete * from analise where cod_analise = " + codigoAnalise + ";";

            CamadaDados.DataBaseCommunicator.DataBaseCommunicator.Query(query);

        }

        /**
         * Modifica a informacao da analise passada como argumento
         * @param a A analise que ira ser modificada
         */
        public static void UpdateAnalise(Analise a)
        {
            String query = "update analise set " + "dataAnalise = " + a.Data + "," + "nomeAnalise = "
                + a.Nome + "," + "tipoAnalise = " + a.Tipo + "," + "estadoWebCheckList = "
                + a.EstadoWebCheckList + "," + "estadoWebFichaAvaliacao = " + a.EstadoWebFichaAvaliacao + "," + "estadoWebQuestionario = "
                + a.EstadoWebQuestionario + "where cod_analise = " + a.Codigo + ";";

            CamadaDados.DataBaseCommunicator.DataBaseCommunicator.Query(query);
        }

        /**
         * Descativa a analise na base de dados
         * @param cod_projecto Código do projecto a ser desactivado
         * @param
         */
        public static void DesactivarAnalise(long codigoAnalise)
        {
            String query = "update analise set activo = 0 where cod_analise = " + codigoAnalise;
            CamadaDados.DataBaseCommunicator.DataBaseCommunicator.Query(query);
        }
        #endregion

        #region Zonas
        /**
         * Retorna as zonas/actividades/área comum que já foram adicionadas anteriormente
         * @return List<Zona> zonas/actividades/área comum
         */
        public static List<Zona> SelectZonas(string tipo)
        {
            String query = "select cod_zona, nome_zona from zona, zona_analise, analise where analise" +
                ".tipo = '" + tipo + "' and zona_analise.cod_analise = analise.cod_analise and zona_analise" +
                ".cod_zona = zona.cod_zona";
            SqlDataReader sqlDataReader = CamadaDados.DataBaseCommunicator.DataBaseCommunicator.ReadData(query);

            List<Zona> zonas = new List<Zona>();

            while (sqlDataReader.Read())
            {
                Zona zona = new Zona((long)sqlDataReader["cod_zona"], (String)sqlDataReader["nome_zona"]);
                zonas.Add(zona);
            }
            return zonas;
        }

        /**
         * Insere uma nova Zona na base de dados
         * @param z Zona que se pretende adicionar
         * @return long Código da zona que foi inserido
         */
        public static long InsertZona(string nome)
        {
            String query = "insert into zona values('" + nome + "');" +
                "SELECT SCOPE_IDENTITY();";

            SqlDataReader sqlDataReader = CamadaDados.DataBaseCommunicator.DataBaseCommunicator.ReadData(query);

            sqlDataReader.Read();
            return long.Parse(sqlDataReader[0].ToString());
        }

        /**
         * Remove uma Zona da base de dados
         * @param codigo CodigoTipo da zona
         */
        public static void deleteZona(long codZona)
        {
            String query = "delete * from zona where cod_zona = " + codZona + ";";

            CamadaDados.DataBaseCommunicator.DataBaseCommunicator.Query(query);
        }

        /**
         * Modifica uma zona
         * @param z Zona que se pretende modificar
         */
        public static void UpdateZona(Zona z)
        {
            String query = "update zona set nome_zona = '" + z.Nome + "' "
                + "where cod_zona = " + z.Codigo + ";";

            CamadaDados.DataBaseCommunicator.DataBaseCommunicator.Query(query);
        }
        #endregion

        #region Zonas-Analise
        /**
         * Retorna as Zonas de uma dada análise
         * @param codigoAnalise Código da análise
         * @return List<Zona> Zonas da análise
         */
        public static List<Zona> SelectZonasAnalise(long codigoAnalise)
        {
            String query = "select zona.cod_zona, nome_zona from zona_analise, zona where " +
                "zona_analise.cod_analise = " + codigoAnalise + " and zona_analise.cod_zona = zona.cod_zona;";
            SqlDataReader sqlDataReader = CamadaDados.DataBaseCommunicator.DataBaseCommunicator.ReadData(query);

            List<Zona> zonas = new List<Zona>();

            while (sqlDataReader.Read())
            {
                Zona zona = new Zona((long)sqlDataReader["cod_zona"], (String)sqlDataReader["nome_zona"]);
                zonas.Add(zona);

            }
            return zonas;
        }

        //Revisto
        public static void InsertZonasAnalise(List<Zona> zonas, long codigoAnalise)
        {
            foreach (Zona z in zonas)
            {
                String query = "insert into zona_analise values(" + z.Codigo + "," + codigoAnalise + ");";

                CamadaDados.DataBaseCommunicator.DataBaseCommunicator.Query(query);
            }

        }

        //Revisto
        public static void DeleteZonasAnalise(long codigoAnalise)
        {
            String query = "delete * from zona_analise where cod_analise = " + codigoAnalise + ";";
            CamadaDados.DataBaseCommunicator.DataBaseCommunicator.Query(query);
        }
        #endregion

        #region Itens
        //Revisto
        public static Dictionary<long, string> SelectItensDefault()
        {
            String query = "select cod_item, nome_item from item where default_item = 1;";
            SqlDataReader sqlDataReader = CamadaDados.DataBaseCommunicator.DataBaseCommunicator.ReadData(query);

            Dictionary<long, string> itens_default = new Dictionary<long, string>();

            while (sqlDataReader.Read())
                itens_default.Add((long)sqlDataReader["cod_item"], (string)sqlDataReader["nome_item"]);

            return itens_default;
        }

        //Revisto
        public static Dictionary<long, string> SelectAllItens()
        {
            String query = "select cod_item, nome_item from item order by default_item DESC;";
            SqlDataReader sqlDataReader = CamadaDados.DataBaseCommunicator.DataBaseCommunicator.ReadData(query);

            Dictionary<long, string> itens = new Dictionary<long, string>();

            while (sqlDataReader.Read())
                itens.Add((long)sqlDataReader["cod_item"], (string)sqlDataReader["nome_item"]);

            return itens;
        }

        //Revisto
        public static long insertItem(string nome_item)
        {
            String query = "insert into item values('" + nome_item + "',0); " +
                           "SELECT SCOPE_IDENTITY();";

            SqlDataReader sqlDataReader = CamadaDados.DataBaseCommunicator.DataBaseCommunicator.ReadData(query);

            sqlDataReader.Read();
            return long.Parse(sqlDataReader[0].ToString());
        }

        //Revisto
        public static void DeleteItem(long codItem)
        {
            String query = "delete * from item where cod_item = " + codItem + ";";

            CamadaDados.DataBaseCommunicator.DataBaseCommunicator.Query(query);
        }

        //Revisto
        public static void UpdateItem(long codItem, string novo_nome)
        {
            String query = "update item set nome_item = '" + novo_nome + "' "
                + "where cod_item = " + codItem + ";";

            CamadaDados.DataBaseCommunicator.DataBaseCommunicator.Query(query);
        }
        #endregion

        #region Itens-Analise
        //Revisto
        public static List<Item> SelectItensAnalise(long codigoAnalise)
        {
            String query = "select item.cod_item, nome_item, default_item, ponderacao_analista, " +
                "ponderacao_profissional, ponderacao_cliente, inter_vermelho, inter_laranja, inter_amarelo, " +
                "inter_verdelima, inter_verde, limite_inferior_analista from item_analise, item where cod_analise = " +
                codigoAnalise + " and item_analise.cod_item = item.cod_item ;";
            SqlDataReader sqlDataReader = CamadaDados.DataBaseCommunicator.DataBaseCommunicator.ReadData(query);

            List<Item> items = new List<Item>();

            while (sqlDataReader.Read())
            {
                Item item = new Item((long)sqlDataReader["cod_item"],
                    sqlDataReader["nome_item"].ToString(),
                    sqlDataReader["default_item"].ToString() == "True" ? 1 : 0,
                    double.Parse(sqlDataReader["ponderacao_analista"].ToString()),
                    double.Parse(sqlDataReader["ponderacao_profissional"].ToString()),
                    double.Parse(sqlDataReader["ponderacao_cliente"].ToString()),
                    double.Parse(sqlDataReader["inter_vermelho"].ToString()),
                    double.Parse(sqlDataReader["inter_laranja"].ToString()),
                    double.Parse(sqlDataReader["inter_amarelo"].ToString()),
                    double.Parse(sqlDataReader["inter_verdelima"].ToString()),
                    double.Parse(sqlDataReader["inter_verde"].ToString()),
                    double.Parse(sqlDataReader["limite_inferior_analista"].ToString()));
                items.Add(item);
            }
            return items;
        }

        //Revisto
        public static void InsertItensAnalise(List<Item> itens, long codigoAnalise)
        {
            foreach (Item i in itens)
            {
                double b = i.PonderacaoAnalista;
                String query = "insert into item_analise values(" + 
                    i.CodigoItem + "," + 
                    codigoAnalise + "," + 
                    (i.PonderacaoAnalista * 1000).ToString("0,000") + "," + 
                    (i.PonderacaoProfissional * 1000).ToString("0,000") + "," + 
                    (i.PonderacaoCliente * 1000).ToString("0,000") + "," + 
                    (i.IntervaloVermelho * 1000).ToString("0,000") + "," + 
                    (i.IntervaloLaranja * 1000).ToString("0,000") + "," + 
                    (i.IntervaloAmarelo * 1000).ToString("0,000") + "," +
                    (i.IntervaloVerdeLima * 1000).ToString("0,000") + "," + 
                    (i.IntervaloVerde * 1000).ToString("0,000") + "," +
                    (i.LimiteInferiorAnalista * 1000).ToString("0,000") + ");";

                CamadaDados.DataBaseCommunicator.DataBaseCommunicator.Query(query);
            }
        }

        //Revisto
        public static void DeleteItemsAnalise(long codigoAnalise)
        {
            String query = "delete * from item_analise where cod_analise = " + codigoAnalise + ";";
            CamadaDados.DataBaseCommunicator.DataBaseCommunicator.Query(query);
        }

        //Revisto
        public static void UpdateItemAnalise(Item i)
        {
            String query = "update item_analise set " + 
                "ponderacao_analista = " + (i.PonderacaoAnalista * 1000).ToString("0,000") + "," + 
                "ponderacao_profissional = " + (i.PonderacaoProfissional * 1000).ToString("0,000") + "," + 
                "ponderacao_cliente = " + (i.PonderacaoCliente * 1000).ToString("0,000") + "," + 
                "inter_vermelho = " + (i.IntervaloVermelho * 1000).ToString("0,000") + "," + 
                "inter_laranja = " + (i.IntervaloLaranja * 1000).ToString("0,000") + "," + 
                "inter_amarelo = " + (i.IntervaloAmarelo * 1000).ToString("0,000") + "," +
                "inter_verdelima = " + (i.IntervaloVerdeLima * 1000).ToString("0,000") + "," + 
                "inter_verde = " + (i.IntervaloVerde * 1000).ToString("0,000") + "," +
                "limite_inferior_analista = " + (i.LimiteInferiorAnalista * 1000).ToString("0,000") + " " + 
                "where cod_item = " + i.CodigoItem + ";";

            CamadaDados.DataBaseCommunicator.DataBaseCommunicator.Query(query);
        }
        #endregion

        #region Questionario
        static public long InsertQuestionario(Questionario questionario)
        {
            string query = "INSERT INTO questionario VALUES (" + questionario.CodigoAnalise + ");" +
                "SELECT SCOPE_IDENTITY();";

            SqlDataReader sqlDataReader = CamadaDados.DataBaseCommunicator.DataBaseCommunicator.ReadData(query);

            sqlDataReader.Read();
            return long.Parse(sqlDataReader[0].ToString());

        }

        static public List<long> getCodsQuestionrarios(long codAnalise)
        {
            List<long> cods = new List<long>();

            string query = "Select cod_questionario from questionario where cod_analise = " + codAnalise.ToString();

            SqlDataReader sqlDataReader = CamadaDados.DataBaseCommunicator.DataBaseCommunicator.ReadData(query);

            while (sqlDataReader.Read())
            {
                long cod = (long)sqlDataReader["cod_questionario"];
                cods.Add(cod);
            }

            return cods;
        }

        static public Questionario GetQuestionario(long codQuestionario, long codAnalise)
        {
            Questionario q = new Questionario(codQuestionario, codAnalise);

            #region Respostas numero

            string query = "select resposta_questionario_numero.cod_zona, resposta_questionario_numero.numero_pergunta, " +
            "resposta_questionario_numero.valor, resposta_questionario_numero.cod_pergunta_questionario, pergunta_questionario.cod_item, " +
            "TipoEscala.numeroEscalaResposta " + 
            "from resposta_questionario_numero, pergunta_questionario, tipoescala " +
            "where resposta_questionario_numero.cod_questionario = " + codQuestionario.ToString() +
            " and resposta_questionario_numero.cod_analise = " + codAnalise.ToString() +
            " and pergunta_questionario.cod_pergunta_questionario = resposta_questionario_numero.cod_pergunta_questionario" +
            " and pergunta_questionario.cod_tipoescala = tipoescala.cod_tipoescala";

            SqlDataReader sqlDataReader = CamadaDados.DataBaseCommunicator.DataBaseCommunicator.ReadData(query);

            List<float> pergunta = new List<float>();
            while (sqlDataReader.Read())
            {
                float numero_pergunta = (float)sqlDataReader["numero_pergunta"];
                long cod_item = sqlDataReader["cod_item"].ToString() == "" ? -1 : (long)sqlDataReader["cod_item"];
                long cod_zona = long.Parse(sqlDataReader["cod_zona"].ToString());
                short valor = short.Parse(sqlDataReader["valor"].ToString());
                long cod_pergunta = long.Parse(sqlDataReader["cod_pergunta_questionario"].ToString());

                if (!pergunta.Contains((float)sqlDataReader["numero_pergunta"]) || 
                    (pergunta.Contains((float)sqlDataReader["numero_pergunta"]) && (int)sqlDataReader["numeroEscalaResposta"]==-2)
                    )
                {
                    if (!pergunta.Contains((float)sqlDataReader["numero_pergunta"]))
                        pergunta.Add((float)sqlDataReader["numero_pergunta"]);

                    Resposta resposta;
                    if ((int)sqlDataReader["numeroEscalaResposta"] == -2)
                    {
                        resposta = new Resposta(codAnalise, -1, codQuestionario, -1, numero_pergunta,
                       cod_item, cod_zona, valor, null, 3, Resposta.TipoResposta.RespostaNum);
                    }
                    else
                    {
                        resposta = new Resposta(codAnalise, -1, codQuestionario, -1, numero_pergunta,
                        cod_item, 1, valor, null, 3, Resposta.TipoResposta.RespostaNum);
                    }
                    resposta.CodigoPergunta = cod_pergunta;
                    q.AdicionarRespostaNumero(resposta);
                }
            }

            #endregion

            #region Respostas string

            query = "select resposta_questionario_string.cod_zona, resposta_questionario_string.numero_pergunta, " +
            "resposta_questionario_string.valor, resposta_questionario_string.cod_pergunta_questionario, pergunta_questionario.cod_item " +
            "from resposta_questionario_string, pergunta_questionario " +
            "where resposta_questionario_string.cod_questionario = " + codQuestionario.ToString() +
            " and resposta_questionario_string.cod_analise = " + codAnalise.ToString() +
            " and pergunta_questionario.cod_pergunta_questionario = resposta_questionario_string.cod_pergunta_questionario";

            sqlDataReader = CamadaDados.DataBaseCommunicator.DataBaseCommunicator.ReadData(query);

            while (sqlDataReader.Read())
            {
                float numero_pergunta = (float)sqlDataReader["numero_pergunta"];
                long cod_item = sqlDataReader["cod_item"].ToString() == "" ? -1 : (long)sqlDataReader["cod_item"];
                long cod_zona = long.Parse(sqlDataReader["cod_zona"].ToString());
                string valor = sqlDataReader["valor"].ToString();
                long cod_pergunta = long.Parse(sqlDataReader["cod_pergunta_questionario"].ToString());

                Resposta resposta = new Resposta(codAnalise, -1, codQuestionario, -1, numero_pergunta,
                    cod_item, cod_zona, 0, valor, 3, Resposta.TipoResposta.RespostaStr);

                resposta.CodigoPergunta = cod_pergunta;
                q.AdicionarRespostaString(resposta);
            }

            #endregion

            #region Respostas memo

            query = "select resposta_questionario_memo.cod_zona, resposta_questionario_memo.numero_pergunta, " +
            "resposta_questionario_memo.valor, resposta_questionario_memo.cod_pergunta_questionario, pergunta_questionario.cod_item " +
            "from resposta_questionario_memo, pergunta_questionario " +
            "where resposta_questionario_memo.cod_questionario = " + codQuestionario.ToString() +
            " and resposta_questionario_memo.cod_analise = " + codAnalise.ToString() +
            " and pergunta_questionario.cod_pergunta_questionario = resposta_questionario_memo.cod_pergunta_questionario";

            sqlDataReader = CamadaDados.DataBaseCommunicator.DataBaseCommunicator.ReadData(query);

            while (sqlDataReader.Read())
            {
                float numero_pergunta = (float)sqlDataReader["numero_pergunta"];
                long cod_item = sqlDataReader["cod_item"].ToString() == "" ? -1 : (long)sqlDataReader["cod_item"];
                long cod_zona = long.Parse(sqlDataReader["cod_zona"].ToString());
                string valor = sqlDataReader["valor"].ToString();
                long cod_pergunta = long.Parse(sqlDataReader["cod_pergunta_questionario"].ToString());

                Resposta resposta = new Resposta(codAnalise, -1, codQuestionario, -1, numero_pergunta,
                    cod_item, cod_zona, 0, valor, 3, Resposta.TipoResposta.RespostaMemo);

                resposta.CodigoPergunta = cod_pergunta;
                q.AdicionarRespostaMemo(resposta);
            }

            #endregion

            return q;
        }

        #endregion

        #region Ficha Avaliacao
        static public long InsertFichaAvaliacao(FichaAvaliacao fichaAvaliacao)
        {
            string query = "INSERT INTO ficha_avaliacao VALUES (" + fichaAvaliacao.CodigoAnalise + "," + fichaAvaliacao.CodigoZona + ");" +
                "SELECT SCOPE_IDENTITY();";

            SqlDataReader sqlDataReader = CamadaDados.DataBaseCommunicator.DataBaseCommunicator.ReadData(query);

            sqlDataReader.Read();
            return long.Parse(sqlDataReader[0].ToString());
        }

        static public Dictionary<long, long> GetCodigosFichaAvaliacao(long codAnalise)
        {
            Dictionary<long,long> cods = new Dictionary<long,long>();

            string query = "Select cod_fichaAvaliacao, cod_zona from ficha_avaliacao where cod_analise = " + codAnalise.ToString();

            SqlDataReader sqlDataReader = CamadaDados.DataBaseCommunicator.DataBaseCommunicator.ReadData(query);

            while (sqlDataReader.Read())
            {
                long codfa = (long)sqlDataReader["cod_fichaAvaliacao"];
                long codz = (long)sqlDataReader["cod_zona"];
                cods.Add(codfa,codz);
            }

            return cods;
        }

        static public FichaAvaliacao GetFichaAvaliacao(long codFichaAvaliacao, long codAnalise, long codZona)
        {
            FichaAvaliacao fa = new FichaAvaliacao(codFichaAvaliacao, codAnalise, codZona);

            #region Respostas numero

            string query = "select resposta_ficha_avaliacao_numero.numero_pergunta, resposta_ficha_avaliacao_numero.valor, "+
                "pergunta_ficha_avaliacao.cod_item " +
                "from resposta_ficha_avaliacao_numero, pergunta_ficha_avaliacao "+
                "where resposta_ficha_avaliacao_numero.cod_fichaAvaliacao = " + codFichaAvaliacao +
                " and resposta_ficha_avaliacao_numero.cod_analise = " + codAnalise + 
                " and pergunta_ficha_avaliacao.numero_pergunta = resposta_ficha_avaliacao_numero.numero_pergunta" +
                " and pergunta_ficha_avaliacao.cod_analise = resposta_ficha_avaliacao_numero.cod_analise";

            SqlDataReader sqlDataReader = CamadaDados.DataBaseCommunicator.DataBaseCommunicator.ReadData(query);

            while (sqlDataReader.Read())
            {
                long cod_item = (long)sqlDataReader["cod_item"];
                short valor = short.Parse(sqlDataReader["valor"].ToString());
                float numero_pergunta = float.Parse(sqlDataReader["numero_pergunta"].ToString());

                Resposta resposta = new Resposta(codAnalise, -1, -1, codFichaAvaliacao, numero_pergunta,
                    cod_item, 0, valor, null, 2, Resposta.TipoResposta.RespostaNum);

                fa.adicionarRespostaNumero(resposta);
            }

            #endregion

            #region Respostas string

            query = "select resposta_ficha_avaliacao_string.numero_pergunta, resposta_ficha_avaliacao_string.valor, " +
                "pergunta_ficha_avaliacao.cod_item " +
                "from resposta_ficha_avaliacao_string, pergunta_ficha_avaliacao " +
                "where resposta_ficha_avaliacao_string.cod_fichaAvaliacao = " + codFichaAvaliacao +
                " and resposta_ficha_avaliacao_string.cod_analise = " + codAnalise +
                " and pergunta_ficha_avaliacao.numero_pergunta = resposta_ficha_avaliacao_string.numero_pergunta" +
                " and pergunta_ficha_avaliacao.cod_analise = resposta_ficha_avaliacao_string.cod_analise";

            sqlDataReader = CamadaDados.DataBaseCommunicator.DataBaseCommunicator.ReadData(query);

            while (sqlDataReader.Read())
            {
                long cod_item = (long)sqlDataReader["cod_item"];
                string valor = sqlDataReader["valor"].ToString();
                float numero_pergunta = float.Parse(sqlDataReader["numero_pergunta"].ToString());

                Resposta resposta = new Resposta(codAnalise, -1, -1, codFichaAvaliacao, numero_pergunta,
                    cod_item, 0, 0, valor, 2, Resposta.TipoResposta.RespostaStr);

                fa.adicionarRespostaString(resposta);
            }

            #endregion

            return fa;
        }
        #endregion

        #region CheckList
        static public long InsertRespostaCheckList(Resposta resposta)
        {
            string query = "INSERT INTO resposta_checkList VALUES (" +
                        resposta.CodigoAnalise + ", " + resposta.CodigoZona + ", " +
                        resposta.CodigoItem + ", " + resposta.Valor + ");" +
                        "SELECT SCOPE_IDENTITY();";

            //MessageBox.Show(Query.ToString());

            SqlDataReader sqlDataReader = CamadaDados.DataBaseCommunicator.DataBaseCommunicator.ReadData(query);

            sqlDataReader.Read();
            return long.Parse(sqlDataReader[0].ToString());
        }

        static public CheckList GetCheckList(long codAnalise)
        {
            CheckList cl = new CheckList(codAnalise);

            string query = "select cod_zona, cod_item, valor from resposta_checkList";

            SqlDataReader sqlDataReader = CamadaDados.DataBaseCommunicator.DataBaseCommunicator.ReadData(query);

            while (sqlDataReader.Read())
            {
                long cod_item = long.Parse(sqlDataReader["cod_item"].ToString());
                long cod_zona = long.Parse(sqlDataReader["cod_zona"].ToString());
                short valor = short.Parse(sqlDataReader["valor"].ToString());

                Resposta resposta = new Resposta(codAnalise, -1, -1, -1, -1,cod_item, 
                    cod_zona, valor, null, 1, Resposta.TipoResposta.RespostaNum);

                cl.adicionarRespostaNumero(resposta);
            }

            return cl;
        }
        #endregion

        #region Respostas

        #region Respostas CheckList
        static public void SelectRespostaCheckList(long codigoAnalise, List<Resposta> respostas)
        {
            SqlDataReader sqlDataReaderZona, sqlDataReaderItem, sqlDataReaderesposta;

            sqlDataReaderZona = DataBaseCommunicator.ReadData("SELECT Zona_analise.cod_zona FROM Zona_Analise WHERE Zona_Analise.cod_analise=" + codigoAnalise);

            while (sqlDataReaderZona.Read())
            {
                sqlDataReaderItem = DataBaseCommunicator.ReadData("SELECT Item.cod_item " +
                                                               "FROM Item, Item_Analise " +
                                                               "WHERE Item.cod_item = Item_Analise.cod_item " +
                                                                    "AND Item_Analise.cod_analise=" + codigoAnalise);
                while (sqlDataReaderItem.Read())
                {
                    sqlDataReaderesposta = DataBaseCommunicator.ReadData("SELECT cod_resposta_checkList, cod_analise, cod_zona, cod_item, valor " +
                                                                        "FROM resposta_checkList " +
                                                                        "WHERE cod_analise=" + codigoAnalise +
                                                                            " AND cod_zona= " + sqlDataReaderZona["cod_zona"] +
                                                                            " AND cod_item= " + sqlDataReaderItem["cod_item"]);
                    while (sqlDataReaderesposta.Read())
                    {
                        if (sqlDataReaderesposta["valor"].ToString() != "")
                        {
                            //Console.WriteLine("CHECKLIST: codigoAnalise " + codigoAnalise + "codr " + (long)sqlDataReadeResposta["cod_resposta_checkList"] + "-1" + "-1" + "-1" + (long)sqlDataReaderItem["codigoItem"] + (long)sqlDataReaderZona["codigo"] + short.Parse(sqlDataReadeResposta["valor"].ToString()) + " " + 1 + Classes.Resposta.TipoResposta.RespostaNum);
                            respostas.Add(new Resposta(codigoAnalise, (long)sqlDataReaderesposta["cod_resposta_checkList"], -1, -1, -1, (long)sqlDataReaderItem["cod_item"], (long)sqlDataReaderZona["cod_zona"], short.Parse(sqlDataReaderesposta["valor"].ToString()), "", 1, Classes.Resposta.TipoResposta.RespostaNum));
                        }
                    }
                    sqlDataReaderesposta.Close();
                }
                sqlDataReaderItem.Close();
            }
            sqlDataReaderZona.Close();
        }
        #endregion

        #region Respostas Ficha de Avaliação
        static public void SelectRespostaFichaAvaliacao(long codigoAnalise, List<Resposta> respostas)
        {
            SqlDataReader sqlDataReaderPergunta, sqlDataReadeResposta;

            sqlDataReaderPergunta = DataBaseCommunicator.ReadData("SELECT ficha_avaliacao.cod_fichaAvaliacao " +
                                                "FROM  ficha_avaliacao " +
                                                "WHERE ficha_avaliacao.cod_analise=" + codigoAnalise );
            while (sqlDataReaderPergunta.Read())
            {
                sqlDataReadeResposta = DataBaseCommunicator.ReadData("SELECT resposta_ficha_avaliacao_numero.numero_pergunta, pergunta_ficha_avaliacao.cod_item, resposta_ficha_avaliacao_numero.cod_fichaAvaliacao, ficha_avaliacao.cod_zona, valor " +
                                                                   " FROM  resposta_ficha_avaliacao_numero, pergunta_ficha_avaliacao, ficha_avaliacao " +
                                                                   " WHERE resposta_ficha_avaliacao_numero.cod_fichaAvaliacao=" + sqlDataReaderPergunta["cod_fichaAvaliacao"] +
                                                                   " AND resposta_ficha_avaliacao_numero.cod_fichaAvaliacao = ficha_avaliacao.cod_fichaAvaliacao" +
                                                                   " AND pergunta_ficha_avaliacao.numero_pergunta = resposta_ficha_avaliacao_numero.numero_pergunta");
                while (sqlDataReadeResposta.Read())
                {
                    //Console.WriteLine("FICHA AVALIACAO:" + codigoAnalise + "/-1" + "/-1" + "/" + (long)sqlDataReadeResposta["codigoFichaAvaliacao"] + "/" + short.Parse(sqlDataReadeResposta["numeroPergunta"].ToString()) + "/" + (long)sqlDataReadeResposta["codigoItem"] + "/" + (long)sqlDataReadeResposta["codigo"] + "/" + short.Parse(sqlDataReadeResposta["valor"].ToString()) + "/" + "" + "/" + 2 + "/" + Classes.Resposta.TipoResposta.RespostaNum);
                    respostas.Add(new Resposta(codigoAnalise, -1, -1, (long)sqlDataReadeResposta["cod_fichaAvaliacao"], short.Parse(sqlDataReadeResposta["numero_pergunta"].ToString()), (long)sqlDataReadeResposta["cod_item"], (long)sqlDataReadeResposta["cod_zona"], short.Parse(sqlDataReadeResposta["valor"].ToString()), "", 2, Classes.Resposta.TipoResposta.RespostaNum));
                }
                sqlDataReadeResposta.Close();

                sqlDataReadeResposta = DataBaseCommunicator.ReadData("SELECT resposta_ficha_avaliacao_string.numero_pergunta, resposta_ficha_avaliacao_string.cod_fichaAvaliacao, ficha_avaliacao.cod_zona, valor " +
                                                                   " FROM resposta_ficha_avaliacao_string, ficha_avaliacao " +
                                                                   " WHERE resposta_ficha_avaliacao_string.cod_fichaAvaliacao=" + sqlDataReaderPergunta["cod_fichaAvaliacao"] +
                                                                   " AND resposta_ficha_avaliacao_string.cod_fichaAvaliacao = ficha_avaliacao.cod_fichaAvaliacao");
                
                if (sqlDataReadeResposta.Read())
                    respostas.Add(new Resposta(codigoAnalise, -1, -1, int.Parse(sqlDataReadeResposta["cod_fichaAvaliacao"].ToString()), -1, -1, int.Parse(sqlDataReadeResposta["cod_zona"].ToString()), -1, sqlDataReadeResposta["valor"].ToString(), 2, Classes.Resposta.TipoResposta.RespostaMemo));
                sqlDataReadeResposta.Close();
            }
        }
        #endregion

		#region Inserir Respostas Ficha de Avaliacao
		static public long InsertRespostaFichaAvaliacao(Resposta resposta)
        {
            string query;
            switch (resposta.GetTipoResposta)
            {
                case Resposta.TipoResposta.RespostaNum:
                    query = "INSERT INTO resposta_ficha_avaliacao_numero VALUES (" +
                        resposta.CodigoFichaAvaliacao + ", " + resposta.CodigoAnalise + ", " + resposta.NumeroPergunta + ", " +
                         + resposta.Valor + ");" + 
                        "SELECT SCOPE_IDENTITY();";
                    break;
                default:
                    query = "INSERT INTO resposta_ficha_avaliacao_string VALUES (" +
                        resposta.CodigoFichaAvaliacao + ", " + resposta.CodigoAnalise + ", " + resposta.NumeroPergunta + ",'" +
                         + resposta.Valor + "');" + 
                        "SELECT SCOPE_IDENTITY();";
                    break;
            }

            SqlDataReader sqlDataReader = CamadaDados.DataBaseCommunicator.DataBaseCommunicator.ReadData(query);

            sqlDataReader.Read();
            return long.Parse(sqlDataReader[0].ToString());
        }
        #endregion

        #region Respostas Questionario
        static public void SelectRespostaQuestionario(long codigoAnalise, List<Resposta> respostas)
        {
            SqlDataReader sqlDataReader, sqlDataReaderesposta;
            int cod_item, cod_zona;

            sqlDataReader = DataBaseCommunicator.ReadData("SELECT cod_pergunta_questionario, numero_pergunta, TipoEscala.numeroEscalaResposta, cod_zona, cod_item " +
                                                "FROM pergunta_questionario, TipoEscala " +
                                                "WHERE pergunta_questionario.cod_tipoEscala=TipoEscala.cod_tipoEscala " +
                                                "AND pergunta_questionario.cod_analise=" + codigoAnalise);


            while (sqlDataReader.Read())
            {
                if (sqlDataReader["cod_item"].ToString() == "")
                    cod_item = -1;
                else
                    cod_item = int.Parse(sqlDataReader["cod_item"].ToString());

                if (sqlDataReader["cod_zona"].ToString() == "")
                    cod_zona = -1;
                else
                    cod_zona = int.Parse(sqlDataReader["cod_zona"].ToString());

                //String
                if (int.Parse(sqlDataReader["numeroEscalaResposta"].ToString()) == 0)
                {
                    sqlDataReaderesposta = DataBaseCommunicator.ReadData("SELECT cod_questionario, valor, resposta_questionario_string.cod_zona, resposta_questionario_string.cod_zona,resposta_questionario_string.numero_pergunta " +
                                                                       " FROM resposta_questionario_string, pergunta_questionario " +
                                                                       " WHERE resposta_questionario_string.numero_pergunta=" + sqlDataReader["numero_pergunta"].ToString() +
                                                                       " AND resposta_questionario_string.numero_pergunta=pergunta_questionario.numero_pergunta" +
                                                                       " AND resposta_questionario_string.cod_analise=" + codigoAnalise);
                    while (sqlDataReaderesposta.Read())
                    {
                        //Console.WriteLine("QUESTIONARIO:" + codigoAnalise + "-1" + int.Parse(sqlDataReadeResposta["codigoQuestionario"].ToString()) + "-1" + int.Parse(sqlDataReader["numeroPergunta"].ToString()) + codigoItem + codigo + "-1" + sqlDataReadeResposta["valor"].ToString() + 3 + Classes.Resposta.TipoResposta.RespostaStr);
                        respostas.Add(new Resposta(codigoAnalise, -1, int.Parse(sqlDataReaderesposta["cod_questionario"].ToString()), -1, int.Parse(sqlDataReader["numero_pergunta"].ToString()), cod_item, int.Parse(sqlDataReaderesposta["cod_zona"].ToString()), -1, sqlDataReaderesposta["valor"].ToString(), 3, Classes.Resposta.TipoResposta.RespostaStr));
                    }

                }
                //memo
                else if (int.Parse(sqlDataReader["numeroEscalaResposta"].ToString()) == -1)
                {
                    sqlDataReaderesposta = DataBaseCommunicator.ReadData("SELECT resposta_questionario_memo.cod_questionario, resposta_questionario_memo.valor, resposta_questionario_memo.cod_zona,resposta_questionario_memo.numero_pergunta " +
                                                                       " FROM resposta_questionario_memo, pergunta_questionario " +
                                                                       " WHERE resposta_questionario_memo.numero_pergunta=" + sqlDataReader["numero_pergunta"].ToString() +
                                                                       " AND resposta_questionario_memo.numero_pergunta=pergunta_questionario.numero_pergunta" +
                                                                       " AND resposta_questionario_string.cod_analise=" + codigoAnalise);
                    while (sqlDataReaderesposta.Read())
                    {
                        respostas.Add(new Resposta(codigoAnalise, -1, int.Parse(sqlDataReaderesposta["cod_questionario"].ToString()), -1, int.Parse(sqlDataReader["numero_pergunta"].ToString()), cod_item, int.Parse(sqlDataReaderesposta["cod_zona"].ToString()), -1, sqlDataReaderesposta["valor"].ToString(), 3, Classes.Resposta.TipoResposta.RespostaMemo));
                    }
                    sqlDataReaderesposta.Close();
                }
                //numero
                else
                {
                    string numperg = sqlDataReader["numero_pergunta"].ToString();
                    float num1 = (float.Parse(numperg));
                    int num2 = (int)num1;

                    string num_perg;
                    if (num1 > num2)
                        num_perg = numperg.Split(',')[0] + "." + numperg.Split(',')[1];
                    else 
                        num_perg = numperg;

                    sqlDataReaderesposta = DataBaseCommunicator.ReadData("SELECT resposta_questionario_numero.cod_questionario, resposta_questionario_numero.valor, resposta_questionario_numero.cod_zona, resposta_questionario_numero.numero_pergunta " +
                                                                       " FROM resposta_questionario_numero, pergunta_questionario " +
                                                                       " WHERE resposta_questionario_numero.numero_pergunta=" + num_perg +
                                                                       " AND resposta_questionario_numero.numero_pergunta=pergunta_questionario.numero_pergunta" +
                                                                       " AND resposta_questionario_numero.cod_analise=" + codigoAnalise);

                    while (sqlDataReaderesposta.Read())
                    {
                        if (sqlDataReaderesposta["valor"].ToString() != "" && sqlDataReader["numero_pergunta"].ToString() != "")
                        {
                            String a = sqlDataReaderesposta["cod_questionario"].ToString();
                            String b = numperg;
                            String c = sqlDataReaderesposta["cod_zona"].ToString();
                            String d = sqlDataReaderesposta["valor"].ToString();

                            //Console.WriteLine("QUESTIONARIO:" + codigoAnalise + -1 + long.Parse(sqlDataReadeResposta["codigoQuestionario"].ToString()) + -1 + long.Parse(sqlDataReader["numeroPergunta"].ToString()) + codigoItem + codigo + short.Parse(sqlDataReadeResposta["valor"].ToString()) + "" + 3 + Classes.Resposta.TipoResposta.RespostaNum);
                            respostas.Add(new Resposta(codigoAnalise, -1, long.Parse(sqlDataReaderesposta["cod_questionario"].ToString()), -1, float.Parse(numperg), cod_item, long.Parse(sqlDataReaderesposta["cod_zona"].ToString()), short.Parse(sqlDataReaderesposta["valor"].ToString()), "", 3, Classes.Resposta.TipoResposta.RespostaNum));
                        }
                    }
                    sqlDataReaderesposta.Close();
                }

            }
            sqlDataReader.Close();
        }
        #endregion

        #region Inserir Resposta Questionário
        static public long InsertRespostaQuestionario(Resposta resposta)
        {
            string query;
            switch (resposta.GetTipoResposta)
            {
                case Resposta.TipoResposta.RespostaNum:
                    query = "INSERT INTO resposta_questionario_numero VALUES (" +
                        resposta.CodigoQuestionario + ", " + resposta.CodigoAnalise + ", " + resposta.CodigoZona + ", " +
                        ((resposta.NumeroPergunta > (int)resposta.NumeroPergunta) ? (resposta.NumeroPergunta.ToString().Split(',')[0] + "." + resposta.NumeroPergunta.ToString().Split(',')[1]) : resposta.NumeroPergunta.ToString()) + ", " + 
                        resposta.Valor + ", " + resposta.CodigoPergunta + ");" + "SELECT SCOPE_IDENTITY();";
                    break;
                case Resposta.TipoResposta.RespostaStr:
                    query = "INSERT INTO resposta_questionario_string VALUES (" +
                        resposta.CodigoQuestionario + ", " + resposta.CodigoAnalise + ", " + resposta.CodigoZona + ", " +
                        resposta.NumeroPergunta + ", '" + resposta.ValorString + "', " + resposta.CodigoPergunta + ");" +
                        "SELECT SCOPE_IDENTITY();";
                    break;
                default:
                    query = "INSERT INTO resposta_questionario_memo VALUES (" +
                        resposta.CodigoQuestionario + ", " + resposta.CodigoAnalise + ", " + resposta.CodigoZona + ", " +
                        resposta.NumeroPergunta + ", '" + resposta.ValorString + "', " + resposta.CodigoPergunta + ");" +
                        "SELECT SCOPE_IDENTITY();";
                    break;
            }

            SqlDataReader sqlDataReader = CamadaDados.DataBaseCommunicator.DataBaseCommunicator.ReadData(query);

            sqlDataReader.Read();
            long cod = long.Parse(sqlDataReader[0].ToString());
            sqlDataReader.Close();
            return cod;
        }
        #endregion

        #region Select Resposta Sexo
        static public List<int> SelectRespostasSexo (long codigoAnalise)
        {
            SqlDataReader sqlDataReader;

            sqlDataReader = DataBaseCommunicator.ReadData("SELECT valor, cod_resposta_questionario_numero " +
                                                "FROM pergunta_questionario, resposta_questionario_numero " +
                                                "WHERE pergunta_questionario.cod_pergunta_questionario = resposta_questionario_numero.cod_pergunta_questionario " +
                                                "AND pergunta_questionario.numero_pergunta = 1 " +
                                                "AND pergunta_questionario.cod_analise=" + codigoAnalise);

            List<int> respostas = new List<int>();
            String aux = null;

            while (sqlDataReader.Read())
            {
                aux = sqlDataReader["valor"].ToString();
                int resposta = Convert.ToInt32(aux);
                respostas.Add(resposta);
            }
           
            return respostas;
        }

        #endregion

        #region Idade
        static public List<int> SelectIdades (long codigoAnalise)
        {
            List<int> idades = new List<int>();

            string query = "SELECT valor, cod_resposta_questionario_numero " +
                           "FROM pergunta_questionario, resposta_questionario_numero " +
                           "WHERE pergunta_questionario.cod_pergunta_questionario = resposta_questionario_numero.cod_pergunta_questionario " +
                           "AND pergunta_questionario.numero_pergunta = 16 " +
                           "AND pergunta_questionario.cod_analise=" + codigoAnalise; 
            
           SqlDataReader sqlDataReader = CamadaDados.DataBaseCommunicator.DataBaseCommunicator.ReadData(query);

           String aux = null;

           while (sqlDataReader.Read())
           {
               aux = sqlDataReader["valor"].ToString();
               int resposta = Convert.ToInt32(aux);
               //Console.WriteLine("idade :" + resposta);
               idades.Add(resposta);
           }

           return idades;
        }

        #endregion

        #region Cliente Habitual
        static public List<int> SelectRespostasHabitual(long codigoAnalise)
        {
            SqlDataReader sqlDataReader;

            sqlDataReader = DataBaseCommunicator.ReadData("SELECT valor, cod_resposta_questionario_numero " +
                                                "FROM pergunta_questionario, resposta_questionario_numero " +
                                                "WHERE pergunta_questionario.cod_pergunta_questionario = resposta_questionario_numero.cod_pergunta_questionario " +
                                                "AND pergunta_questionario.numero_pergunta = 2 " +
                                                "AND pergunta_questionario.cod_analise=" + codigoAnalise);

            List<int> respostas = new List<int>();
            String aux = null;

            while (sqlDataReader.Read())
            {
                aux = sqlDataReader["valor"].ToString();
                int resposta = Convert.ToInt32(aux);
                respostas.Add(resposta);
            }

            return respostas;
        }
        #endregion

        #region Escala Resposta
        public static List<EscalaResposta> SelectRespostas(long cod_tipoEscala)
        {
            List<EscalaResposta> resps = new List<EscalaResposta>();

            String query = "select * from EscalaResposta where cod_TipoEscala = "
                + cod_tipoEscala + ";";

            SqlDataReader sqlDataReader = CamadaDados.DataBaseCommunicator.DataBaseCommunicator.ReadData(query);

            while (sqlDataReader.Read())
            {
                EscalaResposta e = new EscalaResposta(
                    (long)sqlDataReader["cod_EscalaResposta"],
                    (long)sqlDataReader["cod_TipoEscala"],
                    (String)sqlDataReader["descricaoEscalaResposta"],
                    (int)sqlDataReader["valorEscalaResposta"]);

                resps.Add(e);
            }
            sqlDataReader.Close();
            return resps;
        }

        public static EscalaResposta selectEscalaResposta(long codEscala)
        {
            String query = "select * from EscalaResposta where cod_EscalaResposta = "
                + codEscala + ";";
            SqlDataReader sqlDataReader = CamadaDados.DataBaseCommunicator.DataBaseCommunicator.ReadData(query);

            EscalaResposta e = null;

            while (sqlDataReader.Read())
            {
                e = new EscalaResposta((long)sqlDataReader["cod_EscalaResposta"],
                    (long)sqlDataReader["cod_TipoEscala"],
                    (String)sqlDataReader["descricaoEscalaResposta"],
                    (int)sqlDataReader["valorEscalaResposta"]);
            }

            return e;
        }

        // usada 
        public static long insertEscalaResposta(EscalaResposta e)
        {
            String query = "insert into EscalaResposta values (" + 
                e.CodigoTipo + ",'" + 
                e.Descricao + "'," + 
                e.Valor + ");" +
                "SELECT SCOPE_IDENTITY();";
            //MessageBox.Show(Query);
            SqlDataReader sqlDataReader = CamadaDados.DataBaseCommunicator.DataBaseCommunicator.ReadData(query);

            sqlDataReader.Read();
            return long.Parse(sqlDataReader[0].ToString());
        }

        public static void deleteEscalaResposta(String codEscala)
        {
            String query = "delete * from EscalaResposta where cod_EscalaResposta = " +
                codEscala + ";";

            CamadaDados.DataBaseCommunicator.DataBaseCommunicator.Query(query);
        }

        public static void updateEscalaResposta(EscalaResposta e)
        {
            String query = "update EscalaResposta set cod_TipoEscala = " + e.CodigoTipo + ","
                + "descricaoEscalaResposta" + e.Descricao + ";" + "valorEscalaResposta" + e.Valor +
                ";" + "where cod_EscalaResposta = " + e.CodigoEscala + ";";

            CamadaDados.DataBaseCommunicator.DataBaseCommunicator.Query(query);
        }
        #endregion

        #region TipoEscala
        public static Dictionary<string, List<TipoEscala>> GetTiposResposta()
        {
            Dictionary<string, List<TipoEscala>> resps = new Dictionary<string, List<TipoEscala>>();

            string query = "select * from TipoEscala";
            SqlDataReader sqlDataReader = CamadaDados.DataBaseCommunicator.DataBaseCommunicator.ReadData(query);
            
            while (sqlDataReader.Read())
            {
                TipoEscala te = new TipoEscala(
                    (long)sqlDataReader["cod_tipoEscala"],
                    (string)sqlDataReader["tipoEscalaResposta"],
                    (int)sqlDataReader["numeroEscalaResposta"],
                    sqlDataReader["default_tipoEscala"].ToString() == "True" ? 1 : 0,
                    SelectRespostas((long)sqlDataReader["cod_tipoEscala"]));

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
            SqlDataReader sqlDataReader = CamadaDados.DataBaseCommunicator.DataBaseCommunicator.ReadData(query);

            TipoEscala t = null;

            while (sqlDataReader.Read())
            {
                t = new TipoEscala((long)sqlDataReader["cod_tipoEscala"],
                    (String)sqlDataReader["tipoEscalaResposta"],
                    (int)sqlDataReader["numeroEscalaResposta"],
                    sqlDataReader["default_tipoEscala"].ToString() == "true" ? 1 : 0,
                    SelectRespostas((long)sqlDataReader["cod_tipoEscala"]));
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

            SqlDataReader sqlDataReader = CamadaDados.DataBaseCommunicator.DataBaseCommunicator.ReadData(query);

            sqlDataReader.Read();
            return long.Parse(sqlDataReader[0].ToString());

        }
        #endregion

        #region NumeroEscala
        //Desvolve a cardinalidade das escala de reposta para um dada analise escalaResposta para um dada pergunta dum tipo de resposta
        static public int NumeroEscalaResposta(long analise, float num_pergunta, int tipoResposta)
        {
            SqlDataReader sqlDataReader, sqlDataReaderTipoEscala;

            //Questionario
            if (tipoResposta == 3)
            {
                /*Console.WriteLine("SELECT cod_tipoEscala " +
                                                        "FROM pergunta_questionario " +
                                                        "WHERE codigoAnalise=" + analise +
                                                            " AND numeroPergunta=" + num_pergunta);
                 */
                sqlDataReader = DataBaseCommunicator.ReadData("SELECT cod_tipoEscala " +
                                                        "FROM pergunta_questionario " +
                                                        "WHERE cod_analise=" + analise +
                                                            " AND numero_pergunta=" + num_pergunta);
            }
            //FIcha de Avalicao -if(tipoResposta==2)
            else
            {
                /*Console.WriteLine("SELECT cod_tipoEscala " +
                                                        "FROM pergunta_ficha_avaliacao " +
                                                        "WHERE codigoAnalise=" + analise +
                                                            " AND numeroPergunta=" + num_pergunta);
                 */
                sqlDataReader = DataBaseCommunicator.ReadData("SELECT cod_tipoEscala " +
                                                        "FROM pergunta_ficha_avaliacao " +
                                                        "WHERE cod_analise=" + analise +
                                                            " AND numero_pergunta=" + num_pergunta);
            }

            if (sqlDataReader.Read())
            {
               // if (sqlDataReader["cod_tipoEscala"].ToString()!="")
                //{
                    sqlDataReaderTipoEscala = DataBaseCommunicator.ReadData("SELECT numeroEscalaResposta " +
                                                                        "FROM TipoEscala " +
                                                                            "WHERE cod_tipoEscala=" + sqlDataReader["cod_tipoEscala"]);

                    sqlDataReader.Close();
                    if (sqlDataReaderTipoEscala.Read())
                        return (int.Parse(sqlDataReaderTipoEscala["numeroEscalaResposta"].ToString()));
               // }
            }
            return -1;
        }
        #endregion

        #endregion

        #region Perguntas

        public static long InsertPerguntaFichaAvaliacao(PerguntaFichaAvaliacao p)
        {
            StringBuilder a = new StringBuilder();
            a.Append("insert into pergunta_ficha_avaliacao VALUES (");
            a.Append(p.CodigoAnalise.ToString() + ",");
            a.Append(p.NumeroPergunta.ToString() + ",");
            a.Append(p.CodigoItem.ToString() + ",'");
            a.Append(p.Texto + "',");
            a.Append(p.CodigoTipoEscala.ToString() + ");");
            a.Append("SELECT SCOPE_IDENTITY();");

            string query = a.ToString();

            SqlDataReader sqlDataReader = CamadaDados.DataBaseCommunicator.DataBaseCommunicator.ReadData(query);

            sqlDataReader.Read();
            long cod = long.Parse(sqlDataReader[0].ToString());
            sqlDataReader.Close();
            return cod;
        }

        public static long InsertPerguntaQuestionario(PerguntaQuestionario p)
        {
            StringBuilder a = new StringBuilder();
            a.Append("insert into pergunta_questionario VALUES (");
            a.Append(p.CodigoAnalise.ToString() + ",");
            a.Append((p.NumeroPergunta > (int)p.NumeroPergunta) ? (p.NumeroPergunta.ToString().Split(',')[0] + "." + p.NumeroPergunta.ToString().Split(',')[1] + ",") : p.NumeroPergunta.ToString() + ",");
            a.Append(((p.CodigoZona==0)?"null":p.CodigoZona.ToString()) + ",");
            a.Append(((p.CodigoItem==-1)?"null":p.CodigoItem.ToString()) + ",");
            a.Append(p.CodigoTipoEscala.ToString() + ",'");
            a.Append(p.Texto + "','");
            a.Append(p.TipoQuestao + "');");
            a.Append("SELECT SCOPE_IDENTITY();");

            string query = a.ToString();

            SqlDataReader sqlDataReader = CamadaDados.DataBaseCommunicator.DataBaseCommunicator.ReadData(query);

            sqlDataReader.Read();
            long cod = long.Parse(sqlDataReader[0].ToString());
            sqlDataReader.Close();
            return cod;
        }

        public static bool ExisteFichaAvaliacao(long codigoAnalise)
        {
            string query = "select count(*) from pergunta_ficha_avaliacao where cod_analise = " + codigoAnalise + ";";

            SqlDataReader sqlDataReader = CamadaDados.DataBaseCommunicator.DataBaseCommunicator.ReadData(query);

            sqlDataReader.Read();
            return (int.Parse(sqlDataReader[0].ToString()) > 0) ? true : false; 
        }

        public static bool ExisteQuestionario(long codigoAnalise)
        {
            string query = "select count(*) from pergunta_questionario where cod_analise = " + codigoAnalise + ";";

            SqlDataReader sqlDataReader = CamadaDados.DataBaseCommunicator.DataBaseCommunicator.ReadData(query);

            sqlDataReader.Read();
            return (int.Parse(sqlDataReader[0].ToString()) > 0) ? true : false; 
        }

        public static bool DisponivelFichaAvaliacaoOnline(long codigoAnalise)
        {
            string query = "select estadoWebFichaAvaliacao from analise where cod_analise = " + codigoAnalise + ";";

            SqlDataReader sqlDataReader = CamadaDados.DataBaseCommunicator.DataBaseCommunicator.ReadData(query);

            sqlDataReader.Read();
            return (int.Parse(sqlDataReader[0].ToString()) == 1) ? true : false; 
        }

        public static bool DisponivelQuestionarioOnline(long codigoAnalise)
        {
            string query = "select estadoWebQuestionario from analise where cod_analise = " + codigoAnalise + ";";

            SqlDataReader sqlDataReader = CamadaDados.DataBaseCommunicator.DataBaseCommunicator.ReadData(query);

            sqlDataReader.Read();
            return (int.Parse(sqlDataReader[0].ToString()) == 1) ? true : false;
        }

        public static bool TemRespostasFichaAvaliacao(long codigoAnalise)
        {
            string query = "select count(*) from resposta_ficha_avaliacao_numero where cod_analise = " + codigoAnalise + ";";

            SqlDataReader sqlDataReader = CamadaDados.DataBaseCommunicator.DataBaseCommunicator.ReadData(query);

            sqlDataReader.Read();
            return (int.Parse(sqlDataReader[0].ToString()) > 0) ? true : false; 
        }

        public static bool TemRespostasQuestionario(long codigoAnalise)
        {
            bool b;
            string query = "select count(*) from resposta_questionario_numero where cod_analise = " + codigoAnalise + ";";

            SqlDataReader sqlDataReader = CamadaDados.DataBaseCommunicator.DataBaseCommunicator.ReadData(query);

            sqlDataReader.Read();
            b = (int.Parse(sqlDataReader[0].ToString()) > 0) ? true : false;
            if (b) return b;

            query = "select count(*) from resposta_questionario_string where cod_analise = " + codigoAnalise + ";";

            sqlDataReader = CamadaDados.DataBaseCommunicator.DataBaseCommunicator.ReadData(query);

            sqlDataReader.Read();
            b = (int.Parse(sqlDataReader[0].ToString()) > 0) ? true : false;
            if (b) return b;

            query = "select count(*) from resposta_questionario_memo where cod_analise = " + codigoAnalise + ";";

            sqlDataReader = CamadaDados.DataBaseCommunicator.DataBaseCommunicator.ReadData(query);

            sqlDataReader.Read();
            b = (int.Parse(sqlDataReader[0].ToString()) > 0) ? true : false;
            return b;
        }

        public static List<PerguntaFichaAvaliacao> SelectPerguntasFichaAvaliacao(long codigoAnalise)
        {
            string query = "select * from pergunta_ficha_avaliacao where cod_analise = " + codigoAnalise.ToString() + ";";

            SqlDataReader sqlDataReader = CamadaDados.DataBaseCommunicator.DataBaseCommunicator.ReadData(query);

            List<PerguntaFichaAvaliacao> pergs = new List<PerguntaFichaAvaliacao>();

            while (sqlDataReader.Read())
            {
                PerguntaFichaAvaliacao perguntaFichaAvaliacao = new PerguntaFichaAvaliacao(
                    (long)sqlDataReader["cod_pergunta_ficha_avaliacao"],
                    (long)sqlDataReader["cod_analise"],
                    (int)sqlDataReader["numero_pergunta"],
                    (long)sqlDataReader["cod_item"],
                    (string)sqlDataReader["texto"],
                    (long)sqlDataReader["cod_tipoEscala"]);
                pergs.Add(perguntaFichaAvaliacao);
            }

            return pergs;
        }

        public static List<PerguntaQuestionario> SelectPerguntasQuestionario(long codigoAnalise)
        {
            string query = "select * from pergunta_questionario where cod_analise = " + codigoAnalise.ToString() + ";";

            SqlDataReader sqlDataReader = CamadaDados.DataBaseCommunicator.DataBaseCommunicator.ReadData(query);

            List<PerguntaQuestionario> pergs = new List<PerguntaQuestionario>();

            while (sqlDataReader.Read())
            {
                PerguntaQuestionario perguntaQuestionario = new PerguntaQuestionario(
                    (long)sqlDataReader["cod_pergunta_questionario"],
                    (long)sqlDataReader["cod_analise"],
                    (float)sqlDataReader["numero_pergunta"],
                    ((sqlDataReader["cod_zona"].ToString() == "") ? 0 : (long)sqlDataReader["cod_zona"]),
                    ((sqlDataReader["cod_item"].ToString() == "") ? -1 : (long)sqlDataReader["cod_item"]),
                    (string)sqlDataReader["texto"],
                    (long)sqlDataReader["cod_tipoEscala"],
                    (string)sqlDataReader["tipo_questao"]);
                pergs.Add(perguntaQuestionario);
            }

            return pergs;
        }

        public static void UpdatePerguntasFichaAvaliacao(PerguntaFichaAvaliacao perguntaFichaAvaliacao)
        {
            String query = "update pergunta_ficha_avaliacao set " +
                "cod_item = " + perguntaFichaAvaliacao.CodigoItem + "," +
                "texto = '" + perguntaFichaAvaliacao.Texto + "'," +
                "cod_tipoEscala = " + perguntaFichaAvaliacao.CodigoTipoEscala +
                " where cod_analise = " + perguntaFichaAvaliacao.CodigoAnalise + " and " +
                "numero_pergunta = " + perguntaFichaAvaliacao.NumeroPergunta + ";";

            CamadaDados.DataBaseCommunicator.DataBaseCommunicator.Query(query);
        }

        // not used
        public static void UpdatePerguntasQuestionario(PerguntaQuestionario perguntaQuestionario)
        {
            String query = "update pergunta_questionario set " +
                "cod_zona = " + ((perguntaQuestionario.CodigoZona == -1) ? "null" : perguntaQuestionario.CodigoZona.ToString()) + "," +
                "cod_item = " + ((perguntaQuestionario.CodigoItem == -1) ? "null" : perguntaQuestionario.CodigoItem.ToString()) + "," +
                "texto = '" + perguntaQuestionario.Texto + "'," +
                "cod_tipoEscala = " + perguntaQuestionario.CodigoTipoEscala + "," +
                "tipo_questao = '" + perguntaQuestionario.TipoQuestao +
                "' where cod_analise = " + perguntaQuestionario.CodigoAnalise + " and " +
                "numero_pergunta = " + perguntaQuestionario.NumeroPergunta + ";";

            CamadaDados.DataBaseCommunicator.DataBaseCommunicator.Query(query);
        }

        public static void DeletePerguntasQuestionario(long codigoAnalise)
        {
            string query = "delete from pergunta_questionario where cod_analise = " + codigoAnalise + ";";

            CamadaDados.DataBaseCommunicator.DataBaseCommunicator.Query(query);
        }
        #endregion

        #region Website
        #region Get Estado Formulários

        public static bool GetEstadoCheckListOnline(long codigoProjecto, long codigoAnalise)
        {
            string query = "SELECT analise.estadoWebCheckList FROM analise WHERE analise.cod_analise = " + codigoAnalise + 
                            " AND analise.cod_projecto = " + codigoProjecto;

            SqlDataReader sqlDataReader = CamadaDados.DataBaseCommunicator.DataBaseCommunicator.ReadData(query);

            if (sqlDataReader.Read())
            {
                if (int.Parse(sqlDataReader["estadoWebCheckList"].ToString()) == 1)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public static bool GetEstadoFichaAvaliacaoOnline(long codigoProjecto, long codigoAnalise)
        {
            string query = "SELECT analise.estadoWebFichaAvaliacao FROM analise WHERE analise.cod_analise = " + codigoAnalise +
                            " AND analise.cod_projecto = " + codigoProjecto;

            SqlDataReader sqlDataReader = CamadaDados.DataBaseCommunicator.DataBaseCommunicator.ReadData(query);

            sqlDataReader.Read();
            if (int.Parse(sqlDataReader["estadoWebFichaAvaliacao"].ToString()) == 1)
                return true;
            else
                return false;
        }

        public static bool GetEstadoQuestionariosOnline(long codigoProjecto, long codigoAnalise)
        {
            string query = "SELECT analise.estadoWebQuestionario FROM analise WHERE analise.cod_analise = " + codigoAnalise +
                            " AND analise.cod_projecto = " + codigoProjecto;

            SqlDataReader sqlDataReader = CamadaDados.DataBaseCommunicator.DataBaseCommunicator.ReadData(query);

            sqlDataReader.Read();
            if (int.Parse(sqlDataReader["estadoWebQuestionario"].ToString()) == 1)
                return true;
            else
                return false;
        }
        #endregion
        #region Set Estado Formulários
        public static void SetEstadoCheckListOnline(long codigoProjecto, long codigoAnalise, bool estado)
        {
            int value;
            if(estado)
                value=1;
            else
                value=0;

            DataBaseCommunicator.Query("Update analise "+
                                       "Set estadoWebCheckList=" + value +
                                        "where analise.cod_projecto = "+ codigoProjecto +
                                        " and analise.cod_analise = "+ codigoAnalise);
        }

        public static void SetEstadoFichaAvaliacaoOnline(long codigoProjecto, long codigoAnalise, bool estado)
        {
            int value;
            if(estado)
                value=1;
            else
                value=0;

            DataBaseCommunicator.Query("Update analise " +
                                       "Set estadoWebFichaAvaliacao="+ value +
                                        "where analise.cod_projecto = " + codigoProjecto +
                                        " and analise.cod_analise = " + codigoAnalise);
        }

        public static void SetEstadoQuestionarioOnline(long codigoProjecto, long codigoAnalise, bool estado)
        {
            int value;
            if(estado)
                value=1;
            else
                value=0;

            DataBaseCommunicator.Query("Update analise " +
                                       "Set estadoWebQuestionario=" + value +
                                        "where analise.cod_projecto = " + codigoProjecto +
                                        " and analise.cod_analise = " + codigoAnalise);
        }
        #endregion
        #endregion
    }
}