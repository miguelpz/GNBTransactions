using GNB.Transactions.Data;
using GNB.Transactions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GNB.Transactions.Application
{
    public interface IGNBServices
    {
        List<Transaction> getAllTransactions();
        List<Rate> getAllRates();
        string getTransactionsConvertTo(string sku);
    }
}
        


