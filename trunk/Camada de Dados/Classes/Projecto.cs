using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ETdA.Camada_de_Dados;

namespace ETdA.Camada_de_Dados.Classes
{
    class Projecto
    {

        //Variaveis de instância
        private String codProjecto;
        private String nomeEstabelecimento;
        private DateTime ultimaActualizacao;
        private List<String> codAnalises;

        //Constructores
        public Projecto(String codProj, String nomeEst, 
            DateTime ultimaAct, List<String> analises)
        {
            codProjecto = codProj;
            nomeEstabelecimento = nomeEst;
            ultimaActualizacao = ultimaAct;
            codAnalises = analises;
        }

        public Projecto ()
        {
            codProjecto = "";
            nomeEstabelecimento = "";
            ultimaActualizacao = new DateTime();
            codAnalises = new List<String>();
        }

        public Projecto(Projecto p)
        {
            codProjecto = p.Codigo;
            nomeEstabelecimento = p.Nome;
            ultimaActualizacao = p.Data;
            codAnalises = p.Analises;
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
            get { return analises; }
            set { analises = value; }
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

            Camada_de_Dados.DataBaseCommunicator.
                FuncsToDataBase.insertAnalise(codProjecto,a);
            a.Codigo = Camada_de_Dados.DataBaseCommunicator.
                FuncsToDataBase.selectCodigoAnalise(a.Data);

            codAnalises.Add(a.Codigo);
        }

        /*
         * Adiciona uma Analise
         */
        public void adicionaAnalise(String cod)
        {
            codAnalises.Add(cod);
        }

        /*
         * Remove Analise
         */
        public void removeAnalise(String codigo)
        {
            Boolean found = false;
            for (int i = 0; i < analises.Count && !found; i++)
                if (analises[i].Codigo == codigo)
                {
                    analises.RemoveAt(i);
                    found = true;
                }

            Camada_de_Dados.DataBaseCommunicator.
                FuncsToDataBase.deleteAnalise(codigo);
        }

        public void modificaAnalise(Analise a)
        {
            Boolean found = false;
            for (int i = 0; i < codAnalises.Count && !found; i++)
                if (codAnalises[i].Equals(codigo))
                {
                    analises.RemoveAt(i);
                    found = true;
                }

            Camada_de_Dados.DataBaseCommunicator.
                FuncsToDataBase.deleteAnalise(codigo);
        }

        /*
         * Devolve uma Analise com o codigo recebido
         */
        public Analise getAnaliseByCode(String codigo)
        {
            Analise a = null;
            for (int i = 0; i < analises.Count; i++)
                if (analises[i].Codigo == codigo)
                    a = analises[i].clone();
            return a;
        }

        /* Fim de Gestao de Analises */
    }
}
