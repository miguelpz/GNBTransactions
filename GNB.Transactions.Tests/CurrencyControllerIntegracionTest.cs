using GNB.Transactions.API.Controllers;
using GNB.Transactions.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace GNB.Transactions.Tests
{
    [TestClass]
    public class CurrencyControllerIntegracionTest:ControllerBaseTest
    {       
        // Devuelve un List<Rate> y el número correcto de elementos.
        [TestMethod]
        public void ResutadosObtenidosTest()
        {
            // Arrange
            CurrencyController currencyController = new CurrencyController(gnbServices);

            // Act
            var result = currencyController.Get();

            // Assert
            Assert.IsInstanceOfType(result,typeof(List<Rate>));         
            Assert.AreEqual(12, result.Count);
        }
    }
}
