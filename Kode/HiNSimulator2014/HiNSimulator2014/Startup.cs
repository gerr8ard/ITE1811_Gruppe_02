using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HiNSimulator2014.Startup))]
namespace HiNSimulator2014
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
