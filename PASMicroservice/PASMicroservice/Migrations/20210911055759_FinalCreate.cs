using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PASMicroservice.Migrations
{
    public partial class FinalCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ListingTypes",
                columns: table => new
                {
                    ListingTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListingTypes", x => x.ListingTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Listings",
                columns: table => new
                {
                    ListingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: true, defaultValue: 0.0),
                    PriceContact = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    PriceDeal = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ListingTypeId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Listings", x => x.ListingId);
                    table.ForeignKey(
                        name: "FK_Listings_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Listings_ListingTypes_ListingTypeId",
                        column: x => x.ListingTypeId,
                        principalTable: "ListingTypes",
                        principalColumn: "ListingTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name", "ParentCategoryId" },
                values: new object[,]
                {
                    { new Guid("329f5f35-9ae7-4bd7-89ff-480cfa938804"), "PC komponente", null },
                    { new Guid("c1df5575-00ce-4ca8-88c0-750c9fab1772"), "Usluge | IT", null }
                });

            migrationBuilder.InsertData(
                table: "ListingTypes",
                columns: new[] { "ListingTypeId", "Name" },
                values: new object[,]
                {
                    { 1, "product" },
                    { 2, "service" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name", "ParentCategoryId" },
                values: new object[,]
                {
                    { new Guid("dcb3e419-3f9a-4f45-ae1a-df2a57e7eefa"), "Grafičke kartice", new Guid("329f5f35-9ae7-4bd7-89ff-480cfa938804") },
                    { new Guid("4c65f2f6-34f0-4440-8a7f-18a617459b7e"), "Usluge | Web development", new Guid("c1df5575-00ce-4ca8-88c0-750c9fab1772") }
                });

            migrationBuilder.InsertData(
                table: "Listings",
                columns: new[] { "ListingId", "CategoryId", "Description", "ListingTypeId", "Name", "PriceDeal", "UserId" },
                values: new object[] { new Guid("ae63dcce-07d0-468e-9940-e840ee895aac"), new Guid("329f5f35-9ae7-4bd7-89ff-480cfa938804"), null, 1, "Cooler Master CPU Hladnjak", true, 1339 });

            migrationBuilder.InsertData(
                table: "Listings",
                columns: new[] { "ListingId", "CategoryId", "Description", "ListingTypeId", "Name", "PriceContact", "UserId" },
                values: new object[] { new Guid("5d6c5c19-d166-41c3-ba84-112a542c4b0c"), new Guid("c1df5575-00ce-4ca8-88c0-750c9fab1772"), "Za vise informacija, kontaktirati", 2, "SEO optimizacija", true, 1337 });

            migrationBuilder.InsertData(
                table: "Listings",
                columns: new[] { "ListingId", "CategoryId", "Description", "ListingTypeId", "Name", "Price", "PriceContact", "PriceDeal", "UserId" },
                values: new object[] { new Guid("accbc9e4-5705-4683-b30a-40b6e5758a73"), new Guid("dcb3e419-3f9a-4f45-ae1a-df2a57e7eefa"), "polovna graficka kartica", 1, "Sapphire Nitro+ RX 580 8GB", 150.0, false, false, 1337 });

            migrationBuilder.InsertData(
                table: "Listings",
                columns: new[] { "ListingId", "CategoryId", "Description", "ListingTypeId", "Name", "Price", "PriceContact", "PriceDeal", "UserId" },
                values: new object[] { new Guid("a6a07aeb-86bb-4a15-9c70-ce4e439fb965"), new Guid("dcb3e419-3f9a-4f45-ae1a-df2a57e7eefa"), "nova graficka kartica", 1, "Gigabyte RTX 3080 Ti", 2499.9499999999998, false, false, 1338 });

            migrationBuilder.InsertData(
                table: "Listings",
                columns: new[] { "ListingId", "CategoryId", "Description", "ListingTypeId", "Name", "Price", "PriceContact", "PriceDeal", "UserId" },
                values: new object[] { new Guid("794a9f38-8e36-4f94-901f-76c395727fc5"), new Guid("4c65f2f6-34f0-4440-8a7f-18a617459b7e"), null, 2, "Izrada Wordpress veb sajta", 150.0, false, false, 1337 });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Listings_CategoryId",
                table: "Listings",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Listings_ListingTypeId",
                table: "Listings",
                column: "ListingTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Listings");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "ListingTypes");
        }
    }
}
