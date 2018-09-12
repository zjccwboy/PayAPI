using System.Collections.Generic;

namespace ThirdParty_KLPPay.Models
{
    /// <summary>
    /// 批量代付接口请求数据结构
    /// </summary>
    public class BatchDFPayModelRequest : BaseModel<List<BatchDFPayContent>, BatchDFPayHead>
    {
        public override List<BatchDFPayContent> content { get; set; }
        public override BatchDFPayHead head { get; set; }
    }

    public class BatchDFPayHead : BaseHead
    {
        public string merchantId { get; set; }
        public string signType { get; set; }
        public string transactType { get; set; }
        public string version { get; set; }
    }

    public class BatchDFPayContent
    {
        public string accountName { get; set; }
        public string accountNo { get; set; }
        public string accountType { get; set; }
        public int amt { get; set; }
        public string bankName { get; set; }
        public string bankNo { get; set; }
        public string mchtBatchNo { get; set; }
        public string mchtOrderNo { get; set; }
        public string notifyUrl { get; set; }
        public string orderDateTime { get; set; }
        public string purpose { get; set; }
        public string remark { get; set; }
    }

    /// <summary>
    /// 批量代付接口返回数据结构
    /// </summary>
    public class BatchDFPayModelResponse
    {
        public string totalCnt { get; set; }
        public string requestId { get; set; }
        public string signMsg { get; set; }
        public string signType { get; set; }
        public string mchtId { get; set; }
        public string responseCode { get; set; }
        public string responseMsg { get; set; }

    }

}
