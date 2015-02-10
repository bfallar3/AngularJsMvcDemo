using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AngularJsMvcDemo.Startup))]
namespace AngularJsMvcDemo
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
