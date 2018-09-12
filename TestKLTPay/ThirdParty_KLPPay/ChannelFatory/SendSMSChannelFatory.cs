using ThirdParty_KLPPay.Models;

namespace ThirdParty_KLPPay.ChannelFatory
{
    /// <summary>
    /// 发送短信
    /// </summary>
    public class SendSMSChannelFatory : BaseChannelFatory<SendSMSContent, SendSMSHead, SendSMSModelResponse>
    {
        //测试环境地址：https://ipay.chinasmartpay.cn/openapi/quickPayment/sendSms
        //生产环境地址：https://openapi.openepay.com/openapi/quickPayment/sendSms

        public override string Url { get; } = "https://ipay.chinasmartpay.cn/openapi/quickPayment/sendSms";
    }
}
