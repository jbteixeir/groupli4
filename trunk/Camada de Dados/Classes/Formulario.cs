using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdA
{
    class Formulario
    {
        //Variaveis de Instancia
        private String tipo;
        private String codFormulario;

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
