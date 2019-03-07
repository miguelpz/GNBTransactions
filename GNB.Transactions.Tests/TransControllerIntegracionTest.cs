using GNB.Transactions.API.Controllers;
using GNB.Transactions.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace GNB.Transactions.Tests
{
    [TestClass]
    public class TransControllerIntegracionTest:ControllerBaseTest
    {
        

        // Obtiene un listado con la totalidad de transacciones disponibles en el servicio.
        [TestMethod]
        public void GetListaTest()
        {
            // Arrange
            TransController currencyController = new TransController(gnbServices);

            // Act
            var result = currencyController.GetLista();

            // Assert
            Assert.IsInstanceOfType(result,typeof(List<Transaction>));         
            Assert.AreEqual(4, result.Count);
        }

        // Obtiene un string con Json serializado de transacciones filtrado por SKU convertido a EUR y con suma total.
        [TestMethod]
        public void GetTransactiosToEurSumTest()
        {
            // Arrange
            TransController currencyController = new TransController(gnbServices);

            // Act
            var result = currencyController.Get("M9114");           
            var jsonResult = JObject.Parse(result);
            var count = jsonResult.Property("list").Value.Count();
            double amount = (double)jsonResult.Property("totalAmount").Value;

            // Assert
            Assert.IsTrue(count == 2);
            Assert.AreEqual(expected: 48.21, actual: amount, delta: 0.01);
        }

    }
}
