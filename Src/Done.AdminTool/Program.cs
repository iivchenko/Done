using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using System;
using Done.Core.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Done.Data;

namespace Done.AdminTool
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateApp(ConfigureServices()).Execute(args);
        }

        private static CommandLineApplication CreateApp(IServiceProvider services)
        {
            var app = new CommandLineApplication();
            app.Name = "done";
            app.HelpOption("-?|-h|--help");

            app.Command("initialize", (command) =>
            {                
                command.Description = "Create default roles (Administrators, Users), create default Administrator(pass: 1111) within Administrators role.";
                command.HelpOption("-?|-h|--help");            
                command.OnExecute(async () =>
                    {
                        Console.WriteLine("Start db initialization");

                        var userManager = services.GetService<UserManager<IdentityUser>>();
                        var roleManager = services.GetService<RoleManager<IdentityRole>>();

                        var administrator = new IdentityUser
                        {
                            UserName = "Administrator",
                            NormalizedUserName = "Administrator"
                        };

                        await roleManager.CreateAsync(new IdentityRole{ Name = "Administrators" });
                        await roleManager.CreateAsync(new IdentityRole{ Name = "Users" });
                        await userManager.CreateAsync(administrator, "1111");
                        await userManager.AddToRoleAsync(administrator, "Administrators");

                        Console.WriteLine("Finished db initialization");
                        return 0;
                    });
            });

            return app;
        }

        private static IServiceProvider ConfigureServices()
        {
            var collection = new ServiceCollection();

            var connection = "Server=.;Database=Done; Integrated Security=SSPI";
            collection
                .AddDbContext<DoneContext>(options => options.UseSqlServer(connection));

            collection
                .AddIdentity<IdentityUser, IdentityRole>
                (
                    options => 
                    {
                        options.Password.RequiredLength = 4;
                        options.Password.RequireLowercase = false;
                        options.Password.RequireUppercase = false;
                        options.Password.RequireNonAlphanumeric = false;
                        options.Password.RequireDigit = false;
                    }
                )
                .AddEntityFrameworkStores<DoneContext>()
                .AddDefaultTokenProviders();

            return collection.BuildServiceProvider();
        }        
    }
}
