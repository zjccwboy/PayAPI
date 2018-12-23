<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TianXiaPayWebTester.QuickH5.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" action="http://apitest2.tfb8.com/cgi-bin/v2.0/api_mwappay_apply.cgi"  method="post" accept-charset="gb2312">
        <div>
            <table>
                <tr>
                    <td>商户号</td>
                    <td>
                        <input name="spid" id="spid" type="text" value="<% =RequestModel.spid%>" />
                    </td>
                </tr>

                <tr>
                    <td>用户号</td>
                    <td>
                        <input name="sp_userid" id="sp_userid" type="text" value="<% =RequestModel.sp_userid%>" />
                    </td>
                </tr>

                <tr>
                    <td>支付订单号</td>
                    <td>
                        <input name="spbillno" id="spbillno" type="text" value="<% =RequestModel.spbillno%>" />
                    </td>
                </tr>

                <tr>
                    <td>订单交易金额</td>
                    <td>
                        <input name="money" id="money" type="text" value="<% =RequestModel.money%>" />
                    </td>
                </tr>

                <tr>
                    <td>金额类型</td>
                    <td>
                        <input name="cur_type" id="cur_type" type="text" value="<% =RequestModel.cur_type%>" />
                    </td>
                </tr>

                <tr>
                    <td>用户类型</td>
                    <td>
                        <input name="user_type" id="user_type" type="text" value="<% =RequestModel.user_type%>" />
                    </td>
                </tr>

                <tr>
                    <td>渠道类型</td>
                    <td>
                        <input name="channel" id="channel" type="text" value="<% =RequestModel.channel%>" />
                    </td>
                </tr>

                <tr>
                    <td>版本号</td>
                    <td>
                        <input name="version" id="version" type="text" value="<% =RequestModel.version%>" />
                    </td>
                </tr>

                <tr>
                    <td>页面回调地址</td>
                    <td>
                        <input name="return_url" id="return_url" type="text" value="<% =RequestModel.return_url%>" />
                    </td>
                </tr>

                <tr>
                    <td>后台回调地址</td>
                    <td>
                        <input name="notify_url" id="notify_url" type="text" value="<% =RequestModel.notify_url%>" />
                    </td>
                </tr>

                <tr>
                    <td>订单备注</td>
                    <td>
                        <input name="memo" id="memo" type="text" value="<% =RequestModel.memo%>" />
                    </td>
                </tr>


                <tr>
                    <td>订单有效时长</td>
                    <td>
                        <input name="expire_time" id="expire_time" type="text" value="<% =RequestModel.expire_time%>" />
                    </td>
                </tr>

                <tr>
                    <td>银行账号</td>
                    <td>
                        <input name="bank_accno" id="bank_accno" type="text" value="<% =RequestModel.bank_accno%>" />
                    </td>
                </tr>

                <tr>
                    <td>银行账号类型</td>
                    <td>
                        <input name="bank_acctype" id="bank_acctype" type="text" value="<% =RequestModel.bank_acctype%>" />
                    </td>
                </tr>


                <tr>
                    <td>签名</td>
                    <td>
                        <input name="sign" id="sign" type="text" value="<% =RequestModel.sign%>" />
                    </td>
                </tr>

                <tr>
                    <td>签名类型</td>
                    <td>
                        <input name="encode_type" id="encode_type" type="text" value="<% =RequestModel.encode_type%>" />
                    </td>
                </tr>

<%--                <tr>
                    <td>提交</td>
                    <td>
                        <input type="submit" />
                    </td>
                </tr>--%>

            </table>
        </div>
    </form>
        <script type="text/javascript">document.all.form1.submit();</script>
</body>
</html>
