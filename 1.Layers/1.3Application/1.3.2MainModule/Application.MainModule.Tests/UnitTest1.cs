using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Application.MainModule.Servicios.Almacen;

namespace Application.MainModule.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var aplicaDescarga = AlmacenGasServicio.AplicarDescargas();

            Assert.IsTrue(aplicaDescarga.Count > 0);
        }
    }
}
