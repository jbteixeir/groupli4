<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qt.aspx.cs" Inherits="qt" %>

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
        allchecked = 1;
        rbname = "";
        for (j = 1; j < thisform.length + 1 && allchecked == 1; j++) {
            if (rbname != thisform.elements[j].name) {
                if (thisform.elements[j].type == "number" || thisform.elements[j].type == "text") {
                    if (thisform.elements[j].value == null || thisform.elements[j].value == "") {
                        allchecked = 0;
                    }
                }
                else if (thisform.elements[j].type != "undefined" && thisform.elements[j].type != "select-one" && allchecked != 0) {
                    rbname = thisform.elements[j].name;
                    var radios = document.getElementsByName(thisform.elements[j].name);
                    myOption = -1;
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
        }
        if (allchecked == 0) {
            alert("Existem campos por preencher!");
            return false;
        }
    }
</script>

<body>
    <h1>
        ETdAnalyser Forms
    </h1>
    <% 
        if (form == "QT")
        {%>
    <h2>
        Cliente
    </h2>
    <%}
        else
            Response.Redirect("erro.aspx");
    %>
    <div align="center" id="container">
        <%Response.Write("<form name=\"qtform\" action=\"" + form + "Insert.aspx\" method=POST>"); %>
        <%readerQuestao = DatabaseReadData("SELECT texto, tipo_questao, cod_zona, cod_item, pergunta_questionario.cod_tipoEscala, TipoEscala.numeroEscalaResposta, pergunta_questionario.texto, numero_pergunta " +
                                                "FROM pergunta_questionario, TipoEscala " +
                                                "WHERE pergunta_questionario.cod_tipoEscala=TipoEscala.cod_tipoEscala " +
                                                "AND pergunta_questionario.cod_analise=" + anl, readerQuestao);

          while (readerQuestao.Read())
          {
                
        %>
        <fieldset>
            <legend>
                <% Response.Write(readerQuestao["numero_pergunta"]);%>
            </legend>
            <h3>
                <%Response.Write(readerQuestao["texto"]); %>
            </h3>
            <%
                if (int.Parse(readerQuestao["numeroEscalaResposta"].ToString()) == 1)
                {
                    Response.Write("<input type=\"number\" name=" + readerQuestao["numero_pergunta"] + " maxlength=\"3\">");
                }
                else if (int.Parse(readerQuestao["numeroEscalaResposta"].ToString()) == 0)
                {
                    Response.Write("<input type=\"text\" name=" + readerQuestao["numero_pergunta"] + " maxlength=\"20\"/>");
                }
                else if (int.Parse(readerQuestao["numeroEscalaResposta"].ToString()) == -1)
                {
                    Response.Write("<textarea name=" + readerQuestao["numero_pergunta"] + " cols=\"100\" rows=\"5\" maxlength=\"3000\"></textarea><br>");
                }
                else
                {
                    readerEscala = DatabaseReadData("SELECT EscalaResposta.descricaoEscalaResposta, EscalaResposta.valorEscalaResposta" +
                                                           " FROM EscalaResposta, TipoEscala" +
                                                                " WHERE TipoEscala.cod_tipoEscala=EscalaResposta.cod_tipoEscala " +
                                                                    "AND TipoEscala.cod_tipoEscala = " + readerQuestao["cod_tipoEscala"] +
                                                                "ORDER BY valorEscalaResposta ASC", readerEscala);
                    while (readerEscala.Read())
                    {

                        Response.Write("<br><input type=\"radio\" name=\"" +
                            readerQuestao["numero_pergunta"] + "\" value=" + readerEscala["valorEscalaResposta"] +
                            ">\n" + readerEscala["descricaoEscalaResposta"]);
                    }
                }

                if (readerQuestao["cod_zona"].ToString() == "")
                {
                    Response.Write("<br><br><h3>Indique por favor o local</h3>");
                    reader = DatabaseReadData("SELECT zona.nome_zona, zona_analise.cod_zona " +
                                                           "FROM zona, zona_analise " +
                                                                "WHERE zona_analise.cod_zona = zona.cod_zona " +
                                                                    "AND zona_analise.cod_analise=" + anl, reader);

                    reader.Read();
                    Response.Write("<br><select name=\"" + readerQuestao["numero_pergunta"] + reader["cod_zona"] + "\">");
                    Response.Write("<option value=\"" + reader["cod_zona"] + "\">" + reader["nome_zona"] + "");

                    while (reader.Read())
                    {
                        Response.Write("<option value=\"" + reader["cod_zona"] + "\">" + reader["nome_zona"] + "");
                    }

                    Response.Write("</select>");

                    reader.Close();
                }
                
            %>
            <br />
        </fieldset>
        <%}
          if (readerQuestao.IsClosed == false)
              readerQuestao.Close(); %>
        <div id="fm-submit" class="fm-req">
            <input type="submit" value="Submeter" onclick="valbutton(qtform);return false;" />
        </div>
        </form>
    </div>
</body>
</html>
