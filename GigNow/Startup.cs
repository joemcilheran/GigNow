using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GigNow.Startup))]
namespace GigNow
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
