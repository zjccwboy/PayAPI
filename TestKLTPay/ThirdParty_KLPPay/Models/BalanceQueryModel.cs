

namespace ThirdParty_KLPPay.Models
{
    public class BalanceQueryModelRequest : BaseModel<BalanceQueryContent, BalanceQueryHead>
    {
        public override BalanceQueryContent content { get; set; }
        public override BalanceQueryHead head { get; set; }
    }

    public class BalanceQueryContent
    {

    }

    public class BalanceQueryHead : BaseHead
    {
        public string merchantId { get; set; }
        public string sign { get; set; }
        public string signType { get; set; }
        public string version { get; set; }
    }

    public class BalanceQueryModelResponse
    {
        public string responseCode { get; set; }
        public string responseMsg { get; set; }
        public string requestId { get; set; }
        public string mchtId { get; set; }
        public string signMsg { get; set; }
        public string signType { get; set; }
        public string balance { get; set; }
        public string creditBalance { get; set; }
        public string availableBlance { get; set; }
    }

}
