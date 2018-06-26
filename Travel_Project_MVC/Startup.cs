using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Travel_Project_MVC.Startup))]
namespace Travel_Project_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
