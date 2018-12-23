using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TianXiaPay;

namespace TianXiaPayWebTester.QuickRefund
{
    public partial class Default : System.Web.UI.Page
    {
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

            var requestLog = factory.GenerateRequestFormString(request);
            WriteLog("2.1 单笔代付接口请求参数", requestLog);

            var response = factory.GetRefundResult(request);
            WriteLog("2.1 单笔代付接口返回参数", response);

            var echoMessage = $"请求报文：{requestLog}\r\n \r\n 返回报文：{response}";

            Response.Write(echoMessage);
            Response.End();
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