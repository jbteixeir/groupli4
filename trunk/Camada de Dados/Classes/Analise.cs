using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdA.Camada_de_Dados.Classes
{
    class Analise
    {
        //Variáveis de Instância
        private long codAnalise;
        private long codProjecto;
        private DateTime dataAnalise;
        private String nomeAnalise;
        private String tipoAnalise;
        private List<Zona> zonas;
        private List<Item> itens;
        private Boolean estadoWebsiteCheckList;
        private Boolean estadoWebsiteFichaAvaliacao;
        private Boolean estadoWebsiteQuestionario;
        private Formulario checkList;
        private List<Formulario> fichasAvaliacao;
        private List<Formulario> questionarios;
		private Relatorio relatorio;

        //Constructores

        public Analise(long cod, long codProj, DateTime data, String nome,
            String tipo, List<Zona> zonas, List<Item> itens, 
            Boolean estadoWebCL, Boolean estadoWebFA, Boolean estadoWebQ)
        {
            codAnalise = cod;
            codProjecto = codProj;
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
            codAnalise = -1;
            codProjecto = -1;
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
            fichasAvaliacao = new List<Formulario>();
            questionarios = new List<Formulario>();
        }

        public Analise(Analise a)
        {
            codAnalise = a.Codigo;
            codProjecto = a.CodigoProj;
            dataAnalise = a.Data;
            nomeAnalise = a.Nome;
            tipoAnalise = a.Tipo;
            zonas = a.Zonas;
            itens = a.Itens;
            estadoWebsiteCheckList = a.EstadoWebCL;
            estadoWebsiteFichaAvaliacao = a.EstadoWebFA;
            estadoWebsiteQuestionario = a.EstadoWebQ;
            checkList = a.CheckList;
            fichasAvaliacao = a.FichasAvaliacao;
            questionarios = a.Questionarios;
        }

        //Métodos

        public long Codigo
        {
            get { return codAnalise; }
            set { codAnalise = value; }
        }
        public long CodigoProj
        {
            get { return codProjecto; }
            set { codProjecto = value; }
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
        public List<Formulario> FichasAvaliacao
        {
            get { return fichasAvaliacao; }
            set { fichasAvaliacao = value; }
        }
        public List<Formulario> Questionarios
        {
            get { return questionarios; }
            set { questionarios = value; }
        }

        public Analise clone()
        {
            return new Analise(this);
        }

        /* ------------------------------------------------------ */
        /* Metodos de Gestao */
        /* ------------------------------------------------------ */

        /* Introduzir Formularios Manualmente */

        public void insereQuestionario(Formulario q)
        {
            questionarios.Add(q);
        }

        public void insereFichaAvaliaca(Formulario fa)
        {
            
        }

        public void insereCheckList(Formulario cl)
        {
            checkList = cl;
        }
    }
}


