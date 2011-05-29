using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdA.Camada_de_Interface
{
    class teste
    {
        public delegate void eventoEventHandler(object sender, EventArgs e);

        //[Category("Bartender - CustomEvents"), Description("Ocorre sempre ...")]
        public static event eventoEventHandler evento;

        public virtual void RaiseAoSalvar()
        {
            if (AoSalvar != null)
                AoSalvar(null, new EventArgs());
        }
    }
}
