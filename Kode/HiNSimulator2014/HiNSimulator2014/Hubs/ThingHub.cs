using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace HiNSimulator2014.Hubs
{
    /// <summary>
    /// ThingHub: En SignalR hub som brukes for å varse andre spillere 
    /// på samme lokasjon om nye ting som kommer til eller forsvinner
    /// 
    /// LocationID er thing_loc_<id>
    /// hvor <id> er id'en til en location
    /// 
    /// Skrevet av: Alexander Lindquister
    /// </summary>
    public class ThingHub : Hub
    {
        public Task JoinLocation(string LocationID)
        {
            return Groups.Add(Context.ConnectionId, LocationID);
        }

        public Task LeaveLocation(string LocationID)
        {
            return Groups.Remove(Context.ConnectionId, LocationID);
        }

        public Task RemoveLocationThing(string LocationID, int thingID, string thingName)
        {
            return Clients.Group(LocationID).removeLocationThing(thingID, thingName);
        }

        public Task AddLocationThing(string LocationID, int thingID, string thingName)
        {
            return Clients.Group(LocationID).addLocationThing(thingID, thingName);
        }
    }
}