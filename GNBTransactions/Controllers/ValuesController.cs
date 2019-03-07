using GNB.Transactions.Application;
using GNB.Transactions.Entities;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GNBTransactions.Controllers
{
    public class ValuesController : ApiController
    {
        // private IDatos _datos;
        //private ITest _test;
        // private ICurrencyProcess _currencyProcess;

        private IGNBServices _gnbServices;
        private string hola;

        public ValuesController(GNBServices gnbServices)
        {
            _gnbServices = gnbServices;
            hola = "hola";

        }

        //public ValuesController(ITransServices transServices)
        //{
        //    _datos = datos;
        //    _test = test;
        //    _currencyProcess = currencyProcess;
        // }

        //public ValuesController(IGNBServices igbnServices)
        //{
        //    _igbnServices = igbnServices;

            
            
        //}



        // GET api/values
        //public string GetAllRates()
        //{
        //    return _currencyProcess.getAllRates();
        //}

        public string getAllTransactions()
        {
            return hola;
;                
        }



        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}

    public class Rates
    {
        public string from { get; set; }
        public string to { get; set; }
        public string rate { get; set; }


    }