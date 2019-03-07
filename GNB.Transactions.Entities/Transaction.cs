using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GNB.Transactions.Entities
{
    public class Transaction
    {
        public string sku { get; set; }
        public string amount { get; set; }
        public string currency { get; set; }
    }
}
