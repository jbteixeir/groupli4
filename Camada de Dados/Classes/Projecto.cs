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
        private delegate void eventoEventHandler(object sender, EventArgs e);
        private static event eventoEventHandler evento_analise_adicionada;
        private static event eventoEventHandler evento_analise_removida;
        //Variaveis de instância
        private long codProjecto;
        private String nomeEstabelecimento;
        private DateTime ultimaActualizacao;
        private Dictionary<long,string> cod_name_analise;
        private Dictionary<long, Analise> analises;

        private void init_Eventos()
        {
            evento_analise_adicionada += new eventoEventHandler(
                Camada_de_Interface.InterfaceGuestaoProjectos.addAnaliseReenc);
        }

        //Constructores
        public Projecto(long codProj, String nomeEst, 
            DateTime ultimaAct, Dictionary<long,string> analises)
        {
            codProjecto = codProj;
            nomeEstabelecimento = nomeEst;
            ultimaActualizacao = ultimaAct;
            cod_name_analise = analises;
        }

        public Projecto ()
        {
            codProjecto = -1;
            nomeEstabelecimento = "";
            ultimaActualizacao = new DateTime();
            ultimaActualizacao = DateTime.Now;
            cod_name_analise = new Dictionary<long, string>();
            analises = new Dictionary<long, Analise>();
        }

        public Projecto(Projecto p)
        {
            codProjecto = p.Codigo;
            nomeEstabelecimento = p.Nome;
            ultimaActualizacao = p.Data;
            cod_name_analise = p.Cod_Name_Analise;
            analises = p.Analises;
        }

        //Métodos

        public long Codigo
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
        public List<String> Nomes_Analises
        {
            get 
            {
                return new List<string>(cod_name_analise.Values);
            }
        }
        public Dictionary<long, string> Cod_Name_Analise
        {
            get { return cod_name_analise; }
            set { cod_name_analise = value; }
        }
        public Dictionary<long, Analise> Analises
        {
            get { return analises; }
            set { analises = value; }
        }

        public long getCodigoAnalise(string nome)
        {
            long cod = -1;
            bool found = false;
            for (int i = 0; i < cod_name_analise.Count && !found; i++)
                if (cod_name_analise.Values.ElementAt(i) == nome)
                {
                    cod = cod_name_analise.Keys.ElementAt(i);
                    found = true;
                }
            return cod;
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
            return cod_name_analise.Values.Contains(nomeAnalise);
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
            
            a.Codigo = Camada_de_Dados.DataBaseCommunicator.
                FuncsToDataBase.insertAnalise(codProjecto,a);
            
            cod_name_analise.Add(a.Codigo,a.Nome);
            analises.Add(a.Codigo, a);
            List<string> s = new List<string>();
            s.Add(ETdA.ETdA.getProjecto(codProjecto).Nome);
            s.Add(a.Nome);
            evento_analise_adicionada(s, new EventArgs());
        }

        /*
         * 
         */
        public void abreAnalise(String nomeAnalise)
        {
            long cod = -1;
            Boolean found = false;
            for (int i = 0; i < cod_name_analise.Count && !found; i++)
            {
                KeyValuePair<long, string> p = cod_name_analise.ElementAt(i);
                if (p.Value == nomeEstabelecimento)
                {
                    cod = p.Key;
                    found = true;
                }
            }

            if (!analises.Keys.Contains(cod))
            {
                Analise a = Camada_de_Dados.DataBaseCommunicator.
                    FuncsToDataBase.selectAnalise(cod);
                analises.Add(a.Codigo, a);
            }
        }

        /*
         * Remove Analise
         */
        public void removeAnalise(String nomeAnalise)
        {
            long cod = -1;
            Boolean found = false;
            for (int i = 0; i < cod_name_analise.Count && !found; i++)
            {
                KeyValuePair<long, string> p = cod_name_analise.ElementAt(i);
                if (p.Value == nomeEstabelecimento)
                {
                    cod = p.Key;
                    cod_name_analise.Remove(cod) ;
                    if (analises[cod] != null)
                        analises.Remove(cod);
                    found = true;
                }
            }

            Camada_de_Dados.DataBaseCommunicator.
                FuncsToDataBase.deleteAnalise(cod);
        }

        public void modificaAnalise(Analise a)
        {
            Camada_de_Dados.DataBaseCommunicator.
                FuncsToDataBase.updateAnalise(a);
        }

        /* Fim de Gestao de Analises */
    }
}
