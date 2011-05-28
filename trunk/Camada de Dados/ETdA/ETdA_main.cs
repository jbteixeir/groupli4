using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ETdA.Camada_de_Dados.Classes;
using ETdA.Camada_de_Dados.Classes.Estruturas;

namespace ETdA.Camada_de_Dados.ETdA
{
    class ETdA
    {
        private static Analista analista;
        private static Dictionary<string, string> cod_nome;
        private static Projecto projectoAberto;

        public static Analista Analista
        {
            get { return analista; }
            set { analista = value; }
        }

        public static List<String> Nomes_Estabelecimentos
        {
            get 
            {
                return new List<string>(cod_nome.Values);
            }
        }

        public static Projecto Projecto
        {
            get { return projectoAberto; }
            set { projectoAberto = value; }
        }

        /* ------------------------------------------------------ */
        /* Metodos */
        /* ------------------------------------------------------ */

        public static void init()
        {
            cod_nome = Camada_de_Dados.DataBaseCommunicator.FuncsToDataBase.selectNomeProjectos();

            projectosRecentes();
        }

        private static List<String> projectosRecentes()
        {
            List<String> proj = new List<string>(cod_nome.Values);
            List<String> projectos_recentes = new List<String>();

            for (int i = 0; i < 5 && i < proj.Count; i++)
                projectos_recentes.Add(proj[i]);

            return projectos_recentes;
        }

        /* Gestao dos Projectos */

        /*
         * Verifica se Ja tem Projecto com esse nome na Base de dados
         */
        public static Boolean podeAdicionarProjecto(String nomeEstabelecimento)
        {
            List<string> p = new List<string>(cod_nome.Values);

            return p.Contains(nomeEstabelecimento);
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

            cod_nome.Add(p.Codigo,nomeEstabelecimento);
            projectoAberto = p;
        }

        /*
         * Abre um projecto com o nome de estabelecimento Recebido
         */
        public static void abreProjecto(string nomeEstabelecimento)
        {
            String cod = null;
            Boolean found = false;
            for (int i = 0; i < cod_nome.Count && !found; i++)
            {
                KeyValuePair<string, string> p = cod_nome.ElementAt(i);
                if (p.Value == nomeEstabelecimento)
                {
                    cod = p.Key;
                    found = true;
                }
            }

            projectoAberto = Camada_de_Dados.DataBaseCommunicator.
                FuncsToDataBase.selectProjecto(cod);
            /*projectoAberto.Cod_Name_Analise = Camada_de_Dados.
                DataBaseCommunicator.FuncsToDataBase.selectNomesAnalises(
                projectoAberto.Codigo);*/
        }

        /*
         * Remove Projecto da aplicação
         */
        public static void removeProjecto(String nomeEstabelecimento)
        {
            String cod = null;
            Boolean found = false;
            for (int i = 0; i < cod_nome.Count && !found; i++)
            {
                KeyValuePair<string, string> p = cod_nome.ElementAt(i);
                if (p.Value == nomeEstabelecimento)
                {
                    cod = p.Key;
                    cod_nome.Remove(cod);
                    found = true;
                }
            }

            Camada_de_Dados.DataBaseCommunicator.FuncsToDataBase.
                deleteProjecto(cod);
        }

        public static void modificaProjecto(String nomeEstabelecimentoNovo)
        {
            projectoAberto.Nome = nomeEstabelecimentoNovo;
            /*Camada_de_Dados.DataBaseCommunicator.FuncsToDataBase.
               updateProjecto(projectoAberto);*/
        }

        public static void ultimaAlteracao(DateTime novaData)
        {
            projectoAberto.Data = novaData;
            /*Camada_de_Dados.DataBaseCommunicator.FuncsToDataBase.
               updateProjecto(projectoAberto);*/
        }

        /* Fim Gestao Projectos */

        /* Gestao de Analistas */
        
        public static bool adicionaAnalista(String username, String password)
        {
            return Camada_de_Dados.DataBaseCommunicator.
                FuncsToDataBase.insertAnalista(username, password);
        }
        /*
        public static void removeAnalista(String username, String password)
        {
            Camada_de_Dados.DataBaseCommunicator.
                FuncsToDataBase.deleteAnalista(username);
        }
        */
        public static bool loginAnalista(String server, String database, 
            String username, String password)
        {
            return Camada_de_Dados.DataBaseCommunicator.
                FuncsToDataBase.ligaAnalista(server, database, username, password);
        }
        /* Fim de Gestao de Analistas */
    }
}
