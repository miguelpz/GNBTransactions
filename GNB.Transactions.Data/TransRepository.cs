using GNB.Transactions.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GNB.Transactions.Data
{
    public class TransRepository:IDataTrans
    {
        public List<Transaction> getTransactions()
        {
            List<Transaction> transList;
            var json = ConnectionFactory.GetTransactionsConnecion().GetData();
            transList = JsonConvert.DeserializeObject<List<Transaction>>(json);
            return transList;
        }       
    }
}
