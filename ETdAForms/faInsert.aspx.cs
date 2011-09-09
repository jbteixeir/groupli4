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

public partial class faInsert : System.Web.UI.Page
{
    public System.Data.SqlClient.SqlConnection connection;
    public string usr, form, prj, anl;
    public int success, cod_fichaAvaliacao;
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

        if (!(usr != null && usr != "" && prj != null && prj != "" && anl != null && anl != "" && form != null && form != ""))
            Response.Redirect("Erro.aspx");

        reader = DatabaseReadData("INSERT INTO ficha_avaliacao VALUES (" + Session["anl"] + ")" +
            "SELECT SCOPE_IDENTITY() ", reader);
        reader.Read();

        cod_fichaAvaliacao = int.Parse(reader[0].ToString());
        reader.Close();

        success = 1;
        InsertCLFormDatabase();
    }

    protected void InsertCLFormDatabase()
    {
        reader = DatabaseReadData("SELECT numero_pergunta, item.cod_item, item.nome_item " +
                                                "FROM  item, pergunta_ficha_avaliacao " +
                                                "WHERE  item.cod_item=pergunta_ficha_avaliacao.cod_item " +
                                                    "AND pergunta_ficha_avaliacao.cod_analise=" + anl, reader);

        while (reader.Read())
        {


            if (DatabaseQuery("INSERT INTO resposta_ficha_avaliacao_numero values (" + cod_fichaAvaliacao + "," + anl + "," + reader["numero_pergunta"] + "," + Request[reader["nome_item"].ToString()] + "," + Session["zn"] + ")") == -1)
                success = -1;

        }
        reader.Close();
        if (DatabaseQuery("INSERT INTO resposta_ficha_avaliacao_string values (" + cod_fichaAvaliacao + "," + anl + ", null, '" + Request["comments"] + "'," + Session["zn"] + ")") == -1)
            success = -1;
        
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
