using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestHYPayWeb.ViewModles
{
    public class TransQueryModel
    {
        public string insCode { get; set; }
        public string insMerchantCode { get; set; }
        public string hpMerCode { get; set; }
        public string orderNo { get; set; }
        public string transDate { get; set; }
        public string transSeq { get; set; }
        public string productType { get; set; }
        public string paymentType { get; set; }
        public string nonceStr { get; set; }
        public string signature { get; set; }
    }
}