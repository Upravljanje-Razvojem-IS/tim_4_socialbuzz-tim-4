using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Logistics.Infrastructure.Migrations
{
    public partial class RemoveMock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "ItemMockId",
                table: "Purchases");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ItemMockId",
                table: "Purchases",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ItemMock",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Item = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriceOfOne = table.Column<double>(type: "float", nullable: false),
                    WeightOfOne = table.Column<float>(type: "real", nullable: false)
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
    }
}
