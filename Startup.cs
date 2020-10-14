using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebBucketApp.Startup))]
namespace WebBucketApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
