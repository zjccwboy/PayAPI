using ThirdParty_KLPPay.Models;

namespace ThirdParty_KLPPay.ChannelFatory
{
    /// <summary>
    /// 单笔实时代付回执接口
    /// </summary>
    public class SingleElectronicCertificateChannelFatory : BaseChannelFatory<SingleElectronicCertificateContent, SingleElectronicCertificateHead, SingleElectronicCertificateModelResponse>
    {
        //测试环境地址：https://ipay.chinasmartpay.cn/openapi/singlePayment/electronicCertificate
        //生产环境地址：https://openapi.openepay.com/openapi/singlePayment/electronicCertificate

        public override string Url { get; } = "https://ipay.chinasmartpay.cn/openapi/singlePayment/electronicCertificate";
    }
}
