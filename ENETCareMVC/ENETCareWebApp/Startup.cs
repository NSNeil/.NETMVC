using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ENETCareWebApp.Startup))]
namespace ENETCareWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
