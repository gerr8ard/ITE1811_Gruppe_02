using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace HiNSimulator2014.Hubs
{
    /// <summary>
    /// ArtificialPlayerHub: En SignalR hub som brukes for å varse andre spillere 
    /// på samme lokasjon om kunstige aktører som kommer til eller forsvinner
    /// 
    /// LocationID er art_plyrs_loc_<id>
    /// hvor <id> er id'en til en location
    /// 
    /// Skrevet av: Alexander Lindquister
    /// </summary>
    public class ArtificialPlayerHub : Hub
    {
        public Task JoinLocation(string LocationID)
        {
            return Groups.Add(Context.ConnectionId, LocationID);
        }

        public Task LeaveLocation(string LocationID)
        {
            return Groups.Remove(Context.ConnectionId, LocationID);
        }

        public Task RemoveArtificialPlayer(string LocationID, int artificialPlayerId, string artificialPlayerName)
        {
            return Clients.Group(LocationID).removeArtificialPlayer(artificialPlayerId, artificialPlayerName);
        }

        public Task AddArtificialPlayer(string LocationID, int artificialPlayerId, string artificialPlayerName)
        {
            return Clients.Group(LocationID).addArtificialPlayer(artificialPlayerId, artificialPlayerName);
        }
    }
}