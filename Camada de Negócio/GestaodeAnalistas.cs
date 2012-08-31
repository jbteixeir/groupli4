using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ETdAnalyser.Camada_de_Dados.DataBaseCommunicator;
using System.Windows.Forms;

namespace ETdAnalyser.Camada_de_Negócio
{
	class GestaodeAnalistas
	{
		//Métodos
        public static bool registaAnalista(String username, String password)
        {
            return Camada_de_Dados.ETdA.ETdA.adicionaAnalista(username,password);
        }

		//void removeAnalisa(String codigoAnalisenalista);
		//void logout();

        public static Boolean loadConnectionSuper()
        {
            try
            {
                FileStream fs = new FileStream("Config.cfg", FileMode.Open, FileAccess.Read);
                BinaryReader r = new BinaryReader(fs);

                string server = r.ReadString();
                string database = r.ReadString();
                r.ReadString();
                r.ReadString();
                r.ReadString();
                string username = r.ReadString();
                string password = r.ReadString();

                r.Close();
                fs.Close();

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
                FileStream fs = new FileStream("Config.cfg", FileMode.Open, FileAccess.Read);
                BinaryReader r = new BinaryReader(fs);

                string server = r.ReadString();
                r.ReadString();
                r.ReadString();
                r.ReadString();
                string database = r.ReadString();

                /*
                sr = new System.IO.StreamReader("Utilizador.cfg");
                string username = sr.ReadLine();
                string password = sr.ReadLine();
                sr.Close();
                */

                fs = new FileStream("Utilizador.cfg", FileMode.Open, FileAccess.Read);
                r = new BinaryReader(fs);

                string username = r.ReadString();
                string password = r.ReadString();

                fs.Close();
                r.Close();
                Camada_de_Dados.ETdA.ETdA.Username = username;

                return Camada_de_Dados.ETdA.ETdA.loginAnalista(server, database, username, password);
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
                FileStream fs = new FileStream("Config.cfg", FileMode.Open, FileAccess.Read);
                BinaryReader r = new BinaryReader(fs);

                string server = r.ReadString();
                r.ReadString();
                r.ReadString();
                r.ReadString();
                string database = r.ReadString();

                r.Close();
                fs.Close();

                Camada_de_Dados.ETdA.ETdA.Username = username;

                return Camada_de_Dados.ETdA.ETdA.loginAnalista(server, database, username, password);
            }
            catch
            {
                return false;
            }
        }

        public static void guarda_dados(string username, string password)
        {
            /*
            System.IO.StreamWriter sr = new System.IO.StreamWriter("Utilizador.cfg");

            sr.WriteLine(username);
            sr.WriteLine(password);
            sr.Close();
            */

            FileStream fs = new FileStream("Utilizador.cfg", FileMode.Create);
            BinaryWriter w = new BinaryWriter(fs);

            w.Write(username);
            w.Write(password);

            w.Close();
            fs.Close();
        }

        public static void remove_dados()
        {
            File.Delete("Utilizador.cfg");
        }

        public static void alterar_ligacaoBaseDados(string server, string database, string webserver, string porta, string superusername, string superpassword)
        {
            FileStream fs = new FileStream("Config.cfg", FileMode.Create);
            BinaryWriter w = new BinaryWriter(fs);

            w.Write(server);
            w.Write(database);
            w.Write(webserver);
            w.Write(porta);
            w.Write("ETdAnalyser");
            w.Write(superusername);
            w.Write(superpassword);

            w.Close();
            fs.Close();
        }

        public static string nomeservidor()
        {
             try
            {
                FileStream fs = new FileStream("Config.cfg", FileMode.Open, FileAccess.Read);
                BinaryReader r = new BinaryReader(fs);

                return r.ReadString();
            }
             catch
             {
                 return "";
             }
        }

        public static string nomeServidorWeb()
        {
            try
            {
                FileStream fs = new FileStream("Config.cfg", FileMode.Open, FileAccess.Read);
                BinaryReader r = new BinaryReader(fs);

                r.ReadString();
                r.ReadString();
                return r.ReadString();
            }
            catch
            {
                MessageBox.Show("Ficheiro de Configuração inexistente ou corrompido.\nPor favor reconfigure a ligação aos servidores.");
                return "";
            }
        }

        public static string portaServidorWeb()
        {
            try
            {
                FileStream fs = new FileStream("Config.cfg", FileMode.Open, FileAccess.Read);
                BinaryReader r = new BinaryReader(fs);

                r.ReadString();
                r.ReadString();
                r.ReadString();
                return r.ReadString();
            }
            catch
            {
                MessageBox.Show("Ficheiro de Configuração inexistente ou corrompido.\nPor favor reconfigure a ligação aos servidores.");
                return "";
            }
        }

        public static bool existeFicheiroConfiguracao()
        {
            try
            {
                FileStream fs = new FileStream("Config.cfg", FileMode.Open, FileAccess.Read);
                return (true);
            }
            catch
            {
                return (false);
            }
        }
	}
}
