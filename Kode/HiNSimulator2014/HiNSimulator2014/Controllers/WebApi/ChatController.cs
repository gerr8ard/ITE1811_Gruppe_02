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
using System.Diagnostics;
using HiNSimulator2014.Classes;

namespace HiNSimulator2014.Controllers.WebApi
{
    public class ChatController : ApiController
    {

        private IRepository repository;
        private ApplicationUserManager _userManager;

        public ChatController()
        {
            repository = new Repository();
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
       
        // GET: api/Chat/GetPlayersInCurrentLocation
        [HttpGet]
        public List<SimpleUser> GetPlayersInCurrentLocation(int id)
        {
            var playerList = new List<SimpleUser>();
                var list = repository.GetPlayersInLocation(repository.GetLocation((int) id));
                foreach (ApplicationUser u in list)
                {
                    playerList.Add(new SimpleUser { PlayerName = u.PlayerName, PlayerId = u.Id});
                }
                
                return playerList;
        }

        public String GetUsername()
        {
            ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
            return user.PlayerName;
        }

        public String getUserId()
        {
            ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
            return user.Id;
        }

        protected Location GetCurrentLocationPrivate()
        {
            ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());


            //var user = repo.GetUserByName(Context.User.Identity.Name);
            if (user != null && user.CurrentLocation != null)
                return repository.GetLocation(user.CurrentLocation.LocationID);
            else
                return repository.GetLocation("Glassgata");
        }
        
    }
}
