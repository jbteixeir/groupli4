using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETdA.Camada_de_Negócio
{
    class GestaodeAnalistas
    {
        //Métodos
        void registaAnalista(String nome, String username, String password);
        void login(String username, String password);
        void removeAnalisa(String codAnalista);
        void logout();

    }
}
