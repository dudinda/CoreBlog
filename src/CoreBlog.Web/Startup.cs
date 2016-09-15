using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json.Serialization;
using CoreBlog.Web.ViewModels.ControlPanel;
using CoreBlog.Web.ViewModels.Post;
using CoreBlog.Data.Entities;
using CoreBlog.Web.ViewModels.Account;
using CoreBlog.Web.Services;
using CoreBlog.Data.Context;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Html;

namespace CoreBlog.Web
{
    public class Startup
    {
        private IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration);
            services.AddDbContext<BlogContext>();
            services.AddIdentity<BlogUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;

                options.SignIn.RequireConfirmedEmail = true;

                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;

                options.Cookies.ApplicationCookie.LoginPath = "/Auth/Login";
                options.Cookies.ApplicationCookie.AccessDeniedPath = "/Auth/Forbidden";
                
            })
            .AddEntityFrameworkStores<BlogContext>()
            .AddDefaultTokenProviders();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy =>
                {
                    policy.RequireRole("Admin");
                });

                options.AddPolicy("Active", policy =>
                {
                    policy.RequireRole("Admin", "User");
                });

            });

            services.AddLogging();

            services.AddScoped<IBlogContext, BlogContext>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IPageService, PageService>();
            services.AddScoped<IMailService, MailService>();
            services.AddTransient<BlogInit>();

            services
                .AddMvc(option =>
                {
                    option.CacheProfiles.Add("Default",
                        new CacheProfile()
                        {
                            NoStore = true
                        });
                })
                .AddJsonOptions(option =>
                {
                    option
                        .SerializerSettings
                        .ContractResolver = new CamelCasePropertyNamesContractResolver();
                });       
        }


        public void Configure(IApplicationBuilder app,
                              IHostingEnvironment env,
                              ILoggerFactory loggerFactory,
                              IServiceScopeFactory scopeFactory)
        {
            loggerFactory.AddConsole(LogLevel.Information);
            loggerFactory.AddDebug(LogLevel.Error);


            if (env.IsDevelopment())
            {
                loggerFactory.AddDebug(LogLevel.Information);
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {

                app.UseExceptionHandler("/errors/500");
                app.UseStatusCodePagesWithReExecute("/errors/{0}");
            }
            app.UseStaticFiles();
            app.UseCookieAuthentication();
            app.UseIdentity();

            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<RegistrationViewModel, BlogUser>();
                config.CreateMap<Post, PostViewModel>().ReverseMap();
                config.CreateMap<Tag, TagViewModel>().ReverseMap();
                config.CreateMap<Category, CategoryViewModel>().ReverseMap();
                config.CreateMap<Post, PostControlPanelViewModel>().ReverseMap();
                config.CreateMap<BlogUser, UserControlPanelViewModel>().ReverseMap();
                config.CreateMap<Image, ImageViewModel>().ReverseMap();
                config.CreateMap<Post, CreatePostViewModel>().ReverseMap();
            });
            app.UseMvc(routes =>
            {

                routes.MapRoute(
                    name: "Home",
                    template: "{controller}/{page?}",
                    defaults: new { controller = "Blog", Action = "Index" }
                    );

                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{page?}"
                    );

                routes.MapRoute(
                    name: "Error",
                    template: "errors/{code}",
                    defaults: new { controller = "Errors", Action = "Error" }
                    );
            });

            using (var scope = scopeFactory.CreateScope())
            {
                var initializer = scope.ServiceProvider.GetService<BlogInit>();
                initializer.SeedDataAsync().Wait();
            }

        }
    }
}
