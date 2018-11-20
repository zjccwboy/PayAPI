using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Refund
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));

        static string dirPath = System.AppDomain.CurrentDomain.BaseDirectory;
        static string signType = "01";
        static string charset = "UTF-8";
        static string pfxFilePath = dirPath + "MID_RSA_PRIVATE_KEY.pfx";
        static string pfxPassword = "123456";
        static string cerFilePath = dirPath + "SAND_PUBLIC_KEY.cer";
        static string Mid = "";
        static string ServerUrl = "http://61.129.71.103:8003/gateway/api/order/refund";
        static JavaScriptSerializer jsonSer = new JavaScriptSerializer();

        static void Main(string[] args)
        {
            Refund();
        }

        static void Refund()
        {
            string strDateTime = System.DateTime.Now.ToString("yyyyMMddhhmmss");

            Dictionary<string, object> msgData = new Dictionary<string, object>();
            Dictionary<string, object> head = new Dictionary<string, object>();
            Dictionary<string, object> body = new Dictionary<string, object>();

            head["version"] = "1.0";
            head["method"] = "sandpay.trade.refund";
            head["accessType"] = "1";   //1普通商户接入，2平台商户接入
            head["channelType"] = "07"; //通道类型 07 PC, 08 移动
            head["mid"] = "10020025"; //商户ID
            Mid = "10020025";   //商户ID
            head["plMid"] = ""; //平台ID 接入类型为2时必填
            head["reqTime"] = DateTime.UtcNow.AddHours(8).ToString("yyyyMMddHHmmss");
            head["productId"] = "00000016";

            body["orderCode"] = "201708031341080000";
            body["oriOrderCode"] = "201708031341080000";
            body["refundAmount"] = "000000000001";
            body["notifyUrl"] = "http://47.92.68.54/SDNotifyUrl";
            body["refundReason"] = "退货";
            body["extend"] = "201708031341080000";

            msgData["head"] = head;
            msgData["body"] = body;

            var response = SendMessageSample(pfxFilePath, pfxPassword, cerFilePath, ServerUrl, signType, charset, msgData);
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
