using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HiNSimulator2014.Models;
using System.Diagnostics;

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

        public IEnumerable<Location> GetConnectedRooms()
        {
            // Debug.Write("Kaller Location GET");
            var user = repository.GetUserByName(User.Identity.Name);
            if (user != null && user.CurrentLocation != null)
                return repository.GetConnectedLocations(user.CurrentLocation.LocationID);
            else
                return repository.GetConnectedLocations(repository.GetLocation("Glassgata"));
        }

        // GET apiLocation/MoveTo/5
        [HttpGet]
        public IEnumerable<Location> MoveTo(int id)
        {
            Debug.Write("forespurt index: " + id);
            repository.UpdatePlayerLocation(User.Identity.Name, id);
            return repository.GetConnectedLocations(id);
        }

        // GET api/Location/CheckAccess/5
        [HttpGet]
        // Sjekker om spilleren har tilgang til ønsket rom, enten for at døren er åpen, eller
            // Spilleren har en Thing i sitt Inventory med påkrevd KeyLevel.
        public bool CheckAccess(int id)
        {
            Location currentLocation = GetCurrentLocation();
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

        // GET api/Location/GetCurrentLocation
        [HttpGet]
        public Location GetCurrentLocation()
        {
            var user = repository.GetUserByName(User.Identity.Name);
            if (user != null && user.CurrentLocation != null)
                return repository.GetLocation(user.CurrentLocation.LocationID);
            else
                return repository.GetLocation("Glassgata");
        }

        // Henter info om valgt location
        [HttpGet]
        public String GetInfo(int id)
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
