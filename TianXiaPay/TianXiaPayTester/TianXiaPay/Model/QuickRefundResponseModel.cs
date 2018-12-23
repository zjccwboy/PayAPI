using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianXiaPay
{
    public class QuickRefundResponseModel : IResponseModel
    {
        public string retcode { get; set; }
        public string retmsg { get; set; }
        public string spid { get; set; }
        public string sp_serialno { get; set; }
        public string tfb_serialno { get; set; }
        public string serialno_state { get; set; }
        public string serialno_desc { get; set; }
        public string tfb_rsptime { get; set; }
        public string sign { get; set; }
    }
}
