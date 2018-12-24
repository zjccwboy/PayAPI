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
    public partial class Default : System.Web.UI.Page
    {
        const string Spid = "1800776625";
        const string Key = "12345";


        public string Title => "银联扫码支付";
        public string Amount => "100";
        public string OrderNo => GenerateOrderNo();
        public string QRCode { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            var options = new BankUnionORCodeOptions
            {
                spid = Spid,
                sp_billno = this.OrderNo,
                tran_amt = this.Amount,
            };

            var factory = new BankUnionORCodeFactory(Key);
            var request = factory.GenerateRequestModel(options);
            var responseString = factory.GetResponseString(request);

            var responseModel = factory.GenerateResponseModel(responseString);
            this.QRCode = QRCodeHelper.GetQRCodeBase64String(responseModel.qrcode);

            var data = factory.GenerateRequestFormString(request);
            WriteLog("银联扫码支付", data);
            WriteTemp($"天付宝银联渠道支付接口<br>请求地址：{factory.Url}<br>请求参数：{data}<br>返回参数：{responseString}<br>");
        }

        private void WriteTemp(string tempInfo)
        {
            try
            {
                //删掉上次请求参数
                string requestFile = HttpContext.Current.Request.PhysicalApplicationPath + @"BankUnionQRCodeRequestInfo.temp";
                if (File.Exists(requestFile))
                {
                    File.Delete(requestFile);
                }

                //删掉上次的异步通知
                var notifyFile = HttpContext.Current.Request.PhysicalApplicationPath + @"BankUnionQRCodeNotify.temp";
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

        private string GenerateOrderNo()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt64(buffer, 0).ToString();
        }
    }
}