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
    public partial class TransQuery : System.Web.UI.Page
    {
        public TransQueryModel TransQueryModel;

        protected void Page_Load(object sender, EventArgs e)
        {
            TransQueryModel = new TransQueryModel();

            TransQueryModel.orderNo = Request.Form["orderNo"];
            TransQueryModel.insCode = PayConfig.insCode;
            TransQueryModel.insMerchantCode = PayConfig.insMerchantCode;
            TransQueryModel.hpMerCode = PayConfig.hpMerCode;
            TransQueryModel.transDate = Request.Form["transDate"];
            TransQueryModel.transSeq = "";
            TransQueryModel.productType = PayConfig.productType;
            TransQueryModel.paymentType = PayConfig.paymentType;
            TransQueryModel.nonceStr = new Random().Next(1, 12).ToString();

            var signString = $"{TransQueryModel.insCode}|{TransQueryModel.insMerchantCode}|{TransQueryModel.hpMerCode}|" +
                $"{TransQueryModel.orderNo}|{TransQueryModel.transDate}|" +
                $"{TransQueryModel.transSeq}|{TransQueryModel.productType}|" +
                $"{TransQueryModel.paymentType}|{TransQueryModel.nonceStr}|{PayConfig.signKey}";

            TransQueryModel.signature = Helper.GetMD5(signString);

            var postData =
                $"orderNo={TransQueryModel.orderNo}&" +
                $"insCode={TransQueryModel.insCode}&" +
                $"insMerchantCode={TransQueryModel.insMerchantCode}&" +
                $"hpMerCode={TransQueryModel.hpMerCode}&" +
                $"transDate={TransQueryModel.transDate}&" +
                $"transSeq={TransQueryModel.transSeq}&" +
                $"productType={TransQueryModel.productType}&" +
                $"paymentType={TransQueryModel.paymentType}&" +
                $"nonceStr={TransQueryModel.nonceStr}&" +
                $"signature={TransQueryModel.signature}" +
                $"";

            Log.Logger.Info("商户付款订单结果查询", TransQueryModel);
            Log.Logger.Info("商户付款订单结果查询报文", postData);
        }
    }
}