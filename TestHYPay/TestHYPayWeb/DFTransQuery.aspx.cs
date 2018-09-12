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
    public partial class DFTransQuery : System.Web.UI.Page
    {
        public DFTransQueryModel DFTransQueryModel;
        protected void Page_Load(object sender, EventArgs e)
        {
            DFTransQueryModel = new DFTransQueryModel();

            DFTransQueryModel.accountType = Request.Form["accountType"];
            DFTransQueryModel.insCode = PayConfig.insCode;
            DFTransQueryModel.insMerchantCode = PayConfig.insMerchantCode;
            DFTransQueryModel.hpMerCode = PayConfig.hpMerCode;
            DFTransQueryModel.nonceStr = new Random().Next(1, 12).ToString();

            var signString = $"{DFTransQueryModel.insCode}|{DFTransQueryModel.insMerchantCode}" +
                $"|{DFTransQueryModel.hpMerCode}|{DFTransQueryModel.accountType}" +
                $"|{DFTransQueryModel.nonceStr}|{PayConfig.signKey}";
            DFTransQueryModel.signature = Helper.GetMD5(signString);

            var postData =
                $"accountType={DFTransQueryModel.accountType}&" +
                $"insCode={DFTransQueryModel.insCode}&" +
                $"insMerchantCode={DFTransQueryModel.insMerchantCode}&" +
                $"hpMerCode={DFTransQueryModel.hpMerCode}&" +
                $"nonceStr={DFTransQueryModel.nonceStr}&" +
                $"signature={DFTransQueryModel.signature}" +
                $"";

            Log.Logger.Info("商户账户余额查询查询", DFTransQueryModel);
            Log.Logger.Info("商户账户余额查询报文", postData);
        }
    }
}