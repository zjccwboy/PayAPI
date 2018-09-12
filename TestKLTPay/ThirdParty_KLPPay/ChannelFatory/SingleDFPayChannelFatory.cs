using ThirdParty_KLPPay.Models;

namespace ThirdParty_KLPPay.ChannelFatory
{
    /// <summary>
    /// 单笔实时代付接口
    /// </summary>
    public class SingleDFPayChannelFatory : BaseChannelFatory<SingleDFContent, SingleDFHead, SingleDFPayModelResponse>
    {
        //测试环境地址：https://ipay.chinasmartpay.cn/openapi/singlePayment/payment
        //生产环境地址：https://openapi.openepay.com/openapi/singlePayment/payment

        public override string Url { get; } = "https://ipay.chinasmartpay.cn/openapi/singlePayment/payment";
    }
}
