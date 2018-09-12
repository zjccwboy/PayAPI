
namespace ThirdParty_KLPPay.Models
{

    public class SingleDFQueryModelRequest : BaseModel<SingleDFQueryContent, SingleDFQueryHead>
    {
        public override SingleDFQueryContent content { get; set; }
        public override SingleDFQueryHead head { get; set; }
    }

    public class SingleDFQueryContent
    {
        public string mchtOrderNo { get; set; }
        public string orderDate { get; set; }
        public string paymentBusinessType { get; set; }
    }

    public class SingleDFQueryHead : BaseHead
    {
        public string merchantId { get; set; }
        public string signType { get; set; }
        public string transactType { get; set; }
        public string version { get; set; }
    }

    public class SingleDFQueryModelResponse
    {
        public string responseCode { get; set; }
        public string responseMsg { get; set; }
        public string requestId { get; set; }
        public string mchtId { get; set; }
        public string signMsg { get; set; }
        public string signType { get; set; }
        public string orderDesc { get; set; }
        public string mchtOrderNo { get; set; }
        public string amount { get; set; }
        public string orderState { get; set; }
    }

}
