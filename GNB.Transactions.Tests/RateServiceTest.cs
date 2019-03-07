using GNB.Transactions.Data;
using GNB.Transactions.Domain.Rates;
using GNB.Transactions.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace GNB.Transactions.Tests
{
    [TestClass]
    public class RateServiceTest
    {
        private Mock<IDataRates> mock;
        private RateService rateService;

        [TestInitialize]
        public void InitializeTest()
        {
            mock = new Mock<IDataRates>();
            mock.Setup(m => m.getRates()).Returns(new List<Rate>{
                new Rate{from="USD", to="EUR", rate="1.0"},
                new Rate{from="EUR", to="USD", rate="1.0"},
                new Rate{from="USD", to="CAD", rate="1.47"},
                new Rate{from="CAD", to="USD", rate="0.68"},
                new Rate{from="EUR", to="AUD", rate="0.97"},
                new Rate{from="AUD", to="EUR", rate="1.03"}
            });

            rateService = new RateService(mock.Object);
        }

        // Comprobación con datos moqueados.
        [TestMethod]
        public void CalculoRatesTest()
        {                    
            // Arrange
            string[] resultRatesTest = { "1,0", "1,47", "0,97", "1", "0,97", "1,47", "0,68", "0,68", "0,66", "1,03", "1,03", "1,52" };

            // Act           
            var result = rateService.getAllRates();

            // Assert
            Assert.IsTrue(result.Count == 12);

            bool correctRates = true;
            for (int x = 0; x < 12; x++)
            {
                if (!result[x].rate.Equals(resultRatesTest[x]))
                    correctRates = false;

            }
            Assert.IsTrue(correctRates);
        }

        // Comprobar función de conversion con parametros correctos: cantidad a converit, moneda acual, moneda a convertir.
        [TestMethod]
        public void ConversionRateConParametrosCorrectosTest()
        {                                
            // Assert
            double result = (double)rateService.convertCurrent(1M, "AUD", "USD");
            Assert.AreEqual(expected :1.03, actual: result, delta:0.001);
        }

        // Comprobar que da error tipifiado como 0 cuando los parametros de entrada no existen y no se puede efectuar la conversion.
        [TestMethod]
        public void ConversionRateConParametrosIncorrectosTest()
        {                   
            // Assert
            double result = (double)rateService.convertCurrent(1M, "AUDDD", "USD");
            Assert.AreEqual(0,result);
        }
    }
}