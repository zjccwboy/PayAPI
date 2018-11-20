using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.KJZF.API
{
    public partial class bindCard_test : System.Web.UI.Page
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(bindCard_test));

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
                CryptUtils_Csharp.MessageWorker.trafficMessage resp;

                string serverUrl = Request.Form["urlstring"];

                Dictionary<string, object> msgData = new Dictionary<string, object>();
                Dictionary<string, object> head = new Dictionary<string, object>();
                Dictionary<string, object> body = new Dictionary<string, object>();

                head["version"] = "1.0";
                head["method"] = "sandPay.fastPay.apiPay.bindCard";
                head["accessType"] = Request.Form["accessType"];
                head["channelType"] = Request.Form["channelType"];
                head["mid"] = Request.Form["mid"];
                Mid = Request.Form["mid"];
                head["reqTime"] = Request.Form["reqTime"];
                head["productId"] = "00000018";
                head["plMid"] = Request.Form["plMid"];
                head["subMid"] = Request.Form["subMid"];
                head["subMidAddr"] = Request.Form["subMidAddr"];
                head["subMidName"] = Request.Form["subMidName"];

                body["userId"] = Request.Form["userId"];
                body["applyNo"] = Request.Form["applyNo"];
                body["cardNo"] = Request.Form["cardNo"];
                body["userName"] = Request.Form["userName"];
                body["phoneNo"] = Request.Form["phoneNo"];
                body["certificateType"] = Request.Form["certificateType"];
                body["certificateNo"] = Request.Form["certificateNo"];
                body["creditFlag"] = Request.Form["creditFlag"];
                body["checkNo"] = Request.Form["checkNo"];//#Cvn2码：贷记卡绑卡时必送
                body["checkExpiry"] = Request.Form["checkExpiry"];//#卡有效期：贷记卡绑卡时必送
                body["notifyUrl"] = Request.Form["notifyUrl"];//#卡有效期：贷记卡绑卡时必送
                body["frontUrl"] = Request.Form["frontUrl"];//#卡有效期：贷记卡绑卡时必送

                msgData["head"] = head;
                msgData["body"] = body;

                resp = SendMessageSample(
                                            pfxFilePath,
                                            pfxPassword,
                                            cerFilePath,
                                            serverUrl,
                                            signType,
                                            charset,
                                            msgData
                                            );


                if (resp.data == null)
                {
                    //resp.extend复用，抛出错误信息，正式生产不能这么搞
                    Response.Write("<script>alert('" + resp.extend + "')</script>");
                    log.Error("服务器返回为空或无法解析");
                    return;
                }

                Response.Write("<script>alert('" + resp.data + "')</script>");
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
