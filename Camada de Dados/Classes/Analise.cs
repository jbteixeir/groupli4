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
        }
       
        }
}
