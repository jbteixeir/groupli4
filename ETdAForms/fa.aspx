<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fa.aspx.cs" Inherits="fa" %>

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
        //alert("legnth="+thisform.length);
        for (j = 1; j < thisform.length + 1 && allchecked == 1; j++) {
            if (rbname != thisform.elements[j].name && thisform.elements[j].name != "comments") {
          //      alert("nome elemtento="+thisform.elements[j].name);
                rbname = thisform.elements[j].name;
                var radios = document.getElementsByName(thisform.elements[j].name);
                myOption = -1;
                //    alert("num radios="+radios.length);
                for (i = radios.length - 1; i > -1; i--) {
                    if (radios[i].checked) {
                        myOption = i; i = -1;
                    }
                }
                if (myOption == -1 && radios.length > 0) {
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
        if (form == "FA")
        {%>
    <h2>
        Profissional
    </h2>
    <%}
        else
            Response.Redirect("erro.aspx");
    %>
    <div align="center" id="container">
        <%Response.Write("<form name=\"faform\" action=\"" + form + "Insert.aspx\" method=POST>"); %>
        <%readerQuestao = DatabaseReadData("SELECT  item.nome_item, pergunta_ficha_avaliacao.texto, numero_pergunta " +
                                                "FROM  item, pergunta_ficha_avaliacao " +
                                                "WHERE  item.cod_item=pergunta_ficha_avaliacao.cod_item " +
                                                    "AND pergunta_ficha_avaliacao.cod_analise=" + anl, readerQuestao);

          while (readerQuestao.Read())
          {
                
        %>
        <fieldset>
            <legend>
                <% Response.Write(readerQuestao["nome_item"]);%>
            </legend>
            <h3>
                <%Response.Write(readerQuestao["texto"]); %></h3>
            <%
                readerEscala = DatabaseReadData("SELECT EscalaResposta.descricaoEscalaResposta, EscalaResposta.valorEscalaResposta" +
                                                           " FROM EscalaResposta, TipoEscala" +
                                                                " WHERE TipoEscala.cod_tipoEscala=EscalaResposta.cod_tipoEscala " +
                                                                    "AND TipoEscala.default_tipoEscala=1 " +
                                                                    "AND TipoEscala.numeroEscalaResposta=5 " +
                                                                    "AND TipoEscala.tipoEscalaResposta = 'Qualidade' " +
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
        <br />
        <%}
          if (readerQuestao.IsClosed == false)
              readerQuestao.Close(); %>
        <fieldset>
            <legend>Opinião / Sugestão </legend>
            <h3>
                Utilize este espaço para dar a sua opinião / sugestão</h3>
            <textarea name="comments" cols="100" rows="5"></textarea><br />
        </fieldset>
        <div id="fm-submit" class="fm-req">
            <input type="submit" value="Submeter" onclick="valbutton(faform);return false;" />
        </div>
        </form>
    </div>
</body>
</html>
