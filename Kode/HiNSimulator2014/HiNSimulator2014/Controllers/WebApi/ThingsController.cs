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

        public ThingsController(IRepository repo)
        {
            repository = repo;
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
        public List<SimpleThing> GetThingsInCurrentLocation(int? id)
        {
            List<SimpleThing> simpleList = new List<SimpleThing>();
            List<Thing> thingList;
 
            if (id != null)
            {
                thingList = repository.GetThingsInLocation(repository.GetLocation((int)id));
            }
            else
            {
                ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
                thingList = repository.GetThingsInLocation(user.CurrentLocation);
            }
            foreach (Thing t in thingList)
            {
                simpleList.Add(new SimpleThing { ThingID = t.ThingID, Name = t.Name });
            }

            return simpleList;
        }

        // GET: api/Things/GetThingsInInventory
        [HttpGet]
        public List<SimpleThing> GetThingsInInventory()
        {
            ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
            List<SimpleThing> simpleList = new List<SimpleThing>();

            foreach (Thing t in repository.GetThingsForOwner(user))
            {
                simpleList.Add(new SimpleThing { ThingID = t.ThingID, Name = t.Name });
            }

            return simpleList;
        }

        // GET: api/Things/GetThing/5
        [HttpGet]
        public SimpleThing GetThing(int id)
        {
            Thing t = repository.GetThingById(id);

            return new SimpleThing {
                ThingID = t.ThingID,
                Name = t.Name,
                Description = t.Description,
                KeyLevel = t.KeyLevel,
                ImageID = t.ImageID,
                PlayerWritable = t.PlayerWritable,
                WrittenText = t.WrittenText };
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
            if (thing != null && !thing.IsStationary && thing.CurrentLocation != null &&
                thing.CurrentLocation.LocationID == user.CurrentLocation.LocationID )
            {
                thing.CurrentLocation = null;
                thing.ArtificialPlayerOwner = null;
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
            if (thing != null && thing.CurrentOwner != null && thing.CurrentOwner == user)
            {
                thing.CurrentOwner = null;
                thing.ArtificialPlayerOwner = null;
                thing.CurrentLocation = user.CurrentLocation;
                repository.UpdateThing(thing);
                return true;
            }
            return false;
        }

    }
}
