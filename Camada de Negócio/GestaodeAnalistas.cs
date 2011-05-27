using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ETdA.Camada_de_Dados.DataBaseCommunicator;

namespace ETdA.Camada_de_Negócio
{
	class GestaodeAnalistas
	{
		//Métodos
		//void registaAnalista(String nome, String username, String password);

		//void removeAnalisa(String codAnalista);
		//void logout();

        public static Boolean loadConnectionSuper()
        {
            try
            {
                System.IO.StreamReader sr = new System.IO.StreamReader("Super.cfg");

                string server = sr.ReadLine();
                string database = sr.ReadLine();
                string username = sr.ReadLine();
                string password = sr.ReadLine();

                sr.Close();

                return DataBaseCommunicator.connectToSuper(server, username, password, database);
            }
            catch
            {
                return false;
            }
        }

        public static Boolean loadConnectionUtilizadorLogado()
        {
            try
            {
                System.IO.StreamReader sr = new System.IO.StreamReader("Config.cfg");
                string server = sr.ReadLine();
                string database = sr.ReadLine();
                sr.Close();

                sr = new System.IO.StreamReader("Utilizador.cfg");
                string username = sr.ReadLine();
                string password = sr.ReadLine();
                sr.Close();

                return DataBaseCommunicator.connect(server, username, password, database);
            }
            catch
            {
                return false;
            }
        }

        /* 
         * Faz login na base de dados
         * @return se nao conseguir fazer login retorna false
         */
        public static Boolean login(string username, string password)
        {
            try
            {
                System.IO.StreamReader sr = new System.IO.StreamReader("Config.cfg");

                string server = sr.ReadLine();
                string database = sr.ReadLine();
                sr.Close();

                return DataBaseCommunicator.connect(server, username, password, database);
            }
            catch
            {
                return false;
            }
        }

	}
}
