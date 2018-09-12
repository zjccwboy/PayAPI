<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DFTransQuery.aspx.cs" Inherits="TestHYPayWeb.DFTransQuery" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="js/md5.js" type="text/javascript"></script>
    <script src="js/jquery.min.js" type="text/javascript"></script>
    <title></title>

    <script type="text/javascript">
        $(function () {
            $("#form1").submit();
        })
    </script>
</head>
<body>
    <form id="form1" action="http://180.168.61.86:27380/hpayTransGatewayWeb/trans/df/queryAccount.htm" method="post">
        <input type="hidden" name="insCode" value="<%=DFTransQueryModel.insCode%>" />
        <input type="hidden" name="insMerchantCode" value="<%=DFTransQueryModel.insMerchantCode%>" />
        <input type="hidden" name="hpMerCode" value="<%=DFTransQueryModel.hpMerCode%>" />
        <input type="hidden" name="accountType" value="<%=DFTransQueryModel.accountType%>" />
        <input type="hidden" name="nonceStr" value="<%=DFTransQueryModel.nonceStr%>" />
        <input type="hidden" name="signature" value="<%=DFTransQueryModel.signature%>" />
    </form>
</body>
</html>
