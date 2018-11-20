using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThirdParty_KLPPay.Models
{
    /// <summary>
    /// 3.2	单笔订单查询接口
    /// </summary>
    public class SingleOrderQueryModel : BaseModel<SingleOrderQueryContent, SingleOrderQueryHead>
    {
        public override SingleOrderQueryContent content { get; set; }
        public override SingleOrderQueryHead head { get; set; }
    }

    public class SingleOrderQueryContent
    {
        public string orderNo { get; set; }
    }

    public class SingleOrderQueryHead : BaseHead
    {
        public string merchantId { get; set; }
        public string signType { get; set; }
        public string transactType { get; set; }
        public string version { get; set; }
    }

    public class SingleOrderQueryResponse
    {
        public string responseCode { get; set; }
        public string responseMsg { get; set; }
        public string requestId { get; set; }
        public string mchtId { get; set; }
        public string signMsg { get; set; }
        public string signType { get; set; }
        public string payType { get; set; }
        public string mchtOrderId { get; set; }
        public string orderNo { get; set; }
        public string orderDatetime { get; set; }
        public string orderAmount { get; set; }
        public string ext1 { get; set; }
        public string ext2 { get; set; }
        public string payResult { get; set; }
        public string errorCode { get; set; }
        public string errorMsg { get; set; }
    }
}
