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
    public partial class Return : System.Web.UI.Page
    {

        public QuickH5PayPageReturnModel ReturnModel { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            this.ReturnModel = new QuickH5PayPageReturnModel
            {
                spid = this.Request["spid"],
                spbillno = this.Request["spbillno"],
                money = this.Request["money"],
                cur_type = this.Request["cur_type"],
                listid = this.Request["listid"],
                result = this.Request["result"],
                pay_type = this.Request["pay_type"],
                user_type = this.Request["user_type"],
                sign = this.Request["sign"],
                encode_type = this.Request["encode_type"],
            };

            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                Formatting = Newtonsoft.Json.Formatting.Indented,
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
            };
            var returnInfo = Newtonsoft.Json.JsonConvert.SerializeObject(this.ReturnModel, settings);
            WriteLog("3.3 银联H5支付页面回调", returnInfo);

            var request = ReadRequestInfo();
            this.Response.Write(request);
            this.Response.Write($"页面回调参数：{returnInfo}");
            this.Response.Write("<br><a href=\"ReadNotify.aspx\">查看异步通知</a>");
            this.Response.End();
        }

        private string ReadRequestInfo()
        {
            string requestFile = HttpContext.Current.Request.PhysicalApplicationPath + @"RequestInfo.temp";
            using (var fileSteam = new FileStream(requestFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                using(var sr = new StreamReader(fileSteam, Encoding.UTF8))
                {
                    var result = sr.ReadToEnd();
                    return result;
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