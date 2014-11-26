using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HiNSimulator2014.Models;
using HiNSimulator2014.Controllers.Admin;
using System.Web.Mvc;
using System.Data.Entity;
using Moq;

namespace HiNSimulator2014.Tests.Controllers
{
    /// <summary>
    /// Test kalsse for ArtificialPlayersController
    /// </summary>
    [TestClass]
    public class ArtificialPlayersControllerTest
    {
        // Fake repository
        private Mock<IRepository> _repository;

        // Controller
        private ArtificialPlayersController controller;

        // Test data
        private List<ArtificialPlayer> allPlayers;
        private ArtificialPlayer testPlayer;
        private Location testLocation;

        [TestInitialize]
        public void SetupContext()
        {
            _repository = new Mock<IRepository>();
            controller = new ArtificialPlayersController(_repository.Object);

            allPlayers = new List<ArtificialPlayer>();

            testLocation = new Location
            {
                LocationID = 1,
                LocationName = "Anonym korridor",
                LocationType = "Korridor",
                AcessTypeRole = "",
                ShortDescription = "En hemmelig korridor ved HiN",
                LongDescription = "En svært hemmelig korridor ved HiN",
                ImageID = null,
                Image = null
            };

            testPlayer = new ArtificialPlayer
            {
                ArtificialPlayerID = 1,
                Name = "Kunstige Kari",
                Type = "Student",
                Description = "En helt vanlig student",
                AccessLevel = "",
                IsStationary = false,
                CurrentLocation = testLocation,
                ImageID = null,
                ImageObject = null
            };

            allPlayers.Add(testPlayer);
        }

        /// <summary>
        /// Tester metoden Index()
        /// </summary>
        [TestMethod]
        public void ArtificialPlayersIndexTest()
        {
            // Arrange
            _repository.Setup(x => x.GetAllArtificialPlayersWithImagesAndLocations()).Returns(allPlayers);

            // Act
            var result = (ViewResult)controller.Index();
            var model = result.ViewData.Model as List<ArtificialPlayer>;

            // Assert
            _repository.Verify(x => x.GetAllArtificialPlayersWithImagesAndLocations(),
                "Metode ble ikke kalt: GetAllArtificialPlayersWithImagesAndLocations");

            Assert.IsNotNull(result, "Viewresult er null");
            Assert.AreSame(allPlayers, model, "Feil model sendt til view");
        }

        /// <summary>
        /// Tester metoden Details(int? id)
        /// </summary>
        [TestMethod]
        public void ArtificialPlayerDetailsTest()
        {
            // Arrange
            _repository.Setup(x => x.GetArtificialPlayer(testPlayer.ArtificialPlayerID))
                .Returns(testPlayer);

            // Act
            var result = (ViewResult)controller.Details(testPlayer.ArtificialPlayerID);
            var model = result.ViewData.Model as ArtificialPlayer;

            // Assert
            _repository.Verify(x => x.GetArtificialPlayer(testPlayer.ArtificialPlayerID),
                "Metode ble ikke kalt: GetArtificialPlayer");

            Assert.IsNotInstanceOfType(result, typeof(HttpStatusCodeResult), "Ikke forventet viewresult");
            Assert.IsNotInstanceOfType(result, typeof(HttpNotFoundResult), "Ikke forventet viewresult");
            Assert.IsNotNull(result, "Viewresult er null");
            Assert.AreSame(testPlayer, model, "Feil model sendt til view");
        }

        /// <summary>
        /// Tester metoden Create(ArtificialPlayer artificialPlayer)
        /// </summary>
        [TestMethod]
        public void CreateArtificialPlayerTest()
        {
            // Arrange 
            _repository.Setup(x => x.SaveArtificialPlayer(testPlayer));
            _repository.Setup(x => x.GetLocationSet());
            _repository.Setup(x => x.GetImageSet());

            // Act
            var result = (RedirectToRouteResult)controller.Create(testPlayer);

            // Assert
            _repository.Verify(x => x.SaveArtificialPlayer(testPlayer), 
                "Metode ble ikke kalt: SaveArtificialPlayer");
            _repository.Verify(x => x.GetLocationSet(), Times.Never(), 
                "Metode ble kalt: GetLocationSet");
            _repository.Verify(x => x.GetImageSet(), Times.Never(), 
                "Metode ble kalt: GetImageSet");

            Assert.IsNotNull(result, "Viewresult er null");
            Assert.AreEqual("Index", result.RouteValues["action"].ToString(), "Ikke redirigert til Index");
        }

        /// <summary>
        /// Tester metoden Edit(ArtificialPlayer artificialPlayer)
        /// </summary>
        [TestMethod]
        public void EditArtificialPlayerTest()
        {
            // Arrange 
            _repository.Setup(x => x.UpdateArtificialPlayer(testPlayer));
            _repository.Setup(x => x.GetLocationSet());
            _repository.Setup(x => x.GetImageSet());

            // Act
            var result = (RedirectToRouteResult)controller.Edit(testPlayer);

            // Assert
            _repository.Verify(x => x.UpdateArtificialPlayer(testPlayer), 
                "Metode ble ikke kalt: UpdateArtificialPlayer");
            _repository.Verify(x => x.GetLocationSet(), Times.Never(), 
                "Metode ble kalt: GetLocationSet");
            _repository.Verify(x => x.GetImageSet(), Times.Never(), 
                "Metode ble kalt: GetImageSet");

            Assert.IsNotNull(result, "Viewresult er null");
            Assert.AreEqual("Index", result.RouteValues["action"].ToString(), "Ikke redirigert til Index");
        }

        /// <summary>
        /// Tester metoden DeleteConfirmed(int id)
        /// </summary>
        [TestMethod]
        public void DeleteArtificialPlayerTest()
        {
            // Arrange
            _repository.Setup(x => x.GetArtificialPlayer(testPlayer.ArtificialPlayerID))
                .Returns(testPlayer);
            _repository.Setup(x => x.RemoveArtificialPlayer(testPlayer));

            // Act
            var result = (RedirectToRouteResult)controller.DeleteConfirmed(testPlayer.ArtificialPlayerID);

            // Assert
            _repository.Verify(x => x.GetArtificialPlayer(testPlayer.ArtificialPlayerID),
                "Metode ikke kalt: GetArtificialPlayer");
            _repository.Verify(x => x.RemoveArtificialPlayer(testPlayer),
                "Metode ikke kalt: RemoveArtificialPlayer");

            Assert.IsNotNull(result, "Viewresult er null");
            Assert.AreEqual("Index", result.RouteValues["action"].ToString(), "Ikke redirigert til Index");
        }

    }
}
