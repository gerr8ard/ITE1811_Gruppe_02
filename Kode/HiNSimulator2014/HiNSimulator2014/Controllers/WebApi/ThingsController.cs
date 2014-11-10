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
        
        // GET: api/Things
        public List<Thing> GetThingsInCurrentLocation()
        {
            Location currentLoc = UserManager.FindById(User.Identity.GetUserId()).CurrentLocation;

            return repository.GetThingsInLocation(currentLoc);
        }

    }
}
