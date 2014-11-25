using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HiNSimulator2014.Controllers.Admin;
using System.Web.Mvc;

namespace HiNSimulator2014.Tests.Controllers
{
    [TestClass]
    public class AdminControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            AdminController adminController = new AdminController();

            // Act
            ViewResult result = adminController.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
