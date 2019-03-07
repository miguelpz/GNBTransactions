using GNB.Transactions.Application;
using GNB.Transactions.Data;
using GNB.Transactions.Domain.Rates;
using GNB.Transactions.Domain.Transactions;
using GNB.Transactions.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace GNB.Transactions.Tests
{
    public class ControllerBaseTest
    {

        protected Mock<IDataRates> mockRatesContainer;
        protected RateService rateService;
        protected Mock<IDataTrans> mockTransContainer;
        protected TransService transService;
        protected GNBServices gnbServices;

        [TestInitialize]
        public void InitializeTest()
        {
            mockRatesContainer = new Mock<IDataRates>();
            mockRatesContainer.Setup(m => m.getRates()).Returns(new List<Rate>{
                new Rate{from="USD", to="EUR", rate="1.0"},
                new Rate{from="EUR", to="USD", rate="1.0"},
                new Rate{from="USD", to="CAD", rate="1.47"},
                new Rate{from="CAD", to="USD", rate="0.68"},
                new Rate{from="EUR", to="AUD", rate="0.97"},
                new Rate{from="AUD", to="EUR", rate="1.03"}
            });

            rateService = new RateService(mockRatesContainer.Object);


            mockTransContainer = new Mock<IDataTrans>();
            mockTransContainer.Setup(m => m.getTransactions()).Returns(new List<Transaction>{
                new Transaction{sku="M9114", amount="28.7",currency="EUR"},
                new Transaction{sku="J3943", amount="14.7",currency="USD"},
                new Transaction{sku="M9114", amount="28.7",currency="CAD"},
                new Transaction{sku="J3943", amount="20.7",currency="AUD"},
            });

            transService = new TransService(rateService, mockTransContainer.Object);
            gnbServices = new GNBServices(mockTransContainer.Object, rateService, transService);
        }
    }
}
