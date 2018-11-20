using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CryptUtils_Csharp;
using log4net;

namespace Merchant_Demo_QuickPay.tools
{
    public partial class asyncNotice : System.Web.UI.Page
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(asyncNotice));
        static string dirPath = System.AppDomain.CurrentDomain.BaseDirectory;
        static string pfxFilePath = dirPath + "MID_RSA_PRIVATE_KEY.pfx";
        static string pfxPassword = "123456";
        static string cerFilePath = dirPath + "SAND_PUBLIC_KEY.cer";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.HttpMethod == "POST")
            {
                string asyncNotice = Request.Form.ToString();
                MessageWorker worker = new MessageWorker();
                worker.PFXFile = pfxFilePath; //商户pfx证书路径
                worker.PFXPassword = pfxPassword;  //商户pfx证书密码
                worker.CerFile = cerFilePath; //杉德cer证书路径

                //验签
                MessageWorker.trafficMessage asyncMessage = worker.CheckSignMessageAfterResponse(worker.UrlDecodeMessage(asyncNotice));
                log.Debug("验签结果：" + asyncMessage.sign);

                log.Debug("反馈报文：" + asyncMessage.data);

                Response.Write("resp=000000");


            }
        }
    }
}