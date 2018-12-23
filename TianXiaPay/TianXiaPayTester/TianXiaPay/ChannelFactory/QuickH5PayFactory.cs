using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianXiaPay
{
    public class QuickH5PayFactory : ChannelFactory<QuickH5PayRequestModel, QuickH5PayOptions>
    {
        //正式环境请求URL：http://api.tfb8.com/cgi-bin/v2.0/api_mwappay_normal_apply.cgi
        //测试环境请求URL：http://apitest2.tfb8.com/cgi-bin/v2.0/api_mwappay_apply.cgi
        public override string Url => "http://apitest2.tfb8.com/cgi-bin/v2.0/api_mwappay_apply.cgi";
        public QuickH5PayFactory(string key) : base(key) { }

        public override QuickH5PayRequestModel GenerateRequestModel(QuickH5PayOptions options)
        {
            var result = new QuickH5PayRequestModel
            {
                spid = options.spid,
                sp_userid = options.sp_userid,
                spbillno = options.spbillno,
                money = options.money,
                cur_type = "1",
                user_type = "1",
                channel = "1",
                version = "V1.0.0",
                //return_url = "http://localhost:55256/QuickH5/Return.aspx",
                //notify_url = "http://localhost:55256/QuickH5/Notify.aspx",
                return_url = "http://47.92.68.54:8003/QuickH5/Return.aspx",
                notify_url = "http://47.92.68.54:8003/QuickH5/Notify.aspx",
                memo = "Tester",
                expire_time = "600",
                encode_type = "MD5",
                bank_accno = options.bank_accno,
                bank_acctype = options.bank_acctype,
            };
            result.sign = this.GenerateSign(result);
            return result;
        }
    }
}
