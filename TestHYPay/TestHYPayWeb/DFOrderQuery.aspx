<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DFOrderQuery.aspx.cs" Inherits="TestHYPayWeb.DFOrderQuery" %>

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
        <input id="insCode" type="hidden" name="insCode" value="<%=OrderQueryModel.insCode%>" />
        <input id="insMerchantCode" type="hidden" name="insMerchantCode" value="<%=OrderQueryModel.insMerchantCode%>" />
        <input id="hpMerCode" type="hidden" name="hpMerCode" value="<%=OrderQueryModel.hpMerCode%>" />
        <input id="orderNo" type="hidden" name="orderNo" value="<%=OrderQueryModel.orderNo%>" />
        <input id="transDate" type="hidden" name="transDate" value="<%=OrderQueryModel.transDate%>" />
        <input id="transSeq" type="hidden" name="transSeq" value="<%=OrderQueryModel.transSeq%>" />
        <input id="productType" type="hidden" name="productType" value="<%=OrderQueryModel.productType%>" />
        <input id="paymentType" type="hidden" name="paymentType" value="<%=OrderQueryModel.paymentType%>" />
        <input id="nonceStr" type="hidden" name="nonceStr" value="<%=OrderQueryModel.nonceStr%>" />
        <input id="signature" type="hidden" name="signature" value="<%=OrderQueryModel.signature%>" />
    </form>
</body>
</html>
