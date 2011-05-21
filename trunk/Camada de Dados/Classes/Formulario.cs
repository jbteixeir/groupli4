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
    }
}
