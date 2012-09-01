using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdAnalyser.CamadaDados.Classes
{
    class Item
    {
        //Variaveis de Instancia
        private long codigoItem;
        private String nomeItem;
        private int defaultItem;
        private double ponderacaoAnalista;
        private double ponderacaoProfissional;
        private double ponderacaoCliente;
        private double intervaloVemelho;
        private double intervaloLaranja;
        private double intervaloAmarelo;
        private double intervaloVerdelima;
        private double intervaloVerde;
        private double limiteInferiorAnalista;
        
        //Constructores

        public Item(long cod, String nome, int def, double ponderacao_analista, double ponderacao_profissional, double ponderacao_cliente, double inter_vemelho, double inter_laranja, double inter_amarelo, double inter_verdelima, double inter_verde, double limite_inferior_analista)
        {
            codigoItem = cod;
            nomeItem = nome;
            defaultItem = def;
            this.ponderacaoAnalista = ponderacao_analista;
            this.ponderacaoProfissional=ponderacao_profissional;
            this.ponderacaoCliente = ponderacao_cliente;
            this.intervaloVemelho = inter_vemelho;
            this.intervaloLaranja = inter_laranja;
            this.intervaloAmarelo = inter_amarelo;
            this.intervaloVerdelima = inter_verdelima;
            this.intervaloVerde = inter_verde;
            this.limiteInferiorAnalista = limite_inferior_analista;
        }

        public Item()
        {
            codigoItem = -1;
            nomeItem = "";
            defaultItem = 0;
            ponderacaoAnalista = 0.33f;
            ponderacaoProfissional = 0.33f;
            ponderacaoCliente = 0.33f;
            intervaloVemelho = 1;
            intervaloLaranja = 2;
            intervaloAmarelo = 3;
            intervaloVerdelima = 4;
            intervaloVerde = 5;
            limiteInferiorAnalista = 0;
        }

        //Métodos
        public long CodigoItem
        {
            get { return codigoItem; }
            set { codigoItem = value; }
        }
        public String NomeItem
        {
            get { return nomeItem; }
            set { nomeItem = value; }
        }
        public int Default
        {
            get { return defaultItem; }
            set { defaultItem = value; }
        }
        
        public double PonderacaoAnalista
        {
            get { return ponderacaoAnalista; }
            set { ponderacaoAnalista = value; }
        }
        public double PonderacaoProfissional
        {
            get { return ponderacaoProfissional; }
            set { ponderacaoProfissional = value; }
        }
        public double PonderacaoCliente
        {
            get { return ponderacaoCliente; }
            set { ponderacaoCliente = value; }
        }
        public double LimiteInferiorAnalista
        {
            get { return limiteInferiorAnalista; }
            set { limiteInferiorAnalista = value; }
        }
        public double IntervaloVermelho
        {
            get { return intervaloVemelho; }
            set { intervaloVemelho = value; }
        }
        public double IntervaloLaranja
        {
            get { return intervaloLaranja; }
            set { intervaloLaranja = value; }
        }
        public double IntervaloAmarelo
        {
            get { return intervaloAmarelo; }
            set { intervaloAmarelo = value; }
        }
        public double IntervaloVerdeLima
        {
            get { return intervaloVerdelima; }
            set { intervaloVerdelima = value; }
        }
        public double IntervaloVerde
        {
            get { return intervaloVerde; }
            set { intervaloVerde = value; }
        }
    }
}
