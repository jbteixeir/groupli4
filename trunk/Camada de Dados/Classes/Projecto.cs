using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ETdAnalyser.CamadaDados;
using ETdAnalyser.CamadaDados.Classes.Estruturas;
using System.Windows;

namespace ETdAnalyser.CamadaDados.Classes
{
    class Projecto
    {
        private delegate void eventoEventHandler(object sender, EventArgs e);

        private static event eventoEventHandler EventoAnaliseAdicionada;
        //private static event eventoEventHandler evento_analise_removida;
        
        //Variaveis de instância
        private long codigoProjecto;
        private String nomeEstabelecimento;
        private DateTime ultimaActualizacao;
        private Dictionary<long,string> codigoNomeAnalise;
        private Dictionary<long, Analise> analises;

        // s_final
        private void iniciarEventos()
        {
            EventoAnaliseAdicionada += new eventoEventHandler(
                CamadaInterface.InterfaceGuestaoProjectos.addAnaliseReenc);
        }

        #region Construtores
        public Projecto(long codigoProjectoroj, String nomeEst, 
            DateTime ultimaAct, Dictionary<long,string> analises)
        {
            codigoProjecto = codigoProjectoroj;
            nomeEstabelecimento = nomeEst;
            ultimaActualizacao = ultimaAct;
            codigoNomeAnalise = analises;
            this.analises = new Dictionary<long, Analise>();
            iniciarEventos();
        }

        public Projecto ()
        {
            codigoProjecto = -1;
            nomeEstabelecimento = "";
            ultimaActualizacao = new DateTime();
            ultimaActualizacao = DateTime.Now;
            codigoNomeAnalise = new Dictionary<long, string>();
            analises = new Dictionary<long, Analise>();
            iniciarEventos();
        }

        public Projecto(Projecto p)
        {
            codigoProjecto = p.Codigo;
            nomeEstabelecimento = p.Nome;
            ultimaActualizacao = p.Data;
            codigoNomeAnalise = p.CodigoNomeAnalise;
            analises = p.Analises;
            iniciarEventos();
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
        public Dictionary<long, string> CodigoNomeAnalise
        {
            get
            { 
                Dictionary<long,string> _new = new Dictionary<long,string>();
                for (int i = 0; i < codigoNomeAnalise.Count; i++)
                    _new.Add(codigoNomeAnalise.ElementAt(i).Key, codigoNomeAnalise.ElementAt(i).Value);
                return _new;
            }
            set { codigoNomeAnalise = value; }
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
        public Boolean PodeAdicionarAnalise(String nomeAnalise)
        {
            return !codigoNomeAnalise.Values.Contains(nomeAnalise);
        }

        /*
         * Adiciona uma nova Analise
         */
        // s_final
        public void AdicionarAnalise(String tipoAnalise, 
            String nomeAnalise, List<Zona> zonas, List<Item> itens)
        {
            Analise a = new Analise();
            a.Tipo = tipoAnalise;
            a.Nome = nomeAnalise;
            a.Zonas = zonas;
            a.Itens = itens;
            
            a.Codigo = CamadaDados.DataBaseCommunicator.
                FuncsToDataBase.InsertAnalise(codigoProjecto,a);
            
            codigoNomeAnalise.Add(a.Codigo,a.Nome);
            analises.Add(a.Codigo, a);

            List<string> s = new List<string>();
            s.Add(codigoProjecto.ToString());
            s.Add(a.Codigo.ToString());
            s.Add(a.Nome);
            EventoAnaliseAdicionada(s, new EventArgs());
        }

        // s_final
        public void AbrirAnalise(long codigoAnalise)
        {
            if (!analises.Keys.Contains(codigoAnalise))
            {
                Analise a = CamadaDados.DataBaseCommunicator.
                    FuncsToDataBase.SelectAnalise(codigoAnalise);
                analises.Add(a.Codigo, a);
            }
        }

        public void RemoverAnalise(String nomeAnalise)
        {
            long cod = -1;
            Boolean found = false;
            for (int i = 0; i < codigoNomeAnalise.Count && !found; i++)
            {
                KeyValuePair<long, string> p = codigoNomeAnalise.ElementAt(i);
                if (p.Value == nomeAnalise)
                {
                    cod = p.Key;
                    codigoNomeAnalise.Remove(cod) ;
                    if (analises[cod] != null)
                        analises.Remove(cod);
                    found = true;
                }
            }

            CamadaDados.DataBaseCommunicator.
                FuncsToDataBase.DeleteAnalise(cod);
        }

        public void RemoveAnalise(long codigoAnalise)
        {
            codigoNomeAnalise.Remove(codigoAnalise);
            analises.Remove(codigoAnalise);
        }

        public void AlterarAnalise(Analise a)
        {
            CamadaDados.DataBaseCommunicator.
                FuncsToDataBase.UpdateAnalise(a);
        }

    }
}
