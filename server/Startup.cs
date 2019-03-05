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

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin1",
                    builder => builder.AllowAnyOrigin());
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
            //app.UseHttpsRedirection();
            app.UseMvc();
        }

        private static void UpdateDatabase(IApplicationBuilder app)
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
    }
}
