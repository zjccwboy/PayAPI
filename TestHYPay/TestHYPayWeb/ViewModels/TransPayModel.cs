using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestHYPayWeb.ViewModles
{
    public class TransPayModel
    {
        public string orderAmount { get; set; }
        public string name { get; set; }
        public string idNumber { get; set; }
        public string accNo { get; set; }
        public string telNo { get; set; }
        public string insCode { get; set; }
        public string insMerchantCode { get; set; }
        public string hpMerCode { get; set; }
        public string orderNo { get; set; }
        public string orderTime { get; set; }
        public string currencyCode { get; set; }
        public string productType { get; set; }
        public string paymentType { get; set; }
        public string merGroup { get; set; }
        public string nonceStr { get; set; }
        public string frontUrl { get; set; }
        public string backUrl { get; set; }
        public string signature { get; set; }
    }
}