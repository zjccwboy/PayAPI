using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TianXiaPayWebTester.QuickH5
{
    public partial class ReadNotify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var notify = ReadRequestInfo();
            if (string.IsNullOrEmpty(notify))
            {
                this.Response.Write("异步通知不存在，请稍后刷新。");
                this.Response.End();
            }
            else
            {
                this.Response.Write(notify);
                this.Response.End();
            }
        }

        private string ReadRequestInfo()
        {
            string notifyInfo = HttpContext.Current.Request.PhysicalApplicationPath + @"Notify.temp";
            using (var fileSteam = new FileStream(notifyInfo, FileMode.OpenOrCreate, FileAccess.ReadWrite))
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