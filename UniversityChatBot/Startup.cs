using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UniversityChatBot.Startup))]
namespace UniversityChatBot
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
