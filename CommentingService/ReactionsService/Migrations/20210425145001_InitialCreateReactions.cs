using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReactionsService.Migrations
{
    public partial class InitialCreateReactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reactions",
                columns: table => new
                {
                    ReactionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostID = table.Column<int>(type: "int", nullable: false),
                    TypeOfReactionID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reactions", x => x.ReactionID);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfReaction",
                columns: table => new
                {
                    TypeOfReactionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReactionType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfReaction", x => x.TypeOfReactionID);
                });

            migrationBuilder.InsertData(
                table: "Reactions",
                columns: new[] { "ReactionID", "PostID", "TypeOfReactionID", "UserID" },
                values: new object[] { new Guid("8ca02e0f-a565-43d7-b8d1-da0a073118fb"), 1, 1, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reactions");

            migrationBuilder.DropTable(
                name: "TypeOfReaction");
        }
    }
}
