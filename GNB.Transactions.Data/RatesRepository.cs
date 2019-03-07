using GNB.Transactions.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GNB.Transactions.Data
{
    public class RatesRepository:IDataRates
    {
        public List<Rate> getRates()
        {
            List<Rate> ratesList;

            var json = ConnectionFactory.GetRatesConnecion().GetData();
            ratesList = JsonConvert.DeserializeObject<List<Rate>>(json);
            return ratesList;
        }
    }
}
