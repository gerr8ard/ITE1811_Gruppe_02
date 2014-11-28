using System;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using HiNSimulator2014.Controllers.WebApi;
using HiNSimulator2014.Models;
using HiNSimulator2014.Classes;

namespace HiNSimulator2014.Tests.Controllers
{
    /// <summary>
    /// ThingsControllerTest tester WebAPI ThingsController
    /// 
    /// Skrevet av: Alexander Lindquiser
    /// </summary>
    [TestClass]
    public class ThingsControllerTest
    {
        // Tester av spillere bare kan skrive på ting som skal være mulig å skrive på.
        [TestMethod]
        public void TestWriteOnThing()
        {
            // Arrange
            Mock<IRepository> _repo = new Mock<IRepository>();
            ThingsController ctrl = new ThingsController(_repo.Object);

            Thing t1 = new Thing { Name = "ting1", PlayerWritable = false };
            Thing t2 = new Thing { Name = "ting2", PlayerWritable = true };

            _repo.Setup(x => x.GetThingById(1)).Returns(t1);
            _repo.Setup(x => x.GetThingById(2)).Returns(t2);

            // Act
            bool res1 = ctrl.WriteOnThing(1, "tekstpaating");
            bool res2 = ctrl.WriteOnThing(2, "tekstpaating");

            // Assert
            Assert.IsFalse(res1);
            Assert.IsTrue(res2);
        }

        // Tester av ved request av GetThing returneres en SimpleThing og ikke Thing modellen.
        // Dette for at API kallet skal skje mye hurtigere.
        [TestMethod]
        public void TestGetThing()
        {
            // Arrange
            Mock<IRepository> _repo = new Mock<IRepository>();
            ThingsController ctrl = new ThingsController(_repo.Object);

            Thing thing = new Thing { Name = "Ting", Description = "Nada"};
            _repo.Setup(x => x.GetThingById(1)).Returns(thing);

            // Act
            var res = ctrl.GetThing(1);

            // Assert
            Assert.IsInstanceOfType(res, typeof(SimpleThing));
        }
    }
}
