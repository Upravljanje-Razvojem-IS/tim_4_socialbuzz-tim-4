using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PASMicroservice.Migrations
{
    public partial class ModifiedCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ParentId = table.Column<Guid>(nullable: true),
                    TypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PAS",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: true, defaultValue: 0.0),
                    PriceContact = table.Column<bool>(nullable: true, defaultValue: false),
                    PriceDeal = table.Column<bool>(nullable: true, defaultValue: false),
                    TypeId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PAS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PASTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PASTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "ParentId", "TypeId" },
                values: new object[,]
                {
                    { new Guid("329f5f35-9ae7-4bd7-89ff-480cfa938804"), "PC komponente", null, 1 },
                    { new Guid("dcb3e419-3f9a-4f45-ae1a-df2a57e7eefa"), "Grafičke kartice", new Guid("329f5f35-9ae7-4bd7-89ff-480cfa938804"), 1 },
                    { new Guid("c1df5575-00ce-4ca8-88c0-750c9fab1772"), "Usluge | IT", null, 2 },
                    { new Guid("4c65f2f6-34f0-4440-8a7f-18a617459b7e"), "Usluge | Web development", new Guid("c1df5575-00ce-4ca8-88c0-750c9fab1772"), 2 }
                });

            migrationBuilder.InsertData(
                table: "PAS",
                columns: new[] { "Id", "CategoryId", "Description", "Name", "Price", "PriceContact", "PriceDeal", "TypeId", "UserId" },
                values: new object[,]
                {
                    { new Guid("accbc9e4-5705-4683-b30a-40b6e5758a73"), new Guid("dcb3e419-3f9a-4f45-ae1a-df2a57e7eefa"), "polovna graficka kartica", "Sapphire Nitro+ RX 580 8GB", 150.0, false, false, 1, 1337 },
                    { new Guid("a6a07aeb-86bb-4a15-9c70-ce4e439fb965"), new Guid("dcb3e419-3f9a-4f45-ae1a-df2a57e7eefa"), "nova graficka kartica", "Gigabyte RTX 3080 Ti", 2499.9499999999998, false, false, 1, 1338 }
                });

            migrationBuilder.InsertData(
                table: "PAS",
                columns: new[] { "Id", "CategoryId", "Description", "Name", "PriceDeal", "TypeId", "UserId" },
                values: new object[] { new Guid("ae63dcce-07d0-468e-9940-e840ee895aac"), new Guid("329f5f35-9ae7-4bd7-89ff-480cfa938804"), null, "Cooler Master CPU Hladnjak", true, 1, 1339 });

            migrationBuilder.InsertData(
                table: "PAS",
                columns: new[] { "Id", "CategoryId", "Description", "Name", "Price", "PriceContact", "PriceDeal", "TypeId", "UserId" },
                values: new object[] { new Guid("794a9f38-8e36-4f94-901f-76c395727fc5"), new Guid("4c65f2f6-34f0-4440-8a7f-18a617459b7e"), null, "Izrada Wordpress veb sajta", 150.0, false, false, 2, 1337 });

            migrationBuilder.InsertData(
                table: "PAS",
                columns: new[] { "Id", "CategoryId", "Description", "Name", "PriceContact", "TypeId", "UserId" },
                values: new object[] { new Guid("5d6c5c19-d166-41c3-ba84-112a542c4b0c"), new Guid("c1df5575-00ce-4ca8-88c0-750c9fab1772"), "Za vise informacija, kontaktirati", "SEO optimizacija", true, 2, 1337 });

            migrationBuilder.InsertData(
                table: "PASTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "product" },
                    { 2, "service" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "PAS");

            migrationBuilder.DropTable(
                name: "PASTypes");
        }
    }
}
