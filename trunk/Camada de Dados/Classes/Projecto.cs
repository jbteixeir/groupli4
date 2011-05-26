using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ETdA.Camada_de_Dados;
using ETdA.Camada_de_Dados.Classes.Estruturas;

namespace ETdA.Camada_de_Dados.Classes
{
    class Projecto
    {

        //Variaveis de instância
        private String codProjecto;
        private String nomeEstabelecimento;
        private DateTime ultimaActualizacao;
        private List<Tuplo<String,String>> cod_name_analise;
        private Analise analise_Aberta;

        //Constructores
        public Projecto(String codProj, String nomeEst, 
            DateTime ultimaAct, List<Tuplo<String,String>> analises)
        {
            codProjecto = codProj;
            nomeEstabelecimento = nomeEst;
            ultimaActualizacao = ultimaAct;
            cod_name_analise = analises;
        }

        public Projecto ()
        {
            codProjecto = "";
            nomeEstabelecimento = "";
            ultimaActualizacao = new DateTime();
            cod_name_analise = new List<Tuplo<String,String>>();
        }

        public Projecto(Projecto p)
        {
            codProjecto = p.Codigo;
            nomeEstabelecimento = p.Nome;
            ultimaActualizacao = p.Data;
            cod_name_analise = p.Cod_Name_Analise;
        }

        //Métodos

        public String Codigo
        {
            get { return codProjecto; }
            set { codProjecto = value; }
        }
        public String Nome
        {
            get { return nomeEstabelecimento; }
            set { nomeEstabelecimento = value; }
        }
        public DateTime Data
        {
            get { return ultimaActualizacao; }
            set { ultimaActualizacao = value; }
        }
        public List<String> Analises
        {
            get 
            {
                List<String> nomes = new List<string>();
                foreach (Tuplo<String, String> t in cod_name_analise)
                    nomes.Add(t.Snd);
                return nomes;
            }
        }
        public List<Tuplo<String,String>> Cod_Name_Analise
        {
            get { return cod_name_analise; }
            set { cod_name_analise = value; }
        }
        public Analise Analise_Aberta
        {
            get { return analise_Aberta; }
            set { analise_Aberta = value; }
        }

        public Projecto clone()
        {
            return new Projecto(this);
        }

        /* ------------------------------------------------------ */
        /* Metodos de Gestao */
        /* ------------------------------------------------------ */

        /* Gestao de Analises */

        /*
         * Verifica se Ja tem Projecto com esse nome na Base de dados
         */
        public Boolean podeAdicionarAnalise(String nomeAnalise)
        {
            Boolean found = false;
            for (int i = 0; i < cod_name_analise.Count && !found; i++)
                if (cod_name_analise[i].Snd == nomeAnalise)
                    found = true;

            return found;
        }

        /*
         * Adiciona uma nova Analise
         */
        public void adicionaNovaAnalise(String tipoAnalise, 
            String nomeAnalise, List<Zona> zonas, List<Item> itens)
        {
            Analise a = new Analise();
            a.Tipo = tipoAnalise;
            a.Nome = nomeAnalise;
            a.Zonas = zonas;
            a.Itens = itens;
            /*
            Camada_de_Dados.DataBaseCommunicator.
                FuncsToDataBase.insertAnalise(codProjecto,a);
            a.Codigo = Camada_de_Dados.DataBaseCommunicator.
                FuncsToDataBase.selectCodigoAnalise(a.Data);

            for (int i = 0 ; i < zonas.Count ; i++)
                Camada_de_Dados.DataBaseCommunicator.
                    FuncsToDataBase.insertZonaAnalise(zonas[i].Codigo,
                    a.Codigo);

            for (int i = 0; i < itens.Count; i++)
                Camada_de_Dados.DataBaseCommunicator.
                    FuncsToDataBase.insertZonaAnalise(itens[i].Codigo,
                    a.Codigo);
            */
            Tuplo<String, String> t = new Tuplo<String, String>(a.Codigo,a.Nome);
            cod_name_analise.Add(t);
            analise_Aberta = a;
        }

        /*
         * 
         */
        public void abreAnalise(String nomeAnalise)
        {
            String cod = null;
            Boolean found = false;
            for (int i = 0; i < cod_name_analise.Count && !found; i++)
                if (cod_name_analise[i].Snd == nomeAnalise)
                {
                    cod = cod_name_analise[i].Fst;
                    found = true;
                }
            /*
            analise_Aberta = Camada_de_Dados.DataBaseCommunicator.
                FuncsToDataBase.selectAnalise(cod);
            analise_Aberta.Zonas = Camada_de_Dados.DataBaseCommunicator.
                FuncsToDataBase.selectZonasAnalise(cod);
            analise_Aberta.Itens = Camada_de_Dados.DataBaseCommunicator.
                FuncsToDataBase.selectItensAnalise(cod);*/
        }

        /*
         * Remove Analise
         */
        public void removeAnalise(String nomeAnalise)
        {
            String cod = null;
            Boolean found = false;
            for (int i = 0; i < cod_name_analise.Count && !found; i++)
                if (cod_name_analise[i].Snd == nomeAnalise)
                {
                    cod = cod_name_analise[i].Fst;
                    cod_name_analise.RemoveAt(i);
                    found = true;
                }
            /*
            Camada_de_Dados.DataBaseCommunicator.
                FuncsToDataBase.deleteAnalise(cod);*/
        }

        public void modificaAnalise()
        {
            /*
            Camada_de_Dados.DataBaseCommunicator.
                FuncsToDataBase.updateAnalise(codProjecto, analise_Aberta);*/
        }

        /* Fim de Gestao de Analises */
    }
}
