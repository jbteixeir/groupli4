using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdAnalyser.CamadaDados.Classes
{
    class ResultadoItem
    {
        //resultado do questionário
        private double resultadoQuestionarioGeral;
        //contem o numero de pessoas cujo resultado da sua resposta corresponde a uma cor -> primeiro Int escalaResposta a soma Segundo Int
        private Dictionary<int, double> resultadoQuestionarioParcial;

        //resultado da ficha de avaliação
        private double resultadoFichaAvaliacaoGeral;
        //contem o numero de pessoas cujo resultado da sua resposta corresponde a uma cor -> primeiro Int escalaResposta a soma Segundo Int
        private Dictionary<int, double> resultadoFichaAvaliacaoParcial;

        //resultado da checklist
        private double resultadoCheckListGeral;
        //contem o numero de pessoas cujo resultado da sua resposta corresponde a uma cor -> primeiro Int escalaResposta a soma Segundo Int
        private Dictionary<int, double> resultadoCheckListParcial; 

        //observação do analista para um Item
        private string observacao;

        //resultado final
        private double resultadoFinal;

        //Se a observação já foi preenchida
        private bool observacaoPreenchida;

        //mostrar ou não os resultados parciais
        private bool mostrarResultadoParcial;

        public ResultadoItem(double resultado_questionario_geral, Dictionary<int, double> resultado_questionario_parcial, double resultado_fichaAvaliacao_geral, Dictionary<int, double> resultado_fichaAvaliacao_parcial, double resultado_checklist_geral, Dictionary<int, double> resultado_checklist_parcial, string observacao, double resultado_final)
        {
            this.resultadoQuestionarioGeral = resultado_questionario_geral;
            this.resultadoQuestionarioParcial=resultado_questionario_parcial;

            this.resultadoFichaAvaliacaoGeral=resultado_fichaAvaliacao_geral;
            this.resultadoFichaAvaliacaoParcial=resultado_fichaAvaliacao_parcial;

            this.resultadoCheckListGeral=resultado_checklist_geral;
            this.resultadoCheckListParcial=resultado_checklist_parcial;

            this.resultadoFinal = resultado_final;

            this.observacao = observacao;
            this.observacaoPreenchida = false;

            this.mostrarResultadoParcial = false;

        }

        public ResultadoItem()
        {
            this.resultadoQuestionarioGeral = -1;
            this.resultadoQuestionarioParcial = new Dictionary<int, double>();

            this.resultadoFichaAvaliacaoGeral = -1;
            this.resultadoFichaAvaliacaoParcial = new Dictionary<int,double>();

            this.resultadoCheckListGeral = -1;
            this.resultadoCheckListParcial = new Dictionary<int,double>();

            resultadoFinal = -1;

            observacao = "";
            observacaoPreenchida = false;

            mostrarResultadoParcial = false;
        }

        //Métodos
        public double ResultadoQuestionarioGeral
        {
            get { return resultadoQuestionarioGeral; }
            set { resultadoQuestionarioGeral = value; }
        }

        public Dictionary<int, double> ResultadoQuestionarioParcial
        {
            get { return resultadoQuestionarioParcial; }
            set { resultadoQuestionarioParcial = value; }
        }

        public double ResultadoFichaAvaliacaoGeral
        {
            get { return resultadoFichaAvaliacaoGeral; }
            set { resultadoFichaAvaliacaoGeral = value; }
        }

        public Dictionary<int, double> ResultadoFichaAvaliacaoParcial
        {
            get { return resultadoFichaAvaliacaoParcial; }
            set { resultadoFichaAvaliacaoParcial = value; }
        }

        public double ResultadoCheckListGeral
        {
            get { return resultadoCheckListGeral; }
            set { resultadoCheckListGeral = value; }
        }

        public Dictionary<int, double> ResultadoCheckListParcial
        {
            get { return resultadoCheckListParcial; }
            set { resultadoCheckListParcial = value; }
        }
        public string Observacao
        {
            get { return observacao; }
            set { observacao = value;}
        }

        public double ResultadoFinal
        {
            get { return resultadoFinal; }
            set { resultadoFinal = value; }
        }

        public bool ObservacaoPreenchida
        {
            get { return observacaoPreenchida; }
            set { observacaoPreenchida = value; }
        }

        public bool MostrarResultadoParcial
        {
            get { return mostrarResultadoParcial; }
            set { mostrarResultadoParcial = value; }
        }
    }
}
