using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StarProgrammerExtensionLibrary.Startup))]
namespace StarProgrammerExtensionLibrary
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
