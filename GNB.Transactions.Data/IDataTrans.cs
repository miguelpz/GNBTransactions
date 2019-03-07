using GNB.Transactions.Entities;
using System.Collections.Generic;

namespace GNB.Transactions.Data
{
    public interface IDataTrans
    {
        List<Transaction> getTransactions();
    }
}