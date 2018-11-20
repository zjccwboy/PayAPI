<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="middle.aspx.cs" Inherits="WebApplication1.WebForm1" %>

<!DOCTYPE html>

<html>
<head  runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>Insert title here</title>
<script type="text/javascript" src="./scripts/paymentjs.js"></script>
<script type="text/javascript" src="./scripts/jquery-1.7.2.min.js"></script>
</head>
<body> 
<script>
    function wap_pay() {
    	var responseText = $("#credential").text();
    	console.log(responseText);
    	paymentjs.createPayment(responseText, function(result, err) {
			console.log(result);
			console.log(err.msg);
			console.log(err.extra);
		});
	}
</script>
<div style="display: none" >

	<%//String credential = (String)Request.getAttribute("JWP_ATTR");
        string credential = Request["JWP_ATTR"];
        System.Diagnostics.Debug.WriteLine("===" + credential);
	%> 
	<p id="credential"><%=credential%></p>
</div>
</body>

<script>
window.onload=function(){
	wap_pay();
};
</script>
</html>