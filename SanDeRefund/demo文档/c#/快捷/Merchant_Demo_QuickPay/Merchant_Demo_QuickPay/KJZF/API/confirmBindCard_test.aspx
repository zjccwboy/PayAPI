<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="confirmBindCard_test.aspx.cs" Inherits="WebApplication1.KJZF.API.confirmBindCard_test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>确认绑卡</title>
    <script type="text/javascript" src="../../scripts/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/json2.js"></script>
    <script type="text/javascript" src="../../scripts/common.js"></script>

</head>
<body onload="load()">
    <div>
        <form id="form_query" method="post" onsubmit="return check()">
             绑卡确认<br />
            <br />

            
            <br />
            请求地址<input type="text" name="urlstring" id="urlstring" value="http://61.129.71.103:8003/fastPay/apiPay/confirmBindCard" style="width:600px" /><br />
            <br />
            商户号
            <input type="text" name="mid" id="mid" value="10020025" /><br />
            接入方式<input type="text" name="accessType" id="accessType" value="1" /><br />
            渠道类型<input type="text" name="channelType" id="channelType" value="07" /><br />
            平台商户号<input type="text" name="plMid" id="plMid" value="" /><br />
            subMid<input type="text" name="subMid" id="subMid" value="" /><br />
            subMidAddr<input type="text" name="subMidAddr" id="subMidAddr" value="" /><br />
            subMidName<input type="text" name="subMidName" id="subMidName" value="" /><br />
            请求时间<input type="text" name="reqTime" id="reqTime" value="0001" /><br />
            用户id<input type="text" name="userId" id="userId" value="" /><br />
            绑卡申请号<input type="text" name="sdMsgNo" id="sdMsgNo" value="" /><br />
            手机号<input type="text" name="phoneNo" id="phoneNo" value="" /><br />
            短信验证码<input type="text" name="smsCode" id="smsCode" value="" /><br />
            异步通知地址<input type="text" name="notifyUrl" id="notifyUrl" value="http://192.168.22.194/Merchant_Demo_QuickPay/tools/asyncNotice.aspx" /><br />
            前端通知地址<input type="text" name="frontUrl" id="frontUrl" value="http://192.168.22.194/Merchant_Demo_QuickPay/tools/asyncNotice.aspx" /><br />

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
