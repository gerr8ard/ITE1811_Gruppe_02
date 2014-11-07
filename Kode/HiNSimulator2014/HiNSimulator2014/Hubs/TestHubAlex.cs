using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace HiNSimulator2014.Hubs
{
    public class TestHubAlex : Hub
    {
        public void SrvHello(string str)
        {
            Clients.All.CliHello(str);
        }

        public void TakeThing(string thingID)
        {
            Clients.All.removeThing(thingID);
        }


    }
}