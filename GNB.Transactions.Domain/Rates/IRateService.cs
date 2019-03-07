using GNB.Transactions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GNB.Transactions.Domain.Rates
{
    public interface IRateService
    {
        List<Rate> getAllRates();
        decimal convertCurrent(decimal value, string currencyFrom, string currencyTo);      
    }
}
