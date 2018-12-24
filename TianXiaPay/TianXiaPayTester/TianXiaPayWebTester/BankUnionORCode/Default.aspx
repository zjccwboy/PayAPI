<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TianXiaPayWebTester.BankUnionORCode.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="Content-Language" content="zh-cn" />
    <meta name="apple-mobile-web-app-capable" content="no" />
    <meta name="apple-touch-fullscreen" content="yes" />
    <meta name="format-detection" content="telephone=no,email=no" />
    <meta name="apple-mobile-web-app-status-bar-style" content="white" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Cache-control" content="no-cache" />
    <meta http-equiv="Cache" content="no-cache" />
    <meta name="viewport" content="width=device-width,user-scalable=no,initial-scale=1,maximum-scale=1,minimum-scale=1" />
    <title>商品支付</title>
    <style>
        [v-cloak] {
            display: none !important;
        }
    </style>
    <link href="../css/app.css" rel="stylesheet" />
    <script src="../js/jquery.min.1.8.3.js"></script>

    <script type="text/javascript">
        var totalTime = 6000 * 4;
        var count = 0;
        setInterval(function () {
            count++;
            if (count > totalTime) {
                return;
            }

            var orderNo = $("#OrderNo").val();
            $.ajax({
                type: "get",
                url: "Query.aspx",
                data: "OrderNo=" + orderNo,
                cache: false,
                success: function (msg) {
                    if (msg == "SUCCESS") {
                        window.location.href = "PayShow.aspx";
                    }
                },
                error: function (XmlHttpRequest, textStatus, errorThrown) {
                    alert("错误信息;" + XmlHttpRequest.status);
                }
            })
        }, 1000)
    </script>

</head>
<body>
    <div id="main-container">
        <div class="mod-top"></div>
        <div class="mod-ct">
            <div class="order"></div>
            <div class="amount">￥<% =Amount%></div>
            <div data-role="qrPayImgWrapper" class="qrcode-img-wrapper">
                <div data-role="qrPayImg" class="qrcode-img-area">
                    <div class="show-qr-container">
                        <canvas width="210" height="210" style="display: none;"></canvas>
                        <img alt="Scan me!" src="<% =QRCode%>" style="display: block;" />
                    </div>
                </div>
            </div>
            <div class="handler-panel">
                <div class="time-item dom-interval">
                    <div class="time-item">
                        <h1>未到账可联系我们</h1>
                    </div>
                    <div class="time-item">
                        <h1>订单:<% =OrderNo%></h1>
                        <input type="hidden" id="OrderNo" value="<% =OrderNo%>" />
                    </div>
                </div>
                <div class="tip">
                    <div class="ico-scan"></div>
                    <div class="tip-text">
                        <p><% =Title%></p>
                    </div>
                </div>
                <div class="tip-text"></div>
            </div>
        </div>
    </div>
</body>
</html>