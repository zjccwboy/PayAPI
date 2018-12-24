using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TianXiaPayWebTester.BankUnionORCode
{
    public partial class PayShow : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var request = ReadRequestInfo();
            this.Response.Write(request);
            this.Response.Write("<br><a href=\"ReadNotify.aspx\">查看异步通知</a>");
            this.Response.End();
        }

        private string ReadRequestInfo()
        {
            string notifyInfo = HttpContext.Current.Request.PhysicalApplicationPath + @"BankUnionQRCodeRequestInfo.temp";          
            using (var fileSteam = new FileStream(notifyInfo, FileMode.Open, FileAccess.Read))
            {
                using (var sr = new StreamReader(fileSteam, Encoding.UTF8))
                {
                    var result = sr.ReadToEnd();
                    return result;
                }
            }
        }
    }
}