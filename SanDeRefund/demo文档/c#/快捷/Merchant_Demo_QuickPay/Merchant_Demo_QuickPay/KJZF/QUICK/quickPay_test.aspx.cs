using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using CryptUtils_Csharp;

namespace Merchant_Demo_QuickPay.KJZF.QUICK
{
    public partial class quickPay_test : System.Web.UI.Page
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(quickPay_test));

        static string dirPath = System.AppDomain.CurrentDomain.BaseDirectory;
        static string signType = "01";
        static string charset = "UTF-8";
        static string pfxFilePath = dirPath + "MID_RSA_PRIVATE_KEY.pfx";
        static string pfxPassword = "123456";
        static string cerFilePath = dirPath + "SAND_PUBLIC_KEY.cer";
        static string Mid = "";
        static JavaScriptSerializer jsonSer = new JavaScriptSerializer();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.HttpMethod == "POST")
            {
                string strDateTime = System.DateTime.Now.ToString("yyyyMMddhhmmss");

                //解密后的服务器返回
                string serverUrl = Request.Form["urlstring"];


                Dictionary<string, object> msgData = new Dictionary<string, object>();
                Dictionary<string, object> head = new Dictionary<string, object>();
                Dictionary<string, object> body = new Dictionary<string, object>();

                head["version"] = Request.Form["version"];
                head["method"] = Request.Form["method"];
                head["accessType"] = Request.Form["accessType"];
                head["channelType"] = Request.Form["channelType"];
                head["mid"] = Request.Form["mid"];
                Mid = Request.Form["mid"];
                head["plMid"] = Request.Form["plMid"];
                head["reqTime"] = Request.Form["reqTime"];
                head["productId"] = Request.Form["productId"];

                body["userId"] = Request.Form["userId"];
                body["orderCode"] = Request.Form["orderCode"];
                body["orderTime"] = Request.Form["orderTime"];
                body["totalAmount"] = Request.Form["totalAmount"];
                body["subject"] = Request.Form["subject"];
                body["body"] = Request.Form["body"];
                body["currencyCode"] = Request.Form["currencyCode"];
                body["notifyUrl"] = Request.Form["notifyUrl"];
                body["frontUrl"] = Request.Form["frontUrl"];
                body["clearCycle"] = Request.Form["clearCycle"];
                body["extend"] = Request.Form["extend"];

                msgData["head"] = head;
                msgData["body"] = body;


                Dictionary<string, object> signedMessage = makeMessage(pfxFilePath,
                pfxPassword,
                cerFilePath,
                signType,
                charset,
                msgData);

                //尽量使用POST，get有可能会报请求字符串超长的错误
                Server.Transfer("~/tools/webtransf.aspx?urlstr=" + serverUrl + "&msg=" + jsonSer.Serialize(signedMessage));


           }
        }







        protected CryptUtils_Csharp.MessageWorker.trafficMessage SendMessageSample(
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


        protected Dictionary<string, object> makeMessage(
         string pfxFilePath,
         string pfxPassword,
         string cerFilePath,
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


            CryptUtils_Csharp.MessageWorker.trafficMessage msgRequest = worker.SignMessageBeforePost(msgRequestSource);


            //string signedMessage = worker.UrlEncodeMessage(msgRequest);
            Dictionary<string, object> signedMessageDic = new Dictionary<string, object>();

            signedMessageDic["charset"] = msgRequest.charset;
            signedMessageDic["signType"] = msgRequest.signType;
            signedMessageDic["data"] = msgData;// msgRequest.data;
            signedMessageDic["sign"] = System.Web.HttpUtility.UrlEncode(msgRequest.sign);
            signedMessageDic["extend"] = msgRequest.extend;
            string signedMessage = jsonSer.Serialize(signedMessageDic);
            log.Debug("服务器返回为：" + signedMessage);
            return signedMessageDic;
            // return System.Web.HttpUtility.UrlEncode(signedMessage);
        }
    }




}
