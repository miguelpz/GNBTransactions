

using GNB.Transactions.Data;
using GNB.Transactions.Domain.FormatOperations;
using GNB.Transactions.Entities;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GNB.Transactions.Domain.Rates
{
    public class RateService : IRateService
    {
        private List<Rate> rateOrigin;
        private IDataRates _ratesContainer;
        private Logger logger = LogManager.GetCurrentClassLogger();

        //Get rates given
        public RateService(IDataRates ratesContainer)
        {
            _ratesContainer = ratesContainer;
            try
            {
                rateOrigin = _ratesContainer.getRates();
            }
            catch(Exception ex)
            {
                logger.Debug(ex, "Error conversion to model");
                throw ex;               
            }            
        }
     
        // Return all currencies with their rates.
        public List<Currency> getAllCurrencies()
        {
            // Un elemento currency por moneda: 5 monedas, 5 elementos
            List<Currency> resultado = new List<Currency>();

            foreach (var rate in rateOrigin)
            {
                Currency currency_from = resultado.Where(o => o.id == rate.from).FirstOrDefault();
                if (currency_from == null)
                {
                    currency_from = new Currency(rate.from);
                    resultado.Add(currency_from);
                }

                Currency currency_to = resultado.Where(o => o.id == rate.to).FirstOrDefault();
                if (currency_to == null)
                {
                    currency_to = new Currency(rate.to);
                    resultado.Add(currency_to);
                }

                if (!currency_from.conversiones.ContainsKey(currency_to))
                {

                    currency_from.conversiones.Add(currency_to, decimal.Parse(rate.rate.Replace('.', ',')));
                }

                if (!currency_to.conversiones.ContainsKey(currency_from))
                {
                    currency_to.conversiones.Add(currency_from, 1 / decimal.Parse(rate.rate.Replace('.', ',')));
                }
            }

            foreach (var currency in resultado)
            {
                currency.procesarRates(new List<Currency>());
            }

            return resultado;
        }

        // Return  all rates
        public List<Rate> getAllRates()
        {
            List<Rate> _rateListFinal = new List<Rate>();
            List<Currency> currencis = getAllCurrencies();

            foreach (var currency in currencis)
            {
                foreach (var currency2 in currency.conversiones)
                {
                    _rateListFinal.Add(new Rate
                    {
                        from = currency.id,
                        to = currency2.Key.id,
                        //Round to Currency Bank Format
                        rate = Formats.roundCurrency(currency2.Value),
                    });
                }
            }

            return _rateListFinal;
        }

        //Get change with names of currencies.
        public decimal convertCurrent(decimal value, string currencyFrom, string currencyTo)
        {
            try
            {               
                // Cuando moneda de origen y destino es la misma no esta en la lista de conversiones.
                if (currencyFrom==currencyTo)                
                    return value;
                else
                    return  getAllCurrencies().Where(c => c.id == currencyFrom).FirstOrDefault()
                    .conversiones.Where(conv => conv.Key.id == currencyTo).FirstOrDefault().Value * value;              
            }
            catch
            {
                return 0;
            }                      
        }
    }
}
