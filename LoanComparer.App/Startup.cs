using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LoanComparer.App.Startup))]
namespace LoanComparer.App
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
