using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC.Presentacion.Startup))]
namespace MVC.Presentacion
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
