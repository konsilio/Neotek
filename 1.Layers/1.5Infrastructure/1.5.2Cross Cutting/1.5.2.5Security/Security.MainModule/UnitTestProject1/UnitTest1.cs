using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Security.MainModule;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        [TestMethod]
        public void ValidaUsaurio()
        {
            DirectorioINFOTEC dr = new DirectorioINFOTEC();
            
            Assert.AreEqual(true, dr.UserValild("antonio.lopez"));
        }

        [TestMethod]
        public void ObtieneUsuario()
        {
            DirectorioINFOTEC dr = new DirectorioINFOTEC();
            bool resl=dr.GetUser("clara.fragoso").Existe;
            Assert.AreEqual(true,resl);

        }

        [TestMethod]
        public void ValidaYConsultaUsuario()
        {
            DirectorioINFOTEC dr = new DirectorioINFOTEC();
            bool resl = dr.GetDataUser("kevin.salomon","").Existe;
            
            Assert.AreEqual(true, resl);
        }
    }
}
