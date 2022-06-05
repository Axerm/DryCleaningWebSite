using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DryCleaningWebSite.Models;

namespace DryCleaningWebSite.Data
{
    public static class OrdersInitiolizer
    {
        public static async Task InitializeAsync(ApplicationDbContext _applicationDbContext
            , UserManager<Employee> _userManager)
        {
            var orders = _applicationDbContext.Orders
                .Include(o => o.Employee)
                    .ThenInclude(e => e.Filial)
                .Include(o => o.Employee)
                    .ThenInclude(e => e.OrderAdditionalInfos)
                .Include(c => c.Client)
                    .ThenInclude(c => c.Discount)
                .Include(f => f.Filial)
                    .ThenInclude(f => f.Employees)
                .Include(f => f.Filial)
                .Include(t => t.ThingType)
                .Include(m => m.ManufacturerMarking)
                .Include(p => p.PollutionDegree)
                .Include(b => b.BurnoutDegree)
                .Include(g => g.GlueParts)
                .Include(o => o.OperationalWearDegree)
                .Include(s => s.ServiceType)
                .Include(s => s.Stock)
                .Include(s => s.StatusOrder)
                .Include(s => s.StatusPay)
                .Include(o => o.OrderAdditionalInfos)
                    .ThenInclude(o => o.Employee)
                .AsQueryable();

            var foundOrder = await _applicationDbContext.Orders.FirstOrDefaultAsync(o => o.Id == 1);

            if (foundOrder == null)
            {
                Random random = new Random();

                DateTime date;
                Employee employee = await _userManager.FindByNameAsync("user1r");

                for (int i = 0; i < 10; ++i)
                {
                    date = new DateTime(2021, 06, random.Next(1, 10)).Date;

                    Order order1 = new Order()
                    {
                        AdditionalCost = 100,
                        BurnoutDegree = await _applicationDbContext.BurnoutDegrees.FirstOrDefaultAsync(b => b.Id == 1),
                        Client = await _applicationDbContext.Clients.FirstOrDefaultAsync(b => b.Id == 1),
                        DateOfReceipt = date,
                        DateOfPlainIssue = date.AddDays(random.Next(1, 10)).Date,
                        Employee = employee,
                        Filial = await _applicationDbContext.Filials.FirstOrDefaultAsync(b => b.Id == 1),
                        GlueParts = await _applicationDbContext.GluePartss.FirstOrDefaultAsync(b => b.Id == 1),
                        HasAbrasion = false,
                        HasBloodWine = false,
                        HasDeformation = false,
                        HasOilFat = false,
                        HasPaintBlackPasta = false,
                        HasShine = false,
                        HasSpotUnknownOrigin = false,
                        ManufacturerMarking = await _applicationDbContext.ManufacturerMarkings.FirstOrDefaultAsync(b => b.Id == 1),
                        OperationalWearDegree = await _applicationDbContext.OperationalWearDegrees.FirstOrDefaultAsync(b => b.Id == 1),
                        PollutionDegree = await _applicationDbContext.PollutionDegrees.FirstOrDefaultAsync(b => b.Id == 1),
                        ServiceType = await _applicationDbContext.ServiceTypes.FirstOrDefaultAsync(b => b.Id == 1),
                        StatusOrder = await _applicationDbContext.StatusOrders.FirstOrDefaultAsync(b => b.Id == 1),
                        StatusPay = await _applicationDbContext.StatusPays.FirstOrDefaultAsync(b => b.Id == 1),
                        Stock = await _applicationDbContext.Stocks.FirstOrDefaultAsync(b => b.Id == 1),
                        ThingType = await _applicationDbContext.ThingTypes.FirstOrDefaultAsync(b => b.Id == 1),
                        ThingSize = 45,
                    };

                    await _applicationDbContext.Orders.AddAsync(order1);

                    OrderAdditionalInfo[] orderAdditionalInfos1 = new OrderAdditionalInfo[]
                    {
                        new OrderAdditionalInfo() { Data = "Правка 1", Employee = employee, Order = order1 },
                        new OrderAdditionalInfo() { Data = "Правка 2", Employee = employee, Order = order1 },
                        new OrderAdditionalInfo() { Data = "Правка 3", Employee = employee, Order = order1 },
                    };

                    await _applicationDbContext.OrderAdditionalInfos.AddRangeAsync(orderAdditionalInfos1);

                    //-------------------------------------------------------------------------------------------

                    date = new DateTime(2021, 06, random.Next(1, 10)).Date;

                    Order order2 = new Order()
                    {
                        AdditionalCost = 100,
                        BurnoutDegree = await _applicationDbContext.BurnoutDegrees.FirstOrDefaultAsync(b => b.Id == 2),
                        Client = await _applicationDbContext.Clients.FirstOrDefaultAsync(b => b.Id == 2),
                        DateOfReceipt = date,
                        DateOfPlainIssue = date.AddDays(random.Next(1, 10)).Date,
                        Employee = employee,
                        Filial = await _applicationDbContext.Filials.FirstOrDefaultAsync(b => b.Id == 2),
                        GlueParts = await _applicationDbContext.GluePartss.FirstOrDefaultAsync(b => b.Id == 2),
                        HasAbrasion = false,
                        HasBloodWine = false,
                        HasDeformation = true,
                        HasOilFat = false,
                        HasPaintBlackPasta = false,
                        HasShine = true,
                        HasSpotUnknownOrigin = false,
                        ManufacturerMarking = await _applicationDbContext.ManufacturerMarkings.FirstOrDefaultAsync(b => b.Id == 2),
                        OperationalWearDegree = await _applicationDbContext.OperationalWearDegrees.FirstOrDefaultAsync(b => b.Id == 2),
                        PollutionDegree = await _applicationDbContext.PollutionDegrees.FirstOrDefaultAsync(b => b.Id == 2),
                        ServiceType = await _applicationDbContext.ServiceTypes.FirstOrDefaultAsync(b => b.Id == 2),
                        StatusOrder = await _applicationDbContext.StatusOrders.FirstOrDefaultAsync(b => b.Id == 2),
                        StatusPay = await _applicationDbContext.StatusPays.FirstOrDefaultAsync(b => b.Id == 1),
                        Stock = await _applicationDbContext.Stocks.FirstOrDefaultAsync(b => b.Id == 1),
                        ThingType = await _applicationDbContext.ThingTypes.FirstOrDefaultAsync(b => b.Id == 2),
                        ThingSize = 50,
                    };

                    await _applicationDbContext.Orders.AddAsync(order2);

                    OrderAdditionalInfo[] orderAdditionalInfos2 = new OrderAdditionalInfo[]
                    {
                        new OrderAdditionalInfo() { Data = "Правка 1", Employee = employee, Order = order2 },
                        new OrderAdditionalInfo() { Data = "Правка 2", Employee = employee, Order = order2 },
                        new OrderAdditionalInfo() { Data = "Правка 3", Employee = employee, Order = order2 },
                    };

                    await _applicationDbContext.OrderAdditionalInfos.AddRangeAsync(orderAdditionalInfos2);
                }

                _applicationDbContext.SaveChanges();

            }
        }
    }
}