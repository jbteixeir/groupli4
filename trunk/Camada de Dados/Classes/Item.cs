using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdA.Camada_de_Dados.Classes
{
    class Item
    {
        //Variaveis de Instancia
        private int cod_item;
        private String nome_item;
        private int defaultItem;
        private float ponderacao_analista;
        private float ponderacao_profissional;
        private float ponderacao_cliente;
        private float inter_vemelho;
        private float inter_laranja;
        private float inter_amarelo;
        private float inter_verdelima;
        private float inter_verde;
        private float limite_inferior_analista;
        
        //Constructores

        public Item(int cod, String nome, int def, float ponderacao_analista, float ponderacao_profissional, float ponderacao_cliente, float inter_vemelho, float inter_laranja, float inter_amarelo, float inter_verdelima, float inter_verde, float limite_inferior_analista)
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
        public int CodigoItem
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
        
        public float PonderacaoAnalista
        {
            get { return ponderacao_analista; }
            set { ponderacao_analista = value; }
        }
        public float PonderacaoProfissional
        {
            get { return ponderacao_profissional; }
            set { ponderacao_profissional = value; }
        }
        public float PonderacaoCliente
        {
            get { return ponderacao_cliente; }
            set { ponderacao_cliente = value; }
        }
        public float LimiteInferiorAnalista
        {
            get { return limite_inferior_analista; }
            set { limite_inferior_analista = value; }
        }
        public float Inter_Vermelho
        {
            get { return inter_vemelho; }
            set { inter_vemelho = value; }
        }
        public float Inter_Laranja
        {
            get { return inter_laranja; }
            set { inter_laranja = value; }
        }
        public float Inter_Amarelo
        {
            get { return inter_amarelo; }
            set { inter_amarelo = value; }
        }
        public float Inter_Verde_Lima
        {
            get { return inter_verdelima; }
            set { inter_verdelima = value; }
        }
        public float Inter_Verde
        {
            get { return inter_verde; }
            set { inter_verde = value; }
        }
    }
}
