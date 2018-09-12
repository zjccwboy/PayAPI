using ThirdParty_KLPPay.Models;

namespace ThirdParty_KLPPay.ChannelFatory
{
    /// <summary>
    /// 单笔实时代付查询交易接口
    /// </summary>
    public class SingleDFQueryChannelFatory : BaseChannelFatory<SingleDFQueryContent, SingleDFQueryHead, SingleDFQueryModelResponse>
    {
        //测试环境地址：https://ipay.chinasmartpay.cn/openapi/singlePayment/query
        //生产环境地址：https://openapi.openepay.com/openapi/singlePayment/query

        public override string Url { get; } = "https://ipay.chinasmartpay.cn/openapi/singlePayment/query";
    }
}
