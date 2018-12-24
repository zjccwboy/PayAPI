using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianXiaPay
{
    public class BankUnionORCodeResponseModel : IResponseModel
    {
        public string cur_type { get; set; }
        public string sign_type { get; set; }
        public string ver { get; set; }
        public string input_charset { get; set; }
        public string sign_key_index { get; set; }
        public string retcode { get; set; }
        public string retmsg { get; set; }
        public string spid { get; set; }
        public string listid { get; set; }
        public string sp_billno { get; set; }
        public string pay_type { get; set; }
        public string qrcode { get; set; }
        public string tran_amt { get; set; }
        public string sysd_time { get; set; }
        public string sign { get; set; }
    }
}
