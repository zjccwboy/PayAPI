
namespace ThirdParty_KLPPay.Models
{
    /// <summary>
    /// 单笔实时代付接口请求数据结构
    /// </summary>
    public class SingleDFPayModelRequest : BaseModel<SingleDFContent, SingleDFHead>
    {
        public override SingleDFContent content { get; set; }
        public override SingleDFHead head { get; set; }
    }

    public class SingleDFContent
    {
        public string accountName { get; set; }
        public string accountNo { get; set; }
        public string accountType { get; set; }
        public long amt { get; set; }
        public string bankName { get; set; }
        public string bankNo { get; set; }
        public string mchtOrderNo { get; set; }
        public string notifyUrl { get; set; }
        public string orderDateTime { get; set; }
        public string purpose { get; set; }
        public string remark { get; set; }
    }

    public class SingleDFHead : BaseHead
    {
        public string merchantId { get; set; }
        public string signType { get; set; }
        public string transactType { get; set; }
        public string version { get; set; }
    }

    /// <summary>
    /// 单笔实时代付接口返回数据结构
    /// </summary>
    public class SingleDFPayModelResponse
    {
        public string mchtId { get; set; }
        public string signType { get; set; }
        public string signMsg { get; set; }
        public string requestId { get; set; }
        public string orderState { get; set; }
        public string responseCode { get; set; }
        public string responseMsg { get; set; }
    }

}
