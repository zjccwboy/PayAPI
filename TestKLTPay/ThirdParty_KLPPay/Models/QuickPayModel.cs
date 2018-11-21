
namespace ThirdParty_KLPPay.Models
{
    public class QuickPayModelRequest : BaseModel<QuickPayContent, QuickPayHead>
    {
        public override QuickPayHead head { get; set; }
        public override QuickPayContent content { get; set; }
    }

    public class QuickPayHead : BaseHead
    {
        public string version { get; set; }
        public string merchantId { get; set; }
        public string signType { get; set; }
    }

    public class QuickPayContent
    {
        public string orderNo { get; set; }
        public string productId { get; set; }
        public int orderCurrency { get; set; }
        public int? productNum { get; set; }
        public string smsCode { get; set; }
        public int? orderExpireDatetime { get; set; }
        public string productName { get; set; }
        public string productDesc { get; set; }
        public string orderDatetime { get; set; }
        public int orderAmount { get; set; }
        public string acctValiddate { get; set; }
        public string originalRequestId { get; set; }
        public int? productPrice { get; set; }
        public string pickupUrl { get; set; }
        public string cvv2 { get; set; }
        public string receiveUrl { get; set; }
        public string ext1 { get; set; }
        public string ext2 { get; set; }
    }

    public class QuickPayModelResponse
    {
        public string responseCode { get; set; }
        public string responseMsg { get; set; }
        public object requestId { get; set; }
        public string mchtId { get; set; }
        public string signMsg { get; set; }
        public string signType { get; set; }
        public int orderAmount { get; set; }
        public int orderCurrency { get; set; }
        public string orderDatetime { get; set; }
        public string orderNo { get; set; }
        public string orderState { get; set; }
    }

}
