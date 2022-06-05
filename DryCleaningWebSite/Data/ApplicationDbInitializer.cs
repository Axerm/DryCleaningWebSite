using DryCleaningWebSite.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DryCleaningWebSite.Data
{
    public static class ApplicationDbInitializer
    {
        public static async Task SeedUsers(UserManager<Employee> userManager
            , RoleManager<IdentityRole> roleManager)
        {
            if (userManager.FindByNameAsync("admin").Result == null)
            {

                if (!(await roleManager.RoleExistsAsync(ConstantValues.AdminRoleName)))
                    await roleManager.CreateAsync(new IdentityRole(ConstantValues.AdminRoleName));
                if (!(await roleManager.RoleExistsAsync(ConstantValues.ReseptionistRoleName)))
                    await roleManager.CreateAsync(new IdentityRole(ConstantValues.ReseptionistRoleName));
                if (!(await roleManager.RoleExistsAsync(ConstantValues.TechnologistRoleName)))
                    await roleManager.CreateAsync(new IdentityRole(ConstantValues.TechnologistRoleName));

                Employee admin = new Employee
                {
                    Name = "Admin",
                    FamilyName = "Admin",
                    FatherName = "Admin",
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",

                    EmailConfirmed = false,
                    PhoneNumber = "88001002030",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                };

                IdentityResult result = await userManager.CreateAsync(admin, "123123eE`");

                if (result.Succeeded)
                    await userManager.AddToRoleAsync(admin, ConstantValues.AdminRoleName);
            }
        }

        public static async Task SeedClients(ApplicationDbContext applicationDbContext)
        {
            if (applicationDbContext?.Clients?.Any() ?? true)
                return;

            Client[] clients = new Client[]
            {
                new Client()
                {
                    Address = "Client1Address",
                    DiscountId = 1,
                    FamilyName = "Александров",
                    Name = "Александр",
                    FatherName = "Александрович",
                    IsDeprecated = false,
                    IsNotifyPromotions = true,
                    TelephoneNumber = "88005553535",
                },
                new Client()
                {
                    Address = "Client2Address",
                    DiscountId = 2,
                    FamilyName = "Сергеев",
                    Name = "Сергей",
                    FatherName = "Сергеевич",
                    IsDeprecated = false,
                    IsNotifyPromotions = true,
                    TelephoneNumber = "88005553536",
                },
                new Client()
                {
                    Address = "Client3Address",
                    DiscountId = 2,
                    FamilyName = "Иванов",
                    Name = "Иван",
                    FatherName = "Иванович",
                    IsDeprecated = false,
                    IsNotifyPromotions = true,
                    TelephoneNumber = "88005553537",
                }
            };

            await applicationDbContext.Clients.AddRangeAsync(clients);
            await applicationDbContext.SaveChangesAsync();
        }

        public static async Task SeedOrders(ApplicationDbContext applicationDbContext
            , UserManager<Employee> userManager)
        {
            if (applicationDbContext?.Orders?.Any() ?? true)
                return;

            Random random = new Random();
            List<Order> orders = new List<Order>();
            int dayOfDateOfReceipt, dayOfDateOfPlainIssue;

            for (int i = 0; i < 10; ++i)
            {
                dayOfDateOfReceipt = random.Next(14, 20);
                dayOfDateOfPlainIssue = random.Next(21, 27);

                Employee employee = random.Next(1, 4) switch
                {
                    1 => await userManager.FindByNameAsync("user1"),
                    2 => await userManager.FindByNameAsync("user2"),
                    3 => await userManager.FindByNameAsync("user3"),
                    _ => await userManager.FindByNameAsync("user1"),
                };

                orders.Add(new Order()
                {
                    AdditionalCost = 0,
                    BurnoutDegreeId = 1,
                    ClientId = random.Next(1, 4),
                    DateOfPlainIssue = new DateTime(2021, 6, dayOfDateOfPlainIssue),
                    DateOfReceipt = new DateTime(2021, 6, dayOfDateOfReceipt),
                    Employee = employee,
                    FilialId = employee.FilialId,
                    GluePartsId = 1,
                    ManufacturerMarkingId = 1,
                    HasAbrasion = false,
                    HasBloodWine= false,
                    HasDeformation= false,
                    HasOilFat = false,
                    HasPaintBlackPasta = false,
                    HasShine = false,
                    HasSpotUnknownOrigin = false,
                    OperationalWearDegreeId = 1,
                    PollutionDegreeId = 1,
                    ServiceTypeId = 1,
                    StatusOrderId = random.Next(1, 8),
                    StatusPayId = 3,
                    StockId = 1,
                    ThingTypeId = 1
                });
            }

            await applicationDbContext.Orders.AddRangeAsync(orders);
            await applicationDbContext.SaveChangesAsync();
        }
    }
}