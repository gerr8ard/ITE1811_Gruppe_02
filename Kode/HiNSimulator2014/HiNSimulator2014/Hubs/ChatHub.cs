using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using System.Diagnostics;
using HiNSimulator2014.Models;
using Microsoft.AspNet.Identity.Owin;
using HiNSimulator2014.Controllers.WebApi;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Globalization;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Host.SystemWeb;
using HiNSimulator2014.Classes;

namespace HiNSimulator2014.Hubs
{
    public class ChatHub : Hub
    {
        private Repository repo = new Repository();
        private ApplicationUserManager _userManager;

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
       
        public void Send(string LocationID, string name, string message)
        {
            Clients.Group(LocationID).addNewMessageToPage(name, message);
        }

        public Task JoinLocation(string LocationID)
        {
            if (LocationID.Equals("-1"))
            {
                LocationID = GetCurrentLocationPrivate().LocationID.ToString();
            }
            return Groups.Add(Context.ConnectionId, LocationID);
            
        }

        public Task LeaveLocation(string LocationID)
        {
            if (LocationID.Equals("-1"))
            {
                LocationID = GetCurrentLocationPrivate().LocationID.ToString();
            }
            return Groups.Remove(Context.ConnectionId, LocationID);
        }

        public Task RemoveLocationPlayer(string LocationID, string playerName, string playerId)
        {
            if (LocationID.Equals(""))
            {
                LocationID = GetCurrentLocationPrivate().LocationID.ToString();
            }
            return Clients.Group(LocationID).removeLocationPlayer(LocationID, playerName, playerId);
        }

        public Task AddLocationPlayer(string LocationID, string playerName, string playerId)
        {
            return Clients.Group(LocationID).addLocationPlayer(LocationID, playerName, playerId);
        }

        // Henter lagret posissjon fra databasen
        private Location GetCurrentLocationPrivate()
        {
            ChatController cc = new ChatController();
            var user = cc.getUser();
            //ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
           
            
            //var user = repo.GetUserByName(Context.User.Identity.Name);
            if (user != null && user.CurrentLocation != null)
                return repo.GetLocation(user.CurrentLocation.LocationID);
            else
                return repo.GetLocation("Glassgata");
        }

    }
}