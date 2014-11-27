using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Diagnostics;
using HiNSimulator2014.Models;
using HiNSimulator2014.Classes;
using System.Threading.Tasks;

namespace HiNSimulator2014.Hubs
{
    public class ChatHub : Hub
    {
        private Repository  repo = new Repository();
        static List<SimpleUser> ListOfUsers = new List<SimpleUser>();
        static List<MessageDetail> CurrentMessage = new List<MessageDetail>();

        public void Send(string locationId, string name, string message)
        {
            Debug.Write("mottar melding fra " + name + ": " + message);
            var groupName = "loc_" + locationId;
            Clients.Group(groupName).addNewMessageToPage(name, message);
        }

        public void Connect(string userID, string locationId)
        {
            Debug.Write("\nmottar connect fra client ");

            var connectionId = Context.ConnectionId;
            var user = repo.GetUserByID(userID);
            
            if (user != null)
            {
                var groupName = "loc_" + locationId;
                Debug.Write("\nKlient " + user.PlayerName + " kobler seg til gruppe " + groupName);

                Groups.Add(connectionId, groupName);

                Clients.Group(groupName).addLocationPlayer(user.PlayerName, userID);
                
                // Sjekker om tilkoblet bruker allerede finnes i listen på hubben
                if (ListOfUsers.Count(x => x.ConnectionId == connectionId) == 0)
                {
                    ListOfUsers.Add(new SimpleUser { 
                        ConnectionId = connectionId, 
                        PlayerId = userID, 
                        PlayerName = user.PlayerName,
                        LocationId = locationId.ToString()
                    });

                } Debug.Write("\nNumber of connected clients " + ListOfUsers.Count + " gruppenavn " + groupName);
                Clients.Caller.setPlayersInRoom(ListOfUsers);
            }
        }

        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled = true)
        {
            var user = ListOfUsers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
           
            if (user != null)
            { 
                Debug.Write("\ninne i ondisconnected " + user.PlayerName);
                ListOfUsers.Remove(user);

                var groupName = "loc_" + user.LocationId;
                var id = Context.ConnectionId;
                Clients.Group(groupName).removeLocationPlayer(id, user.PlayerName);

            }

            return base.OnDisconnected(stopCalled);
        }
        
        public Task RemoveLocationPlayer(string playerName, string locationId, string userId)
        {
            var groupName = "loc_" + locationId;
            
            if (locationId.Equals(""))
            {
                return null;
            }
            Debug.Write("\nmottar remove fra client " + playerName + " gruppenavn " + groupName);
            return Clients.OthersInGroup(groupName).removeLocationPlayer(playerName, userId);
        }

        public Task AddLocationPlayer(string locationId, string playerName, string userId)
        {
            var groupName = "loc_" + locationId;
            // Oppaterer locationId til bruker
            // http://stackoverflow.com/questions/19280986/best-way-to-update-an-element-in-a-generic-list
            var user = ListOfUsers.Where(u => u.PlayerId == userId).FirstOrDefault();
            user.LocationId = locationId;
            // Sender listen med brukere i rommet tilbake til klienten
            Clients.Caller.setPlayersInRoom(ListOfUsers);
            return Clients.OthersInGroup(groupName).addLocationPlayer(playerName, userId);
        }

        public void SendPrivateMessage(string toUserId, string message)
        {
            string fromUserId = Context.ConnectionId;

            var toUser = ListOfUsers.FirstOrDefault(x => x.PlayerId == toUserId);
            var fromUser = ListOfUsers.FirstOrDefault(x => x.ConnectionId == fromUserId);

            if(toUser != null && fromUser != null)
            {
                Debug.Write("private massage from userid " + fromUser.PlayerName + " tu user " + toUser.PlayerName + " kontaining massage: " + message);
                Clients.Client(toUser.ConnectionId).sendPrivateMessage(fromUser.PlayerId, fromUser.PlayerName, message);
            }
        }

        public Task leaveLocation(string locationId)
        {
            var groupName = "loc_" + locationId;
            return Groups.Remove(Context.ConnectionId, groupName);
        }

    }
}