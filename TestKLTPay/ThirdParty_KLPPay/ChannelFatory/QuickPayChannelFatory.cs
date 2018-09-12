using ThirdParty_KLPPay.Models;

namespace ThirdParty_KLPPay.ChannelFatory
{
    /// <summary>
    /// 确认支付
    /// </summary>
    public class QuickPayChannelFatory : BaseChannelFatory<QuickPayContent, QuickPayHead, QuickPayModelResponse>
    {
        //测试环境地址：https://ipay.chinasmartpay.cn/openapi/quickPayment/confirmPay
        //生产环境地址：https://openapi.openepay.com/openapi/quickPayment/confirmPay

        public override string Url { get; } = "https://ipay.chinasmartpay.cn/openapi/quickPayment/confirmPay";
    }
}
