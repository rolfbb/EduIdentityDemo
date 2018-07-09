using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EduIdentityDemo.Startup))]
namespace EduIdentityDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
