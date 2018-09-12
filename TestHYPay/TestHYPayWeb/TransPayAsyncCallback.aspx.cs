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
    public partial class TransPayCallback : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var model = new TransPayAsyncCallbackModel();
                model.hpMerCode = Request.Form["hpMerCode"];
                model.orderNo = Request.Form["orderNo"];
                model.transDate = Request.Form["transDate"];
                model.transStatus = Request.Form["transStatus"];
                model.transAmount = Request.Form["transAmount"];
                model.transSeq = Request.Form["transSeq"];
                model.statusCode = Request.Form["statusCode"];
                model.statusMsg = Request.Form["statusMsg"];
                model.signature = Request.Form["signature"];

                Response.Write("success");


                var content =
                    $"hpMerCode={model.hpMerCode}&" +
                    $"orderNo={model.orderNo}&" +
                    $"transDate={model.transDate}&" +
                    $"transStatus={model.transStatus}&" +
                    $"transAmount={model.transAmount}&" +
                    $"transSeq={model.transSeq}&" +
                    $"statusCode={model.statusCode}&" +
                    $"statusMsg={model.statusMsg}&" +
                    $"signature={model.signature}" +
                    $"";

                Log.Logger.Info("异步通知", model);
                Log.Logger.Info("异步通知报文", content);
            }
            catch(Exception ex)
            {
                var builder = new StringBuilder();
                foreach(var key in Request.Form.AllKeys)
                {
                    builder.Append($"{key}={Request.Form[key]}|");
                }
                Log.Logger.Error("异步通知异常", ex, Request.Form, builder.ToString());
            }
        }
    }
}