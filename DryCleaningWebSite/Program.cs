using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DryCleaningWebSite.Data;
using DryCleaningWebSite.Models;

namespace DryCleaningWebSite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

            //using (var scope = host.Services.CreateScope())
            //{
            //    var services = scope.ServiceProvider;
            //    try
            //    {
            //        var userManager = services.GetRequiredService<UserManager<Employee>>();
            //        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            //        var context = services.GetRequiredService<ApplicationDbContext>();
            //        await UsersAndRolesInitiolizer.InitializeAsync(context, userManager, roleManager);
            //        //await TransportListsInitiolizer.InitializeAsync(context);
            //        await OrdersInitiolizer.InitializeAsync(context, userManager);
            //        await ClietnAndDiscountInitiolizer.InitializeAsync(context);
            //    }
            //    catch (Exception ex)
            //    {
            //        var logger = services.GetRequiredService<ILogger<Program>>();
            //        logger.LogError(ex, "При инициализации информацией базы данных возникла ошибка");
            //    }
            //}

            //host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
