using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Logistics.Infrastructure.Migrations
{
    public partial class DistancePrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_DeliveryServices_DeliveryServiceId",
                table: "Purchases");

            migrationBuilder.DropTable(
                name: "DeliveryServices");

            migrationBuilder.RenameColumn(
                name: "DeliveryServiceId",
                table: "Purchases",
                newName: "DistancePriceId");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_DeliveryServiceId",
                table: "Purchases",
                newName: "IX_Purchases_DistancePriceId");

            migrationBuilder.CreateTable(
                name: "DistancePrices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MinimalDistance = table.Column<int>(type: "int", nullable: false),
                    MaximalDistance = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistancePrices", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_DistancePrices_DistancePriceId",
                table: "Purchases",
                column: "DistancePriceId",
                principalTable: "DistancePrices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_DistancePrices_DistancePriceId",
                table: "Purchases");

            migrationBuilder.DropTable(
                name: "DistancePrices");

            migrationBuilder.RenameColumn(
                name: "DistancePriceId",
                table: "Purchases",
                newName: "DeliveryServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_DistancePriceId",
                table: "Purchases",
                newName: "IX_Purchases_DeliveryServiceId");

            migrationBuilder.CreateTable(
                name: "DeliveryServices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryServices", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_DeliveryServices_DeliveryServiceId",
                table: "Purchases",
                column: "DeliveryServiceId",
                principalTable: "DeliveryServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
