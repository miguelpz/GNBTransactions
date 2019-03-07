using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GNB.Transactions.Domain.Transactions
{
    public interface ITransService
    {
        string getTransConvert(string sku, string currencyTo);
        string ErrorConversion { get; }
        string ErrorNoDatos { get; }
    }
}
