using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RRental.Web.Api.Controllers;
using RRental.Web.Api.Models;

namespace RRental.Web.Api.Tests
{
    [TestClass]
    public class RRentalWebApiTest
    {
        //TODO unit tests
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            var controller = new RentController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            var rows = new List<string> {"Tractor;HeavyEquipment;5"};
            //Act

            var response = controller.AddToCart(rows);

            //Assert
        }
    }
}
