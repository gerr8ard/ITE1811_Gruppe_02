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

        /// <summary>
        /// Metode som sender ut melding til de som er i samme rom som deg selv.
        /// </summary>
        /// <param name="LocationID"></param>
        /// <param name="name"></param>
        /// <param name="message"></param>
        public void Send(string LocationID, string name, string message)
        {
            Debug.Write("inne i send: " + name + " mld: " + message);
            Clients.Group(LocationID).addNewMessageToPage(name, message);
        }

        public Task JoinLocation(string LocationID)
        {
            Debug.Write("\nJoinLocation " + LocationID);
            if (LocationID.Equals("-1"))
            {
                LocationID = GetCurrentLocationPrivate().LocationID.ToString();
            }
            return Groups.Add(Context.ConnectionId, LocationID);
            
        }

        public Task LeaveLocation(string LocationID)
        {
            Debug.Write("\nLeaveLocation " + LocationID);
            if (LocationID.Equals("-1"))
            {
                LocationID = GetCurrentLocationPrivate().LocationID.ToString();
            }
            return Groups.Remove(Context.ConnectionId, LocationID);
        }

        public Task RemoveLocationPlayer(string LocationID, string playerName, string playerId)
        {
            Debug.Write("\nRemoveLocationPlayer " + LocationID + " bruker " + playerName );
            if (LocationID.Equals(""))
            {
                LocationID = GetCurrentLocationPrivate().LocationID.ToString();
            }
            return Clients.Group(LocationID).removeLocationPlayer(LocationID, playerName, playerId);
        }

        public Task AddLocationPlayer(string LocationID, string playerName, string playerId)
        {
            Debug.Write("\nAddLocationPlayer " + LocationID + "bruker " + playerName);
            return Clients.Group(LocationID).addLocationPlayer(LocationID, playerName, playerId);
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