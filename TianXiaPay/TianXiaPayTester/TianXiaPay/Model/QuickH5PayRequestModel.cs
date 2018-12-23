using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianXiaPay
{
    public class QuickH5PayRequestModel : IRequestModel
    {
        public string spid { get; set; }
        public string sp_userid { get; set; }
        public string spbillno { get; set; }
        public string money { get; set; }
        public string cur_type { get; set; }
        public string user_type { get; set; }
        public string channel { get; set; }
        public string return_url { get; set; }
        public string notify_url { get; set; }
        public string errpage_url { get; set; }
        public string memo { get; set; }
        public string expire_time { get; set; }
        public string attach { get; set; }
        public string bank_accno { get; set; }
        public string bank_acctype { get; set; }
        public string sign { get; set; }
        public string encode_type { get; set; }
        public string risk_ctrl { get; set; }
        public string version { get; set; }
    }
}
