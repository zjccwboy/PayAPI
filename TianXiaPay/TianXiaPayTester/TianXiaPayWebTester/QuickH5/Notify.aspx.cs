using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TianXiaPay;

namespace TianXiaPayWebTester.QuickH5
{
    public partial class Notify : System.Web.UI.Page
    {

        public QuickH5PayNotifyModel NotifyModel { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.NotifyModel = new QuickH5PayNotifyModel
            {
                retcode = this.Request["retcode"],
                retmsg = this.Request["retmsg"],
                spid = this.Request["spid"],
                spbillno = this.Request["spbillno"],
                listid = this.Request["listid"],
                money = this.Request["money"],
                cur_type = this.Request["cur_type"],
                result = this.Request["result"],
                pay_type = this.Request["pay_type"],
                user_type = this.Request["user_type"],
                attach = this.Request["attach"],
                sign = this.Request["sign"],
                encode_type = this.Request["encode_type"],
            };

            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                Formatting = Newtonsoft.Json.Formatting.Indented,
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
            };
            var notifyInfo = Newtonsoft.Json.JsonConvert.SerializeObject(this.NotifyModel, settings);

            WriteLog("3.2 银联H5支付结果异步回调", notifyInfo);
            WriteNotify($"异步回调参数：{notifyInfo}<br>");
        }

        private void WriteNotify(string notify)
        {
            //删掉上次的异步通知
            var notifyFile = HttpContext.Current.Request.PhysicalApplicationPath + @"Notify.temp";
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