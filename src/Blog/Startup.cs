using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Blog.Models;
using Blog.Models.PostViewModels;
using Blog.Models.Account;
using Microsoft.AspNetCore.Mvc;
using Blog.Models.Data;
using Blog.ViewModels;
using Blog.Service;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json.Serialization;
using Blog.ViewModels.ControlPanelViewModels;
using Blog.Controllers;
using Blog.Models.AccountViewModels;
using Sakura.AspNetCore;
using Blog.ViewModels.PostViewModels;
using Blog.Models.Entities;

namespace Blog
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();

                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
          
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddDbContext<BlogContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<BlogUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;

            })
            .AddEntityFrameworkStores<BlogContext>();

    

            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IPageService, PageService>();
            services.AddScoped<IMailService, MailService>();
            services.AddTransient<BlogInit>();


            services.AddMvc(option => {
                option.CacheProfiles.Add("Default",
                    new CacheProfile()
                    {
                       NoStore = true            
                    });
                })
                .AddJsonOptions(option =>
                {
                    option.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });

            }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app,
                                    IHostingEnvironment env,
                                    ILoggerFactory loggerFactory,
                                    BlogInit init)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            
            app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            app.UseIdentity();

            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<RegistrationViewModel, BlogUser>();
                config.CreateMap<Post, PostViewModel>().ReverseMap();
                config.CreateMap<Tag, TagViewModel>().ReverseMap();
                config.CreateMap<Category, CategoryViewModel>().ReverseMap();
                config.CreateMap<CreatePostViewModel, Post>();
                config.CreateMap<Post, PostControlPanelViewModel>().ReverseMap();
                config.CreateMap<BlogUser, UserControlPanelViewModel>().ReverseMap();
                config.CreateMap<Image, ImageViewModel>().ReverseMap();
            });
           

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "Home",
                    template: "{controller}/{page?}",
                    defaults: new {controller = "Blog", Action="Index"}
                    );

                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{page?}"
                    );

            });

            await init.SeedDataAsync();

        }
    }
}
