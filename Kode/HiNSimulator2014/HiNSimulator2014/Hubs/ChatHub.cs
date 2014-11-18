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
    public class ChatTest : Hub
    {
        
        public void Send(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(name, message);
        }

        public Task AddUserToChat(string LocationID, int userId, string userName)
        {
            return Clients.Group(LocationID).addUserToChat(userId, userName);
        }

        public Task JoinLocation(string LocationID)
        {
            Debug.Write("ny spiller joiner :)" + Groups.ToString());
            return Groups.Add(Context.ConnectionId, LocationID);
            
        }

        public Task LeaveLocation(string LocationID)
        {
            Debug.Write("ny spiller liver :)");
            return Groups.Remove(Context.ConnectionId, LocationID);
        }

        public Task RemoveLocationPlayer(string LocationID, string playerName)
        {
            Debug.Write("locid= " + LocationID + "plnm= " + playerName);
            return Clients.Group(LocationID).removeLocationPlayer(playerName);
        }

        public Task AddLocationPlayer(string LocationID, string playerName)
        {
            return Clients.Group(LocationID).addLocationPlayer(playerName);
        }

    }
}