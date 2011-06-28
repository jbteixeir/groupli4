using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdA.Camada_de_Dados.Classes
{
    class ResultadoItem
    {
        //resultado do questionário
        private double resultado_questionario_geral;
        private Dictionary<int, double> resultado_questionario_parcial;

        //resultado da ficha de avaliação
        private double resultado_fichaAvaliacao_geral;
        private Dictionary<int, double> resultado_fichaAvaliacao_parcial;

        //resultado da checklist
        private double resultado_checklist_geral;
        private Dictionary<int, double> resultado_checklist_parcial;

        //observação do analista para um Item
        private string observacao;

        //resultado final
        private double resultado_final;

        //Se a observação já foi preenchida
        private int checkObs;

        //mostrar ou não os resultados parciais
        private int mostraResParc;

        public ResultadoItem(double resultado_questionario_geral, Dictionary<int, double> resultado_questionario_parcial, double resultado_fichaAvaliacao_geral, Dictionary<int, double> resultado_fichaAvaliacao_parcial, double resultado_checklist_geral, Dictionary<int, double> resultado_checklist_parcial, string observacao, double resultado_final)
        {
            this.resultado_questionario_geral = resultado_questionario_geral;
            this.resultado_questionario_parcial=resultado_questionario_parcial;

            this.resultado_fichaAvaliacao_geral=resultado_fichaAvaliacao_geral;
            this.resultado_fichaAvaliacao_parcial=resultado_fichaAvaliacao_parcial;

            this.resultado_checklist_geral=resultado_checklist_geral;
            this.resultado_checklist_parcial=resultado_checklist_parcial;

            this.observacao = observacao;

            this.resultado_final = resultado_final;

            this.checkObs = 0;
        }

        public ResultadoItem()
        {
            this.resultado_questionario_geral = -1;
            this.resultado_questionario_parcial = new Dictionary<int, double>();

            this.resultado_fichaAvaliacao_geral = -1;
            this.resultado_fichaAvaliacao_parcial = new Dictionary<int,double>();

            this.resultado_checklist_geral = -1;
            this.resultado_checklist_parcial = new Dictionary<int,double>();

            observacao = "";

            resultado_final = -1;

            this.checkObs = 0;
        }

        //Métodos
        public double ResultadoQuestionarioGeral
        {
            get { return resultado_questionario_geral; }
            set { resultado_questionario_geral = value; }
        }

        public Dictionary<int, double> ResultadoQuestionarioParcial
        {
            get { return resultado_questionario_parcial; }
            set { resultado_questionario_parcial = value; }
        }

        public double ResultadoFichaAvaliacaoGeral
        {
            get { return resultado_fichaAvaliacao_geral; }
            set { resultado_fichaAvaliacao_geral = value; }
        }

        public Dictionary<int, double> ResultadoFichaAvaliacaoParcial
        {
            get { return resultado_fichaAvaliacao_parcial; }
            set { resultado_fichaAvaliacao_parcial = value; }
        }

        public double ResultadoCheckListGeral
        {
            get { return resultado_checklist_geral; }
            set { resultado_checklist_geral = value; }
        }

        public Dictionary<int, double> ResultadoCheckListParcial
        {
            get { return resultado_checklist_parcial; }
            set { resultado_checklist_parcial = value; }
        }
        public string Observacao
        {
            get { return observacao; }
            set { observacao = value;
                  checkObs = 1;      }
        }

        public double ResultadoFinal
        {
            get { return resultado_final; }
            set { resultado_final = value; }
        }

        public int CheckObservacoes
        {
            get { return checkObs; }
            set { checkObs = value; }
        }

        public int mostraResultadosParciais
        {
            get { return mostraResParc; }
            set { mostraResParc = value; }
        }
    }
}
