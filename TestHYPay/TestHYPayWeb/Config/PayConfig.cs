using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestHYPayWeb.Config
{
    public class PayConfig
    {
        public static string insCode = "80000384";
        public static string insMerchantCode = "887581298600467";
        public static string hpMerCode = "WKJGWKQTCS@20180813173307";
        public static string currencyCode = "156";
        public static string productType = "100000";
        public static string paymentType = "2008";
        public static string frontUrl = "http://47.92.68.54:8000/TransPaySyncCallback.aspx";
        public static string backUrl = "http://47.92.68.54:8000/TransPayAsyncCallback.aspx";
        public static string signKey = "3F7DB75AFBE34A4B40ECD0CC4A8B6492";
    }
}