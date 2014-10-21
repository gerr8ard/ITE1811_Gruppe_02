using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MockUp_Gruppe02_ITE1811.Startup))]
namespace MockUp_Gruppe02_ITE1811
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
