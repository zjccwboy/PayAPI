using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TestHYPayWeb.ViewModles;
using TestHYPayWeb.Config;
using Logger;

namespace TestHYPayWeb
{
    public partial class TransPay : System.Web.UI.Page
    {
        public TransPayModel PayModel;

        protected void Page_Load(object sender, EventArgs e)
        {
            PayModel = new TransPayModel();

            PayModel.insCode = PayConfig.insCode;
            PayModel.insMerchantCode = PayConfig.insMerchantCode;
            PayModel.hpMerCode = PayConfig.hpMerCode;
            PayModel.currencyCode = PayConfig.currencyCode;
            PayModel.productType = PayConfig.productType;
            PayModel.paymentType = PayConfig.paymentType;
            PayModel.frontUrl = PayConfig.frontUrl;
            PayModel.backUrl = PayConfig.backUrl;

            PayModel.orderAmount = Request.Form["orderAmount"];
            PayModel.name = Request.Form["name"];
            PayModel.idNumber = Request.Form["idNumber"];
            PayModel.accNo = Request.Form["accNo"];
            PayModel.telNo = Request.Form["telNo"];

            PayModel.orderNo = DateTime.Now.Ticks.ToString();
            PayModel.orderTime = DateTime.Now.ToString("yyyyMMddhhmmss");
            PayModel.nonceStr = new Random().Next(1, 12).ToString();

            var signString = $"{PayModel.insCode}|{PayModel.insMerchantCode}|{PayModel.hpMerCode}|{PayModel.orderNo}|" +
                $"{PayModel.orderTime}|{PayModel.orderAmount}|{PayModel.name}|{PayModel.idNumber}|{PayModel.accNo}|" +
                $"{PayModel.telNo}|{PayModel.productType}|{PayModel.paymentType}|{PayModel.nonceStr}|{PayConfig.signKey}";

            PayModel.signature = Helper.GetMD5(signString);

            var postData =
                    $"orderAmount={PayModel.orderAmount}&" +
                    $"name={PayModel.name}&" +
                    $"idNumber={PayModel.idNumber}&" +
                    $"accNo={PayModel.accNo}&" +
                    $"telNo={PayModel.telNo}&" +
                    $"insCode={PayModel.insCode}&" +
                    $"insMerchantCode={PayModel.insMerchantCode}&" +
                    $"hpMerCode={PayModel.hpMerCode}&" +
                    $"orderNo={PayModel.orderNo}&" +
                    $"orderTime={PayModel.orderTime}&" +
                    $"currencyCode={PayModel.currencyCode}&" +
                    $"productType={PayModel.productType}&" +
                    $"paymentType={PayModel.paymentType}&" +
                    $"nonceStr={PayModel.nonceStr}&" +
                    $"frontUrl={PayModel.frontUrl}&" +
                    $"backUrl={PayModel.backUrl}&" +
                    $"signature={PayModel.signature}" +
                    $"";

            Log.Logger.Info("消费交易", PayModel);
            Log.Logger.Info("消费交易报文", postData);
        }
    }
}