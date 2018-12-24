using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianXiaPay
{
    public class BankUnionQRCodeRequestModel : IRequestModel
    {

        public string sign_type { get; set; }
        public string ver { get; set; }
        public string input_charset { get; set; }
        public string sign_key_index { get; set; }
        public string spid { get; set; }
        public string notify_url { get; set; }
        public string pay_show_url { get; set; }
        public string sp_billno { get; set; }
        public string pay_type { get; set; }
        public string tran_time { get; set; }
        public string tran_amt { get; set; }
        public string cur_type { get; set; }
        public string auth_code { get; set; }
        public string termId { get; set; }
        public string item_name { get; set; }
        public string item_attach { get; set; }
        public string sign { get; set; }
    }
}
