<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ETdAnalyser Forms</title>
    <style type="text/css">
        #form1
        {
            height: 177px;
            width: 800px;
            z-index: 1;
            left: 10px;
            top: 15px;
            position: absolute;
        }
    </style>
    <link rel="stylesheet" type="text/css" href="style.css" />
</head>

<script language="JavaScript" type="text/javascript">
    function valbutton(thisform) {
        // place any other field validations that you require here
        // validate myradiobuttons
        myOption = -1;
        for (i = thisform.zn.length - 1; i > -1; i--) {
            if (thisform.zn[i].checked) {
                myOption = i; i = -1;
            }
        }
        if (myOption == -1) {
            alert("Seleccione uma zona ou actividade!");
            return false;
        }
        // place any other field validations that you require here
        thisform.submit(); // this line submits the form after validation
    }
</script>

<body>
    <h1>
        ETdAnalyser Forms
    </h1>
    <% 
        switch (form)
        {
            case "FA":%>
    <h2>
        Profissional</h2>
    <%break;
        case "QT":%>
    <h2>
        Cliente</h2>
    <%break;
        case "CL":%>
    <h2>
        Analista</h2>
    <%break;
        default:
      Response.Redirect("Erro.aspx");
      break;
    } %>
    
        <% if (reader.IsClosed == false)
               reader.Close();
           reader = DatabaseReadData("SELECT COUNT(cod_zona) FROM Zona_Analise WHERE Zona_Analise.cod_analise=" + anl);
           reader.Read();
           if (int.Parse(reader[0].ToString()) == 1)
           {
               if (reader.IsClosed == false)
                   reader.Close();
               reader = DatabaseReadData("SELECT Zona_analise.cod_zona FROM Zona_Analise WHERE Zona_Analise.cod_analise=" + anl);
               reader.Read();
               Response.Redirect(form + ".aspx?zn=" + reader["cod_zona"].ToString());
           }
           else
           {
               if (reader.IsClosed == false)
                   reader.Close();
               reader = DatabaseReadData("SELECT Zona.cod_zona, Zona.nome_zona FROM Zona, Zona_Analise WHERE Zona_Analise.cod_zona=Zona.cod_zona" +
                                                    " AND Zona_Analise.cod_analise=" + anl);
        %>
        <div align="center" id="container">
        <fieldset>
            <legend>Seleccione uma Zona ou Actividade</legend>
            <br/>
            <%Response.Write("<form name=\"zonactiv\" action=\"" + form + ".aspx\" method=\"get\">\n"); %>
            <% while (reader.Read())
               {
                   Response.Write("\t\t<input type=\"radio\" value=\"" + reader["cod_zona"] + "\" name=\"zn\" />" + reader["nome_zona"] + "<br />\n");
            }%>
               <div id="fm-submit" class="fm-req">
                    <input type="submit" value="Submeter" onclick="valbutton(zonactiv);return false;" />
                </div>
            </form>
            <%
               reader.Close();
           }
           if (reader.IsClosed == false)
               reader.Close();
            %>
                
        </fieldset>
    </div>
</body>

</html>
