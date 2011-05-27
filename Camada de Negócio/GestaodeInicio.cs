using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ETdA.Camada_de_Dados.DataBaseCommunicator;

namespace ETdA.Camada_de_Negócio
{
    class GestaodeInicio
    {
        public static Boolean loadConnection()
        {
            System.IO.StreamReader sr = new System.IO.StreamReader("Config.ini");

            string server = sr.ReadLine();
            string database = sr.ReadLine();
            string username = sr.ReadLine();
            string password = sr.ReadLine();
            sr.Close();

            return DataBaseCommunicator.connect(server, username, password, database);
        }
    }
}
