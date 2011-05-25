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

            SqlDataReader r = Camada_de_Dados.DataBaseCommunicator.DataBaseCommunicator.search(
                dadosProcura, null, null, null, null, null, null, tabelas);

            projectos = new List<Projecto>();
            projectos_recentes = new List<Projecto>();

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
            List<Projecto> recentes = new List<Projecto>();

            recentes.Add( projectos[0].clone() );
            recentes.Add( projectos[1].clone() );
            recentes.Add( projectos[2].clone() );
            recentes.Add( projectos[3].clone() );
            recentes.Add( projectos[4].clone() );

            for (int i = 5 ; i < projectos.Count ; i++ )
            {
                Boolean inserted = false;
                for (int j = 0; j < 5 && !inserted; j++)
                {
                    int result = DateTime.Compare(
                        recentes[j].Data,projectos[i].Data);
                    if (result < 0)
                    {
                        recentes.RemoveAt(j);
                        recentes.Add(projectos[i].clone());
                        inserted = true;
                    }
                }
            }
            reordena(recentes);
        }

        private static void reordena(List<Projecto> recentes)
        {
            Projecto max;
            
            while (projectos_recentes.Count < 4)
            {
                max = recentes[0].clone() ;
                for (int i = 1; i < recentes.Count ; i++)
                {
                    int result = DateTime.Compare(
                        max.Data, recentes[i].Data);
                    if (result < 0)
                    {
                        max = recentes[i].clone() ;
                    }
                }
                projectos_recentes.Add(max);
                recentes.Remove(max);
            }
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
