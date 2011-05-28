using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdA.Camada_de_Dados.Classes
{
    class Item
    {
        //Variaveis de Instancia
        private String codParametro;
        private String nomeParametro;
        private int defaultItem;
        private float ponderacao_analista;
        private float ponderacao_profissional;
        private float ponderacao_cliente;
        private float inter_vemelho;
        private float inter_laranja;
        private float inter_amarelo;
        private float inter_verdelima;
        private float inter_verde;

        //Constructores

        public Item(String cod, String nome, int def, float ponderacao_analista, float ponderacao_profissional, float ponderacao_cliente, float inter_vemelho, float inter_laranja, float inter_amarelo, float inter_verdelima, float inter_verde)
        {
            codParametro = cod;
            nomeParametro = nome;
            defaultItem = def;
            this.ponderacao_analista = ponderacao_analista;
            this.ponderacao_profissional=ponderacao_profissional;
            this.ponderacao_cliente = ponderacao_cliente;
            this.inter_vemelho = inter_vemelho;
            this.inter_laranja = inter_laranja;
            this.inter_amarelo = inter_amarelo;
            this.inter_verdelima = inter_verdelima;
            this.inter_verde = inter_verde;
        }

        public Item()
        {
            codParametro = "";
            nomeParametro = "";
            defaultItem = 0;
            ponderacao_analista = 0.33f;
            ponderacao_profissional = 0.33f;
            ponderacao_cliente = 0.33f;
            inter_vemelho = 1;
            inter_laranja = 2;
            inter_amarelo = 3;
            inter_verdelima = 4;
            inter_verde = 5;
        }

        //Métodos
        public String Codigo
        {
            get { return codParametro; }
            set { codParametro = value; }
        }
        public String Nome
        {
            get { return nomeParametro; }
            set { nomeParametro = value; }
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

    }
}
