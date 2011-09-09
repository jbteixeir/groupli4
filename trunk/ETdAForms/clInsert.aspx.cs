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

public partial class clInsert : System.Web.UI.Page
{
    public System.Data.SqlClient.SqlConnection connection;
    public string usr, form, prj, anl;
    public int success;
    public System.Data.SqlClient.SqlDataReader reader;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            usr = Session["usr"].ToString();
            prj = Session["prj"].ToString();
            anl = Session["anl"].ToString();
            form = Session["form"].ToString();

            connection = (System.Data.SqlClient.SqlConnection)Session["connection"];
        }
        catch
        {
            Response.Redirect("Erro.aspx");
        }

        if (usr != null && usr != "" && prj != null && prj != "" && anl != null && anl != "" && form != null && form!= "") ;
        else
            Response.Redirect("Erro.aspx");
        success = 1;
        InsertCLFormDatabase();
    }

    protected void InsertCLFormDatabase(){
        reader = DatabaseReadData("SELECT Item.cod_item, Item.nome_item "+
                                                "FROM Item, Item_Analise "+
                                                "WHERE Item.cod_item = Item_Analise.cod_item "+ 
                                                    "AND Item_Analise.cod_analise="+anl, reader);

        while (reader.Read())
        {
            if (Request[reader["nome_item"].ToString()] == null)
            {
                if (DatabaseQuery("INSERT INTO resposta_checklist values (" + anl + "," + Session["zn"] + "," + reader["cod_item"] + ", null)") == -1)
                    success = -1;
            }
            else 
            {   if (DatabaseQuery("INSERT INTO resposta_checklist values (" + anl + "," + Session["zn"] + "," + reader["cod_item"] + "," + Request[reader["nome_item"].ToString()] + ")") == -1)
                    success = -1;
            }
        }        
    }

    protected int DatabaseQuery(string query)
    {
        System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(query, connection);
        return command.ExecuteNonQuery();
    }

    protected System.Data.SqlClient.SqlDataReader DatabaseReadData(string query, System.Data.SqlClient.SqlDataReader rdr)
    {

        rdr = null;
        System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(query, connection);

        rdr = command.ExecuteReader();
        return rdr;
    }
}
