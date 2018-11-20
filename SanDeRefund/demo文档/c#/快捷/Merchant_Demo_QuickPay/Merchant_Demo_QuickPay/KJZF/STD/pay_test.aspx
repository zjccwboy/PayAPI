<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pay_test.aspx.cs" Inherits="Merchant_Demo_QuickPay.KJZF.STD.pay_test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>绑卡支付</title>
    <script type="text/javascript" src="../../scripts/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/json2.js"></script>
    <script type="text/javascript" src="../../scripts/common.js"></script>
</head>
<body onload="load()">
    <div>
        <form id="form_query" method="post" onsubmit="return check()">
            绑卡支付API<br />

            请求地址<input type="text" name="urlstring" id="urlstring" value="http://61.129.71.103:8003/fastPay/apiPay/pay" style="width: 600px" /><br />
            <br />
            商户号
            <input type="text" name="mid" id="mid" value="10020025" /><br />
            接入方式<input type="text" name="accessType" id="accessType" value="1" /><br />
            渠道类型<input type="text" name="channelType" id="channelType" value="07" /><br />
            产品编号<select name="productId" id="productId">
                <!--  <option value="00000016">00000016-一键快捷产品</option>-->
                <option value="00000017">00000017-标准快捷</option>
                <!-- <option value="00000018" selected="selected">00000018 API快捷</option> -->
            </select><br />

            平台商户号<input type="text" name="plMid" id="plMid" value="" /><br />
            subMid<input type="text" name="subMid" id="subMid" value="" /><br />
            subMidAddr<input type="text" name="subMidAddr" id="subMidAddr" value="" /><br />
            subMidName<input type="text" name="subMidName" id="subMidName" value="" /><br />
            请求时间<input type="text" name="reqTime" id="reqTime" value="0001" /><br />

            用户id<input type="text" name="userId" id="userId" value="" /><br />
            绑卡ID<input type="text" name="bId" id="bId" value="" /><br />
            手机号<input type="text" name="phoneNo" id="phoneNo" value="" /><br />
            商户订单号<input type="text" name="orderCode" id="orderCode" value="" />同短信发送的订单号<br />
            商户下单时间<input type="text" name="orderTime" id="orderTime" value="" /><br />
            交易金额<input type="text" name="totalAmount" id="totalAmount" value="000000000050" /><br />
            币种<input type="text" name="currencyCode" id="currencyCode" value="156" /><br />
            商品名称<input type="text" name="subject" id="subject" value="快捷测试0.5元" /><br />
            商品描述<input type="text" name="body" id="body" value="快捷支付测试0.5元" /><br />

            TD0清算标志<select name="clearCycle" id="clearCycle">
                <option value="0">T1</option>
                <option value="1">T0</option>
                <option value="2">D0</option>
            </select><br />

            异步通知地址<input type="text" name="notifyUrl" id="notifyUrl" value="http://192.168.22.194/Merchant_Demo_QuickPay/tools/asyncNotice.aspx" /><br />
            前端通知地址<input type="text" name="frontUrl" id="frontUrl" value="http://192.168.22.194/Merchant_Demo_QuickPay/tools/asyncNotice.aspx" /><br />
            <br />
            短信码<input type="text" name="smsCode" id="smsCode" value="" /><br />

            <input type="submit" value="支付" id="button_pay" />
        </form>
    </div>
</body>
<script>

    //提示到服务器
    $(function () {
        var current = CurentTime();
        $("#reqTime").val(current);
    })


</script>
</html>
