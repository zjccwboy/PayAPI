<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DfTansPay.aspx.cs" Inherits="TestHYPayWeb.DfTansPay" %>

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
    <form id="form1" action="http://180.168.61.86:27380/hpayTransGatewayWeb/trans/df/transdf.htm" method="post">
        <input id="insCode" type="hidden" name="insCode" value="<%=DFPayModel.insCode%>" />
        <input id="insMerchantCode" type="hidden" name="insMerchantCode" value="<%=DFPayModel.insMerchantCode%>" />
        <input id="hpMerCode" type="hidden" name="hpMerCode" value="<%=DFPayModel.hpMerCode%>" />
        <input id="orderNo" type="hidden" name="orderNo" value="<%=DFPayModel.orderNo%>" />
        <input id="orderDate" type="hidden" name="orderDate" value="<%=DFPayModel.orderDate%>" />
        <input id="orderTime" type="hidden" name="orderTime" value="<%=DFPayModel.orderTime%>" />
        <input id="currencyCode" type="hidden" name="currencyCode" value="<%=DFPayModel.currencyCode%>" />
        <input id="orderAmount" type="hidden" name="orderAmount" value="<%=DFPayModel.orderAmount%>" />
        <input id="orderType" type="hidden" name="orderType" value="<%=DFPayModel.orderType%>" />
        <input id="certType" type="hidden" name="certType" value="<%=DFPayModel.certType%>" />
        <input id="certNumber" type="hidden" name="certNumber" value="<%=DFPayModel.certNumber%>" />
        <input id="accountType" type="hidden" name="accountType" value="<%=DFPayModel.accountType%>" />
        <input id="accountName" type="hidden" name="accountName" value="<%=DFPayModel.accountName%>" />
        <input id="accountNumber" type="hidden" name="accountNumber" value="<%=DFPayModel.accountNumber%>" />
        <input id="mainBankName" type="hidden" name="mainBankName" value="<%=DFPayModel.mainBankName%>" />
        <%--<input id="mainBankCode" type="hidden" name="mainBankCode" value="<%=DFPayModel.mainBankCode%>" />--%>
        <%--<input id="openBranchBankName" type="hidden" name="openBranchBankName" value="<%=DFPayModel.openBranchBankName%>" />--%>
        <input id="mobile" type="hidden" name="mobile" value="<%=DFPayModel.mobile%>" />
        <%--<input id="attach" type="hidden" name="attach" value="<%=DFPayModel.attach%>" />--%>
        <input id="nonceStr" type="hidden" name="nonceStr" value="<%=DFPayModel.nonceStr%>" />
        <input id="signature" type="hidden" name="signature" value="<%=DFPayModel.signature%>" />
    </form>
</body>
</html>
