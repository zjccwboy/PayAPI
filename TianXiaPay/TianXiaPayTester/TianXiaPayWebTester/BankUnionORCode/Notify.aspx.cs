using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TianXiaPay;

namespace TianXiaPayWebTester.BankUnionORCode
{
    public partial class Notify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var model = new BankUnionORCodeNotifyModel
            {
                sign_type = this.Request["sign_type"],
                input_charset = this.Request["input_charset"],
                sign = this.Request["sign"],
                retcode = this.Request["retcode"],
                retmsg = this.Request["retmsg"],
                notify_type = this.Request["notify_type"],
                listid = this.Request["listid"],
                sp_billno = this.Request["sp_billno"],
                refund_listid = this.Request["refund_listid"],
                pay_type = this.Request["pay_type"],
                tran_time = this.Request["tran_time"],
                tran_amt = this.Request["tran_amt"],
                tran_state = this.Request["tran_state"],
                sysd_time = this.Request["sysd_time"],
                refund_state = this.Request["refund_state"],
                item_name = this.Request["item_name"],
                item_attach = this.Request["item_attach"],
                card_status = this.Request["card_status"],
                enc_card = this.Request["enc_card"],
            };

            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                Formatting = Newtonsoft.Json.Formatting.Indented,
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
            };
            var notifyInfo = Newtonsoft.Json.JsonConvert.SerializeObject(model, settings);

            WriteLog("天付宝银联渠道支付接口", notifyInfo);
            WriteNotify($"异步回调参数：{notifyInfo}<br>");
        }

        private void WriteNotify(string notify)
        {
            //删掉上次的异步通知
            var notifyFile = HttpContext.Current.Request.PhysicalApplicationPath + @"BankUnionQRCodeNotify.temp";
            if (File.Exists(notifyFile))
            {
                File.Delete(notifyFile);
            }

            //写本次请求参数
            using (var fileSteam = new FileStream(notifyFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                using (var streamWrite = new StreamWriter(fileSteam, Encoding.UTF8))
                {
                    streamWrite.BaseStream.Seek(0, SeekOrigin.End);
                    streamWrite.Write(notify);
                }
            }
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