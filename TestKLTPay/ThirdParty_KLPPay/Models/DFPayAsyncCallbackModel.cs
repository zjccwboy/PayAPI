

namespace ThirdParty_KLPPay.Models
{
    /// <summary>
    /// 9	代付结果异步回调返回接口参数
    /// </summary>
    public class DFPayAsyncCallbackModelResponse
    {
        public string merchantId { get; set; }
        public string merchantOrderId { get; set; }
        public string accountName { get; set; }
        public string accountNo { get; set; }
        public string accountType { get; set; }
        public string orderStatus { get; set; }
        public string amount { get; set; }
        public string bankName { get; set; }
        public string bankNo { get; set; }
        public string notifyUrl { get; set; }
        public string remark { get; set; }
        public string retryTimes { get; set; }
        public string errorCode { get; set; }
        public string errorMsg { get; set; }
        public string sign { get; set; }
    }
}
