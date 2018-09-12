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
    public partial class DfTansPay : System.Web.UI.Page
    {
        public DFTansPayModel DFPayModel;
        protected void Page_Load(object sender, EventArgs e)
        {
            DFPayModel = new DFTansPayModel();

            DFPayModel.insCode = PayConfig.insCode;
            DFPayModel.insMerchantCode = PayConfig.insMerchantCode;
            DFPayModel.hpMerCode = PayConfig.hpMerCode;
            DFPayModel.currencyCode = PayConfig.currencyCode;

            DFPayModel.certType = "01";
            DFPayModel.certNumber = "341126197709218366";
            DFPayModel.accountType = "01";

            DFPayModel.orderType = Request.Form["orderType"];
            DFPayModel.orderAmount = Request.Form["orderAmount"];
            DFPayModel.accountName = Request.Form["accountName"];
            DFPayModel.accountNumber = Request.Form["accountNumber"];
            DFPayModel.mainBankName = Request.Form["mainBankName"];
            //DFPayModel.mainBankCode = Request.Form["mainBankCode"];
            //DFPayModel.openBranchBankName = Request.Form["openBranchBankName"];
            DFPayModel.mobile = Request.Form["mobile"];
            //DFPayModel.attach = Request.Form["attach"];

            DFPayModel.orderNo = DateTime.Now.Ticks.ToString();
            DFPayModel.orderDate = DateTime.Now.ToString("yyyyMMdd");
            DFPayModel.orderTime = DateTime.Now.ToString("yyyyMMddhhmmss");
            DFPayModel.nonceStr = new Random().Next(1, 12).ToString();

            var signString = $"{DFPayModel.insCode}|{DFPayModel.insMerchantCode}|{DFPayModel.hpMerCode}|{DFPayModel.orderNo}|" +
                $"{DFPayModel.orderDate}|{DFPayModel.orderTime}|{DFPayModel.currencyCode}|{DFPayModel.orderAmount}|{DFPayModel.orderType}|" +
                $"{DFPayModel.accountType}|{DFPayModel.accountName}|{DFPayModel.accountNumber}|{DFPayModel.nonceStr}|{PayConfig.signKey}";

            DFPayModel.signature = Helper.GetMD5(signString).ToUpper();            

            var postData = 
                $"insCode={DFPayModel.insCode}&" +
                $"insMerchantCode={DFPayModel.insMerchantCode}&" +
                $"insMerchantCode={DFPayModel.hpMerCode}&" +
                $"orderNo={DFPayModel.orderNo}&" +
                $"orderDate={DFPayModel.orderDate}&" +
                $"orderTime={DFPayModel.orderTime}&" +
                $"currencyCode={DFPayModel.currencyCode}&" +
                $"orderAmount={DFPayModel.orderAmount}&" +
                $"orderType={DFPayModel.orderType}&" +
                $"certType={DFPayModel.certType}&" +
                $"certNumber={DFPayModel.certNumber}&" +
                $"accountType={DFPayModel.accountType}&" +
                $"accountName={DFPayModel.accountName}&" +
                $"accountNumber={DFPayModel.accountNumber}&" +
                $"mainBankName={DFPayModel.mainBankName}&" +
                //$"mainBankCode={DFPayModel.mainBankCode}&" +
                //$"openBranchBankName={DFPayModel.openBranchBankName}&" +
                $"mobile={DFPayModel.mobile}&" +
                //$"attach={DFPayModel.attach}&" +
                $"nonceStr={DFPayModel.nonceStr}&" +
                $"signature={DFPayModel.signature}" +
                $"";

            Log.Logger.Info("代付交易", DFPayModel);
            Log.Logger.Info("代付交易报文", postData);
        }
    }
}