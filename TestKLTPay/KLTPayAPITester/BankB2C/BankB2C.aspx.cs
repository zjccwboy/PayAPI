using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ThirdParty_KLPPay;
using ThirdParty_KLPPay.ChannelFatory;
using ThirdParty_KLPPay.Models;

namespace KLTPayAPITester.BankB2C
{
    public partial class BankB2C : System.Web.UI.Page
    {
        public PayGateWayPCModel PayGateWayPCModel { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            PayGateWayPCModel = new PayGateWayPCModel();

            var orderAmount = Request.Form["orderAmount"];
            var productName = Request.Form["productName"];
            var merchantId = Request.Form["merchantId"];

            this.PayGateWayPCModel.head = new PayGateWayPCHead
            {
                merchantId = merchantId,
                signType = "1",
                version = "18",
            };

            this.PayGateWayPCModel.content = new PayGateWayPContent
            {
                pickupUrl = "http://47.92.68.54:8002/BankB2C/pickupUrl.aspx",
                receiveUrl = "http://47.92.68.54:8002/BankB2C/receiveUrl.aspx",
                orderNo = GuidUtils.GetLongStringGuid(),
                orderAmount = int.Parse(orderAmount),
                orderCurrency = 156,
                orderDateTime = DateTime.Now.ToString("yyyyMMddHHmmss"),
                productName = productName,
                payType = 1,
                issuerId = "00000000",
            };

            var fatory = new PayGateWayPCChannelFatory();
            var response = fatory.CreateResult(PayGateWayPCModel);
            var resule = Newtonsoft.Json.JsonConvert.SerializeObject(response);
            Response.Write(resule);
            Response.End();
        }
    }
}