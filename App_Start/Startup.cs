using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(PrmFindJobSerivces.App_Start.Startup))]

namespace PrmFindJobSerivces.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen();
        }
    }
}
