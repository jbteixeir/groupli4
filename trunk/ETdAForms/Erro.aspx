<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Erro.aspx.cs"  Inherits="Erro"%>

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
        for (i = thisform.myradiobutton.length - 1; i > -1; i--) {
            if (thisform.myradiobutton[i].checked) {
                myOption = i; i = -1;
            }
        }
        if (myOption == -1) {
            alert("You must select a radio button");
            return false;
        }
        alert("You selected button number " + myOption
+ " which has a value of "
+ thisform.myradiobutton[myOption].value);
        // place any other field validations that you require here
        thisform.submit(); // this line submits the form after validation
    }
</script>

<body>
    <div align="center" id="Div1">
        <h1>
            ETdAnalyser Forms
        </h1>
        <div align="center" id="container">
            <center>
                <h3>
                    Ocorreu um erro com o ETdA Forms. Por favor retroceda e refresque a página. </h3>
                <input type="button" value=" Retroceder " onclick="history.back();">
        </div>
    </center>
</body>
</html>
