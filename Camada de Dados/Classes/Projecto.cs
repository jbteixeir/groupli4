using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ETdAnalyser.Camada_de_Dados;
using ETdAnalyser.Camada_de_Dados.Classes.Estruturas;
using System.Windows;

namespace ETdAnalyser.Camada_de_Dados.Classes
{
    class Projecto
    {
        private delegate void eventoEventHandler(object sender, EventArgs e);

        private static event eventoEventHandler evento_analise_adicionada;
        //private static event eventoEventHandler evento_analise_removida;
        
        //Variaveis de instância
        private long codigoProjecto;
        private String nomeEstabelecimento;
        private DateTime ultimaActualizacao;
        private Dictionary<long,string> cod_name_analise;
        private Dictionary<long, Analise> analises;

        // s_final
        private void init_Eventos()
        {
            evento_analise_adicionada += new eventoEventHandler(
                Camada_de_Interface.InterfaceGuestaoProjectos.addAnaliseReenc);
        }

        #region Construtores
        public Projecto(long codigoProjectoroj, String nomeEst, 
            DateTime ultimaAct, Dictionary<long,string> analises)
        {
            codigoProjecto = codigoProjectoroj;
            nomeEstabelecimento = nomeEst;
            ultimaActualizacao = ultimaAct;
            cod_name_analise = analises;
            this.analises = new Dictionary<long, Analise>();
            init_Eventos();
        }

        public Projecto ()
        {
            codigoProjecto = -1;
            nomeEstabelecimento = "";
            ultimaActualizacao = new DateTime();
            ultimaActualizacao = DateTime.Now;
            cod_name_analise = new Dictionary<long, string>();
            analises = new Dictionary<long, Analise>();
            init_Eventos();
        }

        public Projecto(Projecto p)
        {
            codigoProjecto = p.Codigo;
            nomeEstabelecimento = p.Nome;
            ultimaActualizacao = p.Data;
            cod_name_analise = p.Cod_Name_Analise;
            analises = p.Analises;
            init_Eventos();
        }
        #endregion

        #region Gets/Sets
        public long Codigo
        {
            get { return codigoProjecto; }
            set { codigoProjecto = value; }
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
        public Dictionary<long, string> Cod_Name_Analise
        {
            get
            { 
                Dictionary<long,string> _new = new Dictionary<long,string>();
                for (int i = 0; i < cod_name_analise.Count; i++)
                    _new.Add(cod_name_analise.ElementAt(i).Key, cod_name_analise.ElementAt(i).Value);
                return _new;
            }
            set { cod_name_analise = value; }
        }
        public Dictionary<long, Analise> Analises
        {
            get { return new Dictionary<long,Analise>(analises); }
            set { analises = value; }
        }
        #endregion

        public Projecto clone()
        {
            return new Projecto(this);
        }

        /* ------------------------------------------------------ */
        /* Metodos */
        /* ------------------------------------------------------ */

        /*
         * Verifica se já existe alguma análise com o nome recebido
         */
        // s_final
        public Boolean podeAdicionarAnalise(String nomeAnalise)
        {
            return !cod_name_analise.Values.Contains(nomeAnalise);
        }

        /*
         * Adiciona uma nova Analise
         */
        // s_final
        public void adicionaNovaAnalise(String tipoAnalise, 
            String nomeAnalise, List<Zona> zonas, List<Item> itens)
        {
            Analise a = new Analise();
            a.Tipo = tipoAnalise;
            a.Nome = nomeAnalise;
            a.Zonas = zonas;
            a.Itens = itens;
            
            a.Codigo = Camada_de_Dados.DataBaseCommunicator.
                FuncsToDataBase.insertAnalise(codigoProjecto,a);
            
            cod_name_analise.Add(a.Codigo,a.Nome);
            analises.Add(a.Codigo, a);

            List<string> s = new List<string>();
            s.Add(codigoProjecto.ToString());
            s.Add(a.Codigo.ToString());
            s.Add(a.Nome);
            evento_analise_adicionada(s, new EventArgs());
        }

        // s_final
        public void abreAnalise(long codigoAnalise)
        {
            if (!analises.Keys.Contains(codigoAnalise))
            {
                Analise a = Camada_de_Dados.DataBaseCommunicator.
                    FuncsToDataBase.selectAnalise(codigoAnalise);
                analises.Add(a.Codigo, a);
            }
        }

        public void removeAnalise(String nomeAnalise)
        {
            long cod = -1;
            Boolean found = false;
            for (int i = 0; i < cod_name_analise.Count && !found; i++)
            {
                KeyValuePair<long, string> p = cod_name_analise.ElementAt(i);
                if (p.Value == nomeAnalise)
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

        public void removeAnalise(long codigoAnalise)
        {
            cod_name_analise.Remove(codigoAnalise);
            analises.Remove(codigoAnalise);
        }

        public void modificaAnalise(Analise a)
        {
            Camada_de_Dados.DataBaseCommunicator.
                FuncsToDataBase.updateAnalise(a);
        }

    }
}
