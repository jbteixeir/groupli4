using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdA.Camada_de_Dados
{
    public class Analise
    {
        //Variáveis de Instância
        private String codAnalise;
        private DateTime dataAnalise;
        private String tipoAnalise;
        private Boolean estadoWebsite;
        private String nomeAnalise;
        private Formulario checkList;
        private Formulario fichaAvaliacao;
        private Formulario questionario;
        private List<Item> itens;
    
        //Constructores

        public Analise(String cod, DateTime data, String tipo, Boolean estado, String nome)
        {
            codAnalise = cod;
            dataAnalise = data;
            tipoAnalise = tipo;
            estadoWebsite = estado;
            nomeAnalise = nome;
        }

        public Analise()
        {
            codAnalise = "";
            dataAnalise = new DateTime();
            tipoAnalise = "";
            estadoWebsite = true;
            nomeAnalise = "";
            checkList = new Formulario();
            fichaAvaliacao = new Formulario();
            questionario = new Formulario();
            itens = new List<Item>();
        }

        public Analise(Analise a)
        {
            codAnalise = a.Codigo;
            dataAnalise = a.Data;
            tipoAnalise = a.Tipo;
            estadoWebsite = a.Estado;
            nomeAnalise = a.Nome;

        }

        //Métodos

        public String Codigo
        {
            get { return codAnalise; }
            set { codAnalise = value; }
        }
        
        public DateTime Data
	    {
		    get { return dataAnalise;}
		    set { dataAnalise = value;}
	    }
	
        
        public String Tipo
	    {
		    get { return tipoAnalise;}
		    set { tipoAnalise = value;}
	    }

        public Boolean Estado
        {
            get { return estadoWebsite;}
            set { estadoWebsite = value;}
        }

        public String Nome
        {
            get { return nomeAnalise;}
            set { nomeAnalise = value;}
        }

        public Formulario CheckList
        {
            get { return checkList; }
            set { checkList = value; }
        }

        public Formulario FichaAvaliacao
        {
            get { return fichaAvaliacao; }
            set { fichaAvaliacao = value; }
        }

        public Formulario Questionario
        {
            get { return questionario; }
            set { questionario = value; }
        }

        public List<Item> Itens
        {
            get { return itens; }
            set { itens = value; }
        }


        public Analise clone()
        {
            return new Analise(this);
        }

        /* ------------------------------------------------------ */
        /* Metodos de Gestao */
        /* ------------------------------------------------------ */

        /* Gestao Itens */

        public void adicionaNovoItem(String nome)
        {
            Item i = new Item();
            i.Nome = nome;

            itens.Add(i);
        }

        public void adicionaItem(Item i)
        {
            itens.Add(i);
        }

        public void

        /* Fim Gestao Itens */


    }
}


