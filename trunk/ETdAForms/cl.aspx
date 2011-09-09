<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cl.aspx.cs" Inherits="cl" %>

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
        allchecked = 1;

        rbname = "";
        for (j = 1; j < thisform.length + 1 && allchecked == 1; j++) {
            if (rbname != thisform.elements[j].name) {
                rbname = thisform.elements[j].name;
                var radios = document.getElementsByName(thisform.elements[j].name);
                myOption = -1;
                for (i = radios.length - 1; i > -1; i--) {
                    if (radios[i].checked) {
                        myOption = i; i = -1;
                    }
                }
                if (myOption == -1 && radios.length>0) {
                    allchecked = 0;
                }
            }
        }
        if (allchecked == 0) {
            alert("Existem itens por assinalar!");
            return false;
        }
}
</script>

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
        <%Response.Write("<form name=\"clform\" action=\"" + form + "Insert.aspx?form=" + form + "&usr=" + usr + "&anl=" + anl + "&prj=" + prj + "\" method=POST>"); %>
        <%readerQuestao = DatabaseReadData("SELECT Item.cod_item, Item.nome_item "+
                                                "FROM Item, Item_Analise "+
                                                "WHERE Item.cod_item = Item_Analise.cod_item "+ 
                                                    "AND Item_Analise.cod_analise="+anl, readerQuestao);

          while (readerQuestao.Read())
          {
                
        %>
        <fieldset>
            <legend>
                <% Response.Write(readerQuestao["nome_item"]);%>
            </legend>
            <%
                readerEscala = DatabaseReadData("SELECT EscalaResposta.descricaoEscalaResposta, EscalaResposta.valorEscalaResposta" +
                                                           " FROM EscalaResposta, TipoEscala" +
                                                                " WHERE TipoEscala.cod_tipoEscala=EscalaResposta.cod_tipoEscala " +
                                                                    "AND TipoEscala.default_tipoEscala=1 "+
                                                                    "AND TipoEscala.numeroEscalaResposta=5 " +
                                                                    "AND TipoEscala.tipoEscalaResposta = 'Qualidade' "+
                                                                "ORDER BY valorEscalaResposta ASC", readerEscala);
                while (readerEscala.Read())
                {

                    Response.Write("<br><input type=\"radio\" name=\"" +
                        readerQuestao["nome_item"] + "\" value=" + readerEscala["valorEscalaResposta"] +
                        ">\n" + readerEscala["descricaoEscalaResposta"]);
                }
                ;%>
            <br />
        </fieldset>
        <%}
            if (readerQuestao.IsClosed == false)
                readerQuestao.Close(); %>
        <div id="fm-submit" class="fm-req">
             <input type="submit" value="Submeter" onclick="valbutton(clform);return false;" />
        </div>
        </form>
    </div>
</body>
</html>
