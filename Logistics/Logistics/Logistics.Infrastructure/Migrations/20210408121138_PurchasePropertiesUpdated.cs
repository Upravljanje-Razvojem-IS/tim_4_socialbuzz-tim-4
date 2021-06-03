using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Logistics.Infrastructure.Migrations
{
    public partial class PurchasePropertiesUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ToAddress",
                table: "Purchases");

            migrationBuilder.RenameColumn(
                name: "Weight",
                table: "Purchases",
                newName: "WeightOfOne");

            migrationBuilder.AddColumn<float>(
                name: "PriceOfOne",
                table: "Purchases",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "TotalPriceWithWeightAndDistance",
                table: "Purchases",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "TotalWeight",
                table: "Purchases",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_ToAddressId",
                table: "Purchases",
                column: "ToAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Addresses_ToAddressId",
                table: "Purchases",
                column: "ToAddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Addresses_ToAddressId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_ToAddressId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "PriceOfOne",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "TotalPriceWithWeightAndDistance",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "TotalWeight",
                table: "Purchases");

            migrationBuilder.RenameColumn(
                name: "WeightOfOne",
                table: "Purchases",
                newName: "Weight");

            migrationBuilder.AddColumn<Guid>(
                name: "ToAddress",
                table: "Purchases",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
