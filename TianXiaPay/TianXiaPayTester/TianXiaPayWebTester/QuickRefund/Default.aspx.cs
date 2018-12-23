using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using TianXiaPay;

namespace TianXiaPayWebTester.QuickRefund
{
    public partial class Default : System.Web.UI.Page
    {
        private const string PublicKey = @"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCjDrkoVbyv4jTxeKtKEiK2mZie
zQvfJV3sGhiwOnB+By5sa5Sa6Ls4dt5AGVqKHxyQVKRpu/utwtEt2MijWx45P1y2
xGe7oDz2hUXP0j8sSa1NP26TmWHwO7czgJxxrdJ6RNqskSfjwsa5YMsqmcrumxUI
xeCg5EOkgU26bnPoZQIDAQAB";

        private const string PrivateKey = @"MIICeAIBADANBgkqhkiG9w0BAQEFAASCAmIwggJeAgEAAoGBAL80WRy2i3JLrTHp
8f0gYVGd+lu9k6ZSixMCEoRVsNbpt3NIdXI/aWW/G/GfW0c4TbacyhTV7Yn+I+eM
rXjIFoi1CoYi0eIIQrZC5cU+KBA+reYDbAX30o8K4d90QTlPTzueW6TP/c2f/E7U
b0rHXj7d6Als1jO1oE0uiErQKTO9AgMBAAECgYEAkKuYHVyVaBaQZirf2SmN2QZS
LuXi+L6N0gUIY66+je3qy0Rw8M+//Kc8CncLxnh4fIpncJppD7cGDaXof3HNcbMj
IpBvwDJhpqLVH7JNx16HeQ73uWcqCHMLecNFQR8XAEiQJ0JbGYuGOxUqFYG87YEK
XFIBGL50+ZlBHARRGoECQQD7JPdZhMp6dBfTp93/6Oa+OvBl6wMDAvUUIT1RAF4U
VeQ+1vg0BBtJE0TPGnacO3DcCobA+KKpDBz88BxRmvIhAkEAwua15+io6vUciObB
UPAuNrJEEdW9KxK/rocI5B/cIc4zSIgrl5nLvy5uqMuXiL94E0Oa4dwDkbbtaeuf
wA8GHQJBAKOehMu8mNHImtFZN2gHi3T6Hy63OsIWhib0NOd17tUe1FIgaZox5rjo
Jdcr7YSBsViaPwqvsgGik6wynrCH2yECQDAKYigppwlTJZdxGZFzwlBlHHYw8xHc
6zZ/vmdMmxwSEX39YpFZrWkQbuJYXJ+uYlCNR24IpzCRoG+NTrEugtkCQQCCanl2
vQTbK7gW4BXVuwcc6CxvPCEo7BHESTVCgV0hwSIwusE6UfQKVRGM/yjok8U2Y0F2
C5yhJD/WdHvYkWKv";

        private const string key = "12345";
        private const string spid = "1800046681";
        private const string acct_name = "陶建波";
        private const string acct_id = "6217991610012522927";
        private const string business_type = "20101";
        protected void Page_Load(object sender, EventArgs e)
        {
            var options = new QuickRefundOptions
            {
                spid = spid,
                sp_serialno = GenerateOrderNo(),
                tran_amt = "100",
                business_type = business_type,
                acct_name = acct_name,
                acct_id = acct_id,
            };

            var factory = new QuickRefundFactory(key);
            var request = factory.GenerateRequestModel(options);

            var data = factory.GenerateRequestFormString(request);
            WriteLog("2.1 单笔代付接口请求参数", data);

            var pubKey = RSAUtils.RSAPublicKeyJava2DotNetP(PublicKey);
            string encData = RSAUtils.RSAEncrypt(data, pubKey);
            var cipher_data = Server.UrlEncode(encData);

            var response = factory.GetRefundResult(cipher_data);
            var model = FormatResponse(response);

            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                Formatting = Newtonsoft.Json.Formatting.Indented,
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
            };
            var dendata = Newtonsoft.Json.JsonConvert.SerializeObject(model, settings);

            WriteLog("2.1 单笔代付接口返回参数", dendata);

            Response.Write($"天付宝代付API<br>");
            Response.Write($"请求地址：{factory.Url}<br>");

            Response.Write($"请求参数未加密：{data}<br>");
            Response.Write($"请求参数已加密cipher_data：{cipher_data}<br>");

            Response.Write($"返回参数密文：{response}<br>");
            Response.Write($"返回参数明文：{dendata}<br>");
            Response.End();
        }

        private class ResponseModel
        {
            public string retcode { get; set; }
            public string retmsg { get; set; }
            public string cipher_data { get; set; }
        }

        private ResponseModel FormatResponse(string rpXml)
        {
            var result = new ResponseModel();
            using (var sr = new StringReader(rpXml))
            {
                var el = XElement.Load(sr);
                result.retcode = el.XPathSelectElement("/retcode").Value;
                result.retmsg = el.XPathSelectElement("/retmsg").Value;
                result.retmsg = Encoding.UTF8.GetString(Encoding.GetEncoding("gb2312").GetBytes(result.retmsg));
                var cipher_data = el.XPathSelectElement("/cipher_data").Value;

                var priKey = RSAUtils.RSAPrivateKeyJava2DotNet(PrivateKey);
                result.cipher_data = RSAUtils.RSADecrypt(priKey, Convert.FromBase64String(cipher_data), Encoding.UTF8);
            }
            return result;
        }

        private string GenerateOrderNo()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt64(buffer, 0).ToString();
        }

        private void WriteLog(string caption, string log)
        {
            try
            {
                string strPath = strPath = HttpContext.Current.Request.PhysicalApplicationPath + @"log\Tester.log";
                using (var fileSteam = new FileStream(strPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    using (var streamWrite = new StreamWriter(fileSteam, Encoding.GetEncoding("gb2312")))
                    {
                        streamWrite.BaseStream.Seek(0, SeekOrigin.End);
                        streamWrite.Write(caption + "：\r\n");
                        streamWrite.Write(log + "\r\n");
                        streamWrite.Write("------------------------------------------------------------\r\n");
                        streamWrite.Write("\r\n");
                    }
                }
            }
            catch { }
        }
    }
}