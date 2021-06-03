using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Logistics.Infrastructure.Migrations
{
    public partial class Coordinates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Item",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "PriceOfOne",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "WeightOfOne",
                table: "Purchases");

            migrationBuilder.AddColumn<Guid>(
                name: "ItemId",
                table: "Purchases",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ItemMockId",
                table: "Purchases",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Longitude",
                table: "Cities",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Latitude",
                table: "Cities",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ItemMock",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Item = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeightOfOne = table.Column<float>(type: "real", nullable: false),
                    PriceOfOne = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemMock", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_ItemMockId",
                table: "Purchases",
                column: "ItemMockId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_ItemMock_ItemMockId",
                table: "Purchases",
                column: "ItemMockId",
                principalTable: "ItemMock",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_ItemMock_ItemMockId",
                table: "Purchases");

            migrationBuilder.DropTable(
                name: "ItemMock");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_ItemMockId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "ItemMockId",
                table: "Purchases");

            migrationBuilder.AddColumn<string>(
                name: "Item",
                table: "Purchases",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "PriceOfOne",
                table: "Purchases",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "WeightOfOne",
                table: "Purchases",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AlterColumn<string>(
                name: "Longitude",
                table: "Cities",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "Latitude",
                table: "Cities",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
