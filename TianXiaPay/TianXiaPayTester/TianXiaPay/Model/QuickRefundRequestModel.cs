using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianXiaPay
{
    public class QuickRefundRequestModel : IRequestModel
    {
        public string version { get;set; }
        public string spid { get; set; }
        public string sp_serialno { get; set; }
        public string sp_reqtime { get; set; }
        public string tran_amt { get; set; }
        public string cur_type { get; set; }
        public string pay_type { get; set; }
        public string acct_name { get; set; }
        public string acct_id { get; set; }
        public string acct_type { get; set; }
        public string mobile { get; set; }
        public string cre_id { get; set; }
        public string bank_name { get; set; }
        public string bank_settle_no { get; set; }
        public string bank_branch_name { get; set; }
        public string business_type { get; set; }
        public string business_no { get; set; }
        public string memo { get; set; }
        public string sign { get; set; }
    }
}
