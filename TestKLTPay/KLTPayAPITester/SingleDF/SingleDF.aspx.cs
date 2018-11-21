using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ThirdParty_KLPPay;
using ThirdParty_KLPPay.ChannelFatory;
using ThirdParty_KLPPay.Models;

namespace KLTPayAPITester.SingleDF
{
    public partial class SingleDF : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var orderAmount = Request.Form["orderAmount"];
            var merchantId = Request.Form["merchantId"];
            var orderNo = Request.Form["orderNo"];

            var model = new SingleDFPayModelRequest();
            model.head = new SingleDFHead
            {
                merchantId = merchantId,
                signType = "1",
                version = "18",
            };

            model.content = new SingleDFContent
            {
                accountName = "全渠道",
                accountNo = "6216261000000000018",
                accountType = "1",
                amt = int.Parse(orderAmount),
                bankName = "平安银行",
                bankNo = "105290068018",
                mchtOrderNo = orderNo,
                notifyUrl = "http://47.92.68.54:8002/SingleDF/notifyUrl.aspx",
                orderDateTime = DateTime.Now.ToString("yyyyMMddHHmmss"),
                purpose = "test",
                remark = "单笔代付",
            };

            var fatory = new SingleDFPayChannelFatory();
            var result = fatory.CreateResult(model);

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            Response.Write(json);
            Response.End();
        }
    }
}