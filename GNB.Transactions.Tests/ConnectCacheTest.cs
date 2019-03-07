using GNB.Transactions.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace GNB.Transactions.Tests
{
    [TestClass]
    public class ConnectCacheTest
    {
        private Mock<IConnect> mock;
        private Mock<IConnect> mockSinDatos;
        private const string TEST_STRING = "Esta es una cadena que simula datos serializados";

        [TestInitialize]
        public void InitializeTest()
        {
            mockSinDatos = new Mock<IConnect>();
            mockSinDatos.Setup(m => m.ConnectTo()).Throws(new Exception());


            mock = new Mock<IConnect>();
            mock.Setup(m => m.ConnectTo()).Returns(
                TEST_STRING
            );            
        }

        // Cuando no hay problemas de conexión con el servidor.
        [TestMethod]
        public void ConConexion()
        {
            // Act
            ConnectCache connectionTest = new ConnectCache(mock.Object);

            // Assert
            Assert.AreEqual(connectionTest.GetData(), TEST_STRING);
        }

        // Servidor no responde desde un primer momento.
        [TestMethod]
        [ExpectedException(typeof(Exception),
        "Verificar que produce excepcion cuando de entrada no responde el servidor")]
        public void SinConexion()
        {           
            // Act
            ConnectCache connectionTest = new ConnectCache(mockSinDatos.Object);
            connectionTest.CleanCache();
            connectionTest.GetData();
        }

        // Conexión en primer intento, sin conexion en el segundo en el cual debe ofrecer datos.
        [TestMethod]  
        public void ConConexionSinConexion()
        {
            // Act
            // En esta conexion con exito la cache se carga.
            ConnectCache connectionTest = new ConnectCache(mock.Object);
            connectionTest.CleanCache();            
            connectionTest.GetData();

            // Assert
            // En esta conexion fallida hay que recuperar los datos de la cache.
            connectionTest = new ConnectCache(mockSinDatos.Object);
            Assert.AreEqual(connectionTest.GetData(), TEST_STRING);
        }

    }
}
