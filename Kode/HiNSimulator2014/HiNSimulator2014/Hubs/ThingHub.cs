using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Host.SystemWeb;
using Microsoft.AspNet.SignalR;

using HiNSimulator2014.Models;

namespace HiNSimulator2014.Hubs
{
    /// <summary>
    /// ThingHub: En SignalR hub som brukes for å varse andre spillere 
    /// på samme lokasjon om nye ting som kommer til eller forsvinner
    /// 
    /// Skrevet av: Alexander Lindquister
    /// </summary>
    [Authorize]
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