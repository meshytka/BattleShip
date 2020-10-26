using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Battleship.DAL.BD.Migrations
{
    public partial class InitialDBCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    idFirstPlayer = table.Column<Guid>(nullable: false),
                    statusOfGame = table.Column<int>(nullable: false),
                    frstPlayerTurn = table.Column<bool>(nullable: false),
                    idSecondPlayer = table.Column<Guid>(nullable: false),
                    mapFirstPlayer = table.Column<int[,]>(nullable: true),
                    mapSecondPlayer = table.Column<int[,]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.idFirstPlayer);
                });

            migrationBuilder.CreateTable(
                name: "Maps",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    map = table.Column<int[,]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maps", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Boards");

            migrationBuilder.DropTable(
                name: "Maps");
        }
    }
}
