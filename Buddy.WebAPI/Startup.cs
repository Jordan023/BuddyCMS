using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Buddy.WebAPI.Startup))]
namespace Buddy.WebAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}