using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdAnalyser.CamadaDados.Classes
{
    class Zona
    {
        //Variaveis de Instancia
        private long codigo;
        private String nome;

        //Constructores

        public Zona(long cod, String nome)
        {
            codigo = cod;
            nome = nome;
        }

        public Zona()
        {
            codigo = -1;
            nome = "";
        }

        public Zona(Zona i)
        {
            codigo = i.Codigo;
            nome = i.Nome;
        }

        //Métodos
        public long Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        public String Nome
        {
            get { return nome; }
            set { nome = value; }
        }
    }
}
