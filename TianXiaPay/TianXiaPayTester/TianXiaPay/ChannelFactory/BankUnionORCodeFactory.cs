using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace TianXiaPay
{
    public class BankUnionORCodeFactory : ChannelFactory<BankUnionQRCodeRequestModel, BankUnionORCodeOptions>
    {

        //测试环境：http://apitest.tfb8.com/cgi-bin/v2.0/api_yl_pay_apply.cgi
        //生产环境：http://upay.tfb8.com/cgi-bin/v2.0/api_yl_pay_apply.cgi

        public BankUnionORCodeFactory(string key) : base(key) { }
        public override string Url => "http://apitest.tfb8.com/cgi-bin/v2.0/api_yl_pay_apply.cgi";

        public override BankUnionQRCodeRequestModel GenerateRequestModel(BankUnionORCodeOptions options)
        {
            var result = new BankUnionQRCodeRequestModel
            {
                //sign_type = "MD5",
                //ver = "1",
                //input_charset = "GBK",
                notify_url = "http://47.92.68.54:8003/BankUnionORCode/Notify.aspx",
                pay_show_url = "http://47.92.68.54:8003/BankUnionORCode/PayShow.aspx",
                pay_type = "800201",
                tran_time = DateTime.UtcNow.AddHours(8).ToString("yyyyMMddHHmmss"),
                cur_type = "CNY",
                item_name = "Testing",
                spid = options.spid,
                sp_billno = options.sp_billno,
                tran_amt = options.tran_amt,
            };
            result.sign = this.GenerateSign(result).ToUpper();
            return result;
        }

        public string GetResponseString(BankUnionQRCodeRequestModel requestModel)
        {
            var data = this.GenerateRequestFormString(requestModel);
            var webClient = new WebClient();
            var response = webClient.UploadString(this.Url, data);
            return response;
        }

        public BankUnionORCodeResponseModel GenerateResponseModel(string responseString)
        {
            var result = new BankUnionORCodeResponseModel();

            using (var sr = new StringReader(responseString))
            {
                var el = XElement.Load(sr);

                result.cur_type = el.XPathSelectElement("/cur_type") == null ? null : el.XPathSelectElement("/cur_type").Value;
                result.listid = el.XPathSelectElement("/listid") == null ? null : el.XPathSelectElement("/listid").Value;
                result.pay_type = el.XPathSelectElement("/pay_type") == null ? null : el.XPathSelectElement("/pay_type").Value;
                result.qrcode = el.XPathSelectElement("/qrcode") == null ? null : el.XPathSelectElement("/qrcode").Value;
                result.retcode = el.XPathSelectElement("/retcode") == null ? null : el.XPathSelectElement("/retcode").Value;
                result.retmsg = el.XPathSelectElement("/retmsg") == null ? null : el.XPathSelectElement("/retmsg").Value;
                result.sign = el.XPathSelectElement("/sign") == null ? null : el.XPathSelectElement("/sign").Value;
                result.sp_billno = el.XPathSelectElement("/sp_billno") == null ? null : el.XPathSelectElement("/sp_billno").Value;
                result.spid = el.XPathSelectElement("/spid") == null ? null : el.XPathSelectElement("/spid").Value;
                result.sysd_time = el.XPathSelectElement("/sysd_time") == null ? null : el.XPathSelectElement("/sysd_time").Value;
                result.tran_amt = el.XPathSelectElement("/tran_amt") == null ? null : el.XPathSelectElement("/tran_amt").Value;
            }

            return result;
        }
    }
}
