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

        //[Authorize]
        public IEnumerable<Location> Get()
        {
            Debug.Write("Kaller Location GET");
            var user = repository.GetUserByName(User.Identity.Name);
            if (user != null && user.CurrentLocation != null)
                return repository.GetConnectedLocations(user.CurrentLocation.LocationID);
            else
                return repository.GetConnectedLocations(repository.GetLocation("Glassgata"));
        }

        // GET api/movement/5
        //[Authorize]
        [HttpGet]
        public IEnumerable<Location> Get(int id)
        {
            Debug.Write("forespurt index: " + id);
            //repository.UpdatePlayerLocation(User.Identity.Name, locationId);
            return repository.GetConnectedLocations(id);
        }

        // GET apiLocation/MoveTo/5
        //[Authorize]
        [HttpGet]
        public IEnumerable<Location> MoveTo(int id)
        {
            Debug.Write("forespurt index: " + id);
            //repository.UpdatePlayerLocation(User.Identity.Name, locationId);
            return repository.GetConnectedLocations(id);
        }


    }
}
