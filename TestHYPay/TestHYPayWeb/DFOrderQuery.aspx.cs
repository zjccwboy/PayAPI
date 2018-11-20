using Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TestHYPayWeb.Config;
using TestHYPayWeb.ViewModles;

namespace TestHYPayWeb
{
    public partial class DFOrderQuery : System.Web.UI.Page
    {
        public TransQueryModel OrderQueryModel;
        protected void Page_Load(object sender, EventArgs e)
        {
            OrderQueryModel = new TransQueryModel();

            var accountType = Request.Form["accountType"];
            switch (accountType)
            {
                case "D0":
                    PayConfig.paymentType = "2007";
                    break;
                case "T1":
                    PayConfig.paymentType = "2023";
                    break;
                case "DT":
                    PayConfig.paymentType = "2007";
                    break;
                case "DF":
                    PayConfig.paymentType = "2012";
                    break;
            }

            PayConfig.productType = "100002";

            OrderQueryModel.orderNo = Request.Form["orderNo"];
            OrderQueryModel.insCode = PayConfig.insCode;
            OrderQueryModel.insMerchantCode = PayConfig.insMerchantCode;
            OrderQueryModel.hpMerCode = PayConfig.hpMerCode;
            OrderQueryModel.transDate = Request.Form["transDate"];
            OrderQueryModel.transSeq = Request.Form["transSeq"];
            OrderQueryModel.productType = PayConfig.productType;
            OrderQueryModel.paymentType = PayConfig.paymentType;
            OrderQueryModel.nonceStr = new Random().Next(1, 12).ToString();

            var signString = $"{OrderQueryModel.insCode}|{OrderQueryModel.insMerchantCode}|{OrderQueryModel.hpMerCode}|" +
                $"{OrderQueryModel.orderNo}|{OrderQueryModel.transDate}|" +
                $"{OrderQueryModel.transSeq}|{OrderQueryModel.productType}|" +
                $"{OrderQueryModel.paymentType}|{OrderQueryModel.nonceStr}|{PayConfig.signKey}";

            OrderQueryModel.signature = Helper.GetMD5(signString);

            var postData =
                $"orderNo={OrderQueryModel.orderNo}&" +
                $"insCode={OrderQueryModel.insCode}&" +
                $"insMerchantCode={OrderQueryModel.insMerchantCode}&" +
                $"hpMerCode={OrderQueryModel.hpMerCode}&" +
                $"transDate={OrderQueryModel.transDate}&" +
                $"transSeq={OrderQueryModel.transSeq}&" +
                $"productType={OrderQueryModel.productType}&" +
                $"paymentType={OrderQueryModel.paymentType}&" +
                $"nonceStr={OrderQueryModel.nonceStr}&" +
                $"signature={OrderQueryModel.signature}" +
                $"";

            Log.Logger.Info("代付订单查询", OrderQueryModel);
            Log.Logger.Info("代付订单查询报文", postData);
        }
    }
}