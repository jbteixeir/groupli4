using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdAnalyser.Camada_de_Dados.Classes
{
    class CheckList : Formulario
    {
        private List<Resposta> respostas_numero;

        public CheckList () : 
        base()
        {
            respostas_numero = new List<Resposta>();
        }

        public CheckList(long _cod_analise) :
            base(_cod_analise)
        {
            respostas_numero = new List<Resposta>();
        }

        /* Gets\Sets */

        public List<Resposta> Respostas_Numero
        {
            get
            {
                List<Resposta> novo = new List<Resposta>();
                foreach (Resposta r in respostas_numero)
                    novo.Add(r);
                return novo;
            }
            set
            {
                respostas_numero = new List<Resposta>();
                foreach (Resposta r in value)
                    respostas_numero.Add(r);
            }
        }

        /* Metodos */

        public void add_resposta_numero(Resposta r)
        {
            respostas_numero.Add(r);
        }
    }
}
