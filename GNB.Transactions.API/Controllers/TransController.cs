using GNB.Transactions.Application;
using GNB.Transactions.Entities;
using System.Collections.Generic;
using System.Web.Http;

namespace GNB.Transactions.API.Controllers
{
    public class TransController : ApiController
    {

        private IGNBServices _igbnServices;

        public TransController(IGNBServices gbnServices)
        {
            _igbnServices = gbnServices;
        }

        // GET: api/Trans/
        //Obtain direct list of all transactions
        public List<Transaction> GetLista()
        {
            return _igbnServices.getAllTransactions();
        }

        // GET: api/Trans/sku
        //Obtain transactions by a id and, convert to EUR and calculate total amount. 
        public string Get(string id)
        {
            return _igbnServices.getTransactionsConvertTo(id);
        }
    }
}
