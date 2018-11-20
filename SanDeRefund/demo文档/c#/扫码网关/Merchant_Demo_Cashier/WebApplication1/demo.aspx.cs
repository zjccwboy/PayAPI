using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;

namespace WebApplication1
{
    public partial class demo : System.Web.UI.Page
    {
        string logHeader = "WebApplication_";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.HttpMethod == "POST")
            {
                string dirPath = System.AppDomain.CurrentDomain.BaseDirectory;
                string signType = "01";
                string charset = "UTF-8";
                string pfxFilePath = dirPath + "MID_RSA_PRIVATE_KEY.pfx";
                string pfxPassword = "123456";
                string cerFilePath = dirPath + "SAND_PUBLIC_KEY.cer";

                //定义Json转换类
                JavaScriptSerializer jsonSer = new JavaScriptSerializer();
                Dictionary<string, object> dicRslt;

                //解密后的服务器返回
                CryptUtils_Csharp.MessageWorker.trafficMessage resp;

                string serverUrl = "http://61.129.71.103:8003/gateway/api/order/pay";
                Dictionary<string, object> msgData = new Dictionary<string, object>();


                string strDateTime = System.DateTime.Now.ToString("yyyyMMddhhmmss");

                Dictionary<string, object> head = new Dictionary<string, object>();
                head["version"] = "1.0";
                head["method"] = "sandpay.trade.pay";
                head["productId"] = "00000007";
                head["accessType"] = "1";
                head["mid"] = Request.Form["mid"];
                head["channelType"] = "07";

                head["reqTime"] = strDateTime;
                head["subMid"] = "";
                head["subMidAddr"] = "";
                head["subMidName"] = "";

                Dictionary<string, object> body = new Dictionary<string, object>();
                body["bizExtendParams"] = Request.Form["bizExtendParams"];
                body["body"] = Request.Form["body"];
                body["clientIp"] = Request.Form["clientIp"];
                body["extend"] = Request.Form["extend"];
                body["frontUrl"] = Request.Form["frontUrl"];
                body["merchExtendParams"] = Request.Form["merchExtendParams"];
                body["notifyUrl"] = Request.Form["notifyUrl"];
                body["operatorId"] = Request.Form["operatorId"];
                body["orderCode"] = Request.Form["orderCode"];
                body["payExtra"] = (Request.Form["payExtra"].Length == 0) ? new Dictionary<string, object>() : jsonSer.Deserialize<Dictionary<string, object>>(Request.Form["payExtra"]);
                body["payMode"] = Request.Form["payMode"];
                body["storeId"] = Request.Form["storeId"];
                body["subject"] = Request.Form["subject"];
                body["terminalId"] = Request.Form["terminalId"];
                body["totalAmount"] = Request.Form["totalAmount"];
                body["txnTimeOut"] = Request.Form["txnTimeOut"];

                msgData["head"] = head;
                msgData["body"] = body;

                System.Diagnostics.Debug.WriteLine(jsonSer.Serialize(msgData));


                
                resp = SendMessageSample(
                                            pfxFilePath,
                                            pfxPassword,
                                            cerFilePath,
                                            serverUrl,
                                            signType,
                                            charset,
                                            msgData
                                            );



                dicRslt = jsonSer.DeserializeObject(resp.data) as Dictionary<string, object>;
                Dictionary<string, object> respHead = (Dictionary<string, object>)dicRslt["head"];
                Dictionary<string, object> respBody = (Dictionary<string, object>)dicRslt["body"];

                if ("000000" != respHead["respCode"].ToString())
                {
                    Response.Write("<script>alert('" + resp.data + "')</script>");

                    return;
                }

                string credential = System.Web.HttpUtility.UrlEncode( respBody["credential"].ToString());

                //req.setAttribute();
                // String url = "jsp/middle.jsp";

                Response.Redirect("middle.aspx?JWP_ATTR=" + credential, true);
                //Server.Execute("middle.aspx?JWP_ATTR=" + credential);

               // Request.getRequestDispatcher(url).forward(req, resp);

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


            CryptUtils_Csharp.Logger.Logging(logHeader, "待发送报文为：" + msgRequestSource.data, true);

            CryptUtils_Csharp.MessageWorker.trafficMessage respMessage = worker.postMessage(ServerUrl, msgRequestSource);

            CryptUtils_Csharp.Logger.Logging(logHeader, "服务器返回为：" + respMessage.data, true);
            return respMessage;
        }

    }
}