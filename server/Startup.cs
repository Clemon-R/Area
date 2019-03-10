using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.AspNetCore.Identity;
using Area.Services.App;
using Area.Models;
using System.Reflection;
using Area.Services;
using Area.Wrappers;
using Area.Services.Actions;
using Area.Services.Reactions;
using Area.Factory;
using Area.Services.Triggers;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using Area.Controllers;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json.Serialization;

namespace Area
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // This line uses 'UseSqlServer' in the 'options' parameter
            // with the connection string defined above.
            services.AddDbContext<ApplicationDbContext>();

            var resultServices = Assembly.GetExecutingAssembly().DefinedTypes.Where(x => x.GetInterfaces().Contains(typeof(IService)));
            foreach (var service in resultServices)
            {
                Console.WriteLine($"Serivce found({service.FullName})");
                services.Add(new ServiceDescriptor(service, service, ServiceLifetime.Scoped));
            }

            var resultWrappers = Assembly.GetExecutingAssembly().DefinedTypes.Where(x => x.GetInterfaces().Contains(typeof(IWrapper)));
            foreach (var wrapper in resultWrappers)
            {
                Console.WriteLine($"Wrapper found({wrapper.FullName})");
                services.Add(new ServiceDescriptor(wrapper, wrapper, ServiceLifetime.Singleton));
            }

            var resultActions = Assembly.GetExecutingAssembly().DefinedTypes.Where(x => x.GetInterfaces().Contains(typeof(IAction)));
            foreach (var wrapper in resultActions)
            {
                Console.WriteLine($"Action found({wrapper.FullName})");
                services.Add(new ServiceDescriptor(wrapper, wrapper, ServiceLifetime.Singleton));
            }

            var resultReactions = Assembly.GetExecutingAssembly().DefinedTypes.Where(x => x.GetInterfaces().Contains(typeof(IReaction)));
            foreach (var wrapper in resultReactions)
            {
                Console.WriteLine($"Reaction found({wrapper.FullName})");
                services.Add(new ServiceDescriptor(wrapper, wrapper, ServiceLifetime.Singleton));
            }

            var resultFactory = Assembly.GetExecutingAssembly().DefinedTypes.Where(x => x.GetInterfaces().Contains(typeof(IFactory)));
            foreach (var factory in resultFactory)
            {
                Console.WriteLine($"Factory found({factory.FullName})");
                services.Add(new ServiceDescriptor(factory, factory, ServiceLifetime.Singleton));
            }

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin1",
                    builder => builder.AllowAnyOrigin());
            });
            services.AddRouting();
            services.AddHttpContextAccessor();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            UpdateDatabase(app);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors("AllowOrigin");

            var rb = new RouteBuilder(app);
            RequestDelegate factorialRequestHandler = c => {
                c.Response.Headers.Add("Content-Type", "application/json");
                return c.Response.WriteAsync(this.GetAboutContent(app));
            };
            rb.MapRoute(
                template: "about.json",
                handler: factorialRequestHandler
            );
            app.UseRouter(rb.Build());
            app.UseMvc();
            LaunchBackgroundJob(app);
        }

        private void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }

        private string GetAboutContent(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                var service = serviceScope.ServiceProvider.GetService<AreaService>();
                var settings = new JsonSerializerSettings();
                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                return JsonConvert.SerializeObject(service.About(), Formatting.Indented, settings);
            }
        }

        private void LaunchBackgroundJob(IApplicationBuilder app)
        {
            var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>().CreateScope();
            var bg = serviceScope.ServiceProvider.GetService<BackgroundJobService>();
            bg.Start();
        }
    }
}
