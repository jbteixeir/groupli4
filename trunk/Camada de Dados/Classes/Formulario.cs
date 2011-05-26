using System.Collections.Generic;
using System;

namespace ETdA.Camada_de_Dados.Classes
{
    class Formulario
    {
        //Variaveis de Instancia
        private String tipo;
        private String codFormulario;
        private List<Questao> perguntas;
        private List<EscalaResposta> opcoesResposta;
        private List<Resposta> respostas;

        //Constructores
        public Formulario()
        {
            tipo = "";
            codFormulario = "";
        }

        public Formulario(String type, String codForm)
        {
            tipo = type;
            codFormulario = codForm;
        }

        //Métodos

        public String Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        public String Codigo
        {
            get { return codFormulario; }
            set { codFormulario = value; }
        }
    }
}
