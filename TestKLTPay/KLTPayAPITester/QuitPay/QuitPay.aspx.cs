using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ThirdParty_KLPPay;
using ThirdParty_KLPPay.ChannelFatory;
using ThirdParty_KLPPay.Models;

namespace KLTPayAPITester.QuitPay
{
    public partial class QuitPay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var orderAmount = Request.Form["orderAmount"];
            var productName = Request.Form["productName"];
            var merchantId = Request.Form["merchantId"];
            var orderNo = Request.Form["orderNo"];

            var smsResult = SendSMS(merchantId, orderAmount, orderNo);
            var result = QuickPay(smsResult, merchantId, orderAmount, productName, "111111");
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            Response.Write(json);
            Response.End();
        }

        /// <summary>
        /// 发送短信接口
        /// </summary>
        static SendSMSModelResponse SendSMS(string merchantId, string orderAmount, string orderNo)
        {
            var model = new SendSMSModelRequest();
            model.head = new SendSMSHead
            {
                merchantId = merchantId,
                signType = "1",
            };

            model.content = new SendSMSContent
            {
                payerAcctNo = "6216261000000000018",
                payerIdNo = "341126197709218366",
                payerIdType = "01",
                payerName = "全渠道",
                payerTelephone = "13552535506",
                orderAmount = orderAmount,
                orderNo = orderNo,
            };

            var fatory = new SendSMSChannelFatory();
            var result = fatory.CreateResult(model);

            return result;
        }

        /// <summary>
        /// 快捷确认支付接口
        /// </summary>
        /// <param name="smsResponse"></param>
        /// <param name="smsCode"></param>
        static QuickPayModelResponse QuickPay(SendSMSModelResponse smsResponse, string merchantId, string orderAmount, string productName, string smsCode)
        {
            var model = new QuickPayModelRequest();

            model.head = new QuickPayHead
            {
                merchantId = merchantId,
                version = "18",
                signType = "1",
            };

            model.content = new QuickPayContent
            {
                orderNo = smsResponse.orderNo, //发送短信接口的原订单号
                orderCurrency = 156,
                smsCode = smsCode,
                productName = productName,
                orderDatetime = DateTime.Now.ToString("yyyyMMddHHmmss"),
                orderAmount = int.Parse(orderAmount),
                acctValiddate = DateTime.Now.ToString("yyMM"),//yyMM
                originalRequestId = smsResponse.requestId, //发送短信接口返回的 requestId
                receiveUrl = "http://47.92.68.54:8002/QuitPay/receiveUrl.aspx", //交易结果通知，需要参考文档 4 确认支付 自行处理.
            };

            var fatory = new QuickPayChannelFatory();
            var result = fatory.CreateResult(model);
            return result;
        }
    }
}