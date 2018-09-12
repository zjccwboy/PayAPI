<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransQuery.aspx.cs" Inherits="TestHYPayWeb.TransQuery" %>

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
    <form id="form1" action="http://180.168.61.86:27380/hpayTransGatewayWeb/trans/query.htm" method="post">
        <input id="insCode" type="hidden" name="insCode" value="<%=TransQueryModel.insCode%>" />
        <input id="insMerchantCode" type="hidden" name="insMerchantCode" value="<%=TransQueryModel.insMerchantCode%>" />
        <input id="hpMerCode" type="hidden" name="hpMerCode" value="<%=TransQueryModel.hpMerCode%>" />
        <input id="orderNo" type="hidden" name="orderNo" value="<%=TransQueryModel.orderNo%>" />
        <input id="transDate" type="hidden" name="transDate" value="<%=TransQueryModel.transDate%>" />
        <input id="transSeq" type="hidden" name="transSeq" value="<%=TransQueryModel.transSeq%>" />
        <input id="productType" type="hidden" name="productType" value="<%=TransQueryModel.productType%>" />
        <input id="paymentType" type="hidden" name="paymentType" value="<%=TransQueryModel.paymentType%>" />
        <input id="nonceStr" type="hidden" name="nonceStr" value="<%=TransQueryModel.nonceStr%>" />
        <input id="signature" type="hidden" name="signature" value="<%=TransQueryModel.signature%>" />
    </form>
</body>
</html>
