using System;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using HiNSimulator2014.Controllers.WebApi;
using HiNSimulator2014.Models;

namespace HiNSimulator2014.Tests.Controllers
{
    [TestClass]
    public class ThingsControllerTest
    {
        [TestMethod]
        public void TestWriteOnThing()
        {
            // Arrange
            Mock<IRepository> _repo = new Mock<IRepository>();

            Thing t1 = new Thing { Name = "ting1", PlayerWritable = false };
            Thing t2 = new Thing { Name = "ting2", PlayerWritable = true };

            _repo.Setup(x => x.GetThingById(1)).Returns(t1);
            _repo.Setup(x => x.GetThingById(2)).Returns(t2);

            ThingsController ctrl = new ThingsController(_repo.Object);

            // Act
            bool res1 = ctrl.WriteOnThing(1, "tekstpaating");
            bool res2 = ctrl.WriteOnThing(2, "tekstpaating");

            // Assert
            Assert.IsFalse(res1);
            Assert.IsTrue(res2);
        }
    }
}
