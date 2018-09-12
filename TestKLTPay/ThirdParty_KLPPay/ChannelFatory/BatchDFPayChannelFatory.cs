using System.Collections.Generic;
using ThirdParty_KLPPay.Models;

namespace ThirdParty_KLPPay.ChannelFatory
{
    /// <summary>
    /// 批量代付接口
    /// </summary>
    public class BatchDFPayChannelFatory : BaseChannelFatory<List<BatchDFPayContent>, BatchDFPayHead, BatchDFPayModelResponse>
    {
        //测试环境地址：https://ipay.chinasmartpay.cn/openapi/batchPayment/payment
        //生产环境地址：https://openapi.openepay.com/openapi/batchPayment/payment

        public override string Url { get; } = "https://ipay.chinasmartpay.cn/openapi/batchPayment/payment";
    }
}
