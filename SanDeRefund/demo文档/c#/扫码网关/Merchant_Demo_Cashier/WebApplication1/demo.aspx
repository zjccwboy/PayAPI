﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="demo.aspx.cs" Inherits="WebApplication1.demo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>支付流水</title>
<script type="text/javascript" src="./scripts/jquery-1.7.2.min.js"></script>
<script type="text/javascript" src="./scripts/json2.js"></script>
<script type="text/javascript" src="./scripts/common.js"></script>
</head>
<body>
<div>
<form id="form_query" method="post"  onsubmit="return check()">
商户号<input type="text" name="mid" id="mid" value="" /><br/>
商户订单号<input type="text" name="orderCode" id="orderCode" value="1000000000000000" /><br/>
订单金额<input type="text" name="totalAmount" id="totalAmount" value="000000000012" /><br/>
订单标题<input type="text" name="subject" id="subject" value="话费充值" /><br/>
订单描述<input type="text" name="body" id="body" value="用户购买话费0.12" /><br/>
订单超时时间<input type="text" name="txnTimeOut" id="txnTimeOut" value="20161230000000" /><br/>
支付模式:<select name="payMode" id="payMode">
			<option value="">空</option>
			<option value="sand_h5">sand_h5</option>
            <option value="sand_pc">sand_pc</option>
            <option value="sand_api">sand_api</option>
            <option value="bank_pc" selected="selected">bank_pc</option>
		</select><br/>
支付扩展域<input type="text" name="payExtra" id="payExtra" value="{&quot;bankCode&quot;:&quot;01050000&quot;,&quot;payType&quot;:&quot;3&quot;}" /><br/>
客户端IP<input type="text" name="clientIp" id="clientIp" value="127.0.0.1" /><br/>
异步通知地址<input type="text" name="notifyUrl" id="notifyUrl" value="http://127.0.0.1/WebGateway/stateChangeServlet" /><br/>
前台通知地址<input type="text" name="frontUrl" id="frontUrl" value="http://61.129.71.103:8003/jspsandpay/payReturn.jsp" /><br/>
商户门店编号<input type="text" name="storeId" id="storeId" value="" /><br/>
商户终端编号<input type="text" name="terminalId" id="terminalId" value="" /><br/>
操作员编号<input type="text" name="operatorId" id="operatorId" value="" /><br/>
业务扩展参数<input type="text" name="bizExtendParams" id="bizExtendParams" value="" /><br/>
商户扩展参数<input type="text" name="merchExtendParams" id="merchExtendParams" value="" /><br/>
扩展域<input type="text" name="extend" id="extend" value="" /><br/>
<input type="submit"   value="支付" id="button_pay" /></form>
</div>
</body>
<script>
	//提示到服务器
	$(function () {
	       var current = CurentTime();
		   $("#orderCode").val(current);
	})
	function check() {
	    var mid = $.trim($("#mid").attr("value"))
	    if (mid.length == 0) {
	        alert("商户号为空");
	        return false;
	    } else {
	        return true;
	    }
	}
</script>
</html>
