using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GNB.Transactions.Domain.FormatOperations
{
    public class Formats
    {
        //Format to Currency Banq
        public static string roundCurrency (decimal value)
        {
            return Math.Round(value, 2, MidpointRounding.ToEven).ToString();
        }
    }
}
