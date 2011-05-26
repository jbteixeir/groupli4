using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdA.Camada_de_Dados.Classes
{
    class Relatorio
    {
        //Variaveis de Instancia
       // private List<JFreeChart>;
        private List<String> cores;
        private List<String> observacoes;

        //Constructores

        //Métodos

        public List<String> Cores
        {
            get { return cores; }
            set { cores = value; }
        }

        public List<String> Obs
        {
            get { return observacoes; }
            set { observacoes = value; }
        }

        //void adicionarObservacao(String codParametro, String obs);
        
    }
}
