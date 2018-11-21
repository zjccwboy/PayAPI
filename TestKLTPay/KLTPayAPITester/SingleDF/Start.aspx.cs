using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ThirdParty_KLPPay;

namespace KLTPayAPITester.SingleDF
{
    public partial class Start : System.Web.UI.Page
    {
        public string orderNumber { get; set; }
        public string orderDate { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            this.orderNumber = GuidUtils.GetLongStringGuid();
            this.orderDate = DateTime.Now.ToString("yyyyMMdd");
        }
    }
}