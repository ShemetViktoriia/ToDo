using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ToDo_WebUI.Startup))]
namespace ToDo_WebUI
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
