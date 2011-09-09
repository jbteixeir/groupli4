using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class _Default : System.Web.UI.Page
{
    public System.Data.SqlClient.SqlConnection connection;
    public string usr, form, prj, anl;
    public System.Data.SqlClient.SqlDataReader reader;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Page.IsPostBack == false)
        {
            if (Request["usr"] != null && Request["form"] != null && Request["prj"] != null && Request["anl"] != null)
            {

                usr = Request["usr"].ToString();
                Session["usr"] = usr;
                form = Request["form"].ToString();
                Session["form"] = form;
                prj = Request["prj"].ToString();
                Session["prj"] = prj;
                anl = Request["anl"].ToString();
                Session["anl"] = anl;

                ReadConfig("/inetpub/wwwroot/ETdA/config.ini");
            }
            else
            {
                usr = Session["usr"].ToString();
                form = Session["form"].ToString();
                prj = Session["prj"].ToString();
                anl = Session["anl"].ToString();
                connection = (System.Data.SqlClient.SqlConnection)Session["connection"];

            }
        }

        switch (form)
        {
            case "FA":
                reader = DatabaseReadData("select estadowebFichaAvaliacao from Analise where cod_analise=" + anl + "and cod_projecto=" + prj);
                reader.Read();
                if (form.Equals(reader.Read().ToString()))
                {
                    reader.Close();
                    Response.Redirect("Erro.aspx");
                }
                //Response.Redirect("fa.aspx?usr=" + usr + "&anl=" + anl + "&prj=" + prj);
                break;
            case "QT":
                reader = DatabaseReadData("select estadowebQuestionario from Analise where cod_analise=" + anl + "and cod_projecto=" + prj);
                reader.Read();
                if (form.Equals(reader.Read().ToString()))
                {
                    reader.Close();
                    Response.Redirect("Erro.aspx");
                }
                Response.Redirect("qt.aspx");
                break;
            case "CL":
                reader = DatabaseReadData("select estadowebCheckList from Analise where cod_analise=" + anl + "and cod_projecto=" + prj);
                reader.Read();
                if (form.Equals(reader.Read().ToString()))
                {
                    reader.Close();
                    Response.Redirect("Erro.aspx");
                }
                //Response.Redirect("cl.aspx?usr=" + usr + "&anl=" + anl + "&prj=" + prj);
                break;
            default:
                reader.Close();
                Response.Redirect("Erro.aspx");
                break;
        }

    }

    protected void ReadConfig(string filepath)
    {
        System.IO.StreamReader sr = new System.IO.StreamReader(filepath);

        string server = sr.ReadLine();
        string database = sr.ReadLine();
        string username = sr.ReadLine();
        string password = sr.ReadLine();
        sr.Close();

        DatabaseConnect(username, password, server, database);
    }

    protected System.Data.SqlClient.SqlConnection DatabaseConnect(string username, string password, string server, string database)
    {
        string connectionstring
            = "Data Source=" + server + ";" +
             "Initial Catalog=" + database + "_" +
             usr + ";" +
             "User ID=" + username + ";" +
             "Password=" + password + ";" +
             "MultipleActiveResultSets = True";

        this.connection = new System.Data.SqlClient.SqlConnection(connectionstring);
        connection.Open();
        Session["connection"] = connection;
        return connection;
    }

    protected void DatabaseQuery(string query)
    {
        System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(query, connection);
        command.ExecuteNonQuery();
    }

    /*
     * Executa query's de leitura à base de dados
     */
    protected System.Data.SqlClient.SqlDataReader DatabaseReadData(string query)
    {

        System.Data.SqlClient.SqlDataReader reader = null;
        System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(query, connection);

        reader = command.ExecuteReader();
        return reader;
    }

}
