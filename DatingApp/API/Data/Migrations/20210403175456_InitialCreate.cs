using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                    //DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    //KnownAs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    //Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    //LastActive = table.Column<DateTime>(type: "datetime2", nullable: false),
                    //Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    //Introduction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    //LookingFor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    //Interests = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    //City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    //Country = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
