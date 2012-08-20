using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ETdA.Camada_de_Dados.Classes;
using ETdA.Camada_de_Dados.Classes.Estruturas;
using System.Windows;

namespace ETdA.Camada_de_Dados.ETdA
{
    class ETdA
    {
        private delegate void eventoEventHandler(object sender, EventArgs e);

		//private static event eventoEventHandler evento_analista_registado;
        private static event eventoEventHandler evento_projecto_adicionado;
		//private static event eventoEventHandler evento_projecto_removido;

        private static Dictionary<long, string> cod_nome_projectos;
        private static Dictionary<long, Projecto> projectos;
        private static string username;

        public static Dictionary<long, Projecto> Projectos
        {
            get { return projectos; }
            set { projectos = value; }
        }

        // s_final
        private static void initEventos()
        {
            evento_projecto_adicionado +=new eventoEventHandler(
                Camada_de_Interface.InterfaceGuestaoProjectos.addProjectoReenc);
        }

        // s_final
        public static void init()
        {
            
            initEventos();

            cod_nome_projectos = Camada_de_Dados.DataBaseCommunicator.FuncsToDataBase.selectNomeProjectos();
            projectos = new Dictionary<long, Projecto>();
        }

        // s_final
        public static Dictionary<long, string> Codes_Nomes_Projectos
        {
            get 
            {
                return new Dictionary<long,string>(cod_nome_projectos);
            }
        }

        public static string Username
        {
            get { return username; }
            set { username = value; }
        }

        // s_final
        public static Projecto getProjecto(long codProjecto)
        {
            if (!projectos.Keys.Contains(codProjecto))
                abreProjecto(codProjecto);
                
            return projectos[codProjecto];
        }

        /* ------------------------------------------------------ */
        /* Metodos */
        /* ------------------------------------------------------ */

        // s_final
        public static Dictionary<long, string> projectosRecentes()
        {
            Dictionary<long, string> rs = new Dictionary<long, string>();

            for (int i = 0; i < 5 && i < cod_nome_projectos.Count; i++)
                rs.Add(cod_nome_projectos.ElementAt(i).Key, cod_nome_projectos.ElementAt(i).Value);

            return rs;
        }

        #region Gestao Projectos
        /*
         * Verifica se Ja tem Projecto com esse nome na Base de dados
         */
        // s_final
        public static Boolean podeAdicionarProjecto(String nomeEstabelecimento)
        {
            return !cod_nome_projectos.Values.Contains(nomeEstabelecimento);
        }

        /*
         * Adiciona Novo Projecto na aplicação
         */
        // s_final
        public static void adicionaNovoProjecto(String nomeEstabelecimento)
        {
            Projecto p = new Projecto();
            p.Nome = nomeEstabelecimento;
            p.Data = DateTime.Now;

            p.Codigo = Camada_de_Dados.DataBaseCommunicator.FuncsToDataBase.insertProjecto(p);

            cod_nome_projectos.Add(p.Codigo,p.Nome);
            projectos.Add(p.Codigo, p);

            List<string> s = new List<string>();
            s.Add(p.Codigo.ToString());
            s.Add(p.Nome);

            evento_projecto_adicionado(s, new EventArgs());
        }

        /*
         * Abre um projecto com o código de estabelecimento
         */
        // s_final
        public static void abreProjecto(long codProjecto)
        {
            if (!projectos.Keys.Contains(codProjecto))
            {
                Projecto proj = Camada_de_Dados.DataBaseCommunicator.
                    FuncsToDataBase.selectProjecto(codProjecto);
                projectos.Add(proj.Codigo, proj);
            }
        }

        public static void removeProjecto(String nomeProjecto)
        {
            long cod = -1;
            Boolean found = false;
            for (int i = 0; i < cod_nome_projectos.Count && !found; i++)
            {
                KeyValuePair<long, string> p = cod_nome_projectos.ElementAt(i);
                if (p.Value == nomeProjecto)
                {
                    cod = p.Key;
                    cod_nome_projectos.Remove(cod);
                    if (projectos[cod] != null)
                        projectos.Remove(cod);
                    found = true;
                }
            }

            Camada_de_Dados.DataBaseCommunicator.FuncsToDataBase.
                deleteProjecto(cod);
        }

        public static void removeProjecto(long codProjecto)
        {
            cod_nome_projectos.Remove(codProjecto);

            foreach (Analise a in projectos[codProjecto].Analises.Values)
            {
                Camada_de_Negócio.GestaodeAnalises.removerAnalise(codProjecto, a.Codigo);
                Camada_de_Dados.DataBaseCommunicator.FuncsToDataBase.desactivarAnalise(a.Codigo);
            }
            
            projectos.Remove(codProjecto);
        }
        public static void modificaProjecto(Projecto p)
        {
            Camada_de_Dados.DataBaseCommunicator.FuncsToDataBase.
               updateProjecto(p);
        }

        public static void ultimaAlteracao(Projecto p)
        {
            Camada_de_Dados.DataBaseCommunicator.FuncsToDataBase.
               updateProjecto(p);
        }

        #endregion

        #region Gestao Analistas
        public static bool adicionaAnalista(String username, String password)
        {
            return Camada_de_Dados.DataBaseCommunicator.
                FuncsToDataBase.insertAnalista(username, password);
        }

        public static bool loginAnalista(String server, String database, 
            String username, String password)
        {
            return Camada_de_Dados.DataBaseCommunicator.
                FuncsToDataBase.ligaAnalista(server, database, username, password);
        }
        #endregion
    }
}
