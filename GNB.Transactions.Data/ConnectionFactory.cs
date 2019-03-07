using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GNB.Transactions.Data
{
    public class ConnectionFactory
    {
        static private ConnectCache ratesConnection;
        static private ConnectCache transactionsConnection;

        private const string RATE_URI = "http://quiet-stone-2094.herokuapp.com/rates.json";
        private const string TRANSACTION_URI = "http://quiet-stone-2094.herokuapp.com/transactions.json";

        public static ConnectCache GetRatesConnecion()
        {
            if (ratesConnection == null)
            {
                ratesConnection = new ConnectCache(new Connect(RATE_URI));
                return ratesConnection;
            }
            else
            {
                return ratesConnection;
            }
        }

        public static ConnectCache GetTransactionsConnecion()
        {
            if (transactionsConnection == null)
            {
                transactionsConnection = new ConnectCache(new Connect(TRANSACTION_URI));
                return transactionsConnection;
            }
            else
            {
                return transactionsConnection;
            }
        }
    }
}

