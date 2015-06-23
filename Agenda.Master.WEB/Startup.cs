using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Agenda.Master.WEB.Startup))]
namespace Agenda.Master.WEB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
