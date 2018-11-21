using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThirdParty_KLPPay.Models
{
    public class PayGateWayPCModel : BaseModel<PayGateWayPContent, PayGateWayPCHead>
    {
        public override PayGateWayPContent content { get; set; }
        public override PayGateWayPCHead head { get; set; }
    }

    public class PayGateWayPCHead : BaseHead
    {
        public string merchantId { get; set; }
        public string signType { get; set; }
        public string transactType { get; set; }
        public string version { get; set; }
    }

    public class PayGateWayPContent
    {
        public string ext1 { get; set; }
        public string ext2 { get; set; }
        public string issuerId { get; set; }
        public int orderAmount { get; set; }
        public int orderCurrency { get; set; }
        public string orderDateTime { get; set; }
        public string orderExpireDatetime { get; set; }
        public string orderNo { get; set; }
        public int payType { get; set; }
        public string payerAcctNo { get; set; }
        public string payerEmaill { get; set; }
        public string payerName { get; set; }
        public string payerTelPhone { get; set; }
        public string pickupUrl { get; set; }
        public string productDesc { get; set; }
        public string productId { get; set; }
        public string productName { get; set; }
        public int? productNum { get; set; }
        public decimal? productPrice { get; set; }
        public string receiveUrl { get; set; }
        public string termId { get; set; }
    }

    public class PayGateWayPCModelResponse
    {
        public string responseCode { get; set; }
        public string responseMsg { get; set; }
        public string requestId { get; set; }
        public string mchtId { get; set; }
        public string signMsg { get; set; }
        public string signType { get; set; }
        public string version { get; set; }
        public string payType { get; set; }
        public string issuerId { get; set; }
        public string mchtOrderId { get; set; }
        public string orderNo { get; set; }
        public string orderDatetime { get; set; }
        public string orderAmount { get; set; }
        public string payDatetime { get; set; }
        public string ext1 { get; set; }
        public string ext2 { get; set; }
        public string payResult { get; set; }
        public string redirectUrl { get; set; }
        public string payData { get; set; }
    }
}
