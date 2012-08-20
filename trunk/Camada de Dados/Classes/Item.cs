using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdAnalyser.Camada_de_Dados.Classes
{
    class Item
    {
        //Variaveis de Instancia
        private long cod_item;
        private String nome_item;
        private int defaultItem;
        private double ponderacao_analista;
        private double ponderacao_profissional;
        private double ponderacao_cliente;
        private double inter_vemelho;
        private double inter_laranja;
        private double inter_amarelo;
        private double inter_verdelima;
        private double inter_verde;
        private double limite_inferior_analista;
        
        //Constructores

        public Item(long cod, String nome, int def, double ponderacao_analista, double ponderacao_profissional, double ponderacao_cliente, double inter_vemelho, double inter_laranja, double inter_amarelo, double inter_verdelima, double inter_verde, double limite_inferior_analista)
        {
            cod_item = cod;
            nome_item = nome;
            defaultItem = def;
            this.ponderacao_analista = ponderacao_analista;
            this.ponderacao_profissional=ponderacao_profissional;
            this.ponderacao_cliente = ponderacao_cliente;
            this.inter_vemelho = inter_vemelho;
            this.inter_laranja = inter_laranja;
            this.inter_amarelo = inter_amarelo;
            this.inter_verdelima = inter_verdelima;
            this.inter_verde = inter_verde;
            this.limite_inferior_analista = limite_inferior_analista;
        }

        public Item()
        {
            cod_item = -1;
            nome_item = "";
            defaultItem = 0;
            ponderacao_analista = 0.33f;
            ponderacao_profissional = 0.33f;
            ponderacao_cliente = 0.33f;
            inter_vemelho = 1;
            inter_laranja = 2;
            inter_amarelo = 3;
            inter_verdelima = 4;
            inter_verde = 5;
            limite_inferior_analista = 0;
        }

        //Métodos
        public long CodigoItem
        {
            get { return cod_item; }
            set { cod_item = value; }
        }
        public String NomeItem
        {
            get { return nome_item; }
            set { nome_item = value; }
        }
        public int Default
        {
            get { return defaultItem; }
            set { defaultItem = value; }
        }
        
        public double PonderacaoAnalista
        {
            get { return ponderacao_analista; }
            set { ponderacao_analista = value; }
        }
        public double PonderacaoProfissional
        {
            get { return ponderacao_profissional; }
            set { ponderacao_profissional = value; }
        }
        public double PonderacaoCliente
        {
            get { return ponderacao_cliente; }
            set { ponderacao_cliente = value; }
        }
        public double LimiteInferiorAnalista
        {
            get { return limite_inferior_analista; }
            set { limite_inferior_analista = value; }
        }
        public double Inter_Vermelho
        {
            get { return inter_vemelho; }
            set { inter_vemelho = value; }
        }
        public double Inter_Laranja
        {
            get { return inter_laranja; }
            set { inter_laranja = value; }
        }
        public double Inter_Amarelo
        {
            get { return inter_amarelo; }
            set { inter_amarelo = value; }
        }
        public double Inter_Verde_Lima
        {
            get { return inter_verdelima; }
            set { inter_verdelima = value; }
        }
        public double Inter_Verde
        {
            get { return inter_verde; }
            set { inter_verde = value; }
        }
    }
}
