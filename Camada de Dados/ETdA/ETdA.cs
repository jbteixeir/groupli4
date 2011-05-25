using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ETdA.Camada_de_Dados.ETdA
{
    class ETdA
    {
        private static Analista analista;
        private static List<Projecto> projectos;
        private static List<Projecto> projectos_recentes;

        public static Analista Analista
        {
            get { return analista; }
            set { analista = value; }
        }

        public static List<Projecto> Projectos
        {
            get { return projectos; }
            set { projectos = value; }
        }

        public static List<Projecto> Projectos_Recentes
        {
            get { return projectos_recentes; }
            set { projectos_recentes = value; }
        }

        /* ------------------------------------------------------ */
        /* Metodos */
        /* ------------------------------------------------------ */

        public static void init()
        {
            List<String> dadosProcura = new List<String>();
            dadosProcura.Add("*");
            List<int> tabelas = new List<int>();
            tabelas.Add(DataBaseCommunicator.DataBaseCommunicator.PROJECTOS);
            String orderby = "data";
            String order = "desc";

            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.search(
                dadosProcura, null, null, null, null, orderby, order, tabelas);

            projectos = new List<Projecto>();

            while (r.Read())
            {
                Projecto p = new Projecto((string)r["codProjecto"],
                    (string)r["estabelecimento"], (DateTime)r["data"]);

                projectos.Add(p);
            }

            projectosRecentes();
        }

        private static void projectosRecentes()
        {
            projectos_recentes = new List<Projecto>();

            for (int i = 0; i < 5 && i < projectos.Count; i++)
                projectos_recentes.Add(projectos[i].clone());
        }

        public static void adicionaProjecto(String nomeProjecto)
        {
            Projecto p = new Projecto();
            p.Nome = nomeProjecto;
            p.Data = DateTime.Now;

            projectos.Add(p);
        }


    }
}
