using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ThirdParty_KLPPay;

namespace KLTPayAPITester.BankB2C
{
    public partial class Start : System.Web.UI.Page
    {
        public string orderNumber { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            this.orderNumber = GuidUtils.GetLongStringGuid();
        }
    }
}