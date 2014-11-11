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

namespace HiNSimulator2014.Controllers.WebApi
{
    /// <summary>
    /// ThingsController: En WebAPI kontroller for å manipulere ting i spillet
    /// Kontrolleren brukes til å hente ting i en lokasjon, ting spilleren eier, informasjon om en ting,
    /// samt mulighet til å endre tings eier og andre atributter tilhørende ting.
    /// 
    /// Skrevet av: Alexander Lindquister
    /// </summary>
    [Authorize]
    public class ThingsController : ApiController
    {
        private IRepository repository;
        private ApplicationUserManager _userManager;

        public ThingsController()
        {
            repository = new Repository();
        }

        public ThingsController(ApplicationUserManager userManager)
        {
            repository = new Repository();
            UserManager = userManager;
        }

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

        // GET: api/Things/GetThingsInCurrentLocation
        [HttpGet]
        public List<Thing> GetThingsInCurrentLocation()
        {
            ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
            return repository.GetThingsInLocation(user.CurrentLocation);
        }

        // GET: api/Things/GetThingsInInventory
        [HttpGet]
        public List<Thing> GetThingsInInventory()
        {
            ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
            return repository.GetThingsForOwner(user);
        }

        // GET: api/Things/GetThing/5
        [HttpGet]
        public Thing GetThing(int id)
        {
            return repository.GetThingById(id);
        }

        // POST: api/Things/WriteOnThing/5
        [HttpPost]
        public bool WriteOnThing(int id, [FromBody] string value)
        {
            Thing thing = repository.GetThingById(id);

            if (thing != null && thing.PlayerWritable)
            {
                thing.WrittenText = value;
                repository.UpdateThing(thing);
                return true;
            }
            return false;
        }

        // GET: api/Things/TakeThing/5
        // Metode for å plukke opp en ting fra et rom og legge den til i sitt inventory
        [HttpGet]
        public bool TakeThing(int id)
        {
            ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
            Thing thing = repository.GetThingById(id);

            // Sjekke at tingen eksisterer, den er i et rom, den ikke har en eier,
            // at spilleren er i samme rom som tingen og at tingen ikke er fast inventar i rommet
            if (thing != null && thing.CurrentLocation != null && thing.CurrentOwner == null &&
                thing.CurrentLocation.LocationID == user.CurrentLocation.LocationID && !thing.IsStationary)
            {
                thing.CurrentLocation = null;
                thing.CurrentOwner = user;
                repository.UpdateThing(thing);
                return true;
            }
            return false;
        }


        // GET: api/Things/DropThing/5
        // Metode for å legge fra seg en ting fra sitt inventory i rommet spilleren står i
        [HttpGet]
        public bool DropThing(int id)
        {
            ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
            Thing thing = repository.GetThingById(id);

            // Sjekke at tingen eksisterer, den ikke har en lokasjon og at spilleren eier tingen
            if (thing != null && thing.CurrentLocation == null && thing.CurrentOwner != null && thing.CurrentOwner == user)
            {
                thing.CurrentOwner = null;
                thing.CurrentLocation = user.CurrentLocation;
                repository.UpdateThing(thing);
                return true;
            }
            return false;
        }

    }
}
