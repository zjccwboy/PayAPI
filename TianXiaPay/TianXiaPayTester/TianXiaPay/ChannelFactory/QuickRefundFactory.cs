using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace TianXiaPay
{
    //public class QuickRefundFactory : BaseQuickRefundFactory
    //{
    //    public QuickRefundFactory(string key) : base(key) { }

    //    public QuickRefundResponseModel GetRefundResult(QuickRefundRequestModel request)
    //    {

    //    }
    //}

    public class QuickRefundFactory : ChannelFactory<QuickRefundRequestModel, QuickRefundOptions>
    {
        //正式环境请求URL：http://api.tfb8.com/cgi-bin/v2.0/api_pay_single.cgi
        //测试环境请求URL：http://apitest.tfb8.com/cgi-bin/v2.0/api_pay_single.cgi

        public override string Url => "http://apitest.tfb8.com/cgi-bin/v2.0/api_pay_single.cgi";

        public QuickRefundFactory(string key) : base(key) { }

        public override QuickRefundRequestModel GenerateRequestModel(QuickRefundOptions options)
        {
            var result = new QuickRefundRequestModel
            {
                version = "1.0",
                spid = options.spid,
                sp_serialno = options.sp_serialno,
                sp_reqtime = DateTime.UtcNow.AddHours(8).ToString("yyyyMMddHHmmss"),
                tran_amt = options.tran_amt,
                cur_type = "1",
                pay_type = "1",
                acct_name = options.acct_name,
                acct_id = options.acct_id,
                acct_type = "0",
                business_type = options.business_type,
                memo = "6",
            };
            result.sign = this.GenerateSign(result);
            return result;
        }

        public string GetRefundResult(string encData)
        {
            var webClient = new WebClient();
            var response = webClient.UploadString(this.Url, "POST", $"cipher_data={encData}");
            return response;
        }

        //private string HttpPost(string POSTURL, string PostData)
        //{
        //    //发送请求的数据
        //    HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(POSTURL);
        //    myHttpWebRequest.Method = "POST";
        //    myHttpWebRequest.UserAgent = "chrome";
        //    UTF8Encoding encoding = new UTF8Encoding();
        //    byte[] byte1 = encoding.GetBytes(PostData);
        //    myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
        //    myHttpWebRequest.ContentLength = byte1.Length;
        //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
        //    Stream newStream = myHttpWebRequest.GetRequestStream();
        //    newStream.Write(byte1, 0, byte1.Length);
        //    newStream.Close();

        //    //发送成功后接收返回的信息
        //    HttpWebResponse response = (HttpWebResponse)myHttpWebRequest.GetResponse();
        //    string lcHtml = string.Empty;
        //    Encoding enc = Encoding.GetEncoding("UTF-8");
        //    Stream stream = response.GetResponseStream();
        //    StreamReader streamReader = new StreamReader(stream, enc);
        //    lcHtml = streamReader.ReadToEnd();
        //    return lcHtml;
        //}
    }
}
