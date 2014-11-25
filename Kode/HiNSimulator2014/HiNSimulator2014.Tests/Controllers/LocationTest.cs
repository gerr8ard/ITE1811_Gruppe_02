using System;
using Microsoft.AspNet.Identity;
using HiNSimulator2014.Models;
using System.Diagnostics;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using HiNSimulator2014.Controllers.WebApi;
using HiNSimulator2014.Classes;
using System.Collections.Generic;

namespace HiNSimulator2014.Tests.Controllers
{
    [TestClass]
    public class LocationTest
    {

        [TestMethod]
        public void TestLockedConnection()
        {
            // Mocker repository
            Mock<IRepository> _repo = new Mock<IRepository>();

            // Testobjekter
            Location l1 = new Location { 
                LocationID = 1, 
                LocationName = "Lab1" 
            };
            Location l2 = new Location { 
                LocationID = 2, 
                LocationName = "Lab2" 
            };
            Location l3 = new Location { 
                LocationID = 3, 
                LocationName = "Lab3" 
            };

            // To LocationConnections, én åpen og én med keyLevel.
            LocationConnection lcLocked = new LocationConnection { 
                LocationOne = l1, 
                LocationTwo = l2,
                RequiredKeyLevel = 3
            };

            LocationConnection lcOpen = new LocationConnection
            {
                LocationOne = l1,
                LocationTwo = l3,
                RequiredKeyLevel = 0
            };

            // En mock bruker
            ApplicationUser mockUser = new ApplicationUser
            {
                PlayerName = "Arne",
                CurrentLocation = l1
            };

            // Mocker inventory
            List<Thing> currentInventory = new List<Thing>();
            currentInventory.Add(new Thing { KeyLevel = 2 });

            // Setter opp returverdier
            _repo.Setup(r => r.GetLocation(1)).Returns(l1);
            _repo.Setup(r => r.GetLocationConnection(1, 2)).Returns(lcLocked);
            _repo.Setup(r => r.GetLocationConnection(1, 3)).Returns(lcOpen);
            _repo.Setup(r => r.GetThingsForOwner(mockUser)).Returns(currentInventory);

            LocationController lCtrl = new LocationController(_repo.Object, mockUser);

            // Tester aksess
            int lockedResult = lCtrl.CheckAccess(2);
            int openResult = lCtrl.CheckAccess(3);

            // Assert
            // Hvis døren er låst skal det ikke returneres 0
            Assert.AreNotEqual(lockedResult, 0);
            // Hvis døren er åpen skal det returneres 0
            Assert.AreEqual(openResult, 0);
        }

        [TestMethod]
        public void TestCurrentLocation()
        {
            // Mocker repository
            Mock<IRepository> _repo = new Mock<IRepository>();

            // Testobjekter
            Location l1 = new Location
            {
                LocationID = 1,
                LocationName = "Lab1"
            };

            // En mock bruker
            ApplicationUser mockUser = new ApplicationUser
            {
                PlayerName = "Bjarne",
                CurrentLocation = l1
            };

            // Setter opp returverdier
            _repo.Setup(r => r.GetLocation(1)).Returns(l1);

            LocationController lCtrl = new LocationController(_repo.Object, mockUser);

            // Tester movement
            Location l = lCtrl.GetCurrentLocation();

            //Assert
            // Tester om location som ligger i "databasen" er 
            //den samme som returneres av kontrolleren
            Assert.AreEqual(l1, l);


        }

    }

}
