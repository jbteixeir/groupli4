using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ETdA.Camada_de_Dados;

namespace ETdA
{
    class Projecto
    {

        //Variaveis de instância
        private String codProjecto;
        private String nomeEstabelecimento;
        private DateTime ultimaActualizacao;
        private List<Analise> analises;

        //Constructores
        public Projecto(String codProj, String nomeEst, 
            DateTime ultimaAct, List<Analise> analises)
        {
            codProjecto = codProj;
            nomeEstabelecimento = nomeEst;
            ultimaActualizacao = ultimaAct;
            this.analises = analises;
        }

        public Projecto ()
        {
            codProjecto = "";
            nomeEstabelecimento = "";
            ultimaActualizacao = new DateTime();
            analises = new List<Analise>();
        }

        public Projecto(Projecto p)
        {
            codProjecto = p.Codigo;
            nomeEstabelecimento = p.Nome;
            ultimaActualizacao = p.Data;
            analises = p.Analises;
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

        public List<Analise> Analises
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
        public void adicionaNovaAnalise(String tipoAnalise, String nomeAnalise)
        {
            Analise a = new Analise();
            a.Tipo = tipoAnalise;
            a.Nome = nomeAnalise;

            analises.Add(a);
        }

        /*
         * Adiciona uma Analise
         */
        public void adicionaAnalise(Analise a)
        {
            analises.Add(a);
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
