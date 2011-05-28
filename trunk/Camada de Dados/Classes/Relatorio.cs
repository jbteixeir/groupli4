//using System;
//using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System;

namespace ETdA.Camada_de_Dados
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

        public void gerarResultadosRelatorio(int codigoAnalise, List<Classes.Resposta> respostas)
        {
            DataBaseCommunicator.FuncsToDataBase.selectRespostaCheckList(codigoAnalise, respostas);
            DataBaseCommunicator.FuncsToDataBase.selectRespostaFichaAvaliacao(codigoAnalise, respostas);
            DataBaseCommunicator.FuncsToDataBase.selectRespostaQuestionario(codigoAnalise, respostas);


        }
    }
}
