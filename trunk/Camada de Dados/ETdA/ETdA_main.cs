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

        //[Category(""), Description("Ocorre sempre ...")]
        private static event eventoEventHandler evento_analista_registado;
        private static event eventoEventHandler evento_projecto_adicionado;
        private static event eventoEventHandler evento_projecto_removido;
        private static event eventoEventHandler evento_analise_adicionada;
        private static event eventoEventHandler evento_analise_removida;

        private static Dictionary<long, string> cod_nome_projectos;
        private static Dictionary<long, Projecto> projectos;

        private static void initEventos()
        {
            //evento_analista_registado += Camada_de_Interface.InterfaceLogin.
            evento_projecto_adicionado +=new eventoEventHandler(
                Camada_de_Interface.InterfaceGuestaoProjectos.addProjectoReenc);
        }

        public static void init()
        {
            initEventos();

            cod_nome_projectos = Camada_de_Dados.DataBaseCommunicator.FuncsToDataBase.selectNomeProjectos();

            projectosRecentes();
        }

        public static List<String> Nomes_Estabelecimentos
        {
            get 
            {
                return new List<string>(cod_nome_projectos.Values);
            }
        }

        public static Projecto getProjecto(long codProjecto)
        {
            return projectos[codProjecto];
        }

        /* ------------------------------------------------------ */
        /* Metodos */
        /* ------------------------------------------------------ */

        private static List<String> projectosRecentes()
        {
            List<String> proj = new List<string>(cod_nome_projectos.Values);
            List<String> projectos_recentes = new List<String>();

            for (int i = 0; i < 5 && i < proj.Count; i++)
                projectos_recentes.Add(proj[i]);

            return projectos_recentes;
        }

        /* Gestao dos Projectos */
        #region Gestao Projectos
        /*
         * Verifica se Ja tem Projecto com esse nome na Base de dados
         */
        public static Boolean podeAdicionarProjecto(String nomeEstabelecimento)
        {
            List<string> p = new List<string>(cod_nome_projectos.Values);

            return !p.Contains(nomeEstabelecimento);
        }

        /*
         * Adiciona Novo Projecto na aplicação
         */
        public static void adicionaNovoProjecto(String nomeEstabelecimento)
        {
            Projecto p = new Projecto();
            p.Nome = nomeEstabelecimento;
            p.Data = DateTime.Now;

            MessageBox.Show(p.Data.ToString("yyyymmdd hh:mm:ss"));

            Camada_de_Dados.DataBaseCommunicator.FuncsToDataBase.insertProjecto(p);
            String cod = Camada_de_Dados.DataBaseCommunicator.
                FuncsToDataBase.selectCodigoProjecto(nomeEstabelecimento);
            p.Codigo = cod;

            cod_nome.Add(p.Codigo,nomeEstabelecimento);
            projectoAberto = p;

            evento_projecto_adicionado(p.Nome, new EventArgs());
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
            projectoAberto.Cod_Name_Analise = Camada_de_Dados.
                DataBaseCommunicator.FuncsToDataBase.selectNomesAnalises(
                projectoAberto.Codigo);
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
#endregion
        /* Fim Gestao Projectos */

        /* Gestao de Analistas */
        #region Gestao Analistas
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
        #endregion
        /* Fim de Gestao de Analistas */
    }
}
