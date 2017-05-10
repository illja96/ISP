using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ISP.Startup))]
namespace ISP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
