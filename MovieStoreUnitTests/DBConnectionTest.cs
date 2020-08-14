using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieStore;

namespace MovieStoreUnitTests
{
    [TestClass]
    public class DBConnection
    {
        [TestMethod]
        public void TestDBConnection_StateOpen_ReturnsTrue()
        {
            AuthUser l = new AuthUser();
            bool r = l.TestDBConnection;
            Assert.IsTrue(r);
        }
    }
}
