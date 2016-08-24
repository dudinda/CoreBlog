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

namespace CoreBlog.Web
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
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<BlogContext>();
            services.AddIdentity<BlogUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;

            })
            .AddEntityFrameworkStores<BlogContext>()
            .AddDefaultTokenProviders();

            services.AddLogging();

            services.AddScoped<IBlogContext, BlogContext>();
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
        public void Configure(IApplicationBuilder app,
                                    IHostingEnvironment env,
                                    ILoggerFactory loggerFactory,
                                    IServiceScopeFactory scopeFactory)
        {
            loggerFactory.AddConsole(LogLevel.Information);
            loggerFactory.AddDebug(LogLevel.Error);
            

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/errors/500");
            }

            app.UseStatusCodePagesWithReExecute("/errors/{0}");
            app.UseStaticFiles();

            app.UseIdentity();

            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<RegistrationViewModel, BlogUser>();
                config.CreateMap<Post, PostViewModel>().ReverseMap();
                config.CreateMap<Tag, TagViewModel>().ReverseMap();
                config.CreateMap<Category, CategoryViewModel>().ReverseMap();
                config.CreateMap<CreatePostViewModel, Post>().ReverseMap();
                config.CreateMap<Post, PostControlPanelViewModel>().ReverseMap();
                config.CreateMap<BlogUser, UserControlPanelViewModel>().ReverseMap();
                config.CreateMap<Image, ImageViewModel>().ReverseMap();
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
