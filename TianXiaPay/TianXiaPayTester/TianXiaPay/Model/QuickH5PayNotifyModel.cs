using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianXiaPay
{
    public class QuickH5PayNotifyModel : IAsyncNotifyModel
    {
        public string retcode { get; set; }
        public string retmsg { get; set; }
        public string spid { get; set; }
        public string spbillno { get; set; }
        public string listid { get; set; }
        public string money { get; set; }
        public string cur_type { get; set; }
        public string result { get; set; }
        public string pay_type { get; set; }
        public string user_type { get; set; }
        public string attach { get; set; }
        public string sign { get; set;}
        public string encode_type { get; set; }

    }
}
