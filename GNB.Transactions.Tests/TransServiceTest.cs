using System;
using System.Collections.Generic;
using GNB.Transactions.Data;
using GNB.Transactions.Domain.Rates;
using GNB.Transactions.Domain.Transactions;
using GNB.Transactions.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace GNB.Transactions.Tests
{
    [TestClass]
    public class TransServiceTest
    {
        private Mock<IDataTrans> mockTransContainer;
        private Mock<IRateService> mockRateService;
        
        [TestInitialize]
        public void InitializeTest()
        {
            mockTransContainer = new Mock<IDataTrans>();
            mockTransContainer.Setup(m => m.getTransactions()).Returns(new List<Transaction>{
                new Transaction{sku="M9114", amount="28.7",currency="EUR"},
                new Transaction{sku="J3943", amount="14.7",currency="USD"},
                new Transaction{sku="M9114", amount="28.7",currency="CAD"},
                new Transaction{sku="J3943", amount="20.7",currency="AUD"},
            });

            mockRateService = new Mock<IRateService>();
            mockRateService.Setup(m => m.convertCurrent(28.7M, "EUR", "EUR")).Returns(28.7M);
            mockRateService.Setup(m => m.convertCurrent(28.7M, "CAD", "EUR")).Returns(19.51M);         
        }

        // Convertir con parámetros correctos.
        [TestMethod]
        public void getTransConvertParametrosCorrectosTest()
        {
            // Act
            TransService transService = new TransService(mockRateService.Object, mockTransContainer.Object);

            // Assert
            string result = transService.getTransConvert("M9114", "EUR");
            var jsonResult = JObject.Parse(result);
            var count = jsonResult.Property("list").Value.Count();            
            double amount = (double)jsonResult.Property("totalAmount").Value;

            Assert.IsTrue(count == 2);
            Assert.AreEqual(expected: 48.21, actual: amount, delta: 0.01);
        }

        // Convertir con un SKU que no existe.
        [TestMethod]
        public void getTransConvertSkuNoExisteTest()
        {
            // Act
            TransService transService = new TransService(mockRateService.Object, mockTransContainer.Object);
            string sku = "XXXXX";
            string result = transService.getTransConvert(sku, "EUR");
     
            // Assert
            Assert.AreEqual(String.Format(transService.ErrorNoDatos,sku), result);
        }

        // Convertir a una moneda de destino inexistente.
        [TestMethod]
        public void getTransConvertMonedaNoExistenteTest()
        {
            // Act
            TransService transService = new TransService(mockRateService.Object, mockTransContainer.Object);           
            string result = transService.getTransConvert("M9114", "XXX");

            // Assert
            Assert.AreEqual(transService.ErrorConversion, result);
        }
    }
}