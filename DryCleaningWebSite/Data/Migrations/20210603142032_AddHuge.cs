using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DryCleaningWebSite.Data.Migrations
{
    public partial class AddHuge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FamilyName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FatherName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FilialId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeprecated",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BurnoutDegrees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeprecated = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BurnoutDegrees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<int>(type: "int", nullable: false),
                    IsDeprecated = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Filials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeprecated = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GluePartss",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeprecated = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GluePartss", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ManufacturerMarkings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeprecated = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManufacturerMarkings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperationalWearDegrees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(type: "int", nullable: false),
                    IsDeprecated = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationalWearDegrees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PollutionDegrees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Degree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeprecated = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PollutionDegrees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeprecated = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatusOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeprecated = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatusPays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeprecated = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusPays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<int>(type: "int", nullable: false),
                    IsDeprecated = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ThingTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeprecated = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThingTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FamilyName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FatherName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true, computedColumnSql: "[FamilyName] + ' ' + [Name] + ' ' + [FatherName]"),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TelephoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsNotifyPromotions = table.Column<bool>(type: "bit", nullable: false),
                    DiscountId = table.Column<int>(type: "int", nullable: true),
                    IsDeprecated = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_Discounts_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FilialId = table.Column<int>(type: "int", nullable: true),
                    DateOfReceipt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfPlainIssue = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfAffectedPlainIssue = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: true),
                    ThingTypeId = table.Column<int>(type: "int", nullable: true),
                    ThingSize = table.Column<int>(type: "int", nullable: false),
                    ThingComplect = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManufacturerMarkingId = table.Column<int>(type: "int", nullable: true),
                    PollutionDegreeId = table.Column<int>(type: "int", nullable: true),
                    BurnoutDegreeId = table.Column<int>(type: "int", nullable: true),
                    GluePartsId = table.Column<int>(type: "int", nullable: true),
                    HasBloodWine = table.Column<bool>(type: "bit", nullable: false),
                    HasPaintBlackPasta = table.Column<bool>(type: "bit", nullable: false),
                    HasOilFat = table.Column<bool>(type: "bit", nullable: false),
                    HasShine = table.Column<bool>(type: "bit", nullable: false),
                    HasSpotUnknownOrigin = table.Column<bool>(type: "bit", nullable: false),
                    HasAbrasion = table.Column<bool>(type: "bit", nullable: false),
                    HasDeformation = table.Column<bool>(type: "bit", nullable: false),
                    DefectsBefore = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefectsAfter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RemovableFittings = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NonRemovableFittings = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperationalWearDegreeId = table.Column<int>(type: "int", nullable: true),
                    ServiceTypeId = table.Column<int>(type: "int", nullable: true),
                    StockId = table.Column<int>(type: "int", nullable: true),
                    StatusOrderId = table.Column<int>(type: "int", nullable: true),
                    StatusPayId = table.Column<int>(type: "int", nullable: true),
                    AdditionalCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FinalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DateOfIssue = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_BurnoutDegrees_BurnoutDegreeId",
                        column: x => x.BurnoutDegreeId,
                        principalTable: "BurnoutDegrees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Filials_FilialId",
                        column: x => x.FilialId,
                        principalTable: "Filials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_GluePartss_GluePartsId",
                        column: x => x.GluePartsId,
                        principalTable: "GluePartss",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_ManufacturerMarkings_ManufacturerMarkingId",
                        column: x => x.ManufacturerMarkingId,
                        principalTable: "ManufacturerMarkings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_OperationalWearDegrees_OperationalWearDegreeId",
                        column: x => x.OperationalWearDegreeId,
                        principalTable: "OperationalWearDegrees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_PollutionDegrees_PollutionDegreeId",
                        column: x => x.PollutionDegreeId,
                        principalTable: "PollutionDegrees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_ServiceTypes_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalTable: "ServiceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_StatusOrders_StatusOrderId",
                        column: x => x.StatusOrderId,
                        principalTable: "StatusOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_StatusPays_StatusPayId",
                        column: x => x.StatusPayId,
                        principalTable: "StatusPays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Stocks_StockId",
                        column: x => x.StockId,
                        principalTable: "Stocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_ThingTypes_ThingTypeId",
                        column: x => x.ThingTypeId,
                        principalTable: "ThingTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderAdditionalInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    OrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderAdditionalInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderAdditionalInfos_AspNetUsers_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderAdditionalInfos_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "BurnoutDegrees",
                columns: new[] { "Id", "IsDeprecated", "Name" },
                values: new object[,]
                {
                    { 1, false, "Изменение цвета" },
                    { 2, false, "Слабый" },
                    { 3, false, "Средний" }
                });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "IsDeprecated", "Value" },
                values: new object[,]
                {
                    { 1, false, 0 },
                    { 2, false, 5 },
                    { 3, false, 10 },
                    { 4, false, 25 }
                });

            migrationBuilder.InsertData(
                table: "Filials",
                columns: new[] { "Id", "Address", "IsDeprecated", "Name" },
                values: new object[,]
                {
                    { 1, "ц/п Address", false, "ц/п" },
                    { 2, "п/п 1 Address", false, "п/п 1" },
                    { 3, "п/п 2 Address", false, "п/п 2" },
                    { 4, "п/п 3 Address", false, "п/п 3" }
                });

            migrationBuilder.InsertData(
                table: "GluePartss",
                columns: new[] { "Id", "IsDeprecated", "Name" },
                values: new object[,]
                {
                    { 2, false, "Имеются" },
                    { 1, false, "Отсутствуют" }
                });

            migrationBuilder.InsertData(
                table: "ManufacturerMarkings",
                columns: new[] { "Id", "IsDeprecated", "Name" },
                values: new object[,]
                {
                    { 1, false, "Имеется" },
                    { 2, false, "Отсутствует" },
                    { 3, false, "Запрещает химическую чистку" },
                    { 4, false, "Не соответствует ГОСТу ISO 3758-2014" }
                });

            migrationBuilder.InsertData(
                table: "OperationalWearDegrees",
                columns: new[] { "Id", "IsDeprecated", "Name" },
                values: new object[,]
                {
                    { 1, false, 10 },
                    { 2, false, 30 },
                    { 3, false, 50 },
                    { 4, false, 75 },
                    { 5, false, 100 }
                });

            migrationBuilder.InsertData(
                table: "PollutionDegrees",
                columns: new[] { "Id", "Degree", "IsDeprecated" },
                values: new object[,]
                {
                    { 2, "Сильное", false },
                    { 3, "Очень сильное", false },
                    { 1, "Общее", false }
                });

            migrationBuilder.InsertData(
                table: "ServiceTypes",
                columns: new[] { "Id", "IsDeprecated", "Name" },
                values: new object[,]
                {
                    { 1, false, "Химическая чистка" },
                    { 2, false, "Аквачистка" },
                    { 3, false, "Стирка" },
                    { 4, false, "Крашение" },
                    { 5, false, "Глажение" },
                    { 6, false, "Ремонт" }
                });

            migrationBuilder.InsertData(
                table: "StatusOrders",
                columns: new[] { "Id", "IsDeprecated", "Name" },
                values: new object[,]
                {
                    { 6, false, "Отправлен на приёмный пункт" },
                    { 9, false, "Отказ от услуг химчистки" },
                    { 8, false, "Закрыт" },
                    { 7, false, "Принят приёмным пунктом" },
                    { 5, false, "Услуги оказаны" },
                    { 4, false, "Оказание услуг химчистки" },
                    { 3, false, "Принят химчисткой" },
                    { 2, false, "Отправлен в химчистку" },
                    { 1, false, "Оформлен" }
                });

            migrationBuilder.InsertData(
                table: "StatusPays",
                columns: new[] { "Id", "IsDeprecated", "Name" },
                values: new object[,]
                {
                    { 1, false, "Не оплачен" },
                    { 2, false, "Ожидает оплаты" }
                });

            migrationBuilder.InsertData(
                table: "StatusPays",
                columns: new[] { "Id", "IsDeprecated", "Name" },
                values: new object[] { 3, false, "Оплачен" });

            migrationBuilder.InsertData(
                table: "Stocks",
                columns: new[] { "Id", "IsDeprecated", "Value" },
                values: new object[,]
                {
                    { 1, false, 0 },
                    { 2, false, 15 },
                    { 3, false, 20 },
                    { 4, false, 35 }
                });

            migrationBuilder.InsertData(
                table: "ThingTypes",
                columns: new[] { "Id", "IsDeprecated", "Name", "Price" },
                values: new object[,]
                {
                    { 2, false, "Футболка", 200m },
                    { 1, false, "Пальто", 1000m },
                    { 3, false, "Джинсы", 350m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FilialId",
                table: "AspNetUsers",
                column: "FilialId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_DiscountId",
                table: "Clients",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderAdditionalInfos_EmployeeId",
                table: "OrderAdditionalInfos",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderAdditionalInfos_OrderId",
                table: "OrderAdditionalInfos",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BurnoutDegreeId",
                table: "Orders",
                column: "BurnoutDegreeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ClientId",
                table: "Orders",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_EmployeeId",
                table: "Orders",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_FilialId",
                table: "Orders",
                column: "FilialId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_GluePartsId",
                table: "Orders",
                column: "GluePartsId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ManufacturerMarkingId",
                table: "Orders",
                column: "ManufacturerMarkingId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OperationalWearDegreeId",
                table: "Orders",
                column: "OperationalWearDegreeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PollutionDegreeId",
                table: "Orders",
                column: "PollutionDegreeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ServiceTypeId",
                table: "Orders",
                column: "ServiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StatusOrderId",
                table: "Orders",
                column: "StatusOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StatusPayId",
                table: "Orders",
                column: "StatusPayId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StockId",
                table: "Orders",
                column: "StockId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ThingTypeId",
                table: "Orders",
                column: "ThingTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Filials_FilialId",
                table: "AspNetUsers",
                column: "FilialId",
                principalTable: "Filials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Filials_FilialId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "OrderAdditionalInfos");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "BurnoutDegrees");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Filials");

            migrationBuilder.DropTable(
                name: "GluePartss");

            migrationBuilder.DropTable(
                name: "ManufacturerMarkings");

            migrationBuilder.DropTable(
                name: "OperationalWearDegrees");

            migrationBuilder.DropTable(
                name: "PollutionDegrees");

            migrationBuilder.DropTable(
                name: "ServiceTypes");

            migrationBuilder.DropTable(
                name: "StatusOrders");

            migrationBuilder.DropTable(
                name: "StatusPays");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "ThingTypes");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FilialId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FamilyName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FatherName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FilialId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsDeprecated",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");
        }
    }
}
