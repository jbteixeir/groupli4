using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public delegate void AoSalvarEventHandler(object sender, EventArgs e);

namespace ETdA.Camada_de_Interface
{
    class teste
    {
        //[Category("Bartender - CustomEvents"), Description("Ocorre sempre ...")]
        public event AoSalvarEventHandler AoSalvar;

        public virtual void RaiseAoSalvar()
        {
            if (AoSalvar != null)
                AoSalvar(null, new EventArgs());
        }

    }
}
