using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TianXiaPay;

namespace TianXiaPayWebTester.QuickH5
{
    public partial class Default : System.Web.UI.Page
    {
        private const string key = "12345";
        private const string spid = "1800044038";
        private const string bank_accno = "6217991610012522927";
        private const string bank_acctype = "01";


        public QuickH5PayRequestModel RequestModel { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            var options = new QuickH5PayOptions
            {
                spid = spid,
                sp_userid = spid,
                spbillno = GenerateOrderNo(),
                money = "100",
                bank_accno = bank_accno,
                bank_acctype = bank_acctype,
            };

            var factory = new QuickH5PayFactory(key);
            this.RequestModel = factory.GenerateRequestModel(options);

            var webClient = new WebClient();
            var data = factory.GenerateRequestFormString(this.RequestModel);
            WriteLog("3.1 银联H5支付申请", data);
            WriteTemp($"3.1 银联H5支付申请<br>请求地址：{factory.Url}<br>请求参数：{data}<br>");
        }

        private string GenerateOrderNo()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt64(buffer, 0).ToString();
        }

        private void WriteTemp(string tempInfo)
        {
            try
            {
                //删掉上次请求参数
                string requestFile = HttpContext.Current.Request.PhysicalApplicationPath + @"RequestInfo.temp";
                if (File.Exists(requestFile))
                {
                    File.Delete(requestFile);
                }

                //删掉上次的异步通知
                var notifyFile = HttpContext.Current.Request.PhysicalApplicationPath + @"Notify.temp";
                if (File.Exists(notifyFile))
                {
                    File.Delete(notifyFile);
                }

                //写本次请求参数
                using (var fileSteam = new FileStream(requestFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    using (var streamWrite = new StreamWriter(fileSteam, Encoding.UTF8))
                    {
                        streamWrite.BaseStream.Seek(0, SeekOrigin.End);
                        streamWrite.Write(tempInfo);
                    }
                }
            }
            catch { }
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