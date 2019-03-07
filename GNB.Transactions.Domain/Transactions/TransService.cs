

using GNB.Transactions.Data;
using GNB.Transactions.Domain;
using GNB.Transactions.Domain.FormatOperations;
using GNB.Transactions.Domain.Rates;
using GNB.Transactions.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GNB.Transactions.Domain.Transactions
{
    public class TransService : ITransService
    {

        private Logger logger = LogManager.GetCurrentClassLogger();
        private IRateService _rateService;
        private IDataTrans _transContainer;

        private const string ERROR_CONVERSION = "Ha habido un error en la conversión";
        private const string ERROR_NO_DATOS = "No existen transacciones para sku: {0}";

        public string ErrorConversion { get=>ERROR_CONVERSION;}
        public string ErrorNoDatos { get => ERROR_NO_DATOS; }



        public TransService(IRateService rateService, IDataTrans transContainer)
        {           
              _transContainer = transContainer;         
              _rateService = rateService;
        }

        //Return transactions set from a "sku" convert to a currency and total.
        public string getTransConvert(string sku, string currencyTo)
        {        
            decimal totalAmount = 0;
            decimal partialAmount;

            List<Transaction> listTransFilter = new List<Transaction>();

            try
            {
                foreach (Transaction transaction in _transContainer.getTransactions())
                {
                    if (transaction.sku == sku)
                    {                       
                        partialAmount = _rateService.convertCurrent(decimal.Parse(transaction.amount.Replace('.', ',')), transaction.currency, currencyTo);

                        if (partialAmount == 0)
                        {
                            logger.Debug(ERROR_CONVERSION);
                            return ERROR_CONVERSION; // Si la funcion de conversion ha devuelto error 0.
                        }

                        transaction.amount = Formats.roundCurrency(partialAmount).Replace(',', '.');
                        transaction.currency = currencyTo;
                        listTransFilter.Add(transaction);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Debug(ex, "Calculate amount error");
                throw ex;
            }

            if (listTransFilter.Count > 0)
            {           
                totalAmount = listTransFilter.Select(o => decimal.Parse(o.amount.Replace('.', ','))).Sum();
                return JsonConvert.SerializeObject(new { list = listTransFilter, totalAmount = Formats.roundCurrency(totalAmount).Replace(',', '.') });
            }
            else
            {
                string resultado = String.Format(ERROR_NO_DATOS, sku);
                logger.Debug(resultado);
                return resultado;
            }     
        }
    }
}