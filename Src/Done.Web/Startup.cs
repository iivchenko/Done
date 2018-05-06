using Done.Core.Data;
using Done.Data;
using Done.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Done.Web
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
            string connection = Configuration.GetConnectionString("Done");
            services.AddDbContext<DoneContext>(options => options.UseSqlServer(connection));

            services
                .AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<DoneContext>();
            
            services.AddTransient<DbInitializer>();

            services.AddScoped<IRepository<Goal>, Repository<Goal>>(provider => new Repository<Goal>(provider.GetService<DoneContext>()));
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DbInitializer dbInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "goal_search",
                    template: "search/{pattern}",
                    defaults: new { controller = "Goals", action = "Index" });

                routes.MapRoute(
                    name: "goal",
                    template: "goal/{id}",
                    defaults: new { controller = "Goals", action = "Edit" });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Goals}/{action=Index}/{id?}");
            });

            // TODO: Improve shit
            dbInitializer.Initialize().Wait();
        }
    }
}
