
namespace ThirdParty_KLPPay.Models
{
    public class BatchQueryModelRequest : BaseModel<BatchQueryContent, BatchQueryHead>
    {
        public override BatchQueryHead head { get; set; }
        public override BatchQueryContent content { get; set; }
    }

    public class BatchQueryHead : BaseHead
    {
        public string merchantId { get; set; }
        public string sign { get; set; }
        public string signType { get; set; }
        public string version { get; set; }
    }

    public class BatchQueryContent
    {
        public string mchtBatchNo { get; set; }
    }

    public class BatchQueryModelResponse
    {
        public string responseCode { get; set; }
        public string responseMsg { get; set; }
        public object requestId { get; set; }
        public string mchtId { get; set; }
        public string signMsg { get; set; }
        public string signType { get; set; }
        public string mchtBatchNo { get; set; }
        public string orderDateTime { get; set; }
        public string totalStatus { get; set; }
        public int totalCount { get; set; }
        public int totalSuccCount { get; set; }
        public int totalFailCount { get; set; }
        public int totalSuccAmount { get; set; }
        public int totalFailAmount { get; set; }
        public Agentpaydatailinfo[] agentPayDatailInfos { get; set; }
    }

    public class Agentpaydatailinfo
    {
        public string mchtOrderNo { get; set; }
        public string status { get; set; }
        public string accountName { get; set; }
        public string accountNo { get; set; }
        public int accountType { get; set; }
        public string bankNo { get; set; }
        public string bankName { get; set; }
        public int amt { get; set; }
        public object purpose { get; set; }
        public object notifyUrl { get; set; }
        public object remark { get; set; }
        public string errorCode { get; set; }
        public string errorMsg { get; set; }
    }

}
