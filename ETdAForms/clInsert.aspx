<%@ Page Language="C#" AutoEventWireup="true" CodeFile="clInsert.aspx.cs" Inherits="clInsert" %>

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
</head>
<link rel="stylesheet" type="text/css" href="style.css" />

<script language="JavaScript" type="text/javascript" src="style.js"></script>

<body>
    <h1>
        ETdAnalyser Forms
    </h1>
    <% 
        if (form == "CL")
        {%>
    <h2>
        Analista
    </h2>
    <%}
        else
            Response.Redirect("erro.aspx");
    %>
    <div align="center" id="container">
        <%if (success == 1)
          {
              Response.Write("<form action=\"Default.aspx?form=" + form + "&usr=" + usr + "&anl=" + anl + "&prj=" + prj + "\" method=\"post\">");
              Response.Write("<center><h3>CheckList submetida com sucesso!</h3></center>");
          }
          else
          {
              Response.Write("<form action=\"" + form + ".aspx?form=" + form + "&usr=" + usr + "&anl=" + anl + "&prj=" + prj + "\">");
              
              Response.Write("<center><h3>Erro ao submeter checklist</h3></center>");
          }%>
        <div id="fm-submit" class="fm-req">
            <input value="Concluir" type="submit" />
        </div>
        </form>
    </div>
</body>
</html>
