using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GasMundial.Presentacion.MVC.Startup))]
namespace GasMundial.Presentacion.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
