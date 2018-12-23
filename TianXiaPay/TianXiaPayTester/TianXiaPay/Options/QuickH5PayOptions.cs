using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianXiaPay
{
    public class QuickH5PayOptions : IOptions
    {
        public string spid { get; set; }
        public string sp_userid { get; set; }
        public string spbillno { get; set; }
        public string money { get; set; }
        public string bank_accno { get; set; }
        public string bank_acctype { get; set; }
    }
}
