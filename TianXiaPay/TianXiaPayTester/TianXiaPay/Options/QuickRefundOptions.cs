using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianXiaPay
{
    public class QuickRefundOptions : IOptions
    {
        public string spid { get; set; }
        public string sp_serialno { get; set; }
        public string tran_amt { get; set; }
        public string business_type { get; set; }
        public string acct_name { get; set; }
        public string acct_id { get; set; }

    }
}
