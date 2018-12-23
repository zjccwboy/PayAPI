using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TianXiaPay
{
    //public class QuickRefundFactory : BaseQuickRefundFactory
    //{
    //    public QuickRefundFactory(string key) : base(key) { }

    //    public QuickRefundResponseModel GetRefundResult(QuickRefundRequestModel request)
    //    {

    //    }
    //}

    public class QuickRefundFactory : ChannelFactory<QuickRefundRequestModel, QuickRefundOptions>
    {
        //正式环境请求URL：http://api.tfb8.com/cgi-bin/v2.0/api_pay_single.cgi
        //测试环境请求URL：http://apitest.tfb8.com/cgi-bin/v2.0/api_pay_single.cgi

        public override string Url => "http://apitest.tfb8.com/cgi-bin/v2.0/api_pay_single.cgi";

        public QuickRefundFactory(string key) : base(key) { }

        public override QuickRefundRequestModel GenerateRequestModel(QuickRefundOptions options)
        {
            var result = new QuickRefundRequestModel
            {
                version = "1.0",
                spid = options.spid,
                sp_serialno = options.sp_serialno,
                sp_reqtime = DateTime.UtcNow.AddHours(8).ToString("yyyyMMddHHmmss"),
                tran_amt = options.tran_amt,
                cur_type = "1",
                pay_type = "1",
                acct_name = options.acct_name,
                acct_id = options.acct_id,
                acct_type = "0",
                business_type = options.business_type,
                memo = "6",
            };
            result.sign = this.GenerateSign(result);
            return result;
        }

        public string GetRefundResult(QuickRefundRequestModel request)
        {
            var data = this.GenerateRequestFormString(request);
            var webClient = new WebClient();
            var result = webClient.UploadString(this.Url, data);
            result = Encoding.UTF8.GetString(Encoding.GetEncoding("GB2312").GetBytes(result));
            return result;
        }
    }
}
