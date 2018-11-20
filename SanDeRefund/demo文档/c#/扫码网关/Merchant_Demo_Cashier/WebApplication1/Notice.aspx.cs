using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CryptUtils_Csharp;

namespace WebApplication1
{
    public partial class Notice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.HttpMethod == "POST")
            {
                //不要调用UrlDecodeMessage，直接拆分请求字串，进行验签

                // 获取异步通知请求字串
                string requestString = Request.Form.ToString();
                // 分割请求字串
                string[] EncryptBody = requestString.Split('&');

                //分割后的请求字串转换通信报文实体
                MessageWorker.trafficMessage message = new MessageWorker.trafficMessage();
                for (int i = 0; i < EncryptBody.Length; i++)
                {
                    string[] tmp = EncryptBody[i].Split('=');
                    switch (tmp[0])
                    {
                        //需要添加引用System.Web，用于url转码，处理base64产生的+/=
                        case "charset": message.charset = EncryptBody[i].Replace("charset=", "").Trim('"'); break;
                        case "signType": message.signType = EncryptBody[i].Replace("signType=", "").Trim('"'); break;
                        case "data": message.data = EncryptBody[i].Replace("data=", "").Trim('"'); break;
                        case "sign": message.sign = EncryptBody[i].Replace("sign=", "").Trim('"'); break;
                        case "extend": message.extend = EncryptBody[i].Replace("extend=", "").Trim('"'); break;
                    }
                }
                //验签，验签结果会提现在sign字段，以true或false方式提现
                MessageWorker worker = new MessageWorker();
                worker.CheckSignMessageAfterResponse(message);
            }
        }
    }
}