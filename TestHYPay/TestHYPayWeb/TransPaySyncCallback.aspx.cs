using Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TestHYPayWeb.ViewModles;

namespace TestHYPayWeb
{
    public partial class TransPaySyncCallback : System.Web.UI.Page
    {
        public TransPaySyncCallbackModel SyncCallbackModel;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var model = new TransPaySyncCallbackModel();
                model.insCode = Request.Form["insCode"];
                model.insMerchantCode = Request.Form["insMerchantCode"];
                model.hpMerCode = Request.Form["hpMerCode"];
                model.orderNo = Request.Form["orderNo"];
                model.transDate = Request.Form["transDate"];
                model.transStatus = Request.Form["transStatus"];
                model.transResultMsg = Request.Form["transResultMsg"];
                model.transAmount = Request.Form["transAmount"];
                model.actualAmount = Request.Form["actualAmount"];
                model.transSeq = Request.Form["transSeq"];
                model.statusCode = Request.Form["statusCode"];
                model.statusMsg = Request.Form["statusMsg"];
                model.reserved = Request.Form["reserved"];
                model.signature = Request.Form["signature"];

                Response.Write("success");

                var content =
                    $"insCode={model.insCode}&" +
                    $"insMerchantCode={model.insMerchantCode}&" +
                    $"hpMerCode={model.hpMerCode}&" +
                    $"orderNo={model.orderNo}&" +
                    $"transDate={model.transDate}&" +
                    $"transStatus={model.transStatus}&" +
                    $"transResultMsg={model.transResultMsg}&" +
                    $"transAmount={model.transAmount}&" +
                    $"actualAmount={model.actualAmount}&" +
                    $"transSeq={model.transSeq}&" +
                    $"statusCode={model.statusCode}&" +
                    $"statusMsg={model.statusMsg}&" +
                    $"reserved={model.reserved}&" +
                    $"signature={model.signature}" +
                    $"";

                Log.Logger.Info("同步通知", model);
                Log.Logger.Info("同步通知报文", content);
            }
            catch (Exception ex)
            {
                var builder = new StringBuilder();
                foreach (var key in Request.Form.AllKeys)
                {
                    builder.Append($"{key}={Request.Form[key]}|");
                }
                Log.Logger.Error("同步通知异常", ex, Request.Form, builder.ToString());
            }
        }
    }
}