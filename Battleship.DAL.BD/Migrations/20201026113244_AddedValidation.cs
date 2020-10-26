using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Battleship.DAL.BD.Migrations
{
    public partial class AddedValidation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int[,]>(
                name: "map",
                table: "Maps",
                nullable: false,
                oldClrType: typeof(int[,]),
                oldType: "integer[]",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int[,]>(
                name: "map",
                table: "Maps",
                type: "integer[]",
                nullable: true,
                oldClrType: typeof(int[,]));
        }
    }
}
