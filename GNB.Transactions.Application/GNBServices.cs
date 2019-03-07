using GNB.Transactions.Data;
using GNB.Transactions.Domain.Rates;
using GNB.Transactions.Domain.Transactions;
using GNB.Transactions.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GNB.Transactions.Application
{
    public class GNBServices : IGNBServices
    {


        private IRateService _rateService;
        private ITransService _transService;
        private IDataTrans _dataTrans;

        public GNBServices(IDataTrans transContainer, IRateService rateService, ITransService transService)
        {
            _rateService = rateService;
            _transService = transService;
            _dataTrans = transContainer;
        }

        // Obtain list of all transactions direc from data input
        public List<Transaction> getAllTransactions()
        {
            return _dataTrans.getTransactions();
        }

        //Calculate all rates and give them 
        public List<Rate> getAllRates()
        {
            return _rateService.getAllRates();
        }

        //Convert to EUR all transaction and calculate total amount.
        public string getTransactionsConvertTo(string sku)
        {
            return _transService.getTransConvert(sku, "EUR");
        }

    }

}