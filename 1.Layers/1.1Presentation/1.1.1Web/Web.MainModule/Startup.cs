using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Web.MainModule.Startup))]
namespace Web.MainModule
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
