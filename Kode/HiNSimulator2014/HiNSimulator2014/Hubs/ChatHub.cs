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
        private Repository  repo = new Repository();
        private ApplicationUserManager _userManager;
        static List<SimpleUser> ListOfUsers = new List<SimpleUser>();
        static List<MessageDetail> CurrentMessage = new List<MessageDetail>();

        public void Send(string LocationID, string name, string message)
        {
            Clients.Group(LocationID).addNewMessageToPage(name, message);
        }

        public void Connect(string userID)
        {

            var connectionId = Context.ConnectionId;
            var user = repo.GetUserByID(userID);
            Debug.Write("mottar connect fra client " + user);
            if (user != null)
            {
                var locationId = user.CurrentLocation.LocationID;
                var groupName = "loc_" + locationId;

                Groups.Add(connectionId, groupName);
                Clients.OthersInGroup(groupName).addLocationPlayer(connectionId, user.PlayerName);

                if (ListOfUsers.Count(x => x.ConnectionId == connectionId) == 0)
                {
                    ListOfUsers.Add(new SimpleUser { ConnectionId = connectionId, PlayerId = userID, PlayerName = user.PlayerName });

                } Debug.Write("Number of connected clients " + ListOfUsers.Count);
            }
        }

        public Task Disconnect(string LocationID)
        {
            return Groups.Remove(Context.ConnectionId, LocationID);
        }

        public Task RemoveLocationPlayer(string LocationID, string playerName, string playerId)
        {
            var connectionid = Context.ConnectionId;

            if (LocationID.Equals(""))
            {
                return null;
            }
            return Clients.Group(LocationID).removeLocationPlayer(LocationID, playerName, playerId, connectionid);
        }

        public Task AddLocationPlayer(string LocationID, string playerName, string playerID)
        {
            var connectionid = Context.ConnectionId;

            return Clients.Group(LocationID).addLocationPlayer(LocationID, playerName, playerID, connectionid);
        }

        public void SendPrivateMessage(string toUserId, string message)
        {
            string fromUserId = Context.ConnectionId;

            Debug.Write("massage from userid " + fromUserId + " tu user " + toUserId + " kontaining massage " + message);

            var toUser = ListOfUsers.FirstOrDefault(x => x.PlayerId == toUserId);
            var fromUser = ListOfUsers.FirstOrDefault(x => x.PlayerId == fromUserId);

            Debug.Write("listofusers " + ListOfUsers + " touser" + toUser + " fromuser " + fromUser);

            if(toUser != null && fromUser != null)
            {
                Clients.Client(toUser.ConnectionId).sendPrivateMessage(fromUserId, fromUser.PlayerName, message);

                Clients.Caller.sendPrivateMessage(toUserId, fromUser.PlayerName, message);
            }
        }

    }
}