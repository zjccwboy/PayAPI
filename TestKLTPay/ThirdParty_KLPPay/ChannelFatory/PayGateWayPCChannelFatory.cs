using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThirdParty_KLPPay.Models;

namespace ThirdParty_KLPPay.ChannelFatory
{
    /// <summary>
    /// 3.1	页面订单提交接口
    /// </summary>
    public class PayGateWayPCChannelFatory : BaseChannelFatory<PayGateWayPContent, PayGateWayPCHead, PayGateWayPCModelResponse>
    {
        //测试环境地址：https://ipay.chinasmartpay.cn/openapi/merchantPayment/order
        //生产环境地址：https://openapi.openepay.com/openapi/merchantPayment/order

        public override string Url => "https://ipay.chinasmartpay.cn/openapi/merchantPayment/order";
    }
}
