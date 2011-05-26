using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ETdA.Camada_de_Dados.ETdA
{
    class ETdA
    {
        private static Analista analista;
        private static List<Projecto> projectos;
        private static List<String> projectos_recentes;

        public static Analista Analista
        {
            get { return analista; }
            set { analista = value; }
        }

        public static List<Projecto> Projectos
        {
            get { return projectos; }
            set { projectos = value; }
        }

        public static List<String> Projectos_Recentes
        {
            get { return projectos_recentes; }
            set { projectos_recentes = value; }
        }

        /* ------------------------------------------------------ */
        /* Metodos */
        /* ------------------------------------------------------ */

        private static void init()
        {
            projectos = Camada_de_Dados.DataBaseCommunicator.FuncsToDataBase.selectProjectos();

            projectosRecentes();
        }

        private static void projectosRecentes()
        {
            projectos_recentes = new List<String>();

            for (int i = 0; i < 5 && i < projectos.Count; i++)
                projectos_recentes.Add(projectos[i].Codigo);
        }

        /* Gestao dos Projectos */

        /*
         * Verifica se Ja tem Projecto com esse nome na Base de dados
         */
        public static Boolean podeAdicionarProjecto(String nomeEstabelecimento)
        {
            List<String> nomes = Camada_de_Dados.DataBaseCommunicator.
                FuncsToDataBase.selectNomeProjectos();

            return !nomes.Contains(nomeEstabelecimento);
        }

        /*
         * Adiciona Novo Projecto na aplicação
         */
        public static void adicionaNovoProjecto(String nomeEstabelecimento)
        {
            Projecto p = new Projecto();
            p.Nome = nomeEstabelecimento;
            p.Data = DateTime.Now;

            Camada_de_Dados.DataBaseCommunicator.FuncsToDataBase.insertProjecto(p);
            String cod = Camada_de_Dados.DataBaseCommunicator.
                FuncsToDataBase.selectCodigoProjecto(nomeEstabelecimento);
            p.Codigo = cod;

            projectos.Add(p);
        }

        /*
         * Adiciona Projecto na aplicação
         */
        public static void adicionaProjecto(Projecto p)
        {
            projectos.Add(p);
        }

        /*
         * Remove Projecto da aplicação
         */
        public static void removeProjecto(String codigo)
        {
            Boolean found = false;
            for (int i = 0; i < projectos.Count && !found; i++)
                if (projectos[i].Codigo == codigo)
                {
                    if (projectos_recentes.Contains(projectos[i].Codigo))
                        projectos_recentes.Remove(projectos[i].Codigo);
                    projectos.RemoveAt(i);
                    found = true;
                }

            Camada_de_Dados.DataBaseCommunicator.FuncsToDataBase.
                deleteProjecto(codigo);
        }

        /*
         * Devolve um projecto com o codigo recevido
         */
        public static Projecto getProjectoByCode(String codigo)
        {
            Projecto p = null;
            for (int i = 0; i < projectos.Count; i++)
                if (projectos[i].Codigo == codigo)
                    p = projectos[i].clone();
            return p;
        }

        /*
         * Devolve um projecto com o nome de estabelecimento Recebido
         */
        public static Projecto getProjectoByName(String nomeEstaelecimento)
        {
            Projecto p = null;
            for (int i = 0; i < projectos.Count; i++)
                if (projectos[i].Nome == nomeEstaelecimento)
                    p = projectos[i].clone();
            return p;
        }

        /* End Gestao Projectos */

        /* Gestao de Analistas */

        public static void adicionaAnalista(String username, String password)
        {
            Camada_de_Dados.DataBaseCommunicator.
                FuncsToDataBase.insertAnalista(username, password);
        }

        public static void removeAnalista(String username, String password)
        {
            Camada_de_Dados.DataBaseCommunicator.
                FuncsToDataBase.deleteAnalista(username, password);
        }

        public static void editAnalista(String username, String password)
        {
            Camada_de_Dados.DataBaseCommunicator.
                FuncsToDataBase.deleteAnalista(username, password);
        }

        public static void getAnalista(String username, String password)
        {

        }

        /* Fim de Gestao de Analistas */
    }
}
