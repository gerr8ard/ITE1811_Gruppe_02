﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HiNSimulator2014.Models;
using System.Diagnostics;

namespace HiNSimulator2014.Controllers.WebApi
{
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

        public IEnumerable<Location> Get()
        {
            Debug.Write("Kaller Location GET");
            var user = repository.GetUserByName(User.Identity.Name);
            if (user != null && user.CurrentLocation != null)
                return repository.GetConnectedLocations(user.CurrentLocation.LocationID);
            else
                return repository.GetConnectedLocations(repository.GetLocation("Glassgata"));
        }

        /*
        // GET api/movement/5
        [HttpGet]
        public IEnumerable<Location> Get(int id)
        {
            Debug.Write("forespurt index: " + id);
            //repository.UpdatePlayerLocation(User.Identity.Name, locationId);
            return repository.GetConnectedLocations(id);
        }**/

        // GET apiLocation/MoveTo/5
        [HttpGet]
        public IEnumerable<Location> MoveTo(int id)
        {
            Debug.Write("forespurt index: " + id);
            repository.UpdatePlayerLocation(User.Identity.Name, id);
            return repository.GetConnectedLocations(id);
        }

        // Henter info om valgt location
        [HttpGet]
        public String GetInfo(int id)
        {
            if (id != -1)
            {
                var location = repository.GetLocation(id);
                if (location.ShortDescription != null && location.LongDescription != null)
                    return "You are in " + location.ShortDescription + " | " + location.LongDescription;

                if (location.ShortDescription == null)
                    return "You are in " + location.LongDescription;

                return "You are in " + location.ShortDescription;
            }
            // Fancy kul velkomstmelding
            var user = repository.GetUserByName(User.Identity.Name);
            String locationInfo = GetInfo(repository.GetLocation("Glassgata").LocationID);
            if (user != null && user.CurrentLocation != null)
                locationInfo = GetInfo(user.CurrentLocation.LocationID);
            return "Welcome to HiN. You are in " + locationInfo;
        }


    }
}