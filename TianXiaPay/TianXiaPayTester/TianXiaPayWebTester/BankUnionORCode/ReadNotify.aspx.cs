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
    public partial class ReadNotify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var notify = ReadNotifyInfo();
            this.Response.Write(notify);
            this.Response.End();
        }

        private string ReadNotifyInfo()
        {
            string notifyInfo = HttpContext.Current.Request.PhysicalApplicationPath + @"BankUnionQRCodeNotify.temp";
            if (!File.Exists(notifyInfo))
                return string.Empty;
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