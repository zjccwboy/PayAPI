﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="stdPay_test.aspx.cs" Inherits="Merchant_Demo_QuickPay.KJZF.STD.stdPay_test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>快速绑卡</title>
    <style type="text/css">
        input {
            width: 320px;
        }
    </style>
    <script type="text/javascript" src="../../scripts/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/json2.js"></script>
    <script type="text/javascript" src="../../scripts/common.js"></script>
</head>
<body onload="load()">
    <div>
        <form id="form_query" method="post" onsubmit="return check()">
            一键快捷<br />

            请求地址<input type="text" name="urlstring" id="urlstring" style="width: 400px" value="http://61.129.71.103:8003/fastPay/stdPay/index" />

            <table>
                <tr>
                    <td>
                        <h3>报文头</h3>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <label>[version]版本号:</label>
                    </td>
                    <td>
                        <input type="text" name="version" id="version" value="1.0" /></td>
                </tr>
                <tr>
                    <td>
                        <label>[method]接口名称:</label>
                    </td>
                    <td>
                        <input type="text" name="method" id="method" value="sandPay.fastPay.stdPay.index" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>[productId]产品编码:</label>
                    </td>
                    <td>
                        <select name="productId" id="productId">
                            <option value="00000017">00000017-标准快捷产品</option>
                        </select></td>
                </tr>
                <tr>
                    <td>
                        <label>[accessType]接入类型:</label>
                    </td>
                    <td>
                        <select name="accessType" id="accessType">
                            <option value="1">1-普通商户接入（目前支持1）</option>
                            <option value="2">2-平台商户接入</option>
                        </select></td>
                </tr>
                <tr>
                    <td>
                        <label>[mid]商户号:</label>
                    </td>
                    <td>
                        <input type="text" name="mid" id="mid" value="" />
                    </td>
                </tr>

                <tr>
                    <td>
                        <label>[plMid]平台商户号:</label>
                    </td>
                    <td>
                        <input type="text" name="plMid" id="plMid" value="" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>[channelType]渠道类型:</label>
                    </td>
                    <td>
                        <select name="channelType" id="channelType">
                            <option value="07">07-互联网</option>
                            <option value="08">08-移动端</option>
                        </select></td>
                </tr>
                <tr>
                    <td>
                        <label>[reqTime]请求时间:</label>
                    </td>
                    <td>
                        <input type="text" name="reqTime" id="reqTime" value="000004" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <h3>报文体</h3>
                    </td>
                    <td></td>
                </tr>


                <tr>
                    <td>
                        <label>[userId]用户ID:</label>
                    </td>
                    <td>
                        <input type="text" name="userId" id="userId" value="" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>[applyNo]商户上送申请绑卡流水号:</label>

                    </td>
                    <td>
                        <input type="text" name="applyNo" id="applyNo" value="S000001" />
                    </td>
                </tr>

                <tr>
                    <td>
                        <label>[notifyUrl]异步通知地址:</label>
                    </td>
                    <td>
                        <input type="text" name="notifyUrl" id="notifyUrl" value="http://192.168.22.194/Merchant_Demo_QuickPay/tools/asyncNotice.aspx" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>[frontUrl]前台通知地址:</label>
                    </td>
                    <td>
                        <input type="text" name="frontUrl" id="frontUrl" value="http://192.168.22.194/Merchant_Demo_QuickPay/tools/asyncNotice.aspx" />
                    </td>
                </tr>

                <tr>
                    <td>
                        <label>[extend]扩展域:</label>
                    </td>
                    <td>
                        <input type="text" name="extend" id="extend" value="" />
                    </td>
                </tr>

            </table>



            <input type="submit" value="支付" id="button_pay" />
        </form>
    </div>
</body>
<script>

    //提示到服务器
    $(function () {
        var current = CurentTime();
        $("#applyNo").val(current);

        $("#reqTime").val(current);
    })


</script>
</html>
