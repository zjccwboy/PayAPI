using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestHYPayWeb.ViewModles
{
    public class DFTransQueryModel
    {
        public string insCode { get; set; }
        public string insMerchantCode { get; set; }
        public string hpMerCode { get; set; }
        public string accountType { get; set; }
        public string nonceStr { get; set; }
        public string signature { get; set; }
    }
}