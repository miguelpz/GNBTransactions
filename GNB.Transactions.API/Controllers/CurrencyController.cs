using GNB.Transactions.Application;
using GNB.Transactions.Entities;
using System.Collections.Generic;
using System.Web.Http;

namespace GNB.Transactions.API.Controllers
{
    public class CurrencyController : ApiController
    {
        private IGNBServices _igbnServices;

        public CurrencyController(IGNBServices igbnServices)
        {
            _igbnServices = igbnServices;
        }

        // GET: api/Currency/
        //Obtain all rates of currencys.
        public List<Rate> Get()
        {
            return _igbnServices.getAllRates();
        }
    }
}