using CoreBlog.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WilderBlog.Data
{
    // to resolve migrations in class library
    public class Startup
    {
        private IConfigurationRoot config;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(env.ContentRootPath)
              .AddJsonFile("config.json", false, true)
              .AddEnvironmentVariables();

            config = builder.Build();

        }

        public void ConfigureServices(IServiceCollection svc)
        {
            svc.AddSingleton<IConfigurationRoot>(config);

            svc.AddEntityFrameworkSqlServer()
              .AddDbContext<BlogContext>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseIdentity();
        }
    }
}