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
        private Dictionary<string,string> cod_name_analise;
        private Analise analise_Aberta;

        //Constructores
        public Projecto(String codProj, String nomeEst, 
            DateTime ultimaAct, Dictionary<string,string> analises)
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
            cod_name_analise = new Dictionary<string, string>();
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
                return new List<string>(cod_name_analise.Values);
            }
        }
        public Dictionary<string, string> Cod_Name_Analise
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
            List<string> l = new List<string>(cod_name_analise.Values);

            return l.Contains(nomeAnalise);
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
            cod_name_analise.Add(a.Codigo,a.Nome);
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
            {
                KeyValuePair<string, string> p = cod_name_analise.ElementAt(i);
                if (p.Value == nomeEstabelecimento)
                {
                    cod = p.Key;
                    found = true;
                }
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
            {
                KeyValuePair<string, string> p = cod_name_analise.ElementAt(i);
                if (p.Value == nomeEstabelecimento)
                {
                    cod = p.Key;
                    cod_name_analise.Remove(cod) ;
                    found = true;
                }
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
