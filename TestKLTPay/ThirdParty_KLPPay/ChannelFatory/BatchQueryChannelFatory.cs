using ThirdParty_KLPPay.Models;

namespace ThirdParty_KLPPay.ChannelFatory
{
    /// <summary>
    ///批量查询接口
    /// </summary>
    public class BatchQueryChannelFatory : BaseChannelFatory<BatchQueryContent, BatchQueryHead, BatchQueryModelResponse>
    {
        //测试环境地址：https://ipay.chinasmartpay.cn/openapi/batchPayment/query
        //生产环境地址：https://openapi.openepay.com/openapi/batchPayment/query

        public override string Url { get; } = "https://ipay.chinasmartpay.cn/openapi/batchPayment/query";
    }
}
