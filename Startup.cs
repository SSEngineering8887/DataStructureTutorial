using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DataStruct.Startup))]
namespace DataStruct
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
