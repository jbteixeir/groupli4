using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdAnalyser.Camada_de_Dados.Classes
{
    class Analise
    {
        //Variáveis de Instância
        private long codigoAnalise;
        private long codigoProjecto;
        private DateTime dataAnalise;
        private String nomeAnalise;
        private String tipoAnalise;
        private List<Zona> zonas;
        private List<Item> itens;
        private Boolean estadoWebsiteCheckList;
        private Boolean estadoWebsiteFichaAvaliacao;
        private Boolean estadoWebsiteQuestionario;
        private Formulario checkList;
        private List<FichaAvaliacao> fichasAvaliacao;
        private List<Questionario> questionarios;
		//private Relatorio relatorio;

        //Constructores

        public Analise(long cod, long codigoProjectoroj, DateTime data, String nome,
            String tipo, List<Zona> zonas, List<Item> itens, 
            Boolean estadoWebCL, Boolean estadoWebFA, Boolean estadoWebQ)
        {
            codigoAnalise = cod;
            codigoProjecto = codigoProjectoroj;
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
            codigoAnalise = -1;
            codigoProjecto = -1;
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
            fichasAvaliacao = new List<FichaAvaliacao>();
            questionarios = new List<Questionario>();
        }

        public Analise(Analise a)
        {
            codigoAnalise = a.Codigo;
            codigoProjecto = a.CodigoProj;
            dataAnalise = a.Data;
            nomeAnalise = a.Nome;
            tipoAnalise = a.Tipo;
            zonas = a.Zonas;
            itens = a.Itens;
            estadoWebsiteCheckList = a.EstadoWebCL;
            estadoWebsiteFichaAvaliacao = a.EstadoWebFA;
            estadoWebsiteQuestionario = a.EstadoWebQ;
            fichasAvaliacao = a.FichasAvaliacao;
            questionarios = a.Questionarios;
        }

        //Métodos

        public long Codigo
        {
            get { return codigoAnalise; }
            set { codigoAnalise = value; }
        }
        public long CodigoProj
        {
            get { return codigoProjecto; }
            set { codigoProjecto = value; }
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
        public List<FichaAvaliacao> FichasAvaliacao
        {
            get { return fichasAvaliacao; }
            set { fichasAvaliacao = value; }
        }
        public List<Questionario> Questionarios
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
    }
}


