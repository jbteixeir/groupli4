using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ETdA.Camada_de_Dados.Classes;
using ETdA.Camada_de_Dados.Classes.Estruturas;

namespace ETdA.Camada_de_Dados.ETdA
{
    class ETdA_main
    {
        private Analista analista;
        private IList<Tuplo<String,String>> cod_nome;
        private Projecto projectoAberto;

        public Analista Analista
        {
            get { return analista; }
            set { analista = value; }
        }

        public List<String> Nomes_Estabelecimentos
        {
            get 
            {
                List<String> nomes = new List<string>();
                foreach (Tuplo<String, String> t in cod_nome)
                    nomes.Add(t.Snd);
                return nomes;
            }
        }

        public Projecto Projecto
        {
            get { return projectoAberto; }
            set { projectoAberto = value; }
        }

        /* ------------------------------------------------------ */
        /* Metodos */
        /* ------------------------------------------------------ */

        private void init()
        {
            cod_nome = Camada_de_Dados.DataBaseCommunicator.FuncsToDataBase.selectNomeProjectos();

            projectosRecentes();
        }

        private List<String> projectosRecentes()
        {
            List<String> projectos_recentes = new List<String>();

            for (int i = 0; i < 5 && i < cod_nome.Count; i++)
                projectos_recentes.Add(cod_nome[i].Snd);

            return projectos_recentes;
        }

        /* Gestao dos Projectos */

        /*
         * Verifica se Ja tem Projecto com esse nome na Base de dados
         */
        public Boolean podeAdicionarProjecto(String nomeEstabelecimento)
        {
            Boolean found = false;
            for (int i = 0; i < cod_nome.Count && !found; i++)
                if (cod_nome[i].Snd == nomeEstabelecimento)
                    found = true;

            return found;
        }

        /*
         * Adiciona Novo Projecto na aplicação
         */
        public void adicionaNovoProjecto(String nomeEstabelecimento)
        {
            Projecto p = new Projecto();
            p.Nome = nomeEstabelecimento;
            p.Data = DateTime.Now;

            Camada_de_Dados.DataBaseCommunicator.FuncsToDataBase.insertProjecto(p);
            String cod = Camada_de_Dados.DataBaseCommunicator.
                FuncsToDataBase.selectCodigoProjecto(nomeEstabelecimento);
            p.Codigo = cod;

            Tuplo<String, String> t = new Tuplo<String, String>(p.Codigo, p.Nome);

            cod_nome.Add(t);
            projectoAberto = p;
        }

        /*
         * Abre um projecto com o nome de estabelecimento Recebido
         */
        public void abreProjecto(String nomeEstabelecimento)
        {
            String cod = null;
            Boolean found = false;
            for (int i = 0; i < cod_nome.Count && !found; i++)
                if (cod_nome[i].Snd == nomeEstabelecimento)
                {
                    cod = cod_nome[i].Fst;
                    found = true;
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
        public void removeProjecto(String nomeEstabelecimento)
        {
            String cod = null;
            Boolean found = false;
            for (int i = 0; i < cod_nome.Count && !found; i++)
                if (cod_nome[i].Snd == nomeEstabelecimento)
                {
                    cod = cod_nome[i].Fst; 
                    cod_nome.RemoveAt(i);
                    found = true;
                }

            /*Camada_de_Dados.DataBaseCommunicator.FuncsToDataBase.
                deleteProjecto(cod);*/
        }

        public void modificaProjecto(String nomeEstabelecimentoNovo)
        {
            projectoAberto.Nome = nomeEstabelecimentoNovo;
            /*Camada_de_Dados.DataBaseCommunicator.FuncsToDataBase.
               updateProjecto(projectoAberto);*/
        }

        public void ultimaAlteracao(DateTime novaData)
        {
            projectoAberto.Data = novaData;
            /*Camada_de_Dados.DataBaseCommunicator.FuncsToDataBase.
               updateProjecto(projectoAberto);*/
        }

        /* Fim Gestao Projectos */

        /* Gestao de Analistas */
        /*
        public void adicionaAnalista(String username, String password)
        {
            Camada_de_Dados.DataBaseCommunicator.
                FuncsToDataBase.insertAnalista(username, password);
        }

        public void removeAnalista(String username, String password)
        {
            Camada_de_Dados.DataBaseCommunicator.
                FuncsToDataBase.deleteAnalista(username);
        }

        public void editAnalista(String username, String password)
        {
            Camada_de_Dados.DataBaseCommunicator.
                FuncsToDataBase.updateAnalista(username, password);
        }

        public Boolean isAnalista(String username, String password)
        {
            return Camada_de_Dados.DataBaseCommunicator.
                FuncsToDataBase.selectAnalista(username, password);
        }

        public void loginAnalista(String username, String password)
        {
            Camada_de_Dados.DataBaseCommunicator.
                FuncsToDataBase.ligaAnalista(username, password);

            init();
        }
        */
        /* Fim de Gestao de Analistas */
    }
}
