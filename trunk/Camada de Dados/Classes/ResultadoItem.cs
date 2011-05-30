using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdA.Camada_de_Dados.Classes
{
    class ResultadoItem
    {
        private float resultado_questionario_geral;
        private Dictionary<int, float> resultado_questionario_parcial;

        private float resultado_fichaAvaliacao_geral;
        private Dictionary<int, float> resultado_fichaAvaliacao_parcial;

        private float resultado_checklist_geral;
        private Dictionary<int, float> resultado_checklist_parcial;

        private string observacao;

        private float resultado_final;

        private int checkObs;

        public ResultadoItem(float resultado_questionario_geral, Dictionary<int, float> resultado_questionario_parcial, float resultado_fichaAvaliacao_geral, Dictionary<int, float> resultado_fichaAvaliacao_parcial, float resultado_checklist_geral, Dictionary<int, float> resultado_checklist_parcial, string observacao, float resultado_final)
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
            this.resultado_questionario_parcial = new Dictionary<int, float>();

            this.resultado_fichaAvaliacao_geral = -1;
            this.resultado_fichaAvaliacao_parcial = new Dictionary<int,float>();

            this.resultado_checklist_geral = -1;
            this.resultado_checklist_parcial = new Dictionary<int,float>();

            observacao = "";

            resultado_final = -1;

            this.checkObs = 0;
        }

        //Métodos
        public float ResultadoQuestionarioGeral
        {
            get { return resultado_questionario_geral; }
            set { resultado_questionario_geral = value; }
        }

        public Dictionary<int, float> ResultadoQuestionarioParcial
        {
            get { return resultado_questionario_parcial; }
            set { resultado_questionario_parcial = value; }
        }

        public float ResultadoFichaAvaliacaoGeral
        {
            get { return resultado_fichaAvaliacao_geral; }
            set { resultado_fichaAvaliacao_geral = value; }
        }

        public Dictionary<int, float> ResultadoFichaAvaliacaoParcial
        {
            get { return resultado_fichaAvaliacao_parcial; }
            set { resultado_fichaAvaliacao_parcial = value; }
        }

        public float ResultadoCheckListGeral
        {
            get { return resultado_checklist_geral; }
            set { resultado_checklist_geral = value; }
        }

        public Dictionary<int, float> ResultadoCheckListParcial
        {
            get { return resultado_checklist_parcial; }
            set { resultado_checklist_parcial = value; }
        }
        public string Observacao
        {
            get { return observacao; }
            set { observacao = value; }
        }

        public float ResultadoFinal
        {
            get { return resultado_final; }
            set { resultado_final = value; }
        }

        public int CheckObservacoes
        {
            get { return checkObs; }
            set { checkObs = value; }
        }
    }
}
