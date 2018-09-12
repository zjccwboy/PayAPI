using ThirdParty_KLPPay.Models;

namespace ThirdParty_KLPPay.ChannelFatory
{
    /// <summary>
    /// 代付余额查询接口
    /// </summary>
    public class BalanceQueryChannelFatory : BaseChannelFatory<BalanceQueryContent, BalanceQueryHead, BalanceQueryModelResponse>
    {
        //测试环境地址：https://ipay.chinasmartpay.cn/openapi/singlePayment/queryAccountInfo
        //生产环境地址：https://openapi.openepay.com/openapi/singlePayment/queryAccountInfo

        public override string Url { get; } = "https://ipay.chinasmartpay.cn/openapi/singlePayment/queryAccountInfo";
    }
}
