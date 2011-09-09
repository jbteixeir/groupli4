using System;
using System.Collections;
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

public partial class cl : System.Web.UI.Page
{
    public System.Data.SqlClient.SqlConnection connection;
    public string usr, form, prj, anl;
    public System.Data.SqlClient.SqlDataReader reader, readerEscala, readerQuestao;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            usr = Session["usr"].ToString();
            prj = Session["prj"].ToString();
            anl = Session["anl"].ToString();
            form = Session["form"].ToString();

            Session["zn"]=Request["zn"];

            connection = (System.Data.SqlClient.SqlConnection)Session["connection"];
        }
        catch
        {
            Response.Redirect("Erro.aspx");
        }
    }

    protected void DatabaseQuery(string query)
    {
        System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(query, connection);
        command.ExecuteNonQuery();
    }

    /*
     * Executa query's de leitura à base de dados
     */
    protected System.Data.SqlClient.SqlDataReader DatabaseReadData(string query, System.Data.SqlClient.SqlDataReader rdr)
    {

        rdr = null;
        System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(query, connection);

        rdr = command.ExecuteReader();
        return rdr;
    }

}