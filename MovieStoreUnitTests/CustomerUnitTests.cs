using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieStore;

namespace MovieStoreUnitTests
{
    [TestClass]
    public class CustomerUnitTests
    {
        [TestMethod]
        public void ListCustomers_HasRows_ReturnsObject()
        {
            Customer c = new Customer();
            DataTable dt =c.listCustomers();
            Assert.IsNotNull(dt);
        }
    }
}
