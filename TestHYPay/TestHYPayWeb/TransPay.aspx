<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransPay.aspx.cs" Inherits="TestHYPayWeb.TransPay" %>

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
    <form id="form1" action="http://180.168.61.86:27380/hpayTransGatewayWeb/trans/debit.htm" method="post">
        <input id="insCode" type="hidden" name="insCode" value="<%=PayModel.insCode%>" />
        <input id="insMerchantCode" type="hidden" name="insMerchantCode" value="<%=PayModel.insMerchantCode%>" />
        <input id="hpMerCode" type="hidden" name="hpMerCode" value="<%=PayModel.hpMerCode%>" />
        <input id="orderNo" type="hidden" name="orderNo" value="<%=PayModel.orderNo%>" />
        <input id="orderTime" type="hidden" name="orderTime" value="<%=PayModel.orderTime%>" />
        <input id="currencyCode" type="hidden" name="currencyCode" value="<%=PayModel.currencyCode%>" />
        <input id="orderAmount" type="hidden" name="orderAmount" value="<%=PayModel.orderAmount%>" />
        <input id="name" type="hidden" name="name" value="<%=PayModel.name%>" />
        <input id="idNumber" type="hidden" name="idNumber" value="<%=PayModel.idNumber%>" />
        <input id="accNo" type="hidden" name="accNo" value="<%=PayModel.accNo%>" />
        <input id="telNo" type="hidden" name="telNo" value="<%=PayModel.telNo%>" />
        <input id="productType" type="hidden" name="productType" value="<%=PayModel.productType%>" />
        <input id="paymentType" type="hidden" name="paymentType" value="<%=PayModel.paymentType%>" />
        <input id="nonceStr" type="hidden" name="nonceStr" value="<%=PayModel.nonceStr%>" />
        <input id="frontUrl" type="hidden" name="frontUrl" value="<%=PayModel.frontUrl%>" />
        <input id="backUrl" type="hidden" name="backUrl" value="<%=PayModel.backUrl%>" />
        <input id="signature" type="hidden" name="signature" value="<%=PayModel.signature%>" />
    </form>
</body>
</html>
