using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HiNSimulator2014.Models;
using System.Diagnostics;
using HiNSimulator2014.Classes;

namespace HiNSimulator2014.Controllers.WebApi
{
    /// <summary>
    /// LocationController - tar seg av forespørseler som er relaterte til navigasjon
    /// og andre Location-baserte tjenester.
    /// 
    /// @author Andreas Dyrøy Jansson
    /// </summary>
    [Authorize]
    public class LocationController : ApiController
    {
        private Repository repository;


        public LocationController()
        {
            repository = new Repository();
        }

        public LocationController(Repository r)
        {
            repository = r;
        }

        private void UpdatePlayerLocation(int id)
        {
            Debug.Write("flytter til: " + id);
            repository.UpdatePlayerLocation(User.Identity.Name, id);
        }

        // do later: http://stackoverflow.com/questions/1877225/how-do-i-unit-test-a-controller-method-that-has-the-authorize-attribute-applie

        // GET api/Location/CheckAccess/5
        [HttpGet]
        public bool CheckAccess(int id)
        {
            // Sjekker om spilleren har tilgang til ønsket rom, enten for at døren er åpen, eller
            // Spilleren har en Thing i sitt Inventory med påkrevd KeyLevel.
            Location currentLocation = GetCurrentLocationPrivate();
            LocationConnection lc = repository.GetLocationConnection(currentLocation.LocationID, id);
            List<Thing> currentInventory = repository.GetThingsForOwner(User.Identity.Name);
            Debug.Write("\nCurrentLocation: " + currentLocation.LocationID + ", NextLocation: " + id);
            if (lc != null)
            {
                // Hvis døren er default åpen
                if (lc.RequiredKeyLevel <= 0)
                    return true;

                Debug.Write("\nLocationConnection from: " + lc.LocationOne_LocationID + " to: " + lc.LocationTwo_LocationID);
                Debug.Write("isLocked: " + lc.IsLocked);
                foreach (Thing t in currentInventory)
                {
                    if (t.KeyLevel.HasValue)
                    {
                        if (t.KeyLevel >= lc.RequiredKeyLevel)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        // GET api/Location/MoveTo/5
        [HttpGet]
        public SimpleLocation MoveTo(int id)
        {
            Location currentLocation;

            // Hvis id != -1 kom kallet fra en knapp hos klienten
            if (id != -1)
            {
                UpdatePlayerLocation(id);
                currentLocation = repository.GetLocation(id);
            }
            else
            {   // Hvis ikke hentes lagret location fra databasen
                currentLocation = GetCurrentLocationPrivate();
            }

            SimpleLocation simpleLocation = new SimpleLocation();

            simpleLocation.LocationId = currentLocation.LocationID;
            simpleLocation.LocationName = currentLocation.LocationName;
            simpleLocation.LocationInfo = GetInfo(id);

            // Hvis lokasjonen har et bilde
            if (currentLocation.ImageID.HasValue)
                simpleLocation.ImageID = (int)currentLocation.ImageID;

            var connectedLocations = repository.GetConnectedLocations(currentLocation.LocationID);
            foreach (Location l in connectedLocations)
            {
                simpleLocation.AddLocation(new SimpleLocation { 
                    LocationId = l.LocationID, 
                    LocationName = l.LocationName 
                });
            }
            return simpleLocation;
        }

        // Henter lagret posissjon fra databasen
        private Location GetCurrentLocationPrivate()
        {
            var user = repository.GetUserByName(User.Identity.Name);
            if (user != null && user.CurrentLocation != null)
                return repository.GetLocation(user.CurrentLocation.LocationID);
            else
                return repository.GetLocation("Glassgata");
        }

        // Genererer en string med info om valgt location
        private String GetInfo(int id)
        {
            if (id != -1)
            {
                var location = repository.GetLocation(id);
                string init = "<strong>" + location.LocationName + ": </strong>";
                if (location.ShortDescription != null && location.LongDescription != null)
                    return  init + "You are " + location.ShortDescription + " | " + location.LongDescription;

                if (location.ShortDescription == null)
                    return init + "You are " + location.LongDescription;

                return init + "You are " + location.ShortDescription;
            }
            // Fancy kul velkomstmelding
            var user = repository.GetUserByName(User.Identity.Name);
            String locationInfo = GetInfo(repository.GetLocation("Glassgata").LocationID);
            if (user != null && user.CurrentLocation != null)
                locationInfo = GetInfo(user.CurrentLocation.LocationID);
            return "Welcome to HiN. " + locationInfo;
        }

    }
}
