<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Start.aspx.cs" Inherits="KLTPayAPITester.SingleDF.Start" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
 <h4>单笔实时代付</h4>
    <form action="SingleDF.aspx" method="post" runat="server">
        <table>
            <tr>
                <td>金额</td>
                <td>
                    <input type="text" id="orderAmount" name="orderAmount" value="100" />
                </td>
            </tr>
            <tr>
                <td>订单号</td>
                <td>
                    <input type="text" id="orderNo" name="orderNo" value="<% =orderNumber%>"/>
                </td>
            </tr>
            <tr>
                <td>测试商户号</td>
                <td>
                    <input type="text" id="merchantId" name="merchantId" value="903110153110001"/>
                </td>
            </tr>
            <tr>
                <td>订单日期</td>
                <td>
                    <input type="text" id="orderDate" name="orderDate" value="<% =orderDate%>"/>
                </td>
            </tr>
            <tr>
                <td>提交</td>
                <td>
                    <input type="submit" value="提交" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
