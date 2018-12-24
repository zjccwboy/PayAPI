using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianXiaPay
{
    public class BankUnionORCodeNotifyModel : IAsyncNotifyModel
    {
        public string sign_type { get; set; }
        public string input_charset { get; set; }
        public string sign { get; set; }
        public string retcode { get; set; }
        public string retmsg { get; set; }
        public string notify_type { get; set; }
        public string listid { get; set; }
        public string sp_billno { get; set; }
        public string refund_listid { get; set; }
        public string pay_type { get; set; }
        public string tran_time { get; set; }
        public string tran_amt { get; set; }
        public string tran_state { get; set; }
        public string sysd_time { get; set; }
        public string refund_state { get; set; }
        public string item_name { get; set; }
        public string item_attach { get; set; }
        public string card_status { get; set; }
        public string enc_card { get; set; }
    }
}
