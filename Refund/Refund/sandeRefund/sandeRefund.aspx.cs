using log4net;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Refund.sandeRefund
{
    public partial class sandeRefund : System.Web.UI.Page
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(sandeRefund));

        static string dirPath = System.AppDomain.CurrentDomain.BaseDirectory;
        static string signType = "01";
        static string charset = "UTF-8";
        static string pfxFilePath = dirPath + "MID_RSA_PRIVATE_KEY.pfx";
        static string pfxPassword = "123456";
        static string cerFilePath = dirPath + "SAND_PUBLIC_KEY.cer";
        static string Mid = "";
        static string ServerUrl = "https://cashier.sandpay.com.cn/gateway/api/order/refund";


        static JavaScriptSerializer jsonSer = new JavaScriptSerializer();
        protected void Page_Load(object sender, EventArgs e)
        {
            var mid = Request.Form["mid"];
            var pfxFileName = mid + ".pfx";
            var cerFileName = mid + ".cer";
            pfxPassword = Request.Form["pfxPassword"];

            var dirPath = AppDomain.CurrentDomain.BaseDirectory + "sandeRefund\\";
            pfxFilePath = dirPath + pfxFileName;
            cerFilePath = dirPath + cerFileName;

            Dictionary<string, object> msgData = new Dictionary<string, object>();
            Dictionary<string, object> head = new Dictionary<string, object>();
            Dictionary<string, object> body = new Dictionary<string, object>();

            head["version"] = "1.0";
            head["method"] = "sandpay.trade.refund";
            head["accessType"] = "1";   //1普通商户接入，2平台商户接入
            head["channelType"] = "07"; //通道类型 07 PC, 08 移动
            head["mid"] = Request.Form["mid"]; //商户ID
            Mid = Request.Form["mid"];   //商户ID
            head["plMid"] = Request.Form["plMid"]; //平台ID 接入类型为2时必填
            head["reqTime"] = DateTime.UtcNow.AddHours(8).ToString("yyyyMMddHHmmss");
            head["productId"] = "00000016"; //产品类型，需要参考附录

            body["orderCode"] = GetGuid(); //退款订单号
            body["oriOrderCode"] = Request.Form["oriOrderCode"];  //原订单Id
            body["refundAmount"] = Request.Form["refundAmount"];  //退款金额
            body["notifyUrl"] = "http://47.92.68.54/SDNotifyUrl"; //异步通知回调地址
            body["refundReason"] = "退货";
            body["extend"] = "201708031341080000"; //扩展域

            msgData["head"] = head;
            msgData["body"] = body;

            var response = SendMessageSample(pfxFilePath, pfxPassword, cerFilePath, ServerUrl, signType, charset, msgData);

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(response.data);
            Response.Write(json);
            Response.End();
        }

        static string GetGuid()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt64(buffer, 0).ToString();
        }

        static CryptUtils_Csharp.MessageWorker.trafficMessage SendMessageSample(
           string pfxFilePath,
           string pfxPassword,
           string cerFilePath,
           string ServerUrl,
           string signType,
           string charset,
           Dictionary<string, object> msgData)
        {

            //报文结构体初始化
            CryptUtils_Csharp.MessageWorker.trafficMessage msgRequestSource = new CryptUtils_Csharp.MessageWorker.trafficMessage();
            //发送类实体化
            CryptUtils_Csharp.MessageWorker worker = new CryptUtils_Csharp.MessageWorker();


            worker.PFXFile = pfxFilePath; //商户pfx证书路径
            worker.PFXPassword = pfxPassword;  //商户pfx证书密码
            worker.CerFile = cerFilePath; //杉德cer证书路径


            msgRequestSource.charset = charset; //商户号
            msgRequestSource.signType = signType;        //交易代码
            msgRequestSource.extend = "";               //扩展域

            //报文体json
            JavaScriptSerializer jsonSer = new JavaScriptSerializer();
            msgRequestSource.data = jsonSer.Serialize(msgData);



            log.Debug("待发送报文为：" + msgRequestSource.data);
            CryptUtils_Csharp.MessageWorker.trafficMessage respMessage = worker.postMessage(ServerUrl, msgRequestSource);
            log.Debug("服务器返回为：" + respMessage.data);

            return respMessage;
        }
    }
}