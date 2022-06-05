using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using DryCleaningWebSite.Data;
using DryCleaningWebSite.Models;

namespace DryCleaningWebSite.Data
{
    public class ApplicationDbContext : IdentityDbContext<Employee>
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Filial> Filials { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ThingType> ThingTypes { get; set; }
        public DbSet<ManufacturerMarking> ManufacturerMarkings { get; set; }
        public DbSet<PollutionDegree> PollutionDegrees { get; set; }
        public DbSet<BurnoutDegree> BurnoutDegrees { get; set; }
        public DbSet<GlueParts> GluePartss { get; set; }
        public DbSet<OperationalWearDegree> OperationalWearDegrees { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StatusOrder> StatusOrders { get; set; }
        public DbSet<StatusPay> StatusPays { get; set; }
        public DbSet<OrderAdditionalInfo> OrderAdditionalInfos { get; set; }
        public DbSet<Discount> Discounts { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            //_userManager = userManager;

            //Database.EnsureDeleted();
            //Database.EnsureCreated();

            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Client>()
                .Property(u => u.FullName)
                .HasComputedColumnSql("[FamilyName] + ' ' + [Name] + ' ' + [FatherName]");

            SeedSecondaryData(modelBuilder);
        }

        public void SeedSecondaryData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ManufacturerMarking>().HasData(
                new ManufacturerMarking[]
                {
                    new ManufacturerMarking { Id=1, Name="Имеется", IsDeprecated = false },
                    new ManufacturerMarking { Id=2, Name="Отсутствует", IsDeprecated = false },
                    new ManufacturerMarking { Id=3, Name="Запрещает химическую чистку", IsDeprecated = false },
                    new ManufacturerMarking { Id=4, Name="Не соответствует ГОСТу ISO 3758-2014", IsDeprecated = false }
                });

            modelBuilder.Entity<Discount>().HasData(
                new Discount[]
                {
                    new Discount() { Id=1, Value = 0, IsDeprecated = false },
                    new Discount() { Id=2, Value = 5, IsDeprecated = false },
                    new Discount() { Id=3, Value = 10, IsDeprecated = false },
                    new Discount() { Id=4, Value = 25, IsDeprecated = false }
                });

            modelBuilder.Entity<Filial>().HasData(
                new Filial[]
                {
                    new Filial() { Id=1, Name = "ц/п", Address = "ц/п Address", IsDeprecated = false },
                    new Filial() { Id=2, Name = "п/п 1", Address = "п/п 1 Address", IsDeprecated = false },
                    new Filial() { Id=3, Name = "п/п 2", Address = "п/п 2 Address", IsDeprecated = false },
                    new Filial() { Id=4, Name = "п/п 3", Address = "п/п 3 Address", IsDeprecated = false }
                });

            modelBuilder.Entity<GlueParts>().HasData(
                new GlueParts[]
                {
                    new GlueParts() { Id=1, Name = "Отсутствуют", IsDeprecated = false},
                    new GlueParts() { Id=2, Name = "Имеются", IsDeprecated = false }
                });

            modelBuilder.Entity<OperationalWearDegree>().HasData(
                new OperationalWearDegree[]
                {
                    new OperationalWearDegree() { Id=1, Name = 10, IsDeprecated = false},
                    new OperationalWearDegree() { Id=2, Name = 30, IsDeprecated = false},
                    new OperationalWearDegree() { Id=3, Name = 50, IsDeprecated = false},
                    new OperationalWearDegree() { Id=4, Name = 75, IsDeprecated = false},
                    new OperationalWearDegree() { Id=5, Name = 100, IsDeprecated = false},
                });

            modelBuilder.Entity<PollutionDegree>().HasData(
                new PollutionDegree[]
                {
                    new PollutionDegree() { Id=1, Degree = "Общее", IsDeprecated = false},
                    new PollutionDegree() { Id=2, Degree = "Сильное", IsDeprecated = false },
                    new PollutionDegree() { Id=3, Degree = "Очень сильное", IsDeprecated = false }
                });

            modelBuilder.Entity<ServiceType>().HasData(
                new ServiceType[]
                {
                    new ServiceType() { Id=1, Name = "Химическая чистка", IsDeprecated = false},
                    new ServiceType() { Id=2, Name = "Аквачистка", IsDeprecated = false },
                    new ServiceType() { Id=3, Name = "Стирка", IsDeprecated = false },
                    new ServiceType() { Id=4, Name = "Крашение", IsDeprecated = false },
                    new ServiceType() { Id=5, Name = "Глажение", IsDeprecated = false },
                    new ServiceType() { Id=6, Name = "Ремонт", IsDeprecated = false }
                });

            modelBuilder.Entity<StatusOrder>().HasData(
                new StatusOrder[]
                {
                    new StatusOrder() { Id=1, Name = "Оформлен", IsDeprecated = false},
                    new StatusOrder() { Id=2, Name = "Отправлен в химчистку", IsDeprecated = false },
                    new StatusOrder() { Id=3, Name = "Принят химчисткой", IsDeprecated = false },
                    new StatusOrder() { Id=4, Name = "Оказание услуг химчистки", IsDeprecated = false },
                    new StatusOrder() { Id=5, Name = "Услуги оказаны", IsDeprecated = false },
                    new StatusOrder() { Id=6, Name = "Отправлен на приёмный пункт", IsDeprecated = false },
                    new StatusOrder() { Id=7, Name = "Принят приёмным пунктом", IsDeprecated = false },
                    new StatusOrder() { Id=8, Name = "Закрыт", IsDeprecated = false },
                    new StatusOrder() { Id=9, Name = "Отказ от услуг химчистки", IsDeprecated = false }
                });

            modelBuilder.Entity<StatusPay>().HasData(
                new StatusPay[]
                {
                    new StatusPay() { Id=1, Name = "Не оплачен", IsDeprecated = false},
                    new StatusPay() { Id=2, Name = "Ожидает оплаты", IsDeprecated = false },
                    new StatusPay() { Id=3, Name = "Оплачен", IsDeprecated = false }
                });

            modelBuilder.Entity<Stock>().HasData(
                new Stock[]
                {
                    new Stock() { Id=1, Value = 0, IsDeprecated = false },
                    new Stock() { Id=2, Value = 15, IsDeprecated = false },
                    new Stock() { Id=3, Value = 20, IsDeprecated = false },
                    new Stock() { Id=4, Value = 35, IsDeprecated = false }
                });

            modelBuilder.Entity<ThingType>().HasData(
                new ThingType[]
                {
                    new ThingType() { Id=1, Name = "Пальто", Price = 1000, IsDeprecated = false},
                    new ThingType() { Id=2, Name = "Футболка", Price = 200, IsDeprecated = false },
                    new ThingType() { Id=3, Name = "Джинсы", Price = 350, IsDeprecated = false }
                });

            modelBuilder.Entity<BurnoutDegree>().HasData(
                new BurnoutDegree[]
                {
                    new BurnoutDegree() { Id=1, Name = "Изменение цвета", IsDeprecated = false},
                    new BurnoutDegree() { Id=2, Name = "Слабый", IsDeprecated = false },
                    new BurnoutDegree() { Id=3, Name = "Средний", IsDeprecated = false }
                });
        }
    }
}
