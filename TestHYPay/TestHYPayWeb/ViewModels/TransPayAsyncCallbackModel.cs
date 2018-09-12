using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestHYPayWeb.ViewModles
{
    public class TransPayAsyncCallbackModel
    {
        public string hpMerCode { get; set; }
        public string orderNo { get; set; }
        public string transDate { get; set; }
        public string transStatus { get; set; }
        public string transAmount { get; set; }
        public string actualAmount { get; set; }
        public string transSeq { get; set; }
        public string statusCode { get; set; }
        public string statusMsg { get; set; }
        public string signature { get; set; }
    }
}