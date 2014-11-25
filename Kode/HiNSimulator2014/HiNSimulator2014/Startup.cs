using Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

[assembly: OwinStartupAttribute(typeof(HiNSimulator2014.Startup))]
namespace HiNSimulator2014
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            // For debugging
            var hubConfiguration = new HubConfiguration();
            hubConfiguration.EnableDetailedErrors = true;
            app.MapSignalR(hubConfiguration);
        }
    }
}
