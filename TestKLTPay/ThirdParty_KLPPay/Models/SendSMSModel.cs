
namespace ThirdParty_KLPPay.Models
{

    public class SendSMSModelRequest : BaseModel<SendSMSContent, SendSMSHead>
    {
        public override SendSMSContent content { get; set; }
        public override SendSMSHead head { get; set; }
    }

    public class SendSMSContent
    {
        public string payerAcctNo { get; set; }
        public string payerIdNo { get; set; }
        public string payerIdType { get; set; }
        public string payerName { get; set; }
        public string orderNo { get; set; }
        public string orderAmount { get; set; }
        public string payerTelephone { get; set; }
    }

    public class SendSMSHead : BaseHead
    {
        public string merchantId { get; set; }
        public string signType { get; set; }
    }

    public class SendSMSModelResponse
    {
        public string responseCode { get; set; }
        public string responseMsg { get; set; }
        public string requestId { get; set; }
        public string mchtId { get; set; }
        public string signMsg { get; set; }
        public string signType { get; set; }
        public string orderNo { get; set; }
    }
}
