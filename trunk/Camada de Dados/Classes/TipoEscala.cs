using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdAnalyser.CamadaDados.Classes
{
    class TipoEscala
    {
        //Variáveis de instância
        long codigoTipo;
        string descricao;
        int numero;
        // 0 - TextBox -> Letras
        // 1 - TextBox -> Numeros
        // >1 - RadioButons 
        // -1 - Sugestao
        // -2 - CheckButons
        int tipoPredefinido;

        List<EscalaResposta> respostas;

        //Constructores
        public TipoEscala(long cod, String desc, int num, int default_t,List<EscalaResposta> resps)
        {
            codigoTipo = cod;
            descricao = desc;
            numero = num;
            tipoPredefinido = default_t;
            respostas = resps;
        }

        public TipoEscala(String desc, int num)
        {
            descricao = desc;
            numero = num;
            tipoPredefinido = 0;
            respostas = new List<EscalaResposta>();
        }

        public TipoEscala()
        {
            codigoTipo = -1;
            descricao = "";
            numero = 0;
            tipoPredefinido = 0;
            respostas = new List<EscalaResposta>();
        }

        public TipoEscala(TipoEscala t)
        {
            codigoTipo = t.CodigoTipo;
            descricao = t.Descricao;
            numero = t.Numero;
            tipoPredefinido = t.TipoPredefinido;
            respostas = t.Respostas;
        }

        //Métodos
        public long CodigoTipo
        {
            get { return codigoTipo; }
            set { codigoTipo = value; }
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
        public int TipoPredefinido
        {
            get { return tipoPredefinido; }
            set { tipoPredefinido = value; }
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
            a.Append("CodTipo: " + codigoTipo.ToString() + "\n");
            a.Append("Descrição: " + descricao + "\n");
            a.Append("Número: " + numero.ToString() + "\n");
            a.Append("Default: " + tipoPredefinido.ToString() + "\n");
            return a.ToString();
        }
    }
}
