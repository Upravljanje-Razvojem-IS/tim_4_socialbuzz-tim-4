using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Logistics.Infrastructure.Migrations
{
    public partial class EntitiesConfigurationRefactoring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Addresses_FromAddessId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_FromAddessId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "FromAddessId",
                table: "Purchases");

            migrationBuilder.AlterColumn<string>(
                name: "PostalCode",
                table: "Cities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Cities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_FromAddressId",
                table: "Purchases",
                column: "FromAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Addresses_FromAddressId",
                table: "Purchases",
                column: "FromAddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Addresses_FromAddressId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_FromAddressId",
                table: "Purchases");

            migrationBuilder.AddColumn<Guid>(
                name: "FromAddessId",
                table: "Purchases",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PostalCode",
                table: "Cities",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Cities",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_FromAddessId",
                table: "Purchases",
                column: "FromAddessId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Addresses_FromAddessId",
                table: "Purchases",
                column: "FromAddessId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
