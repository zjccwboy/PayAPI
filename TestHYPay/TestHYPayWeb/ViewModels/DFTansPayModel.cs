using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestHYPayWeb.ViewModles
{
    public class DFTansPayModel
    {
        public string insCode { get; set; }
        public string insMerchantCode { get; set; }
        public string hpMerCode { get; set; }
        public string orderNo { get; set; }
        public string orderDate { get; set; }
        public string orderTime { get; set; }
        public string currencyCode { get; set; }
        public string orderAmount { get; set; }
        public string orderType { get; set; }
        public string certType { get; set; }
        public string certNumber { get; set; }
        public string accountType { get; set; }
        public string accountName { get; set; }
        public string accountNumber { get; set; }
        public string mainBankName { get; set; }
        public string mainBankCode { get; set; }
        public string openBranchBankName { get; set; }
        public string mobile { get; set; }
        public string attach { get; set; }
        public string nonceStr { get; set; }
        public string signature { get; set; }
    }
}