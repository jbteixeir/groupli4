using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdAnalyser.CamadaDados.Classes
{
    class CheckList : Formulario
    {
        private List<Resposta> respostasNumero;

        public CheckList () : 
        base()
        {
            respostasNumero = new List<Resposta>();
        }

        public CheckList(long _codigoAnalise) :
            base(_codigoAnalise)
        {
            respostasNumero = new List<Resposta>();
        }

        /* Gets\Sets */

        public List<Resposta> RespostasNumero
        {
            get
            {
                List<Resposta> novo = new List<Resposta>();
                foreach (Resposta resposta in respostasNumero)
                    novo.Add(resposta);
                return novo;
            }
            set
            {
                respostasNumero = new List<Resposta>();
                foreach (Resposta resposta in value)
                    respostasNumero.Add(resposta);
            }
        }

        /* Metodos */

        public void adicionarRespostaNumero(Resposta resposta)
        {
            respostasNumero.Add(resposta);
        }
    }
}
