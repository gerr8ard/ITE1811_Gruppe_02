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

                Clients.Group(groupName).addLocationPlayer(user.PlayerName, connectionId);
                Clients.Caller.getPlayersInRoom(ListOfUsers);

                if (ListOfUsers.Count(x => x.ConnectionId == connectionId) == 0)
                {
                    ListOfUsers.Add(new SimpleUser { 
                        ConnectionId = connectionId, 
                        PlayerId = userID, 
                        PlayerName = user.PlayerName,
                        LocationId = locationId.ToString()
                    });

                } Debug.Write("\nNumber of connected clients " + ListOfUsers.Count + " gruppenavn " + groupName);
                
            }
        }

        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled = true)
        {
            var item = ListOfUsers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
           
            if (item != null)
            { Debug.Write("\ninne i ondisconnected " + item.PlayerName);
                ListOfUsers.Remove(item);

                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, item.PlayerName);

            }

            return base.OnDisconnected(stopCalled);
        }
        
        public Task RemoveLocationPlayer(string playerName, string locationId)
        {
            var connectionid = Context.ConnectionId;
            var groupName = "loc_" + locationId;
            
            if (locationId.Equals(""))
            {
                return null;
            }
            Debug.Write("\nmottar remove fra client " + playerName + " gruppenavn " + groupName);
            return Clients.Group(groupName).removeLocationPlayer(playerName, connectionid);
        }

        public Task AddLocationPlayer(string locationId, string playerName)
        {
            var connectionid = Context.ConnectionId;
            var groupName = "loc_" + locationId;
            Clients.Caller.getPlayersInRoom(ListOfUsers);
            return Clients.Group(groupName).addLocationPlayer(playerName, connectionid);
        }

        public void SendPrivateMessage(string toUserId, string message)
        {
            string fromUserId = Context.ConnectionId;

            

            var toUser = ListOfUsers.FirstOrDefault(x => x.ConnectionId == toUserId);
            var fromUser = ListOfUsers.FirstOrDefault(x => x.ConnectionId == fromUserId);
            

            //Debug.Write("listofusers " + ListOfUsers + " touser" + toUser + " fromuser " + fromUser);

            if(toUser != null && fromUser != null)
            {
                Debug.Write("private massage from userid " + fromUser.PlayerName + " tu user " + toUser.PlayerName + " kontaining massage: " + message);
                Clients.Client(toUser.ConnectionId).sendPrivateMessage(fromUserId, fromUser.PlayerName, message);

                //Clients.Caller.sendPrivateMessage(toUserId, fromUser.PlayerName, message);
            }
        }

        public Task leaveLocation(string locationId)
        {
            var groupName = "loc_" + locationId;
            return Groups.Remove(Context.ConnectionId, groupName);
        }

    }
}