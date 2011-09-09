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

public partial class qtInsert : System.Web.UI.Page
{
    public System.Data.SqlClient.SqlConnection connection;
    public string usr, form, prj, anl;
    public int success, cod_questionario;
    public string zona_cliente;
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

        if (usr != null && usr != "" && prj != null && prj != "" && anl != null && anl != "" && form != null && form != "") ;
        else
            Response.Redirect("Erro.aspx");

        reader = DatabaseReadData("INSERT INTO questionario VALUES (" + Session["anl"] + ")" +
            "SELECT SCOPE_IDENTITY() ", reader);
        reader.Read();

        cod_questionario = int.Parse(reader[0].ToString());
        reader.Close();

        success = 1;
        InsertCLFormDatabase();
    }

    protected void InsertCLFormDatabase()
    {
        reader = DatabaseReadData("SELECT cod_pergunta_questionario, numero_pergunta, TipoEscala.numeroEscalaResposta, cod_zona " +
                                                "FROM pergunta_questionario, TipoEscala " +
                                                "WHERE pergunta_questionario.cod_tipoEscala=TipoEscala.cod_tipoEscala " +
                                                "AND pergunta_questionario.cod_analise=" + anl, reader);

        while (reader.Read())
        {
            if (reader["cod_zona"].ToString() == "")
            {
                zona_cliente = Request[reader["numero_pergunta"].ToString() + reader["cod_zona"].ToString()];
            }
            else
                zona_cliente = reader["cod_zona"].ToString();

            if (int.Parse(reader["numeroEscalaResposta"].ToString()) == 0)
            {
                if (DatabaseQuery("INSERT INTO resposta_questionario_string values (" + cod_questionario + "," + anl + "," + zona_cliente + "," + reader["numero_pergunta"] + ", '" + Request[reader["numero_pergunta"].ToString()] + "'," + reader["cod_pergunta_questionario"] +")") == -1)
                    success = -1;
            }
            else if (int.Parse(reader["numeroEscalaResposta"].ToString()) == -1)
            {
                if (DatabaseQuery("INSERT INTO resposta_questionario_memo values (" + cod_questionario + "," + anl + "," + zona_cliente + "," + reader["numero_pergunta"] + ", '" + Request[reader["numero_pergunta"].ToString()] + "'," + reader["cod_pergunta_questionario"] + ")") == -1)
                    success = -1;
            }
            else
            {
                if (DatabaseQuery("INSERT INTO resposta_questionario_numero values (" + cod_questionario + "," + anl + "," + zona_cliente + "," + reader["numero_pergunta"] + ", '" + Request[reader["numero_pergunta"].ToString()] + "'," + reader["cod_pergunta_questionario"] +")") == -1)
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
