using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QualityRanking.Migrations
{
    public partial class Fixrating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Rankings",
                newName: "RaterId");

            migrationBuilder.AddColumn<Guid>(
                name: "RateeId",
                table: "Rankings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RateeId",
                table: "Rankings");

            migrationBuilder.RenameColumn(
                name: "RaterId",
                table: "Rankings",
                newName: "UserId");
        }
    }
}
