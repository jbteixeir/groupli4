using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ETdAnalyser.CamadaDados.Classes;
using ETdAnalyser.CamadaDados.Classes.Estruturas;
using System.Windows;

namespace ETdAnalyser.CamadaDados.ETdA
{
    class ETdA
    {
        private delegate void eventoEventHandler(object sender, EventArgs e);

		//private static event eventoEventHandler evento_analista_registado;
        private static event eventoEventHandler eventoProjectoAdicionado;
		//private static event eventoEventHandler evento_projecto_removido;

        private static Dictionary<long, string> codigosNomeProjectos;
        private static Dictionary<long, Projecto> projectos;
        private static string username;

        public static Dictionary<long, Projecto> Projectos
        {
            get { return projectos; }
            set { projectos = value; }
        }

        // s_final
        private static void IniciarEventos()
        {
            eventoProjectoAdicionado +=new eventoEventHandler(
                CamadaInterface.InterfaceGuestaoProjectos.addProjectoReenc);
        }

        // s_final
        public static void Iniciar()
        {
            
            IniciarEventos();

            codigosNomeProjectos = CamadaDados.DataBaseCommunicator.FuncsToDataBase.SelectNomeProjectos();
            projectos = new Dictionary<long, Projecto>();
        }

        // s_final
        public static Dictionary<long, string> Codes_Nomes_Projectos
        {
            get 
            {
                return new Dictionary<long,string>(codigosNomeProjectos);
            }
        }

        public static string Username
        {
            get { return username; }
            set { username = value; }
        }

        // s_final
        public static Projecto GetProjecto(long codigoProjecto)
        {
            if (!projectos.Keys.Contains(codigoProjecto))
                AbrirProjecto(codigoProjecto);
                
            return projectos[codigoProjecto];
        }

        /* ------------------------------------------------------ */
        /* Metodos */
        /* ------------------------------------------------------ */

        // s_final
        public static Dictionary<long, string> ProjectosRecentes()
        {
            Dictionary<long, string> rs = new Dictionary<long, string>();

            for (int i = 0; i < 5 && i < codigosNomeProjectos.Count; i++)
                rs.Add(codigosNomeProjectos.ElementAt(i).Key, codigosNomeProjectos.ElementAt(i).Value);

            return rs;
        }

        #region Gestao Projectos
        /*
         * Verifica se Ja tem Projecto com esse nome na Base de dados
         */
        // s_final
        public static Boolean PodeAdicionarProjecto(String nomeEstabelecimento)
        {
            return !codigosNomeProjectos.Values.Contains(nomeEstabelecimento);
        }

        /*
         * Adiciona Novo Projecto na aplicação
         */
        // s_final
        public static void AdicionarNovoProjecto(String nomeEstabelecimento)
        {
            Projecto p = new Projecto();
            p.Nome = nomeEstabelecimento;
            p.Data = DateTime.Now;

            p.Codigo = CamadaDados.DataBaseCommunicator.FuncsToDataBase.InsertProjecto(p);

            codigosNomeProjectos.Add(p.Codigo,p.Nome);
            projectos.Add(p.Codigo, p);

            List<string> s = new List<string>();
            s.Add(p.Codigo.ToString());
            s.Add(p.Nome);

            eventoProjectoAdicionado(s, new EventArgs());
        }

        /*
         * Abre um projecto com o código de estabelecimento
         */
        // s_final
        public static void AbrirProjecto(long codigoProjecto)
        {
            if (!projectos.Keys.Contains(codigoProjecto))
            {
                Projecto proj = CamadaDados.DataBaseCommunicator.
                    FuncsToDataBase.SelectProjecto(codigoProjecto);
                projectos.Add(proj.Codigo, proj);
            }
        }

        public static void RemoverProjecto(String nomeProjecto)
        {
            long cod = -1;
            Boolean found = false;
            for (int i = 0; i < codigosNomeProjectos.Count && !found; i++)
            {
                KeyValuePair<long, string> p = codigosNomeProjectos.ElementAt(i);
                if (p.Value == nomeProjecto)
                {
                    cod = p.Key;
                    codigosNomeProjectos.Remove(cod);
                    if (projectos[cod] != null)
                        projectos.Remove(cod);
                    found = true;
                }
            }

            CamadaDados.DataBaseCommunicator.FuncsToDataBase.
                DeleteProjecto(cod);
        }

        public static void RemoverProjecto(long codigoProjecto)
        {
            codigosNomeProjectos.Remove(codigoProjecto);

            foreach (Analise a in projectos[codigoProjecto].Analises.Values)
            {
                Camada_de_Negócio.GestaodeAnalises.removerAnalise(codigoProjecto, a.Codigo);
                CamadaDados.DataBaseCommunicator.FuncsToDataBase.DesactivarAnalise(a.Codigo);
            }
            
            projectos.Remove(codigoProjecto);
        }
        public static void ModificarProjecto(Projecto p)
        {
            CamadaDados.DataBaseCommunicator.FuncsToDataBase.
               UpdateProjecto(p);
        }

        public static void UltimaAlteracao(Projecto p)
        {
            CamadaDados.DataBaseCommunicator.FuncsToDataBase.
               UpdateProjecto(p);
        }

        #endregion

        #region Gestao Analistas
        public static bool AdicionarAnalista(String username, String password)
        {
            return CamadaDados.DataBaseCommunicator.
                FuncsToDataBase.InsertAnalista(username, password);
        }

        public static bool LoginAnalista(String server, String database, 
            String username, String password)
        {
            return CamadaDados.DataBaseCommunicator.
                FuncsToDataBase.LigaAnalista(server, database, username, password);
        }
        #endregion
    }
}
