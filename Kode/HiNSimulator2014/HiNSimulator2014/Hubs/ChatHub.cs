using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using System.Diagnostics;
using HiNSimulator2014.Models;

namespace HiNSimulator2014.Hubs
{
    public class ChatHub : Hub
    {
        private Repository repo = new Repository();
        
        public void Send(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(name, message);
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
            if (LocationID.Equals("-1"))
            {
                LocationID = GetCurrentLocationPrivate().LocationID.ToString();
            }
            return Clients.Group(LocationID).removeLocationPlayer(playerId, playerName);
        }

        public Task AddLocationPlayer(string LocationID, string playerName, string playerId)
        {
            return Clients.Group(LocationID).addLocationPlayer(playerId, playerName);
        }

        // Henter lagret posissjon fra databasen
        private Location GetCurrentLocationPrivate()
        {
            var user = repo.GetUserByName(Context.User.Identity.Name);
            if (user != null && user.CurrentLocation != null)
                return repo.GetLocation(user.CurrentLocation.LocationID);
            else
                return repo.GetLocation("Glassgata");
        }

    }
}