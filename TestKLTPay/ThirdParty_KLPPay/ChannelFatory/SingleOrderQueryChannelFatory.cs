using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThirdParty_KLPPay.Models;

namespace ThirdParty_KLPPay.ChannelFatory
{
    /// <summary>
    ///  3.2	单笔订单查询接口
    /// </summary>
    public class SingleOrderQueryChannelFatory : BaseChannelFatory<SingleOrderQueryContent, SingleOrderQueryHead, SingleOrderQueryResponse>
    {
        //测试环境地址：https://ipay.chinasmartpay.cn/openapi/merchantPayment/orderQuery
        //生产环境地址：https://openapi.openepay.com/openapi/merchantPayment/orderQuery

        public override string Url => "https://ipay.chinasmartpay.cn/openapi/merchantPayment/orderQuery";
    }
}
