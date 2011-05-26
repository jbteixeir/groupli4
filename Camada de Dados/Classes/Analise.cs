using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdA.Camada_de_Dados.Classes
{
    public class Analise
    {
        //Variáveis de Instância
        private String codAnalise;
        private DateTime dataAnalise;
        private String nomeAnalise;
        private String tipoAnalise;
        private List<Zona> zonas;
        private List<Item> itens;
        private Boolean estadoWebsiteCheckList;
        private Boolean estadoWebsiteFichaAvaliacao;
        private Boolean estadoWebsiteQuestionario;
        private Formulario checkList;
        private Formulario fichaAvaliacao;
        private Formulario questionario;

        //Constructores

        public Analise(String cod, DateTime data, String nome,
            String tipo, List<Zona> zonas, List<Item> itens, 
            Boolean estadoWebCL, Boolean estadoWebFA, Boolean estadoWebQ)
        {
            codAnalise = cod;
            dataAnalise = data;
            nomeAnalise = nome;
            tipoAnalise = tipo;
            this.zonas = zonas;
            this.itens = itens;
            estadoWebsiteCheckList = estadoWebCL;
            estadoWebsiteFichaAvaliacao = estadoWebFA;
            estadoWebsiteQuestionario = estadoWebQ;            
        }

        public Analise()
        {
            codAnalise = "";
            dataAnalise = new DateTime();
            dataAnalise = DateTime.Now;
            nomeAnalise = "";
            tipoAnalise = "";
            zonas = new List<Zona>();
            itens = new List<Item>();
            estadoWebsiteCheckList = false;
            estadoWebsiteFichaAvaliacao = false;
            estadoWebsiteQuestionario = false;
            checkList = new Formulario();
            fichaAvaliacao = new Formulario();
            questionario = new Formulario();
        }

        public Analise(Analise a)
        {
            codAnalise = a.Codigo;
            dataAnalise = a.Data;
            nomeAnalise = a.Nome;
            tipoAnalise = a.Tipo;
            zonas = a.Zonas;
            itens = a.Itens;
            estadoWebsiteCheckList = a.EstadoWebCL;
            estadoWebsiteFichaAvaliacao = a.EstadoWebFA;
            estadoWebsiteQuestionario = a.EstadoWebQ;
            checkList = a.CheckList;
            fichaAvaliacao = a.FichaAvaliacao;
            questionario = a.Questionario;
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
        public String Nome
        {
            get { return nomeAnalise;}
            set { nomeAnalise = value;}
        }       
        public String Tipo
	    {
		    get { return tipoAnalise;}
		    set { tipoAnalise = value;}
	    }
        public List<Zona> Zonas
	    {
		    get { return zonas;}
		    set { zonas = value;}
	    }
        public List<Item> Itens
	    {
		    get { return itens;}
		    set { itens = value;}
	    }
        public Boolean EstadoWebCL
        {
            get { return estadoWebsiteCheckList; }
            set { estadoWebsiteCheckList = value; }
        }
        public Boolean EstadoWebFA
        {
            get { return estadoWebsiteFichaAvaliacao; }
            set { estadoWebsiteFichaAvaliacao = value; }
        }
        public Boolean EstadoWebQ
        {
            get { return estadoWebsiteQuestionario; }
            set { estadoWebsiteQuestionario = value; }
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


