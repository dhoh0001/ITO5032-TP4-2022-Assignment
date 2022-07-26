using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ITO5032_Assignment.Startup))]
namespace ITO5032_Assignment
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
