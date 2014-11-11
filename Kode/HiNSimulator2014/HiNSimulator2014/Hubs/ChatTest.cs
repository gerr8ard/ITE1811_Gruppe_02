using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

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

    }
}