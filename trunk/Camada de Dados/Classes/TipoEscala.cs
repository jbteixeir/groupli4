using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdA.Camada_de_Dados.Classes
{
    class TipoEscala
    {
        //Variáveis de instância
        long codTipo;
        string descricao;
        int numero;
        // 0 - TextBox -> Letras
        // 1 - TextBox -> Numeros
        // >1 - RadioButons 
        // -1 - Sugestao
        // -2 - CheckButons
        int default_tipo;

        List<EscalaResposta> respostas;

        //Constructores
        public TipoEscala(long cod, String desc, int num, int default_t,List<EscalaResposta> resps)
        {
            codTipo = cod;
            descricao = desc;
            numero = num;
            default_tipo = default_t;
            respostas = resps;
        }

        public TipoEscala(String desc, int num)
        {
            descricao = desc;
            numero = num;
            default_tipo = 0;
            respostas = new List<EscalaResposta>();
        }

        public TipoEscala()
        {
            codTipo = -1;
            descricao = "";
            numero = 0;
            default_tipo = 0;
            respostas = new List<EscalaResposta>();
        }

        public TipoEscala(TipoEscala t)
        {
            codTipo = t.Codigo;
            descricao = t.Descricao;
            numero = t.Numero;
            default_tipo = t.Default;
            respostas = t.Respostas;
        }

        //Métodos
        public long Codigo
        {
            get { return codTipo; }
            set { codTipo = value; }
        }

        public String Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }
        public int Numero
        {
            get { return numero; }
            set { numero = value; }
        }
        public int Default
        {
            get { return default_tipo; }
            set { default_tipo = value; }
        }
        public List<EscalaResposta> Respostas
        {
            get 
            {
                List<EscalaResposta> resps = new List<EscalaResposta>();
                foreach (EscalaResposta er in respostas)
                    resps.Add(er.clone());
                return resps;
            }
            set { respostas = value; }
        }

        public string ToString()
        {
            StringBuilder a = new StringBuilder();
            a.Append("Tipo Resposta:\n");
            a.Append("CodTipo: " + codTipo.ToString() + "\n");
            a.Append("Descrição: " + descricao + "\n");
            a.Append("Número: " + numero.ToString() + "\n");
            a.Append("Default: " + default_tipo.ToString() + "\n");
            return a.ToString();
        }
    }
}
