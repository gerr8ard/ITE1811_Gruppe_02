using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Host.SystemWeb;
using HiNSimulator2014.Models;
using HiNSimulator2014.Classes;
using System.Diagnostics;

namespace HiNSimulator2014.Controllers.WebApi
{
    /// <summary>
    /// LocationController - webAPI-kontroller som tar seg av 
    /// forespørseler som er relaterte til navigasjon
    /// og andre Location-baserte tjenester.
    /// 
    /// Skrevet av Andreas Dyrøy Jansson
    /// </summary>
    [Authorize]
    public class LocationController : ApiController
    {
        private IRepository repository;
        private ApplicationUserManager _userManager;
        private ApplicationUser mockUser = null;
        private bool test = false; // Om kontrolleren skal kjøres i testmodus

        // Får tak i UserManager på en fornuftig måte
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // Standard konstruktør
        public LocationController()
        {
            repository = new Repository();


        }

        /// <summary>
        /// Konstruktør for mocking/testing
        /// </summary>
        /// <param name="ir">Mock repository</param>
        /// <param name="au">Mock spiller/user</param>
        public LocationController(IRepository ir, ApplicationUser au)
        {
            repository = ir;
            mockUser = au;
            // Hvis denne konstruktøren brukes betyr det at kontrolleren skal kjøres i testmodus
            test = true; 
        }

        /// <summary>
        /// Oppdaterer spillers posisjon i databasen
        /// </summary>
        /// <param name="newLocationId">Posisjonen det skal endres til</param>
        /// <param name="score">Bruker kan få poeng for forskjellieg handlinger</param>
        private void UpdatePlayerLocation(int newLocationId, int score)
        {
            // Hvis test skal ikke databasen oppdateres
            if (!test)
            {
                Debug.Write("flytter til: " + newLocationId);
                ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
                user.CurrentLocation = repository.GetLocation(newLocationId);
                // Bruker får 1 poeng for hver dør han åpner, 5 hvis han
                // åpner en låst dør
                if (score == -1)
                    score = 5;

                score++;

                user.Score += score;
                UserManager.Update(user);
            }
        }

       
        /// <summary>
        /// Sjekker om pålogget bruker har en gjenstand i sitt inventory som kan låse
        /// opp døren, evt slipper han gjennom hvis døren allerede er åpen
        /// </summary>
        /// <param name="id">Posisjonen spiller ønsker å gå til</param>
        /// <returns>0 hvis døren er åpen, -1 hvis døren ble låst opp, ellers keyLevel som trengs
        /// for å låse opp</returns>
        public int CheckAccess(int id)
        {

            // Startbetingelse -1 gir 0, som betyr åpen dør.
            if (id == -1)
                return 0;

            // Henter bruker
            ApplicationUser user = GetUser();
            // Henter nåværende posisjon
            Location currentLocation = GetCurrentLocation();
            // Henter koblingen mellom rommene
            LocationConnection lc = repository.GetLocationConnection(currentLocation.LocationID, id);
            
            // Henter inventory
            List<Thing> currentInventory = repository.GetThingsForOwner(user);
            Debug.Write("\nCurrentLocation: " + currentLocation.LocationID + ", NextLocation: " + id);

            // Hvis det finnes en kobling
            if (lc != null)
            {
                // Hvis døren er default åpen
                if (lc.RequiredKeyLevel <= 0)
                    return 0;

                Debug.Write("\nLocationConnection from: " + lc.LocationOne_LocationID + " to: " + lc.LocationTwo_LocationID);
                Debug.Write("isLocked: " + lc.IsLocked);
                // Sjekker alle gjenstander i inventory
                foreach (Thing t in currentInventory)
                {
                    if (t.KeyLevel.HasValue)
                    {
                        if (t.KeyLevel >= lc.RequiredKeyLevel)
                        {
                            // Døren ble låst opp
                            return -1;
                        }
                    }
                }
                // Døren er låst
                return lc.RequiredKeyLevel;
            }
            // Døren er åpen
            return 0;
        }

        // GET api/Location/MoveTo/5
        /// <summary>
        /// Kalles fra klienten for å flytte spilleren til ny posisjon
        /// </summary>
        /// <param name="id">Den nye posisjonen</param>
        /// <returns></returns>
        [HttpGet]
        public SimpleLocation MoveTo(int id)
        {
            Location currentLocation;
            // Sjekker om spiller har tilgang til rommet
            int keyLevel = CheckAccess(id);

            // Hvis id != -1 kom kallet fra en knapp hos klienten
            // CheckAccess om døren er åpen/kan åpnes
            if (id != -1 && keyLevel <= 0)
            {
                // Hvis døren er åpen, flytt spiller
                UpdatePlayerLocation(id, keyLevel);
                currentLocation = repository.GetLocation(id);
            }
            else
            {   // Hvis ikke hentes lagret location fra databasen,
                // og bruker står på stedet hvil
                currentLocation = GetCurrentLocation();
            }

            // Lager et nytt SimpleLocation-objekt
            SimpleLocation simpleLocation = new SimpleLocation();
            simpleLocation.keyReturn = keyLevel;
            simpleLocation.LocationId = currentLocation.LocationID;
            simpleLocation.LocationName = currentLocation.LocationName;
            simpleLocation.LocationInfo = GetInfo(id);

            // Hvis lokasjonen har et bilde
            if (currentLocation.ImageID.HasValue)
                simpleLocation.ImageID = (int)currentLocation.ImageID;

            // Henter alle tilkoblede rom
            var connectedLocations = repository.GetConnectedLocations(currentLocation.LocationID);
            // Legger referanse til tilkoblede rom i en liste
            foreach (Location l in connectedLocations)
            {
                simpleLocation.AddLocation(new SimpleLocation { 
                    LocationId = l.LocationID, 
                    LocationName = l.LocationName,
                    LocationInfo = GetToolTip(l)
                });
            }
            // Sender ny posisjon samt tilkoblede dører tilbake
            return simpleLocation;
        }

        // Henter lagret posissjon fra databasen
        public Location GetCurrentLocation()
        {
            var user = GetUser();
            if (user != null && user.CurrentLocation != null)
                return repository.GetLocation(user.CurrentLocation.LocationID);
            else
                // Hvis bruker ikke har en location blir den satt til Glassgata
                return repository.GetLocation("Glassgata");
        }

        /// <summary>
        /// Genererer en string med info om valgt location
        /// </summary>
        /// <param name="id">Id'en til rommet</param>
        /// <returns>Info om rommet, presentert på en "fin" måte</returns>
        private String GetInfo(int id)
        {
            // Når bruker logger seg på er id -1, og meldingen endres til en velkomstmelding
            if (id != -1)
            {
                // Henter location-objektet
                var location = repository.GetLocation(id);
                // Romnummer skal vises med fet skrift
                string init = "<strong>" + location.LocationName + ": </strong>";
                // SJekker om infofeltene har verdier
                if (location.ShortDescription != null && location.LongDescription != null)
                    return  init + "You are " + location.ShortDescription + " | " + location.LongDescription;

                if (location.ShortDescription == null)
                    return init + "You are " + location.LongDescription;

                return init + "You are " + location.ShortDescription;
            }
            // Returnerer en velkomstmelding i starten
            String locationInfo = GetInfo(GetCurrentLocation().LocationID);
            return "Welcome to HiN. " + locationInfo;
        }

        /// <summary>
        /// Genererer tooltip som skal vises når bruker hovrer over en romknapp
        /// </summary>
        /// <param name="location">Locationen det skal hentes tooltip for</param>
        /// <returns>En kort beskrivelse av rommet</returns>
        private String GetToolTip(Location location)
        {
            // Sjekker at rommet har en ShortDescription, hvis ikke brukes LongDesc.
            String info = location.ShortDescription == null ?
                location.LongDescription : location.ShortDescription;
            // For å få det mest mulig grammatisk korrekt
            // starter beskrivelsen av et rom med in, on eller at, alt 
            // etter som. Dette skal nå fjernes fra tooltip-strengen
            String tooltipPF = info.Substring(0, 2);

            // http://stackoverflow.com/questions/2070013/comparing-a-string-with-several-different-strings
            // Fjerner in, on, at osv
            if (new[] { "on", "in", "at" }.Contains(tooltipPF))
            {
                // Starter med stor bokstav
                String toolTip = info.Substring(4, info.Length - 4);
                return info.Substring(3, 1).ToUpper() + toolTip;
            }
            return info;

        }

        // Privat metode som henter et brukerobjekt
        private ApplicationUser GetUser()
        {
            // Hvis kontrolleren skal testes
            if (mockUser != null)
            {
                return mockUser;
            }

            // Innlogget bruker
            return UserManager.FindById(User.Identity.GetUserId());
        }

    }
}
