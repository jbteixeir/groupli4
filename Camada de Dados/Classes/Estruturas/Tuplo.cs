using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdA.Camada_de_Dados.Classes.Estruturas
{
    class Tuplo<Object1,Object2>
    {
        private Object1 objecto1;
        private Object2 objecto2;

        /*public Tuplo()
        {
            objecto1 = null;
            objecto2 = null;
        }*/

        public Tuplo(Object1 o1, Object2 o2)
        {
            objecto1 = o1;
            objecto2 = o2;
        }

        public Tuplo(Tuplo<Object1,Object2> t)
        {
            objecto1 = t.Fst;
            objecto2 = t.Snd;
        }

        public Object1 Fst
        {
            get { return objecto1; }
            set { objecto1 = value; }
        }

        public Object2 Snd
        {
            get { return objecto2; }
            set { objecto2 = value; }
        }

        public Tuplo<Object1,Object2> clone()
        {
            return new Tuplo<Object1,Object2>(this);
        }
    }
}
