using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianXiaPay
{
    public class BankUnionORCodeOptions : IOptions
    {
        public string spid { get; set; }
        public string sp_billno { get; set; }
        public string tran_amt { get; set; }
    }
}
